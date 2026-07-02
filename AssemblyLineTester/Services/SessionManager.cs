using AssemblyLineTester.Models;

namespace AssemblyLineTester.Services
{
    /// Manages the active user login session globally across the application.
    public static class SessionManager
    {
        /// The currently logged-in user.
        public static User? CurrentUser { get; private set; }

        /// True if there is an active logged-in user; otherwise false.
        public static bool IsLoggedIn => CurrentUser != null;

        /// Sets the active user session.
        public static void SetSession(User user)
        {
            CurrentUser = user;
        }

        /// Clears the active user session.
        public static void ClearSession()
        {
            CurrentUser = null;
        }
    }
}
