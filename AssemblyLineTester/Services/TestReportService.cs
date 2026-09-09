using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AssemblyLineTester.Data;
using AssemblyLineTester.Models;
using AssemblyLineTester.Models.Dto;
using AssemblyLineTester.Helpers;

namespace AssemblyLineTester.Services
{
    public class TestReportService
    {
        // Matches the instrument's test type name against the TestTypes table, WIRE if unknown.
        private static string ResolveTestTypeName(IReadOnlyCollection<string> knownTypeNames, string? rawTestType)
        {
            if (string.IsNullOrWhiteSpace(rawTestType)) return "WIRE";

            string trimmed = rawTestType.Trim();
            return knownTypeNames.FirstOrDefault(name => string.Equals(name, trimmed, StringComparison.OrdinalIgnoreCase))
                   ?? "WIRE";
        }

        public ReportSummary GetSummary(ReportFilter filter)
        {
            using var db = new AppDbContext();
            // Drafts stay off the Reports page until the operator confirms them.
            var reports = TestReportQueryHelper.ApplyFilter(db, filter).Where(r => !r.IsDraft);

            int total = reports.Count();
            int pass = reports.Count(r => r.Status == TestStatus.Completed && r.OverallResult == TestResult.Pass);
            int fail = reports.Count(r => r.Status == TestStatus.Completed && r.OverallResult == TestResult.Fail);
            int inProgress = reports.Count(r => r.Status == TestStatus.InProgress);
            int stopped = reports.Count(r => r.Status == TestStatus.Stopped);

            return new ReportSummary
            {
                TotalTested = total,
                PassCount = pass,
                FailCount = fail,
                InProgressCount = inProgress,
                StopCount = stopped,
                PassRatePercent = total == 0 ? 0 : Math.Round(pass * 100.0 / total, 2)
            };
        }

        // Deletes a report; details, steps and summaries cascade with it.
        public void DeleteReport(int reportId)
        {
            RequirePermission("Action.Reports.Edit",
                "You are not allowed to delete saved test reports.");

            using var db = new AppDbContext();
            var report = db.TestReports.FirstOrDefault(r => r.ReportId == reportId);
            if (report is null) return;

            // Captured before the row goes, or the log entry cannot name it.
            string reportNumber = report.ReportNumber;

            db.TestReports.Remove(report);
            db.SaveChanges();

            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Warning, "Test Report", "Delete Report",
                $"Report {reportNumber} deleted.");
        }


        // Daily Pass/Fail/Total for the Reports page chart.
        public List<ReportTrendPoint> GetResultTrend(ReportFilter filter)
        {
            using var db = new AppDbContext();
            return DashboardService.BuildTrendPoints(
                TestReportQueryHelper.ApplyFilter(db, filter).Where(r => !r.IsDraft),
                TrendGranularity.Daily);
        }

        public PagedResult<ReportListItem> GetReportList(ReportFilter filter)
        {
            using var db = new AppDbContext();
            var query = TestReportQueryHelper.ApplyFilter(db, filter).Where(r => !r.IsDraft).OrderByDescending(r => r.TestDateTime);

            int totalCount = query.Count();
            var page = query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();

            var items = page.Select(r => new ReportListItem
            {
                ReportId = r.ReportId,
                ReportName = $"Test Report_{AppTime.ToLocal(r.TestDateTime):yyyyMMdd_HHmmss}",
                DateGenerated = r.CreatedAt,
                GeneratedBy = r.Operator,
                OverallResult = r.OverallResult?.ToString().ToUpperInvariant() ?? "-"
            }).ToList();

            return new PagedResult<ReportListItem>
            {
                Items = items,
                TotalCount = totalCount,
                Page = filter.Page,
                PageSize = filter.PageSize
            };
        }

        public ReportDetailDto? GetReportDetail(int reportId)
        {
            using var db = new AppDbContext();
            var report = db.TestReports.FirstOrDefault(r => r.ReportId == reportId);
            if (report is null) return null;

            var details = db.TestDetails.Where(d => d.ReportId == reportId).ToList();

            var typeSummaries = db.ReportTestTypeSummaries
                .Where(s => s.ReportId == reportId)
                .OrderByDescending(s => s.TotalCount)
                .Take(4)
                .Select(s => new TestTypeSummaryDto
                {
                    TestType = s.TestTypeName,
                    Total = s.TotalCount,
                    Pass = s.PassCount,
                    Fail = s.FailCount,
                    PassRatePercent = s.TotalCount == 0 ? 100 : Math.Round(s.PassCount * 100.0 / s.TotalCount, 2)
                })
                .ToList();

            var detailDtos = details.Select(d => new TestDetailDto
            {
                NetId = d.NetId,
                HarnessGroup = d.HarnessGroup,
                FromPin = d.FromPin,
                ToPin = d.ToPin,
                FromLabel = d.FromLabel,
                ToLabel = d.ToLabel,
                TestType = d.TestTypeName,
                LowerLimit = d.LowerLimit,
                UpperLimit = d.UpperLimit,
                ExpectedValue = d.ExpectedValue,
                ToleranceValue = d.Tolerance,
                Unit = d.Unit,
                Measured = d.MeasuredValue.HasValue ? $"{d.MeasuredValue.Value} {d.Unit}" : "N/A",
                Result = FormatResult(d.Result, d.MeasuredValue),
                ErrorDetail = d.ErrorDetail
            }).ToList();


            TimeSpan? duration = report.EndDateTime.HasValue ? report.EndDateTime.Value - report.TestDateTime : null;

            return new ReportDetailDto
            {
                ReportNumber = report.ReportNumber,
                TestDateTime = AppTime.ToLocal(report.TestDateTime),
                ProductModel = report.ProductModelName,
                ProductType = report.ProductType,
                Area = report.Area,
                ECNNumber = report.ECNNumber,
                Revision = report.Revision,
                PartNumber = report.PartNumber,
                SerialNumber = report.SerialNumber,
                Operator = report.Operator,
                TestTime = duration.HasValue ? duration.Value.ToString(@"hh\:mm\:ss") : "-",
                OverallResult = report.OverallResult?.ToString().ToUpperInvariant() ?? "-",
                Notes = report.Notes,
                ErrorSummary = report.ErrorSummary,
                TotalNets = report.TotalNets,
                PassCount = report.PassCount,
                FailCount = report.FailCount,
                TypeSummaries = typeSummaries,
                Details = detailDtos
            };
        }

        public int SaveLiveTestReport(string productModel, string productType, string area, string ecnNumber, string revision, string partNumber, string serialNumber, string operatorName, DateTime testDateTime, List<LiveTestDetailRowDto> rows, string overallResultStr, bool isDraft = false, string? notes = null, string? errorSummary = null, DateTime? endDateTime = null)
        {
            using var db = new AppDbContext();

            bool isPass = string.Equals(overallResultStr, "PASS", StringComparison.OrdinalIgnoreCase);
            int passCount = rows != null ? rows.Count(r => r.Status == LiveTestStatus.Pass) : 0;
            int totalCount = rows != null ? rows.Count : 0;
            int failCount = totalCount - passCount;

            DateTime startUtc = testDateTime == default ? AppTime.UtcNow : (testDateTime.Kind == DateTimeKind.Utc ? testDateTime : AppTime.ToUtc(testDateTime));
            DateTime endUtc = endDateTime.HasValue ? (endDateTime.Value.Kind == DateTimeKind.Utc ? endDateTime.Value : AppTime.ToUtc(endDateTime.Value)) : AppTime.UtcNow;
            if (endUtc < startUtc)
            {
                endUtc = startUtc;
            }

            var report = new TestReport
            {
                ReportNumber = NextReportNumber(db),
                ProductModelName = productModel ?? "",
                ProductType = productType ?? "",
                Area = area ?? "",
                ECNNumber = ecnNumber ?? "",
                Revision = string.IsNullOrEmpty(revision) ? "1.0" : revision,
                PartNumber = partNumber ?? "",
                SerialNumber = string.IsNullOrEmpty(serialNumber) ? $"SN-{DateTime.Now:HHmmss}" : serialNumber,
                Operator = string.IsNullOrEmpty(operatorName) ? "Unknown" : operatorName,
                TestDateTime = startUtc,
                EndDateTime = endUtc,
                TotalNets = totalCount,
                PassCount = passCount,
                FailCount = failCount,
                Status = TestStatus.Completed,
                OverallResult = isPass ? TestResult.Pass : TestResult.Fail,
                Notes = notes?.Trim() ?? "",
                ErrorSummary = errorSummary?.Trim(),
                IsDraft = isDraft,
                CreatedAt = DateTime.UtcNow
            };

            db.TestReports.Add(report);
            db.SaveChanges();

            if (rows != null && rows.Count > 0)
            {
                var details = new List<TestDetail>();
                var steps = new List<TestStep>();

                var knownTypeNames = db.TestTypes.Select(t => t.TestTypeName).ToList();

                int stepCount = 1;
                foreach (var r in rows)
                {
                    bool rowPass = r.Status == LiveTestStatus.Pass;
                    string rowTypeName = ResolveTestTypeName(knownTypeNames, r.TestType);

                    details.Add(new TestDetail
                    {
                        ReportId = report.ReportId,
                        NetId = r.NetId ?? $"N{stepCount:000}",
                        // Empty for an instrument-driven run, which knows no harness group.
                        HarnessGroup = r.HarnessGroup,
                        FromPin = string.IsNullOrEmpty(r.FromPoint) ? "-" : r.FromPoint,
                        ToPin = string.IsNullOrEmpty(r.ToPoint) ? "-" : r.ToPoint,
                        FromLabel = r.FromLabel,
                        ToLabel = r.ToLabel,
                        TestTypeName = rowTypeName,
                        MeasuredValue = r.MeasuredValue,
                        LowerLimit = r.LowerLimit,
                        UpperLimit = r.UpperLimit,
                        ExpectedValue = double.TryParse(r.ExpectedValue, NumberStyles.Any, CultureInfo.InvariantCulture, out double expected) ? expected : null,
                        Tolerance = r.Tolerance,
                        Unit = string.IsNullOrWhiteSpace(r.Unit) ? "Ω" : r.Unit,
                        Result = rowPass,
                        ErrorDetail = rowPass ? null : "Measurement out of limits"
                    });

                    steps.Add(new TestStep
                    {
                        ReportId = report.ReportId,
                        StepNo = stepCount++,
                        TestTypeName = rowTypeName,
                        Result = rowPass,
                        // Nothing measures per-step duration yet.
                        DurationSeconds = 0
                    });
                }

                db.TestDetails.AddRange(details);
                db.TestSteps.AddRange(steps);
                db.SaveChanges();
                SaveReportTestTypeSummaries(db, report.ReportId, details);
                db.SaveChanges();
            }

            // Drafts are not logged; the entry that matters is ConfirmDraft.
            if (!isDraft)
            {
                new SystemLogService().Log(
                    Models.SystemLog.LogSeverity.Info, "Test Report", "Save Report",
                    $"Report {report.ReportNumber} saved for part {partNumber}: {overallResultStr}, {totalCount} nets.");
            }

            return report.ReportId;
        }

        // The timestamp only resolves to a second, so same-second saves get a -2, -3 suffix.
        // ponytail: one process only; two app instances could still race the unique index.
        private static string NextReportNumber(AppDbContext db)
        {
            string basis = $"TR-{DateTime.Now:yyyyMMdd-HHmmss}";
            string number = basis;

            for (int i = 2; db.TestReports.Any(r => r.ReportNumber == number); i++)
            {
                number = $"{basis}-{i}";
            }

            return number;
        }

        private static string FormatResult(bool result, double? measured)
        {
            if (result) return "PASS";
            return measured.HasValue ? "FAIL" : "OPEN";
        }

        public int SaveDraftLiveTestReport(
            string productModel, string productType, string area, string ecnNumber, string revision,
            string partNumber, DateTime testDateTime, List<LiveTestDetailRowDto> rows, string overallResultStr,
            string? errorSummary = null, DateTime? endDateTime = null)
        {
            string operatorName = SessionManager.CurrentUser?.FullName ?? "Unknown";
            return SaveLiveTestReport(
                productModel, productType, area, ecnNumber, revision, partNumber,
                serialNumber: $"PENDING-{testDateTime:HHmmss}",
                operatorName: operatorName,
                testDateTime, rows, overallResultStr, isDraft: true, errorSummary: errorSummary, endDateTime: endDateTime);
        }

        // Removes drafts older than the given age; confirmed reports are never touched.
        public int PurgeOldDrafts(int olderThanDays = 30)
        {
            using var db = new AppDbContext();
            var cutoff = DateTime.UtcNow.AddDays(-olderThanDays);

            var oldDrafts = db.TestReports
                .Where(r => r.IsDraft && r.CreatedAt < cutoff)
                .ToList();

            if (oldDrafts.Count == 0) return 0;

            db.TestReports.RemoveRange(oldDrafts);
            db.SaveChanges();

            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Info, "Test Report", "Purge Drafts",
                $"{oldDrafts.Count} draft reports older than {olderThanDays} days removed.");

            return oldDrafts.Count;
        }

        // Last step of running a test, so deliberately not permission-gated.
        public void ConfirmDraft(int reportId, string serialNumber, string? notes = null, string? errorSummary = null)
        {
            var report = SetSerialNumber(reportId, serialNumber, markConfirmed: true, notes, errorSummary);

            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Info, "Test Report", "Confirm Report",
                $"Report {report.ReportNumber} saved with serial {report.SerialNumber}.");
        }

        // Rewrites the serial on an already-confirmed report, so it is gated and logged Warning.
        public void AmendSerialNumber(int reportId, string serialNumber)
        {
            RequirePermission("Action.Reports.Edit",
                "You are not allowed to amend saved test reports.");

            var report = SetSerialNumber(reportId, serialNumber, markConfirmed: false);

            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Warning, "Test Report", "Amend Serial Number",
                $"Report {report.ReportNumber} serial amended to {report.SerialNumber}.");
        }

        private static TestReport SetSerialNumber(int reportId, string serialNumber, bool markConfirmed, string? notes = null, string? errorSummary = null)
        {
            using var db = new AppDbContext();
            var report = db.TestReports.FirstOrDefault(r => r.ReportId == reportId)
                ?? throw new InvalidOperationException($"Report {reportId} not found.");

            if (!string.IsNullOrWhiteSpace(serialNumber)) report.SerialNumber = serialNumber.Trim();
            if (notes is not null) report.Notes = notes.Trim();
            if (!string.IsNullOrWhiteSpace(errorSummary)) report.ErrorSummary = errorSummary.Trim();
            if (markConfirmed) report.IsDraft = false;

            db.SaveChanges();
            return report;
        }

        // The gate that actually holds; hiding the button in the UI is only a courtesy.
        private static void RequirePermission(string permissionKey, string message)
        {
            if (!SessionManager.HasPermission(permissionKey))
                throw new UnauthorizedAccessException(message);
        }

        // Pre-aggregates per-test-type counts into ReportTestTypeSummary.
        private static void SaveReportTestTypeSummaries(AppDbContext db, int reportId, List<TestDetail> details)
        {
            var summaries = details
                .GroupBy(d => d.TestTypeName)
                .Select(g => new ReportTestTypeSummary
                {
                    ReportId = reportId,
                    TestTypeName = g.Key,
                    TotalCount = g.Count(),
                    PassCount = g.Count(d => d.Result),
                    FailCount = g.Count(d => !d.Result)
                });

            db.ReportTestTypeSummaries.AddRange(summaries);
        }
    }
}