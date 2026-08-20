using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AssemblyLineTester.Data;
using AssemblyLineTester.Models;

namespace AssemblyLineTester.Services
{
    // Database operations for user accounts.
    public class UserService
    {
        // Every account, with role and status loaded.
        public List<User> GetAllUsers()
        {
            if (!SessionManager.HasPermission("Page.UserManagement"))
            {
                throw new UnauthorizedAccessException("Access denied. You are not allowed to view user data.");
            }

            using (var db = new AppDbContext())
            {
                return db.Users
                    .Include(u => u.Role)
                    .Include(u => u.Status)
                    .ToList();
            }
        }

        // Creates an account after checking permission and uniqueness.
        public void CreateUser(User newUser, string plainPassword = DefaultNewUserPassword)
        {
            if (!SessionManager.HasPermission("Action.UserManagement.Edit"))
            {
                throw new UnauthorizedAccessException("Access denied. You are not allowed to manage user accounts.");
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

                newUser.MustChangePassword = true;

                newUser.CreatedAt = DateTime.UtcNow;
                newUser.UpdatedAt = DateTime.UtcNow;

                // Cleared so EF does not try to insert the navigation entities again.
                newUser.Role = null!;
                newUser.Status = null!;

                // Left at 0 so SQLite assigns it; MAX+1 by hand reused ids of deleted accounts.
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

        // Updates an existing account's identity, role, department and status.
        public void UpdateUser(User updated)
        {
            if (!SessionManager.HasPermission("Action.UserManagement.Edit"))
            {
                throw new UnauthorizedAccessException("Access denied. You are not allowed to manage user accounts.");
            }

            if (updated == null) throw new ArgumentNullException(nameof(updated));
            if (string.IsNullOrWhiteSpace(updated.Username)) throw new ArgumentException("Username is required.");
            if (string.IsNullOrWhiteSpace(updated.FullName)) throw new ArgumentException("Full Name is required.");

            using var db = new AppDbContext();

            var user = db.Users.FirstOrDefault(u => u.UserId == updated.UserId)
                ?? throw new InvalidOperationException("User not found.");

            string cleanUsername = updated.Username.Trim().ToLower();
            if (db.Users.Any(u => u.UserId != updated.UserId && u.Username.ToLower() == cleanUsername))
            {
                throw new InvalidOperationException($"A user with username '{updated.Username}' already exists.");
            }

            if (!string.IsNullOrWhiteSpace(updated.CompanyId))
            {
                string cleanCompanyId = updated.CompanyId.Trim().ToLower();
                if (db.Users.Any(u => u.UserId != updated.UserId && u.CompanyId.ToLower() == cleanCompanyId))
                {
                    throw new InvalidOperationException($"A user with Company ID '{updated.CompanyId}' already exists.");
                }
            }

            // Same rule UpdateUserStatus enforces: nobody may lock themselves out, and no second
            // admin is guaranteed to be there to undo it.
            if (SessionManager.CurrentUser?.UserId == updated.UserId && user.StatusId != updated.StatusId)
            {
                throw new InvalidOperationException("You cannot change the status of your own account.");
            }

            string previousDetails = $"{user.FullName}, {user.Department}, role {user.RoleId}, status {user.StatusId}";

            user.Username = updated.Username.Trim();
            user.CompanyId = updated.CompanyId?.Trim() ?? string.Empty;
            user.FullName = updated.FullName.Trim();
            user.RoleId = updated.RoleId;
            user.Department = updated.Department;
            user.StatusId = updated.StatusId;
            user.UpdatedAt = DateTime.UtcNow;

            db.SaveChanges();

            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Warning,
                "User Management",
                "Edit Account",
                $"Account '{user.Username}' updated (was: {previousDetails}).");
        }

        // Shortest password the application will accept.
        public const int MinimumPasswordLength = 8;

        // Known on purpose so handover needs no secret; MustChangePassword closes the gap at first sign-in.
        public const string DefaultNewUserPassword = "Cat12345";

        // Returns null when the password is acceptable. Length only, no character-class rules.
        public static string? ValidatePassword(string? plainPassword)
        {
            if (string.IsNullOrWhiteSpace(plainPassword))
                return "Password is required.";

            if (plainPassword.Length < MinimumPasswordLength)
                return $"Password must be at least {MinimumPasswordLength} characters.";

            return null;
        }

        // Changes the signed-in user's own password after checking the current one.
        public void ChangeOwnPassword(string currentPassword, string newPassword)
        {
            User? session = SessionManager.CurrentUser
                ?? throw new InvalidOperationException("You are not signed in.");

            string? invalid = ValidatePassword(newPassword);
            if (invalid is not null) throw new ArgumentException(invalid, nameof(newPassword));

            using var db = new AppDbContext();
            var user = db.Users.FirstOrDefault(u => u.UserId == session.UserId)
                ?? throw new InvalidOperationException("Account not found.");

            // Checked against the stored hash, not the session.
            if (!BCrypt.Net.BCrypt.Verify(currentPassword, user.PasswordHash))
                throw new UnauthorizedAccessException("The current password is incorrect.");

            if (BCrypt.Net.BCrypt.Verify(newPassword, user.PasswordHash))
                throw new ArgumentException("The new password must be different from the current one.", nameof(newPassword));

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.MustChangePassword = false;
            user.UpdatedAt = DateTime.UtcNow;
            db.SaveChanges();

            // The session holds a copy of the row and has to be told too.
            session.MustChangePassword = false;
            session.PasswordHash = user.PasswordHash;

            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Warning, "User Management", "Change Password",
                $"User '{user.Username}' changed their own password.");
        }

        // Sets another account's password and forces a change at next sign-in.
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
            // The lockout message points here, so the reset has to release the lock.
            user.FailedLoginCount = 0;
            user.LockoutUntil = null;
            user.UpdatedAt = DateTime.UtcNow;
            db.SaveChanges();

            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Warning, "User Management", "Reset Password",
                $"Password reset for account '{user.Username}'. A change is required at next sign-in.");
        }

        // The switch that grants and withdraws access: only an Active account may sign in.
        public void UpdateUserStatus(int userId, int newStatusId)
        {
            if (!SessionManager.HasPermission("Action.UserManagement.Edit"))
            {
                throw new UnauthorizedAccessException("Access denied. You are not allowed to manage user accounts.");
            }

            // Nobody may disable the account they are signed in with; no second admin is guaranteed.
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

            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Warning,
                "User Management",
                "Change Account Status",
                $"Account '{user.Username}' changed from {previous} to {status.Name}.");
        }
    }
}
