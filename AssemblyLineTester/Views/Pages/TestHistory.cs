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

        private void SetupEvents()
        {
            gridData.CellFormatting += gridData_CellFormatting;
            gridSteps.CellFormatting += gridSteps_CellFormatting;
            btnRefresh.Click += (s, ev) => { LoadDummyData(); LoadDummyStepsData(); };
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
            gridData.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            gridData.ColumnHeadersHeight = 52;
            gridData.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            gridData.DefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);
            gridData.DefaultCellStyle.BackColor = Color.White;
            gridData.DefaultCellStyle.SelectionBackColor = Color.FromArgb(239, 246, 255);
            gridData.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);

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

            SetColumnWidth(gridData, "No", 45, DataGridViewContentAlignment.MiddleCenter);
            SetColumnWidth(gridData, "TestTime", 160);
            SetColumnWidth(gridData, "ProductModel", 90);
            SetColumnWidth(gridData, "ProductType", 130);
            SetColumnWidth(gridData, "Area", 140);
            SetColumnWidth(gridData, "PartNumber", 130);
            SetColumnWidth(gridData, "SerialNumber", 110);
            SetColumnWidth(gridData, "Result", 90, DataGridViewContentAlignment.MiddleCenter);
            SetColumnWidth(gridData, "TotalTime", 100, DataGridViewContentAlignment.MiddleCenter);
            SetColumnWidth(gridData, "Operator", 100);
            SetColumnWidth(gridData, "Action", 90, DataGridViewContentAlignment.MiddleCenter);

            gridData.Rows.Add("1", "15 Mar 2026 10:30:20 AM", "6060", "AC", "Engine Control", "WH-ENG-001", "SN12350", "PASS", "00:09:01", "Fally", "👁️ View");
            gridData.Rows.Add("2", "15 Mar 2026 10:28:15 AM", "6060", "AC", "Engine Control", "WH-ENG-001", "SN12349", "PASS", "00:08:45", "Fally", "👁️ View");
            gridData.Rows.Add("3", "15 Mar 2026 10:26:18 AM", "6060", "AC", "Engine Control", "WH-ENG-001", "SN12348", "FAIL", "00:10:32", "Fally", "👁️ View");
            gridData.Rows.Add("4", "15 Mar 2026 10:24:10 AM", "6060", "DC", "Power Distribution", "WH-PWR-002", "SN12347", "PASS", "00:07:50", "Hendra", "👁️ View");
            gridData.Rows.Add("5", "15 Mar 2026 10:21:05 AM", "797F", "AC", "Engine Control", "WH-ENG-002", "SN12346", "PASS", "00:11:20", "Budi", "👁️ View");
            gridData.Rows.Add("6", "15 Mar 2026 10:18:50 AM", "797F", "DC", "Lighting Harness", "WH-LGT-001", "SN12345", "FAIL", "00:12:05", "Budi", "👁️ View");
            gridData.Rows.Add("7", "15 Mar 2026 10:15:30 AM", "D11T", "AC", "Hydraulic Harness", "WH-HYD-003", "SN12344", "PASS", "00:09:40", "Fally", "👁️ View");
            gridData.Rows.Add("8", "15 Mar 2026 10:12:15 AM", "D11T", "AC", "Transmission Harness", "WH-TRN-001", "SN12343", "PASS", "00:08:30", "Hendra", "👁️ View");
            gridData.Rows.Add("9", "15 Mar 2026 10:09:00 AM", "6060", "DC", "Main Cabin Harness", "WH-CAB-004", "SN12342", "PASS", "00:14:15", "Fally", "👁️ View");
            gridData.Rows.Add("10", "15 Mar 2026 10:05:45 AM", "797F", "AC", "Sensor Harness", "WH-SNS-002", "SN12341", "FAIL", "00:06:55", "Budi", "👁️ View");
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
                string val = e.Value.ToString() ?? "";
                if (val == "PASS")
                {
                    e.CellStyle.BackColor = Color.FromArgb(209, 231, 221);
                    e.CellStyle.ForeColor = Color.FromArgb(15, 81, 50);
                    e.CellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                }
                else if (val == "FAIL")
                {
                    e.CellStyle.BackColor = Color.FromArgb(248, 215, 218);
                    e.CellStyle.ForeColor = Color.FromArgb(132, 32, 41);
                    e.CellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                }
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
