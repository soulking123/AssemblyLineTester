using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AssemblyLineTester.Helpers;
using AssemblyLineTester.Models;
using AssemblyLineTester.Services;
using AssemblyLineTester.Views.Pages.Tabs;

namespace AssemblyLineTester.Views.Pages
{
    public partial class HarnessDatabase : UserControl
    {
        private int _currentPage = 1;
        private int _pageSize = 10;
        private List<ProductHarness> _allHarnessEntities = new();
        private List<ProductHarness> _filteredHarnessEntities = new();
        private ProductHarness? _selectedHarness;
        private Label? activeTabLabel;
        private Label? _lblPageInfo;

        private readonly HarnessService _harnessService = new();

        private HarnessGeneralTab _tabGeneral = null!;
        private HarnessConnectorTab _tabConnector = null!;
        private HarnessWiringTab _tabWiring = null!;

        public HarnessDatabase()
        {
            InitializeComponent();
            AssemblyLineTester.Helpers.UIStyleHelper.StyleDataGridView(dataGridViewList);
            dataGridViewList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            
            dataGridViewList.CellClick += DataGridViewList_CellClick;
            
            InitializeCustomStyles();
            InitializeTabs();
            SetupInteractiveTabs();
            SetupEvents();
            SetupPagination();
            
            PopulateHarnessData();

            DoubleBuffered = true;
        }

        private void InitializeTabs()
        {
            _tabGeneral = new HarnessGeneralTab();
            _tabGeneral.Dock = DockStyle.Fill;
            _tabGeneral.Visible = true;

            _tabConnector = new HarnessConnectorTab();
            _tabConnector.Dock = DockStyle.Fill;
            _tabConnector.Visible = false;

            _tabWiring = new HarnessWiringTab();
            _tabWiring.Dock = DockStyle.Fill;
            _tabWiring.Visible = false;

            panelTabContent.Controls.Add(_tabGeneral);
            panelTabContent.Controls.Add(_tabConnector);
            panelTabContent.Controls.Add(_tabWiring);

            _tabGeneral.Initialize(OnEditHarness_Click, OnDeleteHarness_Click);
            _tabConnector.Initialize();
            
            EnableDoubleBufferingRecursive(panelTabContent);
        }
        
        private static void EnableDoubleBufferingRecursive(Control control)
        {
            if (control is TableLayoutPanel || control is Panel || control is FlowLayoutPanel || control is SplitContainer)
            {
                try
                {
                    typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(control, true, null);
                }
                catch { /* ignore */ }
            }
            foreach (Control child in control.Controls)
            {
                EnableDoubleBufferingRecursive(child);
            }
        }

        private void SetupInteractiveTabs()
        {
            Label[] tabLabels = new[] { lblNavGeneral, lblNavConnector, lblNavWiring };
            activeTabLabel = lblNavGeneral;
            
            foreach (Label t in tabLabels)
            {
                t.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font105Regular;
                t.ForeColor = Color.FromArgb(100, 116, 139);
            }
            lblNavGeneral.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font105Bold;
            lblNavGeneral.ForeColor = Color.FromArgb(30, 41, 59);

            foreach (Label lbl in tabLabels)
            {
                lbl.Padding = new Padding(0, 0, 0, 10);
                lbl.Paint += (s, ev) =>
                {
                    if (lbl == activeTabLabel)
                    {
                        using var brush = new SolidBrush(Color.FromArgb(250, 204, 21));
                        ev.Graphics.FillRectangle(brush, 0, lbl.Height - 6, lbl.Width, 6);
                    }
                };
            }
            
            lblNavGeneral.Click += lblNavGeneral_Click;
            lblNavConnector.Click += lblNavConnector_Click;
            lblNavWiring.Click += lblNavWiring_Click;
        }

        private void DataGridViewList_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dataGridViewList.Rows[e.RowIndex].Tag is not int harnessId) return;

            var harness = _allHarnessEntities.FirstOrDefault(h => h.ProductHarnessId == harnessId);
            if (harness != null)
            {
                _selectedHarness = harness;
                _tabGeneral.RenderHarnessSelected(harness);
                _tabConnector.RefreshConnectorGrid();
                PopulateBottomCards();
                lblNavGeneral_Click(lblNavGeneral, EventArgs.Empty);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                AssemblyLineTester.Helpers.UIStyleHelper.ScaleExplicitFonts(this, AssemblyLineTester.Helpers.UIStyleHelper.GetHostScaleFactor(this));
                dataGridViewList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dataGridViewList.ScrollBars = ScrollBars.Both;

                foreach (DataGridViewColumn col in dataGridViewList.Columns)
                {
                    col.Resizable = DataGridViewTriState.True;
                }

                if (dataGridViewList.Columns["colNo"] is DataGridViewColumn colNo)
                {
                    colNo.MinimumWidth = 50;
                    colNo.Width = 60;
                }
                if (dataGridViewList.Columns["colProductModel"] is DataGridViewColumn colProductModel)
                {
                    colProductModel.MinimumWidth = 95;
                    colProductModel.Width = 115;
                }
                if (dataGridViewList.Columns["colProductType"] is DataGridViewColumn colProductType)
                {
                    colProductType.MinimumWidth = 90;
                    colProductType.Width = 105;
                }
                if (dataGridViewList.Columns["colArea"] is DataGridViewColumn colArea)
                {
                    colArea.MinimumWidth = 170;
                    colArea.Width = 220;
                }
                if (dataGridViewList.Columns["colPartNumber"] is DataGridViewColumn colPartNumber)
                {
                    colPartNumber.MinimumWidth = 145;
                    colPartNumber.Width = 175;
                }
                if (dataGridViewList.Columns["colLastUpdated"] is DataGridViewColumn colLastUpdated)
                {
                    colLastUpdated.MinimumWidth = 140;
                    colLastUpdated.Width = 160;
                    colLastUpdated.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
        }

        private void SetupEvents()
        {
            btnAddHarness.Click += (s, e) =>
            {
                if (!HasHarnessEditPermission()) return;

                using var modal = new AssemblyLineTester.Views.Modals.HarnessModalForm(_harnessService);
                if (modal.ShowDialog(this.FindForm()) == DialogResult.OK)
                {
                    PopulateHarnessData();
                    var created = _allHarnessEntities.OrderByDescending(h => h.ProductHarnessId).FirstOrDefault();
                    if (created != null)
                    {
                        _selectedHarness = created;
                        _tabGeneral.RenderHarnessSelected(created);
                        _tabConnector.RefreshConnectorGrid();
                        PopulateBottomCards();
                        lblNavGeneral_Click(lblNavGeneral, EventArgs.Empty);
                    }
                }
            };

            btnSearch.Click += (s, e) => ApplyFilters();
            cmbProductModel.SelectedIndexChanged += (s, e) => ApplyFilters();
            cmbProductType.SelectedIndexChanged += (s, e) => ApplyFilters();
            cmbArea.SelectedIndexChanged += (s, e) => ApplyFilters();
            cmbStatus.SelectedIndexChanged += (s, e) => ApplyFilters();
            
            btnReset.Click += (s, e) =>
            {
                cmbProductModel.SelectedIndex = 0;
                cmbProductType.SelectedIndex = 0;
                cmbArea.SelectedIndex = 0;
                txtPartNumber.Clear();
                ApplyFilters();
            };
            txtPartNumber.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    ApplyFilters();
                }
            };
        }
        
        private void OnEditHarness_Click(object? sender, EventArgs e)
        {
            if (!HasHarnessEditPermission()) return;

            if (_selectedHarness == null)
            {
                MessageBox.Show("Please select a harness from the list first.", "No Harness Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var modal = new AssemblyLineTester.Views.Modals.HarnessModalForm(_harnessService, _selectedHarness);
            if (modal.ShowDialog(this.FindForm()) == DialogResult.OK)
            {
                int editedId = _selectedHarness.ProductHarnessId;
                PopulateHarnessData();
                var refreshed = _allHarnessEntities.FirstOrDefault(h => h.ProductHarnessId == editedId);
                if (refreshed != null)
                {
                    _selectedHarness = refreshed;
                    _tabGeneral.RenderHarnessSelected(refreshed);
                    _tabConnector.RefreshConnectorGrid();
                    PopulateBottomCards();
                }
            }
        }
        
        private void OnDeleteHarness_Click(object? sender, EventArgs e)
        {
            if (!HasHarnessEditPermission()) return;

            if (_selectedHarness == null)
            {
                MessageBox.Show("Please select a harness from the list first.", "No Harness Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"Delete harness '{_selectedHarness.Name}'? Its connectors and saved images will also be removed. This action cannot be undone.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            var (success, error) = _harnessService.DeleteHarness(_selectedHarness.ProductHarnessId);
            if (!success)
            {
                MessageBox.Show(error ?? "Failed to delete harness.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            PopulateHarnessData();
        }

        private bool HasHarnessEditPermission()
        {
            // Was gated on Action.Database.Backup, which is a different job entirely: anyone trusted
            // to take a backup could delete harnesses. The role-name fallback is gone too, since
            // HasPermission already answers true for Administrator.
            if (!SessionManager.HasPermission("Action.HarnessDatabase.Edit"))
            {
                MessageBox.Show("You do not have permission to modify harness records.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void ApplyFilters()
        {
            SuspendLayout();
            try
            {
                string model = cmbProductModel.SelectedItem?.ToString() ?? "All";
                string type = cmbProductType.SelectedItem?.ToString() ?? "All";
                string area = cmbArea.SelectedItem?.ToString() ?? "All";
                string partNumberQuery = txtPartNumber.Text.Trim();

                _filteredHarnessEntities = _allHarnessEntities.Where(h =>
                    (model == "All" || h.ProductModel == model) &&
                    (type == "All" || h.ProductType == type) &&
                    (area == "All" || h.Area == area) &&
                    (string.IsNullOrEmpty(partNumberQuery) ||
                     h.ConnectorList.Any(c => c.PartNumber.Contains(partNumberQuery, StringComparison.OrdinalIgnoreCase)) ||
                     h.ConnectorListName.Any(n => n.Contains(partNumberQuery, StringComparison.OrdinalIgnoreCase)))
                ).ToList();

                _currentPage = 1;
                _selectedHarness = null;
                _tabGeneral.ShowNoHarnessSelectedState();
                _tabConnector.RefreshConnectorGrid();
                PopulateBottomCards();
                PopulateGrid();
            }
            finally
            {
                ResumeLayout(true);
            }
        }

        private void SetupPagination()
        {
            UIStyleHelper.ClearAndDispose(flpPages.Controls);
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

        private void PopulateHarnessData()
        {
            SuspendLayout();
            try
            {
                _allHarnessEntities = _harnessService.GetAllHarnesses();

                cmbProductModel.Items.Clear();
                cmbProductModel.Items.Add("All");
                cmbProductModel.Items.AddRange(_allHarnessEntities.Select(h => h.ProductModel).Distinct().OrderBy(m => m).ToArray());
                cmbProductModel.SelectedIndex = 0;

                cmbProductType.Items.Clear();
                cmbProductType.Items.Add("All");
                cmbProductType.Items.AddRange(_allHarnessEntities.Select(h => h.ProductType).Distinct().OrderBy(t => t).ToArray());
                cmbProductType.SelectedIndex = 0;

                cmbArea.Items.Clear();
                cmbArea.Items.Add("All");
                cmbArea.Items.AddRange(_allHarnessEntities.Select(h => h.Area).Distinct().OrderBy(a => a).ToArray());
                cmbArea.SelectedIndex = 0;

                _filteredHarnessEntities = new List<ProductHarness>(_allHarnessEntities);

                _selectedHarness = null;
                _tabGeneral.ShowNoHarnessSelectedState();
                _tabConnector.RefreshConnectorGrid();
                PopulateBottomCards();
                PopulateGrid();
            }
            finally
            {
                ResumeLayout(true);
            }
        }

        private void PopulateGrid()
        {
            int totalFiltered = _filteredHarnessEntities.Count;
            int totalPages = (int)Math.Ceiling((double)totalFiltered / _pageSize);
            if (_currentPage > totalPages && totalPages > 0) _currentPage = totalPages;
            if (_currentPage < 1) _currentPage = 1;

            dataGridViewList.Rows.Clear();
            var pageItems = _filteredHarnessEntities.Skip((_currentPage - 1) * _pageSize).Take(_pageSize);
            int rowNo = (_currentPage - 1) * _pageSize + 1;
            foreach (var h in pageItems)
            {
                string partNumbers = h.ConnectorList.Count > 0
                    ? string.Join(", ", h.ConnectorList.Select(c => c.PartNumber).Where(p => !string.IsNullOrEmpty(p)).Distinct())
                    : (h.ConnectorListName.Count > 0 ? string.Join(", ", h.ConnectorListName) : "-");
                string lastUpdated = h.LastUpdate.ToString("dd MMM yyyy");
                int rowIndex = dataGridViewList.Rows.Add(rowNo.ToString(), h.ProductModel, h.ProductType, h.Area, partNumbers, lastUpdated);
                dataGridViewList.Rows[rowIndex].Tag = h.ProductHarnessId;
                rowNo++;
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

            UIStyleHelper.ClearAndDispose(flpPages.Controls);

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

        private void PopulateBottomCards()
        {
            UIStyleHelper.ClearAndDispose(tblSpecsContent.Controls);
            var leftSpecs = new (string Key, string Val)[]
            {
                ("Total Connector", $": {(_selectedHarness?.ConnectorList.Count ?? 0)}"),
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

            UIStyleHelper.ClearAndDispose(tblTestSummaryContent.Controls);
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

        private void lblNavGeneral_Click(object? sender, EventArgs e)
        {
            _tabGeneral.Visible = true;
            _tabConnector.Visible = false;
            _tabWiring.Visible = false;
            _tabGeneral.BringToFront();
            
            if (sender is Label lbl)
            {
                var previous = activeTabLabel;
                activeTabLabel = lbl;
                Label[] allTabs = new[] { lblNavGeneral, lblNavConnector, lblNavWiring };
                foreach (Label t in allTabs)
                {
                    t.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font105Regular;
                    t.ForeColor = Color.FromArgb(100, 116, 139);
                }
                lbl.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font105Bold;
                lbl.ForeColor = Color.FromArgb(30, 41, 59);
                previous?.Invalidate();
                lbl.Invalidate();
            }
        }

        private void lblNavConnector_Click(object? sender, EventArgs e)
        {
            _tabGeneral.Visible = false;
            _tabConnector.Visible = true;
            _tabWiring.Visible = false;
            _tabConnector.BringToFront();
            
            if (sender is Label lbl)
            {
                var previous = activeTabLabel;
                activeTabLabel = lbl;
                Label[] allTabs = new[] { lblNavGeneral, lblNavConnector, lblNavWiring };
                foreach (Label t in allTabs)
                {
                    t.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font105Regular;
                    t.ForeColor = Color.FromArgb(100, 116, 139);
                }
                lbl.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font105Bold;
                lbl.ForeColor = Color.FromArgb(30, 41, 59);
                previous?.Invalidate();
                lbl.Invalidate();
            }
        }

        private void lblNavWiring_Click(object? sender, EventArgs e)
        {
            _tabGeneral.Visible = false;
            _tabConnector.Visible = false;
            _tabWiring.Visible = true;
            _tabWiring.BringToFront();
            
            if (sender is Label lbl)
            {
                var previous = activeTabLabel;
                activeTabLabel = lbl;
                Label[] allTabs = new[] { lblNavGeneral, lblNavConnector, lblNavWiring };
                foreach (Label t in allTabs)
                {
                    t.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font105Regular;
                    t.ForeColor = Color.FromArgb(100, 116, 139);
                }
                lbl.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font105Bold;
                lbl.ForeColor = Color.FromArgb(30, 41, 59);
                previous?.Invalidate();
                lbl.Invalidate();
            }
        }
        
    }
}
