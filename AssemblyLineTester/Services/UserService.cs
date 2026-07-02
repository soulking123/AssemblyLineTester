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
    }
}
