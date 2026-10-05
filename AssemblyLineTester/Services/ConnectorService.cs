using AssemblyLineTester.Data;
using AssemblyLineTester.Helpers;
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

        public static string? ResolveImagePath(string? relativePath) => ImageStorageHelper.ResolveImagePath(relativePath);

        public static List<Connector> GetAllConnectors()
        {
            using var db = new AppDbContext();
            var list = db.Connectors.OrderBy(c => c.Name).ThenBy(c => c.PartNumber).ToList();
            foreach (var c in list) EnsureInclusionListMatches(c);
            return list;
        }

        public static Connector? GetConnectorById(int id)
        {
            using var db = new AppDbContext();
            var c = db.Connectors.FirstOrDefault(c => c.Id == id);
            EnsureInclusionListMatches(c);
            return c;
        }

        private static void EnsureInclusionListMatches(Connector? connector)
        {
            if (connector == null) return;
            connector.IncludedForReport ??= new List<bool>();
            connector.ImagePath ??= new List<string>();
            while (connector.IncludedForReport.Count < connector.ImagePath.Count)
            {
                connector.IncludedForReport.Add(false);
            }
            if (connector.IncludedForReport.Count > connector.ImagePath.Count)
            {
                connector.IncludedForReport = connector.IncludedForReport.Take(connector.ImagePath.Count).ToList();
            }
        }

        // Connectors linked to a harness assembly.
        public static List<Connector> GetConnectorsByHarness(int harnessId)
        {
            using var db = new AppDbContext();
            var harness = db.HarnessAssemblies
                .Include(h => h.Connectors)
                .FirstOrDefault(h => h.Id == harnessId);
            return harness?.Connectors ?? new List<Connector>();
        }

        // Full path of every image belonging to one connector.
        public static List<string> GetConnectorImagePaths(Connector? connector)
        {
            if (connector == null || connector.ImagePath == null || connector.ImagePath.Count == 0) return new List<string>();
            return ImageStorageHelper.ResolveImagePaths(connector.ImagePath);
        }

        // Inserts a connector into the shared catalog and copies its images in.
        public static (Connector? Connector, string? Error) AddConnector(Connector connector, List<string>? sourceImagePaths = null, List<bool>? includedForReport = null)
        {
            if (!SessionManager.HasPermission("Action.HarnessDatabase.Edit"))
                return (null, "You do not have permission to modify connector records.");

            string? validationError = Validate(connector);
            if (validationError != null) return (null, validationError);

            string? imageError = ValidateImages(sourceImagePaths);
            if (imageError != null) return (null, imageError);

            using var db = new AppDbContext();

            connector.Id = 0;
            connector.ImagePath ??= new List<string>();
            connector.IncludedForReport ??= new List<bool>();
            db.Connectors.Add(connector);
            db.SaveChanges();

            if (sourceImagePaths != null && sourceImagePaths.Count > 0)
            {
                var (savedPaths, error) = SaveImagesToFolder(connector, sourceImagePaths);
                if (error != null)
                {
                    db.Connectors.Remove(connector);
                    db.SaveChanges();
                    return (null, error);
                }

                connector.ImagePath = savedPaths;
                if (includedForReport != null)
                {
                    connector.IncludedForReport = includedForReport.Take(savedPaths.Count).ToList();
                }
                while (connector.IncludedForReport.Count < connector.ImagePath.Count)
                {
                    connector.IncludedForReport.Add(false);
                }
                db.SaveChanges();
            }

            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Info, "Product Database", "Create Connector",
                $"Connector '{connector.PartNumber}' created with {connector.ImagePath.Count} images.");

            return (connector, null);
        }

        // Updates fields, copies in new images, then removes unticked ones.
        public static (bool Success, string? Error) UpdateConnector(Connector connector, List<string> newSourceImagePaths, List<string> removedImageFileNames)
        {
            if (!SessionManager.HasPermission("Action.HarnessDatabase.Edit"))
                return (false, "You do not have permission to modify connector records.");

            string? validationError = Validate(connector);
            if (validationError != null) return (false, validationError);

            string? imageError = ValidateImages(newSourceImagePaths);
            if (imageError != null) return (false, imageError);

            using var db = new AppDbContext();
            var existing = db.Connectors.FirstOrDefault(c => c.Id == connector.Id);
            if (existing == null) return (false, "Connector not found.");

            existing.Name = connector.Name;
            existing.PartNumber = connector.PartNumber;
            existing.TotalPin = connector.TotalPin;
            existing.Type = connector.Type;
            existing.Description = connector.Description;

            existing.ImagePath ??= new List<string>();

            // Save new images
            if (newSourceImagePaths != null && newSourceImagePaths.Count > 0)
            {
                var (savedPaths, error) = SaveImagesToFolder(existing, newSourceImagePaths);
                if (error != null) return (false, error);
                existing.ImagePath.AddRange(savedPaths);
            }

            // Remove unselected images
            if (removedImageFileNames != null && removedImageFileNames.Count > 0)
            {
                DeleteRemovedImages(existing, removedImageFileNames);
            }

            db.SaveChanges();

            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Info, "Product Database", "Update Connector",
                $"Connector '{existing.PartNumber}' updated.");

            return (true, null);
        }

        // Updates fields and syncs images with an ordered list (preserving index positions).
        public static (bool Success, string? Error) UpdateConnectorOrdered(Connector connector, List<string> orderedImageItems, List<bool>? includedForReport = null)
        {
            if (!SessionManager.HasPermission("Action.HarnessDatabase.Edit"))
                return (false, "You do not have permission to modify connector records.");

            string? validationError = Validate(connector);
            if (validationError != null) return (false, validationError);

            orderedImageItems ??= new List<string>();

            using var db = new AppDbContext();
            var existing = db.Connectors.FirstOrDefault(c => c.Id == connector.Id);
            if (existing == null) return (false, "Connector not found.");

            existing.Name = connector.Name;
            existing.PartNumber = connector.PartNumber;
            existing.TotalPin = connector.TotalPin;
            existing.Type = connector.Type;
            existing.Description = connector.Description;
            existing.ImagePath ??= new List<string>();

            string partFolderName = SanitizeFolderName(string.IsNullOrWhiteSpace(existing.PartNumber) ? $"Connector_{existing.Id}" : existing.PartNumber);
            string relativeFolder = Path.Combine(ConnectorImageRoot, partFolderName);
            string fullFolder = ImageStorageHelper.GetWritableFolder(relativeFolder);

            // Validate any external/new source files
            var newSources = orderedImageItems
                .Where(p => Path.IsPathRooted(p) && !ImageStorageHelper.IsManagedImageFile(p, relativeFolder))
                .ToList();

            string? imageError = ValidateImages(newSources);
            if (imageError != null) return (false, imageError);

            // Identify removed images
            var currentPathsNormalized = existing.ImagePath.ToDictionary(
                p => (ImageStorageHelper.ResolveImagePath(p) ?? p).Replace('\\', '/').ToLowerInvariant(),
                p => p);

            var orderedNormalized = orderedImageItems
                .Select(p => (ImageStorageHelper.ResolveImagePath(p) ?? p).Replace('\\', '/').ToLowerInvariant())
                .ToHashSet();

            var removedPaths = currentPathsNormalized
                .Where(kvp => !orderedNormalized.Contains(kvp.Key))
                .Select(kvp => kvp.Value)
                .ToList();

            if (removedPaths.Count > 0)
            {
                DeleteRemovedImages(existing, removedPaths);
            }

            int fileIndex = 1;
            var existingFiles = Directory.GetFiles(fullFolder);
            while (existingFiles.Any(f => Path.GetFileNameWithoutExtension(f) == $"{existing.Id}_{fileIndex}"))
            {
                fileIndex++;
            }

            var finalPaths = new List<string>();
            foreach (var item in orderedImageItems)
            {
                if (string.IsNullOrWhiteSpace(item)) continue;

                // If this is an existing file already inside the connector folder
                if (ImageStorageHelper.IsManagedImageFile(item, relativeFolder) && File.Exists(item))
                {
                    string rel = ImageStorageHelper.ToRelativePath(item, relativeFolder);
                    finalPaths.Add(rel);
                }
                else if (File.Exists(item))
                {
                    // It's a new file from outside, copy it in
                    string ext = Path.GetExtension(item).ToLowerInvariant();
                    string destFileName = $"{existing.Id}_{fileIndex}{ext}";
                    string destPath = Path.Combine(fullFolder, destFileName);

                    while (File.Exists(destPath))
                    {
                        fileIndex++;
                        destFileName = $"{existing.Id}_{fileIndex}{ext}";
                        destPath = Path.Combine(fullFolder, destFileName);
                    }

                    try
                    {
                        File.Copy(item, destPath, overwrite: false);
                        string relPath = Path.Combine(relativeFolder, destFileName);
                        finalPaths.Add(relPath);
                        fileIndex++;
                    }
                    catch (Exception ex)
                    {
                        FileLogService.Warn("ConnectorService.UpdateConnectorOrdered",
                            $"Image '{item}' could not be copied: {ex.Message}");
                        return (false, $"Could not save image {Path.GetFileName(item)}: {ex.Message}");
                    }
                }
                else
                {
                    finalPaths.Add(item);
                }
            }

            existing.ImagePath = finalPaths;
            if (includedForReport != null)
            {
                existing.IncludedForReport = includedForReport.Take(finalPaths.Count).ToList();
            }
            existing.IncludedForReport ??= new List<bool>();
            while (existing.IncludedForReport.Count < existing.ImagePath.Count)
            {
                existing.IncludedForReport.Add(false);
            }
            db.SaveChanges();

            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Info, "Product Database", "Update Connector",
                $"Connector '{existing.PartNumber}' updated with {existing.ImagePath.Count} images.");

            return (true, null);
        }

        // Deletes the connector row and its image files.
        public static (bool Success, string? Error) DeleteConnector(int connectorId)
        {
            if (!SessionManager.HasPermission("Action.HarnessDatabase.Edit"))
                return (false, "You do not have permission to modify connector records.");

            using var db = new AppDbContext();
            var existing = db.Connectors.FirstOrDefault(c => c.Id == connectorId);
            if (existing == null) return (false, "Connector not found.");

            // Check if connector is used by any harness assembly
            var usingHarnesses = db.HarnessAssemblies
                .Where(h => h.Connectors.Any(c => c.Id == connectorId))
                .Select(h => $"{h.Name} ({h.PartNumber})")
                .ToList();

            if (usingHarnesses.Count > 0)
            {
                var displayList = usingHarnesses.Take(5).ToList();
                string listText = "• " + string.Join("\n• ", displayList);
                if (usingHarnesses.Count > 5)
                {
                    listText += $"\n...and {usingHarnesses.Count - 5} more";
                }

                return (false, $"Cannot delete connector '{existing.PartNumber}' because it is currently used in the following Harness Assemblies:\n\n{listText}\n\nPlease detach it from these harnesses before deleting.");
            }

            string partNumber = existing.PartNumber;
            var imagesToDelete = existing.ImagePath?.ToList() ?? new List<string>();

            db.Connectors.Remove(existing);
            db.SaveChanges();

            // Clean up files and folder
            string partFolderName = SanitizeFolderName(partNumber);
            string relativeFolder = Path.Combine(ConnectorImageRoot, partFolderName);
            ImageStorageHelper.DeleteFolder(relativeFolder);

            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Warning, "Product Database", "Delete Connector",
                $"Connector '{partNumber}' deleted.");

            return (true, null);
        }

        private static string? Validate(Connector connector)
        {
            if (string.IsNullOrWhiteSpace(connector.Name) && string.IsNullOrWhiteSpace(connector.PartNumber))
                return "Name or Part Number is required.";
            return null;
        }

        private static string? ValidateImages(List<string>? sourceImagePaths)
        {
            if (sourceImagePaths == null) return null;

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

        private static void DeleteRemovedImages(Connector connector, List<string> removedImageFileNames)
        {
            if (removedImageFileNames.Count == 0 || connector.ImagePath == null) return;

            var remaining = new List<string>();

            foreach (var path in connector.ImagePath)
            {
                string fileName = Path.GetFileName(path);
                if (removedImageFileNames.Contains(fileName) || removedImageFileNames.Contains(path))
                {
                    ImageStorageHelper.DeleteImageFile(path);
                }
                else
                {
                    remaining.Add(path);
                }
            }

            connector.ImagePath = remaining;
        }

        private static (List<string> SavedRelativePaths, string? Error) SaveImagesToFolder(Connector connector, List<string> sourceImagePaths)
        {
            var savedRelativePaths = new List<string>();
            string partFolderName = SanitizeFolderName(string.IsNullOrWhiteSpace(connector.PartNumber) ? $"Connector_{connector.Id}" : connector.PartNumber);
            string relativeFolder = Path.Combine(ConnectorImageRoot, partFolderName);
            string fullFolder = ImageStorageHelper.GetWritableFolder(relativeFolder);

            int index = 1;
            var existingFiles = Directory.GetFiles(fullFolder);
            while (existingFiles.Any(f => Path.GetFileNameWithoutExtension(f) == $"{connector.Id}_{index}"))
            {
                index++;
            }

            foreach (var sourcePath in sourceImagePaths)
            {
                if (!File.Exists(sourcePath)) continue;

                string ext = Path.GetExtension(sourcePath).ToLowerInvariant();
                string destFileName = $"{connector.Id}_{index}{ext}";
                string destPath = Path.Combine(fullFolder, destFileName);

                while (File.Exists(destPath))
                {
                    index++;
                    destFileName = $"{connector.Id}_{index}{ext}";
                    destPath = Path.Combine(fullFolder, destFileName);
                }

                try
                {
                    File.Copy(sourcePath, destPath, overwrite: false);
                    string relPath = Path.Combine(relativeFolder, destFileName);
                    savedRelativePaths.Add(relPath);
                }
                catch (Exception ex)
                {
                    FileLogService.Warn("ConnectorService.SaveImagesToFolder",
                        $"Image '{sourcePath}' could not be copied: {ex.Message}");
                    return (savedRelativePaths, $"Could not save image {Path.GetFileName(sourcePath)}: {ex.Message}");
                }

                index++;
            }

            return (savedRelativePaths, null);
        }

        private static string SanitizeFolderName(string name)
        {
            var invalid = Path.GetInvalidFileNameChars();
            var cleaned = new string(name.Select(ch => invalid.Contains(ch) || ch == ' ' ? '_' : ch).ToArray()).Trim('_');
            return string.IsNullOrEmpty(cleaned) ? "Connector" : cleaned;
        }
    }
}