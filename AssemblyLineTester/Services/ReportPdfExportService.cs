using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AssemblyLineTester.Models.Dto;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;

namespace AssemblyLineTester.Services
{
    // Renders a saved report as PDF; the counterpart to ReportExcelExportService.
    public class ReportPdfExportService
    {
        private const double Margin = 40;

        // Space kept clear at the foot of a page so rows never run into the footer.
        private const double FooterReserve = 40;

        public void ExportReportToPdf(int reportId, string outputPath)
        {
            var detail = new TestReportService().GetReportDetail(reportId);
            if (detail is null) throw new InvalidOperationException("Report not found.");

            string logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "cat_logo.png");

            using PdfDocument doc = new();

            DrawSummaryPage(doc, detail, logoPath);
            DrawTestSummaryPage(doc, detail, logoPath);
            DrawDetailPages(doc, detail, logoPath);

            int totalPages = StampHeadersAndFooters(doc, detail, logoPath);

            var dir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir)) Directory.CreateDirectory(dir);
            doc.Save(outputPath);

            new SystemLogService().Log(
                Models.SystemLog.LogSeverity.Info, "Test Report", "Export PDF",
                $"Report {detail.ReportNumber} exported to PDF ({totalPages} pages).");
        }

        // Page 1: general information, summary table and result box, portrait.
        private static void DrawSummaryPage(PdfDocument doc, ReportDetailDto detail, string logoPath)
        {
            var page = doc.AddPage();
            page.Size = PdfSharp.PageSize.A4;
            page.Orientation = PdfSharp.PageOrientation.Portrait;
            using XGraphics gfx = XGraphics.FromPdfPage(page);

            var font = new XFont("Arial", 9, XFontStyle.Regular);
            var fontBold = new XFont("Arial", 9, XFontStyle.Bold);

            double y = DrawPageHeader(gfx, page, detail, 0, 0, logoPath);

            y = DrawGeneralInformation(gfx, page, detail, font, fontBold, y);
            y = DrawTypeSummaryTable(gfx, detail, font, fontBold, y);
            y = DrawOverallResultBox(gfx, page, detail, y);
            DrawNotes(gfx, page, detail, font, fontBold, y);
        }

        // Page 2: the Test Summary panel from the Test Execution screen, line for line.
        private static void DrawTestSummaryPage(PdfDocument doc, ReportDetailDto detail, string logoPath)
        {
            var page = doc.AddPage();
            page.Size = PdfSharp.PageSize.A4;
            page.Orientation = PdfSharp.PageOrientation.Portrait;
            using XGraphics gfx = XGraphics.FromPdfPage(page);

            var font = new XFont("Arial", 10, XFontStyle.Regular);
            var fontBold = new XFont("Arial", 10, XFontStyle.Bold);

            double y = DrawPageHeader(gfx, page, detail, 0, 0, logoPath);

            var titleBg = new XSolidBrush(XColor.FromArgb(40, 230, 230, 230));
            gfx.DrawRectangle(titleBg, Margin, y - 2, 200, 18);
            gfx.DrawString("TEST SUMMARY", fontBold, XBrushes.Black, new XRect(Margin, y, 400, 16), XStringFormats.TopLeft);
            y += 26;

            double lineH = 18;
            double width = page.Width - Margin * 2;
            double bottom = page.Height - Margin - FooterReserve;

            if (!string.IsNullOrWhiteSpace(detail.ErrorSummary))
            {
                var lines = detail.ErrorSummary.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                bool inErrorSection = false;
                foreach (var line in lines)
                {
                    if (y + lineH > bottom) break;

                    string trimmed = line.Trim();
                    if (string.IsNullOrEmpty(trimmed))
                    {
                        y += lineH * 0.8;
                        continue;
                    }

                    if (trimmed.StartsWith("TEST RESULT", StringComparison.OrdinalIgnoreCase))
                    {
                        bool isPass = trimmed.EndsWith("PASS", StringComparison.OrdinalIgnoreCase);
                        gfx.DrawString(trimmed, fontBold, isPass ? XBrushes.DarkGreen : XBrushes.DarkRed,
                            new XRect(Margin, y, width, lineH), XStringFormats.TopLeft);
                    }
                    else if (trimmed.Equals("SHORT CONNECTION ERRORS", StringComparison.OrdinalIgnoreCase) ||
                             trimmed.Equals("TEST ERRORS", StringComparison.OrdinalIgnoreCase))
                    {
                        inErrorSection = true;
                        gfx.DrawString(trimmed, fontBold, XBrushes.Black,
                            new XRect(Margin, y, width, lineH), XStringFormats.TopLeft);
                    }
                    else if (inErrorSection || trimmed.StartsWith("Short ", StringComparison.OrdinalIgnoreCase))
                    {
                        gfx.DrawString(trimmed, font, XBrushes.DarkRed,
                            new XRect(Margin, y, width, lineH), XStringFormats.TopLeft);
                    }
                    else
                    {
                        // Map Unicode tick/cross to [OK] / [X] so PdfSharp Arial font renders cleanly
                        string displayLine = trimmed
                            .Replace("✔", "[OK] ")
                            .Replace("✖", "[X] ");

                        bool isFail = displayLine.Contains("[X]") || trimmed.Contains("FAIL", StringComparison.OrdinalIgnoreCase);
                        gfx.DrawString(displayLine, font, isFail ? XBrushes.DarkRed : XBrushes.Black,
                            new XRect(Margin, y, width, lineH), XStringFormats.TopLeft);
                    }

                    y += lineH;
                }
                return;
            }

            gfx.DrawString($"TEST RESULT = {detail.OverallResult}", fontBold,
                detail.OverallResult?.ToUpperInvariant() == "PASS" ? XBrushes.DarkGreen : XBrushes.DarkRed,
                new XRect(Margin, y, width, lineH), XStringFormats.TopLeft);
            y += lineH * 2;

            // "OK"/"X" rather than the screen's tick and cross: PdfSharp maps those glyphs through
            // the base Arial encoding and drops them, which would leave the counts unlabelled.
            foreach (var s in detail.TypeSummaries)
            {
                bool allPassed = s.Fail == 0;
                gfx.DrawString($"[{(allPassed ? "OK" : "X")}]  {s.TestType} ({s.Pass}/{s.Total})", font,
                    allPassed ? XBrushes.Black : XBrushes.DarkRed,
                    new XRect(Margin, y, width, lineH), XStringFormats.TopLeft);
                y += lineH;
            }

            y += lineH;
            gfx.DrawString("TEST ERRORS", fontBold, XBrushes.Black, new XRect(Margin, y, width, lineH), XStringFormats.TopLeft);
            y += lineH * 1.5;

            var failed = detail.Details.Where(d => !string.IsNullOrWhiteSpace(d.ErrorDetail)).ToList();
            if (failed.Count == 0)
            {
                gfx.DrawString("No errors were recorded for this test run.", font, XBrushes.Black,
                    new XRect(Margin, y, width, lineH), XStringFormats.TopLeft);
                return;
            }

            foreach (var d in failed)
            {
                if (y + lineH > bottom)
                {
                    gfx.DrawString($"... and {failed.Count - failed.IndexOf(d)} more, see the detail pages.",
                        font, XBrushes.Black, new XRect(Margin, y, width, lineH), XStringFormats.TopLeft);
                    break;
                }

                gfx.DrawString($"{d.NetId} ({d.TestType}) - {d.ErrorDetail}", font, XBrushes.DarkRed,
                    new XRect(Margin, y, width, lineH), XStringFormats.TopLeft);
                y += lineH;
            }
        }

        // Operator notes as a wrapped paragraph under the result box.
        private static void DrawNotes(XGraphics gfx, PdfPage page, ReportDetailDto detail, XFont font, XFont fontBold, double y)
        {
            gfx.DrawString("NOTES", fontBold, XBrushes.Black, new XRect(Margin, y, 400, 16), XStringFormats.TopLeft);
            y += 18;

            double width = page.Width - Margin * 2;
            double height = Math.Max(0, page.Height - Margin - FooterReserve - y);
            if (height <= 0) return;

            string text = string.IsNullOrWhiteSpace(detail.Notes) ? "-" : detail.Notes.Trim();

            var formatter = new XTextFormatter(gfx) { Alignment = XParagraphAlignment.Left };
            formatter.DrawString(text, font, XBrushes.Black, new XRect(Margin, y, width, height), XStringFormats.TopLeft);
        }

        private static double DrawGeneralInformation(XGraphics gfx, PdfPage page, ReportDetailDto detail, XFont font, XFont fontBold, double y)
        {
            var headerTitleBg = new XSolidBrush(XColor.FromArgb(40, 230, 230, 230));
            gfx.DrawRectangle(headerTitleBg, Margin, y - 2, 200, 18);
            gfx.DrawString("GENERAL INFORMATION", fontBold, XBrushes.Black, new XRect(Margin, y, 400, 16), XStringFormats.TopLeft);
            y += 18;

            double infoStartY = y;
            double infoTableWidth = page.Width - Margin * 2;
            double infoRowH = 20;

            var infoRows = new List<(string Label, string Value)>
            {
                ("Product Model", detail.ProductModel),
                ("Part Number", detail.PartNumber),
                ("Serial Number", detail.SerialNumber),
                ("Operator", detail.Operator),
                ("Result", detail.OverallResult)
            };

            double labelColW = 140;
            double colonColW = 12;
            double valueX = Margin + labelColW + colonColW + 6;
            double valueW = infoTableWidth - labelColW - colonColW - 6;

            var infoBg = new XSolidBrush(XColor.FromArgb(12, 200, 200, 200));
            double infoTableH = infoRows.Count * infoRowH;
            gfx.DrawRectangle(infoBg, Margin, infoStartY, infoTableWidth, infoTableH);

            double curY = infoStartY;
            foreach (var r in infoRows)
            {
                gfx.DrawString(r.Label, fontBold, XBrushes.Black, new XRect(Margin + 6, curY, labelColW - 6, infoRowH), XStringFormats.CenterLeft);
                gfx.DrawString(":", fontBold, XBrushes.Black, new XRect(Margin + labelColW, curY, colonColW, infoRowH), XStringFormats.Center);

                XBrush valBrush = XBrushes.Black;
                if (string.Equals(r.Label, "Result", StringComparison.OrdinalIgnoreCase))
                {
                    if (string.Equals(r.Value, "PASS", StringComparison.OrdinalIgnoreCase)) valBrush = XBrushes.DarkGreen;
                    else if (string.Equals(r.Value, "FAIL", StringComparison.OrdinalIgnoreCase) || string.Equals(r.Value, "OPEN", StringComparison.OrdinalIgnoreCase)) valBrush = XBrushes.DarkRed;
                }

                gfx.DrawString(r.Value ?? "-", font, valBrush, new XRect(valueX, curY, valueW, infoRowH), XStringFormats.CenterLeft);
                curY += infoRowH;
            }

            return infoStartY + infoTableH + 12;
        }

        private static double DrawTypeSummaryTable(XGraphics gfx, ReportDetailDto detail, XFont font, XFont fontBold, double y)
        {
            gfx.DrawString("TEST RESULT SUMMARY", fontBold, XBrushes.Black, new XRect(Margin, y, 400, 16), XStringFormats.TopLeft);
            y += 18;

            double[] colWidths = { 220, 60, 60, 60, 90 };
            double rowH = 20;
            var hdrBg = new XSolidBrush(XColor.FromArgb(40, 230, 230, 230));

            double tx = Margin;
            string[] headers = { "Test Type", "Total", "Pass", "Fail", "Pass Rate" };
            for (int i = 0; i < headers.Length; i++)
            {
                gfx.DrawRectangle(hdrBg, tx, y, colWidths[i], rowH);
                gfx.DrawRectangle(XPens.LightGray, tx, y, colWidths[i], rowH);
                if (i == 0)
                    gfx.DrawString(headers[i], fontBold, XBrushes.Black, new XRect(tx + 6, y, colWidths[i] - 10, rowH), XStringFormats.CenterLeft);
                else
                    gfx.DrawString(headers[i], fontBold, XBrushes.Black, new XRect(tx, y, colWidths[i], rowH), XStringFormats.Center);
                tx += colWidths[i];
            }
            y += rowH;

            int sumTotal = 0, sumPass = 0, sumFail = 0;
            foreach (var s in detail.TypeSummaries)
            {
                tx = Margin;

                gfx.DrawRectangle(XPens.LightGray, tx, y, colWidths[0], rowH);
                gfx.DrawString(Ellipsize(gfx, s.TestType, font, colWidths[0] - 12), font, XBrushes.Black,
                    new XRect(tx + 6, y, colWidths[0] - 10, rowH), XStringFormats.CenterLeft);
                tx += colWidths[0];

                gfx.DrawRectangle(XPens.LightGray, tx, y, colWidths[1], rowH);
                gfx.DrawString(s.Total.ToString(), font, XBrushes.Black, new XRect(tx, y, colWidths[1], rowH), XStringFormats.Center);
                sumTotal += s.Total;
                tx += colWidths[1];

                gfx.DrawRectangle(XPens.LightGray, tx, y, colWidths[2], rowH);
                gfx.DrawString(s.Pass.ToString(), font, XBrushes.Black, new XRect(tx, y, colWidths[2], rowH), XStringFormats.Center);
                sumPass += s.Pass;
                tx += colWidths[2];

                gfx.DrawRectangle(XPens.LightGray, tx, y, colWidths[3], rowH);
                gfx.DrawString(s.Fail.ToString(), font, s.Fail > 0 ? XBrushes.DarkRed : XBrushes.Black,
                    new XRect(tx, y, colWidths[3], rowH), XStringFormats.Center);
                sumFail += s.Fail;
                tx += colWidths[3];

                gfx.DrawRectangle(XPens.LightGray, tx, y, colWidths[4], rowH);
                gfx.DrawString($"{s.PassRatePercent:0.00}%", font, XBrushes.Black, new XRect(tx, y, colWidths[4], rowH), XStringFormats.Center);

                y += rowH;
            }

            tx = Margin;
            for (int i = 0; i < colWidths.Length; i++)
            {
                gfx.DrawRectangle(hdrBg, tx, y, colWidths[i], rowH);
                gfx.DrawRectangle(XPens.LightGray, tx, y, colWidths[i], rowH);
                tx += colWidths[i];
            }

            tx = Margin;
            gfx.DrawString("Total", fontBold, XBrushes.Black, new XRect(tx + 6, y, colWidths[0] - 10, rowH), XStringFormats.CenterLeft);
            tx += colWidths[0];
            gfx.DrawString(sumTotal.ToString(), fontBold, XBrushes.Black, new XRect(tx, y, colWidths[1], rowH), XStringFormats.Center);
            tx += colWidths[1];
            gfx.DrawString(sumPass.ToString(), fontBold, XBrushes.Black, new XRect(tx, y, colWidths[2], rowH), XStringFormats.Center);
            tx += colWidths[2];
            gfx.DrawString(sumFail.ToString(), fontBold, sumFail > 0 ? XBrushes.DarkRed : XBrushes.Black,
                new XRect(tx, y, colWidths[3], rowH), XStringFormats.Center);
            tx += colWidths[3];
            double totalPassRate = sumTotal == 0 ? 100.0 : Math.Round(sumPass * 100.0 / sumTotal, 2);
            gfx.DrawString($"{totalPassRate:0.00}%", fontBold, XBrushes.Black, new XRect(tx, y, colWidths[4], rowH), XStringFormats.Center);

            return y + rowH + 12;
        }

        private static double DrawOverallResultBox(XGraphics gfx, PdfPage page, ReportDetailDto detail, double y)
        {
            double boxW = 280;
            double boxH = 60;
            double boxX = (page.Width - boxW) / 2;

            bool isPass = string.Equals(detail.OverallResult, "PASS", StringComparison.OrdinalIgnoreCase);
            XColor borderColor = isPass ? XColors.DarkGreen : XColors.DarkRed;
            var fillColor = isPass ? XColor.FromArgb(20, XColors.Green) : XColor.FromArgb(20, XColors.Red);

            gfx.DrawRectangle(new XPen(borderColor, 2), new XSolidBrush(fillColor), boxX, y, boxW, boxH);

            var resultFont = new XFont("Arial", 22, XFontStyle.Bold);
            var measuredSize = gfx.MeasureString(detail.OverallResult, resultFont);
            double textY = y + Math.Max(0, (boxH - measuredSize.Height) / 2.0);
            gfx.DrawString(detail.OverallResult, resultFont, new XSolidBrush(borderColor),
                new XRect(boxX, textY, boxW, measuredSize.Height), XStringFormats.Center);

            return y + boxH + 20;
        }

        // Page 2 onwards: detail rows in landscape, spilling onto as many pages as needed.
        private static void DrawDetailPages(PdfDocument doc, ReportDetailDto detail, string logoPath)
        {
            var font = new XFont("Arial", 8, XFontStyle.Regular);
            var fontBold = new XFont("Arial", 8, XFontStyle.Bold);
            double rowH = 18;

            var columns = TestSettingService.GetEnabledResultColumns();

            var page = AddDetailPage(doc);
            XGraphics g = XGraphics.FromPdfPage(page);
            double y = DrawDetailPageTop(g, page, detail, logoPath, fontBold, rowH, columns);

            bool alt = false;
            foreach (var d in detail.Details)
            {
                if (y + rowH > page.Height - Margin - FooterReserve)
                {
                    // PdfSharp refuses two live XGraphics on one page, so release before opening.
                    g.Dispose();
                    page = AddDetailPage(doc);
                    g = XGraphics.FromPdfPage(page);
                    y = DrawDetailPageTop(g, page, detail, logoPath, fontBold, rowH, columns);
                }

                y = DrawDetailRow(g, page, d, font, fontBold, y, rowH, ref alt, columns);
            }

            g.Dispose();
        }

        private static PdfPage AddDetailPage(PdfDocument doc)
        {
            var page = doc.AddPage();
            page.Size = PdfSharp.PageSize.A4;
            page.Orientation = PdfSharp.PageOrientation.Landscape;
            return page;
        }

        // Header, section title and column headings. Returns the y the first row starts at.
        private static double DrawDetailPageTop(XGraphics g, PdfPage page, ReportDetailDto detail, string logoPath, XFont fontBold, double rowH, List<string> columns)
        {
            double y = DrawPageHeader(g, page, detail, 0, 0, logoPath);

            g.DrawString("DETAILED TEST RESULTS", fontBold, XBrushes.Black, new XRect(Margin, y, 400, 16), XStringFormats.TopLeft);
            y += 18;

            double[] cols = DetailColumnWidths(page, columns);
            string[] headers = columns.Concat(new[] { "Result" }).ToArray();
            var hdrBg = new XSolidBrush(XColor.FromArgb(28, 230, 230, 230));

            double headerH = 22;
            double x = Margin;
            for (int i = 0; i < headers.Length; i++)
            {
                g.DrawRectangle(hdrBg, x, y, cols[i], headerH);
                g.DrawRectangle(XPens.LightGray, x, y, cols[i], headerH);
                DrawHeaderCell(g, headers[i], fontBold, new XRect(x + 2, y, cols[i] - 4, headerH));
                x += cols[i];
            }

            return y + headerH;
        }

        private static void DrawHeaderCell(XGraphics g, string text, XFont font, XRect rect)
        {
            double textW = g.MeasureString(text, font).Width;
            if (textW <= rect.Width || !text.Contains(' '))
            {
                g.DrawString(Ellipsize(g, text, font, rect.Width), font, XBrushes.Black, rect, XStringFormats.CenterLeft);
            }
            else
            {
                int lastSpace = text.LastIndexOf(' ');
                string line1 = text.Substring(0, lastSpace).Trim();
                string line2 = text.Substring(lastSpace + 1).Trim();

                double lineH = font.Height;
                double topY = rect.Y + (rect.Height - lineH * 2) / 2.0;

                g.DrawString(Ellipsize(g, line1, font, rect.Width), font, XBrushes.Black,
                    new XRect(rect.X, topY, rect.Width, lineH), XStringFormats.CenterLeft);
                g.DrawString(Ellipsize(g, line2, font, rect.Width), font, XBrushes.Black,
                    new XRect(rect.X, topY + lineH, rect.Width, lineH), XStringFormats.CenterLeft);
            }
        }

        // Column widths as fractions of the printable width, so they follow the page size.
        // Relative widths per configurable column; anything unlisted gets an even share.
        private static readonly Dictionary<string, double> DetailColumnWeights = new()
        {
            ["Net-ID"] = 0.7,
            ["Harness Group"] = 0.8,
            ["From"] = 0.5,
            ["Net From Label"] = 0.8,
            ["To"] = 0.5,
            ["Net To Label"] = 0.8,
            ["Test Type"] = 0.9,
            ["Color"] = 0.55,
            ["Gauge"] = 0.6,
            ["Lower Limit"] = 1.3,
            ["Upper Limit"] = 1.3,
            ["Expected Value"] = 1.3,
            ["Tolerance"] = 1.05,
            ["Measurement"] = 1.45
        };

        // The trailing entry is the Result column, which is always drawn.
        private static double[] DetailColumnWidths(PdfPage page, List<string> columns)
        {
            double w = page.Width - Margin * 2;

            var weights = columns
                .Select(c => DetailColumnWeights.TryGetValue(c, out double weight) ? weight : 1.0)
                .ToList();
            weights.Add(0.7);

            double total = weights.Sum();
            return weights.Select(weight => w * weight / total).ToArray();
        }

        private static double DrawDetailRow(XGraphics g, PdfPage page, TestDetailDto d, XFont font, XFont fontBold, double y, double rowH, ref bool alt, List<string> columns)
        {
            double[] cols = DetailColumnWidths(page, columns);

            bool isPass = string.Equals(d.Result, "PASS", StringComparison.OrdinalIgnoreCase);
            bool isOpen = string.Equals(d.Result, "OPEN", StringComparison.OrdinalIgnoreCase);

            if (!isPass && !isOpen)
            {
                g.DrawRectangle(new XSolidBrush(XColor.FromArgb(40, 255, 200, 200)), Margin, y, page.Width - Margin * 2, rowH);
            }
            else if (isOpen)
            {
                g.DrawRectangle(new XSolidBrush(XColor.FromArgb(30, 255, 230, 200)), Margin, y, page.Width - Margin * 2, rowH);
            }
            else
            {
                if (alt) g.DrawRectangle(new XSolidBrush(XColor.FromArgb(8, 200, 200, 200)), Margin, y, page.Width - Margin * 2, rowH);
                alt = !alt;
            }

            double x = Margin;

            void Cell(int i, string? text, XStringFormat align)
            {
                g.DrawRectangle(XPens.LightGray, x, y, cols[i], rowH);
                var rect = align == XStringFormats.CenterLeft
                    ? new XRect(x + 2, y, cols[i] - 4, rowH)
                    : new XRect(x, y + 2, cols[i], rowH);
                g.DrawString(Ellipsize(g, text, font, cols[i] - 4), font, XBrushes.Black, rect, align);
                x += cols[i];
            }

            for (int i = 0; i < columns.Count; i++)
            {
                var align = columns[i] is "Harness Group" or "From" or "To" or "Test Type" or "Net From Label" or "Net To Label" or "Color" or "Gauge"
                    ? XStringFormats.CenterLeft
                    : XStringFormats.Center;
                Cell(i, d.Column(columns[i]), align);
            }

            var resultRect = new XRect(x, y, cols[columns.Count], rowH);
            XColor resultFill;
            XBrush resultText;
            if (isPass)
            {
                resultFill = XColor.FromArgb(200, 220, 255, 220);
                resultText = new XSolidBrush(XColor.FromArgb(180, XColors.DarkGreen));
            }
            else if (isOpen)
            {
                resultFill = XColor.FromArgb(160, XColors.Orange);
                resultText = XBrushes.DarkOrange;
            }
            else
            {
                resultFill = XColor.FromArgb(40, 255, 200, 200);
                resultText = XBrushes.DarkRed;
            }

            g.DrawRectangle(new XSolidBrush(resultFill), resultRect);
            g.DrawRectangle(XPens.LightGray, x, y, cols[columns.Count], rowH);
            g.DrawString(d.Result, fontBold, resultText, resultRect, XStringFormats.Center);

            return y + rowH;
        }


        // Called twice per page: once during layout, once at the end to stamp "Page n of m".
        private static double DrawPageHeader(XGraphics g, PdfPage page, ReportDetailDto detail, int pageIndex, int totalPages, string logoPath)
        {
            double y = Margin;
            double logoH = 45;
            var titleFont = new XFont("Arial", 14, XFontStyle.Bold);
            var metaFont = new XFont("Arial", 10, XFontStyle.Bold);

            if (File.Exists(logoPath))
            {
                try
                {
                    using var img = XImage.FromFile(logoPath);
                    double imgPointW = img.PixelWidth * 72.0 / (img.HorizontalResolution <= 0 ? 96.0 : img.HorizontalResolution);
                    double imgPointH = img.PixelHeight * 72.0 / (img.VerticalResolution <= 0 ? 96.0 : img.VerticalResolution);
                    double scale = logoH / imgPointH;
                    double drawW = imgPointW * scale;
                    double drawH = imgPointH * scale;
                    g.DrawImage(img, Margin, y, drawW, drawH);
                    logoH = drawH;
                }
                catch (Exception ex)
                {
                    // A missing logo must not stop the export, but it explains unbranded PDFs.
                    FileLogService.Warn("ReportPdfExportService.DrawPageHeader",
                        $"Logo '{logoPath}' could not be drawn: {ex.Message}");
                }
            }

            g.DrawString("TEST REPORT", titleFont, XBrushes.Black, new XRect(Margin, y + logoH + 6, 400, 26), XStringFormats.TopLeft);

            if (totalPages > 0)
            {
                double metaWidth = 220;
                double metaX = page.Width - Margin - metaWidth;
                double labelW = 110;
                double colonW = 12;
                double valueX = metaX + labelW + colonW + 6;
                double lineH = 12;
                double valueW = metaWidth - (labelW + colonW + 6);
                double baseY = y + (logoH / 2.0) - (lineH * 1.5);

                void MetaLine(int line, string label, string value)
                {
                    double lineY = baseY + lineH * line;
                    g.DrawString(label, metaFont, XBrushes.Black, new XRect(metaX, lineY, labelW, lineH), XStringFormats.CenterLeft);
                    g.DrawString(":", metaFont, XBrushes.Black, new XRect(metaX + labelW, lineY, colonW, lineH), XStringFormats.Center);
                    g.DrawString(value, metaFont, XBrushes.Black, new XRect(valueX, lineY, valueW, lineH), XStringFormats.CenterLeft);
                }

                MetaLine(0, "Report No.", detail.ReportNumber);
                MetaLine(1, "Date", detail.TestDateTime.ToString("dd MMM yyyy hh:mm tt", CultureInfo.InvariantCulture));
                MetaLine(2, "Page", $"{pageIndex} of {totalPages}");
            }

            y += logoH + 28;
            g.DrawLine(new XPen(XColors.DarkGray, 1), Margin, y, page.Width - Margin, y);

            return y + 12;
        }

        // Second pass over every page, now that the total is known.
        private static int StampHeadersAndFooters(PdfDocument doc, ReportDetailDto detail, string logoPath)
        {
            int totalPages = doc.Pages.Count;
            var footerFont = new XFont("Arial", 8, XFontStyle.Regular);

            for (int p = 0; p < totalPages; p++)
            {
                var pg = doc.Pages[p];
                using var g = XGraphics.FromPdfPage(pg);

                try
                {
                    DrawPageHeader(g, pg, detail, p + 1, totalPages, logoPath);
                }
                catch (Exception ex)
                {
                    FileLogService.Warn("ReportPdfExportService.StampHeadersAndFooters",
                        $"Header could not be drawn on page {p + 1} of {totalPages}: {ex.Message}");
                }

                double footerY = pg.Height - Margin + 6;
                g.DrawLine(new XPen(XColor.FromArgb(160, XColors.LightGray), 1), Margin, footerY - 12, pg.Width - Margin, footerY - 12);
                g.DrawString($"CONFIDENTIAL - CAT ASSEMBLY LINE TESTER REPORT - Page {p + 1}", footerFont,
                    new XSolidBrush(XColor.FromArgb(120, XColors.Gray)), new XRect(0, footerY - 10, pg.Width, 20), XStringFormats.Center);
            }

            return totalPages;
        }

        // Truncates text with an ellipsis until it fits the given width.
        private static string Ellipsize(XGraphics g, string? text, XFont font, double maxWidth)
        {
            if (string.IsNullOrEmpty(text)) return "-";
            if (g.MeasureString(text, font).Width <= maxWidth) return text;

            const string ell = "...";
            int lo = 0, hi = text.Length;
            while (lo < hi)
            {
                int mid = (lo + hi + 1) / 2;
                if (g.MeasureString(text.Substring(0, mid) + ell, font).Width <= maxWidth) lo = mid; else hi = mid - 1;
            }

            if (lo <= 0) return ell;
            return text.Substring(0, lo) + (lo < text.Length ? ell : "");
        }
    }
}
