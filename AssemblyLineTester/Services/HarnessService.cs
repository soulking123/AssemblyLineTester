using AssemblyLineTester.Data;
using AssemblyLineTester.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AssemblyLineTester.Services
{
    public class HarnessService
    {
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
        public List<HarnessAssembly> GetAllHarnesses()
        {
            using var db = new AppDbContext();
            return db.HarnessAssemblies
                .Include(h => h.Connectors)
                .OrderBy(h => h.Name)
                .ToList();
        }

        public HarnessAssembly? GetHarnessById(int id)
        {
            using var db = new AppDbContext();
            return db.HarnessAssemblies
                .Include(h => h.Connectors)
                .FirstOrDefault(h => h.Id == id);
        }

        // Inserts a new harness assembly into the database.
        public (bool success, string? error) AddHarness(HarnessAssembly harness, List<int>? connectorIds = null, List<string>? sourceImagePaths = null)
        {
            if (!SessionManager.HasPermission("Action.HarnessDatabase.Edit"))
                return (false, "You do not have permission to modify harness records.");

            try
            {
                using var db = new AppDbContext();
                harness.Id = 0;
                harness.ImagePath ??= new List<string>();

                if (connectorIds != null && connectorIds.Count > 0)
                {
                    harness.Connectors = db.Connectors.Where(c => connectorIds.Contains(c.Id)).ToList();
                }

                if (sourceImagePaths != null && sourceImagePaths.Count > 0)
                {
                    harness.ImagePath.AddRange(sourceImagePaths);
                }

                db.HarnessAssemblies.Add(harness);
                db.SaveChanges();

                new SystemLogService().Log(
                    Models.SystemLog.LogSeverity.Info, "Product Database", "Create Harness",
                    $"Harness '{harness.Name}' created.");

                return (true, null);
            }
            catch (Exception ex)
            {
                FileLogService.Error("HarnessService.AddHarness", $"Could not create harness '{harness.Name}'.", ex);
                return (false, ex.Message);
            }
        }

        // Updates an existing harness assembly and synchronizes connectors/images.
        public (bool success, string? error) UpdateHarness(HarnessAssembly harness, List<int>? connectorIds = null, List<string>? newImagePaths = null, List<string>? removedImages = null)
        {
            if (!SessionManager.HasPermission("Action.HarnessDatabase.Edit"))
                return (false, "You do not have permission to modify harness records.");

            try
            {
                using var db = new AppDbContext();
                var existing = db.HarnessAssemblies
                    .Include(h => h.Connectors)
                    .FirstOrDefault(h => h.Id == harness.Id);

                if (existing == null) return (false, "Harness not found.");

                existing.Name = harness.Name;
                existing.PartNumber = harness.PartNumber;
                existing.Description = harness.Description;

                if (connectorIds != null)
                {
                    existing.Connectors.Clear();
                    var newConnectors = db.Connectors.Where(c => connectorIds.Contains(c.Id)).ToList();
                    existing.Connectors.AddRange(newConnectors);
                }

                existing.ImagePath ??= new List<string>();
                if (newImagePaths != null && newImagePaths.Count > 0)
                {
                    existing.ImagePath.AddRange(newImagePaths);
                }
                if (removedImages != null && removedImages.Count > 0)
                {
                    existing.ImagePath.RemoveAll(p => removedImages.Contains(p) || removedImages.Contains(Path.GetFileName(p)));
                }

                db.SaveChanges();

                new SystemLogService().Log(
                    Models.SystemLog.LogSeverity.Info, "Product Database", "Update Harness",
                    $"Harness '{existing.Name}' updated.");

                return (true, null);
            }
            catch (Exception ex)
            {
                FileLogService.Error("HarnessService.UpdateHarness", $"Could not update harness {harness.Id}.", ex);
                return (false, ex.Message);
            }
        }

        // Deletes a harness assembly.
        public (bool success, string? error) DeleteHarness(int harnessId)
        {
            if (!SessionManager.HasPermission("Action.HarnessDatabase.Edit"))
                return (false, "You do not have permission to modify harness records.");

            try
            {
                using var db = new AppDbContext();
                var existing = db.HarnessAssemblies.FirstOrDefault(h => h.Id == harnessId);
                if (existing == null) return (false, "Harness not found.");

                string name = existing.Name;
                db.HarnessAssemblies.Remove(existing);
                db.SaveChanges();

                new SystemLogService().Log(
                    Models.SystemLog.LogSeverity.Warning, "Product Database", "Delete Harness",
                    $"Harness '{name}' deleted.");

                return (true, null);
            }
            catch (Exception ex)
            {
                FileLogService.Error("HarnessService.DeleteHarness", $"Could not delete harness {harnessId}.", ex);
                return (false, ex.Message);
            }
        }
    }
}