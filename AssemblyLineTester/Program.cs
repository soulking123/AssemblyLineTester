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
            if (args.Length > 0 && args[0] == "--export-dummy-pdf")
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                DbInitialize.Initialize();
                GenerateDummyReports(args.Length > 1 ? args[1] : null);
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

        private static void GenerateDummyReports(string? targetDir)
        {
            var outDir = targetDir ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GeneratedReports");
            Directory.CreateDirectory(outDir);

            var service = new ReportPdfExportService();

            // 1. Dummy report with custom Product Type, Area, and ECN No.
            var detailWithData = new ReportDetailDto
            {
                ReportNumber = "TR-20260926-0001",
                TestDateTime = DateTime.Now,
                ProductModel = "CAT-340D-HARNESS",
                ProductType = "Engine Control Wire Harness",
                Area = "Assembly Line 2 - Station B",
                ECNNumber = "ECN-2026-0819",
                Revision = "2.1",
                PartNumber = "PN-8921-344A",
                SerialNumber = "SN-20260926-0842",
                Operator = "John Doe (Operator)",
                TestTime = "00:02:45",
                OverallResult = "PASS",
                Notes = "Dummy test execution report verifying new Product Type, Area, and ECN No. fields on the cover page.",
                TotalNets = 3,
                PassCount = 3,
                FailCount = 0,
                TypeSummaries = new List<TestTypeSummaryDto>
                {
                    new TestTypeSummaryDto { TestType = "WIRE", Total = 2, Pass = 2, Fail = 0, PassRatePercent = 100 },
                    new TestTypeSummaryDto { TestType = "RESISTOR", Total = 1, Pass = 1, Fail = 0, PassRatePercent = 100 }
                },
                Details = new List<TestDetailDto>
                {
                    new TestDetailDto
                    {
                        NetId = "NET-001",
                        HarnessGroup = "HG-POWER",
                        Color = "RED",
                        Gauge = "18 AWG",
                        FromPin = "J1-1",
                        ToPin = "J2-1",
                        FromLabel = "PWR_IN",
                        ToLabel = "PWR_OUT",
                        FromConnector = "J1",
                        ToConnector = "J2",
                        TestType = "WIRE",
                        LowerLimit = 0.0,
                        UpperLimit = 1.5,
                        ExpectedValue = 0.5,
                        ToleranceValue = 1.0,
                        MeasuredValue = 0.42,
                        Measured = "0.42 Ω",
                        Result = "PASS"
                    },
                    new TestDetailDto
                    {
                        NetId = "NET-002",
                        HarnessGroup = "HG-SIGNAL",
                        Color = "BLU",
                        Gauge = "22 AWG",
                        FromPin = "J1-2",
                        ToPin = "J2-2",
                        FromLabel = "SIG_CAN_H",
                        ToLabel = "SIG_CAN_H",
                        FromConnector = "J1",
                        ToConnector = "J2",
                        TestType = "WIRE",
                        LowerLimit = 0.0,
                        UpperLimit = 1.5,
                        ExpectedValue = 0.5,
                        ToleranceValue = 1.0,
                        MeasuredValue = 0.38,
                        Measured = "0.38 Ω",
                        Result = "PASS"
                    },
                    new TestDetailDto
                    {
                        NetId = "NET-003",
                        HarnessGroup = "HG-SIGNAL",
                        Color = "YEL",
                        Gauge = "22 AWG",
                        FromPin = "J1-3",
                        ToPin = "J1-4",
                        FromLabel = "TERM_R+",
                        ToLabel = "TERM_R-",
                        FromConnector = "J1",
                        ToConnector = "J1",
                        TestType = "RESISTOR",
                        LowerLimit = 110.0,
                        UpperLimit = 130.0,
                        ExpectedValue = 120.0,
                        ToleranceValue = 5.0,
                        MeasuredValue = 119.8,
                        Measured = "119.8 Ω",
                        Result = "PASS"
                    }
                }
            };

            string fileWithData = Path.Combine(outDir, "Dummy_Test_Report_With_Data.pdf");
            service.ExportReportToPdf(detailWithData, fileWithData, logToDatabase: false);
            Console.WriteLine($"Exported dummy report with data to: {fileWithData}");

            // 2. Dummy report with empty fields demonstrating "N/A" fallback
            var detailWithNA = new ReportDetailDto
            {
                ReportNumber = "TR-20260926-0002",
                TestDateTime = DateTime.Now,
                ProductModel = "CAT-340D-HARNESS",
                ProductType = "",
                Area = "-",
                ECNNumber = "",
                Revision = "1.0",
                PartNumber = "PN-8921-344A",
                SerialNumber = "SN-20260926-0843",
                Operator = "Jane Smith (Operator)",
                TestTime = "00:01:15",
                OverallResult = "PASS",
                Notes = "Dummy test report verifying fallback to N/A when Product Type, Area, or ECN No. are left blank.",
                TotalNets = 1,
                PassCount = 1,
                FailCount = 0,
                TypeSummaries = new List<TestTypeSummaryDto>
                {
                    new TestTypeSummaryDto { TestType = "WIRE", Total = 1, Pass = 1, Fail = 0, PassRatePercent = 100 }
                },
                Details = new List<TestDetailDto>
                {
                    new TestDetailDto
                    {
                        NetId = "NET-001",
                        HarnessGroup = "HG-GROUND",
                        Color = "BLK",
                        Gauge = "18 AWG",
                        FromPin = "J1-5",
                        ToPin = "J2-5",
                        FromLabel = "GND",
                        ToLabel = "GND",
                        FromConnector = "J1",
                        ToConnector = "J2",
                        TestType = "WIRE",
                        LowerLimit = 0.0,
                        UpperLimit = 1.5,
                        ExpectedValue = 0.5,
                        ToleranceValue = 1.0,
                        MeasuredValue = 0.35,
                        Measured = "0.35 Ω",
                        Result = "PASS"
                    }
                }
            };

            string fileWithNA = Path.Combine(outDir, "Dummy_Test_Report_NA.pdf");
            service.ExportReportToPdf(detailWithNA, fileWithNA, logToDatabase: false);
            Console.WriteLine($"Exported dummy report with N/A fallback to: {fileWithNA}");
        }
    }
}
