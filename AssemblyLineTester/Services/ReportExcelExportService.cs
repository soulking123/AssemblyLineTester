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

            var ws = wb.Worksheets.Add("Summary");
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

            // Sheet 2: one row per net.
            var ws2 = wb.Worksheets.Add("Details");
            int rr = 1;
            var dHdr = new[] { "#", "Net ID", "Harness Group", "From", "To", "Test Type", "Lower", "Upper", "Measure", "Tolerance", "Result" };
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
                ws2.Cell(rr, 1).Value = idx;
                ws2.Cell(rr, 2).Value = d.NetId;
                ws2.Cell(rr, 3).Value = d.HarnessGroup;
                ws2.Cell(rr, 4).Value = d.FromPin;
                ws2.Cell(rr, 5).Value = d.ToPin;
                ws2.Cell(rr, 6).Value = d.TestType;
                // Written as numbers so the columns sort numerically.
                if (d.LowerLimit.HasValue) ws2.Cell(rr, 7).Value = d.LowerLimit.Value; else ws2.Cell(rr, 7).Value = "-";
                if (d.UpperLimit.HasValue) ws2.Cell(rr, 8).Value = d.UpperLimit.Value; else ws2.Cell(rr, 8).Value = "-";
                ws2.Cell(rr, 9).Value = d.Measured;
                ws2.Cell(rr, 10).Value = d.Tolerance;
                ws2.Cell(rr, 11).Value = d.Result;

                ws2.Range(rr, 1, rr, 11).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                ws2.Range(rr, 1, rr, 11).Style.Alignment.WrapText = false;

                if (string.Equals(d.Result, "PASS", StringComparison.OrdinalIgnoreCase))
                {
                    ws2.Cell(rr, 11).Style.Font.FontColor = XLColor.DarkGreen;
                }
                else if (string.Equals(d.Result, "OPEN", StringComparison.OrdinalIgnoreCase))
                {
                    ws2.Cell(rr, 11).Style.Font.FontColor = XLColor.DarkOrange;
                }
                else
                {
                    ws2.Cell(rr, 11).Style.Font.FontColor = XLColor.DarkRed;
                }

                rr++;
                idx++;
            }

            ws2.Columns().AdjustToContents();
            ws2.Column(2).Width = Math.Max(ws2.Column(2).Width, 12);
            ws2.Column(3).Width = Math.Max(ws2.Column(3).Width, 24);
            ws2.Column(6).Width = Math.Max(ws2.Column(6).Width, 18);
            ws2.Column(9).Width = Math.Max(ws2.Column(9).Width, 14);

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
