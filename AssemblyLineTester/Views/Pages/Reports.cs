using System;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        private readonly ReportExportService _exportService = new();
        private int _currentPage = 1;
        private readonly int _pageSize = 10;
        private int _totalPages = 1;
        private int? _selectedReportId;

        // The filter actually being displayed (list/summary/trend). Only changes when.
        private ReportFilter _appliedFilter = new();
        private Chart chartTrend = null!;

        // --- Report Edit/Delete feature ---
        private bool _isEditingReport;
        private TextBox _txtEditSn = null!;

        public Reports()
        {
            InitializeComponent();
            this.Load += Reports_Load;
            SetupEditableReportFields();
        }

        private void Reports_Load(object? sender, EventArgs e)
        {
            if (DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            {
                return;
            }
            InitializeCustomStyles();
            WireEvents();

            // No filter applied yet -> show everything.
            _appliedFilter = new ReportFilter { Page = _currentPage, PageSize = _pageSize };
            RenderReportData(_appliedFilter);
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
            // Plain date picker, no checkbox - dd/MM/yy format, user picks whenever they want.
            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpStart.CustomFormat = "dd/MM/yy";
            dtpEnd.Format = DateTimePickerFormat.Custom;
            dtpEnd.CustomFormat = "dd/MM/yy";
            dtpStart.Value = DateTime.Now.AddDays(-30);
            dtpEnd.Value = DateTime.Now;

            AssemblyLineTester.Helpers.UIStyleHelper.StyleDataGridView(gridReports);
            gridReports.ColumnHeadersHeight = 48;
            gridReports.RowTemplate.Height = 44;
            gridReports.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 4, 10, 4);
            gridReports.DefaultCellStyle.Padding = new Padding(8, 4, 8, 4);

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
            SetupEvents();
        }

        private void SetupEditableReportFields()
        {
            try
            {
                btnSaveReportEdit.Visible = false;
                btnCancelReportEdit.Visible = false;

                _txtEditSn = CreateOverlayTextBox(lblGenSnVal);
                _txtEditSn = CreateOverlayTextBox(lblGenSnVal);

                btnEditReport.Click += (s, e) => EnterEditMode();
                btnSaveReportEdit.Click += (s, e) => SaveReportEdit();
                btnCancelReportEdit.Click += (s, e) => ExitEditMode(refresh: true);

                btnDeleteReport.Click += (s, e) =>
                {
                    if (_selectedReportId is null)
                    {
                        MessageBox.Show("Please select a report to delete.", "Delete Report", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var confirm = MessageBox.Show(
                        "This will permanently delete the report and all its test details. Continue?",
                        "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirm != DialogResult.Yes) return;

                    _reportService.DeleteReport(_selectedReportId.Value);
                    _selectedReportId = null;
                    ClearPreview();
                    LoadReportsGrid(_appliedFilter);
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"SetupEditableReportFields failed:\n{ex.Message}", "Setup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static TextBox CreateOverlayTextBox(Label sourceLabel)
        {
            var tb = new TextBox
            {
                Location = sourceLabel.Location,
                Size = sourceLabel.Size,
                Font = sourceLabel.Font,
                Visible = false
            };
            sourceLabel.Parent!.Controls.Add(tb);
            tb.BringToFront();
            return tb;
        }

        private void EnterEditMode()
        {
            if (_selectedReportId is null)
            {
                MessageBox.Show("Please select a report to edit.", "Edit Report", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _isEditingReport = true;
            _txtEditSn.Text = lblGenSnVal.Text;
            ToggleEditControls(showTextboxes: true);

            btnEditReport.Visible = false;
            btnDeleteReport.Visible = false;
            btnSaveReportEdit.Visible = true;
            btnCancelReportEdit.Visible = true;
        }

        private void ExitEditMode(bool refresh)
        {
            _isEditingReport = false;
            ToggleEditControls(showTextboxes: false);

            btnEditReport.Visible = true;
            btnDeleteReport.Visible = true;
            btnSaveReportEdit.Visible = false;
            btnCancelReportEdit.Visible = false;

            if (refresh && _selectedReportId.HasValue)
            {
                LoadPreview(_selectedReportId.Value);
            }
        }

        private void ToggleEditControls(bool showTextboxes)
        {
            lblGenSnVal.Visible = !showTextboxes;
            _txtEditSn.Visible = showTextboxes;
        }

        private void SaveReportEdit()
        {
            if (_selectedReportId is null) return;

            if (string.IsNullOrWhiteSpace(_txtEditSn.Text))
            {
                MessageBox.Show("Serial Number cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _reportService.UpdateSerialNumber(_selectedReportId.Value, _txtEditSn.Text.Trim());

                ExitEditMode(refresh: true);
                LoadReportsGrid(_appliedFilter);
                MessageBox.Show("Report updated successfully.", "Edit Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update report:\n{ex.Message}", "Edit Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static Button CreateActionButton(string text, Color backColor)
        {
            return new Button
            {
                Text = text,
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(64, 30),
                Margin = new Padding(3, 2, 0, 0),
                Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
                UseVisualStyleBackColor = false
            };
        }

        private void WireEvents()
        {
            // Generate = read current control values and apply them as the new filter.
            btnGenerate.Click += (s, e) =>
            {
                _currentPage = 1;
                _appliedFilter = BuildFilterFromControls();
                _appliedFilter.Page = _currentPage;
                RenderReportData(_appliedFilter);
            };

            // Reset = clear controls back to defaults AND clear the applied filter (show everything again).
            btnReset.Click += (s, e) =>
            {
                ResetFilterControls();
                _currentPage = 1;
                _appliedFilter = new ReportFilter { Page = _currentPage, PageSize = _pageSize };
                RenderReportData(_appliedFilter);
            };

            gridReports.SelectionChanged += (s, e) =>
            {
                if (_isEditingReport) return;

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

            btnExportPDF.Click += (s, e) =>
            {
                if (!SessionManager.HasPermission("Action.Reports.Export"))
                {
                    MessageBox.Show("You do not have permission to export reports.",
                        "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_selectedReportId is null)
                {
                    MessageBox.Show("Please select a report first.", "Export PDF", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using SaveFileDialog sfd = new()
                {
                    Filter = "PDF File (*.pdf)|*.pdf",
                    FileName = $"Report_{_selectedReportId}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
                };
                if (sfd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    _reportService.GenerateReportPdf(_selectedReportId.Value, sfd.FileName);
                    MessageBox.Show("Report exported to PDF successfully.", "Export PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to export PDF:\n{ex.Message}", "Export PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            btnExportExcel.Click += (s, e) =>
            {
                if (!SessionManager.HasPermission("Action.Reports.Export"))
                {
                    MessageBox.Show("You do not have permission to export reports.",
                        "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_selectedReportId is null)
                {
                    MessageBox.Show("Please select a report first.", "Export Excel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using SaveFileDialog sfd = new()
                {
                    Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                    FileName = $"Report_{_selectedReportId}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                };
                if (sfd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    _exportService.ExportReportToExcel(_selectedReportId.Value, sfd.FileName);
                    MessageBox.Show("Report exported to Excel successfully.", "Export Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to export Excel:\n{ex.Message}", "Export Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            btnPrint.Click += (s, e) =>
            {
                if (!SessionManager.HasPermission("Action.Reports.Export"))
                {
                    MessageBox.Show("You do not have permission to export reports.",
                        "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Print belum diimplementasikan.", "Info");
            };
        }

        private void ResetFilterControls()
        {
            dtpStart.Value = DateTime.Now.AddDays(-30);
            dtpEnd.Value = DateTime.Now;
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
                PageSize = _pageSize
            };

            if (!string.Equals(cmbModel.Text, "All Models", StringComparison.OrdinalIgnoreCase))
                filter.ProductModel = cmbModel.Text;

            if (!string.Equals(cmbType.Text, "All Types", StringComparison.OrdinalIgnoreCase))
                filter.ProductType = cmbType.Text;

            if (!string.Equals(cmbArea.Text, "All Areas", StringComparison.OrdinalIgnoreCase))
                filter.Area = cmbArea.Text;

            if (!string.IsNullOrWhiteSpace(cmbOperator.Text)
                && !string.Equals(cmbOperator.Text, "All Operators", StringComparison.OrdinalIgnoreCase))
                filter.Operator = cmbOperator.Text;

            if (!string.Equals(cmbResult.Text, "All Results", StringComparison.OrdinalIgnoreCase))
                filter.Result = cmbResult.Text.ToUpperInvariant();

            if (!string.IsNullOrWhiteSpace(txtPartNumber.Text))
                filter.PartNumberKeyword = txtPartNumber.Text.Trim();

            return filter;
        }

        private void RenderReportData(ReportFilter filter)
        {
            LoadSummaryCards(filter);
            LoadTrendChart(filter);
            LoadReportsGrid(filter);
        }

        private void LoadSummaryCards(ReportFilter filter)
        {
            var summary = _reportService.GetSummary(filter);

            lblSummaryTitle.Text = filter.DateFrom.HasValue && filter.DateTo.HasValue
                ? $"SUMMARY ({filter.DateFrom:dd MMM yyyy} - {filter.DateTo:dd MMM yyyy})"
                : "SUMMARY (All Data)";

            lblSumTotalVal.Text = summary.TotalTested.ToString("N0");
            lblSumPassVal.Text = $"{summary.PassCount:N0} ({FormatPct(summary.PassCount, summary.TotalTested)})";
            lblSumFailVal.Text = $"{summary.FailCount:N0} ({FormatPct(summary.FailCount, summary.TotalTested)})";
            lblSumProgVal.Text = summary.InProgressCount.ToString("N0");
        }

        private static string FormatPct(int count, int total) => total == 0 ? "0.00%" : $"{count * 100.0 / total:0.00}%";

        private void LoadTrendChart(ReportFilter filter)
        {
            var trend = _reportService.GetResultTrend(filter);

            chartTrend.ChartAreas.Clear();
            chartTrend.Series.Clear();
            chartTrend.Legends.Clear();

            ChartArea areaTrend = new("TrendArea") { BackColor = Color.White };
            areaTrend.AxisX.MajorGrid.Enabled = false;
            areaTrend.AxisX.Interval = 1;
            areaTrend.AxisX.LabelStyle.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font85;
            areaTrend.AxisX.LabelStyle.ForeColor = Color.FromArgb(100, 116, 139);
            areaTrend.AxisX.LineColor = Color.FromArgb(226, 232, 240);

            areaTrend.AxisY.MajorGrid.LineColor = Color.FromArgb(241, 245, 249);
            areaTrend.AxisY.LabelStyle.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font85;
            areaTrend.AxisY.LabelStyle.ForeColor = Color.FromArgb(100, 116, 139);
            areaTrend.AxisY.LineColor = Color.Transparent;
            areaTrend.AxisY.Minimum = 0;
            chartTrend.ChartAreas.Add(areaTrend);

            Legend legTrend = new("TrendLegend")
            {
                Docking = Docking.Top,
                Alignment = StringAlignment.Center,
                Font = AssemblyLineTester.Helpers.UIStyleHelper.Font85Bold
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
                gridReports.Rows[rowIdx].Height = gridReports.RowTemplate.Height;
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
            _appliedFilter.Page = _currentPage;
            LoadReportsGrid(_appliedFilter);
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
                    e.CellStyle.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font85Bold;
                }
                else
                {
                    e.CellStyle.BackColor = Color.FromArgb(241, 245, 249);
                    e.CellStyle.ForeColor = Color.FromArgb(51, 65, 85);
                    e.CellStyle.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font85Bold;
                }
            }
            else if (colName == "colAction")
            {
                e.CellStyle.ForeColor = Color.FromArgb(37, 99, 235);
                e.CellStyle.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font85Bold;
            }
        }

        private void SetupEvents()
        {
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardFilters, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardTrend, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardReportList, 12, Color.FromArgb(34, 40, 49), 2.5f);

            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(pnlSumTotal, 10, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(pnlSumPass, 10, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(pnlSumFail, 10, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(pnlSumProg, 10, Color.FromArgb(34, 40, 49), 2.5f);

            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(pnlPreviewContainer, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardPaper, 10, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(pnlGenInfoBox, 8, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(pnlNotesCard, 8, Color.FromArgb(34, 40, 49), 2.5f);

            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnGenerate, 10, Color.FromArgb(250, 204, 21), Color.FromArgb(254, 216, 53), Color.FromArgb(234, 186, 10), Color.Transparent);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnReset, 10, Color.FromArgb(241, 245, 249), Color.FromArgb(226, 232, 240), Color.FromArgb(203, 213, 225), Color.Transparent);

            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnExportPDF, 10, Color.FromArgb(250, 204, 21), Color.FromArgb(254, 216, 53), Color.FromArgb(234, 186, 10), Color.Transparent);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnExportExcel, 10, Color.White, Color.FromArgb(248, 250, 252), Color.FromArgb(241, 245, 249), Color.FromArgb(203, 213, 225));
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnPrint, 10, Color.White, Color.FromArgb(248, 250, 252), Color.FromArgb(241, 245, 249), Color.FromArgb(203, 213, 225));
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnEditReport, 10, Color.FromArgb(37, 99, 235), Color.FromArgb(59, 130, 246), Color.FromArgb(29, 78, 216), Color.Transparent);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnDeleteReport, 10, Color.FromArgb(220, 38, 38), Color.FromArgb(239, 68, 68), Color.FromArgb(185, 28, 28), Color.Transparent);

            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnPageFirst, 8, Color.White, Color.FromArgb(248, 250, 252), Color.FromArgb(241, 245, 249), Color.Transparent);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnPagePrev, 8, Color.White, Color.FromArgb(248, 250, 252), Color.FromArgb(241, 245, 249), Color.Transparent);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnPage1, 8, Color.FromArgb(250, 204, 21), Color.FromArgb(254, 216, 53), Color.FromArgb(234, 186, 10), Color.Transparent);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnPage2, 8, Color.White, Color.FromArgb(248, 250, 252), Color.FromArgb(241, 245, 249), Color.Transparent);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnPageLast, 8, Color.White, Color.FromArgb(248, 250, 252), Color.FromArgb(241, 245, 249), Color.Transparent);
        }
    }
}