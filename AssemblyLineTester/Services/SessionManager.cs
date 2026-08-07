using System.Collections.Generic;
using System.Linq;
using AssemblyLineTester.Data;
using AssemblyLineTester.Models;

namespace AssemblyLineTester.Services
{
    // The active login session, shared across the application.
    public static class SessionManager
    {

        private static readonly HashSet<string> _userPermissions = new();

        public static User? CurrentUser { get; private set; }

        public static bool IsLoggedIn => CurrentUser != null;

        // Opens a session and loads that user's permissions.
        public static void SetSession(User user)
        {
            CurrentUser = user;
            LoadUserPermissions(user);
        }

        public static void ClearSession()
        {
            CurrentUser = null;
            _userPermissions.Clear();
        }

        // Role id 1 always passes; the name is editable, so the id is the anchor.
        public static bool HasPermission(string permissionKey)
        {
            if (CurrentUser == null) return false;
            if (CurrentUser.RoleId == 1) return true;
            return _userPermissions.Contains(permissionKey);
        }

        // Caches the granted permission keys for one user.
        public static void LoadUserPermissions(User user)
        {
            _userPermissions.Clear();
            if (user == null) return;

            using var db = new AppDbContext();
            SeedMissingPermissions(db, user.RoleId);

            var perms = db.RolePermissions
                .Where(rp => rp.RoleId == user.RoleId && rp.IsGranted)
                .Select(rp => rp.PermissionKey)
                .ToList();

            foreach (var key in perms)
            {
                _userPermissions.Add(key);
            }
        }

        /// <summary>
        /// Adds any standard permission key this role has no row for yet, at its shipped default.
        /// </summary>
        /// <remarks>
        /// A key added in a later release exists only in code until something writes it. The Users
        /// and Roles page did that, but a role that cannot open it would be refused a permission it
        /// is supposed to hold - so the gap is closed at sign-in instead.
        /// </remarks>
        private static void SeedMissingPermissions(AppDbContext db, int roleId)
        {
            try
            {
                var have = db.RolePermissions
                    .Where(rp => rp.RoleId == roleId)
                    .Select(rp => rp.PermissionKey)
                    .ToHashSet();

                var missing = RolePermissionService.GetStandardPermissionDefinitions()
                    .Where(d => !have.Contains(d.Key))
                    .ToList();

                if (missing.Count == 0) return;

                foreach (var (key, group, description) in missing)
                {
                    db.RolePermissions.Add(new RolePermission
                    {
                        RoleId = roleId,
                        PermissionKey = key,
                        PermissionGroup = group,
                        Description = description,
                        IsGranted = RolePermissionService.GetDefaultGrantState(roleId, key)
                    });
                }

                db.SaveChanges();
                FileLogService.Info("SessionManager.SeedMissingPermissions",
                    $"Added {missing.Count} new permission key(s) for role {roleId}.");
            }
            catch (Exception ex)
            {
                // Never block a sign-in over this; the user simply keeps the rights already stored.
                FileLogService.Warn("SessionManager.SeedMissingPermissions",
                    $"Could not add new permission keys for role {roleId}: {ex.Message}");
            }
        }

        public static void RefreshPermissions()
        {
            if (CurrentUser != null)
            {
                LoadUserPermissions(CurrentUser);
            }
        }
    }
}
