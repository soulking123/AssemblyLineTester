using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace AssemblyLineTester.Views.Pages
{
    public partial class TestHistory : UserControl
    {
        private int _currentPage = 1;
        private int _pageSize = 10;
        private readonly List<string[]> _allHistoryRecords = [];

        public TestHistory()
        {
            InitializeComponent();
            SetupEvents();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                LoadDummyData();
                LoadDummyStepsData();
                InitSummaryChart();
                if (cmbPageSize.Items.Count > 0) cmbPageSize.SelectedIndex = 0;
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

        private void SetupEvents()
        {
            SetupCardRoundedEdges(pnlFilterCard, 12, Color.FromArgb(34, 40, 49), 2.5f);
            SetupCardRoundedEdges(pnlLeft, 12, Color.FromArgb(34, 40, 49), 2.5f);
            SetupCardRoundedEdges(pnlDetail, 12, Color.FromArgb(34, 40, 49), 2.5f);
            SetupCardRoundedEdges(pnlSummary, 12, Color.FromArgb(34, 40, 49), 2.5f);
            SetupCardRoundedEdges(pnlSteps, 12, Color.FromArgb(34, 40, 49), 2.5f);

            SetupButtonRoundedEdges(btnSearch, 10, Color.FromArgb(250, 204, 21), Color.FromArgb(254, 216, 53), Color.FromArgb(234, 186, 10), Color.Transparent);
            SetupButtonRoundedEdges(btnReset, 10, Color.FromArgb(241, 245, 249), Color.FromArgb(226, 232, 240), Color.FromArgb(203, 213, 225), Color.Transparent);
            SetupButtonRoundedEdges(btnRefresh, 10, Color.White, Color.FromArgb(248, 250, 252), Color.FromArgb(241, 245, 249), Color.Transparent);
            SetupButtonRoundedEdges(btnExport, 10, Color.White, Color.FromArgb(248, 250, 252), Color.FromArgb(241, 245, 249), Color.Transparent);
            SetupButtonRoundedEdges(btnViewReport, 10, Color.FromArgb(250, 204, 21), Color.FromArgb(254, 216, 53), Color.FromArgb(234, 186, 10), Color.Transparent);

            SetupBadgeRoundedEdges(lblDetailBadge, 10);

            gridData.CellFormatting += gridData_CellFormatting;
            gridData.CellPainting += gridData_CellPainting;
            gridSteps.CellFormatting += gridSteps_CellFormatting;
            btnRefresh.Click += (s, ev) => { LoadDummyData(); LoadDummyStepsData(); };

            cmbPageSize.SelectedIndexChanged += (s, ev) =>
            {
                if (int.TryParse(cmbPageSize.SelectedItem?.ToString(), out int newSize) && newSize > 0)
                {
                    _pageSize = newSize;
                    _currentPage = 1;
                    PopulateHistoryGrid();
                }
            };

            txtGoToPage.KeyDown += (s, ev) =>
            {
                if (ev.KeyCode == Keys.Enter)
                {
                    ev.SuppressKeyPress = true;
                    if (int.TryParse(txtGoToPage.Text, out int pageNum))
                    {
                        int totalPages = (int)Math.Ceiling(_allHistoryRecords.Count / (double)_pageSize);
                        if (pageNum >= 1 && pageNum <= totalPages)
                        {
                            _currentPage = pageNum;
                            PopulateHistoryGrid();
                        }
                    }
                }
            };
        }

        private static void SetupCardRoundedEdges(Control card, int radius, Color borderColor, float borderWidth = 2.5f)
        {
            if (card is TableLayoutPanel || card.Padding.Left < 4 || card.Padding.Top < 4 || card.Padding.Right < 4 || card.Padding.Bottom < 4)
            {
                card.Padding = new Padding(
                    Math.Max(card.Padding.Left, 4),
                    Math.Max(card.Padding.Top, 4),
                    Math.Max(card.Padding.Right, 4),
                    Math.Max(card.Padding.Bottom, 4)
                );
            }

            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                Color parentBg = card.Parent?.BackColor != null && card.Parent.BackColor != Color.Transparent
                    ? card.Parent.BackColor
                    : Color.FromArgb(244, 245, 247);

                using (var parentBrush = new SolidBrush(parentBg))
                {
                    e.Graphics.FillRectangle(parentBrush, 0, 0, card.Width, card.Height);
                }

                var rectOuter = new Rectangle(0, 0, card.Width, card.Height);
                using var pathOuter = GetRoundedPath(rectOuter, radius);
                using (var cardBrush = new SolidBrush(card.BackColor))
                {
                    e.Graphics.FillPath(cardBrush, pathOuter);
                }

                var rectBorder = new Rectangle(1, 1, card.Width - 3, card.Height - 3);
                using var pathBorder = GetRoundedPath(rectBorder, radius);
                using var pen = new Pen(borderColor, borderWidth);
                e.Graphics.DrawPath(pen, pathBorder);
            };

            card.Resize += (s, e) =>
            {
                if (card.Width > 0 && card.Height > 0)
                {
                    var oldRegion = card.Region;
                    card.Region = new Region(GetRoundedPath(new Rectangle(0, 0, card.Width, card.Height), radius));
                    oldRegion?.Dispose();
                    card.Invalidate();
                }
            };

            if (card.Width > 0 && card.Height > 0)
            {
                card.Region = new Region(GetRoundedPath(new Rectangle(0, 0, card.Width, card.Height), radius));
            }
        }

        private static void SetupButtonRoundedEdges(Button btn, int radius, Color normalColor, Color hoverColor, Color pressedColor, Color borderColor)
        {
            bool isHovered = false;
            bool isPressed = false;

            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = normalColor;

            btn.MouseEnter += (s, e) => { isHovered = true; btn.Invalidate(); };
            btn.MouseLeave += (s, e) => { isHovered = false; isPressed = false; btn.Invalidate(); };
            btn.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) { isPressed = true; btn.Invalidate(); } };
            btn.MouseUp += (s, e) => { if (e.Button == MouseButtons.Left) { isPressed = false; btn.Invalidate(); } };

            btn.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                Color parentBg = btn.Parent?.BackColor != null && btn.Parent.BackColor != Color.Transparent
                    ? btn.Parent.BackColor
                    : Color.White;

                using (var parentBrush = new SolidBrush(parentBg))
                {
                    e.Graphics.FillRectangle(parentBrush, 0, 0, btn.Width, btn.Height);
                }

                Color currentColor = !btn.Enabled ? Color.FromArgb(248, 250, 252) : (isPressed ? pressedColor : (isHovered ? hoverColor : normalColor));

                var rect = new Rectangle(0, 0, btn.Width - 1, btn.Height - 1);
                using var path = GetRoundedPath(rect, radius);
                using (var fillBrush = new SolidBrush(currentColor))
                {
                    e.Graphics.FillPath(fillBrush, path);
                }

                if (borderColor != Color.Transparent && borderColor != Color.Empty)
                {
                    using var pen = new Pen(borderColor, 1.6f);
                    e.Graphics.DrawPath(pen, path);
                }

                TextRenderer.DrawText(
                    e.Graphics,
                    btn.Text,
                    btn.Font,
                    new Rectangle(0, 0, btn.Width, btn.Height),
                    btn.ForeColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis
                );
            };

            btn.Resize += (s, e) =>
            {
                if (btn.Width > 0 && btn.Height > 0)
                {
                    var oldRegion = btn.Region;
                    btn.Region = new Region(GetRoundedPath(new Rectangle(0, 0, btn.Width, btn.Height), radius));
                    oldRegion?.Dispose();
                    btn.Invalidate();
                }
            };

            if (btn.Width > 0 && btn.Height > 0)
            {
                btn.Region = new Region(GetRoundedPath(new Rectangle(0, 0, btn.Width, btn.Height), radius));
            }
        }

        private static void SetupBadgeRoundedEdges(Label badge, int radius)
        {
            badge.Paint += (s, e) =>
            {
                if (s is not Control ctrl) return;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                Color parentBg = badge.Parent?.BackColor != null && badge.Parent.BackColor != Color.Transparent
                    ? badge.Parent.BackColor
                    : Color.White;

                using (var parentBrush = new SolidBrush(parentBg))
                {
                    e.Graphics.FillRectangle(parentBrush, 0, 0, badge.Width, badge.Height);
                }

                var rect = new Rectangle(0, 0, badge.Width - 1, badge.Height - 1);
                using var path = GetRoundedPath(rect, radius);
                using (var badgeBrush = new SolidBrush(badge.BackColor))
                {
                    e.Graphics.FillPath(badgeBrush, path);
                }

                TextRenderer.DrawText(
                    e.Graphics,
                    badge.Text,
                    badge.Font,
                    new Rectangle(0, 0, badge.Width, badge.Height),
                    badge.ForeColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                );
            };

            badge.Resize += (s, e) =>
            {
                if (badge.Width > 0 && badge.Height > 0)
                {
                    var oldRegion = badge.Region;
                    badge.Region = new Region(GetRoundedPath(new Rectangle(0, 0, badge.Width, badge.Height), radius));
                    oldRegion?.Dispose();
                    badge.Invalidate();
                }
            };

            if (badge.Width > 0 && badge.Height > 0)
            {
                badge.Region = new Region(GetRoundedPath(new Rectangle(0, 0, badge.Width, badge.Height), radius));
            }
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

        public void LoadDummyData()
        {
            if (_allHistoryRecords.Count == 0)
            {
                string[] models = ["6060", "6061", "7050", "8010", "5020"];
                string[] types = ["AC", "DC", "HV", "HYBRID"];
                string[] areas = ["Engine Control", "Cabin", "Main Frame", "Boom", "Power Module", "Lighting", "Hydraulic Control", "Monitoring System"];
                string[] parts = ["WH-ENG-001", "WH-CAB-002", "WH-MF-003", "WH-BOOM-004", "WH-PM-005", "WH-LTG-006", "WH-HYD-007", "WH-MON-008"];
                string[] operators = ["Fally", "System", "Admin", "John", "Sarah"];
                string[] results = ["PASS", "PASS", "PASS", "PASS", "FAIL"];

                for (int i = 1; i <= 58; i++)
                {
                    string model = models[i % models.Length];
                    string type = types[i % types.Length];
                    string area = areas[i % areas.Length];
                    string part = parts[i % parts.Length];
                    string serial = $"SN{12300 + i}";
                    string result = results[i % results.Length];
                    string totalTime = $"00:{(08 + i % 5):D2}:{(10 + i * 7) % 60:D2}";
                    string op = operators[i % operators.Length];
                    string time = $"15 Mar 2026 {(10 + i / 60):D2}:{(30 + i % 60) % 60:D2}:{(20 + i * 3) % 60:D2} AM";

                    _allHistoryRecords.Add([i.ToString(), time, model, type, area, part, serial, result, totalTime, op, "⋮"]);
                }
            }

            PopulateHistoryGrid();
        }

        private void PopulateHistoryGrid()
        {
            gridData.Columns.Clear();
            gridData.Rows.Clear();

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

            int totalRecords = _allHistoryRecords.Count;
            int totalPages = (int)Math.Ceiling(totalRecords / (double)_pageSize);
            if (totalPages == 0) totalPages = 1;
            if (_currentPage > totalPages) _currentPage = totalPages;
            if (_currentPage < 1) _currentPage = 1;

            var pageRecords = _allHistoryRecords.Skip((_currentPage - 1) * _pageSize).Take(_pageSize);
            foreach (var rec in pageRecords)
            {
                gridData.Rows.Add(rec);
            }

            UpdatePaginationUI(totalRecords, totalPages);
        }

        private void UpdatePaginationUI(int totalFiltered, int totalPages)
        {
            if (totalFiltered == 0)
            {
                lblEntries.Text = "Showing 0 to 0 of 0 entries";
            }
            else
            {
                int start = (_currentPage - 1) * _pageSize + 1;
                int end = Math.Min(_currentPage * _pageSize, totalFiltered);
                lblEntries.Text = $"Showing {start} to {end} of {totalFiltered} entries";
            }

            if (flpPageNav is null) return;
            flpPageNav.Controls.Clear();

            var btnFirst = CreatePageButton("«", false, _currentPage > 1, (s, e) => { _currentPage = 1; PopulateHistoryGrid(); });
            flpPageNav.Controls.Add(btnFirst);

            var btnPrev = CreatePageButton("‹", false, _currentPage > 1, (s, e) => { if (_currentPage > 1) { _currentPage--; PopulateHistoryGrid(); } });
            flpPageNav.Controls.Add(btnPrev);

            for (int p = 1; p <= totalPages; p++)
            {
                if (p == 1 || p == totalPages || (p >= _currentPage - 1 && p <= _currentPage + 1))
                {
                    int pageNum = p;
                    var btnPage = CreatePageButton(pageNum.ToString(), pageNum == _currentPage, true, (s, e) => { _currentPage = pageNum; PopulateHistoryGrid(); });
                    flpPageNav.Controls.Add(btnPage);
                }
                else if (p == _currentPage - 2 || p == _currentPage + 2)
                {
                    var btnDots = CreatePageButton("...", false, false, null);
                    flpPageNav.Controls.Add(btnDots);
                }
            }

            var btnNext = CreatePageButton("›", false, _currentPage < totalPages, (s, e) => { if (_currentPage < totalPages) { _currentPage++; PopulateHistoryGrid(); } });
            flpPageNav.Controls.Add(btnNext);

            var btnLast = CreatePageButton("»", false, _currentPage < totalPages, (s, e) => { _currentPage = totalPages; PopulateHistoryGrid(); });
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

            if (text != "...")
            {
                Color norm = isActive ? Color.FromArgb(250, 204, 21) : (!isEnabled ? Color.FromArgb(248, 250, 252) : Color.White);
                Color hover = isActive ? Color.FromArgb(254, 216, 53) : (!isEnabled ? Color.FromArgb(248, 250, 252) : Color.FromArgb(248, 250, 252));
                Color press = isActive ? Color.FromArgb(234, 186, 10) : (!isEnabled ? Color.FromArgb(248, 250, 252) : Color.FromArgb(241, 245, 249));
                Color border = Color.Transparent;
                SetupButtonRoundedEdges(btn, 8, norm, hover, press, border);
            }

            return btn;
        }

        public void LoadDummyStepsData()
        {
            gridSteps.Columns.Clear();
            gridSteps.Rows.Clear();

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

            gridSteps.Rows.Add("1", "Continuity Test", "PASS", "00:01:42");
            gridSteps.Rows.Add("2", "Short Circuit Test", "PASS", "00:01:35");
            gridSteps.Rows.Add("3", "Insulation Test", "PASS", "00:01:28");
            gridSteps.Rows.Add("4", "Functional Test", "PASS", "00:02:15");
            gridSteps.Rows.Add("5", "Final Verification", "PASS", "00:02:01");
        }

        private void InitSummaryChart()
        {
            pnlChartContainer.Controls.Clear();
            var pnlCustomChart = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            pnlCustomChart.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                int size = Math.Min(pnlCustomChart.Width, pnlCustomChart.Height) - 20;
                if (size <= 0) return;
                int x = (pnlCustomChart.Width - size) / 2;
                int y = (pnlCustomChart.Height - size) / 2;
                var rect = new Rectangle(x, y, size, size);

                // Doughnut: 80% Pass (Green), 20% Fail (Red)
                using var penGreen = new Pen(Color.FromArgb(76, 175, 80), 18);
                using var penRed = new Pen(Color.FromArgb(244, 67, 54), 18);
                e.Graphics.DrawArc(penGreen, rect, -90, 288);
                e.Graphics.DrawArc(penRed, rect, 198, 72);

                string text = "5\nTOTAL";
                using var font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                using var brush = new SolidBrush(Color.FromArgb(30, 41, 59));
                using var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                e.Graphics.DrawString(text, font, brush, new RectangleF(x, y, size, size), format);
            };
            pnlChartContainer.Controls.Add(pnlCustomChart);
        }

        private static void gridData_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.Value != null)
            {
                // Formatting handled by CellPainting for custom capsule shapes
            }
        }

        private void gridData_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && gridData.Columns[e.ColumnIndex].Name == "Result" && e.Value is not null && e.Graphics is not null)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                string val = e.Value.ToString() ?? "";
                Color bgCol = Color.FromArgb(209, 231, 221);
                Color fgCol = Color.FromArgb(15, 81, 50);

                if (val == "FAIL")
                {
                    bgCol = Color.FromArgb(248, 215, 218);
                    fgCol = Color.FromArgb(132, 32, 41);
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
