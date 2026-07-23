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
        // Base folder for harness images: <app>/assets/img/harness/<folder>/<file>
        private static string ImagesRoot => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "img", "harness");

        public List<ProductHarness> GetAllHarnesses()
        {
            using var db = new AppDbContext();
            return db.ProductHarnesses
                .Include(h => h.ConnectorList)
                .OrderByDescending(h => h.LastUpdate)
                .ToList();
        }

        // Inserts the harness, copies any freshly-picked images into its own folder, and
        // re-points the selected connector's ProductHarnessId to this new harness.
        // harness.ImagePaths may contain absolute paths (freshly picked via OpenFileDialog).
        public (bool success, string? error) AddHarness(ProductHarness harness)
        {
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
                    ImagePaths = new List<string>(), // resolved after insert, once ProductHarnessId is known
                    ConnectorListName = harness.ConnectorListName
                };

                db.ProductHarnesses.Add(newHarness);
                db.SaveChanges(); // need the generated ProductHarnessId before creating the image folder

                newHarness.ImagePaths = ResolveImagePaths(newHarness, harness.ImagePaths);
                db.SaveChanges();

                ReassignConnectors(db, newHarness.ProductHarnessId, selectedConnectorIds);

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        // Updates harness fields. harness.ImagePaths may be a mix of: absolute paths (freshly
        // picked, need copying in) and relative paths (already stored, kept as-is). Any previously
        // stored path missing from this list was removed by the user in the modal - it is simply
        // dropped from the DB list here; the file itself is left on disk untouched.
        public (bool success, string? error) UpdateHarness(ProductHarness harness)
        {
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
                existing.ConnectorListName = harness.ConnectorListName;
                existing.ImagePaths = ResolveImagePaths(existing, harness.ImagePaths);

                db.SaveChanges();

                ReassignConnectors(db, existing.ProductHarnessId, selectedConnectorIds);

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        // Deletes the harness row (its connectors cascade-delete per the FK config) and its image folder.
        public (bool success, string? error) DeleteHarness(int productHarnessId)
        {
            try
            {
                using var db = new AppDbContext();
                var existing = db.ProductHarnesses.FirstOrDefault(h => h.ProductHarnessId == productHarnessId);
                if (existing == null) return (false, "Harness not found.");

                string folderPath = Path.Combine(ImagesRoot, BuildFolderName(existing));
                if (Directory.Exists(folderPath))
                {
                    Directory.Delete(folderPath, recursive: true);
                }

                db.ProductHarnesses.Remove(existing);
                db.SaveChanges();

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        // Walks the submitted path list: freshly-picked absolute files get copied into the harness's
        // image folder; already-relative (previously stored) paths are kept as-is.
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
                    resolved.Add(path); // already a stored relative path
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
            return Path.Combine(folderName, Path.GetFileName(destPath));
        }

        // Folder name = sanitized harness Name + ProductHarnessId, so it stays unique/stable
        // even if two harnesses share the same Name.
        private static string BuildFolderName(ProductHarness harness)
        {
            string safeName = string.Join("_", harness.Name.Split(Path.GetInvalidFileNameChars()));
            if (string.IsNullOrWhiteSpace(safeName)) safeName = "harness";
            return $"{safeName}_{harness.ProductHarnessId}";
        }

        // Points the selected connector's ProductHarnessId at this harness. Note: since a Connector
        // belongs to exactly one ProductHarness (non-nullable FK), assigning a connector that
        // currently belongs to a different harness will move it out of that harness.
        private static void ReassignConnectors(AppDbContext db, int harnessId, List<int> selectedConnectorIds)
        {
            if (selectedConnectorIds.Count == 0) return;

            var connectors = db.Connectors.Where(c => selectedConnectorIds.Contains(c.ConnectorId)).ToList();
            foreach (var c in connectors)
            {
                c.ProductHarnessId = harnessId;
            }
            db.SaveChanges();
        }
    }
}