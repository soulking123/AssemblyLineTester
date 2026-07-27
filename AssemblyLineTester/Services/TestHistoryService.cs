using System;
using System.Linq;
using AssemblyLineTester.Data;
using AssemblyLineTester.Models.Dto;

namespace AssemblyLineTester.Services
{
    public class TestHistoryService
    {
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
                TestTime = r.TestDateTime,
                ProductModel = r.ProductModel,
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

        public bool DeleteHistory(int reportId)
        {
            using var db = new AppDbContext();
            var report = db.TestReports.FirstOrDefault(r => r.ReportId == reportId);
            if (report == null) return false;

            using var tx = db.Database.BeginTransaction();
            try
            {
                var steps = db.TestSteps.Where(s => s.ReportId == reportId);
                db.TestSteps.RemoveRange(steps);
                db.TestReports.Remove(report);
                db.SaveChanges();
                tx.Commit();
                return true;
            }
            catch (Exception ex)
            {
                FileLogService.Error("TestHistoryService.DeleteReport",
                    $"Could not delete report {reportId}; rolling back.", ex);

                try
                {
                    tx.Rollback();
                }
                catch (Exception rollbackFailure)
                {
                    // A failed rollback can leave the report half-deleted, so it is worse than the
                    // original error and must not be the one that disappears.
                    FileLogService.Error("TestHistoryService.DeleteReport",
                        $"Rollback failed after a delete error on report {reportId}.", rollbackFailure);
                }

                return false;
            }
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
                StartTime = report.TestDateTime,
                EndTime = report.EndDateTime,
                TotalTime = FormatDuration(report.TestDateTime, report.EndDateTime),
                ProductModel = report.ProductModel,
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