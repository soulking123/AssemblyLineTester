using AssemblyLineTester.Data;
using AssemblyLineTester.Models;
using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AssemblyLineTester.Services
{
    /// Handles CRUD for Connector records and the image folder that belongs to each connector.
    /// Every connector gets its own folder at Assets/img/connector/{PartNumber}/,
    /// created automatically the first time images are attached.
    public class ConnectorService
    {
        private static readonly string[] AllowedImageExtensions = { ".jpg", ".jpeg", ".png" };
        private const long MaxImageSizeBytes = 2 * 1024 * 1024; // 2MB
        private static readonly string ConnectorImageRoot = Path.Combine("Assets", "img", "connector");

        public List<Connector> GetConnectorsByHarness(int harnessId)
        {
            using var db = new AppDbContext();
            return db.Connectors
                .Where(c => c.ProductHarnessId == harnessId)
                .OrderBy(c => c.PartNumber)
                .ToList();
        }

        public Connector? GetById(int connectorId)
        {
            using var db = new AppDbContext();
            return db.Connectors.FirstOrDefault(c => c.ConnectorId == connectorId);
        }

        public List<Connector> GetAllConnectors()
        {
            using var db = new AppDbContext();
            return db.Connectors.ToList();
        }

        /// Returns the full path of every image belonging to a connector.
        public List<string> GetConnectorImagePaths(Connector? connector)
        {
            var result = new List<string>();
            if (connector == null || string.IsNullOrWhiteSpace(connector.ImagePath)) return result;

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string fullPath = Path.Combine(baseDir, connector.ImagePath);

            if (Directory.Exists(fullPath))
            {
                result.AddRange(
                    Directory.GetFiles(fullPath)
                        .Where(f => AllowedImageExtensions.Contains(Path.GetExtension(f).ToLowerInvariant()))
                        .OrderBy(f => f));
            }
            else if (File.Exists(fullPath))
            {
                result.Add(fullPath); // legacy fallback
            }

            return result;
        }

        /// Inserts a new connector, creates Assets/img/connector/{PartNumber}/ folder,
        /// and copies images with filename pattern: {ConnectorId}_{i}.ext
        public (Connector? Connector, string? Error) AddConnector(Connector connector, List<string> sourceImagePaths)
        {
            string? validationError = Validate(connector);
            if (validationError != null) return (null, validationError);

            using var db = new AppDbContext();

            connector.ConnectorId = 0;
            connector.ImagePath = null;
            db.Connectors.Add(connector);
            db.SaveChanges(); // ConnectorId is now populated

            if (sourceImagePaths.Count > 0)
            {
                var (relativeFolder, error) = SaveImagesToFolder(connector, sourceImagePaths);
                if (error != null) return (null, error);

                connector.ImagePath = relativeFolder;
                db.SaveChanges();
            }

            return (connector, null);
        }

        /// Updates a connector's fields, deletes any images the user removed, and copies in new images.
        public (bool Success, string? Error) UpdateConnector(Connector connector, List<string> newSourceImagePaths, List<string> removedImageFileNames)
        {
            string? validationError = Validate(connector);
            if (validationError != null) return (false, validationError);

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

            if (removedImageFileNames.Count > 0 && !string.IsNullOrWhiteSpace(existing.ImagePath))
            {
                string folderFull = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, existing.ImagePath);
                if (Directory.Exists(folderFull))
                {
                    foreach (var fileName in removedImageFileNames)
                    {
                        string target = Path.Combine(folderFull, fileName);
                        if (File.Exists(target))
                        {
                            try { File.Delete(target); }
                            catch { /* file locked/in use - non-fatal */ }
                        }
                    }
                }
            }

            if (newSourceImagePaths.Count > 0)
            {
                var (relativeFolder, error) = SaveImagesToFolder(existing, newSourceImagePaths);
                if (error != null) return (false, error);
                existing.ImagePath = relativeFolder;
            }

            db.SaveChanges();
            return (true, null);
        }

        /// Deletes the connector row and its image folder.
        public (bool Success, string? Error) DeleteConnector(int connectorId)
        {
            using var db = new AppDbContext();
            var existing = db.Connectors.FirstOrDefault(c => c.ConnectorId == connectorId);
            if (existing == null) return (false, "Connector not found.");

            string? imagePath = existing.ImagePath;

            db.Connectors.Remove(existing);
            db.SaveChanges();

            if (!string.IsNullOrWhiteSpace(imagePath))
            {
                string folderFull = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imagePath);
                if (Directory.Exists(folderFull))
                {
                    try { Directory.Delete(folderFull, recursive: true); }
                    catch { /* non-fatal if a file is locked */ }
                }
            }

            return (true, null);
        }

        private static string? Validate(Connector connector)
        {
            if (string.IsNullOrWhiteSpace(connector.Perf)) return "Perf is required.";
            if (string.IsNullOrWhiteSpace(connector.PartNumber)) return "Part Number is required.";
            if (connector.ProductHarnessId <= 0) return "Connector must belong to a harness.";
            return null;
        }

        /// Creates folder: Assets/img/connector/{PartNumber}/
        /// and saves images as: {ConnectorId}_{i}.ext (e.g. 15_1.jpg, 15_2.png)
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
                if (!AllowedImageExtensions.Contains(ext))
                    return (null, $"Unsupported image type: {Path.GetFileName(sourcePath)}");

                var info = new FileInfo(sourcePath);
                if (info.Length > MaxImageSizeBytes)
                    return (null, $"Image too large (max 2MB): {Path.GetFileName(sourcePath)}");

                // Penamaan baru: {connectorId}_{i}.ext
                string destFileName = $"{connector.ConnectorId}_{index}{ext}";
                string destPath = Path.Combine(fullFolder, destFileName);

                while (File.Exists(destPath))
                {
                    index++;
                    destFileName = $"{connector.ConnectorId}_{index}{ext}";
                    destPath = Path.Combine(fullFolder, destFileName);
                }

                File.Copy(sourcePath, destPath, overwrite: false);
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