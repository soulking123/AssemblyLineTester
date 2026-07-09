using System;
using System.Collections.Generic;
using System.Linq;
using AssemblyLineTester.Data;
using AssemblyLineTester.Models;
using AssemblyLineTester.Models.Dto;

namespace AssemblyLineTester.Services
{
    public class DashboardService
    {
        // Converts the KPI time-range selector into a ReportFilter so we reuse the
        // exact same filtering logic already used by Reports/Test History pages.
        private static ReportFilter BuildRangeFilter(DashboardTimeRange range)
        {
            DateTime from = range switch
            {
                DashboardTimeRange.Today => DateTime.Today,
                DashboardTimeRange.Last7Days => DateTime.Today.AddDays(-6),
                DashboardTimeRange.Last30Days => DateTime.Today.AddDays(-29),
                DashboardTimeRange.Last3Months => DateTime.Today.AddMonths(-3),
                DashboardTimeRange.Last6Months => DateTime.Today.AddMonths(-6),
                DashboardTimeRange.Last1Year => DateTime.Today.AddYears(-1),
                _ => DateTime.Today
            };

            return new ReportFilter { DateFrom = from, DateTo = DateTime.Today };
        }

        // Longer ranges get coarser buckets so the bar chart stays readable
        // (365 daily bars for a 1-year view would be unreadable).
        private static TrendGranularity GetGranularityForRange(DashboardTimeRange range) => range switch
        {
            DashboardTimeRange.Today => TrendGranularity.Daily,
            DashboardTimeRange.Last7Days => TrendGranularity.Daily,
            DashboardTimeRange.Last30Days => TrendGranularity.Daily,
            DashboardTimeRange.Last3Months => TrendGranularity.Weekly,
            DashboardTimeRange.Last6Months => TrendGranularity.Weekly,
            DashboardTimeRange.Last1Year => TrendGranularity.Monthly,
            _ => TrendGranularity.Daily
        };

        private static DateTime BucketKey(DateTime date, TrendGranularity granularity)
        {
            switch (granularity)
            {
                case TrendGranularity.Weekly:
                    // Bucket by the Monday of that week.
                    int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
                    return date.Date.AddDays(-diff);

                case TrendGranularity.Monthly:
                    return new DateTime(date.Year, date.Month, 1);

                default:
                    return date.Date;
            }
        }

        // "HISTORY" bar chart: Pass/Fail/Total grouped adaptively (day/week/month) based on the range.
        public DashboardTrendResult GetHistoryTrend(DashboardTimeRange range)
        {
            using var db = new AppDbContext();
            var filter = BuildRangeFilter(range);
            var granularity = GetGranularityForRange(range);
            var reports = TestReportQueryHelper.ApplyFilter(db, filter).ToList();

            var points = reports
                .GroupBy(r => BucketKey(r.TestDateTime, granularity))
                .OrderBy(g => g.Key)
                .Select(g => new ReportTrendPoint
                {
                    Date = g.Key,
                    PassCount = g.Count(r => r.Status == TestStatus.Completed && r.OverallResult == TestResult.Pass),
                    FailCount = g.Count(r => r.Status == TestStatus.Completed && r.OverallResult == TestResult.Fail),
                    TotalCount = g.Count()
                })
                .ToList();

            return new DashboardTrendResult { Granularity = granularity, Points = points };
        }

        // "SUMMARY" KPI cards: Total Tested, Pass, Fail, Yield %, Avg Cycle Time.
        public DashboardKpiDto GetKpiSummary(DashboardTimeRange range)
        {
            using var db = new AppDbContext();
            var filter = BuildRangeFilter(range);
            var reports = TestReportQueryHelper.ApplyFilter(db, filter).ToList();

            int total = reports.Count;
            int pass = reports.Count(r => r.Status == TestStatus.Completed && r.OverallResult == TestResult.Pass);
            int fail = reports.Count(r => r.Status == TestStatus.Completed && r.OverallResult == TestResult.Fail);

            var completedDurations = reports
                .Where(r => r.Status == TestStatus.Completed && r.EndDateTime.HasValue)
                .Select(r => (r.EndDateTime!.Value - r.TestDateTime).TotalSeconds)
                .ToList();

            double avgCycleSeconds = completedDurations.Count == 0 ? 0 : completedDurations.Average();

            return new DashboardKpiDto
            {
                TotalTested = total,
                PassCount = pass,
                FailCount = fail,
                YieldPercent = total == 0 ? 0 : Math.Round(pass * 100.0 / total, 2),
                AvgCycleTimeSeconds = avgCycleSeconds,
                AvgCycleTimeFormatted = TimeSpan.FromSeconds(avgCycleSeconds).ToString(@"hh\:mm\:ss")
            };
        }


        // "FAIL BY REASON" doughnut: failed net counts grouped by test type, within the range.
        public List<FailReasonSliceDto> GetFailReasonBreakdown(DashboardTimeRange range)
        {
            using var db = new AppDbContext();
            var filter = BuildRangeFilter(range);
            var reportIds = TestReportQueryHelper.ApplyFilter(db, filter).Select(r => r.ReportId).ToList();

            var failedDetails = db.TestDetails
                .Where(d => reportIds.Contains(d.ReportId) && !d.Result)
                .ToList();

            int totalFail = failedDetails.Count;
            if (totalFail == 0) return new List<FailReasonSliceDto>();

            return failedDetails
                .GroupBy(d => d.TestTypeName)
                .OrderByDescending(g => g.Count())
                .Select(g => new FailReasonSliceDto
                {
                    TestTypeName = g.Key,
                    FailCount = g.Count(),
                    PercentOfTotalFail = Math.Round(g.Count() * 100.0 / totalFail, 1)
                })
                .ToList();
        }

        // "RECENT TEST HISTORY" grid: latest N reports regardless of range selector.
        public List<RecentTestItemDto> GetRecentTests(int count = 8)
        {
            using var db = new AppDbContext();
            return db.TestReports
                .OrderByDescending(r => r.TestDateTime)
                .Take(count)
                .Select(r => new RecentTestItemDto
                {
                    ReportId = r.ReportId,
                    TestTime = r.TestDateTime,
                    PartNumber = r.PartNumber,
                    SerialNumber = r.SerialNumber,
                    Result = r.Status == TestStatus.Completed
                        ? r.OverallResult == TestResult.Pass ? "PASS" : "FAIL"
                        : null,
                    Operator = r.Operator
                })
                .ToList();
        }

        // "ACTIVE PROGRAM" card: prioritizes a report currently InProgress, else the most recent one.
        public ActiveProgramDto? GetActiveProgram()
        {
            using var db = new AppDbContext();

            var running = db.TestReports
                .Where(r => r.Status == TestStatus.InProgress)
                .OrderByDescending(r => r.TestDateTime)
                .FirstOrDefault();

            var report = running ?? db.TestReports
                .OrderByDescending(r => r.TestDateTime)
                .FirstOrDefault();

            if (report is null) return null;

            return new ActiveProgramDto
            {
                PartNumber = report.PartNumber,
                ProductModel = report.ProductModel,
                StatusLabel = report.Status switch
                {
                    TestStatus.InProgress => "Running",
                    TestStatus.Stopped => "Stopped",
                    _ => "Idle"
                },
                LastRunAt = report.TestDateTime
            };
        }
    }
}