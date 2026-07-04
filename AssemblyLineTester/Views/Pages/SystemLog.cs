using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace AssemblyLineTester.Views.Pages
{
    public partial class SystemLog : UserControl
    {
        public SystemLog()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(248, 250, 252);
            panelFilters.Paint += PanelFilters_Paint;
            pnlLeft.Paint += PanelFilters_Paint;
            pnlSummary.Paint += PanelFilters_Paint;
            if (pnlChartContainer != null)
            {
                pnlChartContainer.Paint += PnlChartContainer_Paint;
            }
            pnlLevelStats.Paint += PnlLevelStats_Paint;
            pnlModuleStats.Paint += PnlModuleStats_Paint;
            pnlLogDetail.Paint += PanelFilters_Paint;

            gridData.CellPainting += gridData_CellPainting;
            btnRefresh.Click += (s, ev) => { LoadDummyLogData(); };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                LoadDummyLogData();
            }
        }

        public void LoadDummyLogData()
        {
            gridData.Columns.Clear();
            gridData.Rows.Clear();

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
            gridData.Columns.Add("Date", "Date & Time ↓");
            gridData.Columns.Add("Level", "Log Level");
            gridData.Columns.Add("Module", "Module");
            gridData.Columns.Add("User", "User");
            gridData.Columns.Add("Action", "Action");
            gridData.Columns.Add("Description", "Description");
            gridData.Columns.Add("IP", "IP Address");

            SetColumnWidth(gridData, "No", 45, DataGridViewContentAlignment.MiddleCenter);
            SetColumnWidth(gridData, "Date", 165);
            SetColumnWidth(gridData, "Level", 100, DataGridViewContentAlignment.MiddleCenter);
            SetColumnWidth(gridData, "Module", 135);
            SetColumnWidth(gridData, "User", 85);
            SetColumnWidth(gridData, "Action", 120);
            SetColumnWidth(gridData, "Description", 260);
            SetColumnWidth(gridData, "IP", 130);

            gridData.Rows.Add("1", "15 Mar 2026 10:30:20 AM", "INFO", "Test Execution", "Fally", "Start Test", "Test started for Part Number WH-ENG-001", "192.168.1.105   ⋮");
            gridData.Rows.Add("2", "15 Mar 2026 10:30:22 AM", "INFO", "Test Execution", "Fally", "Load Program", "Test program loaded successfully", "192.168.1.105   ⋮");
            gridData.Rows.Add("3", "15 Mar 2026 10:30:35 AM", "INFO", "Test Execution", "Fally", "Step Complete", "Step \"Continuity Test\" completed", "192.168.1.105   ⋮");
            gridData.Rows.Add("4", "15 Mar 2026 10:31:10 AM", "INFO", "Test Execution", "Fally", "Step Complete", "Step \"Short Circuit Test\" completed", "192.168.1.105   ⋮");
            gridData.Rows.Add("5", "15 Mar 2026 10:31:45 AM", "INFO", "Test Execution", "Fally", "Step Complete", "Step \"Insulation Test\" completed", "192.168.1.105   ⋮");
            gridData.Rows.Add("6", "15 Mar 2026 10:32:15 AM", "INFO", "Test Execution", "Fally", "Step Complete", "Step \"Functional Test\" completed", "192.168.1.105   ⋮");
            gridData.Rows.Add("7", "15 Mar 2026 10:32:21 AM", "INFO", "Test Execution", "Fally", "Test Complete", "Test completed with result: PASS", "192.168.1.105   ⋮");
            gridData.Rows.Add("8", "15 Mar 2026 10:32:25 AM", "WARNING", "Harness Database", "Fally", "Update", "ECN revision updated for WH-ENG-001", "192.168.1.105   ⋮");
            gridData.Rows.Add("9", "15 Mar 2026 10:35:02 AM", "ERROR", "Communication", "System", "Connection Lost", "Lost connection with PLC at 192.168.1.20", "192.168.1.100   ⋮");
            gridData.Rows.Add("10", "15 Mar 2026 10:35:07 AM", "INFO", "Communication", "System", "Reconnected", "PLC reconnected successfully", "192.168.1.100   ⋮");
            gridData.Rows.Add("11", "15 Mar 2026 10:40:11 AM", "INFO", "User Management", "Fally", "Login", "User login success", "192.168.1.105   ⋮");
            gridData.Rows.Add("12", "15 Mar 2026 10:45:33 AM", "WARNING", "Database", "System", "Backup", "Database auto backup completed", "192.168.1.100   ⋮");

            if (lblLegInfo != null) lblLegInfo.Text = "INFO : 1,142 (90.8%)";
            if (lblLegWarn != null) lblLegWarn.Text = "WARNING : 84 (6.7%)";
            if (lblLegError != null) lblLegError.Text = "ERROR : 32 (2.5%)";
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

        private void gridData_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && gridData.Columns[e.ColumnIndex].Name == "Level" && e.Value is not null && e.Graphics is not null)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                string val = e.Value.ToString() ?? "";
                Color bgCol = Color.FromArgb(220, 252, 231);
                Color fgCol = Color.FromArgb(22, 101, 52);

                if (val == "WARNING")
                {
                    bgCol = Color.FromArgb(254, 240, 138);
                    fgCol = Color.FromArgb(133, 77, 14);
                }
                else if (val == "ERROR")
                {
                    bgCol = Color.FromArgb(254, 226, 226);
                    fgCol = Color.FromArgb(153, 27, 27);
                }

                Rectangle badgeRect = new Rectangle(e.CellBounds.X + 12, e.CellBounds.Y + 6, e.CellBounds.Width - 24, e.CellBounds.Height - 12);
                using (var brush = new SolidBrush(bgCol))
                using (var path = GetRoundedRectPath(badgeRect, 10))
                {
                    e.Graphics.FillPath(brush, path);
                }

                TextRenderer.DrawText(e.Graphics, val, new Font("Segoe UI", 8.5F, FontStyle.Bold), badgeRect, fgCol, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                e.Handled = true;
            }
        }

        private void PanelFilters_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Control card) return;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, card.Width - 1, card.Height - 1);
            using var path = GetRoundedRectPath(rect, 8);
            using var pen = new Pen(Color.FromArgb(226, 232, 240), 1);
            e.Graphics.DrawPath(pen, path);
        }

        private void PnlChartContainer_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Control container) return;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int size = Math.Min(container.Width, container.Height) - 20;
            if (size <= 0) return;
            int x = (container.Width - size) / 2;
            int y = (container.Height - size) / 2;
            var rect = new Rectangle(x, y, size, size);

            // Doughnut: ~91% INFO (Blue), ~7% WARN (Amber), ~2% ERROR (Red)
            using (var penInfo = new Pen(Color.FromArgb(59, 130, 246), 18))
            using (var penWarn = new Pen(Color.FromArgb(245, 158, 11), 18))
            using (var penErr = new Pen(Color.FromArgb(239, 68, 68), 18))
            {
                e.Graphics.DrawArc(penInfo, rect, -90, 327);
                e.Graphics.DrawArc(penWarn, rect, 237, 24);
                e.Graphics.DrawArc(penErr, rect, 261, 9);
            }

            string text = "1,258\nTOTAL";
            using (var font = new Font("Segoe UI", 9.5F, FontStyle.Bold))
            using (var brush = new SolidBrush(Color.FromArgb(30, 41, 59)))
            using (var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                e.Graphics.DrawString(text, font, brush, new RectangleF(x, y, size, size), format);
            }
        }

        private void PnlLevelStats_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Control card) return;
            PanelFilters_Paint(sender, e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int startY = 75;
            int rowStep = 65;
            int leftMargin = 36;
            int w = card.Width - (leftMargin * 2);
            if (w <= 0) return;

            DrawStatRow(e.Graphics, leftMargin, startY, w, "INFO", "1,142 (90.8%)", 0.908f, Color.FromArgb(59, 130, 246));
            DrawStatRow(e.Graphics, leftMargin, startY + rowStep, w, "WARNING", "84 (6.7%)", 0.067f, Color.FromArgb(245, 158, 11));
            DrawStatRow(e.Graphics, leftMargin, startY + (rowStep * 2), w, "ERROR", "32 (2.5%)", 0.025f, Color.FromArgb(239, 68, 68));
        }

        private void DrawStatRow(Graphics g, int x, int y, int width, string label, string val, float pct, Color barColor)
        {
            using (var fontLabel = new Font("Segoe UI", 9F, FontStyle.Bold))
            using (var fontVal = new Font("Segoe UI", 9F))
            using (var textBrush = new SolidBrush(Color.FromArgb(30, 41, 59)))
            using (var valBrush = new SolidBrush(Color.FromArgb(100, 116, 139)))
            using (var trackBrush = new SolidBrush(Color.FromArgb(241, 245, 249)))
            using (var barBrush = new SolidBrush(barColor))
            {
                g.DrawString(label, fontLabel, textBrush, x, y);
                var valSize = g.MeasureString(val, fontVal);
                g.DrawString(val, fontVal, valBrush, x + width - (int)valSize.Width, y);

                var labelSize = g.MeasureString(label, fontLabel);
                int barTop = y + (int)labelSize.Height + 4;
                int barHeight = 6;

                Rectangle trackRect = new Rectangle(x, barTop, width, barHeight);
                using (var trackPath = GetRoundedRectPath(trackRect, 3))
                {
                    g.FillPath(trackBrush, trackPath);
                }

                int barWidth = Math.Max(8, (int)(width * pct));
                Rectangle barRect = new Rectangle(x, barTop, barWidth, barHeight);
                using (var barPath = GetRoundedRectPath(barRect, 3))
                {
                    g.FillPath(barBrush, barPath);
                }
            }
        }

        private void PnlModuleStats_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Control card) return;
            PanelFilters_Paint(sender, e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int startY = 65;
            int leftMargin = 36;
            int w = card.Width - (leftMargin * 2);
            if (w <= 0) return;

            string[] names = { "Test Execution", "Communication", "Harness Database", "Database", "User Management", "System" };
            string[] counts = { "542 (43%)", "318 (25%)", "185 (15%)", "124 (10%)", "56 (4%)", "33 (3%)" };
            float[] pcts = { 0.43f, 0.25f, 0.15f, 0.10f, 0.04f, 0.03f };

            int availableHeight = card.Height - startY - 20;
            int rowStep = Math.Max(38, availableHeight / names.Length);

            for (int i = 0; i < names.Length; i++)
            {
                DrawStatRow(e.Graphics, leftMargin, startY + (i * rowStep), w, names[i], counts[i], pcts[i], Color.FromArgb(99, 102, 241));
            }
        }

        private GraphicsPath GetRoundedRectPath(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            var size = new Size(diameter, diameter);
            var arc = new Rectangle(bounds.Location, size);
            var path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            path.AddArc(arc, 180, 90);
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
