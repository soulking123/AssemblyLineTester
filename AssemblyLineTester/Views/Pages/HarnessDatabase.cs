
using System.Drawing.Drawing2D;
using AssemblyLineTester.Services;

namespace AssemblyLineTester.Views.Pages
{
    public partial class HarnessDatabase : UserControl
    {
        private int _currentPage = 1;
        private int _pageSize = 10;
        private readonly List<string[]> _allHarnesses = [];
        private Label? _lblPageInfo;

        // Carousel States
        private int _currentHarnessSlide = 0;
        private int _currentMachineSlide = 0;
        private const int TotalSlides = 3;

        public HarnessDatabase()
        {
            InitializeComponent();
            AssemblyLineTester.Helpers.UIStyleHelper.StyleDataGridView(dataGridViewList);
            picHarnessImage.Paint += PicHarnessImage_Paint;
            picMachineArea.Paint += PicMachineArea_Paint;
            picHarnessImage.MouseClick += PicHarnessImage_MouseClick;
            picMachineArea.MouseClick += PicMachineArea_MouseClick;
            dataGridViewList.CellPainting += DataGridViewList_CellPainting;
            
            InitializeCustomStyles();
            SetupEvents();
            SetupPagination();
            PopulateDummyData();
        }

        private void SetupEvents()
        {

            btnAddHarness.Click += (s, e) => { if (!SessionManager.HasPermission("Action.Database.Backup") && SessionManager.CurrentUser?.Role?.Name != "Administrator") { MessageBox.Show("You do not have permission to modify harness database records.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; } MessageBox.Show("Add Harness functionality under construction.", "Info"); };
            btnEdit.Click += (s, e) => { if (!SessionManager.HasPermission("Action.Database.Backup") && SessionManager.CurrentUser?.Role?.Name != "Administrator") { MessageBox.Show("You do not have permission to modify harness database records.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; } MessageBox.Show("Edit Harness functionality under construction.", "Info"); };
            btnDelete.Click += (s, e) => { if (!SessionManager.HasPermission("Action.Database.Backup") && SessionManager.CurrentUser?.Role?.Name != "Administrator") { MessageBox.Show("You do not have permission to modify harness database records.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; } MessageBox.Show("Delete Harness functionality under construction.", "Info"); };
        }

        private void DataGridViewList_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.Graphics == null) return;
            if (e.RowIndex >= 0 && dataGridViewList.Columns[e.ColumnIndex].Name == "colStatus")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                string val = e.Value?.ToString() ?? "";
                if (!string.IsNullOrEmpty(val))
                {
                    Color backCol = val == "Active" ? Color.FromArgb(220, 252, 231) : Color.FromArgb(241, 245, 249);
                    Color textCol = val == "Active" ? Color.FromArgb(22, 101, 52) : Color.FromArgb(71, 85, 105);

                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    var font = AssemblyLineTester.Helpers.UIStyleHelper.Font85Bold;
                    int textWidth = (int)Math.Ceiling(e.Graphics.MeasureString(val, font).Width);
                    int pillWidth = Math.Max(76, textWidth + 24);
                    int pillHeight = 26;
                    int x = e.CellBounds.X + (e.CellBounds.Width - pillWidth) / 2;
                    int y = e.CellBounds.Y + (e.CellBounds.Height - pillHeight) / 2;
                    var pillRect = new Rectangle(x, y, pillWidth, pillHeight);

                    using var brush = new SolidBrush(backCol);
                    using var path = AssemblyLineTester.Helpers.UIStyleHelper.GetRoundedPath(pillRect, 6);
                    e.Graphics.FillPath(brush, path);

                    using var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    using var textBrush = new SolidBrush(textCol);
                    e.Graphics.DrawString(val, font, textBrush, pillRect, sf);
                }
                e.Handled = true;
            }
        }

        private void SetupPagination()
        {
            flpPages.Controls.Clear();
            if (cmbShowEntries.Items.Count > 0) cmbShowEntries.SelectedIndex = 0;
            cmbShowEntries.SelectedIndexChanged += (s, e) =>
            {
                if (int.TryParse(cmbShowEntries.SelectedItem?.ToString(), out int size))
                {
                    _pageSize = size;
                    _currentPage = 1;
                    PopulateGrid();
                }
            };

            flpShowEntries.WrapContents = false;
            _lblPageInfo = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Margin = new Padding(10, 19, 0, 0),
                Name = "lblPageInfo"
            };
            flpShowEntries.Controls.Add(_lblPageInfo);
        }

        private void PopulateDummyData()
        {
            _allHarnesses.Clear();
            string[] areas = ["Engine Control", "Cabin", "Main Frame", "Boom", "Power Module", "Lighting", "Hydraulic Control", "Monitoring System", "Battery Power", "Fuel System", "Attachment Control", "Telematics"];
            string[] status = ["Active", "Active", "Active", "Active", "Inactive"];
            string[] revs = ["A", "A", "B", "C"];

            for (int i = 1; i <= 58; i++)
            {
                string area = areas[(i - 1) % areas.Length];
                string partNo = $"WH-{(area.Length >= 3 ? area[..3].ToUpper() : "GEN")}-{i:D3}";
                string ecn = $"12{(3 + i % 5)}.R00{(i % 3)}";
                string rev = revs[i % revs.Length];
                string st = status[i % status.Length];
                string date = $"{(15 - (i % 10)):D2} Mar 2026";
                _allHarnesses.Add([i.ToString(), "6060", "AC", area, partNo, ecn, rev, st, date]);
            }

            // Populate ComboBoxes
            cmbProductModel.Items.Clear();
            cmbProductModel.Items.AddRange(["All", "6060"]);
            cmbProductModel.SelectedIndex = 0;

            cmbProductType.Items.Clear();
            cmbProductType.Items.AddRange(["All", "AC"]);
            cmbProductType.SelectedIndex = 0;

            cmbArea.Items.Clear();
            cmbArea.Items.AddRange(["All", "Engine Control", "Cabin", "Main Frame", "Boom", "Power Module", "Lighting", "Hydraulic Control", "Monitoring System", "Battery Power", "Fuel System", "Attachment Control", "Telematics"]);
            cmbArea.SelectedIndex = 0;

            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(["All", "Active", "Inactive"]);
            cmbStatus.SelectedIndex = 0;

            PopulateGeneralInfo();
            PopulateBottomCards();
            PopulateGrid();
        }

        private void PopulateGrid()
        {
            int totalFiltered = _allHarnesses.Count;
            int totalPages = (int)Math.Ceiling((double)totalFiltered / _pageSize);
            if (_currentPage > totalPages && totalPages > 0) _currentPage = totalPages;
            if (_currentPage < 1) _currentPage = 1;

            dataGridViewList.Rows.Clear();
            var pageItems = _allHarnesses.Skip((_currentPage - 1) * _pageSize).Take(_pageSize);
            foreach (var item in pageItems)
            {
                dataGridViewList.Rows.Add(item);
            }

            UpdatePaginationUI(totalFiltered, totalPages);
        }

        private void UpdatePaginationUI(int totalFiltered, int totalPages)
        {
            if (_lblPageInfo != null)
            {
                if (totalFiltered == 0)
                {
                    _lblPageInfo.Text = "Showing 0 to 0 of 0 harnesses";
                }
                else
                {
                    int start = (_currentPage - 1) * _pageSize + 1;
                    int end = Math.Min(_currentPage * _pageSize, totalFiltered);
                    _lblPageInfo.Text = $"Showing {start} to {end} of {totalFiltered} harnesses";
                }
            }

            flpPages.Controls.Clear();

            var btnFirst = CreatePageButton("«", false, _currentPage > 1, (s, e) => { _currentPage = 1; PopulateGrid(); });
            flpPages.Controls.Add(btnFirst);

            var btnPrev = CreatePageButton("‹", false, _currentPage > 1, (s, e) => { if (_currentPage > 1) { _currentPage--; PopulateGrid(); } });
            flpPages.Controls.Add(btnPrev);

            for (int p = 1; p <= totalPages; p++)
            {
                if (p == 1 || p == totalPages || (p >= _currentPage - 1 && p <= _currentPage + 1))
                {
                    int pageNum = p;
                    var btnPage = CreatePageButton(pageNum.ToString(), pageNum == _currentPage, true, (s, e) => { _currentPage = pageNum; PopulateGrid(); });
                    flpPages.Controls.Add(btnPage);
                }
                else if (p == _currentPage - 2 || p == _currentPage + 2)
                {
                    var btnDots = CreatePageButton("...", false, false, null);
                    flpPages.Controls.Add(btnDots);
                }
            }

            var btnNext = CreatePageButton("›", false, _currentPage < totalPages, (s, e) => { if (_currentPage < totalPages) { _currentPage++; PopulateGrid(); } });
            flpPages.Controls.Add(btnNext);

            var btnLast = CreatePageButton("»", false, _currentPage < totalPages, (s, e) => { _currentPage = totalPages; PopulateGrid(); });
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
                btn.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font95Bold;
                btn.FlatAppearance.BorderSize = 0;
                btn.Enabled = false;
                btn.Cursor = Cursors.Default;
            }
            else if (isActive)
            {
                btn.BackColor = Color.FromArgb(250, 204, 21);
                btn.ForeColor = Color.FromArgb(15, 23, 42);
                btn.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font95Bold;
                btn.FlatAppearance.BorderColor = Color.FromArgb(234, 179, 8);
            }
            else if (!isEnabled)
            {
                btn.BackColor = Color.FromArgb(248, 250, 252);
                btn.ForeColor = Color.FromArgb(203, 213, 225);
                btn.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font95;
                btn.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            }
            else
            {
                btn.BackColor = Color.White;
                btn.ForeColor = Color.FromArgb(51, 65, 85);
                btn.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font95;
                btn.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            }

            if (onClick != null) btn.Click += onClick;

            if (text != "...")
            {
                Color norm = isActive ? Color.FromArgb(250, 204, 21) : (!isEnabled ? Color.FromArgb(248, 250, 252) : Color.White);
                Color hover = isActive ? Color.FromArgb(254, 216, 53) : (!isEnabled ? Color.FromArgb(248, 250, 252) : Color.FromArgb(248, 250, 252));
                Color press = isActive ? Color.FromArgb(234, 186, 10) : (!isEnabled ? Color.FromArgb(248, 250, 252) : Color.FromArgb(241, 245, 249));
                Color border = Color.Transparent;
                AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btn, 8, norm, hover, press, border);
            }

            return btn;
        }

        private void PopulateGeneralInfo()
        {
            tblGenInfo.Controls.Clear();
            var data = new (string Key, string Val, bool IsStatus)[]
            {
                ("Part Number", ": WH-ENG-001", false),
                ("Product Model", ": 6060", false),
                ("Product Type", ": AC", false),
                ("Area", ": Engine Control", false),
                ("ECN No.", ": 123.R001", false),
                ("Revision", ": A", false),
                ("Status", ": Active", true),
                ("Description", ": Main engine control harness routing through the firewall.\r\n  Requires high-temperature resistant sleeving.", false),
                ("Created By", ": Fally Kusuma", false),
                ("Created Date", ": 10 Mar 2026 09:15 AM", false),
                ("Last Updated By", ": Fally Kusuma", false),
                ("Last Updated Date", ": 14 Mar 2026 03:22 PM", false)
            };

            for (int i = 0; i < data.Length; i++)
            {
                var lblKey = new Label
                {
                    Text = data[i].Key,
                    Font = AssemblyLineTester.Helpers.UIStyleHelper.Font9Bold,
                    ForeColor = Color.FromArgb(71, 85, 105),
                    AutoSize = true,
                    Anchor = AnchorStyles.Left | AnchorStyles.Top,
                    Margin = new Padding(4, 6, 4, 0)
                };
                tblGenInfo.Controls.Add(lblKey, 0, i);

                if (data[i].IsStatus)
                {
                    var pnlStatus = new FlowLayoutPanel
                    {
                        AutoSize = true,
                        FlowDirection = FlowDirection.LeftToRight,
                        WrapContents = false,
                        Margin = new Padding(0, 2, 0, 0)
                    };
                    var lblColon = new Label
                    {
                        Text = ": ",
                        Font = AssemblyLineTester.Helpers.UIStyleHelper.Font9,
                        ForeColor = Color.FromArgb(51, 65, 85),
                        AutoSize = true,
                        Margin = new Padding(0, 4, 4, 0)
                    };
                    string statusText = data[i].Val.Replace(":", "").Trim();
                    var badgeFont = AssemblyLineTester.Helpers.UIStyleHelper.Font85Bold;
                    int textWidth = TextRenderer.MeasureText(statusText, badgeFont).Width;
                    int badgeWidth = Math.Max(76, textWidth + 24);
                    Color backCol = statusText == "Active" ? Color.FromArgb(220, 252, 231) : Color.FromArgb(241, 245, 249);
                    Color textCol = statusText == "Active" ? Color.FromArgb(22, 101, 52) : Color.FromArgb(71, 85, 105);
                    var pnlBadge = new Panel
                    {
                        Size = new Size(badgeWidth, 26),
                        Margin = new Padding(0),
                        BackColor = Color.Transparent
                    };
                    pnlBadge.Paint += (s, pe) =>
                    {
                        pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        var pillRect = new Rectangle(0, 0, pnlBadge.Width, pnlBadge.Height);
                        using var brush = new SolidBrush(backCol);
                        using var path = AssemblyLineTester.Helpers.UIStyleHelper.GetRoundedPath(pillRect, 6);
                        pe.Graphics.FillPath(brush, path);

                        var f = AssemblyLineTester.Helpers.UIStyleHelper.Font85Bold;
                        using var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                        using var textBrush = new SolidBrush(textCol);
                        pe.Graphics.DrawString(statusText, f, textBrush, new RectangleF(0, 0, pnlBadge.Width, pnlBadge.Height), sf);
                    };
                    pnlStatus.Controls.Add(lblColon);
                    pnlStatus.Controls.Add(pnlBadge);
                    tblGenInfo.Controls.Add(pnlStatus, 1, i);
                }
                else
                {
                    var lblVal = new Label
                    {
                        Text = data[i].Val,
                        Font = AssemblyLineTester.Helpers.UIStyleHelper.Font9,
                        ForeColor = Color.FromArgb(15, 23, 42),
                        AutoSize = true,
                        Anchor = AnchorStyles.Left | AnchorStyles.Top,
                        Margin = new Padding(0, 6, 4, 0)
                    };
                    tblGenInfo.Controls.Add(lblVal, 1, i);
                }
            }
        }

        private void PopulateBottomCards()
        {
            tblSpecsContent.Controls.Clear();
            var leftSpecs = new (string Key, string Val)[]
            {
                ("Total Connector", ": 6"),
                ("Total Pin", ": 48"),
                ("Total Wire", ": 62"),
                ("Total Branch", ": 12")
            };
            var rightSpecs = new (string Key, string Val)[]
            {
                ("Min Wire Size", ": 0.50 mm²"),
                ("Max Wire Size", ": 4.00 mm²"),
                ("Nominal Voltage", ": 24 VDC"),
                ("Operating Temperature", ": -40 ~ 85 °C")
            };

            for (int i = 0; i < 4; i++)
            {
                var lblLeftKey = new Label
                {
                    Text = leftSpecs[i].Key,
                    Font = AssemblyLineTester.Helpers.UIStyleHelper.Font95Bold,
                    ForeColor = Color.FromArgb(71, 85, 105),
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    Margin = new Padding(0)
                };
                var lblLeftVal = new Label
                {
                    Text = leftSpecs[i].Val,
                    Font = AssemblyLineTester.Helpers.UIStyleHelper.Font95,
                    ForeColor = Color.FromArgb(15, 23, 42),
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    Margin = new Padding(0, 0, 30, 0)
                };
                tblSpecsContent.Controls.Add(lblLeftKey, 0, i);
                tblSpecsContent.Controls.Add(lblLeftVal, 1, i);

                var lblRightKey = new Label
                {
                    Text = rightSpecs[i].Key,
                    Font = AssemblyLineTester.Helpers.UIStyleHelper.Font95Bold,
                    ForeColor = Color.FromArgb(71, 85, 105),
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    Margin = new Padding(0)
                };
                var lblRightVal = new Label
                {
                    Text = rightSpecs[i].Val,
                    Font = AssemblyLineTester.Helpers.UIStyleHelper.Font95,
                    ForeColor = Color.FromArgb(15, 23, 42),
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    Margin = new Padding(0)
                };
                tblSpecsContent.Controls.Add(lblRightKey, 2, i);
                tblSpecsContent.Controls.Add(lblRightVal, 3, i);
            }

            tblTestSummaryContent.Controls.Clear();
            var summaryData = new (string Key, string Val, Color? ColorOverride)[]
            {
                ("Total Tested", ": 215", null),
                ("Pass", ": 212 (98.60%)", Color.FromArgb(22, 101, 52)),
                ("Fail", ": 3 (1.40%)", Color.FromArgb(239, 68, 68)),
                ("Last Tested", ": 15 Mar 2026 10:30 AM", null)
            };

            for (int i = 0; i < summaryData.Length; i++)
            {
                var lblKey = new Label
                {
                    Text = summaryData[i].Key,
                    Font = AssemblyLineTester.Helpers.UIStyleHelper.Font95Bold,
                    ForeColor = Color.FromArgb(71, 85, 105),
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    Margin = new Padding(0)
                };
                var lblVal = new Label
                {
                    Text = summaryData[i].Val,
                    Font = summaryData[i].ColorOverride.HasValue ? AssemblyLineTester.Helpers.UIStyleHelper.Font95Bold : AssemblyLineTester.Helpers.UIStyleHelper.Font95,
                    ForeColor = summaryData[i].ColorOverride ?? Color.FromArgb(15, 23, 42),
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    Margin = new Padding(0)
                };
                tblTestSummaryContent.Controls.Add(lblKey, 0, i);
                tblTestSummaryContent.Controls.Add(lblVal, 1, i);
            }
        }

        private void PicHarnessImage_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Control ctrl || e.Graphics == null) return;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = ctrl.ClientRectangle;
            if (rect.Width <= 1 || rect.Height <= 1) return;

            using var penBorder = new Pen(Color.FromArgb(148, 163, 184), 1.5f);
            e.Graphics.DrawRectangle(penBorder, 0, 0, rect.Width - 1, rect.Height - 1);

            int cy = rect.Height / 2;
            using var brushConn = new SolidBrush(Color.FromArgb(30, 41, 59));

            if (_currentHarnessSlide == 0)
            {
                using var penCable = new Pen(Color.FromArgb(51, 65, 85), 4);
                using var penBranch1 = new Pen(Color.FromArgb(239, 68, 68), 3);
                using var penBranch2 = new Pen(Color.FromArgb(59, 130, 246), 3);

                e.Graphics.DrawLine(penCable, rect.Width * 0.15f, cy, rect.Width * 0.7f, cy);
                e.Graphics.DrawLine(penBranch1, rect.Width * 0.45f, cy, rect.Width * 0.65f, cy - (rect.Height * 0.25f));
                e.Graphics.DrawLine(penBranch2, rect.Width * 0.55f, cy, rect.Width * 0.75f, cy + (rect.Height * 0.25f));

                e.Graphics.FillRectangle(brushConn, rect.Width * 0.1f, cy - 12, 36, 24);
                e.Graphics.FillRectangle(brushConn, rect.Width * 0.68f, cy - 10, 28, 20);
                e.Graphics.FillRectangle(brushConn, rect.Width * 0.63f, cy - (rect.Height * 0.25f) - 10, 24, 18);
                e.Graphics.FillRectangle(brushConn, rect.Width * 0.73f, cy + (rect.Height * 0.25f) - 8, 24, 18);
            }
            else if (_currentHarnessSlide == 1)
            {
                using var penCable = new Pen(Color.FromArgb(51, 65, 85), 4);
                using var penBranch = new Pen(Color.FromArgb(22, 163, 74), 3);

                e.Graphics.DrawLine(penCable, rect.Width * 0.2f, cy - 50, rect.Width * 0.8f, cy - 50);
                e.Graphics.DrawLine(penBranch, rect.Width * 0.5f, cy - 50, rect.Width * 0.5f, cy + 80);

                e.Graphics.FillRectangle(brushConn, rect.Width * 0.15f, cy - 62, 36, 24);
                e.Graphics.FillRectangle(brushConn, rect.Width * 0.78f, cy - 62, 36, 24);
                e.Graphics.FillRectangle(brushConn, rect.Width * 0.47f, cy + 80, 24, 30);
            }
            else
            {
                using var penCable = new Pen(Color.FromArgb(51, 65, 85), 4);
                
                e.Graphics.DrawLine(penCable, rect.Width * 0.25f, cy, rect.Width * 0.75f, cy);
                
                e.Graphics.FillRectangle(brushConn, rect.Width * 0.2f, cy - 20, 40, 40);
                e.Graphics.FillRectangle(brushConn, rect.Width * 0.72f, cy - 20, 40, 40);
            }

            DrawCarouselUI(e.Graphics, rect, _currentHarnessSlide);
        }

        private void PicMachineArea_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Control ctrl || e.Graphics == null) return;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = ctrl.ClientRectangle;
            if (rect.Width <= 1 || rect.Height <= 1) return;

            using var penBorder = new Pen(Color.FromArgb(148, 163, 184), 1.5f);
            e.Graphics.DrawRectangle(penBorder, 0, 0, rect.Width - 1, rect.Height - 1);

            using var penBox = new Pen(Color.FromArgb(148, 163, 184), 2) { DashStyle = DashStyle.Dash };
            var font = AssemblyLineTester.Helpers.UIStyleHelper.Font9Bold;

            if (_currentMachineSlide == 0)
            {
                var areaRect = new Rectangle((int)(rect.Width * 0.2f), (int)(rect.Height * 0.2f), (int)(rect.Width * 0.6f), (int)(rect.Height * 0.6f));
                e.Graphics.DrawRectangle(penBox, areaRect);

                using var brushHighlight = new SolidBrush(Color.FromArgb(60, 250, 204, 21));
                e.Graphics.FillRectangle(brushHighlight, (int)(rect.Width * 0.4f), (int)(rect.Height * 0.35f), (int)(rect.Width * 0.25f), (int)(rect.Height * 0.3f));

                TextRenderer.DrawText(e.Graphics, "ENGINE ROOM - ZONE A2", font, areaRect, Color.FromArgb(71, 85, 105), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
            else if (_currentMachineSlide == 1)
            {
                var areaRect = new Rectangle((int)(rect.Width * 0.1f), (int)(rect.Height * 0.1f), (int)(rect.Width * 0.4f), (int)(rect.Height * 0.8f));
                e.Graphics.DrawRectangle(penBox, areaRect);

                using var brushHighlight = new SolidBrush(Color.FromArgb(60, 59, 130, 246));
                e.Graphics.FillRectangle(brushHighlight, (int)(rect.Width * 0.15f), (int)(rect.Height * 0.2f), (int)(rect.Width * 0.3f), (int)(rect.Height * 0.2f));

                TextRenderer.DrawText(e.Graphics, "CABIN MAIN CONTROL", font, areaRect, Color.FromArgb(71, 85, 105), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
            else
            {
                var areaRect = new Rectangle((int)(rect.Width * 0.5f), (int)(rect.Height * 0.3f), (int)(rect.Width * 0.4f), (int)(rect.Height * 0.4f));
                e.Graphics.DrawRectangle(penBox, areaRect);

                using var brushHighlight = new SolidBrush(Color.FromArgb(60, 239, 68, 68));
                e.Graphics.FillRectangle(brushHighlight, (int)(rect.Width * 0.55f), (int)(rect.Height * 0.4f), (int)(rect.Width * 0.3f), (int)(rect.Height * 0.2f));

                TextRenderer.DrawText(e.Graphics, "HYDRAULIC PUMP C", font, areaRect, Color.FromArgb(71, 85, 105), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }

            DrawCarouselUI(e.Graphics, rect, _currentMachineSlide);
        }

        private void DrawCarouselUI(Graphics g, Rectangle rect, int currentSlide)
        {
            int arrowZoneWidth = 50;
            using var arrowBgBrush = new SolidBrush(Color.FromArgb(10, 0, 0, 0));
            g.FillRectangle(arrowBgBrush, 0, 0, arrowZoneWidth, rect.Height);
            g.FillRectangle(arrowBgBrush, rect.Width - arrowZoneWidth, 0, arrowZoneWidth, rect.Height);

            using var arrowPen = new Pen(Color.FromArgb(100, 116, 139), 3);
            arrowPen.StartCap = LineCap.Round;
            arrowPen.EndCap = LineCap.Round;
            arrowPen.LineJoin = LineJoin.Round;

            int cy = rect.Height / 2;
            int arrowSize = 12;
            
            int leftX = 20;
            g.DrawLines(arrowPen, [new Point(leftX + arrowSize, cy - arrowSize), new Point(leftX, cy), new Point(leftX + arrowSize, cy + arrowSize)]);

            int rightX = rect.Width - 20 - arrowSize;
            g.DrawLines(arrowPen, [new Point(rightX, cy - arrowSize), new Point(rightX + arrowSize, cy), new Point(rightX, cy + arrowSize)]);

            int dotSize = 8;
            int dotSpacing = 8;
            int totalDotsWidth = (TotalSlides * dotSize) + ((TotalSlides - 1) * dotSpacing);
            int startX = (rect.Width - totalDotsWidth) / 2;
            int dotY = rect.Height - 20;

            for (int i = 0; i < TotalSlides; i++)
            {
                var dotRect = new Rectangle(startX + (i * (dotSize + dotSpacing)), dotY, dotSize, dotSize);
                if (i == currentSlide)
                {
                    using var activeBrush = new SolidBrush(Color.FromArgb(250, 204, 21));
                    g.FillEllipse(activeBrush, dotRect);
                }
                else
                {
                    using var inactiveBrush = new SolidBrush(Color.FromArgb(203, 213, 225));
                    g.FillEllipse(inactiveBrush, dotRect);
                }
            }
        }

        private void PicHarnessImage_MouseClick(object? sender, MouseEventArgs e)
        {
            if (sender is not Control ctrl) return;
            HandleCarouselClick(e.Location, ctrl.Width, ref _currentHarnessSlide);
            ctrl.Invalidate();
        }

        private void PicMachineArea_MouseClick(object? sender, MouseEventArgs e)
        {
            if (sender is not Control ctrl) return;
            HandleCarouselClick(e.Location, ctrl.Width, ref _currentMachineSlide);
            ctrl.Invalidate();
        }

        private void HandleCarouselClick(Point clickLocation, int controlWidth, ref int currentSlide)
        {
            int arrowZoneWidth = 50;
            if (clickLocation.X <= arrowZoneWidth)
            {
                currentSlide = (currentSlide - 1 + TotalSlides) % TotalSlides;
            }
            else if (clickLocation.X >= controlWidth - arrowZoneWidth)
            {
                currentSlide = (currentSlide + 1) % TotalSlides;
            }
        }
    }
}
