using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AssemblyLineTester.Data;
using AssemblyLineTester.Models;

namespace AssemblyLineTester.Services
{
    /// Provides database operations for user accounts.
    public class UserService
    {
        /// Retrieves all user accounts from the database.
        /// Throws an UnauthorizedAccessException if the current user is not an administrator.
        public List<User> GetAllUsers()
        {
            if (SessionManager.CurrentUser?.Role?.Name != "Administrator")
            {
                throw new UnauthorizedAccessException("Access denied. Only administrators can view user data.");
            }

            using (var db = new AppDbContext())
            {
                return db.Users
                    .Include(u => u.Role)
                    .Include(u => u.Status)
                    .ToList();
            }
        }

        /// Creates a new user account in the database after validating permissions and uniqueness.
        /// Throws an UnauthorizedAccessException if the current user is not an administrator.
        public void CreateUser(User newUser, string plainPassword = DefaultNewUserPassword)
        {
            if (SessionManager.CurrentUser?.Role?.Name != "Administrator")
            {
                throw new UnauthorizedAccessException("Access denied. Only administrators can create new users.");
            }

            if (newUser == null) throw new ArgumentNullException(nameof(newUser));
            if (string.IsNullOrWhiteSpace(newUser.Username)) throw new ArgumentException("Username is required.");
            if (string.IsNullOrWhiteSpace(newUser.FullName)) throw new ArgumentException("Full Name is required.");

            string? invalidPassword = ValidatePassword(plainPassword);
            if (invalidPassword is not null) throw new ArgumentException(invalidPassword, nameof(plainPassword));

            using (var db = new AppDbContext())
            {
                string cleanUsername = newUser.Username.Trim().ToLower();
                if (db.Users.Any(u => u.Username.ToLower() == cleanUsername))
                {
                    throw new InvalidOperationException($"A user with username '{newUser.Username}' already exists.");
                }

                if (!string.IsNullOrWhiteSpace(newUser.CompanyId))
                {
                    string cleanCompanyId = newUser.CompanyId.Trim().ToLower();
                    if (db.Users.Any(u => u.CompanyId.ToLower() == cleanCompanyId))
                    {
                        throw new InvalidOperationException($"A user with Company ID '{newUser.CompanyId}' already exists.");
                    }
                }

                newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(plainPassword);

                // The administrator picked this password, so two people know it from the start.
                newUser.MustChangePassword = true;

                newUser.CreatedAt = DateTime.UtcNow;
                newUser.UpdatedAt = DateTime.UtcNow;

                // Prevent EF Core from attempting to insert duplicate navigation entities
                newUser.Role = null!;
                newUser.Status = null!;

                // UserId is left at 0 so SQLite assigns it. Picking MAX+1 by hand reused the id of a
                // deleted account, which would silently repoint that account's old test reports at
                // whoever was created next.
                db.Users.Add(newUser);
                db.SaveChanges();

                string roleName = db.Roles.Where(r => r.Id == newUser.RoleId).Select(r => r.Name).FirstOrDefault()
                                  ?? newUser.RoleId.ToString();

                new SystemLogService().Log(
                    Models.SystemLog.LogSeverity.Warning,
                    "User Management",
                    "Create Account",
                    $"Account '{newUser.Username}' created with role {roleName}.");
            }
        }

        /// <summary>Shortest password the application will accept.</summary>
        public const int MinimumPasswordLength = 8;

        /// <summary>
        /// Password every new account starts with. The account is created with
        /// <see cref="Models.User.MustChangePassword"/> set, so the holder replaces it on first
        /// sign-in and this value never survives past handover.
        /// </summary>
        /// <remarks>
        /// A known constant rather than something random, so the administrator can hand an account
        /// over without also having to transmit a secret. The trade is real and worth naming: until
        /// the new user first signs in, anyone who knows this value can sign in as them and set the
        /// password themselves. Keep the gap short - create accounts when they are about to be used,
        /// not in a batch weeks ahead.
        /// </remarks>
        public const string DefaultNewUserPassword = "Cat12345";

        /// <summary>
        /// Rejects a password that is too short or blank. Returns null when it is acceptable.
        /// </summary>
        /// <remarks>
        /// One rule, applied at both the self-service change and the administrator reset, so the two
        /// paths cannot drift apart. Deliberately not a complexity ruleset: length is what actually
        /// costs an attacker time, and character-class rules mostly produce Password1!.
        /// </remarks>
        public static string? ValidatePassword(string? plainPassword)
        {
            if (string.IsNullOrWhiteSpace(plainPassword))
                return "Password is required.";

            if (plainPassword.Length < MinimumPasswordLength)
                return $"Password must be at least {MinimumPasswordLength} characters.";

            return null;
        }

        /// <summary>
        /// Changes the signed-in user's own password after checking the current one.
        /// </summary>
        public void ChangeOwnPassword(string currentPassword, string newPassword)
        {
            User? session = SessionManager.CurrentUser
                ?? throw new InvalidOperationException("You are not signed in.");

            string? invalid = ValidatePassword(newPassword);
            if (invalid is not null) throw new ArgumentException(invalid, nameof(newPassword));

            using var db = new AppDbContext();
            var user = db.Users.FirstOrDefault(u => u.UserId == session.UserId)
                ?? throw new InvalidOperationException("Account not found.");

            // Verified against the stored hash, not against the session, so a machine left unlocked
            // is not enough on its own to take the account over.
            if (!BCrypt.Net.BCrypt.Verify(currentPassword, user.PasswordHash))
                throw new UnauthorizedAccessException("The current password is incorrect.");

            if (BCrypt.Net.BCrypt.Verify(newPassword, user.PasswordHash))
                throw new ArgumentException("The new password must be different from the current one.", nameof(newPassword));

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.MustChangePassword = false;
            user.UpdatedAt = DateTime.UtcNow;
            db.SaveChanges();

            // The session carries a copy of the row, so it has to be told as well or the forced
            // change would be demanded again on the next check.
            session.MustChangePassword = false;
            session.PasswordHash = user.PasswordHash;

            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Warning, "User Management", "Change Password",
                $"User '{user.Username}' changed their own password.");
        }

        /// <summary>
        /// Sets another account's password. The recipient is required to change it at next sign-in,
        /// because from that moment two people know it.
        /// </summary>
        public void ResetPassword(int userId, string newPassword)
        {
            if (!SessionManager.HasPermission("Action.UserManagement.Edit"))
                throw new UnauthorizedAccessException("Access denied. You are not allowed to manage user accounts.");

            string? invalid = ValidatePassword(newPassword);
            if (invalid is not null) throw new ArgumentException(invalid, nameof(newPassword));

            using var db = new AppDbContext();
            var user = db.Users.FirstOrDefault(u => u.UserId == userId)
                ?? throw new InvalidOperationException("User not found.");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.MustChangePassword = true;
            user.UpdatedAt = DateTime.UtcNow;
            db.SaveChanges();

            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Warning, "User Management", "Reset Password",
                $"Password reset for account '{user.Username}'. A change is required at next sign-in.");
        }

        /// Changes a user account's status. Only an Active account may sign in, so this is the
        /// switch that grants and withdraws access to the application.
        /// Throws an UnauthorizedAccessException if the current user may not manage accounts.
        public void UpdateUserStatus(int userId, int newStatusId)
        {
            if (!SessionManager.HasPermission("Action.UserManagement.Edit"))
            {
                throw new UnauthorizedAccessException("Access denied. You are not allowed to manage user accounts.");
            }

            // Refuse to let someone disable the account they are currently signed in with; there is
            // no second administrator guaranteed to exist to undo it.
            if (SessionManager.CurrentUser?.UserId == userId)
            {
                throw new InvalidOperationException("You cannot change the status of your own account.");
            }

            using var db = new AppDbContext();

            var user = db.Users.FirstOrDefault(u => u.UserId == userId)
                ?? throw new InvalidOperationException("User not found.");

            var status = db.UserStatuses.FirstOrDefault(s => s.Id == newStatusId)
                ?? throw new ArgumentException($"Unknown status id {newStatusId}.", nameof(newStatusId));

            if (user.StatusId == newStatusId)
            {
                return;
            }

            string previous = db.UserStatuses
                .Where(s => s.Id == user.StatusId)
                .Select(s => s.Name)
                .FirstOrDefault() ?? user.StatusId.ToString();

            user.StatusId = newStatusId;
            user.UpdatedAt = DateTime.UtcNow;
            db.SaveChanges();

            // Granting or withdrawing access is exactly the kind of change that needs to be
            // answerable later, so it goes in the system log rather than only in the row itself.
            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Warning,
                "User Management",
                "Change Account Status",
                $"Account '{user.Username}' changed from {previous} to {status.Name}.");
        }
    }
}
