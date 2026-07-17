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
        // The full vendor-defined test type list. No more "fixed 5-step" assumption -
        // a report can use any subset of these, in any quantity, per net.
        private static readonly string[] AllTestTypeNames =
        {
            "4RESISTOR", "4WIRE", "CAPACITOR", "CLOSE", "COMMENT", "DIODE", "LINK",
            "LUA COMPONENT", "OPEN", "PROBE", "RESISTOR", "SIGNATURE DIODE",
            "SIGNATURE WIRE", "TWISTED PAIR", "WIRE"
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

        // TypeSummaries is now DYNAMIC (top 5 test types actually present in this report,
        // ordered by count) instead of a fixed 5-item order - there's no canonical
        // "5 steps" anymore since a report can use any mix of the 15 vendor test types.
        public ReportDetailDto? GetReportDetail(int reportId)
        {
            using var db = new AppDbContext();
            var report = db.TestReports.FirstOrDefault(r => r.ReportId == reportId);
            if (report is null) return null;

            var details = db.TestDetails.Where(d => d.ReportId == reportId).ToList();

            var typeSummaries = details
                .GroupBy(d => d.TestTypeName)
                .Select(g => new TestTypeSummaryDto
                {
                    TestType = g.Key,
                    Total = g.Count(),
                    Pass = g.Count(d => d.Result),
                    Fail = g.Count(d => !d.Result),
                    PassRatePercent = g.Count() == 0 ? 100 : Math.Round(g.Count(d => d.Result) * 100.0 / g.Count(), 2)
                })
                .OrderByDescending(s => s.Total)
                .Take(5) // matches the 5 static rows in Reports.cs's preview panel (lblR1..lblR5)
                .ToList();

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

        // TEMPORARY: seeds sample data spread across the last 365 days.
        // Each report now picks a RANDOM SUBSET of the 15 vendor test types per net,
        // rather than following a fixed 5-step order.
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

            for (int dayOffset = 364; dayOffset >= 0; dayOffset--)
            {
                var day = DateTime.Today.AddDays(-dayOffset);
                bool isWeekend = day.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
                int reportsToday = isWeekend ? rnd.Next(0, 2) : rnd.Next(1, 6);

                for (int n = 0; n < reportsToday; n++)
                {
                    var testStart = day.AddHours(7 + rnd.NextDouble() * 10);

                    string model = productModels[rnd.Next(productModels.Length)];
                    string type = productTypes[rnd.Next(productTypes.Length)];
                    string area = areas[rnd.Next(areas.Length)];
                    string oper = operators[rnd.Next(operators.Length)];
                    string partNumber = $"WH-ENG-{rnd.Next(1, 20):000}";
                    string serialNumber = $"SN{rnd.Next(10000, 99999)}";

                    double statusRoll = rnd.NextDouble();
                    var status = statusRoll < 0.92 ? TestStatus.Completed
                        : statusRoll < 0.97 ? TestStatus.InProgress
                        : TestStatus.Stopped;

                    bool intendedPass = rnd.NextDouble() < 0.60;

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
                        OverallResult = null,
                        CreatedAt = testStart
                    };

                    // Pick a random subset of 4-8 test types used by this report (out of 15).
                    var usedTypes = AllTestTypeNames.OrderBy(_ => rnd.Next()).Take(rnd.Next(4, 9)).ToList();

                    int detailCount = 0;
                    int passCount = 0;
                    var cursor = testStart;

                    foreach (var testType in usedTypes)
                    {
                        int netsForThisType = rnd.Next(3, 15);
                        int stepDurationSeconds = rnd.Next(10, 90);
                        bool stepAllPassed = true;

                        for (int i = 1; i <= netsForThisType; i++)
                        {
                            detailCount++;
                            bool forceFail = !intendedPass && rnd.NextDouble() < 0.15;

                            var (measured, lower, upper, unit, result, errorDetail) = GenerateMeasurement(testType, forceFail, rnd);
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
                            StepNo = allSteps.Count(s => s.Report == report) + 1,
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
                    else
                    {
                        report.OverallResult = null;
                        report.EndDateTime = null;
                    }

                    reports.Add(report);
                }
            }

            using (var db = new AppDbContext())
            {
                db.TestReports.AddRange(reports);
                db.TestDetails.AddRange(allDetails);
                db.TestSteps.AddRange(allSteps);
                db.SaveChanges();
            }
        }

        // ASSUMPTION: unit/measurement behavior per vendor test type - confirm and adjust as needed.
        // OPEN/CLOSE/LINK/PROBE/COMMENT/LUA COMPONENT are treated as non-measurement (logical/marker)
        // test types with no numeric value, always evaluated as pass unless explicitly forced to fail.
        private static (double? measured, double? lower, double? upper, string unit, bool result, string? errorDetail)
            GenerateMeasurement(string testType, bool forceFail, Random rnd)
        {
            switch (testType)
            {
                case "RESISTOR":
                case "4RESISTOR":
                    {
                        double upper = 10.0;
                        double val = forceFail ? Math.Round(upper + rnd.NextDouble() * 20, 2) : Math.Round(rnd.NextDouble() * 8, 2);
                        bool pass = val <= upper;
                        return (val, null, upper, "Ω", pass, pass ? null : "Resistance out of tolerance");
                    }

                case "WIRE":
                case "4WIRE":
                case "TWISTED PAIR":
                case "SIGNATURE WIRE":
                    {
                        double upper = 1.0;
                        if (forceFail && rnd.NextDouble() < 0.3)
                            return (null, null, upper, "Ω", false, "Open circuit detected - no continuity");

                        double val = forceFail ? Math.Round(upper + rnd.NextDouble() * 5, 2) : Math.Round(rnd.NextDouble() * 0.9, 2);
                        bool pass = val <= upper;
                        return (val, null, upper, "Ω", pass, pass ? null : "Resistance exceeds limit");
                    }

                case "CAPACITOR":
                    {
                        double lower = 90.0, upper = 110.0; // e.g. nF, ±10% band
                        double val = forceFail
                            ? Math.Round(rnd.NextDouble() < 0.5 ? lower * 0.5 : upper * 1.5, 1)
                            : Math.Round(lower + rnd.NextDouble() * (upper - lower), 1);
                        bool pass = val >= lower && val <= upper;
                        return (val, lower, upper, "nF", pass, pass ? null : "Capacitance out of tolerance band");
                    }

                case "DIODE":
                case "SIGNATURE DIODE":
                    {
                        double lower = 0.4, upper = 0.9; // forward voltage drop, V
                        double val = forceFail
                            ? Math.Round(rnd.NextDouble() < 0.5 ? 0 : 2.0, 2)
                            : Math.Round(lower + rnd.NextDouble() * (upper - lower), 2);
                        bool pass = val >= lower && val <= upper;
                        return (val, lower, upper, "V", pass, pass ? null : "Forward voltage out of range - possible reversed/shorted diode");
                    }

                case "OPEN":
                case "CLOSE":
                case "LINK":
                case "PROBE":
                case "COMMENT":
                case "LUA COMPONENT":
                    {
                        bool pass = !forceFail;
                        return (null, null, null, "-", pass, pass ? null : $"{testType} check failed");
                    }

                default:
                    return (null, null, null, "-", true, null);
            }
        }

        public int SaveLiveTestReport(string productModel, string productType, string area, string ecnNumber, string revision, string partNumber, string serialNumber, string operatorName, DateTime testDateTime, List<LiveTestDetailRowDto> rows, string overallResultStr)
        {
            using var db = new AppDbContext();

            bool isPass = string.Equals(overallResultStr, "PASS", StringComparison.OrdinalIgnoreCase);
            int passCount = rows != null ? rows.Count(r => r.Status == LiveTestStatus.Pass) : 0;
            int totalCount = rows != null ? rows.Count : 0;
            int failCount = totalCount - passCount;

            var report = new TestReport
            {
                ReportNumber = $"TR-{DateTime.Now:yyyyMMdd-HHmmss}",
                ProductModel = productModel ?? "",
                ProductType = productType ?? "",
                Area = area ?? "",
                ECNNumber = ecnNumber ?? "",
                Revision = string.IsNullOrEmpty(revision) ? "1.0" : revision,
                PartNumber = partNumber ?? "",
                SerialNumber = string.IsNullOrEmpty(serialNumber) ? $"SN-{DateTime.Now:HHmmss}" : serialNumber,
                Operator = string.IsNullOrEmpty(operatorName) ? "OP-402" : operatorName,
                TesterModel = "ALT-2000",
                TestDateTime = testDateTime == default ? DateTime.Now : testDateTime,
                EndDateTime = DateTime.Now,
                TotalNets = totalCount,
                PassCount = passCount,
                FailCount = failCount,
                Status = TestStatus.Completed,
                OverallResult = isPass ? TestResult.Pass : TestResult.Fail,
                CreatedAt = DateTime.UtcNow
            };

            db.TestReports.Add(report);
            db.SaveChanges();

            if (rows != null && rows.Count > 0)
            {
                var details = new List<TestDetail>();
                var steps = new List<TestStep>();

                int stepCount = 1;
                foreach (var r in rows)
                {
                    bool rowPass = r.Status == LiveTestStatus.Pass;
                    details.Add(new TestDetail
                    {
                        ReportId = report.ReportId,
                        NetId = r.NetId ?? $"N{stepCount:000}",
                        HarnessGroup = $"{area} Connector",
                        FromPin = string.IsNullOrEmpty(r.TestPoint) ? "-" : r.TestPoint,
                        ToPin = "-",
                        Color = "Red",
                        TestTypeName = string.IsNullOrEmpty(r.TestType) ? "WIRE" : r.TestType,
                        MeasuredValue = r.MeasuredValue,
                        LowerLimit = null,
                        UpperLimit = null,
                        Unit = "Ω",
                        Result = rowPass,
                        ErrorDetail = rowPass ? null : "Measurement out of limits"
                    });

                    steps.Add(new TestStep
                    {
                        ReportId = report.ReportId,
                        StepNo = stepCount++,
                        TestTypeName = string.IsNullOrEmpty(r.TestType) ? "WIRE" : r.TestType,
                        Result = rowPass,
                        DurationSeconds = 2
                    });
                }

                db.TestDetails.AddRange(details);
                db.TestSteps.AddRange(steps);
                db.SaveChanges();
            }

            return report.ReportId;
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