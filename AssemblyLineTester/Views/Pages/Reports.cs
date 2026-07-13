using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AssemblyLineTester.Views.Pages
{
    public partial class Reports : UserControl
    {
        private Chart chartTrend = null!;

        public Reports()
        {
            InitializeComponent();
            this.Load += Reports_Load;
        }

        private void Reports_Load(object? sender, EventArgs e)
        {
            if (DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            {
                return;
            }
            InitializeCustomStyles();
            LoadDummyData();
        }

        private void InitializeCustomStyles()
        {
            // Create Chart programmatically to avoid WinForms Designer serialization issues
            chartTrend = new Chart
            {
                Dock = DockStyle.Fill,
                Name = "chartTrend",
                TabIndex = 1
            };
            cardTrend.Controls.Add(chartTrend);
            cardTrend.Controls.SetChildIndex(chartTrend, 0);
            if (cmbReportType.Items.Count > 0) cmbReportType.SelectedIndex = 0;
            if (cmbModel.Items.Count > 0) cmbModel.SelectedIndex = 0;
            if (cmbType.Items.Count > 0) cmbType.SelectedIndex = 0;
            if (cmbArea.Items.Count > 0) cmbArea.SelectedIndex = 0;
            if (cmbResult.Items.Count > 0) cmbResult.SelectedIndex = 0;
            if (dtpStart != null) dtpStart.Value = DateTime.Today.AddDays(-7);
            if (dtpEnd != null) dtpEnd.Value = DateTime.Today;
            gridReports.CellFormatting += GridReports_CellFormatting;
            SetupEvents();
        }

        private void LoadDummyData()
        {
            // ----------------------------------------------------
            // 1. Setup Line Chart (Result Trend)
            // ----------------------------------------------------
            chartTrend.ChartAreas.Clear();
            chartTrend.Series.Clear();
            chartTrend.Legends.Clear();

            ChartArea areaTrend = new("TrendArea")
            {
                BackColor = Color.White
            };
            areaTrend.AxisX.MajorGrid.Enabled = false;
            areaTrend.AxisX.Interval = 1;
            areaTrend.AxisX.LabelStyle.Font = new("Segoe UI", 8.5F);
            areaTrend.AxisX.LabelStyle.ForeColor = Color.FromArgb(100, 116, 139);
            areaTrend.AxisX.LineColor = Color.FromArgb(226, 232, 240);

            areaTrend.AxisY.MajorGrid.LineColor = Color.FromArgb(241, 245, 249);
            areaTrend.AxisY.LabelStyle.Font = new("Segoe UI", 8.5F);
            areaTrend.AxisY.LabelStyle.ForeColor = Color.FromArgb(100, 116, 139);
            areaTrend.AxisY.LineColor = Color.Transparent;
            areaTrend.AxisY.Minimum = 85;
            areaTrend.AxisY.Maximum = 100;
            areaTrend.AxisY.Interval = 5;
            chartTrend.ChartAreas.Add(areaTrend);

            Legend legTrend = new("TrendLegend")
            {
                Docking = Docking.Top,
                Alignment = StringAlignment.Center,
                Font = new("Segoe UI", 8.5F, FontStyle.Bold)
            };
            chartTrend.Legends.Add(legTrend);

            Series sPassRate = new("Pass Rate (%)")
            {
                ChartType = SeriesChartType.Spline,
                Color = Color.FromArgb(34, 197, 94), // Modern Green
                BorderWidth = 3,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 7,
                MarkerColor = Color.FromArgb(22, 101, 52),
                IsXValueIndexed = true
            };
            sPassRate["LabelStyle"] = "Top";

            Series sTarget = new("Target Yield (%)")
            {
                ChartType = SeriesChartType.Spline,
                Color = Color.FromArgb(59, 130, 246), // Modern Blue
                BorderWidth = 2,
                BorderDashStyle = ChartDashStyle.Dash,
                MarkerStyle = MarkerStyle.Diamond,
                MarkerSize = 6,
                MarkerColor = Color.FromArgb(30, 64, 175),
                IsXValueIndexed = true
            };

            double[] passRates = [96.5, 98.0, 95.2, 98.8, 97.5, 98.5, 99.1];

            for (int i = 0; i < passRates.Length; i++)
            {
                string dateLabel = DateTime.Now.AddDays(-6 + i).ToString("dd MMM");
                int idx = sPassRate.Points.AddXY(dateLabel, passRates[i]);
                sPassRate.Points[idx].Label = passRates[i].ToString() + "%";
                sPassRate.Points[idx].Font = new("Segoe UI", 8F, FontStyle.Bold);
                sPassRate.Points[idx].LabelForeColor = Color.FromArgb(22, 101, 52);

                sTarget.Points.AddXY(dateLabel, 97.0);
            }

            chartTrend.Series.Add(sPassRate);
            chartTrend.Series.Add(sTarget);

            // ----------------------------------------------------
            // 2. Setup DataGridView (Reports List)
            // ----------------------------------------------------
            gridReports.Rows.Clear();

            object[][] rows = [
                ["1", "Daily Production Summary - Shift 1", "Summary", "15 Mar 2026, 16:00", "System Auto", "1.4 MB", "📥 Download / 👁 View"],
                ["2", "Wiring Harness Continuity Test Log", "Log", "15 Mar 2026, 14:15", "Fally", "840 KB", "📥 Download / 👁 View"],
                ["3", "Defect & Yield Analysis - Model X", "Analysis", "15 Mar 2026, 12:00", "QA Team", "2.1 MB", "📥 Download / 👁 View"],
                ["4", "Weekly Quality Assurance Audit", "Audit", "14 Mar 2026, 17:30", "Eng Lead", "3.4 MB", "📥 Download / 👁 View"],
                ["5", "Main Line Station Status Report", "Summary", "14 Mar 2026, 15:10", "System Auto", "950 KB", "📥 Download / 👁 View"],
                ["6", "Harness Voltage Drop Inspection Log", "Log", "13 Mar 2026, 16:45", "Fally", "1.5 MB", "📥 Download / 👁 View"],
                ["7", "Quality Control Trend Verification", "Analysis", "13 Mar 2026, 11:20", "QA Team", "1.8 MB", "📥 Download / 👁 View"],
                ["8", "Monthly Equipment Calibration Record", "Audit", "12 Mar 2026, 09:00", "Maint Dept", "4.2 MB", "📥 Download / 👁 View"],
                ["9", "Batch #9821 Full Test Protocol Log", "Log", "11 Mar 2026, 14:50", "System Auto", "620 KB", "📥 Download / 👁 View"],
                ["10", "Operator Performance & Throughput", "Summary", "10 Mar 2026, 16:30", "Prod Mgr", "1.1 MB", "📥 Download / 👁 View"]
            ];

            foreach (var r in rows)
            {
                gridReports.Rows.Add(r);
            }
        }

        private void GridReports_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value is null || e.CellStyle is null) return;

            string colName = gridReports.Columns[e.ColumnIndex].Name;

            if (colName == "colReportType")
            {
                string? val = e.Value.ToString();
                if (val == "Summary")
                {
                    e.CellStyle.BackColor = Color.FromArgb(224, 242, 254);
                    e.CellStyle.ForeColor = Color.FromArgb(3, 105, 161);
                    e.CellStyle.Font = new("Segoe UI", 8.5F, FontStyle.Bold);
                }
                else if (val == "Log")
                {
                    e.CellStyle.BackColor = Color.FromArgb(241, 245, 249);
                    e.CellStyle.ForeColor = Color.FromArgb(51, 65, 85);
                    e.CellStyle.Font = new("Segoe UI", 8.5F, FontStyle.Bold);
                }
                else if (val == "Analysis")
                {
                    e.CellStyle.BackColor = Color.FromArgb(243, 232, 255);
                    e.CellStyle.ForeColor = Color.FromArgb(126, 34, 206);
                    e.CellStyle.Font = new("Segoe UI", 8.5F, FontStyle.Bold);
                }
                else if (val == "Audit")
                {
                    e.CellStyle.BackColor = Color.FromArgb(254, 237, 216);
                    e.CellStyle.ForeColor = Color.FromArgb(194, 65, 12);
                    e.CellStyle.Font = new("Segoe UI", 8.5F, FontStyle.Bold);
                }
            }
            else if (colName == "colAction")
            {
                e.CellStyle.ForeColor = Color.FromArgb(37, 99, 235);
                e.CellStyle.Font = new("Segoe UI", 8.5F, FontStyle.Bold);
            }
        }

        private void SetupEvents()
        {
            SetupCardRoundedEdges(cardFilters, 12, Color.FromArgb(34, 40, 49), 2.5f);
            SetupCardRoundedEdges(cardTrend, 12, Color.FromArgb(34, 40, 49), 2.5f);
            SetupCardRoundedEdges(cardReportList, 12, Color.FromArgb(34, 40, 49), 2.5f);

            SetupCardRoundedEdges(pnlSumTotal, 10, Color.FromArgb(34, 40, 49), 2.5f);
            SetupCardRoundedEdges(pnlSumPass, 10, Color.FromArgb(34, 40, 49), 2.5f);
            SetupCardRoundedEdges(pnlSumFail, 10, Color.FromArgb(34, 40, 49), 2.5f);
            SetupCardRoundedEdges(pnlSumProg, 10, Color.FromArgb(34, 40, 49), 2.5f);

            SetupCardRoundedEdges(pnlPreviewContainer, 12, Color.FromArgb(34, 40, 49), 2.5f);
            SetupCardRoundedEdges(cardPaper, 10, Color.FromArgb(34, 40, 49), 2.5f);
            SetupCardRoundedEdges(pnlGenInfoBox, 8, Color.FromArgb(34, 40, 49), 2.5f);
            SetupCardRoundedEdges(pnlNotesCard, 8, Color.FromArgb(34, 40, 49), 2.5f);

            SetupButtonRoundedEdges(btnGenerate, 10, Color.FromArgb(250, 204, 21), Color.FromArgb(254, 216, 53), Color.FromArgb(234, 186, 10), Color.Transparent);
            SetupButtonRoundedEdges(btnReset, 10, Color.FromArgb(241, 245, 249), Color.FromArgb(226, 232, 240), Color.FromArgb(203, 213, 225), Color.Transparent);

            SetupButtonRoundedEdges(btnExportPDF, 10, Color.FromArgb(250, 204, 21), Color.FromArgb(254, 216, 53), Color.FromArgb(234, 186, 10), Color.Transparent);
            SetupButtonRoundedEdges(btnExportExcel, 10, Color.White, Color.FromArgb(248, 250, 252), Color.FromArgb(241, 245, 249), Color.Transparent);
            SetupButtonRoundedEdges(btnPrint, 10, Color.White, Color.FromArgb(248, 250, 252), Color.FromArgb(241, 245, 249), Color.Transparent);

            SetupButtonRoundedEdges(btnPageFirst, 8, Color.White, Color.FromArgb(248, 250, 252), Color.FromArgb(241, 245, 249), Color.Transparent);
            SetupButtonRoundedEdges(btnPagePrev, 8, Color.White, Color.FromArgb(248, 250, 252), Color.FromArgb(241, 245, 249), Color.Transparent);
            SetupButtonRoundedEdges(btnPage1, 8, Color.FromArgb(250, 204, 21), Color.FromArgb(254, 216, 53), Color.FromArgb(234, 186, 10), Color.Transparent);
            SetupButtonRoundedEdges(btnPage2, 8, Color.White, Color.FromArgb(248, 250, 252), Color.FromArgb(241, 245, 249), Color.Transparent);
            SetupButtonRoundedEdges(btnPageDots, 8, Color.FromArgb(248, 250, 252), Color.FromArgb(248, 250, 252), Color.FromArgb(248, 250, 252), Color.Transparent);
            SetupButtonRoundedEdges(btnPageNext, 8, Color.White, Color.FromArgb(248, 250, 252), Color.FromArgb(241, 245, 249), Color.Transparent);
            SetupButtonRoundedEdges(btnPageLast, 8, Color.White, Color.FromArgb(248, 250, 252), Color.FromArgb(241, 245, 249), Color.Transparent);
        }

        private static void SetupCardRoundedEdges(Control card, int radius, Color borderColor, float borderWidth = 2.5f)
        {
            if (card is TableLayoutPanel || card.Padding.Left < 4 || card.Padding.Top < 4 || card.Padding.Right < 4 || card.Padding.Bottom < 4)
            {
                card.Padding = new Padding(
                    Math.Max(card.Padding.Left, 4),
                    Math.Max(card.Padding.Top, 4),
                    Math.Max(card.Padding.Right, 4),
                    Math.Max(card.Padding.Bottom, 4)
                );
            }

            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                Color parentBg = card.Parent?.BackColor != null && card.Parent.BackColor != Color.Transparent
                    ? card.Parent.BackColor
                    : Color.FromArgb(244, 245, 247);

                using (var parentBrush = new SolidBrush(parentBg))
                {
                    e.Graphics.FillRectangle(parentBrush, 0, 0, card.Width, card.Height);
                }

                var rectOuter = new Rectangle(0, 0, card.Width, card.Height);
                using var pathOuter = GetRoundedPath(rectOuter, radius);
                using (var cardBrush = new SolidBrush(card.BackColor))
                {
                    e.Graphics.FillPath(cardBrush, pathOuter);
                }

                var rectBorder = new Rectangle(1, 1, card.Width - 3, card.Height - 3);
                using var pathBorder = GetRoundedPath(rectBorder, radius);
                using var pen = new Pen(borderColor, borderWidth);
                e.Graphics.DrawPath(pen, pathBorder);
            };

            card.Resize += (s, e) =>
            {
                if (card.Width > 0 && card.Height > 0)
                {
                    var oldRegion = card.Region;
                    card.Region = new Region(GetRoundedPath(new Rectangle(0, 0, card.Width, card.Height), radius));
                    oldRegion?.Dispose();
                    card.Invalidate();
                }
            };

            if (card.Width > 0 && card.Height > 0)
            {
                card.Region = new Region(GetRoundedPath(new Rectangle(0, 0, card.Width, card.Height), radius));
            }
        }

        private static void SetupButtonRoundedEdges(Button btn, int radius, Color normColor, Color hoverColor, Color pressColor, Color borderColor)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = normColor;
            btn.Cursor = Cursors.Hand;

            btn.MouseEnter += (s, e) => { btn.BackColor = hoverColor; btn.Invalidate(); };
            btn.MouseLeave += (s, e) => { btn.BackColor = normColor; btn.Invalidate(); };
            btn.MouseDown += (s, e) => { btn.BackColor = pressColor; btn.Invalidate(); };
            btn.MouseUp += (s, e) => { btn.BackColor = hoverColor; btn.Invalidate(); };

            btn.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                Color parentBg = btn.Parent?.BackColor != null && btn.Parent.BackColor != Color.Transparent
                    ? btn.Parent.BackColor
                    : Color.White;

                using (var parentBrush = new SolidBrush(parentBg))
                {
                    e.Graphics.FillRectangle(parentBrush, 0, 0, btn.Width, btn.Height);
                }

                var rectOuter = new Rectangle(0, 0, btn.Width, btn.Height);
                using var pathOuter = GetRoundedPath(rectOuter, radius);
                using (var brush = new SolidBrush(btn.BackColor))
                {
                    e.Graphics.FillPath(brush, pathOuter);
                }

                if (borderColor != Color.Transparent)
                {
                    var rectBorder = new Rectangle(1, 1, btn.Width - 3, btn.Height - 3);
                    using var pathBorder = GetRoundedPath(rectBorder, radius);
                    using var pen = new Pen(borderColor, 1.5f);
                    e.Graphics.DrawPath(pen, pathBorder);
                }

                TextRenderer.DrawText(
                    e.Graphics,
                    btn.Text,
                    btn.Font,
                    rectOuter,
                    btn.ForeColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak
                );
            };

            btn.Resize += (s, e) =>
            {
                if (btn.Width > 0 && btn.Height > 0)
                {
                    var oldRegion = btn.Region;
                    btn.Region = new Region(GetRoundedPath(new Rectangle(0, 0, btn.Width, btn.Height), radius));
                    oldRegion?.Dispose();
                    btn.Invalidate();
                }
            };

            if (btn.Width > 0 && btn.Height > 0)
            {
                btn.Region = new Region(GetRoundedPath(new Rectangle(0, 0, btn.Width, btn.Height), radius));
            }
        }

        private static GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new();
            int d = radius * 2;
            if (rect.Width <= 0 || rect.Height <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }
            if (d > rect.Width) d = rect.Width;
            if (d > rect.Height) d = rect.Height;

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
