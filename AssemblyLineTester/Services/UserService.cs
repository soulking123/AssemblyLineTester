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
        public void CreateUser(User newUser, string plainPassword)
        {
            if (SessionManager.CurrentUser?.Role?.Name != "Administrator")
            {
                throw new UnauthorizedAccessException("Access denied. Only administrators can create new users.");
            }

            if (newUser == null) throw new ArgumentNullException(nameof(newUser));
            if (string.IsNullOrWhiteSpace(newUser.Username)) throw new ArgumentException("Username is required.");
            if (string.IsNullOrWhiteSpace(plainPassword)) throw new ArgumentException("Password is required.");
            if (string.IsNullOrWhiteSpace(newUser.FullName)) throw new ArgumentException("Full Name is required.");

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
                newUser.CreatedAt = DateTime.UtcNow;
                newUser.UpdatedAt = DateTime.UtcNow;

                // Prevent EF Core from attempting to insert duplicate navigation entities
                newUser.Role = null!;
                newUser.Status = null!;

                if (newUser.UserId == 0)
                {
                    int maxId = db.Users.Any() ? db.Users.Max(u => u.UserId) : 0;
                    newUser.UserId = maxId + 1;
                }

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
