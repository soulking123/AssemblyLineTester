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
            var perms = db.RolePermissions
                .Where(rp => rp.RoleId == user.RoleId && rp.IsGranted)
                .Select(rp => rp.PermissionKey)
                .ToList();

            foreach (var key in perms)
            {
                _userPermissions.Add(key);
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
