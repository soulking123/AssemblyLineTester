using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AssemblyLineTester.Data;
using AssemblyLineTester.Services;

namespace AssemblyLineTester.Helpers
{
    /// <summary>
    /// Manages image resolution, storage, and deletion for Product Models, Harnesses, and Connectors.
    /// Redirects all write operations to AppDbContext.DataFolder (%LOCALAPPDATA%\AssemblyLineTester)
    /// to prevent Windows UAC Access Denied errors in installed (Program Files) environments,
    /// while retaining fallback read access to bundled installation assets.
    /// </summary>
    public static class ImageStorageHelper
    {
        private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".bmp", ".gif", ".webp"];

        /// <summary>
        /// Writable root for all user-created and uploaded assets (%LOCALAPPDATA%\AssemblyLineTester).
        /// </summary>
        public static string WritableRoot => AppDbContext.DataFolder;

        /// <summary>
        /// Read-only fallback root (installation directory in Program Files or build directory).
        /// </summary>
        public static string ReadOnlyRoot => AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Resolves a relative or absolute image path to an existing absolute path on disk.
        /// Checks WritableRoot (AppData) first, then ReadOnlyRoot (Program Files), then checks if rooted.
        /// </summary>
        public static string? ResolveImagePath(string? relativeOrAbsolutePath)
        {
            if (string.IsNullOrWhiteSpace(relativeOrAbsolutePath)) return null;

            if (Path.IsPathRooted(relativeOrAbsolutePath) && File.Exists(relativeOrAbsolutePath))
            {
                return relativeOrAbsolutePath;
            }

            // 1. Check in writable AppData folder
            string writableCandidate = Path.Combine(WritableRoot, relativeOrAbsolutePath);
            if (File.Exists(writableCandidate)) return writableCandidate;

            // 2. Check in read-only installation directory
            string readOnlyCandidate = Path.Combine(ReadOnlyRoot, relativeOrAbsolutePath);
            if (File.Exists(readOnlyCandidate)) return readOnlyCandidate;

            // 3. Fallback check under "Images" subfolder if path was stored without Assets/img
            string appDataImages = Path.Combine(WritableRoot, "Images", relativeOrAbsolutePath);
            if (File.Exists(appDataImages)) return appDataImages;

            return null;
        }

        /// <summary>
        /// Resolves an array or list of stored paths into existing absolute file paths.
        /// Supports individual file paths as well as directory paths.
        /// </summary>
        public static List<string> ResolveImagePaths(IEnumerable<string>? storedPaths)
        {
            var result = new List<string>();
            if (storedPaths == null) return result;

            foreach (var path in storedPaths)
            {
                if (string.IsNullOrWhiteSpace(path)) continue;

                string? resolved = ResolveImagePath(path);
                if (resolved != null)
                {
                    result.Add(resolved);
                }
                else
                {
                    // If stored path points to a directory
                    string writableDir = Path.IsPathRooted(path) ? path : Path.Combine(WritableRoot, path);
                    string readOnlyDir = Path.IsPathRooted(path) ? path : Path.Combine(ReadOnlyRoot, path);

                    if (Directory.Exists(writableDir))
                    {
                        result.AddRange(
                            Directory.GetFiles(writableDir)
                                .Where(f => AllowedExtensions.Contains(Path.GetExtension(f).ToLowerInvariant()))
                                .OrderBy(f => f));
                    }
                    else if (Directory.Exists(readOnlyDir))
                    {
                        result.AddRange(
                            Directory.GetFiles(readOnlyDir)
                                .Where(f => AllowedExtensions.Contains(Path.GetExtension(f).ToLowerInvariant()))
                                .OrderBy(f => f));
                    }
                    else
                    {
                        result.Add(path);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Ensures and returns the writable folder path under WritableRoot (AppData).
        /// </summary>
        public static string GetWritableFolder(string relativeFolder)
        {
            string fullPath = Path.Combine(WritableRoot, relativeFolder);
            Directory.CreateDirectory(fullPath);
            return fullPath;
        }

        /// <summary>
        /// Determines if a file already resides in the entity's managed folder (in either WritableRoot or ReadOnlyRoot).
        /// </summary>
        public static bool IsManagedImageFile(string filePath, string relativeFolder)
        {
            if (string.IsNullOrWhiteSpace(filePath)) return false;

            try
            {
                string writableFolder = Path.Combine(WritableRoot, relativeFolder);
                string readOnlyFolder = Path.Combine(ReadOnlyRoot, relativeFolder);

                string normalizedFile = Path.GetFullPath(filePath).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                string normalizedWritable = Path.GetFullPath(writableFolder).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) + Path.DirectorySeparatorChar;
                string normalizedReadOnly = Path.GetFullPath(readOnlyFolder).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) + Path.DirectorySeparatorChar;

                return normalizedFile.StartsWith(normalizedWritable, StringComparison.OrdinalIgnoreCase) ||
                       normalizedFile.StartsWith(normalizedReadOnly, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Converts an absolute file path that resides in WritableRoot or ReadOnlyRoot into a relative path starting with relativeFolder.
        /// </summary>
        public static string ToRelativePath(string fullFilePath, string relativeFolder)
        {
            string fileName = Path.GetFileName(fullFilePath);
            return Path.Combine(relativeFolder, fileName);
        }

        /// <summary>
        /// Safely deletes a file from WritableRoot, and attempts to delete from ReadOnlyRoot.
        /// </summary>
        public static void DeleteImageFile(string relativeOrAbsolutePath)
        {
            if (string.IsNullOrWhiteSpace(relativeOrAbsolutePath)) return;

            string writablePath = Path.IsPathRooted(relativeOrAbsolutePath)
                ? relativeOrAbsolutePath
                : Path.Combine(WritableRoot, relativeOrAbsolutePath);

            if (File.Exists(writablePath))
            {
                try { File.Delete(writablePath); }
                catch (Exception ex)
                {
                    FileLogService.Warn("ImageStorageHelper.DeleteImageFile", $"Could not delete file '{writablePath}': {ex.Message}");
                }
            }

            if (!Path.IsPathRooted(relativeOrAbsolutePath))
            {
                string readOnlyPath = Path.Combine(ReadOnlyRoot, relativeOrAbsolutePath);
                if (File.Exists(readOnlyPath))
                {
                    try { File.Delete(readOnlyPath); }
                    catch { /* Swallow expected Access Denied in Program Files */ }
                }
            }
        }

        /// <summary>
        /// Safely deletes an entire entity folder from WritableRoot and attempts deletion from ReadOnlyRoot.
        /// </summary>
        public static void DeleteFolder(string relativeFolder)
        {
            if (string.IsNullOrWhiteSpace(relativeFolder)) return;

            string writableFolder = Path.Combine(WritableRoot, relativeFolder);
            if (Directory.Exists(writableFolder))
            {
                try { Directory.Delete(writableFolder, recursive: true); }
                catch (Exception ex)
                {
                    FileLogService.Warn("ImageStorageHelper.DeleteFolder", $"Could not delete directory '{writableFolder}': {ex.Message}");
                }
            }

            string readOnlyFolder = Path.Combine(ReadOnlyRoot, relativeFolder);
            if (Directory.Exists(readOnlyFolder))
            {
                try { Directory.Delete(readOnlyFolder, recursive: true); }
                catch { /* Swallow expected Access Denied in Program Files */ }
            }
        }
    }
}
