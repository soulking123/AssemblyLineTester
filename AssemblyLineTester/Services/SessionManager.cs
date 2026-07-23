using System.Collections.Generic;
using System.Linq;
using AssemblyLineTester.Data;
using AssemblyLineTester.Models;

namespace AssemblyLineTester.Services
{
    /// Manages the active user login session globally across the application.
    public static class SessionManager
    {

        private static readonly HashSet<string> _userPermissions = new();

        /// The currently logged-in user.
        public static User? CurrentUser { get; private set; }

        /// True if there is an active logged-in user; otherwise false.
        public static bool IsLoggedIn => CurrentUser != null;

        /// Sets the active user session and loads their assigned permissions.
        public static void SetSession(User user)
        {
            CurrentUser = user;
            LoadUserPermissions(user);
        }

        /// Clears the active user session.
        public static void ClearSession()
        {
            CurrentUser = null;
            _userPermissions.Clear();
        }

        /// Checks if the currently logged-in user has the specified permission.
        /// Always returns true for Administrator role to guarantee anti-lockout protection.
        public static bool HasPermission(string permissionKey)
        {
            if (CurrentUser == null) return false;
            if (CurrentUser.Role?.Name == "Administrator" || CurrentUser.RoleId == 1) return true;
            return _userPermissions.Contains(permissionKey);
        }

        /// Loads and caches all granted permissions for the specified user.
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

        /// Refreshes the active user's permissions from the database.
        public static void RefreshPermissions()
        {
            if (CurrentUser != null)
            {
                LoadUserPermissions(CurrentUser);
            }
        }
    }
}
