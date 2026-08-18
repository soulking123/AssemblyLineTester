using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using AssemblyLineTester.Models.Dto;
using AssemblyLineTester.Services;
using AssemblyLineTester.Helpers;

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


        // Points every filter, pagination and selection control at the same reload.
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

            // Tinted, not solid: the weight belongs to View Report, and a dialog guards the delete.
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnDeleteHistory, 10, Color.FromArgb(254, 242, 242), Color.FromArgb(254, 226, 226), Color.FromArgb(254, 202, 202), Color.FromArgb(254, 202, 202));

            // Red-700 for 4.5:1 contrast on the red-50 fill; red-500 falls short at this size.
            btnDeleteHistory.ForeColor = Color.FromArgb(185, 28, 28);

            AssemblyLineTester.Helpers.UIStyleHelper.SetupBadgeRoundedEdges(lblDetailBadge, 10);

            gridData.CellFormatting += gridData_CellFormatting;
            gridData.CellPainting += gridData_CellPainting;
            gridData.CellClick += GridData_CellClick;
            gridSteps.CellFormatting += gridSteps_CellFormatting;
            
            chartSummary.PostPaint += ChartSummary_PostPaint;

            btnRefresh.Click += (s, ev) => LoadHistoryData();

            btnSearch.Click += (s, ev) =>
            {
                _dateFilterApplied = true;
                GoToPage(1);
            };

            // Enter in the free-text box does what Search does, or the box reads like a dead end.
            txtOperator.KeyDown += (s, ev) =>
            {
                if (ev.KeyCode != Keys.Enter) return;
                ev.SuppressKeyPress = true;
                _dateFilterApplied = true;
                GoToPage(1);
            };

            btnReset.Click += (s, ev) =>
            {
                ResetFilterControls();
                _dateFilterApplied = false;
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
        }

        private void GoToPage(int page)
        {
            _currentPage = Math.Max(1, page);
            LoadHistoryData();
        }

        // Reads the filter controls into a ReportFilter.
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

        // Pulls the page of rows and the summary, then refreshes the whole page.
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

        // Columns and styling only, once, from the constructor.
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

                // The row carries its report id, so a click needs no re-query to find it.
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
            UIStyleHelper.ClearAndDispose(flpPageNav.Controls);

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

            btnDeleteHistory.Enabled = true;
        }

        private void BtnDeleteHistory_Click(object? sender, EventArgs e)
        {
            if (!SessionManager.HasPermission("Action.TestHistory.Delete"))
            {
                MessageBox.Show("You do not have permission to delete test history records.",
                    "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (gridData.CurrentRow == null || gridData.CurrentRow.Tag is not int reportId)
            {
                MessageBox.Show("Please select a history item to delete.", "Delete History", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Delete this test history and its steps? This cannot be undone.", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

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
                default:
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

            LoadSummaryChart();
        }

        private string FormatCountWithPercent(int count)
        {
            double pct = _summary.TotalTested == 0 ? 0 : count * 100.0 / _summary.TotalTested;
            return $"{count} ({pct:F1}%)";
        }

        private void LoadSummaryChart()
        {
            chartSummary.ChartAreas.Clear();
            chartSummary.Series.Clear();
            chartSummary.Legends.Clear();
            chartSummary.BackColor = Color.White;

            ChartArea areaPie = new("PieArea") { BackColor = Color.White };
            chartSummary.ChartAreas.Add(areaPie);

            Series sSummary = new("Summary") { ChartType = SeriesChartType.Doughnut };
            sSummary["DoughnutRadius"] = "25";

            if (_summary.TotalTested == 0)
            {
                int emptyIdx = sSummary.Points.AddY(1);
                sSummary.Points[emptyIdx].Color = Color.FromArgb(226, 232, 240);
                sSummary.Points[emptyIdx].IsVisibleInLegend = false;
            }
            else
            {
                var summaryData = new (int Count, Color Color)[]
                {
                    (_summary.PassCount, Color.FromArgb(76, 175, 80)),
                    (_summary.FailCount, Color.FromArgb(244, 67, 54)),
                    (_summary.InProgressCount, Color.FromArgb(245, 158, 11)),
                    (_summary.StopCount, Color.FromArgb(148, 163, 184))
                };

                foreach (var data in summaryData)
                {
                    if (data.Count > 0)
                    {
                        int idx = sSummary.Points.AddY(data.Count);
                        sSummary.Points[idx].Color = data.Color;
                    }
                }
            }

            chartSummary.Series.Add(sSummary);
        }

        private void ChartSummary_PostPaint(object? sender, ChartPaintEventArgs e)
        {
            if (e.ChartElement is ChartArea area)
            {
                string numText = _summary.TotalTested.ToString();
                string subText = "TOTAL";

                using SolidBrush brushNum = new(Color.FromArgb(30, 41, 59));
                using SolidBrush brushSub = new(Color.FromArgb(100, 116, 139));
                using StringFormat sf = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

                RectangleF chartRect = e.ChartGraphics.GetAbsoluteRectangle(area.Position.ToRectangleF());
                float centerX = chartRect.Left + (chartRect.Width * 0.50f);
                float centerY = chartRect.Top + (chartRect.Height * 0.5f);

                e.ChartGraphics.Graphics.DrawString(numText, AssemblyLineTester.Helpers.UIStyleHelper.Font16Bold, brushNum, centerX, centerY - 14, sf);
                e.ChartGraphics.Graphics.DrawString(subText, AssemblyLineTester.Helpers.UIStyleHelper.Font85Bold, brushSub, centerX, centerY + 14, sf);
            }
        }

        private static void gridData_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            // Badges are drawn by gridData_CellPainting.
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
                    default:
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