using AssemblyLineTester.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace AssemblyLineTester.Services
{
    // Handles SQLite backup and restore using the native SQLite backup API,
    // which is safe to use even while the database is actively being used by the app.
    public static class DatabaseBackupService
    {
        private const string DbFileName = "users.db";

        // COPIES THE LIVE DATABASE TO A BACKUP FILE AT THE GIVEN PATH.
        public static void Backup(string destinationPath)
        {
            if (string.IsNullOrWhiteSpace(destinationPath))
                throw new ArgumentException("Destination path is required.", nameof(destinationPath));

            var destDir = Path.GetDirectoryName(Path.GetFullPath(destinationPath));
            if (!string.IsNullOrEmpty(destDir) && !Directory.Exists(destDir))
                throw new DirectoryNotFoundException($"Destination folder not found: {destDir}");

            using var source = new SqliteConnection($"Data Source={DbFileName}");
            using var target = new SqliteConnection($"Data Source={destinationPath}");
            source.Open();
            target.Open();

            // Copies the entire database page-by-page; safe even while source is in use.
            source.BackupDatabase(target);
        }

        // REPLACES THE LIVE DATABASE WITH THE CONTENTS OF A BACKUP FILE.
        public static void Restore(string sourcePath)
        {
            if (string.IsNullOrWhiteSpace(sourcePath))
                throw new ArgumentException("Source path is required.", nameof(sourcePath));
            if (!File.Exists(sourcePath))
                throw new FileNotFoundException("Backup file not found.", sourcePath);

            // Release pooled connections first so restore does not fight an open handle.
            SqliteConnection.ClearAllPools();

            using var backup = new SqliteConnection($"Data Source={sourcePath}");
            using var live = new SqliteConnection($"Data Source={DbFileName}");
            backup.Open();
            live.Open();

            backup.BackupDatabase(live);

            // Re-apply migrations in case the restored file is from an older schema version.
            using var db = new AppDbContext();
            db.Database.Migrate();
        }
    }
}