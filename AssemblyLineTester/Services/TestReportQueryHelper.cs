using System;
using System.Linq;
using AssemblyLineTester.Data;
using AssemblyLineTester.Helpers;
using AssemblyLineTester.Models;
using AssemblyLineTester.Models.Dto;

namespace AssemblyLineTester.Services
{
    internal static class TestReportQueryHelper
    {
        public static IQueryable<TestReport> ApplyFilter(AppDbContext db, ReportFilter filter)
        {
            var query = db.TestReports.AsQueryable();

            // Pickers give local dates, TestDateTime is UTC, so the range is converted first.
            var (fromUtc, toUtc) = AppTime.LocalDateRangeToUtc(filter.DateFrom, filter.DateTo);

            if (fromUtc.HasValue)
                query = query.Where(r => r.TestDateTime >= fromUtc.Value);

            if (toUtc.HasValue)
                query = query.Where(r => r.TestDateTime < toUtc.Value);

            if (!string.IsNullOrWhiteSpace(filter.ProductModel))
                query = query.Where(r => r.ProductModel == filter.ProductModel);

            if (!string.IsNullOrWhiteSpace(filter.ProductType))
                query = query.Where(r => r.ProductType == filter.ProductType);

            if (!string.IsNullOrWhiteSpace(filter.Area))
                query = query.Where(r => r.Area == filter.Area);

            if (!string.IsNullOrWhiteSpace(filter.Result)
                && Enum.TryParse<TestResult>(filter.Result, true, out var resultEnum))
            {
                query = query.Where(r => r.OverallResult == resultEnum);
            }

            if (!string.IsNullOrWhiteSpace(filter.Operator))
                query = query.Where(r => r.Operator == filter.Operator);

            if (!string.IsNullOrWhiteSpace(filter.PartNumberKeyword))
                query = query.Where(r => r.PartNumber.Contains(filter.PartNumberKeyword));

            return query;
        }
    }
}