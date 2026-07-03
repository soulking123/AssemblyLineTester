using System;
using System.Drawing;
using System.Windows.Forms;

namespace AssemblyLineTester.Views.Pages
{
    public partial class TestHistory : UserControl
    {
        public TestHistory()
        {
            InitializeComponent();
            SetupEvents();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                LoadDummyData();
                LoadDummyStepsData();
                InitSummaryChart();
                if (cmbPageSize.Items.Count > 0) cmbPageSize.SelectedIndex = 0;
            }
        }

        private void LogoBox_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using var pen = new Pen(Color.FromArgb(15, 23, 42), 2.2f);
            Rectangle rect = new Rectangle(4, 4, 22, 22);
            e.Graphics.DrawEllipse(pen, rect);
            e.Graphics.DrawLine(pen, 15, 15, 15, 9);
            e.Graphics.DrawLine(pen, 15, 15, 19, 17);
        }

        private System.Drawing.Drawing2D.GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            int d = radius * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void SetupEvents()
        {
            pnlFilterCard.Paint += pnlFilterCard_Paint;
            gridData.CellFormatting += gridData_CellFormatting;
            gridData.CellPainting += gridData_CellPainting;
            gridSteps.CellFormatting += gridSteps_CellFormatting;
            btnRefresh.Click += (s, ev) => { LoadDummyData(); LoadDummyStepsData(); };
        }

        private void pnlFilterCard_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using var pen = new Pen(Color.FromArgb(226, 232, 240), 1.2f);
            using var path = GetRoundedPath(new Rectangle(0, 0, pnlFilterCard.Width - 1, pnlFilterCard.Height - 1), 8);
            e.Graphics.DrawPath(pen, path);
        }

        private void SetColumnWidth(DataGridView grid, string colName, float fillWeight, DataGridViewContentAlignment alignment = DataGridViewContentAlignment.MiddleLeft)
        {
            var col = grid.Columns[colName];
            if (col != null)
            {
                col.FillWeight = fillWeight;
                col.DefaultCellStyle.Alignment = alignment;
            }
        }

        public void LoadDummyData()
        {
            gridData.Columns.Clear();
            gridData.Rows.Clear();

            // Setup DataGridView styling
            gridData.EnableHeadersVisualStyles = false;
            gridData.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            gridData.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(71, 85, 105);
            gridData.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            gridData.ColumnHeadersHeight = 44;
            gridData.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            gridData.DefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);
            gridData.DefaultCellStyle.BackColor = Color.White;
            gridData.DefaultCellStyle.SelectionBackColor = Color.FromArgb(239, 246, 255);
            gridData.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);
            gridData.RowTemplate.Height = 40;

            gridData.Columns.Add("No", "No.");
            gridData.Columns.Add("TestTime", "Test Time ↓");
            gridData.Columns.Add("ProductModel", "Product Model");
            gridData.Columns.Add("ProductType", "Product Type");
            gridData.Columns.Add("Area", "Area");
            gridData.Columns.Add("PartNumber", "Part Number");
            gridData.Columns.Add("SerialNumber", "Serial Number");
            gridData.Columns.Add("Result", "Result");
            gridData.Columns.Add("TotalTime", "Total Time");
            gridData.Columns.Add("Operator", "Operator");
            gridData.Columns.Add("Action", "Action");

            SetColumnWidth(gridData, "No", 40, DataGridViewContentAlignment.MiddleCenter);
            SetColumnWidth(gridData, "TestTime", 155);
            SetColumnWidth(gridData, "ProductModel", 85);
            SetColumnWidth(gridData, "ProductType", 85, DataGridViewContentAlignment.MiddleCenter);
            SetColumnWidth(gridData, "Area", 135);
            SetColumnWidth(gridData, "PartNumber", 115);
            SetColumnWidth(gridData, "SerialNumber", 100);
            SetColumnWidth(gridData, "Result", 85, DataGridViewContentAlignment.MiddleCenter);
            SetColumnWidth(gridData, "TotalTime", 90, DataGridViewContentAlignment.MiddleCenter);
            SetColumnWidth(gridData, "Operator", 85);
            SetColumnWidth(gridData, "Action", 60, DataGridViewContentAlignment.MiddleCenter);

            gridData.Rows.Add("1", "15 Mar 2026 10:30:20 AM", "6060", "AC", "Engine Control", "WH-ENG-001", "SN12350", "PASS", "00:09:01", "Fally", "⋮");
            gridData.Rows.Add("2", "15 Mar 2026 10:28:15 AM", "6060", "AC", "Engine Control", "WH-ENG-001", "SN12349", "PASS", "00:08:45", "Fally", "⋮");
            gridData.Rows.Add("3", "15 Mar 2026 10:26:18 AM", "6060", "AC", "Engine Control", "WH-ENG-001", "SN12348", "FAIL", "00:10:32", "Fally", "⋮");
            gridData.Rows.Add("4", "15 Mar 2026 10:24:11 AM", "6060", "AC", "Cabin", "WH-CAB-002", "SN12347", "PASS", "00:08:56", "Fally", "⋮");
            gridData.Rows.Add("5", "15 Mar 2026 10:22:03 AM", "6060", "AC", "Main Frame", "WH-MF-003", "SN12346", "PASS", "00:07:58", "Fally", "⋮");
            gridData.Rows.Add("6", "15 Mar 2026 10:20:15 AM", "6060", "AC", "Boom", "WH-BOOM-004", "SN12345", "PASS", "00:09:12", "Fally", "⋮");
            gridData.Rows.Add("7", "15 Mar 2026 10:18:22 AM", "6060", "AC", "Power Module", "WH-PM-005", "SN12344", "PASS", "00:08:30", "Fally", "⋮");
            gridData.Rows.Add("8", "15 Mar 2026 10:16:10 AM", "6060", "AC", "Lighting", "WH-LTG-006", "SN12343", "FAIL", "00:11:02", "Fally", "⋮");
            gridData.Rows.Add("9", "15 Mar 2026 10:14:05 AM", "6060", "AC", "Hydraulic Control", "WH-HYD-007", "SN12342", "PASS", "00:09:05", "Fally", "⋮");
            gridData.Rows.Add("10", "15 Mar 2026 10:12:20 AM", "6060", "AC", "Monitoring System", "WH-MON-008", "SN12341", "PASS", "00:08:40", "Fally", "⋮");
        }

        public void LoadDummyStepsData()
        {
            gridSteps.Columns.Clear();
            gridSteps.Rows.Clear();

            gridSteps.EnableHeadersVisualStyles = false;
            gridSteps.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            gridSteps.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(71, 85, 105);
            gridSteps.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            gridSteps.ColumnHeadersHeight = 38;
            gridSteps.RowTemplate.Height = 36;
            gridSteps.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            gridSteps.DefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);
            gridSteps.DefaultCellStyle.BackColor = Color.White;
            gridSteps.DefaultCellStyle.SelectionBackColor = Color.FromArgb(239, 246, 255);
            gridSteps.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);

            gridSteps.Columns.Add("StepNo", "Step No.");
            gridSteps.Columns.Add("TestStep", "Test Step");
            gridSteps.Columns.Add("Result", "Result");
            gridSteps.Columns.Add("Time", "Time");

            SetColumnWidth(gridSteps, "StepNo", 45, DataGridViewContentAlignment.MiddleCenter);
            SetColumnWidth(gridSteps, "TestStep", 150);
            SetColumnWidth(gridSteps, "Result", 70, DataGridViewContentAlignment.MiddleCenter);
            SetColumnWidth(gridSteps, "Time", 75, DataGridViewContentAlignment.MiddleCenter);

            gridSteps.Rows.Add("1", "Continuity Test", "PASS", "00:01:42");
            gridSteps.Rows.Add("2", "Short Circuit Test", "PASS", "00:01:35");
            gridSteps.Rows.Add("3", "Insulation Test", "PASS", "00:01:28");
            gridSteps.Rows.Add("4", "Functional Test", "PASS", "00:02:15");
            gridSteps.Rows.Add("5", "Final Verification", "PASS", "00:02:01");
        }

        private void InitSummaryChart()
        {
            pnlChartContainer.Controls.Clear();
            var pnlCustomChart = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            pnlCustomChart.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                int size = Math.Min(pnlCustomChart.Width, pnlCustomChart.Height) - 20;
                if (size <= 0) return;
                int x = (pnlCustomChart.Width - size) / 2;
                int y = (pnlCustomChart.Height - size) / 2;
                var rect = new Rectangle(x, y, size, size);

                // Doughnut: 80% Pass (Green), 20% Fail (Red)
                using (var penGreen = new Pen(Color.FromArgb(76, 175, 80), 18))
                using (var penRed = new Pen(Color.FromArgb(244, 67, 54), 18))
                {
                    e.Graphics.DrawArc(penGreen, rect, -90, 288);
                    e.Graphics.DrawArc(penRed, rect, 198, 72);
                }

                string text = "5\nTOTAL";
                using (var font = new Font("Segoe UI", 9.5F, FontStyle.Bold))
                using (var brush = new SolidBrush(Color.FromArgb(30, 41, 59)))
                using (var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                {
                    e.Graphics.DrawString(text, font, brush, new RectangleF(x, y, size, size), format);
                }
            };
            pnlChartContainer.Controls.Add(pnlCustomChart);
        }

        public void gridData_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && gridData.Columns[e.ColumnIndex].Name == "Result" && e.Value != null)
            {
                // Formatting handled by CellPainting for custom capsule shapes
            }
        }

        private void gridData_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && gridData.Columns[e.ColumnIndex].Name == "Result" && e.Value is not null && e.Graphics is not null)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                string val = e.Value.ToString() ?? "";
                Color bgCol = Color.FromArgb(209, 231, 221);
                Color fgCol = Color.FromArgb(15, 81, 50);

                if (val == "FAIL")
                {
                    bgCol = Color.FromArgb(248, 215, 218);
                    fgCol = Color.FromArgb(132, 32, 41);
                }

                Rectangle badgeRect = new Rectangle(e.CellBounds.X + 8, e.CellBounds.Y + 7, e.CellBounds.Width - 16, e.CellBounds.Height - 14);
                using (var brush = new SolidBrush(bgCol))
                using (var path = GetRoundedPath(badgeRect, 10))
                {
                    e.Graphics.FillPath(brush, path);
                }

                TextRenderer.DrawText(e.Graphics, val, new Font("Segoe UI", 8.5F, FontStyle.Bold), badgeRect, fgCol, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                e.Handled = true;
            }
        }

        public void gridSteps_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && gridSteps.Columns[e.ColumnIndex].Name == "Result" && e.Value != null)
            {
                string val = e.Value.ToString() ?? "";
                if (val == "PASS")
                {
                    e.Value = "✔ PASS";
                    e.CellStyle.ForeColor = Color.FromArgb(22, 101, 52);
                    e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                    e.FormattingApplied = true;
                }
                else if (val == "FAIL")
                {
                    e.Value = "✖ FAIL";
                    e.CellStyle.ForeColor = Color.FromArgb(153, 27, 27);
                    e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                    e.FormattingApplied = true;
                }
            }
        }
    }
}
