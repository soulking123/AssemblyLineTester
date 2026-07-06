using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using AssemblyLineTester.Services;

namespace AssemblyLineTester.Views.Pages
{
    public partial class Dashboard : UserControl
    {
        private System.Windows.Forms.Timer? _uiRefreshTimer;

        public Dashboard()
        {
            InitializeComponent();
            InitializeCustomStyles();
            LoadDummyData();
            StartUiRefreshTimer();
        }

        private void InitializeCustomStyles()
        {
            if (cmbTimeRange.Items.Count > 0)
            {
                cmbTimeRange.SelectedIndex = 0;
            }

            // DataGridView Modern Styling
            gridRecent.BorderStyle = BorderStyle.None;
            gridRecent.BackgroundColor = Color.White;
            gridRecent.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            gridRecent.GridColor = Color.FromArgb(241, 245, 249);
            gridRecent.RowHeadersVisible = false;
            gridRecent.AllowUserToAddRows = false;
            gridRecent.AllowUserToDeleteRows = false;
            gridRecent.ReadOnly = true;
            gridRecent.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridRecent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridRecent.EnableHeadersVisualStyles = false;

            // Header Style
            gridRecent.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            gridRecent.ColumnHeadersDefaultCellStyle.Font = new("Segoe UI", 9F, FontStyle.Bold);
            gridRecent.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(248, 250, 252);
            gridRecent.ColumnHeadersDefaultCellStyle.Padding = new Padding(8);
            gridRecent.ColumnHeadersHeight = 40;

            // Row Style
            gridRecent.DefaultCellStyle.BackColor = Color.White;
            gridRecent.DefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);
            gridRecent.DefaultCellStyle.SelectionBackColor = Color.FromArgb(241, 245, 249);
            gridRecent.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);
            gridRecent.DefaultCellStyle.Padding = new Padding(6);
            gridRecent.RowTemplate.Height = 36;

            gridRecent.CellFormatting += GridRecent_CellFormatting;

            // Attach PostPaint for center text in Doughnut Chart
            chartFailReason.PostPaint += ChartFailReason_PostPaint;
        }

        private void LoadDummyData()
        {
            // ----------------------------------------------------
            // 1. Setup Bar Chart (History)
            // ----------------------------------------------------
            chartHistory.ChartAreas.Clear();
            chartHistory.Series.Clear();
            chartHistory.Legends.Clear();

            ChartArea areaBar = new("BarArea")
            {
                BackColor = Color.White
            };
            areaBar.AxisX.MajorGrid.Enabled = false;
            areaBar.AxisX.LabelStyle.Font = new("Segoe UI", 8.5F);
            areaBar.AxisX.LabelStyle.ForeColor = Color.FromArgb(100, 116, 139);
            areaBar.AxisX.LineColor = Color.FromArgb(226, 232, 240);

            areaBar.AxisY.MajorGrid.LineColor = Color.FromArgb(241, 245, 249);
            areaBar.AxisY.LabelStyle.Font = new("Segoe UI", 8.5F);
            areaBar.AxisY.LabelStyle.ForeColor = Color.FromArgb(100, 116, 139);
            areaBar.AxisY.LineColor = Color.Transparent;
            areaBar.AxisY.Maximum = 250;
            areaBar.AxisY.Interval = 50;
            areaBar.AxisX.Interval = 1;
            chartHistory.ChartAreas.Add(areaBar);

            Legend legBar = new("BarLegend")
            {
                Docking = Docking.Top,
                Alignment = StringAlignment.Center,
                Font = new("Segoe UI", 9F, FontStyle.Bold)
            };
            chartHistory.Legends.Add(legBar);

            Series sPass = new("Pass")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.FromArgb(76, 175, 80),
                LegendText = "Pass",
                IsXValueIndexed = true
            };
            sPass["PointWidth"] = "0.45";

            Series sFail = new("Fail")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.FromArgb(244, 67, 54),
                LegendText = "Fail",
                IsXValueIndexed = true
            };
            sFail["PointWidth"] = "0.45";

            int[] passVals = [180, 192, 205, 198, 210, 208, 215];
            int[] failVals = [4, 3, 2, 4, 2, 3, 3];

            for (int i = 0; i < 7; i++)
            {
                string dateLabel = DateTime.Now.AddDays(-6 + i).ToString("dd MMM");

                int pIdx = sPass.Points.AddXY(dateLabel, passVals[i]);
                sPass.Points[pIdx].Label = passVals[i].ToString();
                sPass.Points[pIdx].Font = new("Segoe UI", 8F, FontStyle.Bold);

                int fIdx = sFail.Points.AddXY(dateLabel, failVals[i]);
                sFail.Points[fIdx].Label = failVals[i].ToString();
                sFail.Points[fIdx].Font = new("Segoe UI", 8F, FontStyle.Bold);
            }

            chartHistory.Series.Add(sPass);
            chartHistory.Series.Add(sFail);

            // ----------------------------------------------------
            // 2. Setup Doughnut Chart (Fail By Reason)
            // ----------------------------------------------------
            chartFailReason.ChartAreas.Clear();
            chartFailReason.Series.Clear();
            chartFailReason.Legends.Clear();

            ChartArea areaPie = new("PieArea")
            {
                BackColor = Color.White
            };
            chartFailReason.ChartAreas.Add(areaPie);

            Legend legPie = new("PieLegend")
            {
                Docking = Docking.Bottom,
                Alignment = StringAlignment.Center,
                Font = new("Segoe UI", 9F, FontStyle.Regular)
            };
            chartFailReason.Legends.Add(legPie);

            Series sReason = new("FailReason")
            {
                ChartType = SeriesChartType.Doughnut
            };
            sReason["DoughnutRadius"] = "25";

            int p1 = sReason.Points.AddY(2);
            sReason.Points[p1].Color = Color.FromArgb(244, 67, 54);
            sReason.Points[p1].LegendText = "Continuity Fail   2 (66.7%)";

            int p2 = sReason.Points.AddY(1);
            sReason.Points[p2].Color = Color.FromArgb(255, 152, 0);
            sReason.Points[p2].LegendText = "Insulation Fail   1 (33.3%)";

            int p3 = sReason.Points.AddY(0);
            sReason.Points[p3].Color = Color.FromArgb(33, 150, 243);
            sReason.Points[p3].LegendText = "Short Circuit Fail 0 (0%)";
            sReason.Points[p3].IsEmpty = true;

            chartFailReason.Series.Add(sReason);

            // ----------------------------------------------------
            // 3. Setup DataGridView (Recent Test History)
            // ----------------------------------------------------
            gridRecent.Columns.Clear();
            gridRecent.Columns.Add("ColNo", "No.");
            gridRecent.Columns.Add("ColTime", "Test Time");
            gridRecent.Columns.Add("ColPart", "Part Number");
            gridRecent.Columns.Add("ColSerial", "Serial Number");
            gridRecent.Columns.Add("ColResult", "Result");
            gridRecent.Columns.Add("ColOperator", "Operator");

            var colNo = gridRecent.Columns["ColNo"];
            var colTime = gridRecent.Columns["ColTime"];
            var colPart = gridRecent.Columns["ColPart"];
            var colSerial = gridRecent.Columns["ColSerial"];
            var colResult = gridRecent.Columns["ColResult"];
            var colOperator = gridRecent.Columns["ColOperator"];

            if (colNo != null) { colNo.FillWeight = 30; colNo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; }
            if (colTime != null) colTime.FillWeight = 60;
            if (colPart != null) colPart.FillWeight = 75;
            if (colSerial != null) colSerial.FillWeight = 70;
            if (colResult != null) { colResult.FillWeight = 50; colResult.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; }
            if (colOperator != null) colOperator.FillWeight = 55;

            object[][] rows =
            [
                ["1", "10:30:12", "WH-ENG-001", "SN12350", "PASS", "Fally"],
                ["2", "10:28:56", "WH-ENG-001", "SN12349", "PASS", "Fally"],
                ["3", "10:27:40", "WH-ENG-001", "SN12348", "FAIL", "Fally"],
                ["4", "10:26:18", "WH-ENG-001", "SN12347", "PASS", "Fally"],
                ["5", "10:25:05", "WH-ENG-001", "SN12346", "PASS", "Fally"],
                ["6", "10:23:51", "WH-ENG-001", "SN12345", "PASS", "Fally"],
                ["7", "10:22:33", "WH-ENG-001", "SN12344", "PASS", "Fally"],
                ["8", "10:21:20", "WH-ENG-001", "SN12343", "PASS", "Fally"]
            ];

            foreach (var r in rows)
            {
                gridRecent.Rows.Add(r);
            }
        }

        private void GridRecent_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (gridRecent.Columns[e.ColumnIndex].Name == "ColResult" && e.Value != null && e.CellStyle != null)
            {
                string? val = e.Value.ToString();
                if (val == "PASS")
                {
                    e.CellStyle.BackColor = Color.FromArgb(220, 252, 231);
                    e.CellStyle.ForeColor = Color.FromArgb(22, 101, 52);
                    e.CellStyle.Font = new("Segoe UI", 9F, FontStyle.Bold);
                }
                else if (val == "FAIL")
                {
                    e.CellStyle.BackColor = Color.FromArgb(254, 226, 226);
                    e.CellStyle.ForeColor = Color.FromArgb(153, 27, 27);
                    e.CellStyle.Font = new("Segoe UI", 9F, FontStyle.Bold);
                }
            }
        }

        private void ChartFailReason_PostPaint(object? sender, ChartPaintEventArgs e)
        {
            if (e.ChartElement is ChartArea area)
            {
                string numText = "3";
                string subText = "TOTAL FAIL";

                using Font fontNum = new("Segoe UI", 22F, FontStyle.Bold);
                using Font fontSub = new("Segoe UI", 8.5F, FontStyle.Bold);
                using SolidBrush brushNum = new(Color.FromArgb(30, 41, 59));
                using SolidBrush brushSub = new(Color.FromArgb(100, 116, 139));
                using StringFormat sf = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

                RectangleF chartRect = e.ChartGraphics.GetAbsoluteRectangle(area.Position.ToRectangleF());
                float centerX = chartRect.Left + (chartRect.Width * 0.50f);
                float centerY = chartRect.Top + (chartRect.Height * 0.5f);

                e.ChartGraphics.Graphics.DrawString(numText, fontNum, brushNum, centerX, centerY - 14, sf);
                e.ChartGraphics.Graphics.DrawString(subText, fontSub, brushSub, centerX, centerY + 16, sf);
            }
        }

        private void StartUiRefreshTimer()
        {
            _uiRefreshTimer = new()
            {
                Interval = 1000
            };
            _uiRefreshTimer.Tick += (s, e) => UpdateMaintenanceLabels();
            _uiRefreshTimer.Start();
            UpdateMaintenanceLabels(); // Initial update on load
        }

        private void UpdateMaintenanceLabels()
        {
            // Update Operating Time today (hh:mm:ss)
            TimeSpan timeToday = TimeSpan.FromSeconds(MaintenanceService.ActiveOperatingSeconds);
            lblOpTimeVal.Text = timeToday.ToString(@"hh\:mm\:ss");

            // Update Next Maintenance remaining days
            lblMaintVal.Text = $"{MaintenanceService.RemainingDays} Days";

            // Update Next Maintenance remaining hours (ticking down in real-time)
            double currentRemainingHours = Math.Max(0, MaintenanceService.RemainingHours - (MaintenanceService.ActiveOperatingSeconds / 3600.0));
            lblMaintSub.Text = $"Or {currentRemainingHours:N1} Hours";
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (_uiRefreshTimer != null)
            {
                _uiRefreshTimer.Stop();
                _uiRefreshTimer.Dispose();
                _uiRefreshTimer = null;
            }
            base.OnHandleDestroyed(e);
        }
    }
}
