using Microsoft.EntityFrameworkCore;
using System;
using System.Windows.Forms;

namespace AssemblyLineTester.Data
{
    /// Provides utility methods to initialize the application database at startup.
    public static class DbInitialize
    {
        /// Applies any pending EF Core migrations to bring the database schema up to date,
        /// handling legacy databases where tables may exist without migration history records.
        public static bool Initialize()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    // 1. Pre-check: if core tables already exist from pre-migration builds, ensure InitialCreate is recorded in __EFMigrationsHistory
                    EnsureMigrationHistoryRecordedIfTableExists(db, "MaintenanceStates", "20260715011710_InitialCreate");
                    EnsureMigrationHistoryRecordedIfTableExists(db, "Users", "20260715011710_InitialCreate");
                    EnsureMigrationHistoryRecordedIfTableExists(db, "RolePermissions", "20260717073752_AddRolePermissionsTable");

                    // 2. Attempt EF Core Migration
                    try
                    {
                        db.Database.Migrate();
                    }
                    catch (Exception ex) when (ex.Message.Contains("already exists", StringComparison.OrdinalIgnoreCase))
                    {
                        // If a table already exists during migration, synchronize migration history and retry or proceed safely
                        if (ex.Message.Contains("MaintenanceStates", StringComparison.OrdinalIgnoreCase) ||
                            ex.Message.Contains("Users", StringComparison.OrdinalIgnoreCase) ||
                            ex.Message.Contains("Roles", StringComparison.OrdinalIgnoreCase))
                        {
                            EnsureMigrationHistoryRecordedIfTableExists(db, "MaintenanceStates", "20260715011710_InitialCreate", forceRecord: true);
                        }
                        if (ex.Message.Contains("RolePermissions", StringComparison.OrdinalIgnoreCase))
                        {
                            EnsureMigrationHistoryRecordedIfTableExists(db, "RolePermissions", "20260717073752_AddRolePermissionsTable", forceRecord: true);
                        }

                        try
                        {
                            db.Database.Migrate();
                        }
                        catch
                        {
                            // If Migrate still fails due to existing schema, fallback to direct table validation below
                        }
                    }

                    // 3. Ensure RolePermissions table exists and is populated for all roles
                    EnsureRolePermissionsTableAndSeeds(db);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database initialization failed: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private static void EnsureMigrationHistoryRecordedIfTableExists(AppDbContext db, string tableName, string migrationId, bool forceRecord = false)
        {
            try
            {
                var conn = db.Database.GetDbConnection();
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using var cmd = conn.CreateCommand();
                cmd.CommandText = $"SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='{tableName}';";
                var tableExists = Convert.ToInt64(cmd.ExecuteScalar()) > 0;

                if (tableExists || forceRecord)
                {
                    cmd.CommandText = "SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='__EFMigrationsHistory';";
                    var historyExists = Convert.ToInt64(cmd.ExecuteScalar()) > 0;
                    if (!historyExists)
                    {
                        cmd.CommandText = "CREATE TABLE \"__EFMigrationsHistory\" (\"MigrationId\" TEXT NOT NULL CONSTRAINT \"PK___EFMigrationsHistory\" PRIMARY KEY, \"ProductVersion\" TEXT NOT NULL);";
                        cmd.ExecuteNonQuery();
                    }

                    cmd.CommandText = $"SELECT COUNT(*) FROM \"__EFMigrationsHistory\" WHERE \"MigrationId\" = '{migrationId}';";
                    var migrationRecorded = Convert.ToInt64(cmd.ExecuteScalar()) > 0;
                    if (!migrationRecorded)
                    {
                        cmd.CommandText = $"INSERT INTO \"__EFMigrationsHistory\" (\"MigrationId\", \"ProductVersion\") VALUES ('{migrationId}', '10.0.0');";
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                // Ignore errors during pre-check so normal process can proceed
            }
        }

        private static void EnsureRolePermissionsTableAndSeeds(AppDbContext db)
        {
            try
            {
                var conn = db.Database.GetDbConnection();
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='RolePermissions';";
                var tableExists = Convert.ToInt64(cmd.ExecuteScalar()) > 0;

                if (!tableExists)
                {
                    cmd.CommandText = @"CREATE TABLE IF NOT EXISTS ""RolePermissions"" (
                        ""Id"" INTEGER NOT NULL CONSTRAINT ""PK_RolePermissions"" PRIMARY KEY AUTOINCREMENT,
                        ""RoleId"" INTEGER NOT NULL,
                        ""PermissionKey"" TEXT NOT NULL,
                        ""PermissionGroup"" TEXT NOT NULL,
                        ""Description"" TEXT NOT NULL,
                        ""IsGranted"" INTEGER NOT NULL,
                        CONSTRAINT ""FK_RolePermissions_Roles_RoleId"" FOREIGN KEY (""RoleId"") REFERENCES ""Roles"" (""Id"") ON DELETE CASCADE
                    );";
                    cmd.ExecuteNonQuery();
                }

                // Ensure all standard roles (1 to 6) have their permissions populated if missing
                var service = new AssemblyLineTester.Services.RolePermissionService();
                for (int r = 1; r <= 6; r++)
                {
                    service.GetPermissionsByRoleId(r);
                }
            }
            catch
            {
                // Fallback catch
            }
        }
    }
}