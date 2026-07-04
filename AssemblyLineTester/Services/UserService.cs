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
            }
        }
    }
}
