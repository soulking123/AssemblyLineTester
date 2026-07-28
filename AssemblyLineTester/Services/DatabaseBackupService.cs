using AssemblyLineTester.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;

namespace AssemblyLineTester.Services
{
    // SQLite backup and restore via the native backup API, safe while the app holds the file.
    public static class DatabaseBackupService
    {
        private static string DbPath => AppDbContext.DbPath;

        private static readonly string AutoBackupFolder = Path.Combine(AppDbContext.DataFolder, "Backups");

        private const int MaxAutoBackups = 10;
        private const int MaxPreRestoreCopies = 5;

        // Three backups a day; a restart sooner than that reuses the fresh one.
        private const int MinHoursBetweenAutoBackups = 8;

        // Copies the live database to the given path.
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

            source.BackupDatabase(target);

            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Info, "Database", "Backup Database",
                $"Database backed up to '{destinationPath}'.");
        }

        // Replaces the live database from a backup file, keeping a safety copy first.
        public static void Restore(string sourcePath)
        {
            if (string.IsNullOrWhiteSpace(sourcePath))
                throw new ArgumentException("Source path is required.", nameof(sourcePath));
            if (!File.Exists(sourcePath))
                throw new FileNotFoundException("Backup file not found.", sourcePath);

            // Taken before anything is touched, so a failure here stops the restore.
            string safetyCopy = Path.Combine(
                AppDbContext.DataFolder,
                $"pre-restore-{DateTime.Now:yyyyMMdd-HHmmss}.db");

            if (File.Exists(DbPath))
            {
                Backup(safetyCopy);
            }

            // Pooled connections go first, or the restore fights an open handle.
            SqliteConnection.ClearAllPools();

            using var backup = new SqliteConnection($"Data Source={sourcePath}");
            using var live = new SqliteConnection($"Data Source={DbPath}");
            backup.Open();
            live.Open();

            backup.BackupDatabase(live);

            // The restored file may carry an older schema.
            using var db = new AppDbContext();
            db.Database.Migrate();

            FileLogService.Warn("DatabaseBackupService.Restore",
                $"Live database replaced from '{sourcePath}'. Previous contents saved to '{safetyCopy}'.");
            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Warning, "Database", "Restore Database",
                $"Database restored from '{sourcePath}'. Previous data was saved to '{Path.GetFileName(safetyCopy)}' first.");
        }

        // Startup backup nobody has to remember. Failures are logged, never thrown.
        public static void RunAutomaticBackup()
        {
            try
            {
                SelfCheck();
                Directory.CreateDirectory(AutoBackupFolder);

                if (HasRecentBackup())
                {
                    return;
                }

                string destination = Path.Combine(AutoBackupFolder, $"auto-backup-{DateTime.Now:yyyyMMdd-HHmmss}.db");
                Backup(destination);

                PruneOldBackups(AutoBackupFolder, "auto-backup-*.db", MaxAutoBackups);

                // Pre-restore copies grow without limit too.
                PruneOldBackups(AppDbContext.DataFolder, "pre-restore-*.db", MaxPreRestoreCopies);
            }
            catch (Exception ex)
            {
                FileLogService.Error("DatabaseBackupService.RunAutomaticBackup", "Automatic backup failed.", ex);
            }
        }

        private static bool HasRecentBackup()
        {
            string? newest = Directory.GetFiles(AutoBackupFolder, "auto-backup-*.db")
                .OrderByDescending(f => f, StringComparer.Ordinal)
                .FirstOrDefault();

            return IsRecent(newest, DateTime.Now);
        }

        // Age comes from the filename, not the file timestamp, which copying would have reset.
        internal static bool IsRecent(string? newestBackupPath, DateTime now)
        {
            if (newestBackupPath is null) return false;

            const string prefix = "auto-backup-";
            string name = Path.GetFileNameWithoutExtension(newestBackupPath);
            if (!name.StartsWith(prefix, StringComparison.Ordinal)) return false;

            return DateTime.TryParseExact(name[prefix.Length..], "yyyyMMdd-HHmmss",
                       CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime taken)
                   && taken > now.AddHours(-MinHoursBetweenAutoBackups);
        }

        // Debug-only guard on the backup interval rule.
        [Conditional("DEBUG")]
        public static void SelfCheck()
        {
            var now = new DateTime(2026, 1, 1, 12, 0, 0);

            Debug.Assert(!IsRecent(null, now), "no backup at all must not count as recent");
            Debug.Assert(IsRecent(@"C:\b\auto-backup-20260101-110000.db", now), "an hour old must count as recent");
            Debug.Assert(!IsRecent(@"C:\b\auto-backup-20260101-030000.db", now), "exactly eight hours old must expire");
            Debug.Assert(!IsRecent(@"C:\b\auto-backup-notadate.db", now), "an unreadable name must not block a backup");
            Debug.Assert(!IsRecent(@"C:\b\pre-restore-20260101-110000.db", now), "a pre-restore copy is not an auto backup");
        }

        // Names sort chronologically, so newest-first needs no file metadata.
        private static void PruneOldBackups(string folder, string searchPattern, int keep)
        {
            var files = Directory.GetFiles(folder, searchPattern)
                .OrderByDescending(f => f, StringComparer.Ordinal)
                .Skip(keep)
                .ToList();

            foreach (var file in files)
            {
                File.Delete(file);
            }

            if (files.Count > 0)
            {
                // Logged even though nothing failed; deletion erases its own evidence.
                FileLogService.Warn("DatabaseBackupService.PruneOldBackups",
                    $"{files.Count} old backup file(s) removed from '{folder}' (kept newest {keep}).");
            }
        }
    }
}