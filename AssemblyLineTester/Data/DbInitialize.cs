using AssemblyLineTester.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;

namespace AssemblyLineTester.Data
{
    /// Brings the database schema up to date at startup.
    public static class DbInitialize
    {
        /// <summary>
        /// Applies any pending EF Core migrations. Returns false if the database could not be
        /// brought up to date, in which case the application must not carry on: running against a
        /// half-built schema corrupts data in ways that only surface weeks later.
        /// </summary>
        public static bool Initialize()
        {
            try
            {
                using var db = new AppDbContext();

                string[] pending = [.. db.Database.GetPendingMigrations()];
                db.Database.Migrate();

                // Write-ahead logging survives an unclean shutdown far better than the default
                // rollback journal, and a factory losing power mid-test is routine.
                db.Database.ExecuteSqlRaw("PRAGMA journal_mode=WAL;");

                if (pending.Length > 0)
                {
                    FileLogService.Info("DbInitialize", $"Schema migrated: {string.Join(", ", pending)}");
                }

                return true;
            }
            catch (Exception ex)
            {
                FileLogService.Error("DbInitialize", "Database initialisation failed.", ex);

                MessageBox.Show(
                    $"The database could not be opened or upgraded, so the application cannot start.\r\n\r\n" +
                    $"{ex.Message}\r\n\r\nThe details are in the log folder.",
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
        }
    }
}
