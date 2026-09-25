using AssemblyLineTester.Data;
using AssemblyLineTester.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AssemblyLineTester.Services
{
    // CRUD for harness assemblies, their linked connectors, and their image folder.
    public static class HarnessService
    {
        private static readonly string[] AllowedImageExtensions = { ".jpg", ".jpeg", ".png" };
        private const long MaxImageSizeBytes = 2 * 1024 * 1024;
        private static readonly string HarnessImageRoot = Path.Combine("Assets", "img", "harness");

        // Resolves a relative image path to an existing absolute path.
        public static string? ResolveImagePath(string? relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath)) return null;

            if (Path.IsPathRooted(relativePath) && File.Exists(relativePath)) return relativePath;

            string candidateApp = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
            if (File.Exists(candidateApp)) return candidateApp;

            string imagesFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "AssemblyLineTester", "Images");
            string candidateAppData = Path.Combine(imagesFolder, relativePath);
            return File.Exists(candidateAppData) ? candidateAppData : null;
        }

        // Loads every harness assembly with its linked connectors.
        public static List<HarnessAssembly> GetAllHarnesses()
        {
            using var db = new AppDbContext();
            var list = db.HarnessAssemblies
                .Include(h => h.Connectors)
                .OrderBy(h => h.Name)
                .ThenBy(h => h.PartNumber)
                .ToList();
            foreach (var h in list) EnsureInclusionListMatches(h);
            return list;
        }

        public static HarnessAssembly? GetHarnessById(int id)
        {
            using var db = new AppDbContext();
            var h = db.HarnessAssemblies
                .Include(h => h.Connectors)
                .FirstOrDefault(h => h.Id == id);
            EnsureInclusionListMatches(h);
            return h;
        }

        private static void EnsureInclusionListMatches(HarnessAssembly? harness)
        {
            if (harness == null) return;
            harness.IncludedForReport ??= new List<bool>();
            harness.ImagePath ??= new List<string>();
            while (harness.IncludedForReport.Count < harness.ImagePath.Count)
            {
                harness.IncludedForReport.Add(false);
            }
            if (harness.IncludedForReport.Count > harness.ImagePath.Count)
            {
                harness.IncludedForReport = harness.IncludedForReport.Take(harness.ImagePath.Count).ToList();
            }
        }

        // Full path of every image belonging to one harness assembly.
        public static List<string> GetHarnessImagePaths(HarnessAssembly? harness)
        {
            var result = new List<string>();
            if (harness == null || harness.ImagePath == null || harness.ImagePath.Count == 0) return result;

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            foreach (var path in harness.ImagePath)
            {
                if (string.IsNullOrWhiteSpace(path)) continue;
                string fullPath = Path.IsPathRooted(path) ? path : Path.Combine(baseDir, path);

                if (File.Exists(fullPath))
                {
                    result.Add(fullPath);
                }
                else
                {
                    string? resolved = ResolveImagePath(path);
                    if (resolved != null && File.Exists(resolved))
                    {
                        result.Add(resolved);
                    }
                    else
                    {
                        result.Add(path);
                    }
                }
            }

            return result;
        }

        // Inserts a new harness assembly into the database and copies its images in.
        public static (HarnessAssembly? Harness, string? Error) AddHarness(
            HarnessAssembly harness,
            List<int>? connectorIds = null,
            List<string>? sourceImagePaths = null,
            List<bool>? includedForReport = null)
        {
            if (!SessionManager.HasPermission("Action.HarnessDatabase.Edit"))
                return (null, "You do not have permission to modify harness records.");

            string? validationError = Validate(harness);
            if (validationError != null) return (null, validationError);

            string? imageError = ValidateImages(sourceImagePaths);
            if (imageError != null) return (null, imageError);

            using var db = new AppDbContext();

            harness.Id = 0;
            harness.ImagePath ??= new List<string>();
            harness.IncludedForReport ??= new List<bool>();

            if (connectorIds != null && connectorIds.Count > 0)
            {
                harness.Connectors = db.Connectors.Where(c => connectorIds.Contains(c.Id)).ToList();
            }

            db.HarnessAssemblies.Add(harness);
            db.SaveChanges();

            if (sourceImagePaths != null && sourceImagePaths.Count > 0)
            {
                var (savedPaths, error) = SaveImagesToFolder(harness, sourceImagePaths);
                if (error != null)
                {
                    db.HarnessAssemblies.Remove(harness);
                    db.SaveChanges();
                    return (null, error);
                }

                harness.ImagePath = savedPaths;
                if (includedForReport != null)
                {
                    harness.IncludedForReport = includedForReport.Take(savedPaths.Count).ToList();
                }
                while (harness.IncludedForReport.Count < harness.ImagePath.Count)
                {
                    harness.IncludedForReport.Add(false);
                }
                db.SaveChanges();
            }

            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Info, "Product Database", "Create Harness",
                $"Harness '{harness.Name}' ({harness.PartNumber}) created with {harness.Connectors.Count} connectors and {harness.ImagePath.Count} images.");

            return (harness, null);
        }

        // Updates fields, syncs connectors, and syncs images with an ordered list (preserving index positions).
        public static (bool Success, string? Error) UpdateHarnessOrdered(
            HarnessAssembly harness,
            List<int>? connectorIds,
            List<string>? orderedImageItems,
            List<bool>? includedForReport = null)
        {
            if (!SessionManager.HasPermission("Action.HarnessDatabase.Edit"))
                return (false, "You do not have permission to modify harness records.");

            string? validationError = Validate(harness);
            if (validationError != null) return (false, validationError);

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            orderedImageItems ??= new List<string>();

            // Validate any external/new source files
            var newSources = orderedImageItems
                .Where(p => Path.IsPathRooted(p) && !p.StartsWith(baseDir, StringComparison.OrdinalIgnoreCase))
                .ToList();

            string? imageError = ValidateImages(newSources);
            if (imageError != null) return (false, imageError);

            using var db = new AppDbContext();
            var existing = db.HarnessAssemblies
                .Include(h => h.Connectors)
                .FirstOrDefault(h => h.Id == harness.Id);

            if (existing == null) return (false, "Harness Assembly not found.");

            existing.Name = harness.Name;
            existing.PartNumber = harness.PartNumber;
            existing.Description = harness.Description;
            existing.ImagePath ??= new List<string>();

            // Sync connectors
            existing.Connectors.Clear();
            if (connectorIds != null && connectorIds.Count > 0)
            {
                var connectorsToAttach = db.Connectors.Where(c => connectorIds.Contains(c.Id)).ToList();
                existing.Connectors.AddRange(connectorsToAttach);
            }

            // Identify removed images
            var currentPathsNormalized = existing.ImagePath.ToDictionary(
                p => (Path.IsPathRooted(p) ? p : Path.Combine(baseDir, p)).Replace('\\', '/').ToLowerInvariant(),
                p => p);

            var orderedNormalized = orderedImageItems
                .Select(p => (Path.IsPathRooted(p) ? p : Path.Combine(baseDir, p)).Replace('\\', '/').ToLowerInvariant())
                .ToHashSet();

            var removedPaths = currentPathsNormalized
                .Where(kvp => !orderedNormalized.Contains(kvp.Key))
                .Select(kvp => kvp.Value)
                .ToList();

            if (removedPaths.Count > 0)
            {
                DeleteRemovedImages(existing, removedPaths);
            }

            // Folder setup for copying any new images
            string partFolderName = SanitizeFolderName(string.IsNullOrWhiteSpace(existing.PartNumber) ? $"Harness_{existing.Id}" : existing.PartNumber);
            string relativeFolder = Path.Combine(HarnessImageRoot, partFolderName);
            string fullFolder = Path.Combine(baseDir, relativeFolder);
            Directory.CreateDirectory(fullFolder);

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

                string fullItemPath = Path.IsPathRooted(item) ? item : Path.Combine(baseDir, item);

                // If this is an existing file already inside the harness folder
                if (fullItemPath.StartsWith(fullFolder, StringComparison.OrdinalIgnoreCase) && File.Exists(fullItemPath))
                {
                    string rel = Path.GetRelativePath(baseDir, fullItemPath);
                    finalPaths.Add(rel);
                }
                else if (File.Exists(fullItemPath))
                {
                    // It's a new file from outside, copy it in
                    string ext = Path.GetExtension(fullItemPath).ToLowerInvariant();
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
                        File.Copy(fullItemPath, destPath, overwrite: false);
                        string relPath = Path.Combine(relativeFolder, destFileName);
                        finalPaths.Add(relPath);
                        fileIndex++;
                    }
                    catch (Exception ex)
                    {
                        FileLogService.Warn("HarnessService.UpdateHarnessOrdered",
                            $"Image '{fullItemPath}' could not be copied: {ex.Message}");
                        return (false, $"Could not save image {Path.GetFileName(fullItemPath)}: {ex.Message}");
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
                Models.SystemLog.LogSeverity.Info, "Product Database", "Update Harness",
                $"Harness '{existing.Name}' ({existing.PartNumber}) updated with {existing.Connectors.Count} connectors and {existing.ImagePath.Count} images.");

            return (true, null);
        }

        // Deletes the harness assembly row and its image folder, checking ProductModel dependencies first.
        public static (bool Success, string? Error) DeleteHarness(int harnessId)
        {
            if (!SessionManager.HasPermission("Action.HarnessDatabase.Edit"))
                return (false, "You do not have permission to modify harness records.");

            using var db = new AppDbContext();
            var existing = db.HarnessAssemblies.FirstOrDefault(h => h.Id == harnessId);
            if (existing == null) return (false, "Harness Assembly not found.");

            // Check if harness assembly is used by any ProductModel
            var usingProducts = db.ProductModels
                .Where(p => p.HarnessAssemblies.Any(h => h.Id == harnessId))
                .Select(p => $"{p.Name} ({p.PartNumber})")
                .ToList();

            if (usingProducts.Count > 0)
            {
                var displayList = usingProducts.Take(5).ToList();
                string listText = "• " + string.Join("\n• ", displayList);
                if (usingProducts.Count > 5)
                {
                    listText += $"\n...and {usingProducts.Count - 5} more";
                }

                return (false, $"Cannot delete harness assembly '{existing.Name}' because it is currently used in the following Product Models:\n\n{listText}\n\nPlease detach it from these product models before deleting.");
            }

            string name = existing.Name;
            string partNumber = existing.PartNumber;

            db.HarnessAssemblies.Remove(existing);
            db.SaveChanges();

            // Clean up files and folder
            string partFolderName = SanitizeFolderName(partNumber);
            string folderFull = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, HarnessImageRoot, partFolderName);
            if (Directory.Exists(folderFull))
            {
                try
                {
                    Directory.Delete(folderFull, recursive: true);
                }
                catch (Exception ex)
                {
                    FileLogService.Warn("HarnessService.DeleteHarness",
                        $"Harness image folder '{folderFull}' could not be deleted: {ex.Message}");
                }
            }

            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Warning, "Product Database", "Delete Harness",
                $"Harness '{name}' ({partNumber}) deleted.");

            return (true, null);
        }

        private static string? Validate(HarnessAssembly harness)
        {
            if (string.IsNullOrWhiteSpace(harness.Name) && string.IsNullOrWhiteSpace(harness.PartNumber))
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

        private static void DeleteRemovedImages(HarnessAssembly harness, List<string> removedImageFileNames)
        {
            if (removedImageFileNames.Count == 0 || harness.ImagePath == null) return;

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var remaining = new List<string>();

            foreach (var path in harness.ImagePath)
            {
                string fileName = Path.GetFileName(path);
                if (removedImageFileNames.Contains(fileName) || removedImageFileNames.Contains(path))
                {
                    string fullPath = Path.IsPathRooted(path) ? path : Path.Combine(baseDir, path);
                    if (File.Exists(fullPath))
                    {
                        try { File.Delete(fullPath); } catch { }
                    }
                }
                else
                {
                    remaining.Add(path);
                }
            }

            harness.ImagePath = remaining;
        }

        private static (List<string> SavedRelativePaths, string? Error) SaveImagesToFolder(HarnessAssembly harness, List<string> sourceImagePaths)
        {
            var savedRelativePaths = new List<string>();
            string partFolderName = SanitizeFolderName(string.IsNullOrWhiteSpace(harness.PartNumber) ? $"Harness_{harness.Id}" : harness.PartNumber);
            string relativeFolder = Path.Combine(HarnessImageRoot, partFolderName);
            string fullFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativeFolder);
            Directory.CreateDirectory(fullFolder);

            int index = 1;
            var existingFiles = Directory.GetFiles(fullFolder);
            while (existingFiles.Any(f => Path.GetFileNameWithoutExtension(f) == $"{harness.Id}_{index}"))
            {
                index++;
            }

            foreach (var sourcePath in sourceImagePaths)
            {
                if (!File.Exists(sourcePath)) continue;

                string ext = Path.GetExtension(sourcePath).ToLowerInvariant();
                string destFileName = $"{harness.Id}_{index}{ext}";
                string destPath = Path.Combine(fullFolder, destFileName);

                while (File.Exists(destPath))
                {
                    index++;
                    destFileName = $"{harness.Id}_{index}{ext}";
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
                    FileLogService.Warn("HarnessService.SaveImagesToFolder",
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
            return string.IsNullOrEmpty(cleaned) ? "Harness" : cleaned;
        }
    }
}