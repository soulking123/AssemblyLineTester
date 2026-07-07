using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using AssemblyLineTester.Models.Dto;
using AssemblyLineTester.Services;

namespace AssemblyLineTester.Views.Pages
{
    public partial class Reports : UserControl
    {
        private readonly TestReportService _reportService = new();
        private int _currentPage = 1;
        private readonly int _pageSize = 10;
        private int _totalPages = 1;
        private int? _selectedReportId;

        public Reports()
        {
            InitializeComponent();
            InitializeCustomStyles();
            WireEvents();

            _reportService.SeedSampleDataIfEmpty();
            ResetFilters();     // override the hardcoded Designer date range with a sensible default
            LoadReportData();   // auto-load on page open, no button click required
        }

        private void InitializeCustomStyles()
        {
            if (cmbReportType.Items.Count > 0) cmbReportType.SelectedIndex = 0;
            if (cmbModel.Items.Count > 0) cmbModel.SelectedIndex = 0;
            if (cmbType.Items.Count > 0) cmbType.SelectedIndex = 0;
            if (cmbArea.Items.Count > 0) cmbArea.SelectedIndex = 0;
            if (cmbOperator.Items.Count > 0) cmbOperator.SelectedIndex = 0;
            if (cmbResult.Items.Count > 0) cmbResult.SelectedIndex = 0;

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

            gridReports.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            gridReports.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(71, 85, 105);
            gridReports.ColumnHeadersDefaultCellStyle.Font = new("Segoe UI", 9F, FontStyle.Bold);
            gridReports.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(248, 250, 252);
            gridReports.ColumnHeadersDefaultCellStyle.Padding = new(8, 4, 8, 4);
            gridReports.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridReports.ColumnHeadersHeight = 48;

            gridReports.DefaultCellStyle.BackColor = Color.White;
            gridReports.DefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);
            gridReports.DefaultCellStyle.SelectionBackColor = Color.FromArgb(241, 245, 249);
            gridReports.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);
            gridReports.DefaultCellStyle.Padding = new(6);
            gridReports.RowTemplate.Height = 48;

            if (gridReports.Columns["colNo"] is { } colNo)
                colNo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (gridReports.Columns["colReportType"] is { } colReportType)
                colReportType.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (gridReports.Columns["colDateGenerated"] is { } colDateGenerated)
                colDateGenerated.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (gridReports.Columns["colSize"] is { } colSize)
                colSize.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            if (gridReports.Columns["colAction"] is { } colAction)
                colAction.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridReports.CellFormatting += GridReports_CellFormatting;
        }

        private void WireEvents()
        {
            btnGenerate.Click += (s, e) => { _currentPage = 1; LoadReportData(); };
            btnReset.Click += (s, e) => { ResetFilters(); _currentPage = 1; LoadReportData(); };

            gridReports.SelectionChanged += (s, e) =>
            {
                if (gridReports.CurrentRow?.Tag is int reportId)
                {
                    _selectedReportId = reportId;
                    LoadPreview(reportId);
                }
            };

            btnPageFirst.Click += (s, e) => GoToPage(1);
            btnPagePrev.Click += (s, e) => GoToPage(_currentPage - 1);
            btnPage1.Click += (s, e) => GoToPage(int.Parse(btnPage1.Text));
            btnPage2.Click += (s, e) => GoToPage(int.Parse(btnPage2.Text));
            btnPageNext.Click += (s, e) => GoToPage(_currentPage + 1);
            btnPageLast.Click += (s, e) => GoToPage(_totalPages);

            btnExportPDF.Click += (s, e) => MessageBox.Show("Export PDF belum diimplementasikan.", "Info");
            btnExportExcel.Click += (s, e) => MessageBox.Show("Export Excel belum diimplementasikan.", "Info");
            btnPrint.Click += (s, e) => MessageBox.Show("Print belum diimplementasikan.", "Info");
        }

        // OVERRIDES THE DESIGNER'S HARDCODED DATE RANGE WITH A DYNAMIC, ALWAYS-RELEVANT DEFAULT.
        private void ResetFilters()
        {
            dtpEnd.Value = DateTime.Now;
            dtpStart.Value = DateTime.Now.AddDays(-30);
            cmbModel.SelectedIndex = 0;
            cmbType.SelectedIndex = 0;
            cmbArea.SelectedIndex = 0;
            cmbOperator.SelectedIndex = 0;
            cmbResult.SelectedIndex = 0;
            txtPartNumber.Text = string.Empty;
        }

        private ReportFilter BuildFilterFromControls()
        {
            var filter = new ReportFilter
            {
                DateFrom = dtpStart.Value.Date,
                DateTo = dtpEnd.Value.Date,
                Page = _currentPage,
                PageSize = _pageSize
            };

            if (!string.Equals(cmbModel.Text, "All", StringComparison.OrdinalIgnoreCase))
                filter.ProductModel = cmbModel.Text;

            if (!string.Equals(cmbType.Text, "All", StringComparison.OrdinalIgnoreCase))
                filter.ProductType = cmbType.Text;

            if (!string.Equals(cmbArea.Text, "All", StringComparison.OrdinalIgnoreCase))
                filter.Area = cmbArea.Text;

            if (!string.Equals(cmbOperator.Text, "All", StringComparison.OrdinalIgnoreCase))
                filter.Operator = cmbOperator.Text;

            if (!string.Equals(cmbResult.Text, "All", StringComparison.OrdinalIgnoreCase))
                filter.Result = cmbResult.Text.ToUpperInvariant();

            if (!string.IsNullOrWhiteSpace(txtPartNumber.Text))
                filter.PartNumberKeyword = txtPartNumber.Text.Trim();

            return filter;
        }

        public void LoadReportData()
        {
            var filter = BuildFilterFromControls();

            LoadSummaryCards(filter);
            LoadTrendChart(filter);
            LoadReportsGrid(filter);
        }

        private void LoadSummaryCards(ReportFilter filter)
        {
            var summary = _reportService.GetSummary(filter);

            lblSummaryTitle.Text = $"SUMMARY ({dtpStart.Value:dd MMM yyyy} - {dtpEnd.Value:dd MMM yyyy})";
            lblSumTotalVal.Text = summary.TotalTested.ToString("N0");
            lblSumPassVal.Text = $"{summary.PassCount:N0} ({FormatPct(summary.PassCount, summary.TotalTested)})";
            lblSumFailVal.Text = $"{summary.FailCount:N0} ({FormatPct(summary.FailCount, summary.TotalTested)})";
            lblSumProgVal.Text = summary.InProgressCount.ToString("N0");
        }

        private static string FormatPct(int count, int total) => total == 0 ? "0.00%" : $"{count * 100.0 / total:0.00}%";

        // BUILDS THE "RESULT TREND" CHART WITH 3 LINES: PASS, FAIL, TOTAL (counts per day, not percentage).
        private void LoadTrendChart(ReportFilter filter)
        {
            var trend = _reportService.GetResultTrend(filter);

            chartTrend.ChartAreas.Clear();
            chartTrend.Series.Clear();
            chartTrend.Legends.Clear();

            ChartArea areaTrend = new("TrendArea") { BackColor = Color.White };
            areaTrend.AxisX.MajorGrid.Enabled = false;
            areaTrend.AxisX.Interval = 1;
            areaTrend.AxisX.LabelStyle.Font = new("Segoe UI", 8.5F);
            areaTrend.AxisX.LabelStyle.ForeColor = Color.FromArgb(100, 116, 139);
            areaTrend.AxisX.LineColor = Color.FromArgb(226, 232, 240);

            areaTrend.AxisY.MajorGrid.LineColor = Color.FromArgb(241, 245, 249);
            areaTrend.AxisY.LabelStyle.Font = new("Segoe UI", 8.5F);
            areaTrend.AxisY.LabelStyle.ForeColor = Color.FromArgb(100, 116, 139);
            areaTrend.AxisY.LineColor = Color.Transparent;
            areaTrend.AxisY.Minimum = 0; // let EF pick a sensible max automatically based on data
            chartTrend.ChartAreas.Add(areaTrend);

            Legend legTrend = new("TrendLegend")
            {
                Docking = Docking.Top,
                Alignment = StringAlignment.Center,
                Font = new("Segoe UI", 8.5F, FontStyle.Bold)
            };
            chartTrend.Legends.Add(legTrend);

            Series sTotal = new("Total")
            {
                ChartType = SeriesChartType.Spline,
                Color = Color.FromArgb(100, 116, 139),
                BorderWidth = 2,
                BorderDashStyle = ChartDashStyle.Dash,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 6,
                IsXValueIndexed = true
            };

            Series sPass = new("Pass")
            {
                ChartType = SeriesChartType.Spline,
                Color = Color.FromArgb(34, 197, 94),
                BorderWidth = 3,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 7,
                MarkerColor = Color.FromArgb(22, 101, 52),
                IsXValueIndexed = true
            };

            Series sFail = new("Fail")
            {
                ChartType = SeriesChartType.Spline,
                Color = Color.FromArgb(239, 68, 68),
                BorderWidth = 3,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 7,
                MarkerColor = Color.FromArgb(153, 27, 27),
                IsXValueIndexed = true
            };

            foreach (var point in trend)
            {
                string label = point.Date.ToString("dd MMM");
                sTotal.Points.AddXY(label, point.TotalCount);
                sPass.Points.AddXY(label, point.PassCount);
                sFail.Points.AddXY(label, point.FailCount);
            }

            chartTrend.Series.Add(sTotal);
            chartTrend.Series.Add(sPass);
            chartTrend.Series.Add(sFail);
        }

        private void LoadReportsGrid(ReportFilter filter)
        {
            var result = _reportService.GetReportList(filter);

            _totalPages = Math.Max(1, result.TotalPages);
            if (_currentPage > _totalPages)
            {
                _currentPage = _totalPages;
                filter.Page = _currentPage;
                result = _reportService.GetReportList(filter);
            }

            lblReportListTitle.Text = $"REPORT LIST (Total: {result.TotalCount})";

            gridReports.Rows.Clear();
            int no = (result.Page - 1) * result.PageSize + 1;
            foreach (var r in result.Items)
            {
                int rowIdx = gridReports.Rows.Add(
                    no++,
                    r.ReportName,
                    r.ReportType,
                    r.DateGenerated.ToString("dd MMM yyyy, HH:mm"),
                    r.GeneratedBy,
                    $"{r.SizeKb:N0} KB",
                    "View / Export");
                gridReports.Rows[rowIdx].Tag = r.ReportId;
            }

            UpdatePaginationUI(result.TotalCount);

            if (gridReports.Rows.Count > 0)
            {
                gridReports.Rows[0].Selected = true;
                if (gridReports.Rows[0].Tag is int firstId) LoadPreview(firstId);
            }
            else
            {
                _selectedReportId = null;
                ClearPreview();
            }
        }

        private void GoToPage(int page)
        {
            int target = Math.Max(1, Math.Min(page, _totalPages));
            if (target == _currentPage) return;
            _currentPage = target;
            LoadReportsGrid(BuildFilterFromControls());
        }

        private void UpdatePaginationUI(int totalCount)
        {
            int start = totalCount == 0 ? 0 : (_currentPage - 1) * _pageSize + 1;
            int end = Math.Min(_currentPage * _pageSize, totalCount);
            lblPageInfo.Text = $"Showing {start} to {end} of {totalCount} entries";

            btnPageFirst.Enabled = _currentPage > 1;
            btnPagePrev.Enabled = _currentPage > 1;
            btnPageNext.Enabled = _currentPage < _totalPages;
            btnPageLast.Enabled = _currentPage < _totalPages;

            btnPage1.Text = _currentPage.ToString();
            btnPage1.BackColor = Color.FromArgb(30, 41, 59);
            btnPage1.ForeColor = Color.White;

            bool hasNextPage = _currentPage < _totalPages;
            btnPage2.Visible = hasNextPage;
            if (hasNextPage)
            {
                btnPage2.Text = (_currentPage + 1).ToString();
                btnPage2.BackColor = Color.FromArgb(241, 245, 249);
                btnPage2.ForeColor = Color.FromArgb(51, 65, 85);
            }

            btnPageDots.Visible = _totalPages > _currentPage + 1;
        }

        private void LoadPreview(int reportId)
        {
            var detail = _reportService.GetReportDetail(reportId);
            if (detail is null)
            {
                ClearPreview();
                return;
            }

            lblPaperMeta.Text = $"Report No.: {detail.ReportNumber}\nDate: {detail.TestDateTime:dd MMM yyyy HH:mm}\nPage: 1";

            lblGenModelVal.Text = detail.ProductModel;
            lblGenEcnVal.Text = detail.ECNNumber;
            lblGenTypeVal.Text = detail.ProductType;
            lblGenRevVal.Text = detail.Revision;
            lblGenAreaVal.Text = detail.Area;
            lblGenOpVal.Text = detail.Operator;
            lblGenPartVal.Text = detail.PartNumber;
            lblGenTimeVal.Text = detail.TestTime;
            lblGenSnVal.Text = detail.SerialNumber;
            lblGenResVal.Text = detail.OverallResult;
            lblGenResVal.ForeColor = string.Equals(detail.OverallResult, "PASS", StringComparison.OrdinalIgnoreCase)
                ? Color.FromArgb(4, 120, 87)
                : Color.FromArgb(220, 38, 38);

            var rows = new[]
            {
                (lblR1C1, lblR1C2, lblR1C3, lblR1C4, lblR1C5),
                (lblR2C1, lblR2C2, lblR2C3, lblR2C4, lblR2C5),
                (lblR3C1, lblR3C2, lblR3C3, lblR3C4, lblR3C5),
                (lblR4C1, lblR4C2, lblR4C3, lblR4C4, lblR4C5),
                (lblR5C1, lblR5C2, lblR5C3, lblR5C4, lblR5C5)
            };

            for (int i = 0; i < detail.TypeSummaries.Count && i < rows.Length; i++)
            {
                var s = detail.TypeSummaries[i];
                var (cName, cTotal, cPass, cFail, cRate) = rows[i];
                cName.Text = s.TestType;
                cTotal.Text = s.Total.ToString();
                cPass.Text = s.Pass.ToString();
                cFail.Text = s.Fail.ToString();
                cFail.ForeColor = s.Fail > 0 ? Color.FromArgb(220, 38, 38) : Color.FromArgb(30, 41, 59);
                cRate.Text = $"{s.PassRatePercent:0.00}%";
                cRate.ForeColor = s.Fail > 0 ? Color.FromArgb(220, 38, 38) : Color.FromArgb(30, 41, 59);
            }

            int totalAll = detail.TypeSummaries.Sum(t => t.Total);
            int passAll = detail.TypeSummaries.Sum(t => t.Pass);
            int failAll = detail.TypeSummaries.Sum(t => t.Fail);
            lblR5C2.Text = totalAll.ToString();
            lblR5C3.Text = passAll.ToString();
            lblR5C4.Text = failAll.ToString();
            lblR5C5.Text = totalAll == 0 ? "100.00%" : $"{passAll * 100.0 / totalAll:0.00}%";

            bool overallPass = string.Equals(detail.OverallResult, "PASS", StringComparison.OrdinalIgnoreCase);
            lblPassBox.Text = detail.OverallResult;
            lblPassBox.BackColor = overallPass ? Color.FromArgb(236, 253, 245) : Color.FromArgb(254, 242, 242);
            lblPassBox.ForeColor = overallPass ? Color.FromArgb(4, 120, 87) : Color.FromArgb(220, 38, 38);

            lblNotesContent.Text = overallPass
                ? "- All test parameters are within specification."
                : "- One or more test parameters failed. See detailed results for error information.";
        }

        private void ClearPreview()
        {
            lblPaperMeta.Text = "Report No.: -\nDate: -\nPage: -";
            lblGenModelVal.Text = lblGenEcnVal.Text = lblGenTypeVal.Text = lblGenRevVal.Text = "-";
            lblGenAreaVal.Text = lblGenOpVal.Text = lblGenPartVal.Text = lblGenTimeVal.Text = "-";
            lblGenSnVal.Text = lblGenResVal.Text = "-";
            lblPassBox.Text = "-";
        }

        private void GridReports_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value is null || e.CellStyle is null) return;

            string colName = gridReports.Columns[e.ColumnIndex].Name;

            if (colName == "colReportType")
            {
                string? val = e.Value.ToString();
                if (val == "Summary Report")
                {
                    e.CellStyle.BackColor = Color.FromArgb(224, 242, 254);
                    e.CellStyle.ForeColor = Color.FromArgb(3, 105, 161);
                    e.CellStyle.Font = new("Segoe UI", 8.5F, FontStyle.Bold);
                }
                else
                {
                    e.CellStyle.BackColor = Color.FromArgb(241, 245, 249);
                    e.CellStyle.ForeColor = Color.FromArgb(51, 65, 85);
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