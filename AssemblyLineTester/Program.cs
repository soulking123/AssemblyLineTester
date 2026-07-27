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
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Installed before anything else can throw, so a failure during startup still leaves a
            // trace instead of a bare Windows crash dialog on the factory floor.
            InstallCrashHandlers();

            FileLogService.SelfCheck();
            FileLogService.Info("Startup", AppInfo.Describe());

            // Register code pages provider so PdfSharp and other libraries can use code page encodings (e.g., 1252)
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            //initalize the database
            DbInitialize.Initialize();

            // Once per run is enough, and startup is the only moment nothing else is mid-write.
            new SystemLogService().PurgeOlderThan();

            Application.Run(new LoginForm());

            FileLogService.Info("Shutdown", "Application closed normally.");
        }

        /// <summary>
        /// Routes every exception that nothing else caught into the log before the user sees it.
        /// </summary>
        /// <remarks>
        /// Two handlers are needed and they are not interchangeable: <c>ThreadException</c> catches
        /// what escapes a WinForms message-loop handler and the application can carry on, while
        /// <c>UnhandledException</c> catches everything else - background threads included - and by
        /// then the process is already going down, so it can only record.
        /// </remarks>
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
            // The file gets the stack trace; the System Log page gets a one-line summary a
            // supervisor can read. Writing to the database can itself fail here - the disk log is
            // the one that has to survive, so it goes first.
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
