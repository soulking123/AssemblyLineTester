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
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                _reportService.SeedSampleDataIfEmpty();
                if (cmbPageSize.Items.Count > 0) cmbPageSize.SelectedIndex = 0;

                ResetFilterControls();
                LoadHistoryData();
            }
        }

        private void LogoBox_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using var pen = new Pen(Color.FromArgb(15, 23, 42), 2.2f);
            Rectangle rect = new(4, 4, 22, 22);
            e.Graphics.DrawEllipse(pen, rect);
            e.Graphics.DrawLine(pen, 15, 15, 15, 9);
            e.Graphics.DrawLine(pen, 15, 15, 19, 17);
        }

        private static GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            int d = radius * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        // Registers all filter/pagination/selection controls to a single reload flow.
        private void SetupEvents()
        {
            pnlFilterCard.Paint += pnlFilterCard_Paint;
            gridData.CellFormatting += gridData_CellFormatting;
            gridData.CellPainting += gridData_CellPainting;
            gridData.CellClick += GridData_CellClick;
            gridSteps.CellFormatting += gridSteps_CellFormatting;

            btnRefresh.Click += (s, ev) => LoadHistoryData();

            btnSearch.Click += (s, ev) =>
            {
                _dateFilterApplied = true; // user explicitly asked for this date range now
                GoToPage(1);
            };

            btnReset.Click += (s, ev) =>
            {
                ResetFilterControls();
                _dateFilterApplied = false; // back to showing everything
                GoToPage(1);
            };
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

        private void ResetFilterControls()
        {
            dtpStart.Value = DateTime.Today;
            dtpEnd.Value = DateTime.Today;
            if (cmbProductModel.Items.Count > 0) cmbProductModel.SelectedIndex = 0;
            if (cmbProductType.Items.Count > 0) cmbProductType.SelectedIndex = 0;
            if (cmbArea.Items.Count > 0) cmbArea.SelectedIndex = 0;
            if (cmbResult.Items.Count > 0) cmbResult.SelectedIndex = 0;
            if (cmbOperator.Items.Count > 0) cmbOperator.SelectedIndex = 0;
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
                ProductType = cmbProductType.Text == "All" ? null : cmbProductType.Text,
                Area = cmbArea.Text == "All" ? null : cmbArea.Text,
                Result = cmbResult.Text == "All" ? null : cmbResult.Text,
                Operator = cmbOperator.Text == "All" ? null : cmbOperator.Text,
                Page = page,
                PageSize = _pageSize
            };
        }

        // Main entry point: pulls the page of history rows and the summary, refreshes the whole UI.
        private void LoadHistoryData()
        {
            var filter = BuildFilter(_currentPage);
            var paged = _historyService.GetHistoryList(filter);
            _summary = _reportService.GetSummary(filter);

            PopulateHistoryGrid(paged);
            UpdateSummaryChart();
            UpdatePaginationUI(paged.TotalCount, paged.TotalPages);
            ClearDetailPanel();
        }

        private void pnlFilterCard_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using var pen = new Pen(Color.FromArgb(226, 232, 240), 1.2f);
            using var path = GetRoundedPath(new Rectangle(0, 0, pnlFilterCard.Width - 1, pnlFilterCard.Height - 1), 8);
            e.Graphics.DrawPath(pen, path);
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
            gridData.Columns.Add("Action", "Action");

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
            SetColumnWidth(gridData, "Action", 60, DataGridViewContentAlignment.MiddleCenter);
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
                    item.Operator,
                    "⋮");

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

            var btnFirst = CreatePageButton("«", false, _currentPage > 1, (s, e) => GoToPage(1));
            flpPageNav.Controls.Add(btnFirst);

            var btnPrev = CreatePageButton("‹", false, _currentPage > 1, (s, e) => GoToPage(_currentPage - 1));
            flpPageNav.Controls.Add(btnPrev);

            for (int p = 1; p <= totalPages; p++)
            {
                if (p == 1 || p == totalPages || (p >= _currentPage - 1 && p <= _currentPage + 1))
                {
                    int pageNum = p;
                    var btnPage = CreatePageButton(pageNum.ToString(), pageNum == _currentPage, true, (s, e) => GoToPage(pageNum));
                    flpPageNav.Controls.Add(btnPage);
                }
                else if (p == _currentPage - 2 || p == _currentPage + 2)
                {
                    var btnDots = CreatePageButton("...", false, false, null);
                    flpPageNav.Controls.Add(btnDots);
                }
            }

            var btnNext = CreatePageButton("›", false, _currentPage < totalPages, (s, e) => GoToPage(_currentPage + 1));
            flpPageNav.Controls.Add(btnNext);

            var btnLast = CreatePageButton("»", false, _currentPage < totalPages, (s, e) => GoToPage(totalPages));
            flpPageNav.Controls.Add(btnLast);
        }

        private static Button CreatePageButton(string text, bool isActive, bool isEnabled, EventHandler? onClick)
        {
            Button btn = new()
            {
                Text = text,
                Size = new Size(38, 40),
                Margin = new Padding(2, 6, 2, 6),
                FlatStyle = FlatStyle.Flat,
                Enabled = isEnabled,
                Cursor = isEnabled ? Cursors.Hand : Cursors.Default,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(0, 0, 0, 2),
                UseCompatibleTextRendering = true
            };
            btn.FlatAppearance.BorderSize = 1;

            if (text == "...")
            {
                btn.BackColor = Color.Transparent;
                btn.ForeColor = Color.FromArgb(100, 116, 139);
                btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                btn.FlatAppearance.BorderSize = 0;
                btn.Enabled = false;
                btn.Cursor = Cursors.Default;
            }
            else if (isActive)
            {
                btn.BackColor = Color.FromArgb(250, 204, 21);
                btn.ForeColor = Color.FromArgb(15, 23, 42);
                btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                btn.FlatAppearance.BorderColor = Color.FromArgb(234, 179, 8);
            }
            else if (!isEnabled)
            {
                btn.BackColor = Color.FromArgb(248, 250, 252);
                btn.ForeColor = Color.FromArgb(203, 213, 225);
                btn.Font = new Font("Segoe UI", 9.5F);
                btn.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            }
            else
            {
                btn.BackColor = Color.White;
                btn.ForeColor = Color.FromArgb(51, 65, 85);
                btn.Font = new Font("Segoe UI", 9.5F);
                btn.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            }

            if (onClick is not null)
            {
                btn.Click += onClick;
            }

            return btn;
        }

        private void SetupStepsGridColumns()
        {
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
            using var font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            using var brush = new SolidBrush(Color.FromArgb(30, 41, 59));
            using var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            e.Graphics.DrawString(text, font, brush, new RectangleF(x, y, size, size), format);
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
                using var path = GetRoundedPath(badgeRect, 10);
                e.Graphics.FillPath(brush, path);

                TextRenderer.DrawText(e.Graphics, val, new Font("Segoe UI", 8.5F, FontStyle.Bold), badgeRect, fgCol, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
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
}