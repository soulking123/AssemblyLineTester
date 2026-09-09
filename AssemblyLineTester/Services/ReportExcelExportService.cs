using System;
using System.IO;
using ClosedXML.Excel;
using System.Linq;
using AssemblyLineTester.Models.Dto;

namespace AssemblyLineTester.Services
{
    // Renders a saved report as .xlsx; the counterpart to ReportPdfExportService.
    public class ReportExcelExportService
    {
        public void ExportReportToExcel(int reportId, string outputPath)
        {
            var detail = new TestReportService().GetReportDetail(reportId);
            if (detail is null) throw new InvalidOperationException("Report not found.");

            using var wb = new XLWorkbook();

            var ws = wb.Worksheets.Add("General Information");
            int r = 1;

            ws.Cell(r, 1).Value = "GENERAL INFORMATION";
            ws.Range(r, 1, r, 4).Merge();
            ws.Cell(r, 1).Style.Font.Bold = true;
            r += 2;

            void AddInfo(string label, string? value)
            {
                ws.Cell(r, 1).Value = label;
                ws.Cell(r, 2).Value = ":";
                ws.Cell(r, 3).Value = value ?? "-";
                ws.Range(r, 1, r, 1).Style.Font.Bold = true;
                ws.Range(r, 1, r, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                ws.Range(r, 3, r, 3).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                r++;
            }

            AddInfo("Product Model", detail.ProductModel);
            AddInfo("Part Number", detail.PartNumber);
            AddInfo("Serial Number", detail.SerialNumber);
            AddInfo("Operator", detail.Operator);
            AddInfo("Result", detail.OverallResult);

            int notesRow = r;
            AddInfo("Notes", string.IsNullOrWhiteSpace(detail.Notes) ? "-" : detail.Notes.Trim());
            ws.Range(notesRow, 3, notesRow, 5).Merge();
            ws.Cell(notesRow, 3).Style.Alignment.WrapText = true;
            ws.Cell(notesRow, 3).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;

            r += 1;

            ws.Cell(r, 1).Value = "TEST RESULT SUMMARY";
            ws.Range(r, 1, r, 5).Merge();
            ws.Cell(r, 1).Style.Font.Bold = true;
            r += 1;

            var hdr = new[] { "Test Type", "Total", "Pass", "Fail", "Pass Rate" };
            for (int c = 0; c < hdr.Length; c++)
            {
                ws.Cell(r, c + 1).Value = hdr[c];
                ws.Cell(r, c + 1).Style.Font.Bold = true;
                ws.Cell(r, c + 1).Style.Fill.BackgroundColor = XLColor.FromArgb(230, 230, 230);
                ws.Cell(r, c + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell(r, c + 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            }
            r += 1;

            int sumTotal = 0, sumPass = 0, sumFail = 0;
            foreach (var s in detail.TypeSummaries)
            {
                ws.Cell(r, 1).Value = s.TestType;
                ws.Cell(r, 2).Value = s.Total;
                ws.Cell(r, 3).Value = s.Pass;
                ws.Cell(r, 4).Value = s.Fail;
                ws.Cell(r, 5).Value = s.PassRatePercent / 100.0;

                ws.Cell(r, 5).Style.NumberFormat.Format = "0.00%";

                ws.Range(r, 2, r, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Range(r, 2, r, 5).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                if (s.Fail > 0)
                {
                    ws.Cell(r, 4).Style.Font.FontColor = XLColor.DarkRed;
                }

                sumTotal += s.Total;
                sumPass += s.Pass;
                sumFail += s.Fail;
                r++;
            }

            ws.Cell(r, 1).Value = "Total";
            ws.Cell(r, 2).Value = sumTotal;
            ws.Cell(r, 3).Value = sumPass;
            ws.Cell(r, 4).Value = sumFail;
            ws.Cell(r, 5).Value = (sumTotal == 0) ? 1.0 : (double)sumPass / sumTotal;
            ws.Cell(r, 5).Style.NumberFormat.Format = "0.00%";
            ws.Range(r, 1, r, 5).Style.Fill.BackgroundColor = XLColor.FromArgb(230, 230, 230);
            ws.Range(r, 1, r, 5).Style.Font.Bold = true;
            ws.Range(r, 2, r, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            r += 2;

            ws.Columns(1, 5).AdjustToContents();
            ws.Column(1).Width = Math.Max(ws.Column(1).Width, 20);
            ws.Column(2).Width = Math.Max(ws.Column(2).Width, 3);
            ws.Column(3).Width = Math.Max(ws.Column(3).Width, 12);

            // Sheet 2: the Test Summary panel from the Test Execution screen, line for line.
            var wsSummary = wb.Worksheets.Add("Test Summary");
            int sr = 1;

            if (!string.IsNullOrWhiteSpace(detail.ErrorSummary))
            {
                var lines = detail.ErrorSummary.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                foreach (var line in lines)
                {
                    string trimmed = line.Trim();
                    if (string.IsNullOrEmpty(trimmed))
                    {
                        sr++;
                        continue;
                    }

                    if (trimmed.StartsWith("TEST RESULT", StringComparison.OrdinalIgnoreCase))
                    {
                        bool isPass = trimmed.EndsWith("PASS", StringComparison.OrdinalIgnoreCase);
                        wsSummary.Cell(sr, 1).Value = trimmed;
                        wsSummary.Cell(sr, 1).Style.Font.Bold = true;
                        wsSummary.Cell(sr, 1).Style.Font.FontColor = isPass ? XLColor.DarkGreen : XLColor.DarkRed;
                    }
                    else if (trimmed.Equals("SHORT CONNECTION ERRORS", StringComparison.OrdinalIgnoreCase) ||
                             trimmed.Equals("TEST ERRORS", StringComparison.OrdinalIgnoreCase))
                    {
                        wsSummary.Cell(sr, 1).Value = trimmed;
                        wsSummary.Cell(sr, 1).Style.Font.Bold = true;
                    }
                    else if (trimmed.StartsWith("Short ", StringComparison.OrdinalIgnoreCase))
                    {
                        wsSummary.Cell(sr, 1).Value = trimmed;
                        wsSummary.Cell(sr, 1).Style.Font.FontColor = XLColor.DarkRed;
                    }
                    else
                    {
                        string displayLine = trimmed
                            .Replace("✔", "[OK] ")
                            .Replace("✖", "[X] ");

                        bool isFail = displayLine.Contains("[X]") || trimmed.Contains("FAIL", StringComparison.OrdinalIgnoreCase);
                        wsSummary.Cell(sr, 1).Value = displayLine;
                        if (isFail) wsSummary.Cell(sr, 1).Style.Font.FontColor = XLColor.DarkRed;
                    }

                    sr++;
                }
            }
            else
            {
                wsSummary.Cell(sr, 1).Value = $"TEST RESULT = {detail.OverallResult}";
                wsSummary.Cell(sr, 1).Style.Font.Bold = true;
                wsSummary.Cell(sr, 1).Style.Font.FontColor =
                    detail.OverallResult?.ToUpperInvariant() == "PASS" ? XLColor.DarkGreen : XLColor.DarkRed;
                sr += 2;

                foreach (var s in detail.TypeSummaries)
                {
                    bool allPassed = s.Fail == 0;
                    wsSummary.Cell(sr, 1).Value = $"[{(allPassed ? "OK" : "X")}]  {s.TestType} ({s.Pass}/{s.Total})";
                    if (!allPassed) wsSummary.Cell(sr, 1).Style.Font.FontColor = XLColor.DarkRed;
                    sr++;
                }

                sr += 1;
                wsSummary.Cell(sr, 1).Value = "TEST ERRORS";
                wsSummary.Cell(sr, 1).Style.Font.Bold = true;
                sr += 2;

                var failedRows = detail.Details.Where(d => !string.IsNullOrWhiteSpace(d.ErrorDetail)).ToList();
                if (failedRows.Count == 0)
                {
                    wsSummary.Cell(sr, 1).Value = "No errors were recorded for this test run.";
                }
                else
                {
                    foreach (var d in failedRows)
                    {
                        wsSummary.Cell(sr, 1).Value = $"{d.NetId} ({d.TestType}) - {d.ErrorDetail}";
                        wsSummary.Cell(sr, 1).Style.Font.FontColor = XLColor.DarkRed;
                        sr++;
                    }
                }
            }

            wsSummary.Column(1).AdjustToContents();

            // Sheet 2: one row per net.
            var ws2 = wb.Worksheets.Add("Details");
            int rr = 1;
            // "#" and "Result" frame the sheet; the columns between them follow the Settings tick list.
            var columns = TestSettingService.GetEnabledResultColumns();
            var dHdr = new[] { "#" }.Concat(columns).Concat(new[] { "Result" }).ToArray();
            for (int c = 0; c < dHdr.Length; c++)
            {
                ws2.Cell(rr, c + 1).Value = dHdr[c];
                ws2.Cell(rr, c + 1).Style.Font.Bold = true;
                ws2.Cell(rr, c + 1).Style.Fill.BackgroundColor = XLColor.FromArgb(230, 230, 230);
                ws2.Cell(rr, c + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws2.Cell(rr, c + 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            }
            rr++;

            int idx = 1;
            foreach (var d in detail.Details)
            {
                int resultCol = dHdr.Length;
                ws2.Cell(rr, 1).Value = idx;

                for (int c = 0; c < columns.Count; c++)
                {
                    var cell = ws2.Cell(rr, c + 2);
                    // Limits stay numeric so the columns sort numerically; dual values (diode) or non-numeric stay formatted text.
                    switch (columns[c])
                    {
                        case "Lower Limit" when d.LowerLimit.HasValue && !d.LowerLimit2.HasValue: cell.Value = d.LowerLimit.Value; break;
                        case "Upper Limit" when d.UpperLimit.HasValue && !d.UpperLimit2.HasValue: cell.Value = d.UpperLimit.Value; break;
                        case "Expected Value" when d.ExpectedValue.HasValue && !d.ExpectedValue2.HasValue: cell.Value = d.ExpectedValue.Value; break;
                        default: cell.Value = d.Column(columns[c]); break;
                    }
                }

                ws2.Cell(rr, resultCol).Value = d.Result;

                ws2.Range(rr, 1, rr, resultCol).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                ws2.Range(rr, 1, rr, resultCol).Style.Alignment.WrapText = false;

                if (string.Equals(d.Result, "PASS", StringComparison.OrdinalIgnoreCase))
                {
                    ws2.Cell(rr, resultCol).Style.Font.FontColor = XLColor.DarkGreen;
                }
                else if (string.Equals(d.Result, "OPEN", StringComparison.OrdinalIgnoreCase))
                {
                    ws2.Cell(rr, resultCol).Style.Font.FontColor = XLColor.DarkOrange;
                }
                else
                {
                    ws2.Cell(rr, resultCol).Style.Font.FontColor = XLColor.DarkRed;
                }

                rr++;
                idx++;
            }

            ws2.Columns().AdjustToContents();
            for (int c = 2; c <= dHdr.Length; c++)
            {
                ws2.Column(c).Width = Math.Max(ws2.Column(c).Width, 12);
            }

            ws2.SheetView.FreezeRows(1);
            ws.SheetView.FreezeRows(1);

            ws2.Range(1, 1, rr - 1, dHdr.Length).SetAutoFilter();

            var dir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir)) Directory.CreateDirectory(dir);
            wb.SaveAs(outputPath);

            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Info, "Test Report", "Export Excel",
                $"Report {detail.ReportNumber} exported to Excel ({detail.Details.Count} detail rows).");
        }
    }
}
