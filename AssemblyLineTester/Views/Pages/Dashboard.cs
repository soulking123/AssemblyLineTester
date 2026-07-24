using AssemblyLineTester.Models.Dto;
using AssemblyLineTester.Services;
using AssemblyLineTester.Data;
using AssemblyLineTester.Views.Modals;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;

namespace AssemblyLineTester.Views.Pages
{
    public partial class Dashboard : UserControl
    {
        private readonly DashboardService _dashboardService = new();
        private readonly TestConfigService _configService = new();
        private readonly InstrumentService _instrument = new();
        private System.Windows.Forms.Timer? _uiRefreshTimer;
        private DashboardTimeRange _selectedRange = DashboardTimeRange.Today;
        private int _totalFailForChart;



        public Dashboard()
        {
            InitializeComponent();
            InitializeCustomStyles();
            LoadDashboardData();
            StartUiRefreshTimer();
            this.VisibleChanged += Dashboard_VisibleChanged;
        }

        private void Dashboard_VisibleChanged(object? sender, EventArgs e)
        {
            if (this.Visible && !DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                LoadDashboardData();
            }
        }

        private void InitializeCustomStyles()
        {
            SetupEvents();

            // cmbTimeRange only has "Today" in the Designer - expand it here without touching the Designer file.
            cmbTimeRange.Items.Clear();
            cmbTimeRange.Items.AddRange(new object[]
            {
                "Today", "Last 7 Days", "Last 30 Days", "Last 3 Months", "Last 6 Months", "Last 1 Year"
            });
            cmbTimeRange.SelectedIndex = 0;
            cmbTimeRange.SelectedIndexChanged += CmbTimeRange_SelectedIndexChanged;

            // DataGridView Modern Styling
            AssemblyLineTester.Helpers.UIStyleHelper.StyleDataGridView(gridRecent);
            gridRecent.CellFormatting += GridRecent_CellFormatting;

            // Attach PostPaint for center text in Doughnut Chart
            chartFailReason.PostPaint += ChartFailReason_PostPaint;

            // NOTE: cardTester / cardMachine / cardDatabase / cardModules status labels are left as
            // static Designer defaults (ONLINE / READY / CONNECTED, etc.) for now - there is no
            // hardware/connection status table in the schema yet, so this is intentionally hardcoded.
        }

        private void CmbTimeRange_SelectedIndexChanged(object? sender, EventArgs e)
        {
            _selectedRange = cmbTimeRange.SelectedIndex switch
            {
                1 => DashboardTimeRange.Last7Days,
                2 => DashboardTimeRange.Last30Days,
                3 => DashboardTimeRange.Last3Months,
                4 => DashboardTimeRange.Last6Months,
                5 => DashboardTimeRange.Last1Year,
                _ => DashboardTimeRange.Today
            };
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            LoadKpiCards();
            LoadHistoryChart();
            LoadFailReasonChart();
            LoadRecentGrid();
            LoadActiveProgram();
        }

        private void SetTesterOfflineUI()
        {
            lblTesterStatus.Text = "OFFLINE";
            lblTesterStatus.ForeColor = Color.FromArgb(244, 67, 54); // Red
            lblTesterSub.Text = "No active connection to tester server";
        }

        private void LoadKpiCards()
        {
            var kpi = _dashboardService.GetKpiSummary(_selectedRange);

            lblTotalTestedVal.Text = kpi.TotalTested.ToString("N0");
            lblPassVal.Text = kpi.PassCount.ToString("N0");
            lblFailVal.Text = kpi.FailCount.ToString("N0");
            lblYieldVal.Text = $"{kpi.YieldPercent:0.0}%";
            lblCycleTimeVal.Text = kpi.AvgCycleTimeFormatted;

            double passPct = kpi.TotalTested == 0 ? 0 : Math.Round(kpi.PassCount * 100.0 / kpi.TotalTested, 2);
            double failPct = kpi.TotalTested == 0 ? 0 : Math.Round(kpi.FailCount * 100.0 / kpi.TotalTested, 2);
            lblPassTitle.Text = $"PASS ({passPct:0.00}%)";
            lblFailTitle.Text = $"FAIL ({failPct:0.00}%)";

            lblSummaryTitle.Text = $"SUMMARY REPORT ({cmbTimeRange.Text})";
        }

        // Bar Chart (History): Pass/Fail grouped adaptively (day/week/month) based on the range,
        // from DashboardService.GetHistoryTrend.
        private void LoadHistoryChart()
        {
            var trend = _dashboardService.GetHistoryTrend(_selectedRange);

            chartHistory.ChartAreas.Clear();
            chartHistory.Series.Clear();
            chartHistory.Legends.Clear();

            ChartArea areaBar = new("BarArea") { BackColor = Color.White };
            areaBar.AxisX.MajorGrid.Enabled = false;
            areaBar.AxisX.LabelStyle.Font = new("Segoe UI", 8.5F);
            areaBar.AxisX.LabelStyle.ForeColor = Color.FromArgb(100, 116, 139);
            areaBar.AxisX.LineColor = Color.FromArgb(226, 232, 240);
            areaBar.AxisX.Interval = 1;

            areaBar.AxisY.MajorGrid.LineColor = Color.FromArgb(241, 245, 249);
            areaBar.AxisY.LabelStyle.Font = new("Segoe UI", 8.5F);
            areaBar.AxisY.LabelStyle.ForeColor = Color.FromArgb(100, 116, 139);
            areaBar.AxisY.LineColor = Color.Transparent;
            areaBar.AxisY.Minimum = 0;
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

            string labelFormat = trend.Granularity switch
            {
                TrendGranularity.Weekly => "'Wk' dd MMM",
                TrendGranularity.Monthly => "MMM yyyy",
                _ => "dd MMM"
            };

            foreach (var point in trend.Points)
            {
                string dateLabel = point.Date.ToString(labelFormat);

                int pIdx = sPass.Points.AddXY(dateLabel, point.PassCount);
                sPass.Points[pIdx].Label = point.PassCount.ToString();
                sPass.Points[pIdx].Font = new("Segoe UI", 8F, FontStyle.Bold);

                int fIdx = sFail.Points.AddXY(dateLabel, point.FailCount);
                sFail.Points[fIdx].Label = point.FailCount.ToString();
                sFail.Points[fIdx].Font = new("Segoe UI", 8F, FontStyle.Bold);
            }

            chartHistory.Series.Add(sPass);
            chartHistory.Series.Add(sFail);
        }

        // Doughnut Chart (Fail By Reason): grouped by test type, from DashboardService.GetFailReasonBreakdown.
        private void LoadFailReasonChart()
        {
            var reasons = _dashboardService.GetFailReasonBreakdown(_selectedRange);
            _totalFailForChart = reasons.Sum(r => r.FailCount);

            chartFailReason.ChartAreas.Clear();
            chartFailReason.Series.Clear();
            chartFailReason.Legends.Clear();

            ChartArea areaPie = new("PieArea") { BackColor = Color.White };
            chartFailReason.ChartAreas.Add(areaPie);

            Legend legPie = new("PieLegend")
            {
                Docking = Docking.Bottom,
                Alignment = StringAlignment.Center,
                Font = new("Segoe UI", 9F, FontStyle.Regular)
            };
            chartFailReason.Legends.Add(legPie);

            Series sReason = new("FailReason") { ChartType = SeriesChartType.Doughnut };
            sReason["DoughnutRadius"] = "25";

            Color[] palette =
            {
                Color.FromArgb(244, 67, 54),
                Color.FromArgb(255, 152, 0),
                Color.FromArgb(33, 150, 243),
                Color.FromArgb(156, 39, 176),
                Color.FromArgb(0, 150, 136)
            };

            if (reasons.Count == 0)
            {
                int emptyIdx = sReason.Points.AddY(1);
                sReason.Points[emptyIdx].Color = Color.FromArgb(226, 232, 240);
                sReason.Points[emptyIdx].LegendText = "No failures in this period";
            }
            else
            {
                for (int i = 0; i < reasons.Count; i++)
                {
                    var r = reasons[i];
                    int idx = sReason.Points.AddY(r.FailCount);
                    sReason.Points[idx].Color = palette[i % palette.Length];
                    sReason.Points[idx].LegendText = $"{r.TestTypeName}   {r.FailCount} ({r.PercentOfTotalFail:0.0}%)";
                }
            }

            chartFailReason.Series.Add(sReason);
        }

        // DataGridView (Recent Test History): latest reports, from DashboardService.GetRecentTests.
        private void LoadRecentGrid()
        {
            var recent = _dashboardService.GetRecentTests(8);

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

            gridRecent.Rows.Clear();
            int no = 1;
            foreach (var r in recent)
            {
                int rowIdx = gridRecent.Rows.Add(
                    no++,
                    r.TestTime.ToString("HH:mm:ss"),
                    r.PartNumber,
                    r.SerialNumber,
                    r.Result ?? "-",
                    r.Operator);
                gridRecent.Rows[rowIdx].Tag = r.ReportId;
            }
        }

        // "ACTIVE PROGRAM" card: prioritizes an in-progress report, falls back to the latest one.
        private void LoadActiveProgram()
        {
            var currentUser = SessionManager.CurrentUser?.Username ?? "Unknown";
            var activeConfig = _configService.GetActiveConfig();

            lblProgDetails.Text = activeConfig is null
                ? "No test program has been run yet."
                : $"Config Name      : {activeConfig.ConfigName}\n" +
                  $"Product Model    : {activeConfig.ProductModel}\n" +
                  $"Total Test       : {activeConfig.Nets.Count}\n" +
                  $"Created By       : {activeConfig.CreatedBy}\n" +
                  $"Last Update      : {activeConfig.UpdatedAt:dd MMM yyyy HH:mm}";
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
                    e.CellStyle.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font9Bold;
                }
                else if (val == "FAIL")
                {
                    e.CellStyle.BackColor = Color.FromArgb(254, 226, 226);
                    e.CellStyle.ForeColor = Color.FromArgb(153, 27, 27);
                    e.CellStyle.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font9Bold;
                }
                else
                {
                    e.CellStyle.BackColor = Color.FromArgb(241, 245, 249);
                    e.CellStyle.ForeColor = Color.FromArgb(100, 116, 139);
                    e.CellStyle.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font9Bold;
                }
            }
        }

        private void ChartFailReason_PostPaint(object? sender, ChartPaintEventArgs e)
        {
            if (e.ChartElement is ChartArea area)
            {
                string numText = _totalFailForChart.ToString();
                string subText = "TOTAL FAIL";

                using SolidBrush brushNum = new(Color.FromArgb(30, 41, 59));
                using SolidBrush brushSub = new(Color.FromArgb(100, 116, 139));
                using StringFormat sf = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

                RectangleF chartRect = e.ChartGraphics.GetAbsoluteRectangle(area.Position.ToRectangleF());
                float centerX = chartRect.Left + (chartRect.Width * 0.50f);
                float centerY = chartRect.Top + (chartRect.Height * 0.5f);

                e.ChartGraphics.Graphics.DrawString(numText, AssemblyLineTester.Helpers.UIStyleHelper.Font22Bold, brushNum, centerX, centerY - 14, sf);
                e.ChartGraphics.Graphics.DrawString(subText, AssemblyLineTester.Helpers.UIStyleHelper.Font85Bold, brushSub, centerX, centerY + 16, sf);
            }
        }

        private void StartUiRefreshTimer()
        {
            _uiRefreshTimer = new() { Interval = 1000 };
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

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (!this.Visible && _uiRefreshTimer != null)
            {
                _uiRefreshTimer.Stop();
            }
            else if (this.Visible && _uiRefreshTimer != null)
            {
                _uiRefreshTimer.Start();
                UpdateMaintenanceLabels();
            }
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

        private void SetupEvents()
        {
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardTester, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardMachine, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardDatabase, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardActiveProg, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardSummary, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardHistory, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardModules, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardOperatingTime, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardMaintenance, 12, Color.FromArgb(34, 40, 49), 2.5f);

            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(kpiTotalTested, 8, Color.FromArgb(148, 163, 184), 1.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(kpiPass, 8, Color.FromArgb(148, 163, 184), 1.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(kpiFail, 8, Color.FromArgb(148, 163, 184), 1.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(kpiYield, 8, Color.FromArgb(148, 163, 184), 1.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(kpiCycleTime, 8, Color.FromArgb(148, 163, 184), 1.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(picHarness, 8, Color.FromArgb(148, 163, 184), 1.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(pnlMod1, 8, Color.FromArgb(148, 163, 184), 1.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(pnlMod2, 8, Color.FromArgb(148, 163, 184), 1.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(pnlMod3, 8, Color.FromArgb(148, 163, 184), 1.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(pnlMod4, 8, Color.FromArgb(148, 163, 184), 1.5f);

            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnChangeProg, 8, Color.FromArgb(250, 204, 21), Color.FromArgb(254, 216, 53), Color.FromArgb(234, 186, 10), Color.Transparent);
            btnChangeProg.Click += BtnChangeProg_Click;

            // Subscribe to status changes globally
            InstrumentService.OnConnectionStatusChanged += InstrumentService_OnConnectionStatusChanged;
            UpdateTesterStatusUI(InstrumentService.IsOnline);
        }

        private void InstrumentService_OnConnectionStatusChanged(bool isOnline)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => UpdateTesterStatusUI(isOnline)));
            }
            else
            {
                UpdateTesterStatusUI(isOnline);
            }
        }

        private void UpdateTesterStatusUI(bool isOnline)
        {
            if (isOnline)
            {
                lblTesterStatus.Text = "ONLINE";
                lblTesterStatus.ForeColor = Color.FromArgb(76, 175, 80); // Green
                lblTesterSub.Text = "All tester modules are operational";
            }
            else
            {
                SetTesterOfflineUI();
            }
        }

        private void BtnChangeProg_Click(object? sender, EventArgs e)
        {
            try
            {
                using var db = new AppDbContext();
                using var modal = new SelectTestConfigModalForm(db);
                if (modal.ShowDialog() == DialogResult.OK)
                {
                    var selectedConfig = modal.SelectedConfiguration;
                    if (selectedConfig != null)
                    {
                        var configService = new TestConfigService();
                        configService.SetActiveConfig(selectedConfig.ConfigName);

                        // Reload display
                        LoadActiveProgram();

                        MessageBox.Show($"Successfully changed active program to: {selectedConfig.ConfigName}", "Change Program", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening program selector: {ex.Message}", "Change Program Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}