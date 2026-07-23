using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AssemblyLineTester.Services;

namespace AssemblyLineTester.Views.Pages
{
    public partial class HarnessDatabase : UserControl
    {
        private int _currentPage = 1;
        private int _pageSize = 10;
        private readonly List<string[]> _allHarnesses = [];
        private Label? _lblPageInfo;
        private bool _isGeneralEditMode = false;
        private Image? _customHarnessImage = null;
        private Image? _customMachineImage = null;

        // Carousel States
        private int _currentHarnessSlide = 0;
        private int _currentMachineSlide = 0;
        private const int TotalSlides = 3;

        public HarnessDatabase()
        {
            InitializeComponent();
            AssemblyLineTester.Helpers.UIStyleHelper.StyleDataGridView(dataGridViewList);
            dataGridViewList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            picHarnessImage.Paint += PicHarnessImage_Paint;
            picMachineArea.Paint += PicMachineArea_Paint;
            picHarnessImage.MouseClick += PicHarnessImage_MouseClick;
            picMachineArea.MouseClick += PicMachineArea_MouseClick;
            picConnectorImage.Paint += PicConnectorImage_Paint;
            dataGridViewList.CellPainting += DataGridViewList_CellPainting;
            
            InitializeCustomStyles();
            SetupEvents();
            SetupPagination();
            InitializeConnectorGrid();
            PopulateDummyData();
        }

        private void SetupEvents()
        {
            btnAddHarness.Click += (s, e) => { if (!SessionManager.HasPermission("Action.Database.Backup") && SessionManager.CurrentUser?.Role?.Name != "Administrator") { MessageBox.Show("You do not have permission to modify harness database records.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; } MessageBox.Show("Add Harness functionality under construction.", "Info"); };
            
            btnEditGeneral.Click += (s, e) => { 
                if (!SessionManager.HasPermission("Action.Database.Backup") && SessionManager.CurrentUser?.Role?.Name != "Administrator") 
                { MessageBox.Show("You do not have permission to modify harness database records.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; } 
                
                _isGeneralEditMode = true;
                btnEditGeneral.Visible = false;
                btnDeleteHarness.Visible = false;
                btnChangeHarnessImage.Visible = true;
                btnChangeMachineImage.Visible = true;
                PopulateGeneralInfo(); // Re-render with inputs
            };
            
            btnDeleteHarness.Click += (s, e) => { 
                if (!SessionManager.HasPermission("Action.Database.Backup") && SessionManager.CurrentUser?.Role?.Name != "Administrator") 
                { MessageBox.Show("You do not have permission to modify harness database records.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; } 
                
                MessageBox.Show("Delete Harness functionality under construction.", "Info"); 
            };
            
            btnChangeHarnessImage.Click += (s, e) => {
                using var ofd = new OpenFileDialog { Filter = "Image Files|*.jpg;*.jpeg;*.png", Title = "Select Harness Image" };
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _customHarnessImage?.Dispose();
                    _customHarnessImage = Image.FromFile(ofd.FileName);
                    picHarnessImage.Invalidate();
                }
            };
            
            btnChangeMachineImage.Click += (s, e) => {
                using var ofd = new OpenFileDialog { Filter = "Image Files|*.jpg;*.jpeg;*.png", Title = "Select Machine Area Image" };
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _customMachineImage?.Dispose();
                    _customMachineImage = Image.FromFile(ofd.FileName);
                    picMachineArea.Invalidate();
                }
            };
            
            btnAddConnector.Click += (s, e) => {
                using var form = new Modals.ConnectorModalForm(isEditMode: false);
                form.ShowDialog();
            };
            btnEditConnector.Click += (s, e) => {
                using var form = new Modals.ConnectorModalForm(isEditMode: true);
                form.ShowDialog();
            };
            btnDeleteConnector.Click += (s, e) => {
                MessageBox.Show("Delete Connector functionality under construction.", "Info");
            };
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
        
        private void InitializeConnectorGrid()
        {
            dgvConnectors.Columns.Clear();
            dgvConnectors.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPerf", HeaderText = "Perf", FillWeight = 50, Visible = false });
            dgvConnectors.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPartNumber", HeaderText = "Part Number", FillWeight = 100 });
            dgvConnectors.Columns.Add(new DataGridViewTextBoxColumn { Name = "colContactType", HeaderText = "Type", FillWeight = 70 });
            dgvConnectors.Columns.Add(new DataGridViewTextBoxColumn { Name = "colContactQuality", HeaderText = "Quality", FillWeight = 100, Visible = false });
            dgvConnectors.Columns.Add(new DataGridViewTextBoxColumn { Name = "colWireSize", HeaderText = "Wire", FillWeight = 60 });
            dgvConnectors.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCurrentRating", HeaderText = "Current Rating", FillWeight = 80, Visible = false });
            dgvConnectors.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMountingStyle", HeaderText = "Mount", FillWeight = 70 });
            
            dgvConnectors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvConnectors.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvConnectors.MultiSelect = false;
            dgvConnectors.ReadOnly = true;
            dgvConnectors.AllowUserToAddRows = false;
            dgvConnectors.RowHeadersVisible = false;
            
            AssemblyLineTester.Helpers.UIStyleHelper.StyleDataGridView(dgvConnectors);
            dgvConnectors.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvConnectors.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            
            dgvConnectors.Rows.Add("1", "CN-001", "Pin", "Standard", "1.00 mm²", "10A", "Panel Mount");
            dgvConnectors.Rows.Add("2", "CN-002", "Socket", "Gold Plated", "0.75 mm²", "5A", "Cable Mount");
            
            dgvConnectors.SelectionChanged += DgvConnectors_SelectionChanged;
            if (dgvConnectors.SelectedRows.Count > 0)
                PopulateConnectorInfo(dgvConnectors.SelectedRows[0]);
        }

        private void DgvConnectors_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvConnectors.SelectedRows.Count > 0)
                PopulateConnectorInfo(dgvConnectors.SelectedRows[0]);
            else
                PopulateConnectorInfo(null);
        }

        private void PopulateConnectorInfo(DataGridViewRow? row)
        {
            tblConnectorInfo.Controls.Clear();
            if (row == null) return;

            var data = new (string Key, string Val)[]
            {
                ("Perf", row.Cells["colPerf"].Value?.ToString() ?? ""),
                ("Part Number", row.Cells["colPartNumber"].Value?.ToString() ?? ""),
                ("Contact Type", row.Cells["colContactType"].Value?.ToString() ?? ""),
                ("Contact Quality", row.Cells["colContactQuality"].Value?.ToString() ?? ""),
                ("Wire Size", row.Cells["colWireSize"].Value?.ToString() ?? ""),
                ("Current Rating", row.Cells["colCurrentRating"].Value?.ToString() ?? ""),
                ("Mounting Style", row.Cells["colMountingStyle"].Value?.ToString() ?? "")
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
                tblConnectorInfo.Controls.Add(lblKey, 0, i);

                var lblVal = new Label
                {
                    Text = ": " + data[i].Val,
                    Font = AssemblyLineTester.Helpers.UIStyleHelper.Font9,
                    ForeColor = Color.FromArgb(15, 23, 42),
                    AutoSize = true,
                    Anchor = AnchorStyles.Left | AnchorStyles.Top,
                    Margin = new Padding(0, 6, 4, 0)
                };
                tblConnectorInfo.Controls.Add(lblVal, 1, i);
            }
        }
        
        private void PicConnectorImage_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Control ctrl || e.Graphics == null) return;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int cx = ctrl.Width / 2;
            int cy = ctrl.Height / 2;
            int w = 160;
            int h = 80;
            
            var rect = new Rectangle(cx - w / 2, cy - h / 2, w, h);
            using var brush = new SolidBrush(Color.FromArgb(203, 213, 225));
            using var pen = new Pen(Color.FromArgb(100, 116, 139), 3);
            e.Graphics.FillRectangle(brush, rect);
            e.Graphics.DrawRectangle(pen, rect);
            
            using var pinBrush = new SolidBrush(Color.FromArgb(148, 163, 184));
            for (int i = 0; i < 4; i++)
            {
                int pinX = cx - w / 2 - 20;
                int pinY = cy - h / 2 + 15 + i * 15;
                e.Graphics.FillRectangle(pinBrush, pinX, pinY, 20, 5);
            }

            var f = AssemblyLineTester.Helpers.UIStyleHelper.Font10Bold;
            using var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            e.Graphics.DrawString("Connector", f, Brushes.Black, rect, sf);
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

            flpShowEntries.WrapContents = true;
            float scaleX = this.CurrentAutoScaleDimensions.Width / 13f;
            float scaleY = this.CurrentAutoScaleDimensions.Height / 32f;
            _lblPageInfo = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Margin = new Padding((int)(10 * scaleX), (int)(19 * scaleY), 0, 0),
                Name = "lblPageInfo"
            };
            flpShowEntries.Controls.Add(_lblPageInfo);
            flpShowEntries.Controls.SetChildIndex(_lblPageInfo, 0);
            flpShowEntries.SetFlowBreak(_lblPageInfo, true);
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

            float scaleX = this.CurrentAutoScaleDimensions.Width / 13f;
            float scaleY = this.CurrentAutoScaleDimensions.Height / 32f;

            var btnFirst = CreatePageButton("«", false, _currentPage > 1, (s, e) => { _currentPage = 1; PopulateGrid(); }, scaleX, scaleY);
            flpPages.Controls.Add(btnFirst);

            var btnPrev = CreatePageButton("‹", false, _currentPage > 1, (s, e) => { if (_currentPage > 1) { _currentPage--; PopulateGrid(); } }, scaleX, scaleY);
            flpPages.Controls.Add(btnPrev);

            for (int p = 1; p <= totalPages; p++)
            {
                if (p == 1 || p == totalPages || (p >= _currentPage - 1 && p <= _currentPage + 1))
                {
                    int pageNum = p;
                    var btnPage = CreatePageButton(pageNum.ToString(), pageNum == _currentPage, true, (s, e) => { _currentPage = pageNum; PopulateGrid(); }, scaleX, scaleY);
                    flpPages.Controls.Add(btnPage);
                }
                else if (p == _currentPage - 2 || p == _currentPage + 2)
                {
                    var btnDots = CreatePageButton("...", false, false, null, scaleX, scaleY);
                    flpPages.Controls.Add(btnDots);
                }
            }

            var btnNext = CreatePageButton("›", false, _currentPage < totalPages, (s, e) => { if (_currentPage < totalPages) { _currentPage++; PopulateGrid(); } }, scaleX, scaleY);
            flpPages.Controls.Add(btnNext);

            var btnLast = CreatePageButton("»", false, _currentPage < totalPages, (s, e) => { _currentPage = totalPages; PopulateGrid(); }, scaleX, scaleY);
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
            var data = new (string Key, string Val, bool IsStatus, bool Editable, bool IsMultiline)[]
            {
                ("Part Number", "WH-ENG-001", false, true, false),
                ("Product Model", "6060", false, true, false),
                ("Product Type", "AC", false, true, false),
                ("Area", "Engine Control", false, true, false),
                ("ECN No.", "123.R001", false, true, false),
                ("Revision", "A", false, true, false),
                ("Status", "Active", true, true, false),
                ("Description", "Main engine control harness routing through the firewall.\r\n  Requires high-temperature resistant sleeving.", false, true, true),
                ("Created By", "Fally Kusuma", false, false, false),
                ("Created Date", "10 Mar 2026 09:15 AM", false, false, false),
                ("Last Updated By", "Fally Kusuma", false, false, false),
                ("Last Updated Date", "14 Mar 2026 03:22 PM", false, false, false)
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
                
                if (_isGeneralEditMode && data[i].Editable)
                {
                    var pnlInput = new FlowLayoutPanel
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
                    pnlInput.Controls.Add(lblColon);

                    if (data[i].Key == "Status")
                    {
                        var cmb = new ComboBox
                        {
                            Font = AssemblyLineTester.Helpers.UIStyleHelper.Font9,
                            DropDownStyle = ComboBoxStyle.DropDownList,
                            Width = 150
                        };
                        cmb.Items.AddRange(new string[] { "Active", "Inactive" });
                        cmb.SelectedItem = data[i].Val.Trim();
                        pnlInput.Controls.Add(cmb);
                    }
                    else if (data[i].Key == "Area")
                    {
                        var cmb = new ComboBox
                        {
                            Font = AssemblyLineTester.Helpers.UIStyleHelper.Font9,
                            DropDownStyle = ComboBoxStyle.DropDownList,
                            Width = 200
                        };
                        cmb.Items.AddRange(new string[] { "Engine Control", "Cabin", "Main Frame", "Boom", "Power Module", "Lighting", "Hydraulic Control", "Monitoring System", "Battery Power", "Fuel System", "Attachment Control", "Telematics" });
                        cmb.SelectedItem = data[i].Val.Trim();
                        pnlInput.Controls.Add(cmb);
                    }
                    else
                    {
                        var txt = new TextBox
                        {
                            Text = data[i].Val,
                            Font = AssemblyLineTester.Helpers.UIStyleHelper.Font9,
                            Width = data[i].IsMultiline ? 400 : 200,
                            Multiline = data[i].IsMultiline
                        };
                        if (data[i].IsMultiline) txt.Height = 60;
                        pnlInput.Controls.Add(txt);
                    }
                    tblGenInfo.Controls.Add(pnlInput, 1, i);
                }
                else
                {
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
                            Text = ": " + data[i].Val,
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

            if (_isGeneralEditMode)
            {
                var pnlActions = new FlowLayoutPanel
                {
                    FlowDirection = FlowDirection.LeftToRight,
                    AutoSize = true,
                    Margin = new Padding(0, 16, 0, 0)
                };
                
                var btnCancelEdit = new AssemblyLineTester.Views.Controls.RoundedButton
                {
                    Text = "Cancel",
                    Size = new Size(100, 36),
                    BackColor = Color.FromArgb(241, 245, 249),
                    ForeColor = Color.FromArgb(71, 85, 105),
                    NormalBackColor = Color.FromArgb(241, 245, 249),
                    HoverBackColor = Color.FromArgb(226, 232, 240),
                    PressedBackColor = Color.FromArgb(203, 213, 225),
                    BorderRadius = 8,
                    Font = AssemblyLineTester.Helpers.UIStyleHelper.Font9Bold,
                    Margin = new Padding(0, 0, 10, 0)
                };
                btnCancelEdit.Click += (s, e) => {
                    _isGeneralEditMode = false;
                    btnEditGeneral.Visible = true;
                    btnDeleteHarness.Visible = true;
                    btnChangeHarnessImage.Visible = false;
                    btnChangeMachineImage.Visible = false;
                    PopulateGeneralInfo();
                };

                var btnSaveEdit = new AssemblyLineTester.Views.Controls.RoundedButton
                {
                    Text = "Save Changes",
                    Size = new Size(130, 36),
                    BackColor = Color.FromArgb(250, 204, 21),
                    ForeColor = Color.FromArgb(15, 23, 42),
                    BorderRadius = 8,
                    Font = AssemblyLineTester.Helpers.UIStyleHelper.Font9Bold,
                    Margin = new Padding(0)
                };
                btnSaveEdit.Click += (s, e) => {
                    MessageBox.Show("Changes saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _isGeneralEditMode = false;
                    btnEditGeneral.Visible = true;
                    btnDeleteHarness.Visible = true;
                    btnChangeHarnessImage.Visible = false;
                    btnChangeMachineImage.Visible = false;
                    PopulateGeneralInfo();
                };

                pnlActions.Controls.Add(btnCancelEdit);
                pnlActions.Controls.Add(btnSaveEdit);
                
                // Add the action buttons to the next row spanning both columns
                tblGenInfo.Controls.Add(pnlActions, 1, data.Length);
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

            if (_customHarnessImage != null)
            {
                var ratio = Math.Min((float)rect.Width / _customHarnessImage.Width, (float)rect.Height / _customHarnessImage.Height);
                var w = _customHarnessImage.Width * ratio;
                var h = _customHarnessImage.Height * ratio;
                var x = (rect.Width - w) / 2;
                var y = (rect.Height - h) / 2;
                e.Graphics.DrawImage(_customHarnessImage, x, y, w, h);
                return;
            }

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

            if (_customMachineImage != null)
            {
                var ratio = Math.Min((float)rect.Width / _customMachineImage.Width, (float)rect.Height / _customMachineImage.Height);
                var w = _customMachineImage.Width * ratio;
                var h = _customMachineImage.Height * ratio;
                var x = (rect.Width - w) / 2;
                var y = (rect.Height - h) / 2;
                e.Graphics.DrawImage(_customMachineImage, x, y, w, h);
                return;
            }

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
