using AssemblyLineTester.Data;
using AssemblyLineTester.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AssemblyLineTester.Services
{
    // CRUD for product models, their linked harness assemblies, and their image folder.
    public static class ProductModelService
    {
        private static readonly string[] AllowedImageExtensions = { ".jpg", ".jpeg", ".png" };
        private const long MaxImageSizeBytes = 2 * 1024 * 1024;
        private static readonly string ProductModelImageRoot = Path.Combine("Assets", "img", "product_model");

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

        // Loads every product model with its linked harness assemblies.
        public static List<ProductModel> GetAllProductModels()
        {
            using var db = new AppDbContext();
            return db.ProductModels
                .Include(p => p.HarnessAssemblies)
                .OrderBy(p => p.Name)
                .ThenBy(p => p.PartNumber)
                .ToList();
        }

        public static ProductModel? GetProductModelById(int id)
        {
            using var db = new AppDbContext();
            return db.ProductModels
                .Include(p => p.HarnessAssemblies)
                .FirstOrDefault(p => p.Id == id);
        }

        // Full path of every image belonging to one product model.
        public static List<string> GetProductModelImagePaths(ProductModel? product)
        {
            var result = new List<string>();
            if (product == null || product.ImagePath == null || product.ImagePath.Count == 0) return result;

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            foreach (var path in product.ImagePath)
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

        // Inserts a new product model into the database and copies its images in.
        public static (ProductModel? Product, string? Error) AddProductModel(
            ProductModel product,
            List<int>? harnessIds = null,
            List<string>? sourceImagePaths = null)
        {
            if (!SessionManager.HasPermission("Action.HarnessDatabase.Edit"))
                return (null, "You do not have permission to modify product model records.");

            string? validationError = Validate(product);
            if (validationError != null) return (null, validationError);

            string? imageError = ValidateImages(sourceImagePaths);
            if (imageError != null) return (null, imageError);

            using var db = new AppDbContext();

            product.Id = 0;
            product.ImagePath ??= new List<string>();

            if (harnessIds != null && harnessIds.Count > 0)
            {
                product.HarnessAssemblies = db.HarnessAssemblies.Where(h => harnessIds.Contains(h.Id)).ToList();
            }

            db.ProductModels.Add(product);
            db.SaveChanges();

            if (sourceImagePaths != null && sourceImagePaths.Count > 0)
            {
                var (savedPaths, error) = SaveImagesToFolder(product, sourceImagePaths);
                if (error != null)
                {
                    db.ProductModels.Remove(product);
                    db.SaveChanges();
                    return (null, error);
                }

                product.ImagePath = savedPaths;
                db.SaveChanges();
            }

            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Info, "Product Database", "Create Product Model",
                $"Product Model '{product.Name}' ({product.PartNumber}) created with {product.HarnessAssemblies.Count} harnesses and {product.ImagePath.Count} images.");

            return (product, null);
        }

        // Updates fields, syncs harness assemblies, and syncs images with an ordered list (preserving index positions).
        public static (bool Success, string? Error) UpdateProductModelOrdered(
            ProductModel product,
            List<int>? harnessIds,
            List<string>? orderedImageItems)
        {
            if (!SessionManager.HasPermission("Action.HarnessDatabase.Edit"))
                return (false, "You do not have permission to modify product model records.");

            string? validationError = Validate(product);
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
            var existing = db.ProductModels
                .Include(p => p.HarnessAssemblies)
                .FirstOrDefault(p => p.Id == product.Id);

            if (existing == null) return (false, "Product Model not found.");

            existing.Name = product.Name;
            existing.PartNumber = product.PartNumber;
            existing.Description = product.Description;
            existing.ImagePath ??= new List<string>();

            // Sync harness assemblies
            existing.HarnessAssemblies.Clear();
            if (harnessIds != null && harnessIds.Count > 0)
            {
                var harnessesToAttach = db.HarnessAssemblies.Where(h => harnessIds.Contains(h.Id)).ToList();
                existing.HarnessAssemblies.AddRange(harnessesToAttach);
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
            string partFolderName = SanitizeFolderName(string.IsNullOrWhiteSpace(existing.PartNumber) ? $"Product_{existing.Id}" : existing.PartNumber);
            string relativeFolder = Path.Combine(ProductModelImageRoot, partFolderName);
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

                // If this is an existing file already inside the product folder
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
                        FileLogService.Warn("ProductModelService.UpdateProductModelOrdered",
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
            db.SaveChanges();

            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Info, "Product Database", "Update Product Model",
                $"Product Model '{existing.Name}' ({existing.PartNumber}) updated with {existing.HarnessAssemblies.Count} harnesses and {existing.ImagePath.Count} images.");

            return (true, null);
        }

        // Deletes the product model row, safely detaching from harness assemblies and removing its image folder.
        public static (bool Success, string? Error) DeleteProductModel(int productModelId)
        {
            if (!SessionManager.HasPermission("Action.HarnessDatabase.Edit"))
                return (false, "You do not have permission to modify product model records.");

            using var db = new AppDbContext();
            var existing = db.ProductModels
                .Include(p => p.HarnessAssemblies)
                .FirstOrDefault(p => p.Id == productModelId);

            if (existing == null) return (false, "Product Model not found.");

            string name = existing.Name;
            string partNumber = existing.PartNumber;

            // Safely detach from harness assemblies
            existing.HarnessAssemblies.Clear();

            db.ProductModels.Remove(existing);
            db.SaveChanges();

            // Clean up files and folder
            string partFolderName = SanitizeFolderName(partNumber);
            string folderFull = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ProductModelImageRoot, partFolderName);
            if (Directory.Exists(folderFull))
            {
                try
                {
                    Directory.Delete(folderFull, recursive: true);
                }
                catch (Exception ex)
                {
                    FileLogService.Warn("ProductModelService.DeleteProductModel",
                        $"Product Model image folder '{folderFull}' could not be deleted: {ex.Message}");
                }
            }

            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Warning, "Product Database", "Delete Product Model",
                $"Product Model '{name}' ({partNumber}) deleted.");

            return (true, null);
        }

        private static string? Validate(ProductModel product)
        {
            if (string.IsNullOrWhiteSpace(product.Name) && string.IsNullOrWhiteSpace(product.PartNumber))
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

        private static void DeleteRemovedImages(ProductModel product, List<string> removedImageFileNames)
        {
            if (removedImageFileNames.Count == 0 || product.ImagePath == null) return;

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var remaining = new List<string>();

            foreach (var path in product.ImagePath)
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

            product.ImagePath = remaining;
        }

        private static (List<string> SavedRelativePaths, string? Error) SaveImagesToFolder(ProductModel product, List<string> sourceImagePaths)
        {
            var savedRelativePaths = new List<string>();
            string partFolderName = SanitizeFolderName(string.IsNullOrWhiteSpace(product.PartNumber) ? $"Product_{product.Id}" : product.PartNumber);
            string relativeFolder = Path.Combine(ProductModelImageRoot, partFolderName);
            string fullFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativeFolder);
            Directory.CreateDirectory(fullFolder);

            int index = 1;
            var existingFiles = Directory.GetFiles(fullFolder);
            while (existingFiles.Any(f => Path.GetFileNameWithoutExtension(f) == $"{product.Id}_{index}"))
            {
                index++;
            }

            foreach (var sourcePath in sourceImagePaths)
            {
                if (!File.Exists(sourcePath)) continue;

                string ext = Path.GetExtension(sourcePath).ToLowerInvariant();
                string destFileName = $"{product.Id}_{index}{ext}";
                string destPath = Path.Combine(fullFolder, destFileName);

                while (File.Exists(destPath))
                {
                    index++;
                    destFileName = $"{product.Id}_{index}{ext}";
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
                    FileLogService.Warn("ProductModelService.SaveImagesToFolder",
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
            return string.IsNullOrEmpty(cleaned) ? "Product" : cleaned;
        }
    }
}
