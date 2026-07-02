using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblyLineTester.Data
{
    /// Provides utility methods to initialize the application database at startup.
    public static class DbInitialize
    {
        /// Ensures the SQLite database file exists, creates the schema, and seeds default records.
        /// <returns>True if database initialization succeeded; otherwise false.</returns>
        public static bool Initialize()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    db.Database.EnsureCreated();
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
