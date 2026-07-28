using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using AssemblyLineTester.Helpers;

namespace AssemblyLineTester.Services
{
    /// <summary>
    /// Technical log written to disk, one file per day under <see cref="AppInfo.LogDirectory"/>.
    /// </summary>
    /// <remarks>
    /// This is the engineer's log, not the customer's. Stack traces and startup detail go here;
    /// business events that a supervisor would ask about go to <see cref="SystemLogService"/> and
    /// show up on the System Log page. Errors go to both - a summary there, the detail here.
    /// <para>
    /// It is a file rather than a table on purpose: when the database is what broke, a log kept
    /// inside it is gone exactly when it is needed.
    /// </para>
    /// </remarks>
    public static class FileLogService
    {
        /// <summary>How long daily files are kept. Old ones are swept once per run.</summary>
        public const int RetentionDays = 30;

        private const string FilePrefix = "asl-";
        private const string FileSuffix = ".log";

        private static readonly object Gate = new();
        private static bool _sweptThisRun;

        public static void Info(string context, string message) => Write("INFO ", context, message, null);

        public static void Warn(string context, string message) => Write("WARN ", context, message, null);

        public static void Error(string context, string message, Exception? ex = null) => Write("ERROR", context, message, ex);

        /// <summary>Full path of today's log file.</summary>
        public static string CurrentFile =>
            Path.Combine(AppInfo.LogDirectory, $"{FilePrefix}{DateTime.Now:yyyy-MM-dd}{FileSuffix}");

        /// <summary>Daily log files still on disk, newest first. Used to build a diagnostics bundle.</summary>
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
            // Logging must never be the reason the application falls over, so every failure in
            // here is swallowed - but into the debugger, not into nothing.
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

        /// <summary>
        /// Deletes files past the retention window. Runs at most once per process: a factory PC
        /// stays open all shift, so doing it per-line would be pure waste.
        /// </summary>
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

                    // Parse the date out of the name rather than trusting the file timestamp, which
                    // a copy or a restore would have reset.
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

        /// <summary>
        /// Writes a line, reads it back, and checks the retention cutoff is computed the way the
        /// name-based sweep expects. Debug builds only.
        /// </summary>
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
