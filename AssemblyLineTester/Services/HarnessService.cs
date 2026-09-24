using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AssemblyLineTester.Data;
using AssemblyLineTester.Models;
using Microsoft.EntityFrameworkCore;

namespace AssemblyLineTester.Services
{
    public class HarnessService
    {
        // Harness images live at <app>/assets/img/harness/<folder>/<file>.
        private static string ImagesRootRelative => Path.Combine("assets", "img", "harness");
        private static string ImagesRoot => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ImagesRootRelative);

        // A stored ProductHarness.ImagePaths entry is relative; this is the one place that turns
        // it into a real file path, so every page shows the same picture for the same harness.
        public static string? ResolveImagePath(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath)) return null;

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string candidateBase = Path.Combine(baseDir, relativePath);
            if (File.Exists(candidateBase)) return candidateBase;

            if (File.Exists(relativePath)) return Path.GetFullPath(relativePath);

            string imagesFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "AssemblyLineTester", "Images");
            string candidateAppData = Path.Combine(imagesFolder, relativePath);
            return File.Exists(candidateAppData) ? candidateAppData : null;
        }

        // Loads every harness with its connectors, ordered by attach position.
        public List<ProductHarness> GetAllHarnesses()
        {
            using var db = new AppDbContext();
            var harnesses = db.ProductHarnesses
                .Include(h => h.HarnessConnectors)
                .ThenInclude(hc => hc.Connector)
                .OrderByDescending(h => h.LastUpdate)
                .ToList();

            foreach (var h in harnesses)
            {
                h.ConnectorList = h.HarnessConnectors.OrderBy(hc => hc.Position).Select(hc => hc.Connector!).ToList();
            }

            return harnesses;
        }

        // Inserts the harness, copies its images in, and links the chosen connectors in order.
        public (bool success, string? error) AddHarness(ProductHarness harness)
        {
            if (!SessionManager.HasPermission("Action.HarnessDatabase.Edit"))
                return (false, "You do not have permission to modify harness records.");

            try
            {
                using var db = new AppDbContext();
                var selectedConnectorIds = harness.ConnectorList.Select(c => c.ConnectorId).ToList();

                var newHarness = new ProductHarness
                {
                    Name = harness.Name,
                    ProductModel = harness.ProductModel,
                    ProductType = harness.ProductType,
                    Area = harness.Area,
                    LastUpdate = DateTime.UtcNow,
                    ImagePaths = new List<string>()
                };

                db.ProductHarnesses.Add(newHarness);
                db.SaveChanges();

                newHarness.ImagePaths = ResolveImagePaths(newHarness, harness.ImagePaths);
                db.SaveChanges();

                SyncHarnessConnectors(db, newHarness.ProductHarnessId, selectedConnectorIds);

                new SystemLogService().Log(
                    Models.SystemLog.LogSeverity.Info, "Harness Database", "Create Harness",
                    $"Harness '{newHarness.Name}' created for model {newHarness.ProductModel} with {selectedConnectorIds.Count} connectors.");

                return (true, null);
            }
            catch (Exception ex)
            {
                FileLogService.Error("HarnessService.AddHarness", $"Could not create harness '{harness.Name}'.", ex);
                return (false, ex.Message);
            }
        }

        // Updates harness fields; a path dropped from ImagePaths is unlinked, not deleted from disk.
        public (bool success, string? error) UpdateHarness(ProductHarness harness)
        {
            if (!SessionManager.HasPermission("Action.HarnessDatabase.Edit"))
                return (false, "You do not have permission to modify harness records.");

            try
            {
                using var db = new AppDbContext();
                var existing = db.ProductHarnesses.FirstOrDefault(h => h.ProductHarnessId == harness.ProductHarnessId);
                if (existing == null) return (false, "Harness not found.");

                var selectedConnectorIds = harness.ConnectorList.Select(c => c.ConnectorId).ToList();

                existing.Name = harness.Name;
                existing.ProductModel = harness.ProductModel;
                existing.ProductType = harness.ProductType;
                existing.Area = harness.Area;
                existing.LastUpdate = DateTime.UtcNow;
                existing.ImagePaths = ResolveImagePaths(existing, harness.ImagePaths);

                db.SaveChanges();

                SyncHarnessConnectors(db, existing.ProductHarnessId, selectedConnectorIds);

                new SystemLogService().Log(
                    Models.SystemLog.LogSeverity.Info, "Harness Database", "Update Harness",
                    $"Harness '{existing.Name}' updated: {selectedConnectorIds.Count} connectors, {existing.ImagePaths.Count} images.");

                return (true, null);
            }
            catch (Exception ex)
            {
                FileLogService.Error("HarnessService.UpdateHarness", $"Could not update harness {harness.ProductHarnessId}.", ex);
                return (false, ex.Message);
            }
        }

        // Deletes the harness and its image folder; connectors survive, other harnesses may use them.
        public (bool success, string? error) DeleteHarness(int productHarnessId)
        {
            if (!SessionManager.HasPermission("Action.HarnessDatabase.Edit"))
                return (false, "You do not have permission to modify harness records.");

            try
            {
                using var db = new AppDbContext();
                var existing = db.ProductHarnesses.FirstOrDefault(h => h.ProductHarnessId == productHarnessId);
                if (existing == null) return (false, "Harness not found.");

                string folderPath = Path.Combine(ImagesRoot, BuildFolderName(existing));
                string harnessName = existing.Name;

                db.ProductHarnesses.Remove(existing);
                db.SaveChanges();

                // Folder goes only after the row, or a failed save destroys images for nothing.
                if (Directory.Exists(folderPath))
                {
                    Directory.Delete(folderPath, recursive: true);
                }

                new SystemLogService().Log(
                    Models.SystemLog.LogSeverity.Warning, "Harness Database", "Delete Harness",
                    $"Harness '{harnessName}' deleted along with its image folder.");

                return (true, null);
            }
            catch (Exception ex)
            {
                FileLogService.Error("HarnessService.DeleteHarness", $"Could not delete harness {productHarnessId}.", ex);
                return (false, ex.Message);
            }
        }

        // Absolute paths are freshly picked and get copied in; relative ones are already stored.
        private static List<string> ResolveImagePaths(ProductHarness harness, List<string> submittedPaths)
        {
            var resolved = new List<string>();
            foreach (string path in submittedPaths)
            {
                if (string.IsNullOrWhiteSpace(path)) continue;

                if (Path.IsPathRooted(path) && File.Exists(path))
                {
                    resolved.Add(CopyHarnessImage(harness, path));
                }
                else
                {
                    resolved.Add(path);
                }
            }
            return resolved;
        }

        private static string CopyHarnessImage(ProductHarness harness, string sourceFile)
        {
            string folderName = BuildFolderName(harness);
            string folderPath = Path.Combine(ImagesRoot, folderName);
            Directory.CreateDirectory(folderPath);

            string fileName = Path.GetFileName(sourceFile);
            string destPath = Path.Combine(folderPath, fileName);

            int counter = 1;
            while (File.Exists(destPath))
            {
                string nameOnly = Path.GetFileNameWithoutExtension(fileName);
                string ext = Path.GetExtension(fileName);
                destPath = Path.Combine(folderPath, $"{nameOnly}_{counter}{ext}");
                counter++;
            }

            File.Copy(sourceFile, destPath, overwrite: false);
            return Path.Combine(ImagesRootRelative, folderName, Path.GetFileName(destPath));
        }

        // Name plus id, so two harnesses sharing a name still get separate folders.
        private static string BuildFolderName(ProductHarness harness)
        {
            string safeName = string.Join("_", harness.Name.Split(Path.GetInvalidFileNameChars()));
            if (string.IsNullOrWhiteSpace(safeName)) safeName = "harness";
            return $"{safeName}_{harness.ProductHarnessId}";
        }

        // Replaces only this harness's connector links, in order; other harnesses are untouched.
        private static void SyncHarnessConnectors(AppDbContext db, int harnessId, List<int> orderedConnectorIds)
        {
            var existingLinks = db.HarnessConnectors.Where(hc => hc.ProductHarnessId == harnessId);
            db.HarnessConnectors.RemoveRange(existingLinks);

            for (int i = 0; i < orderedConnectorIds.Count; i++)
            {
                db.HarnessConnectors.Add(new HarnessConnector
                {
                    ProductHarnessId = harnessId,
                    ConnectorId = orderedConnectorIds[i],
                    Position = i + 1
                });
            }

            db.SaveChanges();
        }
    }
}