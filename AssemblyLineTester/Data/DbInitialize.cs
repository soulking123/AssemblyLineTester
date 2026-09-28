using AssemblyLineTester.Helpers;
using AssemblyLineTester.Models;
using AssemblyLineTester.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace AssemblyLineTester.Data
{
    public static class DbInitialize
    {
        // Applies pending migrations; false means the application must not carry on.
        public static bool Initialize()
        {
            try
            {
                using var db = new AppDbContext();

                string[] pending = [.. db.Database.GetPendingMigrations()];
                db.Database.Migrate();

                // WAL survives an unclean shutdown; a factory losing power mid-test is routine.
                db.Database.ExecuteSqlRaw("PRAGMA journal_mode=WAL;");

                if (pending.Length > 0)
                {
                    FileLogService.Info("DbInitialize", $"Schema migrated: {string.Join(", ", pending)}");
                }

                SeedInitialProductCatalog(db);

                return true;
            }
            catch (Exception ex)
            {
                FileLogService.Error("DbInitialize", "Database initialisation failed.", ex);

                MessageBox.Show(
                    $"The database could not be opened or upgraded, so the application cannot start.\r\n\r\n" +
                    $"{ex.Message}\r\n\r\nThe details are in the log folder.",
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
        }

        public static void SeedInitialProductCatalog(AppDbContext db)
        {
            try
            {
                CopyBundledAssetsToAppData();

                // 1. Seed or Update Connectors (18 items from reference)
                var connectorDefs = new[]
                {
                    new { Name = "CF-C1", PartNo = "160-7689", Pins = 70, Type = "Plug", Folder = "CF-C1", Files = new[] { "160-7689-1.jpeg", "160-7689-2.jpeg", "160-7689-3.jpeg", "160-7689-4.jpeg" } },
                    new { Name = "CF-C2", PartNo = "264-5732", Pins = 120, Type = "Plug", Folder = "CF-C2", Files = new[] { "264-5732-1.jpeg", "264-5732-2.jpeg" } },
                    new { Name = "CF-C5", PartNo = "264-5732", Pins = 120, Type = "Plug", Folder = "CF-C5", Files = new[] { "264-5732-1.jpeg", "264-5732-2.jpeg" } },
                    new { Name = "CF-C6", PartNo = "160-7690", Pins = 70, Type = "Plug", Folder = "CF-C6", Files = new[] { "160-7690-1.jpeg", "160-7690-2.jpeg", "160-7690-3.jpeg", "160-7690-4.jpeg" } },
                    new { Name = "CH-C1", PartNo = "324-5680", Pins = 120, Type = "Receptacle", Folder = "CH-C1", Files = new[] { "224-2734-1.jpeg", "224-2734-2.jpeg", "224-2734-3.jpeg", "224-2734-4.jpeg" } },
                    new { Name = "CH-C2", PartNo = "224-2734", Pins = 70, Type = "Receptacle", Folder = "CH-C2", Files = new[] { "224-2734-1.jpeg", "224-2734-2.jpeg", "224-2734-3.jpeg", "224-2734-4.jpeg" } },
                    new { Name = "CH-C3", PartNo = "324-5680", Pins = 120, Type = "Receptacle", Folder = "CH-C3", Files = new[] { "324-5680-1.jpeg", "324-5680-2.jpeg", "324-5680-3.jpeg", "324-5680-4.jpeg" } },
                    new { Name = "CH-C4", PartNo = "224-2734", Pins = 70, Type = "Receptacle", Folder = "CH-C4", Files = new[] { "224-2734-1.jpeg", "224-2734-2.jpeg", "224-2734-3.jpeg", "224-2734-4.jpeg" } },
                    new { Name = "CM-C11", PartNo = "147-1446", Pins = 70, Type = "Plug", Folder = "CM-C11", Files = new[] { "147-1446-1.jpeg", "147-1446-2.jpeg", "147-1446-3.jpeg", "147-1446-4.jpeg" } },
                    new { Name = "CM-C12", PartNo = "147-1446", Pins = 70, Type = "Plug", Folder = "CM-C12", Files = new[] { "147-1446-1.jpeg", "147-1446-2.jpeg", "147-1446-3.jpeg", "147-1446-4.jpeg" } },
                    new { Name = "CM-C17", PartNo = "483-2761", Pins = 44, Type = "Plug", Folder = "CM-C17", Files = new[] { "483-2761-1.jpeg", "483-2761-2.jpeg", "483-2761-3.jpeg", "483-2761-4.jpeg" } },
                    new { Name = "CM-C18", PartNo = "483-2762", Pins = 44, Type = "Plug", Folder = "CM-C18", Files = new[] { "483-2762-1.jpeg", "483-2762-2.jpeg", "483-2762-3.jpeg", "483-2762-4.jpeg" } },
                    new { Name = "CM-C19", PartNo = "483-2763", Pins = 44, Type = "Plug", Folder = "CM-C19", Files = new[] { "483-2763-1.jpeg", "483-2763-2.jpeg", "483-2763-3.jpeg", "483-2763-4.jpeg" } },
                    new { Name = "CM-C5", PartNo = "147-1446", Pins = 70, Type = "Plug", Folder = "CM-C5", Files = new[] { "147-1446-1.jpeg", "147-1446-2.jpeg", "147-1446-3.jpeg", "147-1446-4.jpeg" } },
                    new { Name = "CM-C6", PartNo = "147-1446", Pins = 70, Type = "Plug", Folder = "CM-C6", Files = new[] { "147-1446-1.jpeg", "147-1446-2.jpeg", "147-1446-3.jpeg", "147-1446-4.jpeg" } },
                    new { Name = "CM-C64", PartNo = "264-5732", Pins = 120, Type = "Plug", Folder = "CM-C64", Files = new[] { "264-5732-1.jpeg", "264-5732-2.jpeg" } },
                    new { Name = "LL-C11", PartNo = "264-5732", Pins = 120, Type = "Plug", Folder = "LL-C11", Files = new[] { "264-5732-1.jpeg", "264-5732-2.jpeg" } },
                    new { Name = "LL-C9", PartNo = "160-7689", Pins = 70, Type = "Plug", Folder = "LL-C9", Files = new[] { "160-7689-1.jpeg", "160-7689-2.jpeg", "160-7689-3.jpeg", "160-7689-4.jpeg" } },
                };

                var existingConnectors = db.Connectors.ToList();
                var connectorMap = new Dictionary<string, Connector>(StringComparer.OrdinalIgnoreCase);

                foreach (var def in connectorDefs)
                {
                    var conn = existingConnectors.FirstOrDefault(c => string.Equals(c.Name, def.Name, StringComparison.OrdinalIgnoreCase));
                    var imgPaths = def.Files.Select(f => Path.Combine("Assets", "img", "connector", def.Folder, f)).ToList();
                    var inclusions = def.Files.Select((_, i) => i == 0).ToList();

                    if (conn == null)
                    {
                        conn = new Connector
                        {
                            Name = def.Name,
                            PartNumber = def.PartNo,
                            TotalPin = def.Pins,
                            Type = def.Type,
                            Description = "",
                            ImagePath = imgPaths,
                            IncludedForReport = inclusions
                        };
                        db.Connectors.Add(conn);
                    }
                    else
                    {
                        conn.PartNumber = def.PartNo;
                        conn.TotalPin = def.Pins;
                        conn.Type = def.Type;
                        conn.ImagePath = imgPaths;
                        conn.IncludedForReport = inclusions;
                    }
                    connectorMap[def.Name] = conn;
                }
                db.SaveChanges();

                // 2. Seed or Update Harness Assemblies
                var harnessDefs = new[]
                {
                    new
                    {
                        Name = "646-3490/00",
                        PartNo = "646-3490/00",
                        Image = Path.Combine("Assets", "img", "harness", "646-3490", "646-3490.jpg"),
                        Connectors = new[] { "CH-C1", "CH-C2", "CH-C3", "CH-C4" }
                    },
                    new
                    {
                        Name = "660-2031/00",
                        PartNo = "660-2031/00",
                        Image = Path.Combine("Assets", "img", "harness", "660-2031", "660-2031.jpg"),
                        Connectors = new[] { "CF-C5", "CF-C6", "CF-C2", "CF-C1" }
                    },
                    new
                    {
                        Name = "671-0554/00",
                        PartNo = "671-0554/00",
                        Image = Path.Combine("Assets", "img", "harness", "671-0554", "671-0554.jpg"),
                        Connectors = new[] { "CM-C5", "CM-C6", "CM-C11", "CM-C12", "CM-C17", "CM-C18", "CM-C19", "CM-C64" }
                    }
                };

                var existingHarnesses = db.HarnessAssemblies.Include(h => h.Connectors).ToList();
                var harnessMap = new Dictionary<string, HarnessAssembly>(StringComparer.OrdinalIgnoreCase);

                foreach (var hDef in harnessDefs)
                {
                    var h = existingHarnesses.FirstOrDefault(x =>
                        string.Equals(x.PartNumber, hDef.PartNo, StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(x.Name, hDef.Name, StringComparison.OrdinalIgnoreCase));

                    if (h == null)
                    {
                        h = new HarnessAssembly
                        {
                            Name = hDef.Name,
                            PartNumber = hDef.PartNo,
                            Description = "",
                            ImagePath = new List<string> { hDef.Image },
                            IncludedForReport = new List<bool> { true },
                            Connectors = new List<Connector>()
                        };
                        db.HarnessAssemblies.Add(h);
                    }
                    else
                    {
                        h.Name = hDef.Name;
                        h.PartNumber = hDef.PartNo;
                        h.ImagePath = new List<string> { hDef.Image };
                        h.IncludedForReport = new List<bool> { true };
                    }

                    // Attach connectors
                    h.Connectors ??= new List<Connector>();
                    foreach (var cName in hDef.Connectors)
                    {
                        if (connectorMap.TryGetValue(cName, out var cObj) && !h.Connectors.Any(c => c.Id == cObj.Id))
                        {
                            h.Connectors.Add(cObj);
                        }
                    }

                    harnessMap[hDef.PartNo] = h;
                }
                db.SaveChanges();

                // 3. Seed or Update Product Models
                var modelDefs = new[]
                {
                    new { Name = "6020", PartNo = "", Image = Path.Combine("Assets", "img", "product_model", "6020", "HMS6020.jpeg"), Harnesses = Array.Empty<string>() },
                    new { Name = "6030", PartNo = "", Image = Path.Combine("Assets", "img", "product_model", "6030", "HMS6030.jpeg"), Harnesses = Array.Empty<string>() },
                    new { Name = "6040", PartNo = "6040D", Image = Path.Combine("Assets", "img", "product_model", "6040", "HMS6040.jpeg"), Harnesses = new[] { "660-2031/00", "671-0554/00", "646-3490/00" } },
                    new { Name = "6060", PartNo = "6060D", Image = Path.Combine("Assets", "img", "product_model", "6060", "HMS6060.jpeg"), Harnesses = new[] { "660-2031/00", "671-0554/00", "646-3490/00" } },
                };

                var existingModels = db.ProductModels.Include(p => p.HarnessAssemblies).ToList();

                foreach (var mDef in modelDefs)
                {
                    var m = existingModels.FirstOrDefault(x =>
                        string.Equals(x.Name, mDef.Name, StringComparison.OrdinalIgnoreCase) ||
                        (!string.IsNullOrEmpty(mDef.PartNo) && (string.Equals(x.Name, mDef.PartNo, StringComparison.OrdinalIgnoreCase) || string.Equals(x.PartNumber, mDef.PartNo, StringComparison.OrdinalIgnoreCase))));

                    if (m == null)
                    {
                        m = new ProductModel
                        {
                            Name = mDef.Name,
                            PartNumber = mDef.PartNo,
                            Description = "",
                            ImagePath = new List<string> { mDef.Image },
                            IncludedForReport = new List<bool> { true },
                            HarnessAssemblies = new List<HarnessAssembly>()
                        };
                        db.ProductModels.Add(m);
                    }
                    else
                    {
                        m.Name = mDef.Name;
                        m.PartNumber = mDef.PartNo;
                        m.ImagePath = new List<string> { mDef.Image };
                        m.IncludedForReport = new List<bool> { true };
                    }

                    m.HarnessAssemblies ??= new List<HarnessAssembly>();
                    foreach (var hPart in mDef.Harnesses)
                    {
                        if (harnessMap.TryGetValue(hPart, out var hObj) && !m.HarnessAssemblies.Any(h => h.Id == hObj.Id))
                        {
                            m.HarnessAssemblies.Add(hObj);
                        }
                    }
                }
                db.SaveChanges();
                Console.WriteLine("SeedInitialProductCatalog completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to seed product catalog: {ex}");
                FileLogService.Warn("DbInitialize.SeedInitialProductCatalog", $"Failed to seed product catalog: {ex.Message}");
            }
        }

        private static void CopyBundledAssetsToAppData()
        {
            try
            {
                string readOnlyAssets = Path.Combine(ImageStorageHelper.ReadOnlyRoot, "Assets", "img");
                string writableAssets = Path.Combine(ImageStorageHelper.WritableRoot, "Assets", "img");

                if (!Directory.Exists(readOnlyAssets)) return;
                if (string.Equals(Path.GetFullPath(readOnlyAssets), Path.GetFullPath(writableAssets), StringComparison.OrdinalIgnoreCase)) return;

                Directory.CreateDirectory(writableAssets);

                foreach (string dir in Directory.GetDirectories(readOnlyAssets, "*", SearchOption.AllDirectories))
                {
                    string relDir = Path.GetRelativePath(readOnlyAssets, dir);
                    Directory.CreateDirectory(Path.Combine(writableAssets, relDir));
                }

                foreach (string file in Directory.GetFiles(readOnlyAssets, "*.*", SearchOption.AllDirectories))
                {
                    string relFile = Path.GetRelativePath(readOnlyAssets, file);
                    string target = Path.Combine(writableAssets, relFile);
                    if (!File.Exists(target))
                    {
                        File.Copy(file, target, overwrite: false);
                    }
                }
            }
            catch (Exception ex)
            {
                FileLogService.Warn("DbInitialize.CopyBundledAssetsToAppData", $"Could not copy bundled assets to app data: {ex.Message}");
            }
        }
    }
}
