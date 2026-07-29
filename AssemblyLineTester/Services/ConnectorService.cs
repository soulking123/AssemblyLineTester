using AssemblyLineTester.Data;
using AssemblyLineTester.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AssemblyLineTester.Services
{
    // CRUD for connectors plus the image folder each one owns.
    public static class ConnectorService
    {
        private static readonly string[] AllowedImageExtensions = { ".jpg", ".jpeg", ".png" };
        private const long MaxImageSizeBytes = 2 * 1024 * 1024;
        private static readonly string ConnectorImageRoot = Path.Combine("Assets", "img", "connector");

        // The whole shared catalog, for the add-connector picker.
        public static List<Connector> GetAllConnectors()
        {
            using var db = new AppDbContext();
            return db.Connectors.OrderBy(c => c.PartNumber).ToList();
        }

        // Full path of every image belonging to one connector.
        public static List<string> GetConnectorImagePaths(Connector? connector)
        {
            var result = new List<string>();
            if (connector == null || string.IsNullOrWhiteSpace(connector.ImagePath)) return result;

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string fullPath = Path.Combine(baseDir, connector.ImagePath);

            if (Directory.Exists(fullPath))
            {
                // Folder is keyed by PartNumber, which siblings share, so filter by id prefix.
                result.AddRange(
                    Directory.GetFiles(fullPath)
                        .Where(f => AllowedImageExtensions.Contains(Path.GetExtension(f).ToLowerInvariant()))
                        .Where(f => Path.GetFileName(f).StartsWith($"{connector.ConnectorId}_", StringComparison.Ordinal))
                        .OrderBy(f => f));
            }
            else if (File.Exists(fullPath))
            {
                result.Add(fullPath);
            }

            return result;
        }

        // Inserts a connector into the shared catalog and copies its images in.
        public static (Connector? Connector, string? Error) AddConnector(Connector connector, List<string> sourceImagePaths)
        {
            string? validationError = Validate(connector);
            if (validationError != null) return (null, validationError);

            // Validated before the insert, or a rejected image leaves a committed row behind.
            string? imageError = ValidateImages(sourceImagePaths);
            if (imageError != null) return (null, imageError);

            using var db = new AppDbContext();

            connector.ConnectorId = 0;
            connector.ImagePath = null;
            db.Connectors.Add(connector);
            db.SaveChanges();

            if (sourceImagePaths.Count > 0)
            {
                var (relativeFolder, error) = SaveImagesToFolder(connector, sourceImagePaths);
                if (error != null)
                {
                    // The copy failed, so the row it belongs to goes with it.
                    db.Connectors.Remove(connector);
                    db.SaveChanges();
                    return (null, error);
                }

                connector.ImagePath = relativeFolder;
                db.SaveChanges();
            }

            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Info, "Harness Database", "Create Connector",
                $"Connector '{connector.PartNumber}' created with {sourceImagePaths.Count} images.");

            return (connector, null);
        }

        // Updates fields, copies in new images, then removes the ones the user unticked.
        public static (bool Success, string? Error) UpdateConnector(Connector connector, List<string> newSourceImagePaths, List<string> removedImageFileNames)
        {
            string? validationError = Validate(connector);
            if (validationError != null) return (false, validationError);

            string? imageError = ValidateImages(newSourceImagePaths);
            if (imageError != null) return (false, imageError);

            using var db = new AppDbContext();
            var existing = db.Connectors.FirstOrDefault(c => c.ConnectorId == connector.ConnectorId);
            if (existing == null) return (false, "Connector not found.");

            existing.Perf = connector.Perf;
            existing.PartNumber = connector.PartNumber;
            existing.ContactType = connector.ContactType;
            existing.ContactQuantity = connector.ContactQuantity;
            existing.WireSize = connector.WireSize;
            existing.CurrentRating = connector.CurrentRating;
            existing.MountingStyle = connector.MountingStyle;

            string? folderBeforeUpdate = existing.ImagePath;

            if (newSourceImagePaths.Count > 0)
            {
                var (relativeFolder, error) = SaveImagesToFolder(existing, newSourceImagePaths);
                if (error != null) return (false, error);
                existing.ImagePath = relativeFolder;
            }

            db.SaveChanges();

            // Deleted last, after the commit: a stray copy is recoverable, a deleted image is not.
            DeleteRemovedImages(folderBeforeUpdate, removedImageFileNames);

            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Info, "Harness Database", "Update Connector",
                $"Connector '{existing.PartNumber}' updated: {newSourceImagePaths.Count} images added, {removedImageFileNames.Count} removed.");

            return (true, null);
        }

        // Deletes the connector row and its image folder.
        public static (bool Success, string? Error) DeleteConnector(int connectorId)
        {
            using var db = new AppDbContext();
            var existing = db.Connectors.FirstOrDefault(c => c.ConnectorId == connectorId);
            if (existing == null) return (false, "Connector not found.");

            string? imagePath = existing.ImagePath;
            string partNumber = existing.PartNumber;

            db.Connectors.Remove(existing);
            db.SaveChanges();

            if (!string.IsNullOrWhiteSpace(imagePath))
            {
                string folderFull = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imagePath);
                if (Directory.Exists(folderFull))
                {
                    try
                    {
                        Directory.Delete(folderFull, recursive: true);
                    }
                    catch (Exception ex)
                    {
                        FileLogService.Warn("ConnectorService.DeleteConnector",
                            $"Connector row removed but image folder '{folderFull}' could not be deleted: {ex.Message}");
                    }
                }
            }

            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Warning, "Harness Database", "Delete Connector",
                $"Connector '{partNumber}' deleted.");

            return (true, null);
        }

        private static string? Validate(Connector connector)
        {
            if (string.IsNullOrWhiteSpace(connector.Perf)) return "Perf is required.";
            if (string.IsNullOrWhiteSpace(connector.PartNumber)) return "Part Number is required.";
            return null;
        }

        // Returns null when every file is acceptable, otherwise the first problem found.
        private static string? ValidateImages(List<string> sourceImagePaths)
        {
            foreach (var sourcePath in sourceImagePaths)
            {
                if (!File.Exists(sourcePath)) continue;

                if (!AllowedImageExtensions.Contains(Path.GetExtension(sourcePath).ToLowerInvariant()))
                    return $"Unsupported image type: {Path.GetFileName(sourcePath)}";

                if (new FileInfo(sourcePath).Length > MaxImageSizeBytes)
                    return $"Image too large (max 2MB): {Path.GetFileName(sourcePath)}";
            }

            return null;
        }

        // Removes the unticked files. Never throws - the row is already saved.
        private static void DeleteRemovedImages(string? relativeFolder, List<string> removedImageFileNames)
        {
            if (removedImageFileNames.Count == 0 || string.IsNullOrWhiteSpace(relativeFolder)) return;

            string folderFull = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativeFolder);
            if (!Directory.Exists(folderFull)) return;

            foreach (var fileName in removedImageFileNames)
            {
                string target = Path.Combine(folderFull, fileName);
                if (!File.Exists(target)) continue;

                try
                {
                    File.Delete(target);
                }
                catch (Exception ex)
                {
                    FileLogService.Warn("ConnectorService.DeleteRemovedImages",
                        $"Image '{target}' could not be deleted: {ex.Message}");
                }
            }
        }

        // Copies images into Assets/img/connector/{PartNumber}/ as {ConnectorId}_{i}.ext.
        private static (string? RelativeFolderPath, string? Error) SaveImagesToFolder(Connector connector, List<string> sourceImagePaths)
        {
            string partFolderName = SanitizeFolderName(connector.PartNumber);
            string relativeFolder = Path.Combine(ConnectorImageRoot, partFolderName);
            string fullFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativeFolder);
            Directory.CreateDirectory(fullFolder);

            int index = 1;
            var existingFiles = Directory.GetFiles(fullFolder);
            while (existingFiles.Any(f => Path.GetFileNameWithoutExtension(f) == $"{connector.ConnectorId}_{index}"))
            {
                index++;
            }

            foreach (var sourcePath in sourceImagePaths)
            {
                if (!File.Exists(sourcePath)) continue;

                string ext = Path.GetExtension(sourcePath).ToLowerInvariant();

                string destFileName = $"{connector.ConnectorId}_{index}{ext}";
                string destPath = Path.Combine(fullFolder, destFileName);

                while (File.Exists(destPath))
                {
                    index++;
                    destFileName = $"{connector.ConnectorId}_{index}{ext}";
                    destPath = Path.Combine(fullFolder, destFileName);
                }

                try
                {
                    File.Copy(sourcePath, destPath, overwrite: false);
                }
                catch (Exception ex)
                {
                    FileLogService.Warn("ConnectorService.SaveImagesToFolder",
                        $"Image '{sourcePath}' could not be copied: {ex.Message}");
                    return (null, $"Could not save image {Path.GetFileName(sourcePath)}: {ex.Message}");
                }

                index++;
            }

            return (relativeFolder, null);
        }

        private static string SanitizeFolderName(string name)
        {
            var invalid = Path.GetInvalidFileNameChars();
            var cleaned = new string(name.Select(ch => invalid.Contains(ch) || ch == ' ' ? '_' : ch).ToArray()).Trim('_');
            return string.IsNullOrEmpty(cleaned) ? "Connector" : cleaned;
        }
    }
}