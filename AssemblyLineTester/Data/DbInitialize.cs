using Microsoft.EntityFrameworkCore;
using System;
using System.Windows.Forms;

namespace AssemblyLineTester.Data
{
    /// Provides utility methods to initialize the application database at startup.
    public static class DbInitialize
    {
        /// Applies any pending EF Core migrations to bring the database schema up to date,
        /// creating the database file itself if it does not exist yet.
        public static bool Initialize()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    db.Database.Migrate();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database initialization failed: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}