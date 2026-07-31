using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AssemblyLineTester.Models.Dto;
using AssemblyLineTester.Services;
using static AssemblyLineTester.Models.SystemLog;
using AssemblyLineTester.Helpers;

namespace AssemblyLineTester.Views.Pages
{
    public partial class SystemLog : UserControl
    {
        private readonly SystemLogService _logService = new();
        private SystemLogSummary _summary = new();
        private int _currentPage = 1;
        private readonly int _pageSize = 10;
        private int _totalPages = 1;
        private int _totalCount = 0;

        // AutoScroll never moves what a Paint handler drew, so the rows live in this child panel.
        private readonly Panel _moduleScroll = new() { AutoScroll = true, BackColor = Color.White, Dock = DockStyle.Fill };

        public SystemLog()
        {
            InitializeComponent();
            SetupEvents();
            if (pnlChartContainer is not null)
            {
                pnlChartContainer.Paint += PnlChartContainer_Paint;
            }
            pnlLevelStats.Paint += PnlLevelStats_Paint;

            // Docking applies last-control-first, so the Fill panel must sit at the front.
            lblModuleStatsTitle.Dock = DockStyle.Top;
            lblModuleStatsTitle.Padding = new Padding(32, 28, 0, 12);
            pnlModuleStats.Controls.Add(_moduleScroll);
            _moduleScroll.BringToFront();
            _moduleScroll.Paint += PnlModuleStats_Paint;

            // A Panel does not repaint on resize, so the rows would keep their first geometry.
            pnlLevelStats.Resize += (s, ev) => pnlLevelStats.Invalidate();
            _moduleScroll.Resize += (s, ev) => _moduleScroll.Invalidate();

            gridData.CellPainting += gridData_CellPainting;
            InitializeGridColumns();

            btnSearch.Click += (s, ev) => { _currentPage = 1; LoadLogData(); };
            btnReset.Click += (s, ev) => { ResetFilters(); _currentPage = 1; LoadLogData(); };
            btnRefresh.Click += (s, ev) => LoadLogData();
            btnExport.Click += BtnExport_Click;

            DoubleBuffered = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                AssemblyLineTester.Helpers.UIStyleHelper.ScaleExplicitFonts(this, AssemblyLineTester.Helpers.UIStyleHelper.GetHostScaleFactor(this));
                ResetFilters();
                LoadLogData();
            }
        }

        // Two exports: the filtered log as a spreadsheet, or a diagnostics bundle to send.
        private void BtnExport_Click(object? sender, EventArgs e)
        {
            if (!SessionManager.HasPermission("Action.SystemLog.Clear"))
            {
                MessageBox.Show("You do not have permission to export system logs.", "Access Denied",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var choice = MessageBox.Show(
                "Export the log entries currently listed to an Excel file?\r\n\r\n" +
                "Choose No to build a diagnostics bundle instead - a zip containing the technical " +
                "logs to send to support when something has gone wrong.",
                "Export",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (choice == DialogResult.Yes) ExportFilteredExcel();
            else if (choice == DialogResult.No) ExportDiagnosticsBundle();
        }

        private void ExportFilteredExcel()
        {
            using var dialog = new SaveFileDialog
            {
                Filter = "Excel workbook (*.xlsx)|*.xlsx",
                FileName = $"system-log-{DateTime.Now:yyyyMMdd-HHmmss}.xlsx"
            };

            if (dialog.ShowDialog() != DialogResult.OK) return;

            Cursor previous = Cursor;
            Cursor = Cursors.WaitCursor;
            try
            {
                int exported = _logService.ExportToExcel(BuildFilterFromControls(), dialog.FileName);

                new SystemLogService().Log(LogSeverity.Info, "System Log", "Export Log",
                    $"{exported} log entries exported to Excel.");

                MessageBox.Show($"{exported} log entries exported.", "Export complete",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                FileLogService.Error("SystemLog.ExportFilteredExcel", "Excel export failed.", ex);
                MessageBox.Show($"Export failed: {ex.Message}", "Export", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = previous;
            }
        }

        private void ExportDiagnosticsBundle()
        {
            using var dialog = new SaveFileDialog
            {
                Filter = "Zip archive (*.zip)|*.zip",
                FileName = DiagnosticsService.SuggestedFileName()
            };

            if (dialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                int files = DiagnosticsService.CreateBundle(dialog.FileName);

                new SystemLogService().Log(LogSeverity.Info, "Application", "Export Diagnostics",
                    $"Diagnostics bundle created with {files} log files.");

                MessageBox.Show(
                    $"Diagnostics bundle saved.\r\n\r\n{files} log file(s) plus a machine summary.\r\n" +
                    "Send this file to support.",
                    "Diagnostics bundle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                FileLogService.Error("SystemLog.ExportDiagnosticsBundle", "Bundle creation failed.", ex);
                MessageBox.Show($"Could not build the bundle: {ex.Message}", "Diagnostics bundle",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Puts the filter controls back to the last 30 days with nothing else set.
        private void ResetFilters()
        {
            dtpEnd.Value = DateTime.Now;
            dtpStart.Value = DateTime.Now.AddDays(-30);
            cmbLogLevel.Text = "All";
            cmbModule.Text = "All";
            cmbUser.Text = "All";
            cmbAction.Text = "All";
            txtKeyword.Text = string.Empty;
        }

        // Reads the filter controls into a SystemLogFilter.
        private SystemLogFilter BuildFilterFromControls()
        {
            var filter = new SystemLogFilter
            {
                DateFrom = dtpStart.Value.Date,
                DateTo = dtpEnd.Value.Date,
                Page = _currentPage,
                PageSize = _pageSize
            };

            if (!string.Equals(cmbLogLevel.Text, "All", StringComparison.OrdinalIgnoreCase)
                && Enum.TryParse<LogSeverity>(cmbLogLevel.Text, true, out var level))
            {
                filter.SeverityLevel = level;
            }

            if (!string.Equals(cmbModule.Text, "All", StringComparison.OrdinalIgnoreCase))
                filter.ModuleName = cmbModule.Text;

            if (!string.Equals(cmbUser.Text, "All", StringComparison.OrdinalIgnoreCase))
                filter.UserName = cmbUser.Text;

            if (!string.Equals(cmbAction.Text, "All", StringComparison.OrdinalIgnoreCase))
                filter.ActionName = cmbAction.Text;

            if (!string.IsNullOrWhiteSpace(txtKeyword.Text))
                filter.Keyword = txtKeyword.Text.Trim();

            return filter;
        }

        // Reloads the page of entries and the summary, then repaints everything.
        public void LoadLogData()
        {
            SuspendLayout();
            try
            {
                var filter = BuildFilterFromControls();
                var paged = _logService.GetLogs(filter);

                _totalCount = paged.TotalCount;
                _totalPages = Math.Max(1, paged.TotalPages);

                // The filter may have shrunk the result below the current page.
                if (_currentPage > _totalPages)
                {
                    _currentPage = _totalPages;
                    filter.Page = _currentPage;
                    paged = _logService.GetLogs(filter);
                }

                _summary = _logService.GetSummary(filter);

                gridData.Rows.Clear();
                int no = (paged.Page - 1) * paged.PageSize + 1;
                foreach (var log in paged.Items)
                {
                    gridData.Rows.Add(
                        no++,
                        log.Timestamp.ToLocalTime().ToString("dd MMM yyyy hh:mm:ss tt"),
                        log.SeverityLevel.ToString().ToUpperInvariant(),
                        log.ModuleName,
                        string.IsNullOrEmpty(log.UserName) ? "-" : log.UserName,
                        log.ActionName,
                        log.Message,
                        string.IsNullOrEmpty(log.MachineName) ? "-" : log.MachineName);
                }

                lblTableTitle.Text = $"LOG LIST (Total: {_totalCount:N0})";

                UpdateLegendLabels();
                UpdatePaginationUI(_totalCount, _totalPages);
                UpdateLogDetail();
            }
            finally
            {
                ResumeLayout(true);
                pnlChartContainer?.Invalidate();
                pnlLevelStats.Invalidate();
                _moduleScroll.Invalidate();
            }
        }

        // Mirrors the selected row; the grid already carries every field this panel shows.
        private void UpdateLogDetail()
        {
            var row = gridData.CurrentRow;

            if (row is null || row.Index < 0 || gridData.Rows.Count == 0)
            {
                lblLogDetailContent.Text = _totalCount == 0
                    ? "No entries match the current filter."
                    : "Select a row in the list above to see its detail.";
                return;
            }

            var cells = row.Cells;
            lblLogDetailContent.Text =
                $"Date & Time: {cells["Date"].Value}   |   Log Level: {cells["Level"].Value}   |   " +
                $"Module: {cells["Module"].Value}   |   User: {cells["User"].Value}   |   " +
                $"Machine: {cells["Machine"].Value}   |   Action: {cells["Action"].Value}   |   " +
                $"Description: {cells["Description"].Value}";
        }

        private void UpdateLegendLabels()
        {
            if (lblLegInfo is not null) lblLegInfo.Text = $"INFO : {_summary.InfoCount:N0} ({FormatPct(_summary.InfoCount, _summary.Total)})";
            if (lblLegWarn is not null) lblLegWarn.Text = $"WARNING : {_summary.WarningCount:N0} ({FormatPct(_summary.WarningCount, _summary.Total)})";
            if (lblLegError is not null) lblLegError.Text = $"ERROR : {_summary.ErrorCount:N0} ({FormatPct(_summary.ErrorCount, _summary.Total)})";
        }

        private static string FormatPct(int count, int total) => total == 0 ? "0.0%" : $"{count * 100.0 / total:0.0}%";

        private static float Ratio(int count, int total) => total == 0 ? 0f : (float)count / total;

        // Columns and styling only; rows come from LoadLogData.
        private void InitializeGridColumns()
        {
            gridData.Columns.Clear();

            AssemblyLineTester.Helpers.UIStyleHelper.StyleDataGridView(gridData);
            gridData.ColumnHeadersHeight = 44;
            gridData.RowTemplate.Height = 40;

            gridData.Columns.Add("No", "No.");
            gridData.Columns.Add("Date", "Date & Time ↓");
            gridData.Columns.Add("Level", "Log Level");
            gridData.Columns.Add("Module", "Module");
            gridData.Columns.Add("User", "User");
            gridData.Columns.Add("Action", "Action");
            gridData.Columns.Add("Description", "Description");
            gridData.Columns.Add("Machine", "Machine");

            SetColumnWidth(gridData, "No", 45, DataGridViewContentAlignment.MiddleCenter);
            SetColumnWidth(gridData, "Date", 165);
            SetColumnWidth(gridData, "Level", 100, DataGridViewContentAlignment.MiddleCenter);
            SetColumnWidth(gridData, "Module", 135);
            SetColumnWidth(gridData, "User", 85);
            SetColumnWidth(gridData, "Action", 120);
            SetColumnWidth(gridData, "Description", 260);
            SetColumnWidth(gridData, "Machine", 130);


        }

        // Builds the "Showing X to Y of Z" line and the page buttons.
        private void UpdatePaginationUI(int totalFiltered, int totalPages)
        {
            if (totalFiltered == 0)
            {
                lblPaginationInfo.Text = "Showing 0 to 0 of 0 entries";
            }
            else
            {
                int start = (_currentPage - 1) * _pageSize + 1;
                int end = Math.Min(_currentPage * _pageSize, totalFiltered);
                lblPaginationInfo.Text = $"Showing {start} to {end} of {totalFiltered} entries";
            }

            if (flpPages is null) return;
            UIStyleHelper.ClearAndDispose(flpPages.Controls);

            float scaleX = this.CurrentAutoScaleDimensions.Width / 13f;
            float scaleY = this.CurrentAutoScaleDimensions.Height / 32f;

            var btnFirst = CreatePageButton("«", false, _currentPage > 1, (s, e) => { _currentPage = 1; LoadLogData(); }, scaleX, scaleY);
            flpPages.Controls.Add(btnFirst);

            var btnPrev = CreatePageButton("‹", false, _currentPage > 1, (s, e) => { if (_currentPage > 1) { _currentPage--; LoadLogData(); } }, scaleX, scaleY);
            flpPages.Controls.Add(btnPrev);

            for (int p = 1; p <= totalPages; p++)
            {
                if (p == 1 || p == totalPages || (p >= _currentPage - 1 && p <= _currentPage + 1))
                {
                    int pageNum = p;
                    var btnPage = CreatePageButton(pageNum.ToString(), pageNum == _currentPage, true, (s, e) => { _currentPage = pageNum; LoadLogData(); }, scaleX, scaleY);
                    flpPages.Controls.Add(btnPage);
                }
                else if (p == _currentPage - 2 || p == _currentPage + 2)
                {
                    var btnDots = CreatePageButton("...", false, false, null, scaleX, scaleY);
                    flpPages.Controls.Add(btnDots);
                }
            }

            var btnNext = CreatePageButton("›", false, _currentPage < totalPages, (s, e) => { if (_currentPage < totalPages) { _currentPage++; LoadLogData(); } }, scaleX, scaleY);
            flpPages.Controls.Add(btnNext);

            var btnLast = CreatePageButton("»", false, _currentPage < totalPages, (s, e) => { _currentPage = totalPages; LoadLogData(); }, scaleX, scaleY);
            flpPages.Controls.Add(btnLast);
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

        private static void SetColumnWidth(DataGridView grid, string colName, float fillWeight, DataGridViewContentAlignment alignment = DataGridViewContentAlignment.MiddleLeft)
        {
            var col = grid.Columns[colName];
            if (col is not null)
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

                Rectangle badgeRect = new(e.CellBounds.X + 12, e.CellBounds.Y + 6, e.CellBounds.Width - 24, e.CellBounds.Height - 12);
                using var brush = new SolidBrush(bgCol);
                using var path = AssemblyLineTester.Helpers.UIStyleHelper.GetRoundedPath(badgeRect, 10);
                e.Graphics.FillPath(brush, path);

                TextRenderer.DrawText(e.Graphics, val, AssemblyLineTester.Helpers.UIStyleHelper.Font85Bold, badgeRect, fgCol, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                e.Handled = true;
            }
        }
        private void SetupEvents()
        {
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(panelFilters, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(pnlLeft, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(pnlSummary, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(pnlLevelStats, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(pnlModuleStats, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(pnlLogDetail, 12, Color.FromArgb(34, 40, 49), 2.5f);

            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnSearch, 10, Color.FromArgb(250, 204, 21), Color.FromArgb(254, 216, 53), Color.FromArgb(234, 186, 10), Color.Transparent);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnReset, 10, Color.FromArgb(241, 245, 249), Color.FromArgb(226, 232, 240), Color.FromArgb(203, 213, 225), Color.Transparent);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnExport, 10, Color.FromArgb(241, 245, 249), Color.FromArgb(248, 250, 252), Color.FromArgb(241, 245, 249), Color.Transparent);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnRefresh, 10, Color.FromArgb(241, 245, 249), Color.FromArgb(248, 250, 252), Color.FromArgb(241, 245, 249), Color.Transparent);

            gridData.SelectionChanged += (s, e) => UpdateLogDetail();

            // A long Description would otherwise run off the panel: cap the width so the label wraps.
            pnlLogDetail.Resize += (s, e) => CapLogDetailWidth();
            CapLogDetailWidth();
        }

        private void CapLogDetailWidth()
        {
            int available = pnlLogDetail.ClientSize.Width - lblLogDetailContent.Left - 20;
            lblLogDetailContent.MaximumSize = new Size(Math.Max(200, available), 0);
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

            if (_summary.Total == 0)
            {
                using var penEmpty = new Pen(Color.FromArgb(226, 232, 240), 18);
                e.Graphics.DrawArc(penEmpty, rect, -90, 359.9f);
            }
            else
            {
                float infoSweep = 360f * _summary.InfoCount / _summary.Total;
                float warnSweep = 360f * _summary.WarningCount / _summary.Total;
                float errSweep = 360f * _summary.ErrorCount / _summary.Total;
                float start = -90f;

                using var penInfo = new Pen(Color.FromArgb(59, 130, 246), 18);
                using var penWarn = new Pen(Color.FromArgb(245, 158, 11), 18);
                using var penErr = new Pen(Color.FromArgb(239, 68, 68), 18);

                if (infoSweep > 0) e.Graphics.DrawArc(penInfo, rect, start, infoSweep);
                start += infoSweep;
                if (warnSweep > 0) e.Graphics.DrawArc(penWarn, rect, start, warnSweep);
                start += warnSweep;
                if (errSweep > 0) e.Graphics.DrawArc(penErr, rect, start, errSweep);
            }

            string text = $"{_summary.Total:N0}\nTOTAL";
            using var brush = new SolidBrush(Color.FromArgb(30, 41, 59));
            using var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            e.Graphics.DrawString(text, AssemblyLineTester.Helpers.UIStyleHelper.Font95Bold, brush, new RectangleF(x, y, size, size), format);
        }

        private void PnlLevelStats_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Control card) return;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Measured from the title and the card height, never fixed pixels.
            int startY = lblLevelStatsTitle.Bottom + 6;
            int leftMargin = 36;
            int w = card.Width - (leftMargin * 2);
            if (w <= 0) return;

            int rowStep = Math.Max(38, (card.Height - startY - 20) / 3);

            DrawStatRow(e.Graphics, leftMargin, startY, w, "INFO",
                $"{_summary.InfoCount:N0} ({FormatPct(_summary.InfoCount, _summary.Total)})",
                Ratio(_summary.InfoCount, _summary.Total), Color.FromArgb(59, 130, 246));
            DrawStatRow(e.Graphics, leftMargin, startY + rowStep, w, "WARNING",
                $"{_summary.WarningCount:N0} ({FormatPct(_summary.WarningCount, _summary.Total)})",
                Ratio(_summary.WarningCount, _summary.Total), Color.FromArgb(245, 158, 11));
            DrawStatRow(e.Graphics, leftMargin, startY + (rowStep * 2), w, "ERROR",
                $"{_summary.ErrorCount:N0} ({FormatPct(_summary.ErrorCount, _summary.Total)})",
                Ratio(_summary.ErrorCount, _summary.Total), Color.FromArgb(239, 68, 68));
        }

        private void DrawStatRow(Graphics g, int x, int y, int width, string label, string val, float pct, Color barColor)
        {
            using var textBrush = new SolidBrush(Color.FromArgb(30, 41, 59));
            using var valBrush = new SolidBrush(Color.FromArgb(100, 116, 139));
            using var trackBrush = new SolidBrush(Color.FromArgb(241, 245, 249));
            using var barBrush = new SolidBrush(barColor);
            g.DrawString(label, AssemblyLineTester.Helpers.UIStyleHelper.Font9Bold, textBrush, x, y);
            var valSize = g.MeasureString(val, AssemblyLineTester.Helpers.UIStyleHelper.Font9Regular);
            g.DrawString(val, AssemblyLineTester.Helpers.UIStyleHelper.Font9Regular, valBrush, x + width - (int)valSize.Width, y);

            var labelSize = g.MeasureString(label, AssemblyLineTester.Helpers.UIStyleHelper.Font9Bold);
            int barTop = y + (int)labelSize.Height + 4;
            int barHeight = 6;

            Rectangle trackRect = new(x, barTop, width, barHeight);
            using var trackPath = AssemblyLineTester.Helpers.UIStyleHelper.GetRoundedPath(trackRect, 3);
            g.FillPath(trackBrush, trackPath);

            int barWidth = Math.Max(8, (int)(width * pct));
            Rectangle barRect = new(x, barTop, barWidth, barHeight);
            using var barPath = AssemblyLineTester.Helpers.UIStyleHelper.GetRoundedPath(barRect, 3);
            g.FillPath(barBrush, barPath);
        }

        private void PnlModuleStats_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Panel card) return;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            var modules = _summary.CountByModule
                .OrderByDescending(m => m.Value)
                .ToList();
            if (modules.Count == 0)
            {
                card.AutoScrollMinSize = Size.Empty;
                return;
            }

            // Row pitch comes from the font, so a long module list scrolls instead of squeezing.
            int rowStep = AssemblyLineTester.Helpers.UIStyleHelper.Font9Bold.Height + 26;
            card.AutoScrollMinSize = new Size(0, modules.Count * rowStep);

            // Without this the rows stay put while the scrollbar moves.
            e.Graphics.TranslateTransform(card.AutoScrollPosition.X, card.AutoScrollPosition.Y);

            int leftMargin = 36;
            int w = card.ClientSize.Width - (leftMargin * 2) - SystemInformation.VerticalScrollBarWidth;
            if (w <= 0) return;

            for (int i = 0; i < modules.Count; i++)
            {
                var (module, count) = (modules[i].Key, modules[i].Value);
                DrawStatRow(e.Graphics, leftMargin, i * rowStep, w, module,
                    $"{count:N0} ({FormatPct(count, _summary.Total)})",
                    Ratio(count, _summary.Total), Color.FromArgb(99, 102, 241));
            }
        }
    }
}