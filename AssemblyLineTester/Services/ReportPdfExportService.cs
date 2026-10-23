using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using AssemblyLineTester.Helpers;
using AssemblyLineTester.Models;
using AssemblyLineTester.Models.Dto;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;

namespace AssemblyLineTester.Services
{
    // Renders a saved report as PDF; the counterpart to ReportExcelExportService.
    public class ReportPdfExportService
    {
        static ReportPdfExportService()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        private const double Margin = 40;

        // Space kept clear at the foot of a page so rows never run into the footer.
        private const double FooterReserve = 40;

        public void ExportReportToPdf(int reportId, string outputPath)
        {
            var detail = new TestReportService().GetReportDetail(reportId);
            if (detail is null) throw new InvalidOperationException("Report not found.");
            ExportReportToPdf(detail, outputPath);
        }

        public void ExportReportToPdf(ReportDetailDto detail, string outputPath, bool logToDatabase = true)
        {
            string logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "cat_logo.png");

            using PdfDocument doc = new();

            DrawSummaryPage(doc, detail, logoPath);
            DrawProductHarnessPages(doc, detail, logoPath);
            DrawTestSummaryPages(doc, detail, logoPath);
            DrawDetailPages(doc, detail, logoPath);

            int totalPages = StampHeadersAndFooters(doc, detail, logoPath);

            var dir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir)) Directory.CreateDirectory(dir);
            doc.Save(outputPath);

            if (logToDatabase)
            {
                new SystemLogService().Log(
                    Models.SystemLog.LogSeverity.Info, "Test Report", "Export PDF",
                    $"Report {detail.ReportNumber} exported to PDF ({totalPages} pages).");
            }
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

        private static readonly XColor[] HarnessPalette = new[]
        {
            XColor.FromArgb(22, 163, 74),   // Green (#16A34A)
            XColor.FromArgb(220, 38, 38),  // Red (#DC2626)
            XColor.FromArgb(37, 99, 235),  // Blue (#2563EB)
            XColor.FromArgb(217, 119, 6),  // Amber / Orange (#D97706)
            XColor.FromArgb(124, 58, 237), // Purple (#7C3AED)
            XColor.FromArgb(13, 148, 136), // Teal (#0D9488)
            XColor.FromArgb(225, 29, 72),  // Rose (#E11D48)
            XColor.FromArgb(79, 70, 229),  // Indigo (#4F46E5)
        };

        private static readonly XColor BannerBgColor = XColor.FromArgb(238, 242, 248);
        private static readonly XColor BorderColor = XColor.FromArgb(203, 213, 225);
        private static readonly XColor CardBorderColor = XColor.FromArgb(203, 213, 225);
        private static readonly XColor InnerBoxBorderColor = XColor.FromArgb(226, 232, 240);
        private static readonly XColor SubBannerBgColor = XColor.FromArgb(241, 245, 249);
        private static readonly XColor TextDarkColor = XColor.FromArgb(15, 23, 42);
        private static readonly XColor TextMutedColor = XColor.FromArgb(100, 116, 139);

        // Page 2: Product Harness Routing & Installation / Specification page.
        // Displays the overall machine routing and assembly matrix in Section 1,
        // and harness cards with tested connectors in Section 2.
        // If more than 3 harnesses exist, Section 2 continues onto a new page.
        // Visual catalog pages for Product Model (Point 1), Harness Assemblies (Point 2), and Connectors (Point 3).
        // Only elements with 'Include in Report' checked in the database are rendered.
        private static void DrawProductHarnessPages(PdfDocument doc, ReportDetailDto detail, string logoPath)
        {
            var productModel = FindProductModelForReport(detail.ProductModel, detail.PartNumber);
            var allHarnesses = productModel?.HarnessAssemblies?.ToList() ?? new List<HarnessAssembly>();

            // 1. Point 1: Overall Harness Routing & Installation (Product Model)
            bool hasOverall = productModel != null && IsIncludedInReport(productModel.ImagePath, productModel.IncludedForReport);

            // 2. Point 2: Harness Assemblies Specification
            var includedHarnesses = allHarnesses
                .Where(h => IsIncludedInReport(h.ImagePath, h.IncludedForReport))
                .ToList();

            // 3. Point 3: Connector Specifications & Pinouts
            var includedConnectors = new List<ConnectorCardItem>();
            var seenConnectorIds = new HashSet<int>();

            for (int hIdx = 0; hIdx < includedHarnesses.Count; hIdx++)
            {
                var harness = includedHarnesses[hIdx];
                int origIdx = allHarnesses.IndexOf(harness);
                var hColor = HarnessPalette[(origIdx >= 0 ? origIdx : hIdx) % HarnessPalette.Length];
                var conns = harness.Connectors ?? new List<Connector>();

                foreach (var conn in conns)
                {
                    if (conn == null) continue;
                    if (seenConnectorIds.Contains(conn.Id)) continue;

                    string? imgPath = GetPreferredImagePath(conn.ImagePath, conn.IncludedForReport);
                    if (imgPath != null)
                    {
                        seenConnectorIds.Add(conn.Id);
                        includedConnectors.Add(new ConnectorCardItem
                        {
                            Connector = conn,
                            ParentHarness = harness,
                            AccentColor = hColor,
                            ResolvedImagePath = imgPath
                        });
                    }
                }
            }

            // If none of the 3 points are active, do not generate any visual pages
            if (!hasOverall && includedHarnesses.Count == 0 && includedConnectors.Count == 0)
            {
                return;
            }

            // --- Render Point 1 (Overall) & Point 2 (Harnesses) ---
            if (hasOverall || includedHarnesses.Count > 0)
            {
                const int harnessChunkSize = 3;
                int totalHarnessChunks = includedHarnesses.Count == 0
                    ? 1
                    : (int)Math.Ceiling((double)includedHarnesses.Count / harnessChunkSize);

                for (int chunkIndex = 0; chunkIndex < totalHarnessChunks; chunkIndex++)
                {
                    var page = doc.AddPage();
                    page.Size = PdfSharp.PageSize.A4;
                    page.Orientation = PdfSharp.PageOrientation.Portrait;
                    using XGraphics gfx = XGraphics.FromPdfPage(page);

                    double usableWidth = page.Width - Margin * 2;
                    double bottom = page.Height - Margin - FooterReserve;
                    double y = DrawPageHeader(gfx, page, detail, 0, 0, logoPath);

                    var currentChunkHarnesses = includedHarnesses.Skip(chunkIndex * harnessChunkSize).Take(harnessChunkSize).ToList();
                    int harnessOffset = chunkIndex * harnessChunkSize;

                    if (chunkIndex == 0)
                    {
                        if (hasOverall)
                        {
                            y = DrawSection1OverallRouting(gfx, detail, productModel, allHarnesses, usableWidth, y);
                            y += 12; // Gap between Section 1 and Section 2
                        }

                        if (includedHarnesses.Count > 0)
                        {
                            DrawSection2HarnessSpecifications(gfx, currentChunkHarnesses, harnessOffset, usableWidth, y, bottom, isContinuation: false);
                        }
                    }
                    else
                    {
                        DrawSection2HarnessSpecifications(gfx, currentChunkHarnesses, harnessOffset, usableWidth, y, bottom, isContinuation: true);
                    }
                }
            }

            // --- Render Point 3: Connector Specifications & Pinouts ---
            if (includedConnectors.Count > 0)
            {
                const int connectorChunkSize = 6;
                int totalConnectorChunks = (int)Math.Ceiling((double)includedConnectors.Count / connectorChunkSize);

                for (int chunkIndex = 0; chunkIndex < totalConnectorChunks; chunkIndex++)
                {
                    var page = doc.AddPage();
                    page.Size = PdfSharp.PageSize.A4;
                    page.Orientation = PdfSharp.PageOrientation.Portrait;
                    using XGraphics gfx = XGraphics.FromPdfPage(page);

                    double usableWidth = page.Width - Margin * 2;
                    double bottom = page.Height - Margin - FooterReserve;
                    double y = DrawPageHeader(gfx, page, detail, 0, 0, logoPath);

                    var currentChunkConns = includedConnectors.Skip(chunkIndex * connectorChunkSize).Take(connectorChunkSize).ToList();
                    DrawSection3ConnectorSpecifications(gfx, currentChunkConns, usableWidth, y, bottom, isContinuation: chunkIndex > 0);
                }
            }
        }

        private static double DrawSection1OverallRouting(
            XGraphics gfx,
            ReportDetailDto detail,
            ProductModel? productModel,
            List<HarnessAssembly> harnesses,
            double usableWidth,
            double y)
        {
            var bannerFont = new XFont("Arial", 9.5, XFontStyle.Bold);
            var bannerPen = new XPen(BorderColor, 1);
            var bannerBrush = new XSolidBrush(BannerBgColor);

            double bannerH = 22;
            DrawRoundedBox(gfx, bannerPen, bannerBrush, Margin, y, usableWidth, bannerH, 4);
            gfx.DrawString("1.  OVERALL HARNESS ROUTING & INSTALLATION", bannerFont, new XSolidBrush(TextDarkColor),
                new XRect(Margin + 10, y, usableWidth - 20, bannerH), XStringFormats.CenterLeft);

            y += bannerH + 8;

            double cardH = 195;
            double gap = 10;
            double leftCardW = 230;
            double rightCardW = usableWidth - leftCardW - gap;

            var cardPen = new XPen(CardBorderColor, 1);

            // --- Left Card: Overall Routing Image ---
            double leftX = Margin;
            DrawRoundedBox(gfx, cardPen, XBrushes.White, leftX, y, leftCardW, cardH, 4);

            string? overallImgPath = GetPreferredImagePath(productModel?.ImagePath, productModel?.IncludedForReport);
            DrawImageFitted(gfx, overallImgPath, leftX + 8, y + 8, leftCardW - 16, cardH - 16, "No Overall Routing Image Available");

            // --- Right Card: Test Configuration & Assembly Matrix ---
            double rightX = leftX + leftCardW + gap;
            DrawRoundedBox(gfx, cardPen, XBrushes.White, rightX, y, rightCardW, cardH, 4);

            // Right Card Header Banner
            double headerH = 22;
            DrawRoundedBox(gfx, new XPen(BorderColor, 0.8), new XSolidBrush(BannerBgColor), rightX, y, rightCardW, headerH, 4);
            // Overdraw bottom corners to keep flush with box
            gfx.DrawRectangle(new XSolidBrush(BannerBgColor), rightX, y + headerH - 4, rightCardW, 4);
            gfx.DrawLine(new XPen(BorderColor, 0.8), rightX, y + headerH, rightX + rightCardW, y + headerH);

            gfx.DrawString("TEST CONFIGURATION & ASSEMBLY MATRIX", new XFont("Arial", 8.5, XFontStyle.Bold),
                new XSolidBrush(TextDarkColor), new XRect(rightX + 10, y, rightCardW - 20, headerH), XStringFormats.CenterLeft);

            // Metadata Row: Machine Model & Test Type
            double metaY = y + headerH + 6;
            var metaLabelFont = new XFont("Arial", 8, XFontStyle.Bold);
            var metaValFont = new XFont("Arial", 8, XFontStyle.Bold);

            string modelDisplay = !string.IsNullOrWhiteSpace(detail.ProductModel) && detail.ProductModel != "N/A"
                ? detail.ProductModel
                : (productModel?.Name ?? "N/A");
            string testTypeDisplay = !string.IsNullOrWhiteSpace(detail.ProductType) && detail.ProductType != "N/A" && detail.ProductType != "-"
                ? detail.ProductType
                : "Harness Assembly";

            double col1W = (rightCardW - 20) * 0.48;
            double col2X = rightX + 10 + col1W;
            double col2W = (rightCardW - 20) * 0.52;

            double mLabelW = 75;
            gfx.DrawString("Machine Model:", metaLabelFont, XBrushes.Black, new XRect(rightX + 10, metaY, mLabelW, 14), XStringFormats.CenterLeft);
            string elModel = Ellipsize(gfx, modelDisplay, metaValFont, col1W - mLabelW - 4);
            gfx.DrawString(elModel, metaValFont, new XSolidBrush(XColor.FromArgb(15, 23, 42)), new XRect(rightX + 10 + mLabelW, metaY, col1W - mLabelW, 14), XStringFormats.CenterLeft);

            double tLabelW = 55;
            gfx.DrawString("Test Type:", metaLabelFont, XBrushes.Black, new XRect(col2X, metaY, tLabelW, 14), XStringFormats.CenterLeft);
            string elType = Ellipsize(gfx, testTypeDisplay, metaValFont, col2W - tLabelW - 4);
            gfx.DrawString(elType, metaValFont, new XSolidBrush(XColor.FromArgb(15, 23, 42)), new XRect(col2X + tLabelW, metaY, col2W - tLabelW, 14), XStringFormats.CenterLeft);

            // Separator line under metadata
            double divY = metaY + 18;
            gfx.DrawLine(new XPen(XColor.FromArgb(226, 232, 240), 1), rightX + 8, divY, rightX + rightCardW - 8, divY);

            // Harness List
            double rowStartY = divY + 6;
            double rowH = 20;

            if (harnesses.Count == 0)
            {
                gfx.DrawString("No harness assemblies configured.", new XFont("Arial", 8, XFontStyle.Italic),
                    new XSolidBrush(TextMutedColor), new XRect(rightX + 10, rowStartY + 10, rightCardW - 20, 16), XStringFormats.CenterLeft);
            }
            else
            {
                int maxMatrixRows = Math.Min(harnesses.Count, 5);
                for (int i = 0; i < maxMatrixRows; i++)
                {
                    var h = harnesses[i];
                    var hColor = HarnessPalette[i % HarnessPalette.Length];
                    double curRowY = rowStartY + i * rowH;

                    // Color badge
                    DrawRoundedBox(gfx, null, new XSolidBrush(hColor), rightX + 10, curRowY + 4, 12, 10, 2);

                    // Part Number
                    string pnText = string.IsNullOrWhiteSpace(h.PartNumber) ? h.Name : $"P/N: {h.PartNumber}";
                    gfx.DrawString(pnText, new XFont("Arial", 8.5, XFontStyle.Bold), new XSolidBrush(TextDarkColor),
                        new XRect(rightX + 28, curRowY, 140, rowH), XStringFormats.CenterLeft);

                    // Connector count
                    int connCount = h.Connectors?.Count ?? 0;
                    string connText = $"{connCount} Connectors";
                    gfx.DrawString(connText, new XFont("Arial", 8, XFontStyle.Regular), new XSolidBrush(TextDarkColor),
                        new XRect(rightX + rightCardW - 100, curRowY, 90, rowH), XStringFormats.CenterRight);
                }

                if (harnesses.Count > 5)
                {
                    gfx.DrawString($"+ {harnesses.Count - 5} more harnesses...", new XFont("Arial", 7.5, XFontStyle.Italic),
                        new XSolidBrush(TextMutedColor), new XRect(rightX + 28, rowStartY + 5 * rowH, 140, 14), XStringFormats.CenterLeft);
                }
            }

            // Separator line before Scope footer
            double footerDivY = y + cardH - 24;
            gfx.DrawLine(new XPen(XColor.FromArgb(226, 232, 240), 1), rightX + 8, footerDivY, rightX + rightCardW - 8, footerDivY);

            // Scope footer
            string scopeText = $"Scope: {harnesses.Count} Harnesses Tested";
            gfx.DrawString(scopeText, new XFont("Arial", 8, XFontStyle.Bold), new XSolidBrush(TextDarkColor),
                new XRect(rightX + 10, footerDivY + 2, rightCardW - 20, 20), XStringFormats.CenterLeft);

            return y + cardH;
        }

        private static void DrawSection2HarnessSpecifications(
            XGraphics gfx,
            List<HarnessAssembly> chunkHarnesses,
            int harnessOffset,
            double usableWidth,
            double y,
            double bottom,
            bool isContinuation)
        {
            var bannerFont = new XFont("Arial", 9.5, XFontStyle.Bold);
            var bannerPen = new XPen(BorderColor, 1);
            var bannerBrush = new XSolidBrush(BannerBgColor);

            double bannerH = 22;
            string title = isContinuation
                ? "2.  HARNESS ASSEMBLIES & CONNECTOR SPECIFICATION (CONT.)"
                : "2.  HARNESS ASSEMBLIES & CONNECTOR SPECIFICATION";

            DrawRoundedBox(gfx, bannerPen, bannerBrush, Margin, y, usableWidth, bannerH, 4);
            gfx.DrawString(title, bannerFont, new XSolidBrush(TextDarkColor),
                new XRect(Margin + 10, y, usableWidth - 20, bannerH), XStringFormats.CenterLeft);

            y += bannerH + 8;
            double cardH = Math.Max(180, bottom - y);

            if (chunkHarnesses.Count == 0)
            {
                var cardPen = new XPen(CardBorderColor, 1);
                DrawRoundedBox(gfx, cardPen, XBrushes.White, Margin, y, usableWidth, cardH, 4);

                gfx.DrawString("No harness assembly specification data configured in the product database.",
                    new XFont("Arial", 9, XFontStyle.Italic), new XSolidBrush(TextMutedColor),
                    new XRect(Margin + 20, y, usableWidth - 40, cardH), XStringFormats.Center);
                return;
            }

            int count = chunkHarnesses.Count;
            double cardGap;
            double cardW;
            double startX;

            if (count == 3)
            {
                cardGap = 10;
                cardW = (usableWidth - (2 * cardGap)) / 3.0;
                startX = Margin;
            }
            else if (count == 2)
            {
                cardGap = 12;
                cardW = (usableWidth - cardGap) / 2.0;
                startX = Margin;
            }
            else
            {
                cardGap = 0;
                cardW = Math.Min(320, usableWidth);
                startX = Margin + (usableWidth - cardW) / 2.0;
            }

            for (int i = 0; i < count; i++)
            {
                var harness = chunkHarnesses[i];
                int globalIndex = harnessOffset + i;
                var hColor = HarnessPalette[globalIndex % HarnessPalette.Length];
                double cx = startX + i * (cardW + cardGap);

                DrawHarnessCard(gfx, harness, hColor, cx, y, cardW, cardH);
            }
        }

        private static void DrawHarnessCard(
            XGraphics gfx,
            HarnessAssembly harness,
            XColor hColor,
            double x,
            double y,
            double width,
            double height)
        {
            var cardPen = new XPen(CardBorderColor, 1);
            DrawRoundedBox(gfx, cardPen, XBrushes.White, x, y, width, height, 4);

            // 1. Header (P/N: ...)
            double headerH = 26;
            // Left colored vertical accent strip
            DrawRoundedBox(gfx, null, new XSolidBrush(hColor), x + 6, y + 5, 3.5, 16, 1);

            string pnText = string.IsNullOrWhiteSpace(harness.PartNumber) ? harness.Name : $"P/N: {harness.PartNumber}";
            gfx.DrawString(pnText, new XFont("Arial", 9, XFontStyle.Bold), new XSolidBrush(TextDarkColor),
                new XRect(x + 14, y + 4, width - 18, 18), XStringFormats.CenterLeft);

            // 2. Image Container
            double inset = 6;
            double imgBoxX = x + inset;
            double imgBoxW = width - inset * 2;
            double imgBoxY = y + headerH;
            double imgBoxH = height * 0.52;

            gfx.DrawRectangle(new XPen(InnerBoxBorderColor, 1), imgBoxX, imgBoxY, imgBoxW, imgBoxH);

            string? harnessImg = GetPreferredImagePath(harness.ImagePath, harness.IncludedForReport);
            DrawImageFitted(gfx, harnessImg, imgBoxX + 4, imgBoxY + 4, imgBoxW - 8, imgBoxH - 8, "No Harness Image");

            // 3. Sub-banner: TESTED CONNECTORS (N)
            double subH = 18;
            double subY = imgBoxY + imgBoxH + 6;

            DrawRoundedBox(gfx, new XPen(BorderColor, 0.8), new XSolidBrush(SubBannerBgColor), imgBoxX, subY, imgBoxW, subH, 3);
            int connCount = harness.Connectors?.Count ?? 0;
            gfx.DrawString($"TESTED CONNECTORS ({connCount})", new XFont("Arial", 7.5, XFontStyle.Bold),
                new XSolidBrush(TextDarkColor), new XRect(imgBoxX + 6, subY, imgBoxW - 12, subH), XStringFormats.CenterLeft);

            // 4. Connector Pills Grid
            double pillsStartY = subY + subH + 6;
            double pillsAreaH = height - (pillsStartY - y) - 6;

            var connectors = harness.Connectors ?? new List<Connector>();
            if (connectors.Count == 0)
            {
                gfx.DrawString("No connectors attached", new XFont("Arial", 7.5, XFontStyle.Italic),
                    new XSolidBrush(TextMutedColor), new XRect(imgBoxX, pillsStartY + 4, imgBoxW, 16), XStringFormats.Center);
                return;
            }

            int colCount = 2;
            double pillGapX = 4;
            double pillGapY = 4;
            double pillW = (imgBoxW - pillGapX) / 2.0;
            double pillH = 18;

            int maxRows = Math.Max(1, (int)(pillsAreaH / (pillH + pillGapY)));
            int maxPills = maxRows * colCount;

            for (int idx = 0; idx < connectors.Count && idx < maxPills; idx++)
            {
                int col = idx % colCount;
                int row = idx / colCount;
                double px = imgBoxX + col * (pillW + pillGapX);
                double py = pillsStartY + row * (pillH + pillGapY);

                if (idx == maxPills - 1 && connectors.Count > maxPills)
                {
                    DrawRoundedBox(gfx, new XPen(InnerBoxBorderColor, 0.8), new XSolidBrush(SubBannerBgColor), px, py, pillW, pillH, 3);
                    gfx.DrawString($"+{connectors.Count - idx} more", new XFont("Arial", 7, XFontStyle.Italic),
                        new XSolidBrush(TextMutedColor), new XRect(px, py, pillW, pillH), XStringFormats.Center);
                    break;
                }

                var conn = connectors[idx];
                string label = !string.IsNullOrWhiteSpace(conn.Name) && !string.IsNullOrWhiteSpace(conn.PartNumber)
                    ? $"{conn.Name}: {conn.PartNumber}"
                    : (!string.IsNullOrWhiteSpace(conn.Name) ? conn.Name : conn.PartNumber);

                // Pill container
                DrawRoundedBox(gfx, new XPen(BorderColor, 0.8), XBrushes.White, px, py, pillW, pillH, 3);

                // Left accent strip in harness color
                DrawRoundedBox(gfx, null, new XSolidBrush(hColor), px + 1.2, py + 2, 2.5, pillH - 4, 1);

                // Connector name & part number (ellipsized if necessary)
                var pillFont = new XFont("Arial", 6.8, XFontStyle.Bold);
                string displayLabel = Ellipsize(gfx, label, pillFont, pillW - 7);
                gfx.DrawString(displayLabel, pillFont, new XSolidBrush(TextDarkColor),
                    new XRect(px + 5.5, py, pillW - 7, pillH), XStringFormats.CenterLeft);
            }
        }

        private static void DrawSection3ConnectorSpecifications(
            XGraphics gfx,
            List<ConnectorCardItem> chunkConnectors,
            double usableWidth,
            double y,
            double bottom,
            bool isContinuation)
        {
            var bannerFont = new XFont("Arial", 9.5, XFontStyle.Bold);
            var bannerPen = new XPen(BorderColor, 1);
            var bannerBrush = new XSolidBrush(BannerBgColor);

            double bannerH = 22;
            string title = isContinuation
                ? "3.  CONNECTOR SPECIFICATIONS & PINOUTS (CONT.)"
                : "3.  CONNECTOR SPECIFICATIONS & PINOUTS";

            DrawRoundedBox(gfx, bannerPen, bannerBrush, Margin, y, usableWidth, bannerH, 4);
            gfx.DrawString(title, bannerFont, new XSolidBrush(TextDarkColor),
                new XRect(Margin + 10, y, usableWidth - 20, bannerH), XStringFormats.CenterLeft);

            y += bannerH + 8;
            double availableH = bottom - y;

            int count = chunkConnectors.Count;
            if (count == 0) return;

            int colCount;
            int rowCount;
            double gapX = 10;
            double gapY = 10;

            if (count <= 2)
            {
                colCount = 2;
                rowCount = 1;
            }
            else if (count == 3)
            {
                colCount = 3;
                rowCount = 1;
            }
            else
            {
                colCount = 3;
                rowCount = 2;
            }

            double cardW = (usableWidth - (colCount - 1) * gapX) / colCount;
            double cardH = rowCount == 1
                ? Math.Min(320, availableH)
                : Math.Min(320, (availableH - gapY) / 2.0);

            for (int i = 0; i < count; i++)
            {
                int col = i % colCount;
                int row = i / colCount;
                double cx = Margin + col * (cardW + gapX);
                double cy = y + row * (cardH + gapY);

                DrawConnectorCard(gfx, chunkConnectors[i], cx, cy, cardW, cardH);
            }
        }

        private static void DrawConnectorCard(
            XGraphics gfx,
            ConnectorCardItem item,
            double x,
            double y,
            double width,
            double height)
        {
            var cardPen = new XPen(CardBorderColor, 1);
            DrawRoundedBox(gfx, cardPen, XBrushes.White, x, y, width, height, 4);

            var conn = item.Connector;

            // 1. Header (Connector Name & Part Number)
            double headerH = 26;
            // Left accent vertical strip with parent harness color
            DrawRoundedBox(gfx, null, new XSolidBrush(item.AccentColor), x + 6, y + 4, 3.5, 18, 1);

            string connName = !string.IsNullOrWhiteSpace(conn.Name) ? conn.Name : "Connector";
            string pnText = !string.IsNullOrWhiteSpace(conn.PartNumber) ? $"P/N: {conn.PartNumber}" : "";

            gfx.DrawString(connName, new XFont("Arial", 8.5, XFontStyle.Bold), new XSolidBrush(TextDarkColor),
                new XRect(x + 14, y + 3, width - 20, 12), XStringFormats.CenterLeft);

            if (!string.IsNullOrEmpty(pnText))
            {
                gfx.DrawString(pnText, new XFont("Arial", 7.5, XFontStyle.Bold), new XSolidBrush(TextMutedColor),
                    new XRect(x + 14, y + 14, width - 20, 11), XStringFormats.CenterLeft);
            }

            // 2. Image Container
            double inset = 6;
            double imgBoxX = x + inset;
            double imgBoxW = width - inset * 2;
            double imgBoxY = y + headerH + 2;
            double imgBoxH = height * 0.54;

            gfx.DrawRectangle(new XPen(InnerBoxBorderColor, 1), imgBoxX, imgBoxY, imgBoxW, imgBoxH);
            DrawImageFitted(gfx, item.ResolvedImagePath, imgBoxX + 4, imgBoxY + 4, imgBoxW - 8, imgBoxH - 8, "No Connector Image");

            // 3. Specification Details Block
            double specY = imgBoxY + imgBoxH + 6;
            double specH = height - (specY - y) - 6;

            DrawRoundedBox(gfx, new XPen(BorderColor, 0.8), new XSolidBrush(SubBannerBgColor), imgBoxX, specY, imgBoxW, specH, 3);

            // Spec Rows inside the details block
            double curY = specY + 4;
            var labelFont = new XFont("Arial", 7.5, XFontStyle.Regular);
            var valFont = new XFont("Arial", 7.5, XFontStyle.Bold);
            var labelBrush = new XSolidBrush(TextMutedColor);
            var valBrush = new XSolidBrush(TextDarkColor);

            // Row 1: Parent Harness tag with color dot
            DrawRoundedBox(gfx, null, new XSolidBrush(item.AccentColor), imgBoxX + 6, curY + 3, 5, 5, 2.5);
            string harnessLabel = item.ParentHarness != null
                ? (string.IsNullOrWhiteSpace(item.ParentHarness.PartNumber) ? item.ParentHarness.Name : item.ParentHarness.PartNumber)
                : "Harness";
            string hText = Ellipsize(gfx, $"Harness: {harnessLabel}", valFont, imgBoxW - 20);
            gfx.DrawString(hText, valFont, valBrush, new XRect(imgBoxX + 15, curY, imgBoxW - 20, 12), XStringFormats.CenterLeft);

            curY += 14;
            gfx.DrawLine(new XPen(InnerBoxBorderColor, 0.8), imgBoxX + 4, curY, imgBoxX + imgBoxW - 4, curY);
            curY += 3;

            // Row 2: Type
            string typeVal = !string.IsNullOrWhiteSpace(conn.Type) ? conn.Type : "N/A";
            gfx.DrawString("Type:", labelFont, labelBrush, new XRect(imgBoxX + 6, curY, 40, 12), XStringFormats.CenterLeft);
            gfx.DrawString(typeVal, valFont, valBrush, new XRect(imgBoxX + 46, curY, imgBoxW - 50, 12), XStringFormats.CenterLeft);

            curY += 13;

            // Row 3: Total Pin
            string pinVal = conn.TotalPin > 0 ? $"{conn.TotalPin} Pins" : "N/A";
            gfx.DrawString("Pins:", labelFont, labelBrush, new XRect(imgBoxX + 6, curY, 40, 12), XStringFormats.CenterLeft);
            gfx.DrawString(pinVal, valFont, valBrush, new XRect(imgBoxX + 46, curY, imgBoxW - 50, 12), XStringFormats.CenterLeft);

            curY += 13;

            // Row 4: Description (if fits)
            if (!string.IsNullOrWhiteSpace(conn.Description) && curY + 12 < specY + specH)
            {
                string descEl = Ellipsize(gfx, conn.Description, new XFont("Arial", 7, XFontStyle.Italic), imgBoxW - 12);
                gfx.DrawString(descEl, new XFont("Arial", 7, XFontStyle.Italic), labelBrush,
                    new XRect(imgBoxX + 6, curY, imgBoxW - 12, 12), XStringFormats.CenterLeft);
            }
        }

        private static ProductModel? FindProductModelForReport(string? modelName, string? partNumber)
        {
            var all = ProductModelService.GetAllProductModels();
            if (all == null || all.Count == 0) return null;

            if (!string.IsNullOrWhiteSpace(modelName) && modelName != "N/A" && modelName != "-")
            {
                string trimmed = modelName.Trim();
                var found = all.FirstOrDefault(p => string.Equals(p.Name, trimmed, StringComparison.OrdinalIgnoreCase));
                if (found != null) return found;

                found = all.FirstOrDefault(p => string.Equals(p.PartNumber, trimmed, StringComparison.OrdinalIgnoreCase));
                if (found != null) return found;

                found = all.FirstOrDefault(p => !string.IsNullOrEmpty(p.Name) &&
                    (trimmed.Contains(p.Name, StringComparison.OrdinalIgnoreCase) || p.Name.Contains(trimmed, StringComparison.OrdinalIgnoreCase)));
                if (found != null) return found;
            }

            if (!string.IsNullOrWhiteSpace(partNumber) && partNumber != "N/A" && partNumber != "-")
            {
                string trimmed = partNumber.Trim();
                var found = all.FirstOrDefault(p => string.Equals(p.PartNumber, trimmed, StringComparison.OrdinalIgnoreCase));
                if (found != null) return found;
            }

            return null;
        }

        private static string? GetPreferredImagePath(List<string>? imagePaths, List<bool>? includedForReport)
        {
            if (imagePaths == null || imagePaths.Count == 0 || includedForReport == null || includedForReport.Count == 0)
                return null;

            for (int i = 0; i < imagePaths.Count && i < includedForReport.Count; i++)
            {
                if (includedForReport[i])
                {
                    string? resolved = ImageStorageHelper.ResolveImagePath(imagePaths[i]);
                    if (resolved != null && File.Exists(resolved))
                        return resolved;
                }
            }

            return null;
        }

        private static bool IsIncludedInReport(List<string>? imagePaths, List<bool>? includedForReport)
        {
            return GetPreferredImagePath(imagePaths, includedForReport) != null;
        }

        private sealed class ConnectorCardItem
        {
            public Connector Connector { get; set; } = null!;
            public HarnessAssembly? ParentHarness { get; set; }
            public XColor AccentColor { get; set; }
            public string ResolvedImagePath { get; set; } = string.Empty;
        }

        private static void DrawRoundedBox(XGraphics gfx, XPen? pen, XBrush? brush, double x, double y, double width, double height, double radius)
        {
            double d = radius * 2;
            if (d > width) d = width;
            if (d > height) d = height;

            if (pen != null && brush != null)
            {
                gfx.DrawRoundedRectangle(pen, brush, x, y, width, height, d, d);
            }
            else if (pen != null)
            {
                gfx.DrawRoundedRectangle(pen, x, y, width, height, d, d);
            }
            else if (brush != null)
            {
                gfx.DrawRoundedRectangle(brush, x, y, width, height, d, d);
            }
        }

        private static void DrawImageFitted(XGraphics gfx, string? imagePath, double x, double y, double width, double height, string placeholderText = "No Image Available")
        {
            if (string.IsNullOrWhiteSpace(imagePath) || !File.Exists(imagePath))
            {
                var placeholderBrush = new XSolidBrush(XColor.FromArgb(148, 163, 184));
                var placeholderFont = new XFont("Arial", 8.5, XFontStyle.Italic);
                gfx.DrawString(placeholderText, placeholderFont, placeholderBrush,
                    new XRect(x, y, width, height), XStringFormats.Center);
                return;
            }

            try
            {
                using var img = XImage.FromFile(imagePath);
                double imgW = img.PointWidth > 0 ? img.PointWidth : img.PixelWidth;
                double imgH = img.PointHeight > 0 ? img.PointHeight : img.PixelHeight;

                if (imgW <= 0 || imgH <= 0) return;

                double scale = Math.Min(width / imgW, height / imgH);
                double drawW = imgW * scale;
                double drawH = imgH * scale;
                double drawX = x + (width - drawW) / 2.0;
                double drawY = y + (height - drawH) / 2.0;

                gfx.DrawImage(img, drawX, drawY, drawW, drawH);
            }
            catch (Exception ex)
            {
                FileLogService.Warn("ReportPdfExportService.DrawImageFitted",
                    $"Could not draw image '{imagePath}': {ex.Message}");
                var placeholderBrush = new XSolidBrush(XColor.FromArgb(239, 68, 68));
                var placeholderFont = new XFont("Arial", 8, XFontStyle.Italic);
                gfx.DrawString("Image Load Error", placeholderFont, placeholderBrush,
                    new XRect(x, y, width, height), XStringFormats.Center);
            }
        }

        // Page 2 onwards: the Test Summary panel from the Test Execution screen, line for line.
        // Spills onto as many pages as needed when errors exceed a single page.
        private static void DrawTestSummaryPages(PdfDocument doc, ReportDetailDto detail, string logoPath)
        {
            var font = new XFont("Arial", 10, XFontStyle.Regular);
            var fontBold = new XFont("Arial", 10, XFontStyle.Bold);

            var page = AddTestSummaryPage(doc);
            XGraphics gfx = XGraphics.FromPdfPage(page);

            double lineH = 18;
            double width = page.Width - Margin * 2;
            double bottom = page.Height - Margin - FooterReserve;

            double y = DrawTestSummaryPageTop(gfx, page, detail, logoPath, fontBold);

            void EnsureSpace(double neededHeight)
            {
                if (y + neededHeight > bottom)
                {
                    gfx.Dispose();
                    page = AddTestSummaryPage(doc);
                    gfx = XGraphics.FromPdfPage(page);
                    width = page.Width - Margin * 2;
                    bottom = page.Height - Margin - FooterReserve;
                    y = DrawTestSummaryPageTop(gfx, page, detail, logoPath, fontBold);
                }
            }

            try
            {
                if (!string.IsNullOrWhiteSpace(detail.ErrorSummary))
                {
                    var lines = detail.ErrorSummary.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                    bool inErrorSection = false;
                    foreach (var line in lines)
                    {
                        string trimmed = line.Trim();
                        if (string.IsNullOrEmpty(trimmed))
                        {
                            EnsureSpace(lineH * 0.8);
                            y += lineH * 0.8;
                            continue;
                        }

                        EnsureSpace(lineH);

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

                EnsureSpace(lineH * 2);
                gfx.DrawString($"TEST RESULT = {detail.OverallResult}", fontBold,
                    detail.OverallResult?.ToUpperInvariant() == "PASS" ? XBrushes.DarkGreen : XBrushes.DarkRed,
                    new XRect(Margin, y, width, lineH), XStringFormats.TopLeft);
                y += lineH * 2;

                // "OK"/"X" rather than the screen's tick and cross: PdfSharp maps those glyphs through
                // the base Arial encoding and drops them, which would leave the counts unlabelled.
                foreach (var s in detail.TypeSummaries)
                {
                    EnsureSpace(lineH);
                    bool allPassed = s.Fail == 0;
                    gfx.DrawString($"[{(allPassed ? "OK" : "X")}]  {s.TestType} ({s.Pass}/{s.Total})", font,
                        allPassed ? XBrushes.Black : XBrushes.DarkRed,
                        new XRect(Margin, y, width, lineH), XStringFormats.TopLeft);
                    y += lineH;
                }

                EnsureSpace(lineH * 2.5);
                y += lineH;
                gfx.DrawString("TEST ERRORS", fontBold, XBrushes.Black, new XRect(Margin, y, width, lineH), XStringFormats.TopLeft);
                y += lineH * 1.5;

                var failed = detail.Details.Where(d => !string.IsNullOrWhiteSpace(d.ErrorDetail)).ToList();
                if (failed.Count == 0)
                {
                    EnsureSpace(lineH);
                    gfx.DrawString("No errors were recorded for this test run.", font, XBrushes.Black,
                        new XRect(Margin, y, width, lineH), XStringFormats.TopLeft);
                    return;
                }

                foreach (var d in failed)
                {
                    EnsureSpace(lineH);
                    string idPrefix = !string.IsNullOrWhiteSpace(d.NetId) ? $"{d.NetId} " : "";
                    gfx.DrawString($"{idPrefix}({d.TestType}) - {d.ErrorDetail}", font, XBrushes.DarkRed,
                        new XRect(Margin, y, width, lineH), XStringFormats.TopLeft);
                    y += lineH;
                }
            }
            finally
            {
                gfx.Dispose();
            }
        }

        private static PdfPage AddTestSummaryPage(PdfDocument doc)
        {
            var page = doc.AddPage();
            page.Size = PdfSharp.PageSize.A4;
            page.Orientation = PdfSharp.PageOrientation.Portrait;
            return page;
        }

        private static double DrawTestSummaryPageTop(XGraphics gfx, PdfPage page, ReportDetailDto detail, string logoPath, XFont fontBold)
        {
            double y = DrawPageHeader(gfx, page, detail, 0, 0, logoPath);

            bool isPass = string.Equals(detail.OverallResult, "PASS", StringComparison.OrdinalIgnoreCase) &&
                          (detail.Details == null || !detail.Details.Any(d => !string.IsNullOrWhiteSpace(d.ErrorDetail))) &&
                          (string.IsNullOrWhiteSpace(detail.ErrorSummary) || !detail.ErrorSummary.Contains("FAIL", StringComparison.OrdinalIgnoreCase));

            string title = isPass ? "TEST SUMMARY" : "ERROR SUMMARY";

            var titleBg = new XSolidBrush(XColor.FromArgb(40, 230, 230, 230));
            double titleWidth = 200;
            gfx.DrawRectangle(titleBg, Margin, y - 2, titleWidth, 18);
            gfx.DrawString(title, fontBold, XBrushes.Black, new XRect(Margin, y, 400, 16), XStringFormats.TopLeft);
            return y + 26;
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

            string SafeValue(string? v) =>
                string.IsNullOrWhiteSpace(v) || v == "-" ? "N/A" : v;

            var infoRows = new List<(string Label, string Value)>
            {
                ("Product Model", detail.ProductModel),
                ("Product Type", SafeValue(detail.ProductType)),
                ("Area", SafeValue(detail.Area)),
                ("ECN No.", SafeValue(detail.ECNNumber)),
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
            if (columns == null || columns.Count == 0)
            {
                columns = new List<string>
                {
                    "Connector Group", "Harness Group", "Color", "Gauge", "Net-ID", "From", "Net From Label", "Net From Connector", "To",
                    "Net To Label", "Net To Connector", "Test Type", "Lower Limit", "Upper Limit", "Expected Value",
                    "Tolerance", "Measurement"
                };
            }

            var page = AddDetailPage(doc);
            XGraphics g = XGraphics.FromPdfPage(page);
            double y = DrawDetailPageTop(g, page, detail, logoPath, fontBold, rowH, columns);

            int rowNum = 1;
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

                y = DrawDetailRow(g, page, d, rowNum++, font, fontBold, y, rowH, ref alt, columns);
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
            string[] headers = new[] { "No" }.Concat(columns).Concat(new[] { "Result" }).ToArray();
            var hdrBg = new XSolidBrush(XColor.FromArgb(28, 230, 230, 230));

            double headerH = 22;
            double x = Margin;
            for (int i = 0; i < headers.Length; i++)
            {
                g.DrawRectangle(hdrBg, x, y, cols[i], headerH);
                g.DrawRectangle(XPens.LightGray, x, y, cols[i], headerH);
                if (i == 0)
                {
                    g.DrawString(headers[i], fontBold, XBrushes.Black, new XRect(x, y, cols[i], headerH), XStringFormats.Center);
                }
                else
                {
                    DrawHeaderCell(g, headers[i], fontBold, new XRect(x + 2, y, cols[i] - 4, headerH));
                }
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
        private static readonly Dictionary<string, double> DetailColumnWeights = new(StringComparer.OrdinalIgnoreCase)
        {
            ["No"] = 0.35,
            ["#"] = 0.35,
            ["Net-ID"] = 0.65,
            ["Net ID"] = 0.65,
            ["NetId"] = 0.65,
            ["Harness Group"] = 0.75,
            ["Harness-Group"] = 0.75,
            ["Connector Group"] = 0.9,
            ["Connector-Group"] = 0.9,
            ["From"] = 0.45,
            ["Net From Label"] = 1.3,
            ["Net-From Label"] = 1.3,
            ["NetFromLabel"] = 1.3,
            ["Net From Connector"] = 1.05,
            ["Net-From Connector"] = 1.05,
            ["NetFromConnector"] = 1.05,
            ["To"] = 0.45,
            ["Net To Label"] = 1.3,
            ["Net-To Label"] = 1.3,
            ["NetToLabel"] = 1.3,
            ["Net To Connector"] = 1.05,
            ["Net-To Connector"] = 1.05,
            ["NetToConnector"] = 1.05,
            ["Test Type"] = 0.85,
            ["Test-Type"] = 0.85,
            ["Color"] = 0.5,
            ["Gauge"] = 0.55,
            ["Lower Limit"] = 0.8,
            ["Upper Limit"] = 0.8,
            ["Expected Value"] = 0.8,
            ["Tolerance"] = 0.65,
            ["Measurement"] = 1.35
        };

        // The leading entry is "No", and trailing entry is the Result column, which are always drawn.
        private static double[] DetailColumnWidths(PdfPage page, List<string> columns)
        {
            double w = page.Width - Margin * 2;

            var weights = new List<double> { 0.35 };
            weights.AddRange(columns
                .Select(c => DetailColumnWeights.TryGetValue(c, out double weight) ? weight : 1.0));
            weights.Add(0.65);

            double total = weights.Sum();
            return weights.Select(weight => w * weight / total).ToArray();
        }

        private static double DrawDetailRow(XGraphics g, PdfPage page, TestDetailDto d, int rowNum, XFont font, XFont fontBold, double y, double rowH, ref bool alt, List<string> columns)
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

            Cell(0, rowNum.ToString(), XStringFormats.Center);

            for (int i = 0; i < columns.Count; i++)
            {
                var align = columns[i] is "Harness Group" or "Harness-Group" or "Connector Group" or "Connector-Group" or "From" or "To" or "Test Type" or "Test-Type" or "Net From Label" or "Net-From Label" or "Net To Label" or "Net-To Label" or "Net From Connector" or "Net-From Connector" or "Net To Connector" or "Net-To Connector" or "Color" or "Gauge"
                    ? XStringFormats.CenterLeft
                    : XStringFormats.Center;
                Cell(i + 1, d.Column(columns[i]), align);
            }

            int resultColIdx = columns.Count + 1;
            var resultRect = new XRect(x, y, cols[resultColIdx], rowH);
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
            g.DrawRectangle(XPens.LightGray, x, y, cols[resultColIdx], rowH);
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
