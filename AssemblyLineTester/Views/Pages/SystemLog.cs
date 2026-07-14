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

        public SystemLog()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(248, 250, 252);
            panelFilters.Paint += PanelFilters_Paint;
            pnlLeft.Paint += PanelFilters_Paint;
            pnlSummary.Paint += PanelFilters_Paint;
            if (pnlChartContainer is not null)
            {
                pnlChartContainer.Paint += PnlChartContainer_Paint;
            }
            pnlLevelStats.Paint += PnlLevelStats_Paint;
            pnlModuleStats.Paint += PnlModuleStats_Paint;
            pnlLogDetail.Paint += PanelFilters_Paint;

            gridData.CellPainting += gridData_CellPainting;

            InitializeGridColumns();

            btnSearch.Click += (s, ev) => { _currentPage = 1; LoadLogData(); };
            btnReset.Click += (s, ev) => { ResetFilters(); _currentPage = 1; LoadLogData(); };
            btnRefresh.Click += (s, ev) => LoadLogData();

//            btnRefresh.Click += (s, ev) => { LoadDummyLogData(); };

//            if (cmbLogLevel.Items.Count > 0) cmbLogLevel.SelectedIndex = 0;
//            if (cmbModule.Items.Count > 0) cmbModule.SelectedIndex = 0;
//            if (cmbUser.Items.Count > 0) cmbUser.SelectedIndex = 0;
//            if (cmbAction.Items.Count > 0) cmbAction.SelectedIndex = 0;

//            btnSearch.Click += (s, ev) => { _currentPage = 1; PopulateLogGrid(); };
//            btnReset.Click += (s, ev) =>
//            {
//                if (cmbLogLevel.Items.Count > 0) cmbLogLevel.SelectedIndex = 0;
//                if (cmbModule.Items.Count > 0) cmbModule.SelectedIndex = 0;
//                if (cmbUser.Items.Count > 0) cmbUser.SelectedIndex = 0;
//                if (cmbAction.Items.Count > 0) cmbAction.SelectedIndex = 0;
//                txtKeyword.Text = "";
//                _currentPage = 1;
//                PopulateLogGrid();
//            };

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                _logService.SeedSampleDataIfEmpty();
                ResetFilters();
                LoadLogData();
            }
        }

        // RESETS FILTER CONTROLS TO DEFAULT (LAST 30 DAYS, NO FILTER).
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

        // BUILDS A SystemLogFilter FROM THE CURRENT STATE OF THE FILTER CONTROLS.
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

        // LOADS REAL LOG DATA AND SUMMARY FROM SystemLogService, THEN REFRESHES ALL VISUALS.
        public void LoadLogData()
        {
            var filter = BuildFilterFromControls();
            var paged = _logService.GetLogs(filter);

            _totalCount = paged.TotalCount;
            _totalPages = Math.Max(1, paged.TotalPages);

            // clamp if filter shrank the result set below the current page
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
                    string.IsNullOrEmpty(log.IPAddress) ? "-" : $"{log.IPAddress}   \u22ee");
            }

            UpdateLegendLabels();
            UpdatePaginationUI(_totalCount, _totalPages);
            pnlChartContainer?.Invalidate();
            pnlLevelStats.Invalidate();
            pnlModuleStats.Invalidate();
        }

        private void UpdateLegendLabels()
        {
            if (lblLegInfo is not null) lblLegInfo.Text = $"INFO : {_summary.InfoCount:N0} ({FormatPct(_summary.InfoCount, _summary.Total)})";
            if (lblLegWarn is not null) lblLegWarn.Text = $"WARNING : {_summary.WarningCount:N0} ({FormatPct(_summary.WarningCount, _summary.Total)})";
            if (lblLegError is not null) lblLegError.Text = $"ERROR : {_summary.ErrorCount:N0} ({FormatPct(_summary.ErrorCount, _summary.Total)})";
        }

        private static string FormatPct(int count, int total) => total == 0 ? "0.0%" : $"{count * 100.0 / total:0.0}%";

        private static float Ratio(int count, int total) => total == 0 ? 0f : (float)count / total;

        // ONE-TIME GRID COLUMN/STYLE SETUP. ROW DATA IS POPULATED SEPARATELY IN LoadLogData().
        private void InitializeGridColumns()
        {
            gridData.Columns.Clear();

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

            gridData.Columns.Add("No", "No.");
            gridData.Columns.Add("Date", "Date & Time ↓");
            gridData.Columns.Add("Level", "Log Level");
            gridData.Columns.Add("Module", "Module");
            gridData.Columns.Add("User", "User");
            gridData.Columns.Add("Action", "Action");
            gridData.Columns.Add("Description", "Description");
            gridData.Columns.Add("IP", "IP Address");

            SetColumnWidth(gridData, "No", 45, DataGridViewContentAlignment.MiddleCenter);
            SetColumnWidth(gridData, "Date", 165);
            SetColumnWidth(gridData, "Level", 100, DataGridViewContentAlignment.MiddleCenter);
            SetColumnWidth(gridData, "Module", 135);
            SetColumnWidth(gridData, "User", 85);
            SetColumnWidth(gridData, "Action", 120);
            SetColumnWidth(gridData, "Description", 260);
            SetColumnWidth(gridData, "IP", 130);

//            var filteredLogs = _allLogs.Where(l =>
//            {
//                if (cmbLogLevel.SelectedItem is string level && level != "All" && !string.Equals(l[2], level, StringComparison.OrdinalIgnoreCase)) return false;
//                if (cmbModule.SelectedItem is string module && module != "All" && !string.Equals(l[3], module, StringComparison.OrdinalIgnoreCase)) return false;
//                if (cmbUser.SelectedItem is string user && user != "All" && !string.Equals(l[4], user, StringComparison.OrdinalIgnoreCase)) return false;
//                if (cmbAction.SelectedItem is string action && action != "All" && !string.Equals(l[5], action, StringComparison.OrdinalIgnoreCase)) return false;
//                if (!string.IsNullOrWhiteSpace(txtKeyword.Text) && !l.Any(col => col.Contains(txtKeyword.Text, StringComparison.OrdinalIgnoreCase))) return false;
//                return true;
//            }).ToList();

//            int totalLogs = filteredLogs.Count;
//            int totalPages = (int)Math.Ceiling(totalLogs / (double)_pageSize);
//            if (totalPages == 0) totalPages = 1;
//            if (_currentPage > totalPages) _currentPage = totalPages;
//            if (_currentPage < 1) _currentPage = 1;

//            var pageLogs = filteredLogs.Skip((_currentPage - 1) * _pageSize).Take(_pageSize);
//            foreach (var log in pageLogs)
//            {
//                gridData.Rows.Add(log);
//            }

//            UpdatePaginationUI(totalLogs, totalPages);

        }

        // BUILDS THE "Showing X to Y of Z" TEXT AND THE DYNAMIC PAGE-NUMBER BUTTONS.
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
            flpPages.Controls.Clear();

            var btnFirst = CreatePageButton("«", false, _currentPage > 1, (s, e) => { _currentPage = 1; LoadLogData(); });
            flpPages.Controls.Add(btnFirst);

            var btnPrev = CreatePageButton("‹", false, _currentPage > 1, (s, e) => { if (_currentPage > 1) { _currentPage--; LoadLogData(); } });
            flpPages.Controls.Add(btnPrev);

            for (int p = 1; p <= totalPages; p++)
            {
                if (p == 1 || p == totalPages || (p >= _currentPage - 1 && p <= _currentPage + 1))
                {
                    int pageNum = p;
                    var btnPage = CreatePageButton(pageNum.ToString(), pageNum == _currentPage, true, (s, e) => { _currentPage = pageNum; LoadLogData(); });
                    flpPages.Controls.Add(btnPage);
                }
                else if (p == _currentPage - 2 || p == _currentPage + 2)
                {
                    var btnDots = CreatePageButton("...", false, false, null);
                    flpPages.Controls.Add(btnDots);
                }
            }

            var btnNext = CreatePageButton("›", false, _currentPage < totalPages, (s, e) => { if (_currentPage < totalPages) { _currentPage++; LoadLogData(); } });
            flpPages.Controls.Add(btnNext);

            var btnLast = CreatePageButton("»", false, _currentPage < totalPages, (s, e) => { _currentPage = totalPages; LoadLogData(); });
            flpPages.Controls.Add(btnLast);
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
                using var path = GetRoundedRectPath(badgeRect, 10);
                e.Graphics.FillPath(brush, path);

                TextRenderer.DrawText(e.Graphics, val, new Font("Segoe UI", 8.5F, FontStyle.Bold), badgeRect, fgCol, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                e.Handled = true;
            }
        }

        private void PanelFilters_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Control card) return;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, card.Width - 1, card.Height - 1);
            using var path = GetRoundedRectPath(rect, 8);
            using var pen = new Pen(Color.FromArgb(226, 232, 240), 1);
            e.Graphics.DrawPath(pen, path);
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
            using var font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            using var brush = new SolidBrush(Color.FromArgb(30, 41, 59));
            using var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            e.Graphics.DrawString(text, font, brush, new RectangleF(x, y, size, size), format);
        }

        private void PnlLevelStats_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Control card) return;
            PanelFilters_Paint(sender, e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int startY = 75;
            int rowStep = 65;
            int leftMargin = 36;
            int w = card.Width - (leftMargin * 2);
            if (w <= 0) return;

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
            using var fontLabel = new Font("Segoe UI", 9F, FontStyle.Bold);
            using var fontVal = new Font("Segoe UI", 9F);
            using var textBrush = new SolidBrush(Color.FromArgb(30, 41, 59));
            using var valBrush = new SolidBrush(Color.FromArgb(100, 116, 139));
            using var trackBrush = new SolidBrush(Color.FromArgb(241, 245, 249));
            using var barBrush = new SolidBrush(barColor);
            g.DrawString(label, fontLabel, textBrush, x, y);
            var valSize = g.MeasureString(val, fontVal);
            g.DrawString(val, fontVal, valBrush, x + width - (int)valSize.Width, y);

            var labelSize = g.MeasureString(label, fontLabel);
            int barTop = y + (int)labelSize.Height + 4;
            int barHeight = 6;

            Rectangle trackRect = new(x, barTop, width, barHeight);
            using var trackPath = GetRoundedRectPath(trackRect, 3);
            g.FillPath(trackBrush, trackPath);

            int barWidth = Math.Max(8, (int)(width * pct));
            Rectangle barRect = new(x, barTop, barWidth, barHeight);
            using var barPath = GetRoundedRectPath(barRect, 3);
            g.FillPath(barBrush, barPath);
        }

        private void PnlModuleStats_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Control card) return;
            PanelFilters_Paint(sender, e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int startY = 65;
            int leftMargin = 36;
            int w = card.Width - (leftMargin * 2);
            if (w <= 0) return;

            var topModules = _summary.CountByModule
                .OrderByDescending(m => m.Value)
                .Take(6)
                .ToList();
            if (topModules.Count == 0) return;

            int availableHeight = card.Height - startY - 20;
            int rowStep = Math.Max(38, availableHeight / topModules.Count);

            for (int i = 0; i < topModules.Count; i++)
            {
                var (module, count) = (topModules[i].Key, topModules[i].Value);
                DrawStatRow(e.Graphics, leftMargin, startY + (i * rowStep), w, module,
                    $"{count:N0} ({FormatPct(count, _summary.Total)})",
                    Ratio(count, _summary.Total), Color.FromArgb(99, 102, 241));
            }
        }

        private static GraphicsPath GetRoundedRectPath(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            var size = new Size(diameter, diameter);
            var arc = new Rectangle(bounds.Location, size);
            var path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            path.AddArc(arc, 180, 90);
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}