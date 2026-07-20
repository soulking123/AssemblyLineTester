using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AssemblyLineTester.Data;
using AssemblyLineTester.Models;

namespace AssemblyLineTester.Services
{
    /// <summary>
    /// Provides database operations for role permissions and RBAC configuration.
    /// </summary>
    public class RolePermissionService
    {
        /// <summary>
        /// Retrieves all roles available in the system.
        /// </summary>
        public List<Role> GetAllRoles()
        {
            using var db = new AppDbContext();
            return db.Roles.OrderBy(r => r.Id).ToList();
        }

        /// <summary>
        /// Retrieves all roles available in the system asynchronously.
        /// </summary>
        public async Task<List<Role>> GetAllRolesAsync()
        {
            using var db = new AppDbContext();
            return await db.Roles.OrderBy(r => r.Id).ToListAsync();
        }

        /// <summary>
        /// Retrieves all assigned and available permissions for a given role ID.
        /// If any new permission definitions are missing in the database for the role, they are auto-created.
        /// </summary>
        public List<RolePermission> GetPermissionsByRoleId(int roleId)
        {
            using var db = new AppDbContext();
            var existing = db.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .OrderBy(rp => rp.Id)
                .ToList();

            // Ensure all standard keys exist for this role
            var allDefs = GetStandardPermissionDefinitions();
            bool addedNew = false;

            foreach (var (key, group, desc) in allDefs)
            {
                if (!existing.Any(e => e.PermissionKey == key))
                {
                    var newRp = new RolePermission
                    {
                        RoleId = roleId,
                        PermissionKey = key,
                        PermissionGroup = group,
                        Description = desc,
                        IsGranted = (roleId == 1) // Administrator gets true by default
                    };
                    db.RolePermissions.Add(newRp);
                    existing.Add(newRp);
                    addedNew = true;
                }
            }

            if (addedNew)
            {
                db.SaveChanges();
            }

            return existing;
        }

        /// <summary>
        /// Retrieves all assigned and available permissions for a given role ID asynchronously.
        /// </summary>
        public async Task<List<RolePermission>> GetPermissionsByRoleIdAsync(int roleId)
        {
            using var db = new AppDbContext();
            var existing = await db.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .OrderBy(rp => rp.Id)
                .ToListAsync();

            // Ensure all standard keys exist for this role
            var allDefs = GetStandardPermissionDefinitions();
            bool addedNew = false;

            foreach (var (key, group, desc) in allDefs)
            {
                if (!existing.Any(e => e.PermissionKey == key))
                {
                    var newRp = new RolePermission
                    {
                        RoleId = roleId,
                        PermissionKey = key,
                        PermissionGroup = group,
                        Description = desc,
                        IsGranted = (roleId == 1) // Administrator gets true by default
                    };
                    db.RolePermissions.Add(newRp);
                    existing.Add(newRp);
                    addedNew = true;
                }
            }

            if (addedNew)
            {
                await db.SaveChangesAsync();
            }

            return existing;
        }

        /// <summary>
        /// Updates the permission toggle states for a specific role.
        /// </summary>
        public void UpdatePermissions(int roleId, Dictionary<string, bool> updatedToggles)
        {
            // Safeguard: Administrator role cannot have Settings or User Management access revoked
            if (roleId == 1)
            {
                if (updatedToggles.ContainsKey("Page.Settings")) updatedToggles["Page.Settings"] = true;
                if (updatedToggles.ContainsKey("Page.UserManagement")) updatedToggles["Page.UserManagement"] = true;
            }

            using var db = new AppDbContext();
            var existing = db.RolePermissions.Where(rp => rp.RoleId == roleId).ToList();

            foreach (var rp in existing)
            {
                if (updatedToggles.TryGetValue(rp.PermissionKey, out bool granted))
                {
                    rp.IsGranted = granted;
                }
            }

            db.SaveChanges();

            // If updating the active user's role, refresh the SessionManager cache immediately
            if (SessionManager.CurrentUser != null && SessionManager.CurrentUser.RoleId == roleId)
            {
                SessionManager.RefreshPermissions();
                System.Windows.Forms.Application.OpenForms.OfType<AssemblyLineTester.Views.MainForm>().FirstOrDefault()?.ApplyRolePermissions();
            }
        }

        /// <summary>
        /// Resets all permissions for a specific role back to initial system defaults.
        /// </summary>
        public void ResetRoleToDefaults(int roleId)
        {
            using var db = new AppDbContext();
            var existing = db.RolePermissions.Where(rp => rp.RoleId == roleId).ToList();
            var defs = GetStandardPermissionDefinitions();

            foreach (var rp in existing)
            {
                bool defaultGranted = GetDefaultGrantState(roleId, rp.PermissionKey);
                rp.IsGranted = defaultGranted;
            }

            db.SaveChanges();

            if (SessionManager.CurrentUser != null && SessionManager.CurrentUser.RoleId == roleId)
            {
                SessionManager.RefreshPermissions();
                System.Windows.Forms.Application.OpenForms.OfType<AssemblyLineTester.Views.MainForm>().FirstOrDefault()?.ApplyRolePermissions();
            }
        }

        public static bool GetDefaultGrantState(int roleId, string key)
        {
            if (roleId == 1) return true; // Administrator gets all
            if (roleId == 2) // Operator
            {
                return key is "Page.Dashboard" or "Page.TestExecution" or "Page.TestHistory" or "Action.TestExecution.Run";
            }
            if (roleId == 3) // Engineer
            {
                return key is "Page.Dashboard" or "Page.TestExecution" or "Page.HarnessDatabase" or "Page.TestHistory" or "Page.Reports" or "Page.SystemLog" or "Page.Settings" or "Action.TestExecution.Run" or "Action.TestConfig.Edit" or "Action.Instruments.Calibrate" or "Action.Reports.Export";
            }
            if (roleId == 4) // Technician
            {
                return key is "Page.Dashboard" or "Page.TestExecution" or "Page.HarnessDatabase" or "Page.TestHistory" or "Page.SystemLog" or "Page.Settings" or "Action.TestExecution.Run" or "Action.Instruments.Calibrate" or "Action.Reports.Export";
            }
            if (roleId == 5) // Quality
            {
                return key is "Page.Dashboard" or "Page.TestExecution" or "Page.HarnessDatabase" or "Page.TestHistory" or "Page.Reports" or "Action.Reports.Export";
            }
            if (roleId == 6) // Supervisor
            {
                return key is "Page.Dashboard" or "Page.TestExecution" or "Page.HarnessDatabase" or "Page.TestHistory" or "Page.Reports" or "Page.SystemLog" or "Action.TestExecution.Run" or "Action.Reports.Export";
            }
            return false;
        }

        public static (string Key, string Group, string Description)[] GetStandardPermissionDefinitions()
        {
            return new[]
            {
                ("Page.Dashboard", "Page Access", "Access Dashboard Page"),
                ("Page.TestExecution", "Page Access", "Access Test Execution Page"),
                ("Page.HarnessDatabase", "Page Access", "Access Harness Database Page"),
                ("Page.TestHistory", "Page Access", "Access Test History Page"),
                ("Page.Reports", "Page Access", "Access Reports Page"),
                ("Page.SystemLog", "Page Access", "Access System Log Page"),
                ("Page.Settings", "Page Access", "Access Settings Page"),
                ("Page.UserManagement", "Page Access", "Access User Management Page"),
                ("Action.TestExecution.Run", "Test Operations", "Start and Run Testing Operations"),
                ("Action.TestConfig.Edit", "Test Operations", "Add and Edit Test Parameters & Harness Specs"),
                ("Action.Instruments.Calibrate", "Test Operations", "Calibrate and Configure Hardware Instruments"),
                ("Action.UserManagement.Edit", "System & Logs", "Add, Edit, and Manage System User Accounts"),
                ("Action.Reports.Export", "System & Logs", "Export and Print Test Reports & Analytics"),
                ("Action.SystemLog.Clear", "System & Logs", "Clear and Purge System Activity Logs"),
                ("Action.Database.Backup", "System & Logs", "Create and Restore Database Backups")
            };
        }
    }
}
