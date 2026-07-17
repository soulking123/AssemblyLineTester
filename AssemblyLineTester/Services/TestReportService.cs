using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Globalization;
using AssemblyLineTester.Data;
using AssemblyLineTester.Models;
using AssemblyLineTester.Models.Dto;

namespace AssemblyLineTester.Services
{
    public class TestReportService
    {
        // Type Names for Report 
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

        private static double? ParseLeadingNumber(string? s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;
            // Extract leading numeric part (handles values like "123.45 ohm" or "< 10.0 Ω")
            string trimmed = s.Trim();
            // Remove leading symbols like '<', '>'
            trimmed = trimmed.TrimStart('<', '>', '~');
            var parts = trimmed.Split(' ');
            if (parts.Length == 0) return null;
            if (double.TryParse(parts[0].Replace(",", ""), out double v)) return v;
            // Try to find first token that can be parsed
            foreach (var token in parts)
            {
                var t = token.Replace(",", "");
                if (double.TryParse(t, out double vv)) return vv;
            }
            return null;
        }

        // Truncate text with ellipsis 
        private static string Ellipsize(XGraphics g, string? text, XFont font, double maxWidth)
        {
            if (string.IsNullOrEmpty(text)) return "-";
            // quick check
            var m = g.MeasureString(text, font);
            if (m.Width <= maxWidth) return text;

            const string ell = "...";
            int lo = 0, hi = text.Length;
            while (lo < hi)
            {
                int mid = (lo + hi + 1) / 2;
                string cand = text.Substring(0, mid) + ell;
                if (g.MeasureString(cand, font).Width <= maxWidth) lo = mid; else hi = mid - 1;
            }
            if (lo <= 0) return ell;
            return text.Substring(0, lo) + (lo < text.Length ? ell : "");
        }

        private double DrawPageHeader(XGraphics g, PdfPage page, ReportDetailDto detail, int pageIndex, int totalPages, string logoPath, double margin)
        {
            double y = margin;
            // increase header logo height so it's more prominent
            double logoH = 45;
            var titleFont = new XFont("Arial", 14, XFontStyle.Bold);
            var metaFont = new XFont("Arial", 10, XFontStyle.Bold);

            if (File.Exists(logoPath))
            {
                try
                {
                    using var img = XImage.FromFile(logoPath);
                    // compute image size in points and scale uniformly to desired target height so aspect ratio is preserved
                    double imgPointW = img.PixelWidth * 72.0 / (img.HorizontalResolution <= 0 ? 96.0 : img.HorizontalResolution);
                    double imgPointH = img.PixelHeight * 72.0 / (img.VerticalResolution <= 0 ? 96.0 : img.VerticalResolution);
                    double desiredH = logoH; // target height in points (larger than before)
                    double scale = desiredH / imgPointH;
                    double drawW = imgPointW * scale;
                    double drawH = imgPointH * scale;
                    g.DrawImage(img, margin, y, drawW, drawH);
                    // use actual drawn height when positioning title
                    logoH = drawH;
                }
                catch { }
            }

            g.DrawString("TEST REPORT", titleFont, XBrushes.Black, new XRect(margin, y + logoH + 6, 400, 26), XStringFormats.TopLeft);

            // draw meta on the right only when totalPages > 0 (we'll call again later with correct totalPages)
            if (totalPages > 0)
            {
                // Use the same column logic as the GENERAL INFORMATION table so ':' aligns consistently within the block
                double metaWidth = 220;
                double metaX = page.Width - margin - metaWidth;
                double metaLabelW = 110;
                double metaColonW = 12;
                double metaValueX = metaX + metaLabelW + metaColonW + 6; // small gap after colon
                double metaLineH = 12;
                // vertically center the three-line meta block relative to the logo height
                double metaBaseY = y + (logoH / 2.0) - (metaLineH * 1.5);

                // draw label, colon, value using consistent alignments
                g.DrawString("Report No.", metaFont, XBrushes.Black, new XRect(metaX, metaBaseY, metaLabelW, metaLineH), XStringFormats.CenterLeft);
                g.DrawString(":", metaFont, XBrushes.Black, new XRect(metaX + metaLabelW, metaBaseY, metaColonW, metaLineH), XStringFormats.Center);
                g.DrawString(detail.ReportNumber, metaFont, XBrushes.Black, new XRect(metaValueX, metaBaseY, metaWidth - (metaLabelW + metaColonW + 6), metaLineH), XStringFormats.CenterLeft);

                g.DrawString("Date", metaFont, XBrushes.Black, new XRect(metaX, metaBaseY + metaLineH, metaLabelW, metaLineH), XStringFormats.CenterLeft);
                g.DrawString(":", metaFont, XBrushes.Black, new XRect(metaX + metaLabelW, metaBaseY + metaLineH, metaColonW, metaLineH), XStringFormats.Center);
                g.DrawString(detail.TestDateTime.ToString("dd MMM yyyy hh:mm tt", CultureInfo.InvariantCulture), metaFont, XBrushes.Black, new XRect(metaValueX, metaBaseY + metaLineH, metaWidth - (metaLabelW + metaColonW + 6), metaLineH), XStringFormats.CenterLeft);

                g.DrawString("Page", metaFont, XBrushes.Black, new XRect(metaX, metaBaseY + metaLineH * 2, metaLabelW, metaLineH), XStringFormats.CenterLeft);
                g.DrawString(":", metaFont, XBrushes.Black, new XRect(metaX + metaLabelW, metaBaseY + metaLineH * 2, metaColonW, metaLineH), XStringFormats.Center);
                g.DrawString($"{pageIndex} of {totalPages}", metaFont, XBrushes.Black, new XRect(metaValueX, metaBaseY + metaLineH * 2, metaWidth - (metaLabelW + metaColonW + 6), metaLineH), XStringFormats.CenterLeft);
            }

            y += logoH + 28;
            g.DrawLine(new XPen(XColors.DarkGray, 1), margin, y, page.Width - margin, y);
            y += 12;

            return y;
        }

        /// Generate a PDF file for the given report id using PdfSharp.
        public void GenerateReportPdf(int reportId, string outputPath)
        {
            var detail = GetReportDetail(reportId);
            if (detail is null) throw new InvalidOperationException("Report not found.");

            string logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "cat_logo.png");

            using PdfDocument doc = new();
            // Page 1 - summary
            var page = doc.AddPage();
            page.Size = PdfSharp.PageSize.A4;
            page.Orientation = PdfSharp.PageOrientation.Portrait;
            XGraphics gfx = XGraphics.FromPdfPage(page);

            var fontH = new XFont("Arial", 20, XFontStyle.Bold);
            var fontSub = new XFont("Arial", 10, XFontStyle.Bold);
            var font = new XFont("Arial", 9, XFontStyle.Regular);
            var fontBold = new XFont("Arial", 9, XFontStyle.Bold);

            double margin = 40;
            double y = margin;

            // --- Page 1: General information + Summary + Result box
            y = DrawPageHeader(gfx, page, detail, 0, 0, logoPath, margin);

            // Page 1: write title 'GENERAL INFORMATION' then the general information table
            var headerTitleBg = new XSolidBrush(XColor.FromArgb(40, 230, 230, 230));
            // draw small background behind the heading
            gfx.DrawRectangle(headerTitleBg, margin, y - 2, 200, 18);
            gfx.DrawString("GENERAL INFORMATION", fontBold, XBrushes.Black, new XRect(margin, y, 400, 16), XStringFormats.TopLeft);
            y += 18;

            // General information table - two-column layout similar to reference
            double infoStartX = margin;
            double infoStartY = y;
            double infoTableWidth = page.Width - margin * 2;
            double colLabelW = 110;
            double colValueW = (infoTableWidth / 2) - colLabelW - 6; // two sets per row
            double infoRowH = 20;

            // Simplified GENERAL INFORMATION as single-column list (label : value)
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
            double valueX = infoStartX + labelColW + colonColW + 6;
            double valueW = infoTableWidth - labelColW - colonColW - 6;
            // draw subtle background for the list
            var infoBg = new XSolidBrush(XColor.FromArgb(12, 200, 200, 200));
            double infoTableH = infoRows.Count * infoRowH;
            gfx.DrawRectangle(infoBg, infoStartX, infoStartY, infoTableWidth, infoTableH);

            double curY = infoStartY;
            foreach (var r in infoRows)
            {
                // draw label 
                gfx.DrawString(r.Label, fontBold, XBrushes.Black, new XRect(infoStartX + 6, curY, labelColW - 6, infoRowH), XStringFormats.CenterLeft);
                // draw colon in its own centered column
                gfx.DrawString(":", fontBold, XBrushes.Black, new XRect(infoStartX + labelColW, curY, colonColW, infoRowH), XStringFormats.Center);

                // color Result value
                XBrush valBrush = XBrushes.Black;
                if (string.Equals(r.Label, "Result", StringComparison.OrdinalIgnoreCase))
                {
                    if (string.Equals(r.Value, "PASS", StringComparison.OrdinalIgnoreCase)) valBrush = XBrushes.DarkGreen;
                    else if (string.Equals(r.Value, "FAIL", StringComparison.OrdinalIgnoreCase) || string.Equals(r.Value, "OPEN", StringComparison.OrdinalIgnoreCase)) valBrush = XBrushes.DarkRed;
                }

                gfx.DrawString(r.Value ?? "-", font, valBrush, new XRect(valueX, curY, valueW, infoRowH), XStringFormats.CenterLeft);

                // horizontal separator line for general info made transparent (opacity 0) per request
                gfx.DrawLine(new XPen(XColor.FromArgb(0, XColors.LightGray), 1), infoStartX, curY + infoRowH, infoStartX + infoTableWidth, curY + infoRowH);

                curY += infoRowH;
            }

            y = infoStartY + infoTableH + 12;

            // Summary table
            gfx.DrawString("TEST RESULT SUMMARY", fontBold, XBrushes.Black, new XRect(margin, y, 400, 16), XStringFormats.TopLeft);
            y += 18;

            double tableX = margin;
            double[] colWidths = { 220, 60, 60, 60, 90 };
            double rowH = 20;

            double tx = tableX;
            var hdrBg = new XSolidBrush(XColor.FromArgb(40, 230, 230, 230));
            gfx.DrawRectangle(hdrBg, tableX, y, colWidths.Sum(), rowH);
            string[] headers = { "Test Type", "Total", "Pass", "Fail", "Pass Rate" };
            for (int i = 0; i < headers.Length; i++)
            {
                gfx.DrawRectangle(hdrBg, tx, y, colWidths[i], rowH);
                gfx.DrawRectangle(XPens.LightGray, tx, y, colWidths[i], rowH);
                // left-align the first header (Test Type), center others
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
                tx = tableX;
                gfx.DrawRectangle(XPens.LightGray, tx, y, colWidths[0], rowH);
                var typeText = Ellipsize(gfx, s.TestType, font, colWidths[0] - 12);
                gfx.DrawString(typeText, font, XBrushes.Black, new XRect(tx + 6, y, colWidths[0] - 10, rowH), XStringFormats.CenterLeft);
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
                var failBrush = s.Fail > 0 ? XBrushes.DarkRed : XBrushes.Black;
                gfx.DrawString(s.Fail.ToString(), font, failBrush, new XRect(tx, y, colWidths[3], rowH), XStringFormats.Center);
                sumFail += s.Fail;
                tx += colWidths[3];
                gfx.DrawRectangle(XPens.LightGray, tx, y, colWidths[4], rowH);
                gfx.DrawString($"{s.PassRatePercent:0.00}%", font, XBrushes.Black, new XRect(tx, y, colWidths[4], rowH), XStringFormats.Center);
                y += rowH;
            }

            tx = tableX;
            var totalBg = hdrBg; 
            for (int i = 0; i < colWidths.Length; i++)
            {
                gfx.DrawRectangle(totalBg, tx, y, colWidths[i], rowH);
                gfx.DrawRectangle(XPens.LightGray, tx, y, colWidths[i], rowH);
                tx += colWidths[i];
            }
            // write totals
            tx = tableX;
            gfx.DrawString("Total", fontBold, XBrushes.Black, new XRect(tx + 6, y, colWidths[0] - 10, rowH), XStringFormats.CenterLeft);
            tx += colWidths[0];
            gfx.DrawString(sumTotal.ToString(), fontBold, XBrushes.Black, new XRect(tx, y, colWidths[1], rowH), XStringFormats.Center);
            tx += colWidths[1];
            gfx.DrawString(sumPass.ToString(), fontBold, XBrushes.Black, new XRect(tx, y, colWidths[2], rowH), XStringFormats.Center);
            tx += colWidths[2];
            var totalFailBrush = sumFail > 0 ? XBrushes.DarkRed : XBrushes.Black;
            gfx.DrawString(sumFail.ToString(), fontBold, totalFailBrush, new XRect(tx, y, colWidths[3], rowH), XStringFormats.Center);
            tx += colWidths[3];
            var totalPassRate = sumTotal == 0 ? 100.0 : Math.Round(sumPass * 100.0 / sumTotal, 2);
            gfx.DrawString($"{totalPassRate:0.00}%", fontBold, XBrushes.Black, new XRect(tx, y, colWidths[4], rowH), XStringFormats.Center);
            y += rowH + 12;

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
            gfx.DrawString(detail.OverallResult, resultFont, new XSolidBrush(borderColor), new XRect(boxX, textY, boxW, measuredSize.Height), XStringFormats.Center);

            // Page 2 - detailed results with improved table styling (landscape)
            var page2 = doc.AddPage();
            page2.Size = PdfSharp.PageSize.A4;
            page2.Orientation = PdfSharp.PageOrientation.Landscape;
            XGraphics g2 = XGraphics.FromPdfPage(page2);
            double y2 = margin;
            y2 = DrawPageHeader(g2, page2, detail, 0, 0, logoPath, margin);
            g2.DrawString("DETAILED TEST RESULTS", fontBold, XBrushes.Black, new XRect(margin, y2, 400, 16), XStringFormats.TopLeft);
            y2 += 18;

            double footerReserve = 40; // points reserved at bottom to avoid overlap with footer

            // Detailed table header (clean, bold, shaded)
            double dx = margin;
            double dRowH = 18;
            double contentWidth = page2.Width - margin * 2;
            double netIdW = contentWidth * 0.07;    // Net ID
            double harnessW = contentWidth * 0.25;  // Harness wide
            double fromW = contentWidth * 0.08;     // From
            double toW = contentWidth * 0.08;       // To
            double testTypeW = contentWidth * 0.15; // Test Type
            double lowerW = contentWidth * 0.07;    // Lower
            double upperW = contentWidth * 0.07;    // Upper
            double measureW = contentWidth * 0.10;  // Measure
            double tolW = contentWidth * 0.07;      // Tolerance
            double resultW = contentWidth * 0.06;   // Result
            double[] dCols = { netIdW, harnessW, fromW, toW, testTypeW, lowerW, upperW, measureW, tolW, resultW };
            string[] dHeaders = { "Net ID", "Harness Group", "From", "To", "Test Type", "Lower", "Upper", "Measure", "Tolerance", "Result" };
            var hdrBg2 = new XSolidBrush(XColor.FromArgb(28, 230, 230, 230));
            void DrawDetailHeader(XGraphics gx, ref double localY)
            {
                double localX = margin;
                for (int i = 0; i < dHeaders.Length; i++)
                {
                    gx.DrawRectangle(hdrBg2, localX, localY, dCols[i], dRowH);
                    gx.DrawRectangle(XPens.LightGray, localX, localY, dCols[i], dRowH);
                    gx.DrawString(dHeaders[i], fontBold, XBrushes.Black, new XRect(localX + 3, localY + 4, dCols[i] - 6, dRowH), XStringFormats.TopLeft);
                    localX += dCols[i];
                }
                localY += dRowH;
            }

            DrawDetailHeader(g2, ref y2);

            int idx = 1;
            bool alt = false;
            foreach (var d in detail.Details)
            {
                dx = margin;
                if (y2 + dRowH > page2.Height - margin - footerReserve)
                {
                    // dispose previous page graphics before creating a new one to avoid multiple XGraphics on same page
                    try { g2.Dispose(); } catch { }
                    page2 = doc.AddPage();
                    page2.Size = PdfSharp.PageSize.A4;
                    page2.Orientation = PdfSharp.PageOrientation.Landscape; // ensure all detail pages are landscape
                    g2 = XGraphics.FromPdfPage(page2);
                    // draw consistent header on the new page (no page numbers yet)
                    y2 = DrawPageHeader(g2, page2, detail, 0, 0, logoPath, margin);
                    // title for detail pages
                    g2.DrawString("DETAILED TEST RESULTS", fontBold, XBrushes.Black, new XRect(margin, y2, 400, 16), XStringFormats.TopLeft);
                    y2 += 18;
                    DrawDetailHeader(g2, ref y2);
                }

                // determine row-level styling based on result
                bool rowIsPass = string.Equals(d.Result, "PASS", StringComparison.OrdinalIgnoreCase);
                bool rowIsOpen = string.Equals(d.Result, "OPEN", StringComparison.OrdinalIgnoreCase);
                bool rowIsFail = !rowIsPass && !rowIsOpen;

                if (rowIsFail)
                {
                    // light red background for failing rows
                    g2.DrawRectangle(new XSolidBrush(XColor.FromArgb(40, 255, 200, 200)), margin, y2, page2.Width - margin * 2, dRowH);
                }
                else if (rowIsOpen)
                {
                    // pale orange for open
                    g2.DrawRectangle(new XSolidBrush(XColor.FromArgb(30, 255, 230, 200)), margin, y2, page2.Width - margin * 2, dRowH);
                }
                else
                {
                    // alternate row shading for pass/neutral rows
                    if (alt) g2.DrawRectangle(new XSolidBrush(XColor.FromArgb(8, 200, 200, 200)), margin, y2, page2.Width - margin * 2, dRowH);
                    alt = !alt;
                }

                string lowerStr = "-", upperStr = "-", measuredStr = d.Measured, tolStr = "-";
                if (!string.IsNullOrWhiteSpace(d.Limit))
                {
                    var parts = d.Limit.Split('-');
                    if (parts.Length == 2) { lowerStr = parts[0].Trim(); upperStr = parts[1].Trim(); } else upperStr = d.Limit;
                }
                double? lowerVal = ParseLeadingNumber(lowerStr);
                double? upperVal = ParseLeadingNumber(upperStr);
                if (lowerVal.HasValue && upperVal.HasValue)
                {
                    double mid = (lowerVal.Value + upperVal.Value) / 2.0;
                    if (Math.Abs(mid) > 1e-9) { tolStr = $"±{Math.Abs((upperVal.Value - mid) / mid * 100.0):0.##}%"; }
                }

                // draw cells
                g2.DrawRectangle(XPens.LightGray, dx, y2, dCols[0], dRowH); g2.DrawString(idx.ToString(), font, XBrushes.Black, new XRect(dx, y2 + 3, dCols[0], dRowH), XStringFormats.Center); dx += dCols[0];

                g2.DrawRectangle(XPens.LightGray, dx, y2, dCols[1], dRowH);
                var netHarness = Ellipsize(g2, d.NetId + " - " + d.HarnessGroup, font, dCols[1] - 12);
                g2.DrawString(netHarness, font, XBrushes.Black, new XRect(dx + 3, y2, dCols[1] - 6, dRowH), XStringFormats.CenterLeft);
                dx += dCols[1];

                g2.DrawRectangle(XPens.LightGray, dx, y2, dCols[2], dRowH);
                var fromText = Ellipsize(g2, d.FromPin, font, dCols[2] - 12);
                g2.DrawString(fromText, font, XBrushes.Black, new XRect(dx + 3, y2, dCols[2] - 6, dRowH), XStringFormats.CenterLeft);
                dx += dCols[2];

                g2.DrawRectangle(XPens.LightGray, dx, y2, dCols[3], dRowH);
                var toText = Ellipsize(g2, d.ToPin, font, dCols[3] - 12);
                g2.DrawString(toText, font, XBrushes.Black, new XRect(dx + 3, y2, dCols[3] - 6, dRowH), XStringFormats.CenterLeft);
                dx += dCols[3];

                g2.DrawRectangle(XPens.LightGray, dx, y2, dCols[4], dRowH);
                var testTypeText = Ellipsize(g2, d.TestType, font, dCols[4] - 12);
                g2.DrawString(testTypeText, font, XBrushes.Black, new XRect(dx + 3, y2, dCols[4] - 6, dRowH), XStringFormats.CenterLeft);
                dx += dCols[4];

                g2.DrawRectangle(XPens.LightGray, dx, y2, dCols[5], dRowH); g2.DrawString(lowerStr, font, XBrushes.Black, new XRect(dx, y2 + 3, dCols[5], dRowH), XStringFormats.Center); dx += dCols[5];
                g2.DrawRectangle(XPens.LightGray, dx, y2, dCols[6], dRowH); g2.DrawString(upperStr, font, XBrushes.Black, new XRect(dx, y2 + 3, dCols[6], dRowH), XStringFormats.Center); dx += dCols[6];
                g2.DrawRectangle(XPens.LightGray, dx, y2, dCols[7], dRowH);
                var measuredText = Ellipsize(g2, measuredStr, font, dCols[7] - 12);
                g2.DrawString(measuredText, font, XBrushes.Black, new XRect(dx + 3, y2, dCols[7] - 6, dRowH), XStringFormats.Center);
                dx += dCols[7];
                g2.DrawRectangle(XPens.LightGray, dx, y2, dCols[8], dRowH); g2.DrawString(tolStr, font, XBrushes.Black, new XRect(dx, y2 + 3, dCols[8], dRowH), XStringFormats.Center); dx += dCols[8];

                var rrect = new XRect(dx, y2, dCols[9], dRowH);
                if (rowIsPass)
                {
                    // green result cell
                    g2.DrawRectangle(new XSolidBrush(XColor.FromArgb(200, 220, 255, 220)), rrect);
                    g2.DrawRectangle(XPens.LightGray, dx, y2, dCols[9], dRowH);
                    g2.DrawString(d.Result, fontBold, new XSolidBrush(XColor.FromArgb(180, XColors.DarkGreen)), rrect, XStringFormats.Center);
                }
                else if (rowIsOpen)
                {
                    g2.DrawRectangle(new XSolidBrush(XColor.FromArgb(160, XColors.Orange)), rrect);
                    g2.DrawRectangle(XPens.LightGray, dx, y2, dCols[9], dRowH);
                    g2.DrawString(d.Result, fontBold, XBrushes.DarkOrange, rrect, XStringFormats.Center);
                }
                else
                {
                    // fail - red text on light red cell
                    g2.DrawRectangle(new XSolidBrush(XColor.FromArgb(40, 255, 200, 200)), rrect);
                    g2.DrawRectangle(XPens.LightGray, dx, y2, dCols[9], dRowH);
                    g2.DrawString(d.Result, fontBold, XBrushes.DarkRed, rrect, XStringFormats.Center);
                }

                y2 += dRowH;
                idx++;
            }

            // Draw per-page header meta and footer (page numbers) now that we know total pages
            try { gfx.Dispose(); } catch { }
            try { g2.Dispose(); } catch { }
            int totalPages = doc.Pages.Count;
            var fontMeta = new XFont("Arial", 9, XFontStyle.Regular);
            var fontMetaSmall = new XFont("Arial", 8, XFontStyle.Regular);
            for (int p = 0; p < totalPages; p++)
            {
                var pg = doc.Pages[p];
                using var gpage = XGraphics.FromPdfPage(pg);
                // Reuse DrawPageHeader to render the logo/title/meta consistently (meta will get pageIndex and totalPages)
                try { DrawPageHeader(gpage, pg, detail, p + 1, totalPages, logoPath, margin); } catch { }

                // footer line and confidential text centered
                double footerY = pg.Height - margin + 6;
                gpage.DrawLine(new XPen(XColor.FromArgb(160, XColors.LightGray), 1), margin, footerY - 12, pg.Width - margin, footerY - 12);
                string footerText = $"CONFIDENTIAL - CAT ASSEMBLY LINE TESTER REPORT - Page {p + 1}";
                gpage.DrawString(footerText, fontMetaSmall, new XSolidBrush(XColor.FromArgb(120, XColors.Gray)), new XRect(0, footerY - 10, pg.Width, 20), XStringFormats.Center);
                // record footer top Y is unnecessary because we reserve footer space when rendering tables
            }

            // Save
            doc.Save(outputPath);
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