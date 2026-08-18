using System;
using System.Linq;
using AssemblyLineTester.Data;
using AssemblyLineTester.Models;
using AssemblyLineTester.Models.Dto;
using AssemblyLineTester.Helpers;

namespace AssemblyLineTester.Services
{
    public class TestHistoryService
    {
        public ReportSummary GetHistorySummary(ReportFilter filter)
        {
            using var db = new AppDbContext();
            var query = TestReportQueryHelper.ApplyFilter(db, filter);

            int total = query.Count();
            int pass = query.Count(r => r.Status == TestStatus.Completed && r.OverallResult == TestResult.Pass);
            int fail = query.Count(r => r.Status == TestStatus.Completed && r.OverallResult == TestResult.Fail);
            int inProgress = query.Count(r => r.Status == TestStatus.InProgress);
            int stopped = query.Count(r => r.Status == TestStatus.Stopped);

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

        public PagedResult<TestHistoryListItem> GetHistoryList(ReportFilter filter)
        {
            using var db = new AppDbContext();
            var query = TestReportQueryHelper.ApplyFilter(db, filter).OrderByDescending(r => r.TestDateTime);

            int totalCount = query.Count();
            var page = query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();

            var items = page.Select(r => new TestHistoryListItem
            {
                ReportId = r.ReportId,
                TestTime = AppTime.ToLocal(r.TestDateTime),
                ProductModel = r.ProductModelName,
                ProductType = r.ProductType,
                Area = r.Area,
                PartNumber = r.PartNumber,
                SerialNumber = r.SerialNumber,
                Status = r.Status.ToString(),
                Result = r.OverallResult?.ToString().ToUpperInvariant(),
                TotalTime = FormatDuration(r.TestDateTime, r.EndDateTime),
                Operator = r.Operator
            }).ToList();

            return new PagedResult<TestHistoryListItem>
            {
                Items = items,
                TotalCount = totalCount,
                Page = filter.Page,
                PageSize = filter.PageSize
            };
        }

        // Removes a report and everything cascading from it; false if it was not there.
        public bool DeleteHistory(int reportId)
        {
            // Checked here, not only on the page, which Operator can reach by default.
            if (!SessionManager.HasPermission("Action.TestHistory.Delete"))
                throw new UnauthorizedAccessException("You are not allowed to delete test history records.");

            using var db = new AppDbContext();
            var report = db.TestReports.FirstOrDefault(r => r.ReportId == reportId);
            if (report == null) return false;

            // Captured before the row goes, or the log entry cannot name it.
            string reportNumber = report.ReportNumber;

            try
            {
                db.TestReports.Remove(report);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                FileLogService.Error("TestHistoryService.DeleteHistory",
                    $"Could not delete report {reportId}.", ex);
                return false;
            }

            // After the save, so a failed delete never leaves an entry claiming it happened.
            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Warning, "Test History", "Delete History",
                $"Test history {reportNumber} deleted along with its steps.");

            return true;
        }

        public TestHistoryDetailDto? GetHistoryDetail(int reportId)
        {
            using var db = new AppDbContext();
            var report = db.TestReports.FirstOrDefault(r => r.ReportId == reportId);
            if (report is null) return null;

            var steps = db.TestSteps
                .Where(s => s.ReportId == reportId)
                .OrderBy(s => s.StepNo)
                .ToList();

            var stepDtos = steps.Select(s => new TestStepResultDto
            {
                StepNo = s.StepNo,
                TestType = s.TestTypeName,
                Result = s.Result ? "PASS" : "FAIL",
                Time = TimeSpan.FromSeconds(s.DurationSeconds).ToString(@"hh\:mm\:ss")
            }).ToList();

            return new TestHistoryDetailDto
            {
                ReportNumber = report.ReportNumber,
                StartTime = AppTime.ToLocal(report.TestDateTime),
                EndTime = AppTime.ToLocal(report.EndDateTime),
                TotalTime = FormatDuration(report.TestDateTime, report.EndDateTime),
                ProductModel = report.ProductModelName,
                ProductType = report.ProductType,
                Area = report.Area,
                PartNumber = report.PartNumber,
                SerialNumber = report.SerialNumber,
                ECNNumber = report.ECNNumber,
                Operator = report.Operator,
                Status = report.Status.ToString(),
                OverallResult = report.OverallResult?.ToString().ToUpperInvariant(),
                Steps = stepDtos
            };
        }

        private static string FormatDuration(DateTime start, DateTime? end)
        {
            if (!end.HasValue) return "-";
            return (end.Value - start).ToString(@"hh\:mm\:ss");
        }
    }
}