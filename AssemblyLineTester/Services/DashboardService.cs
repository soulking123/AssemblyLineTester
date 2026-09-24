using System;
using System.Collections.Generic;
using System.Linq;
using AssemblyLineTester.Data;
using AssemblyLineTester.Models;
using AssemblyLineTester.Models.Dto;
using AssemblyLineTester.Helpers;

namespace AssemblyLineTester.Services
{
    public class DashboardService
    {
        // Reuses the same ReportFilter the Reports and Test History pages already filter with.
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

        // Coarser buckets on longer ranges, or a one-year view would draw 365 bars.
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
                    int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
                    return date.Date.AddDays(-diff);

                case TrendGranularity.Monthly:
                    return new DateTime(date.Year, date.Month, 1);

                default:
                    return date.Date;
            }
        }

        // Shared with TestReportService.GetResultTrend, which is this same grouping at Daily.
        internal static List<ReportTrendPoint> BuildTrendPoints(IQueryable<TestReport> reports, TrendGranularity granularity)
        {
            return reports
                .Select(r => new { r.TestDateTime, r.Status, r.OverallResult })
                .AsEnumerable()
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
        }

        // Pass/Fail/Total per bucket for the HISTORY bar chart.
        public DashboardTrendResult GetHistoryTrend(DashboardTimeRange range)
        {
            using var db = new AppDbContext();
            var granularity = GetGranularityForRange(range);
            var points = BuildTrendPoints(
                TestReportQueryHelper.ApplyFilter(db, BuildRangeFilter(range)).Where(r => !r.IsDraft),
                granularity);

            return new DashboardTrendResult { Granularity = granularity, Points = points };
        }

        // Totals behind the SUMMARY KPI cards.
        public DashboardKpiDto GetKpiSummary(DashboardTimeRange range)
        {
            using var db = new AppDbContext();
            var filter = BuildRangeFilter(range);
            var reports = TestReportQueryHelper.ApplyFilter(db, filter).Where(r => !r.IsDraft);

            int total = reports.Count();
            int pass = reports.Count(r => r.Status == TestStatus.Completed && r.OverallResult == TestResult.Pass);
            int fail = reports.Count(r => r.Status == TestStatus.Completed && r.OverallResult == TestResult.Fail);

            var completedDurations = reports
                .Where(r => r.Status == TestStatus.Completed && r.EndDateTime.HasValue)
                .Select(r => new { r.TestDateTime, r.EndDateTime })
                .AsEnumerable()
                .Select(r => (r.EndDateTime!.Value - r.TestDateTime).TotalSeconds)
                .Where(s => s > 0)
                .ToList();

            double avgCycleSeconds = completedDurations.Count == 0 ? 0 : completedDurations.Average();

            return new DashboardKpiDto
            {
                TotalTested = total,
                PassCount = pass,
                FailCount = fail,
                YieldPercent = total == 0 ? 0 : Math.Round(pass * 100.0 / total, 2),
                AvgCycleTimeSeconds = avgCycleSeconds,
                AvgCycleTimeFormatted = TimeSpan.FromSeconds(Math.Max(0, Math.Round(avgCycleSeconds))).ToString(@"hh\:mm\:ss")
            };
        }


        // Per test type, how many reports had at least one failure of it - not the net-fail total.
        public List<FailReasonSliceDto> GetFailReasonBreakdown(DashboardTimeRange range)
        {
            using var db = new AppDbContext();
            var filter = BuildRangeFilter(range);
            var reportIds = TestReportQueryHelper.ApplyFilter(db, filter).Where(r => !r.IsDraft).Select(r => r.ReportId);

            var grouped = db.ReportTestTypeSummaries
                .Where(s => reportIds.Contains(s.ReportId))
                .GroupBy(s => s.TestTypeName)
                .Select(g => new
                {
                    TestType = g.Key,
                    FailCount = g.Count(s => s.FailCount > 0)
                })
                .Where(g => g.FailCount > 0)
                .OrderByDescending(g => g.FailCount)
                .ToList();

            int totalFail = grouped.Sum(g => g.FailCount);
            if (totalFail == 0) return new List<FailReasonSliceDto>();

            return grouped
                .Select(g => new FailReasonSliceDto
                {
                    TestTypeName = g.TestType,
                    FailCount = g.FailCount,
                    PercentOfTotalFail = Math.Round(g.FailCount * 100.0 / totalFail, 1)
                })
                .ToList();
        }

        // Latest N reports for the RECENT TEST HISTORY grid, ignoring the range selector.
        public List<RecentTestItemDto> GetRecentTests(int count = 8)
        {
            using var db = new AppDbContext();
            return db.TestReports
                .Where(r => !r.IsDraft)
                .OrderByDescending(r => r.TestDateTime)
                .Take(count)
                .Select(r => new RecentTestItemDto
                {
                    ReportId = r.ReportId,
                    TestTime = AppTime.ToLocal(r.TestDateTime),
                    PartNumber = r.PartNumber,
                    SerialNumber = r.SerialNumber,
                    Result = r.Status == TestStatus.Completed
                        ? r.OverallResult == TestResult.Pass ? "PASS" : "FAIL"
                        : null,
                    Operator = r.Operator
                })
                .ToList();
        }

    }
}