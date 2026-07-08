using System;
using System.Collections.Generic;
using System.Linq;
using AssemblyLineTester.Data;
using AssemblyLineTester.Models;
using AssemblyLineTester.Models.Dto;

namespace AssemblyLineTester.Services
{
    public class TestReportService
    {
        public static readonly string[] FixedTestTypeOrder =
        {
            "Continuity Test", "Short Circuit Test", "Insulation Test", "Functional Test", "Final Verification"
        };

        public ReportSummary GetSummary(ReportFilter filter)
        {
            using var db = new AppDbContext();
            var reports = TestReportQueryHelper.ApplyFilter(db, filter).ToList();

            int total = reports.Count;
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

        public List<ReportTrendPoint> GetResultTrend(ReportFilter filter)
        {
            using var db = new AppDbContext();
            var reports = TestReportQueryHelper.ApplyFilter(db, filter).ToList();

            return reports
                .GroupBy(r => r.TestDateTime.Date)
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

        public PagedResult<ReportListItem> GetReportList(ReportFilter filter)
        {
            using var db = new AppDbContext();
            var query = TestReportQueryHelper.ApplyFilter(db, filter).OrderByDescending(r => r.TestDateTime);

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

            var groupedByType = details.GroupBy(d => d.TestTypeName).ToDictionary(g => g.Key, g => g.ToList());

            var typeSummaries = FixedTestTypeOrder.Select(typeName =>
            {
                var list = groupedByType.TryGetValue(typeName, out var l) ? l : new List<TestDetail>();
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
                OverallResult = report.OverallResult?.ToString().ToUpperInvariant() ?? "-",
                TypeSummaries = typeSummaries,
                Details = detailDtos
            };
        }

        // TEMPORARY: SEEDS SAMPLE TEST REPORTS + DETAILS + STEPS. Shared by Reports page and Test History page.
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
            var allSteps = new List<TestStep>();

            const int totalReports = 220; // roughly matches the "Total: 215" scale seen in Test History
            var baseTime = DateTime.Now.AddDays(-45);

            for (int r = 1; r <= totalReports; r++)
            {
                var testStart = baseTime.AddMinutes(r * 45 + rnd.Next(0, 20));

                string model = productModels[rnd.Next(productModels.Length)];
                string type = productTypes[rnd.Next(productTypes.Length)];
                string area = areas[rnd.Next(areas.Length)];
                string oper = operators[rnd.Next(operators.Length)];
                string partNumber = $"WH-ENG-{rnd.Next(1, 20):000}";
                string serialNumber = $"SN{rnd.Next(10000, 99999)}";

                double statusRoll = rnd.NextDouble();
                TestStatus status = statusRoll < 0.90 ? TestStatus.Completed
                    : statusRoll < 0.95 ? TestStatus.InProgress
                    : TestStatus.Stopped;

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
                    Status = status,
                    CreatedAt = testStart
                };

                // Completed reports run all 5 steps; InProgress/Stopped only got partway through.
                int stepsToRun = status == TestStatus.Completed
                    ? FixedTestTypeOrder.Length
                    : rnd.Next(1, FixedTestTypeOrder.Length);

                int detailCount = 0;
                int passCount = 0;
                var cursor = testStart;

                int[] countsPerType = { rnd.Next(30, 45), rnd.Next(0, 5), rnd.Next(5, 12), rnd.Next(0, 8), rnd.Next(0, 6) };

                for (int t = 0; t < stepsToRun; t++)
                {
                    string testType = FixedTestTypeOrder[t];
                    int count = countsPerType[t];
                    int stepDurationSeconds = rnd.Next(20, 150);
                    bool stepAllPassed = true;

                    for (int n = 1; n <= count; n++)
                    {
                        detailCount++;
                        bool forceFail = rnd.NextDouble() < 0.04;
                        bool isOpen = forceFail && rnd.NextDouble() < 0.3;

                        var (measured, lower, upper, unit, result, errorDetail) = GenerateMeasurement(testType, forceFail, isOpen, rnd);
                        if (result) passCount++;
                        else stepAllPassed = false;

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

                    allSteps.Add(new TestStep
                    {
                        Report = report,
                        StepNo = t + 1,
                        TestTypeName = testType,
                        Result = stepAllPassed,
                        DurationSeconds = stepDurationSeconds
                    });

                    cursor = cursor.AddSeconds(stepDurationSeconds);
                }

                report.TotalNets = detailCount;
                report.PassCount = passCount;
                report.FailCount = detailCount - passCount;

                if (status == TestStatus.Completed)
                {
                    report.OverallResult = report.FailCount == 0 ? TestResult.Pass : TestResult.Fail;
                    report.EndDateTime = cursor;
                }
                else if (status == TestStatus.Stopped)
                {
                    report.OverallResult = null;
                    report.EndDateTime = cursor; 
                }
                else // InProgress
                {
                    report.OverallResult = null;
                    report.EndDateTime = null; 
                }

                reports.Add(report);
            }

            using (var db = new AppDbContext())
            {
                db.TestReports.AddRange(reports);
                db.TestDetails.AddRange(allDetails);
                db.TestSteps.AddRange(allSteps);
                db.SaveChanges();
            }
        }

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
    }
}