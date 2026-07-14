using AssemblyLineTester.Data;
using AssemblyLineTester.Models;
using AssemblyLineTester.Models.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static AssemblyLineTester.Models.SystemLog;

namespace AssemblyLineTester.Services
{
    public class SystemLogService
    {
        // RECORDS A SINGLE NEW LOG ENTRY.
        public void Log(LogSeverity level, string moduleName, string actionName, string message, string? userName = null, string? ipAddress = null)
        {
            if (string.IsNullOrWhiteSpace(moduleName))
                throw new ArgumentException("Module name is required.", nameof(moduleName));
            if (string.IsNullOrWhiteSpace(actionName))
                throw new ArgumentException("Action name is required.", nameof(actionName));

            var entry = new SystemLog
            {
                Timestamp = DateTime.UtcNow,
                SeverityLevel = level,
                ModuleName = moduleName.Trim(),
                ActionName = actionName.Trim(),
                Message = message?.Trim() ?? string.Empty,
                UserName = string.IsNullOrWhiteSpace(userName) ? SessionManager.CurrentUser?.Username : userName,
                IPAddress = ipAddress
            };

            using (var db = new AppDbContext())
            {
                db.SystemLogs.Add(entry);
                db.SaveChanges();
            }
        }

        // RETRIEVES PAGINATED LOG DATA ACCORDING TO THE ACTIVE FILTER.
        public PagedResult<SystemLog> GetLogs(SystemLogFilter filter)
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            using (var db = new AppDbContext())
            {
                var query = ApplyFilter(db.SystemLogs.AsQueryable(), filter);

                // Count the total BEFORE Skip/Take. Counting after paging
                // would only reflect the current page, not the full filtered result.
                int totalCount = query.Count();

                var items = query
                    .OrderByDescending(l => l.Timestamp)
                    .Skip((filter.Page - 1) * filter.PageSize)
                    .Take(filter.PageSize)
                    .ToList();

                return new PagedResult<SystemLog>
                {
                    Items = items,
                    TotalCount = totalCount,
                    Page = filter.Page,
                    PageSize = filter.PageSize
                };
            }
        }

        // COMPUTES THE SUMMARY (TOTAL PER LEVEL, TOTAL PER MODULE) FOR THE ACTIVE FILTER
        public SystemLogSummary GetSummary(SystemLogFilter filter)
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            using (var db = new AppDbContext())
            {
                var query = ApplyFilter(db.SystemLogs.AsQueryable(), filter);

                var levelCounts = query
                    .GroupBy(l => l.SeverityLevel)
                    .Select(g => new { Level = g.Key, Count = g.Count() })
                    .ToList();

                var moduleCounts = query
                    .GroupBy(l => l.ModuleName)
                    .Select(g => new { Module = g.Key, Count = g.Count() })
                    .ToList();

                var summary = new SystemLogSummary
                {
                    InfoCount = levelCounts.FirstOrDefault(x => x.Level == LogSeverity.Info)?.Count ?? 0,
                    WarningCount = levelCounts.FirstOrDefault(x => x.Level == LogSeverity.Warning)?.Count ?? 0,
                    ErrorCount = levelCounts.FirstOrDefault(x => x.Level == LogSeverity.Error)?.Count ?? 0,
                };
                summary.Total = summary.InfoCount + summary.WarningCount + summary.ErrorCount;

                foreach (var m in moduleCounts)
                {
                    summary.CountByModule[m.Module] = m.Count;
                }

                return summary;
            }
        }

        // APPLIES ALL ACTIVE FILTER CRITERIA TO THE QUERY, SHARED BY GETLOGS AND GETSUMMARY.
        private static IQueryable<SystemLog> ApplyFilter(IQueryable<SystemLog> query, SystemLogFilter filter)
        {
            if (filter.DateFrom.HasValue)
            {
                var from = filter.DateFrom.Value.Date;
                query = query.Where(l => l.Timestamp >= from);
            }

            if (filter.DateTo.HasValue)
            {
                var to = filter.DateTo.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(l => l.Timestamp <= to);
            }

            if (filter.SeverityLevel.HasValue)
            {
                query = query.Where(l => l.SeverityLevel == filter.SeverityLevel.Value);
            }

            if (!string.IsNullOrWhiteSpace(filter.ModuleName))
            {
                query = query.Where(l => l.ModuleName == filter.ModuleName);
            }

            if (!string.IsNullOrWhiteSpace(filter.UserName))
            {
                query = query.Where(l => l.UserName == filter.UserName);
            }

            if (!string.IsNullOrWhiteSpace(filter.ActionName))
            {
                query = query.Where(l => l.ActionName == filter.ActionName);
            }

            if (!string.IsNullOrWhiteSpace(filter.Keyword))
            {
                var keyword = filter.Keyword.Trim();
                query = query.Where(l =>
                    EF.Functions.Like(l.Message, $"%{keyword}%") ||
                    EF.Functions.Like(l.ActionName, $"%{keyword}%"));
            }

            return query;
        }

        // TEMPORARY: SEEDS SAMPLE LOG ENTRIES FOR TESTING BEFORE OTHER MODULES CALL LOG() FOR REAL.
        public void SeedSampleDataIfEmpty()
        {
            using (var db = new AppDbContext())
            {
                if (db.SystemLogs.Any()) return;
            }

            var samples = new (LogSeverity Level, string Module, string Action, string Message, string? User)[]
            {
        (LogSeverity.Info, "Test Execution", "Start Test", "Test started for Part Number WH-ENG-001", "Fally"),
        (LogSeverity.Info, "Test Execution", "Load Program", "Test program loaded successfully", "Fally"),
        (LogSeverity.Info, "Test Execution", "Step Complete", "Step \"Continuity Test\" completed", "Fally"),
        (LogSeverity.Warning, "Harness Database", "Update", "ECN revision updated for WH-ENG-001", "Fally"),
        (LogSeverity.Error, "Communication", "Connection Lost", "Lost connection with PLC at 192.168.1.20", "System"),
        (LogSeverity.Info, "Communication", "Reconnected", "PLC reconnected successfully", "System"),
        (LogSeverity.Info, "User Management", "Login", "User login success", "Fally"),
        (LogSeverity.Warning, "Database", "Backup", "Database auto backup completed", "System"),
            };

            foreach (var s in samples)
            {
                Log(s.Level, s.Module, s.Action, s.Message, s.User);
            }
        }
    }
}