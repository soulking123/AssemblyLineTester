using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using AssemblyLineTester.Models.Dto;
using AssemblyLineTester.Services;

namespace AssemblyLineTester.Views.Pages
{
    public partial class TestHistory : UserControl
    {
        private const int DefaultPageSize = 10;

        private readonly TestHistoryService _historyService = new();
        private readonly TestReportService _reportService = new();

        private int _currentPage = 1;
        private int _pageSize = DefaultPageSize;
        private ReportSummary _summary = new();
        private bool _dateFilterApplied = false;

        public TestHistory()
        {
            InitializeComponent();
            SetupGridColumns();
            SetupStepsGridColumns();
            SetupEvents();
            this.VisibleChanged += TestHistory_VisibleChanged;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                AssemblyLineTester.Helpers.UIStyleHelper.ScaleExplicitFonts(this, AssemblyLineTester.Helpers.UIStyleHelper.GetHostScaleFactor(this));
                if (cmbPageSize.Items.Count > 0) cmbPageSize.SelectedIndex = 0;

                PopulateFilterLookups();
                ResetFilterControls();
                LoadHistoryData();
            }
        }

        private void TestHistory_VisibleChanged(object? sender, EventArgs e)
        {
            if (this.Visible && !DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                LoadHistoryData();
            }
        }



        // Registers all filter/pagination/selection controls to a single reload flow.
        private void SetupEvents()
        {
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(pnlFilterCard, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(pnlLeft, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(pnlDetail, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(pnlSummary, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(pnlSteps, 12, Color.FromArgb(34, 40, 49), 2.5f);

            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnSearch, 10, Color.FromArgb(250, 204, 21), Color.FromArgb(254, 216, 53), Color.FromArgb(234, 186, 10), Color.Transparent);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnReset, 10, Color.FromArgb(241, 245, 249), Color.FromArgb(226, 232, 240), Color.FromArgb(203, 213, 225), Color.Transparent);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnRefresh, 10, Color.FromArgb(241, 245, 249), Color.FromArgb(248, 250, 252), Color.FromArgb(241, 245, 249), Color.Transparent);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnExport, 10, Color.FromArgb(241, 245, 249), Color.FromArgb(248, 250, 252), Color.FromArgb(241, 245, 249), Color.Transparent);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnViewReport, 10, Color.FromArgb(250, 204, 21), Color.FromArgb(254, 216, 53), Color.FromArgb(234, 186, 10), Color.Transparent);

            AssemblyLineTester.Helpers.UIStyleHelper.SetupBadgeRoundedEdges(lblDetailBadge, 10);

            gridData.CellFormatting += gridData_CellFormatting;
            gridData.CellPainting += gridData_CellPainting;
            gridData.CellClick += GridData_CellClick;
            gridSteps.CellFormatting += gridSteps_CellFormatting;

            btnRefresh.Click += (s, ev) => LoadHistoryData();

            // Only an actual date pick counts as "user wants this date range" - Search itself
            // must not silently narrow every query to today just because the pickers default to it.
            dtpStart.ValueChanged += (s, ev) => _dateFilterApplied = true;
            dtpEnd.ValueChanged += (s, ev) => _dateFilterApplied = true;

            btnSearch.Click += (s, ev) => GoToPage(1);

            // Enter in the free-text box does what Search does, or the box reads like a dead end.
            txtOperator.KeyDown += (s, ev) =>
            {
                if (ev.KeyCode != Keys.Enter) return;
                ev.SuppressKeyPress = true;
                GoToPage(1);
            };

            btnReset.Click += (s, ev) =>
            {
                ResetFilterControls();
                GoToPage(1);
            };

            cmbPageSize.SelectedIndexChanged += (s, ev) =>
            {
                if (int.TryParse(cmbPageSize.SelectedItem?.ToString(), out int newSize) && newSize > 0)
                {
                    _pageSize = newSize;
                    GoToPage(1);
                }
            };

            txtGoToPage.KeyDown += (s, ev) =>
            {
                if (ev.KeyCode == Keys.Enter)
                {
                    ev.SuppressKeyPress = true;
                    if (int.TryParse(txtGoToPage.Text, out int pageNum) && pageNum >= 1)
                    {
                        GoToPage(pageNum);
                    }
                }
            };
        }

        // Choices come from the ProductModels and Areas tables, so a row added there shows up here
        // without a rebuild. "All" stays first and is what the filter reads as "no restriction".
        private void PopulateFilterLookups()
        {
            cmbProductModel.Items.Clear();
            cmbProductModel.Items.Add("All");
            cmbProductModel.Items.AddRange(LookupService.GetProductModelNames().ToArray());

            cmbArea.Items.Clear();
            cmbArea.Items.Add("All");
            cmbArea.Items.AddRange(LookupService.GetAreaNames().ToArray());
        }

        private void ResetFilterControls()
        {
            dtpStart.Value = DateTime.Today;
            dtpEnd.Value = DateTime.Today;
            if (cmbProductModel.Items.Count > 0) cmbProductModel.SelectedIndex = 0;
            if (cmbArea.Items.Count > 0) cmbArea.SelectedIndex = 0;
            if (cmbResult.Items.Count > 0) cmbResult.SelectedIndex = 0;
            txtOperator.Clear();
            _dateFilterApplied = false; // back to showing everything, even though the pickers show today
        }

        private void GoToPage(int page)
        {
            _currentPage = Math.Max(1, page);
            LoadHistoryData();
        }

        // Builds the filter DTO from current filter control values.
        private ReportFilter BuildFilter(int page)
        {
            return new ReportFilter
            {
                DateFrom = _dateFilterApplied ? dtpStart.Value.Date : (DateTime?)null,
                DateTo = _dateFilterApplied ? dtpEnd.Value.Date : (DateTime?)null,
                ProductModel = cmbProductModel.Text == "All" ? null : cmbProductModel.Text,
                Area = cmbArea.Text == "All" ? null : cmbArea.Text,
                Result = cmbResult.Text == "All" ? null : cmbResult.Text,
                // Typed by hand and matched on substring, so a surname or a username both land.
                Operator = string.IsNullOrWhiteSpace(txtOperator.Text) ? null : txtOperator.Text.Trim(),
                Page = page,
                PageSize = _pageSize
            };
        }

        // Main entry point: pulls the page of history rows and the summary, refreshes the whole UI.
        private void LoadHistoryData()
        {
            var filter = BuildFilter(_currentPage);
            var paged = _historyService.GetHistoryList(filter);
            _summary = _historyService.GetHistorySummary(filter);

            PopulateHistoryGrid(paged);
            UpdateSummaryChart();
            UpdatePaginationUI(paged.TotalCount, paged.TotalPages);
            ClearDetailPanel();
        }


        private static void SetColumnWidth(DataGridView grid, string colName, float fillWeight, DataGridViewContentAlignment alignment = DataGridViewContentAlignment.MiddleLeft)
        {
            var col = grid.Columns[colName];
            if (col is not null)
            {
                col.FillWeight = fillWeight;
                col.DefaultCellStyle.Alignment = alignment;
            }
        }

        // Sets up grid columns and styling once; called from the constructor only.
        private void SetupGridColumns()
        {
            AssemblyLineTester.Helpers.UIStyleHelper.StyleDataGridView(gridData);
            gridData.ColumnHeadersHeight = 44;
            gridData.RowTemplate.Height = 40;
            gridData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridData.MultiSelect = false;

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

            SetColumnWidth(gridData, "No", 45, DataGridViewContentAlignment.MiddleCenter);
            SetColumnWidth(gridData, "TestTime", 165);
            SetColumnWidth(gridData, "ProductModel", 95);
            SetColumnWidth(gridData, "ProductType", 95, DataGridViewContentAlignment.MiddleCenter);
            SetColumnWidth(gridData, "Area", 145);
            SetColumnWidth(gridData, "PartNumber", 125);
            SetColumnWidth(gridData, "SerialNumber", 110);
            SetColumnWidth(gridData, "Result", 95, DataGridViewContentAlignment.MiddleCenter);
            SetColumnWidth(gridData, "TotalTime", 100, DataGridViewContentAlignment.MiddleCenter);
            SetColumnWidth(gridData, "Operator", 95);
        }

        private void PopulateHistoryGrid(PagedResult<TestHistoryListItem> paged)
        {
            lblTableTitle.Text = $"TEST HISTORY LIST (Total: {paged.TotalCount})";
            gridData.Rows.Clear();

            int startNo = (paged.Page - 1) * paged.PageSize;
            for (int i = 0; i < paged.Items.Count; i++)
            {
                var item = paged.Items[i];
                string displayResult = item.Status == "Completed"
                    ? (item.Result ?? "-")
                    : ToDisplayStatus(item.Status);

                int rowIndex = gridData.Rows.Add(
                    (startNo + i + 1).ToString(),
                    item.TestTime.ToString("dd MMM yyyy hh:mm:ss tt"),
                    item.ProductModel,
                    item.ProductType,
                    item.Area,
                    item.PartNumber,
                    item.SerialNumber,
                    displayResult,
                    item.TotalTime,
                    item.Operator);

                // Store the report id on the row so a click can load its detail without a re-query.
                gridData.Rows[rowIndex].Tag = item.ReportId;
            }

            UpdatePaginationInfoLabel(paged.TotalCount);
        }

        private static string ToDisplayStatus(string status) => status switch
        {
            "InProgress" => "IN PROGRESS",
            "Stopped" => "STOPPED",
            _ => status.ToUpperInvariant()
        };

        private void UpdatePaginationInfoLabel(int totalFiltered)
        {
            if (totalFiltered == 0)
            {
                lblEntries.Text = "Showing 0 to 0 of 0 entries";
                return;
            }

            int start = (_currentPage - 1) * _pageSize + 1;
            int end = Math.Min(_currentPage * _pageSize, totalFiltered);
            lblEntries.Text = $"Showing {start} to {end} of {totalFiltered} entries";
        }

        private void UpdatePaginationUI(int totalFiltered, int totalPages)
        {
            if (totalPages == 0) totalPages = 1;
            if (_currentPage > totalPages) _currentPage = totalPages;
            if (_currentPage < 1) _currentPage = 1;

            if (flpPageNav is null) return;
            flpPageNav.Controls.Clear();

            float scaleX = this.CurrentAutoScaleDimensions.Width / 13f;
            float scaleY = this.CurrentAutoScaleDimensions.Height / 32f;

            var btnFirst = CreatePageButton("«", false, _currentPage > 1, (s, e) => GoToPage(1), scaleX, scaleY);
            flpPageNav.Controls.Add(btnFirst);

            var btnPrev = CreatePageButton("‹", false, _currentPage > 1, (s, e) => GoToPage(_currentPage - 1), scaleX, scaleY);
            flpPageNav.Controls.Add(btnPrev);

            for (int p = 1; p <= totalPages; p++)
            {
                if (p == 1 || p == totalPages || (p >= _currentPage - 1 && p <= _currentPage + 1))
                {
                    int pageNum = p;
                    var btnPage = CreatePageButton(pageNum.ToString(), pageNum == _currentPage, true, (s, e) => GoToPage(pageNum), scaleX, scaleY);
                    flpPageNav.Controls.Add(btnPage);
                }
                else if (p == _currentPage - 2 || p == _currentPage + 2)
                {
                    var btnDots = CreatePageButton("...", false, false, null, scaleX, scaleY);
                    flpPageNav.Controls.Add(btnDots);
                }
            }

            var btnNext = CreatePageButton("›", false, _currentPage < totalPages, (s, e) => GoToPage(_currentPage + 1), scaleX, scaleY);
            flpPageNav.Controls.Add(btnNext);

            var btnLast = CreatePageButton("»", false, _currentPage < totalPages, (s, e) => GoToPage(totalPages), scaleX, scaleY);
            flpPageNav.Controls.Add(btnLast);
        }

        private static Button CreatePageButton(string text, bool isActive, bool isEnabled, EventHandler? onClick, float scaleX, float scaleY)
        {
            Button btn = new()
            {
                Text = text,
                Size = new Size((int)(38 * scaleX), (int)(40 * scaleY)),
                Margin = new Padding((int)(2 * scaleX), (int)(6 * scaleY), (int)(2 * scaleX), (int)(6 * scaleY)),
                FlatStyle = FlatStyle.Flat,
                Enabled = isEnabled,
                Cursor = isEnabled ? Cursors.Hand : Cursors.Default,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(0, 0, 0, (int)(2 * scaleY)),
                UseCompatibleTextRendering = true
            };
            btn.FlatAppearance.BorderSize = 1;

            if (text == "...")
            {
                btn.BackColor = Color.Transparent;
                btn.ForeColor = Color.FromArgb(100, 116, 139);
                btn.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font95Bold;
                btn.FlatAppearance.BorderSize = 0;
                btn.Enabled = false;
                btn.Cursor = Cursors.Default;
                AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btn, 8, Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent);
            }
            else if (isActive)
            {
                btn.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font95Bold;
                AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btn, 8, Color.FromArgb(250, 204, 21), Color.FromArgb(254, 216, 53), Color.FromArgb(234, 186, 10), Color.Transparent);
            }
            else if (!isEnabled)
            {
                btn.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font95Regular;
                AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btn, 8, Color.FromArgb(248, 250, 252), Color.FromArgb(248, 250, 252), Color.FromArgb(248, 250, 252), Color.Transparent);
            }
            else
            {
                btn.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font95Regular;
                AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btn, 8, Color.White, Color.FromArgb(248, 250, 252), Color.FromArgb(241, 245, 249), Color.Transparent);
            }

            if (onClick is not null)
            {
                btn.Click += onClick;
            }

            return btn;
        }

        private void SetupStepsGridColumns()
        {
            AssemblyLineTester.Helpers.UIStyleHelper.StyleDataGridView(gridSteps);
            gridSteps.ColumnHeadersHeight = 38;
            gridSteps.RowTemplate.Height = 36;

            gridSteps.Columns.Add("StepNo", "Step No.");
            gridSteps.Columns.Add("TestStep", "Test Step");
            gridSteps.Columns.Add("Result", "Result");
            gridSteps.Columns.Add("Time", "Time");

            SetColumnWidth(gridSteps, "StepNo", 45, DataGridViewContentAlignment.MiddleCenter);
            SetColumnWidth(gridSteps, "TestStep", 150);
            SetColumnWidth(gridSteps, "Result", 70, DataGridViewContentAlignment.MiddleCenter);
            SetColumnWidth(gridSteps, "Time", 75, DataGridViewContentAlignment.MiddleCenter);
        }

        private void PopulateStepsGrid(List<TestStepResultDto> steps)
        {
            gridSteps.Rows.Clear();
            foreach (var step in steps)
            {
                gridSteps.Rows.Add(step.StepNo.ToString(), step.TestType, step.Result, step.Time);
            }
        }

        private void GridData_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (gridData.Rows[e.RowIndex].Tag is not int reportId) return;
            LoadDetail(reportId);
        }

        private void LoadDetail(int reportId)
        {
            var detail = _historyService.GetHistoryDetail(reportId);
            if (detail is null) return;

            lblDtTestTimeV.Text = detail.StartTime.ToString("dd MMM yyyy hh:mm:ss tt");
            lblDtModelV.Text = detail.ProductModel;
            lblDtTotalV.Text = detail.TotalTime;
            lblDtTypeV.Text = detail.ProductType;
            lblDtStartV.Text = detail.StartTime.ToString("hh:mm:ss tt");
            lblDtAreaV.Text = detail.Area;
            lblDtEndV.Text = detail.EndTime?.ToString("hh:mm:ss tt") ?? "-";
            lblDtPartV.Text = detail.PartNumber;
            lblDtSerialV.Text = detail.SerialNumber;
            lblDtEcnV.Text = detail.ECNNumber;
            lblDtOpV.Text = detail.Operator;

            string resultText = detail.Status == "Completed"
                ? (detail.OverallResult ?? "-")
                : ToDisplayStatus(detail.Status);
            lblDtResultV.Text = resultText;
            lblDetailBadge.Text = resultText;
            ApplyBadgeColor(lblDetailBadge, resultText);

            btnViewReport.Enabled = true;
            PopulateStepsGrid(detail.Steps);

            // Enable delete button when a detail is loaded
            btnDeleteHistory.Enabled = true;
        }

        private void BtnDeleteHistory_Click(object? sender, EventArgs e)
        {
            if (gridData.CurrentRow == null || gridData.CurrentRow.Tag is not int reportId)
            {
                MessageBox.Show("Please select a history item to delete.", "Delete History", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Delete this test history and its steps? This cannot be undone.", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                bool ok = _historyService.DeleteHistory(reportId);
                if (ok)
                {
                    MessageBox.Show("History deleted.", "Delete History", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadHistoryData();
                    ClearDetailPanel();
                    btnDeleteHistory.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Failed to delete history.", "Delete History", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete History", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ClearDetailPanel()
        {
            foreach (var lbl in new[] { lblDtTestTimeV, lblDtModelV, lblDtTotalV, lblDtTypeV, lblDtStartV,
                lblDtAreaV, lblDtEndV, lblDtPartV, lblDtSerialV, lblDtEcnV, lblDtOpV })
            {
                lbl.Text = "-";
            }

            lblDtResultV.Text = "-";
            lblDetailBadge.Text = "-";
            btnViewReport.Enabled = false;
            gridSteps.Rows.Clear();
        }

        private static void ApplyBadgeColor(Label badge, string result)
        {
            switch (result)
            {
                case "PASS":
                    badge.BackColor = Color.FromArgb(209, 231, 221);
                    badge.ForeColor = Color.FromArgb(15, 81, 50);
                    break;
                case "FAIL":
                    badge.BackColor = Color.FromArgb(248, 215, 218);
                    badge.ForeColor = Color.FromArgb(132, 32, 41);
                    break;
                case "IN PROGRESS":
                    badge.BackColor = Color.FromArgb(254, 243, 199);
                    badge.ForeColor = Color.FromArgb(146, 64, 14);
                    break;
                default: // STOPPED or unknown
                    badge.BackColor = Color.FromArgb(226, 232, 240);
                    badge.ForeColor = Color.FromArgb(71, 85, 105);
                    break;
            }
        }

        private void UpdateSummaryChart()
        {
            lblLegPassText.Text = $"Pass : {FormatCountWithPercent(_summary.PassCount)}";
            lblLegFailText.Text = $"Fail : {FormatCountWithPercent(_summary.FailCount)}";
            lblLegProgText.Text = $"In Progress : {FormatCountWithPercent(_summary.InProgressCount)}";
            lblLegStopText.Text = $"Stop : {FormatCountWithPercent(_summary.StopCount)}";


            pnlChartContainer.Controls.Clear();
            var chartPanel = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
            chartPanel.Paint += (s, e) => DrawSummaryDoughnut(e, chartPanel);
            pnlChartContainer.Controls.Add(chartPanel);
        }

        private string FormatCountWithPercent(int count)
        {
            double pct = _summary.TotalTested == 0 ? 0 : count * 100.0 / _summary.TotalTested;
            return $"{count} ({pct:F1}%)";
        }

        private void DrawSummaryDoughnut(PaintEventArgs e, Panel container)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            int size = Math.Min(container.Width, container.Height) - 20;
            if (size <= 0) return;
            int x = (container.Width - size) / 2;
            int y = (container.Height - size) / 2;
            var rect = new Rectangle(x, y, size, size);

            double total = _summary.TotalTested;
            float passSweep = total <= 0 ? 0 : (float)(_summary.PassCount / total * 360.0);
            float failSweep = total <= 0 ? 0 : (float)(_summary.FailCount / total * 360.0);
            float progSweep = total <= 0 ? 0 : (float)(_summary.InProgressCount / total * 360.0);
            float stopSweep = total <= 0 ? 0 : (float)(_summary.StopCount / total * 360.0);

            using var penPass = new Pen(Color.FromArgb(76, 175, 80), 18);
            using var penFail = new Pen(Color.FromArgb(244, 67, 54), 18);
            using var penProg = new Pen(Color.FromArgb(245, 158, 11), 18);
            using var penStop = new Pen(Color.FromArgb(148, 163, 184), 18);

            float angle = -90f;
            e.Graphics.DrawArc(penPass, rect, angle, passSweep); angle += passSweep;
            e.Graphics.DrawArc(penFail, rect, angle, failSweep); angle += failSweep;
            e.Graphics.DrawArc(penProg, rect, angle, progSweep); angle += progSweep;
            e.Graphics.DrawArc(penStop, rect, angle, stopSweep);

            string text = $"{_summary.TotalTested}\nTOTAL";
            using var brush = new SolidBrush(Color.FromArgb(30, 41, 59));
            using var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            e.Graphics.DrawString(text, AssemblyLineTester.Helpers.UIStyleHelper.Font95Bold, brush, new RectangleF(x, y, size, size), format);
        }

        private static void gridData_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            // Badge rendering handled entirely by gridData_CellPainting below.
        }

        private void gridData_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && gridData.Columns[e.ColumnIndex].Name == "Result" && e.Value is not null && e.Graphics is not null)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                string val = e.Value.ToString() ?? "";
                Color bgCol, fgCol;
                switch (val)
                {
                    case "FAIL":
                        bgCol = Color.FromArgb(248, 215, 218); fgCol = Color.FromArgb(132, 32, 41);
                        break;
                    case "IN PROGRESS":
                        bgCol = Color.FromArgb(254, 243, 199); fgCol = Color.FromArgb(146, 64, 14);
                        break;
                    case "STOPPED":
                        bgCol = Color.FromArgb(226, 232, 240); fgCol = Color.FromArgb(71, 85, 105);
                        break;
                    default: // PASS
                        bgCol = Color.FromArgb(209, 231, 221); fgCol = Color.FromArgb(15, 81, 50);
                        break;
                }

                Rectangle badgeRect = new(e.CellBounds.X + 8, e.CellBounds.Y + 7, e.CellBounds.Width - 16, e.CellBounds.Height - 14);
                using var brush = new SolidBrush(bgCol);
                using var path = AssemblyLineTester.Helpers.UIStyleHelper.GetRoundedRectPath(badgeRect, 10);
                e.Graphics.FillPath(brush, path);

                TextRenderer.DrawText(e.Graphics, val, AssemblyLineTester.Helpers.UIStyleHelper.Font85Bold, badgeRect, fgCol, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                e.Handled = true;
            }
        }

        private static void gridSteps_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.Value != null)
            {
                if (sender is not DataGridView grid || e.ColumnIndex >= grid.Columns.Count) return;
                if (grid.Columns[e.ColumnIndex].Name == "Result")
                {
                    string val = e.Value.ToString() ?? "";
                    if (val == "PASS")
                    {
                        e.Value = "✔ PASS";
                        e.CellStyle.ForeColor = Color.FromArgb(22, 101, 52);
                        e.CellStyle.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font9Bold;
                        e.FormattingApplied = true;
                    }
                    else if (val == "FAIL")
                    {
                        e.Value = "✖ FAIL";
                        e.CellStyle.ForeColor = Color.FromArgb(153, 27, 27);
                        e.CellStyle.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font9Bold;
                        e.FormattingApplied = true;
                    }
                }
            }
        }
    }
}