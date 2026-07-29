using AssemblyLineTester.Data;
using AssemblyLineTester.Helpers;
using AssemblyLineTester.Services;
using AssemblyLineTester.Views;
using System.Text;


namespace AssemblyLineTester
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Installed first, so a startup failure still leaves a trace.
            InstallCrashHandlers();

            FileLogService.SelfCheck();
            AppTime.SelfCheck();
            FileLogService.Info("Startup", AppInfo.Describe());

            // PdfSharp needs code page 1252, which .NET does not register by default.
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // Before migrations run: a data-rewriting migration has no undo.
            DatabaseBackupService.RunAutomaticBackup();

            // A half-built schema would corrupt test results in ways that surface weeks later.
            if (!DbInitialize.Initialize())
            {
                FileLogService.Error("Startup", "Aborted: the database could not be initialised.");
                return;
            }

            new SystemLogService().PurgeOlderThan();

            // Drafts are started-but-never-saved tests; nothing in the UI ever shows them.
            new TestReportService().PurgeOldDrafts();

            Application.Run(new LoginForm());

            FileLogService.Info("Shutdown", "Application closed normally.");
        }

        // Both handlers are needed: ThreadException survives, UnhandledException only records.
        private static void InstallCrashHandlers()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            Application.ThreadException += (s, e) =>
            {
                Report("UI thread", e.Exception);
                ShowCrashDialog(e.Exception);
            };

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                if (e.ExceptionObject is Exception ex)
                {
                    Report("Background thread", ex);
                }
                else
                {
                    FileLogService.Error("Background thread", $"Non-exception fault: {e.ExceptionObject}");
                }
            };
        }

        private static void Report(string origin, Exception ex)
        {
            // Disk log first: writing to the database can itself be what failed.
            FileLogService.Error(origin, "Unhandled exception", ex);

            try
            {
                new SystemLogService().Log(
                    Models.SystemLog.LogSeverity.Error,
                    "Application",
                    "Unhandled Exception",
                    $"{ex.GetType().Name}: {ex.Message}");
            }
            catch (Exception logFailure)
            {
                FileLogService.Error("Report", "Could not write the crash to the database log.", logFailure);
            }
        }

        private static void ShowCrashDialog(Exception ex)
        {
            try
            {
                MessageBox.Show(
                    $"Something went wrong and the action was stopped.\r\n\r\n{ex.Message}\r\n\r\n" +
                    $"The details have been written to the log. If this keeps happening, send a diagnostics " +
                    $"bundle from Settings.\r\n\r\nVersion {AppInfo.Version}",
                    "Unexpected error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception dialogFailure)
            {
                FileLogService.Error("ShowCrashDialog", "Could not show the error dialog.", dialogFailure);
            }
        }
    }
}
