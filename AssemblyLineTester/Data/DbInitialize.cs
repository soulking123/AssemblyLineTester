using AssemblyLineTester.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;

namespace AssemblyLineTester.Data
{
    public static class DbInitialize
    {
        // Applies pending migrations; false means the application must not carry on.
        public static bool Initialize()
        {
            try
            {
                using var db = new AppDbContext();

                string[] pending = [.. db.Database.GetPendingMigrations()];
                db.Database.Migrate();

            // WAL survives an unclean shutdown; a factory losing power mid-test is routine.
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
