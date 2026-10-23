using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AssemblyLineTester.Data;
using AssemblyLineTester.Models;

namespace AssemblyLineTester.Services
{
    // Reads and writes role permissions.
    public class RolePermissionService
    {
        public async Task<List<Role>> GetAllRolesAsync()
        {
            using var db = new AppDbContext();
            return await db.Roles.OrderBy(r => r.Id).ToListAsync();
        }

        // Returns the role's permissions, creating any standard key it is missing.
        public async Task<List<RolePermission>> GetPermissionsByRoleIdAsync(int roleId)
        {
            using var db = new AppDbContext();
            var existing = await db.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .OrderBy(rp => rp.Id)
                .ToListAsync();

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
                        // Same rule the seed uses, so a key added after release lands correctly.
                        IsGranted = GetDefaultGrantState(roleId, key)
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

        // Applies the toggle states for one role, refreshing the session if it is the active role.
        public void UpdatePermissions(int roleId, Dictionary<string, bool> updatedToggles)
        {
            RequireUserManagementRights();

            // Administrator can never lose Settings or User Management, or nobody can undo it.
            if (roleId == 1)
            {
                if (updatedToggles.ContainsKey("Page.Settings")) updatedToggles["Page.Settings"] = true;
                if (updatedToggles.ContainsKey("Page.UserManagement")) updatedToggles["Page.UserManagement"] = true;
            }

            using var db = new AppDbContext();
            var existing = db.RolePermissions.Where(rp => rp.RoleId == roleId).ToList();

            var changed = new List<string>();

            foreach (var rp in existing)
            {
                if (updatedToggles.TryGetValue(rp.PermissionKey, out bool granted) && rp.IsGranted != granted)
                {
                    // Only the keys that moved, or the entry buries the one that mattered.
                    changed.Add($"{rp.PermissionKey}={(granted ? "on" : "off")}");
                    rp.IsGranted = granted;
                }
            }

            db.SaveChanges();

            if (changed.Count > 0)
            {
                string roleName = db.Roles.Where(r => r.Id == roleId).Select(r => r.Name).FirstOrDefault()
                                  ?? roleId.ToString();

                new SystemLogService().Log(
                    Models.SystemLog.LogSeverity.Warning, "User Management", "Update Role Permissions",
                    $"Permissions for role '{roleName}' changed: {string.Join(", ", changed)}");
            }

            if (SessionManager.CurrentUser != null && SessionManager.CurrentUser.RoleId == roleId)
            {
                SessionManager.RefreshPermissions();
                System.Windows.Forms.Application.OpenForms.OfType<AssemblyLineTester.Views.MainForm>().FirstOrDefault()?.ApplyRolePermissions();
            }
        }

        // Puts every permission for one role back to its shipped default.
        public void ResetRoleToDefaults(int roleId)
        {
            RequireUserManagementRights();

            using var db = new AppDbContext();
            var existing = db.RolePermissions.Where(rp => rp.RoleId == roleId).ToList();

            foreach (var rp in existing)
            {
                bool defaultGranted = GetDefaultGrantState(roleId, rp.PermissionKey);
                rp.IsGranted = defaultGranted;
            }

            db.SaveChanges();

            string resetRoleName = db.Roles.Where(r => r.Id == roleId).Select(r => r.Name).FirstOrDefault()
                                   ?? roleId.ToString();

            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Warning, "User Management", "Reset Role Defaults",
                $"Role '{resetRoleName}' reset to default permissions ({existing.Count} keys).");

            if (SessionManager.CurrentUser != null && SessionManager.CurrentUser.RoleId == roleId)
            {
                SessionManager.RefreshPermissions();
                System.Windows.Forms.Application.OpenForms.OfType<AssemblyLineTester.Views.MainForm>().FirstOrDefault()?.ApplyRolePermissions();
            }
        }

        // Rewriting a role's rights is user management, not a settings tweak: Page.Settings is not enough.
        private static void RequireUserManagementRights()
        {
            if (!SessionManager.HasPermission("Action.UserManagement.Edit"))
            {
                throw new UnauthorizedAccessException("Access denied. You are not allowed to change role permissions.");
            }
        }

        // Delete rights stay narrow on purpose: Administrator, Engineer and Supervisor only.
        public static bool GetDefaultGrantState(int roleId, string key)
        {
            if (roleId == 1) return true;
            if (roleId == 2) // Operator
            {
                return key is "Page.Dashboard" or "Page.TestExecution" or "Page.TestHistory" or "Action.TestExecution.Run" or "Action.Reports.Export";
            }
            if (roleId == 3) // Engineer
            {
                return key is "Page.Dashboard" or "Page.TestExecution" or "Page.HarnessDatabase" or "Page.TestHistory" or "Page.Reports" or "Page.SystemLog" or "Page.Settings" or "Action.TestExecution.Run" or "Action.TestConfig.Edit" or "Action.Instruments.Calibrate" or "Action.Reports.Export" or "Action.Reports.Edit" or "Action.TestHistory.Delete" or "Action.Maintenance.Reset" or "Action.HarnessDatabase.Edit" or "Action.Settings.Edit";
            }
            if (roleId == 4) // Technician
            {
                return key is "Page.Dashboard" or "Page.TestExecution" or "Page.HarnessDatabase" or "Page.TestHistory" or "Page.SystemLog" or "Page.Settings" or "Action.TestExecution.Run" or "Action.Instruments.Calibrate" or "Action.Reports.Export" or "Action.Maintenance.Reset" or "Action.HarnessDatabase.Edit";
            }
            if (roleId == 5) // Quality
            {
                return key is "Page.Dashboard" or "Page.TestExecution" or "Page.HarnessDatabase" or "Page.TestHistory" or "Page.Reports" or "Action.Reports.Export";
            }
            if (roleId == 6) // Supervisor
            {
                return key is "Page.Dashboard" or "Page.TestExecution" or "Page.HarnessDatabase" or "Page.TestHistory" or "Page.Reports" or "Page.SystemLog" or "Action.TestExecution.Run" or "Action.Reports.Export" or "Action.Reports.Edit" or "Action.TestHistory.Delete";
            }
            // A role beyond the six shipped ones starts at the lowest useful rung rather than at
            // nothing, so Reset Defaults cannot strip it down to an account that cannot sign in
            // anywhere. Everything past the Dashboard is granted deliberately.
            return key == "Page.Dashboard";
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
                ("Action.Database.Backup", "System & Logs", "Create and Restore Database Backups"),

                // Appended, never slotted mid-list: seed ids follow position and would renumber.
                ("Action.Reports.Edit", "System & Logs", "Amend and Delete Saved Test Reports"),
                ("Action.TestHistory.Delete", "System & Logs", "Delete Test History Records"),
                ("Action.Maintenance.Reset", "System & Logs", "Reset the Maintenance Countdown"),
                ("Action.HarnessDatabase.Edit", "Test Operations", "Add, Edit, and Delete Harnesses and Connectors"),
                ("Action.Settings.Edit", "System & Logs", "Change Default Test Settings")
            };
        }
    }
}
