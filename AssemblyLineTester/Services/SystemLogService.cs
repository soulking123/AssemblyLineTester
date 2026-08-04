using AssemblyLineTester.Data;
using AssemblyLineTester.Helpers;
using AssemblyLineTester.Models;
using AssemblyLineTester.Models.Dto;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static AssemblyLineTester.Models.SystemLog;

namespace AssemblyLineTester.Services
{
    public class SystemLogService
    {
        // One business event for the System Log page. Never throws; technical detail goes to FileLogService.
        public void Log(LogSeverity level, string moduleName, string actionName, string message, string? userName = null)
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
                MachineName = AppInfo.MachineName
            };

            try
            {
                using var db = new AppDbContext();
                db.SystemLogs.Add(entry);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                FileLogService.Error("SystemLogService.Log",
                    $"Could not record '{moduleName}/{actionName}' to the database log.", ex);
            }
        }

        // Deletes entries past the retention window; call once at startup.
        public int PurgeOlderThan(int days = RetentionDays)
        {
            if (days < 1) throw new ArgumentOutOfRangeException(nameof(days), "Retention must be at least one day.");

            try
            {
                DateTime cutoff = DateTime.UtcNow.Date.AddDays(-days);

                using var db = new AppDbContext();
                int removed = db.SystemLogs.Where(l => l.Timestamp < cutoff).ExecuteDelete();

                if (removed > 0)
                {
                    FileLogService.Info("SystemLogService.Purge", $"{removed} log entries older than {days} days removed.");
                }

                return removed;
            }
            catch (Exception ex)
            {
                FileLogService.Error("SystemLogService.Purge", "Could not purge old log entries.", ex);
                return 0;
            }
        }

        // How long entries are kept on the System Log page.
        public const int RetentionDays = 180;

        // Writes the whole filtered set to an .xlsx, not just the page on screen.
        public int ExportToExcel(SystemLogFilter filter, string outputPath)
        {
            ArgumentNullException.ThrowIfNull(filter);
            if (string.IsNullOrWhiteSpace(outputPath))
                throw new ArgumentException("Output path is required.", nameof(outputPath));

            List<SystemLog> rows = GetAllMatching(filter);

            using var wb = new XLWorkbook();

            var ws = wb.Worksheets.Add("Log Entries");
            string[] headers = ["No.", "Timestamp", "Level", "Module", "User", "Action", "Description", "Machine"];

            for (int c = 0; c < headers.Length; c++)
            {
                var cell = ws.Cell(1, c + 1);
                cell.Value = headers[c];
                cell.Style.Font.Bold = true;
                cell.Style.Fill.BackgroundColor = XLColor.FromArgb(230, 230, 230);
                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            }

            int r = 2;
            foreach (SystemLog log in rows)
            {
                ws.Cell(r, 1).Value = r - 1;

                // A real DateTime, so the column sorts by time rather than alphabetically.
                ws.Cell(r, 2).Value = log.Timestamp.ToLocalTime();
                ws.Cell(r, 2).Style.DateFormat.Format = "yyyy-mm-dd hh:mm:ss";

                ws.Cell(r, 3).Value = log.SeverityLevel.ToString();
                ws.Cell(r, 4).Value = log.ModuleName;
                ws.Cell(r, 5).Value = string.IsNullOrEmpty(log.UserName) ? "-" : log.UserName;
                ws.Cell(r, 6).Value = log.ActionName;
                ws.Cell(r, 7).Value = log.Message;
                ws.Cell(r, 8).Value = string.IsNullOrEmpty(log.MachineName) ? "-" : log.MachineName;

                ws.Cell(r, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell(r, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Range(r, 1, r, headers.Length).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                // Severity carries the colour; it is what the reader scans for.
                var level = ws.Cell(r, 3).Style;
                level.Font.Bold = true;
                switch (log.SeverityLevel)
                {
                    case LogSeverity.Error:
                        level.Font.FontColor = XLColor.DarkRed;
                        level.Fill.BackgroundColor = XLColor.FromArgb(254, 226, 226);
                        break;
                    case LogSeverity.Warning:
                        level.Font.FontColor = XLColor.FromArgb(146, 64, 14);
                        level.Fill.BackgroundColor = XLColor.FromArgb(254, 243, 199);
                        break;
                    default:
                        level.Font.FontColor = XLColor.FromArgb(30, 64, 138);
                        level.Fill.BackgroundColor = XLColor.FromArgb(219, 234, 254);
                        break;
                }

                r++;
            }

            ws.Columns().AdjustToContents();
            ws.Column(2).Width = Math.Max(ws.Column(2).Width, 20);
            ws.Column(4).Width = Math.Max(ws.Column(4).Width, 18);
            ws.Column(6).Width = Math.Max(ws.Column(6).Width, 22);

            ws.Column(7).Width = 70;
            ws.Column(7).Style.Alignment.WrapText = true;

            ws.SheetView.FreezeRows(1);
            if (rows.Count > 0)
            {
                ws.Range(1, 1, r - 1, headers.Length).SetAutoFilter();
            }

            WriteContextSheet(wb, filter, rows);

            string? dir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
            if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);
            wb.SaveAs(outputPath);

            return rows.Count;
        }

        // Records which filter produced the export, so a quiet week reads apart from a narrow filter.
        private static void WriteContextSheet(XLWorkbook wb, SystemLogFilter filter, List<SystemLog> rows)
        {
            var ws = wb.Worksheets.Add("Export Info");
            int r = 1;

            ws.Cell(r, 1).Value = "SYSTEM LOG EXPORT";
            ws.Range(r, 1, r, 3).Merge();
            ws.Cell(r, 1).Style.Font.Bold = true;
            ws.Cell(r, 1).Style.Font.FontSize = 13;
            r += 2;

            void Info(string label, string value)
            {
                ws.Cell(r, 1).Value = label;
                ws.Cell(r, 1).Style.Font.Bold = true;
                ws.Cell(r, 2).Value = ":";
                ws.Cell(r, 3).Value = value;
                r++;
            }

            Info("Exported at", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Info("Exported by", SessionManager.CurrentUser?.Username ?? "-");
            Info("Application", $"AssemblyLineTester {AppInfo.Version}");
            Info("Machine", AppInfo.MachineName);
            Info("Entries exported", rows.Count.ToString());
            r++;

            ws.Cell(r, 1).Value = "FILTER APPLIED";
            ws.Range(r, 1, r, 3).Merge();
            ws.Cell(r, 1).Style.Font.Bold = true;
            r++;

            Info("Date from", filter.DateFrom?.ToString("yyyy-MM-dd") ?? "(any)");
            Info("Date to", filter.DateTo?.ToString("yyyy-MM-dd") ?? "(any)");
            Info("Level", filter.SeverityLevel?.ToString() ?? "All");
            Info("Module", string.IsNullOrWhiteSpace(filter.ModuleName) ? "All" : filter.ModuleName);
            Info("User", string.IsNullOrWhiteSpace(filter.UserName) ? "All" : filter.UserName);
            Info("Action", string.IsNullOrWhiteSpace(filter.ActionName) ? "All" : filter.ActionName);
            Info("Keyword", string.IsNullOrWhiteSpace(filter.Keyword) ? "(none)" : filter.Keyword);
            r++;

            ws.Cell(r, 1).Value = "BREAKDOWN";
            ws.Range(r, 1, r, 3).Merge();
            ws.Cell(r, 1).Style.Font.Bold = true;
            r++;

            foreach (LogSeverity level in Enum.GetValues<LogSeverity>())
            {
                Info(level.ToString(), rows.Count(x => x.SeverityLevel == level).ToString());
            }
            r++;

            foreach (var group in rows.GroupBy(x => x.ModuleName).OrderByDescending(g => g.Count()))
            {
                Info(group.Key, group.Count().ToString());
            }

            ws.Columns(1, 3).AdjustToContents();
            ws.Column(1).Width = Math.Max(ws.Column(1).Width, 20);
        }

        // Every matching entry, newest first, unpaged; the page-size cap belongs to the grid.
        private static List<SystemLog> GetAllMatching(SystemLogFilter filter)
        {
            using var db = new AppDbContext();
            return ApplyFilter(db.SystemLogs.AsQueryable(), filter)
                .OrderByDescending(l => l.Timestamp)
                .ToList();
        }

        // One page of entries plus the total count for the active filter.
        public PagedResult<SystemLog> GetLogs(SystemLogFilter filter)
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            using (var db = new AppDbContext())
            {
                var query = ApplyFilter(db.SystemLogs.AsQueryable(), filter);

                // Counted before Skip/Take, or it would only describe the current page.
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

        // Totals per level and per module for the active filter.
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

        // Shared by GetLogs and GetSummary so the two can never disagree.
        // Distinct module names present in the log, for the filter dropdown.
        public List<string> GetKnownModules()
        {
            using var db = new AppDbContext();
            return db.SystemLogs
                .Select(l => l.ModuleName)
                .Distinct()
                .OrderBy(m => m)
                .ToList();
        }

        private static IQueryable<SystemLog> ApplyFilter(IQueryable<SystemLog> query, SystemLogFilter filter)
        {
            // Timestamps are UTC and the pickers give local dates, so the range is converted first.
            var (fromUtc, toUtc) = AppTime.LocalDateRangeToUtc(filter.DateFrom, filter.DateTo);

            if (fromUtc.HasValue)
            {
                query = query.Where(l => l.Timestamp >= fromUtc.Value);
            }

            if (toUtc.HasValue)
            {
                query = query.Where(l => l.Timestamp < toUtc.Value);
            }

            if (filter.SeverityLevel.HasValue)
            {
                query = query.Where(l => l.SeverityLevel == filter.SeverityLevel.Value);
            }

            if (!string.IsNullOrWhiteSpace(filter.ModuleName))
            {
                query = query.Where(l => l.ModuleName == filter.ModuleName);
            }

            // Partial match, because these two are typed by hand now: an exact comparison would
            // return nothing for "adm" or "delete" and look like the filter is broken.
            if (!string.IsNullOrWhiteSpace(filter.UserName))
            {
                var user = filter.UserName.Trim();
                query = query.Where(l => l.UserName != null && EF.Functions.Like(l.UserName, $"%{user}%"));
            }

            if (!string.IsNullOrWhiteSpace(filter.ActionName))
            {
                var action = filter.ActionName.Trim();
                query = query.Where(l => EF.Functions.Like(l.ActionName, $"%{action}%"));
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

    }
}