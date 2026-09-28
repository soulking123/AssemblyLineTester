using AssemblyLineTester.Data;
using AssemblyLineTester.Helpers;
using AssemblyLineTester.Models.Dto;
using AssemblyLineTester.Services;
using AssemblyLineTester.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AssemblyLineTester
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "--init-db")
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                DbInitialize.Initialize();
                return;
            }

            ApplicationConfiguration.Initialize();

            // Installed first, so a startup failure still leaves a trace.
            InstallCrashHandlers();

            FileLogService.SelfCheck();
            AppTime.SelfCheck();
            LicenseService.SelfCheck();
            TestSettingService.SelfCheck();
            MaintenanceService.SelfCheck();
            Helpers.ImageLoader.SelfCheck();
            Views.Pages.TestParametersPage.SelfCheck();
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

            var (licenseResult, _) = LicenseService.Validate();
            if (licenseResult != LicenseValidationResult.Valid)
            {
                FileLogService.Error("Startup", $"Aborted: license check failed ({licenseResult}).");

                // The machine id is printed here because a licence is issued against it, and this
                // dialog is the only place the operator can read it while startup is refused.
                MessageBox.Show(
                    $"{DescribeLicenseFailure(licenseResult)}\r\n\r\n" +
                    $"Machine ID: {AppInfo.FormatMachineId(AppInfo.PrimaryMachineId)}\r\n\r\n" +
                    $"Contact your AssemblyLineTester vendor for a new license file, quoting the machine ID above.",
                    "License Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            new SystemLogService().PurgeOlderThan();

            // Drafts are started-but-never-saved tests; nothing in the UI ever shows them.
            new TestReportService().PurgeOldDrafts();

            Application.ApplicationExit += (s, e) =>
            {
                InstrumentService.Shutdown();
            };

            try
            {
                Application.Run(new LoginForm());
            }
            finally
            {
                InstrumentService.Shutdown();
                FileLogService.Info("Shutdown", "Application closed normally.");
            }
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

        private static string DescribeLicenseFailure(LicenseValidationResult result) => result switch
        {
            LicenseValidationResult.MissingFile => "No license file was found for this installation.",
            LicenseValidationResult.InvalidSignature => "The license file is missing, corrupted, or was not issued for this application.",
            LicenseValidationResult.Expired => "This license has expired.",
            LicenseValidationResult.WrongMachine => "This license was issued for a different machine.",
            LicenseValidationResult.ClockRolledBack => "The system clock appears to have been moved backward, which this application does not allow.",
            LicenseValidationResult.CorruptState => "The application's saved license state could not be verified.",
            _ => "The license could not be validated."
        };

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


