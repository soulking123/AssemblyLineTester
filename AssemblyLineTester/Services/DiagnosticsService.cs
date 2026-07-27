using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using AssemblyLineTester.Helpers;

namespace AssemblyLineTester.Services
{
    /// <summary>
    /// Packs the log files and a short machine summary into one zip the operator can send.
    /// </summary>
    /// <remarks>
    /// This is the stand-in for remote debugging: a factory PC is not somewhere an engineer can
    /// attach a debugger, so the machine has to be able to describe itself in one file.
    /// The database is deliberately not included - it holds test results and password hashes, and
    /// bundles travel over chat apps.
    /// </remarks>
    public static class DiagnosticsService
    {
        /// <summary>Log files newer than this go in the bundle. Older ones are rarely the cause.</summary>
        public const int DaysIncluded = 7;

        /// <summary>Suggested file name, stamped so two bundles from one machine cannot collide.</summary>
        public static string SuggestedFileName() =>
            $"ASL-diagnostics-{AppInfo.MachineName}-{DateTime.Now:yyyyMMdd-HHmmss}.zip";

        /// <summary>
        /// Writes the bundle to <paramref name="destinationPath"/> and returns how many log files
        /// went in. A bundle with zero log files is still worth sending: the summary alone names
        /// the version and the machine.
        /// </summary>
        public static int CreateBundle(string destinationPath)
        {
            if (string.IsNullOrWhiteSpace(destinationPath))
                throw new ArgumentException("Destination path is required.", nameof(destinationPath));

            string? folder = Path.GetDirectoryName(Path.GetFullPath(destinationPath));
            if (!string.IsNullOrEmpty(folder)) Directory.CreateDirectory(folder);

            if (File.Exists(destinationPath)) File.Delete(destinationPath);

            DateTime cutoff = DateTime.Now.Date.AddDays(-DaysIncluded);
            int included = 0;

            using var zip = ZipFile.Open(destinationPath, ZipArchiveMode.Create);

            using (Stream entry = zip.CreateEntry("summary.txt").Open())
            using (var writer = new StreamWriter(entry, Encoding.UTF8))
            {
                writer.Write(BuildSummary());
            }

            foreach (string path in FileLogService.LogFiles())
            {
                try
                {
                    if (File.GetLastWriteTime(path) < cutoff) continue;

                    // Copied through a shared-read stream rather than CreateEntryFromFile, because
                    // today's log is open for append by this very process.
                    using Stream source = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    using Stream target = zip.CreateEntry($"logs/{Path.GetFileName(path)}").Open();
                    source.CopyTo(target);
                    included++;
                }
                catch (Exception ex)
                {
                    FileLogService.Warn("DiagnosticsService.CreateBundle",
                        $"Log file '{path}' was skipped: {ex.Message}");
                }
            }

            return included;
        }

        private static string BuildSummary()
        {
            var lines = new List<string>
            {
                "AssemblyLineTester diagnostics bundle",
                $"Created         : {DateTime.Now:yyyy-MM-dd HH:mm:ss} (local)",
                $"Build           : {AppInfo.Version}",
                $"Machine         : {AppInfo.MachineName}",
                $"OS              : {Environment.OSVersion}",
                $"Process         : {(Environment.Is64BitProcess ? "64-bit" : "32-bit")} on {(Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit")} Windows",
                $"CLR             : {Environment.Version}",
                $"Signed-in user  : {SessionManager.CurrentUser?.Username ?? "(nobody)"}",
                $"Windows user    : {Environment.UserName}",
                $"Data folder     : {AppInfo.DataDirectory}",
                $"Log folder      : {AppInfo.LogDirectory}",
                $"Logs included   : last {DaysIncluded} days",
                "",
                "Instrument",
                $"  Connected     : {InstrumentService.IsOnline}",
                "",
                "Screens",
            };

            try
            {
                lines.AddRange(Screen.AllScreens.Select(s =>
                    $"  {s.DeviceName} {s.Bounds.Width}x{s.Bounds.Height}{(s.Primary ? " (primary)" : "")}"));
            }
            catch (Exception ex)
            {
                lines.Add($"  unavailable: {ex.Message}");
            }

            return string.Join(Environment.NewLine, lines) + Environment.NewLine;
        }
    }
}
