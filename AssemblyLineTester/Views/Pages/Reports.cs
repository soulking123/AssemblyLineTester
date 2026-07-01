using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AssemblyLineTester.Views.Pages
{
    public partial class Reports : UserControl
    {
        public Reports()
        {
            InitializeComponent();
            InitializeCustomStyles();
            LoadDummyData();
        }

        private void InitializeCustomStyles()
        {
            // Set Default ComboBox Selections
            if (cmbReportType.Items.Count > 0) cmbReportType.SelectedIndex = 0;
            if (cmbModel.Items.Count > 0) cmbModel.SelectedIndex = 0;
            if (cmbType.Items.Count > 0) cmbType.SelectedIndex = 0;
            if (cmbArea.Items.Count > 0) cmbArea.SelectedIndex = 0;
            if (cmbOperator.Items.Count > 0) cmbOperator.SelectedIndex = 0;
            if (cmbResult.Items.Count > 0) cmbResult.SelectedIndex = 0;

            // DataGridView Modern Styling
            gridReports.BorderStyle = BorderStyle.None;
            gridReports.BackgroundColor = Color.White;
            gridReports.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            gridReports.GridColor = Color.FromArgb(241, 245, 249);
            gridReports.RowHeadersVisible = false;
            gridReports.AllowUserToAddRows = false;
            gridReports.AllowUserToDeleteRows = false;
            gridReports.ReadOnly = true;
            gridReports.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridReports.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridReports.EnableHeadersVisualStyles = false;

            // Header Style
            gridReports.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            gridReports.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(71, 85, 105);
            gridReports.ColumnHeadersDefaultCellStyle.Font = new("Segoe UI", 9F, FontStyle.Bold);
            gridReports.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(248, 250, 252);
            gridReports.ColumnHeadersDefaultCellStyle.Padding = new(8);
            gridReports.ColumnHeadersHeight = 40;

            // Row Style
            gridReports.DefaultCellStyle.BackColor = Color.White;
            gridReports.DefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);
            gridReports.DefaultCellStyle.SelectionBackColor = Color.FromArgb(241, 245, 249);
            gridReports.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);
            gridReports.DefaultCellStyle.Padding = new(6);
            gridReports.RowTemplate.Height = 38;

            // Alignments
            var colNo = gridReports.Columns["colNo"];
            if (colNo is not null) colNo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            var colReportType = gridReports.Columns["colReportType"];
            if (colReportType is not null) colReportType.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            var colDateGenerated = gridReports.Columns["colDateGenerated"];
            if (colDateGenerated is not null) colDateGenerated.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            var colSize = gridReports.Columns["colSize"];
            if (colSize is not null) colSize.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            var colAction = gridReports.Columns["colAction"];
            if (colAction is not null) colAction.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridReports.CellFormatting += GridReports_CellFormatting;
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
                ChartType = SeriesChartType.Line,
                Color = Color.FromArgb(34, 197, 94), // Modern Green
                BorderWidth = 3,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 7,
                MarkerColor = Color.FromArgb(22, 101, 52)
            };

            Series sTarget = new("Target Yield (%)")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.FromArgb(59, 130, 246), // Modern Blue
                BorderWidth = 2,
                BorderDashStyle = ChartDashStyle.Dash,
                MarkerStyle = MarkerStyle.Diamond,
                MarkerSize = 6,
                MarkerColor = Color.FromArgb(30, 64, 175)
            };

            string[] dates = ["08 Mar", "09 Mar", "10 Mar", "11 Mar", "12 Mar", "13 Mar", "14 Mar", "15 Mar"];
            double[] passRates = [96.5, 98.0, 95.2, 98.8, 97.5, 98.5, 96.8, 99.1];

            for (int i = 0; i < dates.Length; i++)
            {
                int idx = sPassRate.Points.AddXY(dates[i], passRates[i]);
                sPassRate.Points[idx].Label = passRates[i].ToString() + "%";
                sPassRate.Points[idx].Font = new("Segoe UI", 7.5F, FontStyle.Bold);

                sTarget.Points.AddXY(dates[i], 97.0);
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
    }
}
