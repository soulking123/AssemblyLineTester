using AssemblyLineTester.Data;
using AssemblyLineTester.Models;
using AssemblyLineTester.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AssemblyLineTester.Services
{
    public class TestReportService
    {
        // FIXED ORDER OF TEST TYPES FOR THE REPORT SUMMARY TABLE (5 STATIC ROWS IN Reports.cs).
        public static readonly string[] FixedTestTypeOrder =
        {
            "Continuity Test", "Short Circuit Test", "Insulation Test", "Functional Test", "Final Verification"
        };

        // BUILDS AN IQUERYABLE WITH ALL COMMON FILTERS APPLIED (SHARED BY LIST/SUMMARY/TREND).
        private static IQueryable<Models.TestReport> ApplyFilter(AppDbContext db, ReportFilter filter)
        {
            var query = db.TestReports.AsQueryable();

            if (filter.DateFrom.HasValue)
                query = query.Where(r => r.TestDateTime >= filter.DateFrom.Value.Date);

            if (filter.DateTo.HasValue)
                query = query.Where(r => r.TestDateTime < filter.DateTo.Value.Date.AddDays(1));

            if (!string.IsNullOrWhiteSpace(filter.ProductModel))
                query = query.Where(r => r.ProductModel == filter.ProductModel);

            if (!string.IsNullOrWhiteSpace(filter.ProductType))
                query = query.Where(r => r.ProductType == filter.ProductType);

            if (!string.IsNullOrWhiteSpace(filter.Area))
                query = query.Where(r => r.Area == filter.Area);

            if (!string.IsNullOrWhiteSpace(filter.Result))
                query = query.Where(r => r.OverallResult == filter.Result);

            if (!string.IsNullOrWhiteSpace(filter.Operator))
                query = query.Where(r => r.Operator == filter.Operator);

            if (!string.IsNullOrWhiteSpace(filter.PartNumberKeyword))
                query = query.Where(r => r.PartNumber.Contains(filter.PartNumberKeyword));

            return query;
        }

        // SUMMARY CARDS: Total Tested, Pass, Fail, In Progress.
        public ReportSummary GetSummary(ReportFilter filter)
        {
            using var db = new AppDbContext();
            var reports = ApplyFilter(db, filter).ToList();

            int total = reports.Count;
            int pass = reports.Count(r => string.Equals(r.OverallResult, "PASS", StringComparison.OrdinalIgnoreCase));
            int fail = reports.Count(r => string.Equals(r.OverallResult, "FAIL", StringComparison.OrdinalIgnoreCase));
            int inProgress = total - pass - fail;

            return new ReportSummary
            {
                TotalTested = total,
                PassCount = pass,
                FailCount = fail,
                InProgressCount = inProgress,
                PassRatePercent = total == 0 ? 0 : Math.Round(pass * 100.0 / total, 2)
            };
        }

        // "RESULT TREND" LINE CHART: PASS/FAIL/TOTAL GROUPED BY DAY.
        public List<ReportTrendPoint> GetResultTrend(ReportFilter filter)
        {
            using var db = new AppDbContext();
            var reports = ApplyFilter(db, filter).ToList();

            return reports
                .GroupBy(r => r.TestDateTime.Date)
                .OrderBy(g => g.Key)
                .Select(g => new ReportTrendPoint
                {
                    Date = g.Key,
                    PassCount = g.Count(r => string.Equals(r.OverallResult, "PASS", StringComparison.OrdinalIgnoreCase)),
                    FailCount = g.Count(r => string.Equals(r.OverallResult, "FAIL", StringComparison.OrdinalIgnoreCase)),
                    TotalCount = g.Count()
                })
                .ToList();
        }

        // MIDDLE "REPORT LIST" GRID, WITH PAGING.
        public PagedResult<ReportListItem> GetReportList(ReportFilter filter)
        {
            using var db = new AppDbContext();
            var query = ApplyFilter(db, filter).OrderByDescending(r => r.TestDateTime);

            int totalCount = query.Count();
            var page = query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();

            var items = page.Select(r => new ReportListItem
            {
                ReportId = r.ReportId,
                ReportName = $"Test Report_{r.TestDateTime:yyyyMMdd_HHmmss}",
                ReportType = "Test Report",
                DateGenerated = r.CreatedAt,
                GeneratedBy = r.Operator,
                SizeKb = EstimateSizeKb(r.TotalNets),
                OverallResult = r.OverallResult
            }).ToList();

            return new PagedResult<ReportListItem>
            {
                Items = items,
                TotalCount = totalCount,
                Page = filter.Page,
                PageSize = filter.PageSize
            };
        }

        // RIGHT-SIDE "REPORT PREVIEW" PANEL - MIRRORS THE EXPORTED PDF LAYOUT.
        // TypeSummaries ALWAYS RETURNS 5 ITEMS, ONE PER FixedTestTypeOrder ENTRY (0-FILLED IF MISSING),
        // SO Reports.cs CAN MAP THEM DIRECTLY TO lblR1..lblR5 BY INDEX.
        public ReportDetailDto? GetReportDetail(int reportId)
        {
            using var db = new AppDbContext();
            var report = db.TestReports
                .Where(r => r.ReportId == reportId)
                .Select(r => new
                {
                    r.ReportNumber,
                    r.TestDateTime,
                    r.EndDateTime,
                    r.ProductModel,
                    r.ProductType,
                    r.Area,
                    r.ECNNumber,
                    r.Revision,
                    r.PartNumber,
                    r.SerialNumber,
                    r.Operator,
                    r.OverallResult
                })
                .FirstOrDefault();

            if (report is null) return null;

            var details = db.TestDetails
                .Where(d => d.ReportId == reportId)
                .ToList();

            var groupedByType = details
                .GroupBy(d => d.TestTypeName)
                .ToDictionary(g => g.Key, g => g.ToList());

            var typeSummaries = FixedTestTypeOrder.Select(typeName =>
            {
                var list = groupedByType.TryGetValue(typeName, out var l) ? l : new List<Models.TestDetail>();
                int total = list.Count;
                int pass = list.Count(d => d.Result);
                int fail = total - pass;

                return new TestTypeSummaryDto
                {
                    TestType = typeName,
                    Total = total,
                    Pass = pass,
                    Fail = fail,
                    PassRatePercent = total == 0 ? 100 : Math.Round(pass * 100.0 / total, 2)
                };
            }).ToList();

            var detailDtos = details.Select(d => new TestDetailDto
            {
                NetId = d.NetId,
                HarnessGroup = d.HarnessGroup,
                FromPin = d.FromPin,
                ToPin = d.ToPin,
                Color = d.Color,
                TestType = d.TestTypeName,
                Limit = FormatLimit(d.LowerLimit, d.UpperLimit, d.Unit),
                Measured = d.MeasuredValue.HasValue ? $"{d.MeasuredValue.Value} {d.Unit}" : "N/A",
                Result = FormatResult(d.Result, d.MeasuredValue),
                ErrorDetail = d.ErrorDetail
            }).ToList();

            TimeSpan? duration = report.EndDateTime.HasValue ? report.EndDateTime.Value - report.TestDateTime : null;

            return new ReportDetailDto
            {
                ReportNumber = report.ReportNumber,
                TestDateTime = report.TestDateTime,
                ProductModel = report.ProductModel,
                ProductType = report.ProductType,
                Area = report.Area,
                ECNNumber = report.ECNNumber,
                Revision = report.Revision,
                PartNumber = report.PartNumber,
                SerialNumber = report.SerialNumber,
                Operator = report.Operator,
                TestTime = duration.HasValue ? duration.Value.ToString(@"hh\:mm\:ss") : "-",
                OverallResult = report.OverallResult,
                TypeSummaries = typeSummaries,
                Details = detailDtos
            };
        }

        // ROUGH FILE-SIZE ESTIMATE FOR DISPLAY ONLY (NOT A REAL EXPORTED FILE SIZE YET).
        private static double EstimateSizeKb(int totalNets) => Math.Round(300 + (totalNets * 8) / 1024.0, 2);

        private static string FormatLimit(double? lower, double? upper, string unit)
        {
            if (lower.HasValue && upper.HasValue) return $"{lower} - {upper} {unit}";
            if (upper.HasValue) return $"< {upper} {unit}";
            if (lower.HasValue) return $"{lower} {unit} min";
            return "-";
        }

        private static string FormatResult(bool result, double? measured)
        {
            if (result) return "PASS";
            return measured.HasValue ? "FAIL" : "OPEN";
        }

        // TEMPORARY: SEEDS SAMPLE TEST REPORTS + DETAILS FOR TESTING/DEMO PURPOSES.
        public void SeedSampleDataIfEmpty()
        {
            using (var db = new AppDbContext())
            {
                if (db.TestReports.Any()) return;
            }

            var rnd = new Random(2026);

            string[] productModels = { "6060", "6070", "EXC-450-MainHarness_Rev3" };
            string[] productTypes = { "AC", "DC" };
            string[] areas = { "Engine Control", "Power Distribution", "Cab Electrical" };
            string[] operators = { "Fally", "B. Santoso", "Doni Pratama", "Rian Hidayat" };
            string[] colors = { "Red", "Black", "Blue", "Green", "Yellow", "White", "Orange", "Brown", "Gray", "Violet" };

            var reports = new List<TestReport>();
            var allDetails = new List<TestDetail>();

            const int totalReports = 24;
            var baseTime = DateTime.Now.AddDays(-14);

            for (int r = 1; r <= totalReports; r++)
            {
                var testStart = baseTime.AddHours(r * 9 + rnd.Next(0, 4));
                string model = productModels[rnd.Next(productModels.Length)];
                string type = productTypes[rnd.Next(productTypes.Length)];
                string area = areas[rnd.Next(areas.Length)];
                string oper = operators[rnd.Next(operators.Length)];
                string partNumber = $"WH-ENG-{rnd.Next(1, 20):000}";
                string serialNumber = $"SN{rnd.Next(10000, 99999)}";

                var report = new TestReport
                {
                    ReportNumber = $"TR-{testStart:yyyyMMdd-HHmmss}",
                    ProductModel = model,
                    ProductType = type,
                    Area = area,
                    ECNNumber = $"12{rnd.Next(1, 9)}.R00{rnd.Next(1, 3)}",
                    Revision = "A",
                    PartNumber = partNumber,
                    SerialNumber = serialNumber,
                    Operator = oper,
                    TesterModel = "ALT-2000",
                    TestDateTime = testStart,
                    CreatedAt = testStart
                };

                int detailCount = 0;
                int passCount = 0;

                // Generate details per fixed test type, with varying counts and an occasional failure.
                int[] countsPerType = { rnd.Next(30, 45), rnd.Next(0, 5), rnd.Next(5, 12), rnd.Next(0, 8), rnd.Next(0, 6) };

                for (int t = 0; t < FixedTestTypeOrder.Length; t++)
                {
                    string testType = FixedTestTypeOrder[t];
                    int count = countsPerType[t];

                    for (int n = 1; n <= count; n++)
                    {
                        detailCount++;
                        bool forceFail = rnd.NextDouble() < 0.04; // ~4% chance of failure per net
                        bool isOpen = forceFail && rnd.NextDouble() < 0.3;

                        var (measured, lower, upper, unit, result, errorDetail) = GenerateMeasurement(testType, forceFail, isOpen, rnd);
                        if (result) passCount++;

                        allDetails.Add(new TestDetail
                        {
                            Report = report,
                            NetId = $"N{detailCount:000}",
                            HarnessGroup = $"{area} Connector",
                            FromPin = $"J{rnd.Next(1, 9)}-{rnd.Next(1, 20)}",
                            ToPin = $"J{rnd.Next(9, 13)}-{rnd.Next(1, 20)}",
                            Color = colors[rnd.Next(colors.Length)],
                            TestTypeName = testType,
                            MeasuredValue = measured,
                            LowerLimit = lower,
                            UpperLimit = upper,
                            Unit = unit,
                            Result = result,
                            ErrorDetail = errorDetail
                        });
                    }
                }

                report.TotalNets = detailCount;
                report.PassCount = passCount;
                report.FailCount = detailCount - passCount;
                report.OverallResult = report.FailCount == 0 ? "PASS" : "FAIL";
                report.EndDateTime = testStart.AddMinutes(rnd.Next(5, 15));

                reports.Add(report);
            }

            using (var db = new AppDbContext())
            {
                db.TestReports.AddRange(reports);
                db.TestDetails.AddRange(allDetails);
                db.SaveChanges();
            }
        }

        // GENERATES A REALISTIC MEASUREMENT + RESULT FOR A GIVEN TEST TYPE.
        private static (double? measured, double? lower, double? upper, string unit, bool result, string? errorDetail)
            GenerateMeasurement(string testType, bool forceFail, bool isOpen, Random rnd)
        {
            switch (testType)
            {
                case "Continuity Test":
                case "Short Circuit Test":
                    {
                        double upper = 1.0;
                        if (isOpen) return (null, null, upper, "Ohm", false, "Open circuit detected - no continuity");
                        double val = forceFail ? Math.Round(upper + rnd.NextDouble() * 5, 2) : Math.Round(rnd.NextDouble() * 0.9, 2);
                        bool pass = val <= upper;
                        return (val, null, upper, "Ohm",
                            pass, pass ? null : "Resistance exceeds limit - possible short or degraded connection");
                    }

                case "Insulation Test":
                    {
                        double lower = 500.0;
                        double val = forceFail
                            ? Math.Round(rnd.NextDouble() * lower * 0.8, 1)
                            : Math.Round(lower + rnd.NextDouble() * 1500, 1);
                        bool pass = val >= lower;
                        return (val, lower, null, "MOhm",
                            pass, pass ? null : "Isolation resistance below minimum requirement");
                    }

                case "Functional Test":
                case "Final Verification":
                    {
                        bool pass = !forceFail;
                        double val = pass ? 1 : 0;
                        return (val, 1, 1, "-", pass, pass ? null : "Functional check failed - output not as expected");
                    }

                default:
                    return (null, null, null, "-", true, null);
            }
        }
    }
}