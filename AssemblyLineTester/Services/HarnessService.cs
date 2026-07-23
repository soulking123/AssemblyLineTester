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
        // Base folder for harness images: <app>/assets/img/harness/<folder>/<file>.
        // ImagesRootRelative is what gets stored in ImagePaths so the path resolves the same way
        // whether against the app base dir or the seed data (which also stores the full relative path).
        private static string ImagesRootRelative => Path.Combine("assets", "img", "harness");
        private static string ImagesRoot => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ImagesRootRelative);

        // Loads every harness with its connectors, ordered by the position each connector was
        // attached in ("J1", "J2", ...). ConnectorList/ConnectorListName are derived fresh here every
        // time instead of trusted from storage, so the two can never drift out of sync.
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
                h.ConnectorListName = BuildConnectorListNames(h.ConnectorList.Count);
            }

            return harnesses;
        }

        // "J1", "J2", ... one per connector, matching ConnectorList's order.
        private static List<string> BuildConnectorListNames(int count) => Enumerable.Range(1, count).Select(i => $"J{i}").ToList();

        // Inserts the harness, copies any freshly-picked images into its own folder, and links the
        // selected (shared-catalog) connectors to it in the order they were picked.
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
                    ConnectorListName = BuildConnectorListNames(selectedConnectorIds.Count)
                };

                db.ProductHarnesses.Add(newHarness);
                db.SaveChanges(); // need the generated ProductHarnessId before creating the image folder

                newHarness.ImagePaths = ResolveImagePaths(newHarness, harness.ImagePaths);
                db.SaveChanges();

                SyncHarnessConnectors(db, newHarness.ProductHarnessId, selectedConnectorIds);

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
                existing.ConnectorListName = BuildConnectorListNames(selectedConnectorIds.Count);
                existing.ImagePaths = ResolveImagePaths(existing, harness.ImagePaths);

                db.SaveChanges();

                SyncHarnessConnectors(db, existing.ProductHarnessId, selectedConnectorIds);

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        // Deletes the harness row (its harness<->connector links cascade-delete per the FK config -
        // the connectors themselves are left alone since other harnesses may still use them) and its image folder.
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
            // Store the full relative path (incl. assets/img/harness) so it resolves against the app base dir.
            return Path.Combine(ImagesRootRelative, folderName, Path.GetFileName(destPath));
        }

        // Folder name = sanitized harness Name + ProductHarnessId, so it stays unique/stable
        // even if two harnesses share the same Name.
        private static string BuildFolderName(ProductHarness harness)
        {
            string safeName = string.Join("_", harness.Name.Split(Path.GetInvalidFileNameChars()));
            if (string.IsNullOrWhiteSpace(safeName)) safeName = "harness";
            return $"{safeName}_{harness.ProductHarnessId}";
        }

        // Replaces this harness's connector links with the given set, in order (Position 1..n -
        // "J1".."Jn"). A connector can be linked to several harnesses at once, so this only ever
        // touches links for this harnessId - other harnesses using the same connectors are untouched.
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