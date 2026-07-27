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
        // Must match the path used in AppDbContext.OnConfiguring().
        private static readonly string DbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "AssemblyLineTester",
            "users.db");

        // COPIES THE LIVE DATABASE TO A BACKUP FILE AT THE GIVEN PATH.
        public static void Backup(string destinationPath)
        {
            if (string.IsNullOrWhiteSpace(destinationPath))
                throw new ArgumentException("Destination path is required.", nameof(destinationPath));

            if (!File.Exists(DbPath))
                throw new FileNotFoundException("Live database not found.", DbPath);

            var destDir = Path.GetDirectoryName(Path.GetFullPath(destinationPath));
            if (!string.IsNullOrEmpty(destDir) && !Directory.Exists(destDir))
                throw new DirectoryNotFoundException($"Destination folder not found: {destDir}");

            using var source = new SqliteConnection($"Data Source={DbPath}");
            using var target = new SqliteConnection($"Data Source={destinationPath}");
            source.Open();
            target.Open();

            // Copies the entire database page-by-page; safe even while source is in use.
            source.BackupDatabase(target);

            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Info, "Database", "Backup Database",
                $"Database backed up to '{destinationPath}'.");
        }

        // REPLACES THE LIVE DATABASE WITH THE CONTENTS OF A BACKUP FILE.
        public static void Restore(string sourcePath)
        {
            if (string.IsNullOrWhiteSpace(sourcePath))
                throw new ArgumentException("Source path is required.", nameof(sourcePath));
            if (!File.Exists(sourcePath))
                throw new FileNotFoundException("Backup file not found.", sourcePath);

            // Restore overwrites everything, and a wrong file picked from a folder full of similar
            // names is unrecoverable otherwise. Taken before any handle is touched, so a failure
            // here stops the restore rather than leaving the operator with neither database.
            string safetyCopy = Path.Combine(
                Path.GetDirectoryName(DbPath)!,
                $"pre-restore-{DateTime.Now:yyyyMMdd-HHmmss}.db");

            if (File.Exists(DbPath))
            {
                Backup(safetyCopy);
            }

            // Release pooled connections first so restore does not fight an open handle.
            SqliteConnection.ClearAllPools();

            using var backup = new SqliteConnection($"Data Source={sourcePath}");
            using var live = new SqliteConnection($"Data Source={DbPath}");
            backup.Open();
            live.Open();

            backup.BackupDatabase(live);

            // Re-apply migrations in case the restored file is from an older schema version.
            using var db = new AppDbContext();
            db.Database.Migrate();

            // Written after the restore, so it lands in the database the operator now has. The
            // file log carries the same line for the case where the restored file is unreadable.
            FileLogService.Warn("DatabaseBackupService.Restore",
                $"Live database replaced from '{sourcePath}'. Previous contents saved to '{safetyCopy}'.");
            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Warning, "Database", "Restore Database",
                $"Database restored from '{sourcePath}'. Previous data was saved to '{Path.GetFileName(safetyCopy)}' first.");
        }
    }
}