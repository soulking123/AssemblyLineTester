using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using AssemblyLineTester.Helpers;

namespace AssemblyLineTester.Services
{
    // Engineer's log, one file per day. A file, not a table, so it survives a broken database.
    public static class FileLogService
    {
        // How long daily files are kept.
        public const int RetentionDays = 30;

        private const string FilePrefix = "asl-";
        private const string FileSuffix = ".log";

        private static readonly object Gate = new();
        private static bool _sweptThisRun;

        public static void Info(string context, string message) => Write("INFO ", context, message, null);

        public static void Warn(string context, string message) => Write("WARN ", context, message, null);

        public static void Error(string context, string message, Exception? ex = null) => Write("ERROR", context, message, ex);

        // Full path of today's log file.
        public static string CurrentFile =>
            Path.Combine(AppInfo.LogDirectory, $"{FilePrefix}{DateTime.Now:yyyy-MM-dd}{FileSuffix}");

        // Daily log files still on disk, newest first.
        public static string[] LogFiles()
        {
            try
            {
                if (!Directory.Exists(AppInfo.LogDirectory)) return [];

                return [.. Directory
                    .EnumerateFiles(AppInfo.LogDirectory, $"{FilePrefix}*{FileSuffix}")
                    .OrderByDescending(f => f)];
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"FileLogService.LogFiles failed: {ex}");
                return [];
            }
        }

        private static void Write(string level, string context, string message, Exception? ex)
        {
            // Logging must never be why the application falls over.
            try
            {
                var line = new StringBuilder()
                    .Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture))
                    .Append(" [").Append(level).Append("] ")
                    .Append(context).Append(" - ")
                    .Append(message);

                if (ex is not null)
                {
                    line.AppendLine()
                        .Append("    ").Append(ex.GetType().FullName).AppendLine()
                        .Append(Indent(ex.ToString()));
                }

                lock (Gate)
                {
                    Directory.CreateDirectory(AppInfo.LogDirectory);
                    SweepOnce();
                    File.AppendAllText(CurrentFile, line.AppendLine().ToString(), Encoding.UTF8);
                }
            }
            catch (Exception loggingFailure)
            {
                Debug.WriteLine($"FileLogService.Write failed: {loggingFailure}");
            }
        }

        private static string Indent(string block) =>
            string.Join(Environment.NewLine, block.Split('\n').Select(l => "    " + l.TrimEnd('\r')));

        // Deletes files past the retention window, at most once per process.
        private static void SweepOnce()
        {
            if (_sweptThisRun) return;
            _sweptThisRun = true;

            try
            {
                DateTime cutoff = DateTime.Now.Date.AddDays(-RetentionDays);

                foreach (string path in Directory.EnumerateFiles(AppInfo.LogDirectory, $"{FilePrefix}*{FileSuffix}"))
                {
                    string stamp = Path.GetFileNameWithoutExtension(path)[FilePrefix.Length..];

                    // Read from the name, not the file timestamp, which a copy would have reset.
                    if (DateTime.TryParseExact(stamp, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                                               DateTimeStyles.None, out DateTime day)
                        && day < cutoff)
                    {
                        File.Delete(path);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"FileLogService.SweepOnce failed: {ex}");
            }
        }

        // Debug-only: writes a line, reads it back, and checks the retention cutoff.
        [Conditional("DEBUG")]
        public static void SelfCheck()
        {
            Info("SelfCheck", "file logger reachable");

            Debug.Assert(File.Exists(CurrentFile), "a written line must produce today's file");
            Debug.Assert(File.ReadAllText(CurrentFile).Contains("file logger reachable"),
                         "the line just written must be readable back");

            string keep = $"{FilePrefix}{DateTime.Now:yyyy-MM-dd}{FileSuffix}";
            string drop = $"{FilePrefix}{DateTime.Now.AddDays(-(RetentionDays + 1)):yyyy-MM-dd}{FileSuffix}";
            DateTime cutoff = DateTime.Now.Date.AddDays(-RetentionDays);

            Debug.Assert(DateTime.ParseExact(Path.GetFileNameWithoutExtension(keep)[FilePrefix.Length..],
                             "yyyy-MM-dd", CultureInfo.InvariantCulture) >= cutoff,
                         "today's file must survive the sweep");
            Debug.Assert(DateTime.ParseExact(Path.GetFileNameWithoutExtension(drop)[FilePrefix.Length..],
                             "yyyy-MM-dd", CultureInfo.InvariantCulture) < cutoff,
                         "a file past the retention window must be swept");
        }
    }
}
