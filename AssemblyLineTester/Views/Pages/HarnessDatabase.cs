
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AssemblyLineTester.Models;
using AssemblyLineTester.Services;

namespace AssemblyLineTester.Views.Pages
{
    public partial class HarnessDatabase : UserControl
    {
        private int _currentPage = 1;
        private int _pageSize = 10;
        private List<ProductHarness> _allHarnessEntities = [];
        private List<ProductHarness> _filteredHarnessEntities = [];
        private ProductHarness? _selectedHarness;
        private Connector? _selectedConnector;
        private int _genInfoRowIndex;
        private Label? _lblConnPartNumber, _lblConnContactType, _lblConnContactQty, _lblConnWireSize, _lblConnCurrentRating, _lblConnMountingStyle;
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
            dataGridViewList.CellClick += DataGridViewList_CellClick;
            this.Load += HarnessDatabase_Load;
            picHarnessImage.MouseClick += PicHarnessImage_MouseClick;
            picMachineArea.MouseClick += PicMachineArea_MouseClick;          
            InitializeCustomStyles();
            SetupEvents();
            SetupPagination();
            PopulateHarnessData();
        }

        // Fires when the user clicks a row in the harness list - loads that harness into the General tab.
        private void DataGridViewList_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dataGridViewList.Rows[e.RowIndex].Tag is not int harnessId) return;

            var harness = _allHarnessEntities.FirstOrDefault(h => h.ProductHarnessId == harnessId);
            if (harness != null) RenderHarnessSelected(harness);
        }

        private void HarnessDatabase_Load(object? sender, EventArgs e)
        {
            if (!DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
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

            btnAddHarness.Click += (s, e) => { if (!SessionManager.HasPermission("Action.Database.Backup") && SessionManager.CurrentUser?.Role?.Name != "Administrator") { MessageBox.Show("You do not have permission to modify harness database records.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; } MessageBox.Show("Add Harness functionality under construction.", "Info"); };
            btnEdit.Click += (s, e) => { if (!SessionManager.HasPermission("Action.Database.Backup") && SessionManager.CurrentUser?.Role?.Name != "Administrator") { MessageBox.Show("You do not have permission to modify harness database records.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; } MessageBox.Show("Edit Harness functionality under construction.", "Info"); };
            btnDelete.Click += (s, e) => { if (!SessionManager.HasPermission("Action.Database.Backup") && SessionManager.CurrentUser?.Role?.Name != "Administrator") { MessageBox.Show("You do not have permission to modify harness database records.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; } MessageBox.Show("Delete Harness functionality under construction.", "Info"); };

            btnSearch.Click += (s, e) => ApplyFilters();
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

        // Filters _allHarnessEntities by Product Model, Product Type, Area, and Part Number,
        // then refreshes the grid from page 1.
        private void ApplyFilters()
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
            ShowNoHarnessSelectedState(); // selected harness may no longer be in the filtered result
            PopulateGrid();
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

        private void PopulateHarnessData()
        {
            var harnessService = new HarnessService();
            _allHarnessEntities = harnessService.GetAllHarnesses();

            // Populate filter ComboBoxes from actual data instead of hardcoded values
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

            ShowNoHarnessSelectedState();
            PopulateBottomCards();
            PopulateGrid();
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
                dataGridViewList.Rows[rowIndex].Tag = h.ProductHarnessId; // used by DataGridViewList_CellClick to look up the entity
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

        // Shown before the user clicks any harness in the list.
        private void ShowNoHarnessSelectedState()
        {
            _selectedHarness = null;
            _selectedConnector = null;

            tblGenInfo.Controls.Clear();
            tblGenInfo.RowStyles.Clear();
            tblGenInfo.RowCount = 1;
            tblGenInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));

            var lbl = new Label
            {
                Text = "Select a harness from the list on the left to view its details.",
                Font = AssemblyLineTester.Helpers.UIStyleHelper.Font9,
                ForeColor = Color.FromArgb(100, 116, 139),
                AutoSize = true,
                Anchor = AnchorStyles.Left | AnchorStyles.Top,
                Margin = new Padding(4, 4, 4, 0)
            };
            tblGenInfo.SetColumnSpan(lbl, 2);
            tblGenInfo.Controls.Add(lbl, 0, 0);

            SetHarnessImage(null);
            SetConnectorImage(null);
        }

        // Called when a harness row is clicked. Shows harness fields, the harness image,
        // and a Connector dropdown; connector-specific fields stay blank until a connector is picked.
        private void RenderHarnessSelected(ProductHarness harness)
        {
            _selectedHarness = harness;
            _selectedConnector = null;
            _genInfoRowIndex = 0;

            tblGenInfo.Controls.Clear();
            tblGenInfo.RowStyles.Clear();
            tblGenInfo.RowCount = 0;

            AddGenInfoValueRow("Harness Name", harness.Name);
            AddGenInfoValueRow("Product Model", harness.ProductModel);
            AddGenInfoValueRow("Product Type", harness.ProductType);
            AddGenInfoValueRow("Area", harness.Area);
            AddGenInfoValueRow("Last Updated", harness.LastUpdate.ToString("dd MMM yyyy HH:mm"));

            AddConnectorDropdownRow(harness.ConnectorListName, harness.ConnectorList);

            _lblConnPartNumber = AddGenInfoValueRow("Part Number", "-");
            _lblConnContactType = AddGenInfoValueRow("Contact Type", "-");
            _lblConnContactQty = AddGenInfoValueRow("Contact Quantity", "-");
            _lblConnWireSize = AddGenInfoValueRow("Wire Size", "-");
            _lblConnCurrentRating = AddGenInfoValueRow("Current Rating", "-");
            _lblConnMountingStyle = AddGenInfoValueRow("Mounting Style", "-");

            SetHarnessImage(harness.ImagePath);
            SetConnectorImage(null);
        }

        // Adds a key/value row to tblGenInfo and returns the value Label so its text can be updated later.
        private Label AddGenInfoValueRow(string key, string value)
        {
            tblGenInfo.RowCount++;
            tblGenInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));

            var lblKey = new Label
            {
                Text = key,
                Font = AssemblyLineTester.Helpers.UIStyleHelper.Font9Bold,
                ForeColor = Color.FromArgb(71, 85, 105),
                AutoSize = true,
                Anchor = AnchorStyles.Left | AnchorStyles.Top,
                Margin = new Padding(4, 4, 4, 0)
            };
            var lblVal = new Label
            {
                Text = value,
                Font = AssemblyLineTester.Helpers.UIStyleHelper.Font9,
                ForeColor = Color.FromArgb(15, 23, 42),
                AutoSize = true,
                Anchor = AnchorStyles.Left | AnchorStyles.Top,
                Margin = new Padding(0, 4, 4, 0)
            };
            tblGenInfo.Controls.Add(lblKey, 0, _genInfoRowIndex);
            tblGenInfo.Controls.Add(lblVal, 1, _genInfoRowIndex);
            _genInfoRowIndex++;
            return lblVal;
        }

        // Adds the Connector dropdown row, populated from the selected harness's connector list name (J1-J6).
        private void AddConnectorDropdownRow(List<string> connectorNames, List<Connector> connectors)
        {
            tblGenInfo.RowCount++;
            tblGenInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));

            var lblKey = new Label
            {
                Text = "Connector",
                Font = AssemblyLineTester.Helpers.UIStyleHelper.Font9Bold,
                ForeColor = Color.FromArgb(71, 85, 105),
                AutoSize = true,
                Anchor = AnchorStyles.Left | AnchorStyles.Top,
                Margin = new Padding(4, 4, 4, 0)
            };
            tblGenInfo.Controls.Add(lblKey, 0, _genInfoRowIndex);

            var cmb = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = AssemblyLineTester.Helpers.UIStyleHelper.Font9,
                Width = 260,
                Anchor = AnchorStyles.Left | AnchorStyles.Top,
                Margin = new Padding(0, 0, 4, 0)
            };

            if (connectorNames.Count == 0)
            {
                cmb.Items.Add("No connector found for this harness");
                cmb.SelectedIndex = 0;
                cmb.Enabled = false;
            }
            else
            {
                foreach (var name in connectorNames) cmb.Items.Add(name);
                cmb.SelectedIndexChanged += (s, e) =>
                {
                    if (s is ComboBox combo && combo.SelectedIndex >= 0)
                    {
                        int index = combo.SelectedIndex;
                        Connector? conn = (index < connectors.Count) ? connectors[index] : (connectors.Count > 0 ? connectors[0] : null);
                        OnConnectorSelected(conn);
                    }
                };
            }

            tblGenInfo.Controls.Add(cmb, 1, _genInfoRowIndex);
            _genInfoRowIndex++;
        }

        // Fires when the user picks a connector from the dropdown - fills in connector fields and the connector image.
        private void OnConnectorSelected(Connector? conn)
        {
            _selectedConnector = conn;
            if (conn != null)
            {
                _lblConnPartNumber!.Text = conn.PartNumber;
                _lblConnContactType!.Text = conn.ContactType;
                _lblConnContactQty!.Text = conn.ContactQuantity.ToString();
                _lblConnWireSize!.Text = conn.WireSize;
                _lblConnCurrentRating!.Text = $"{conn.CurrentRating} A";
                _lblConnMountingStyle!.Text = conn.MountingStyle;
                SetConnectorImage(conn.ImagePath);
            }
            else
            {
                _lblConnPartNumber!.Text = "-";
                _lblConnContactType!.Text = "-";
                _lblConnContactQty!.Text = "-";
                _lblConnWireSize!.Text = "-";
                _lblConnCurrentRating!.Text = "-";
                _lblConnMountingStyle!.Text = "-";
                SetConnectorImage(null);
            }
        }

        // Top image (picHarnessImage) shows the harness's own picture.
        private void SetHarnessImage(string? relativePath) => LoadPictureBoxImage(picHarnessImage, relativePath);

        // Bottom image (picMachineArea) now shows the selected connector's picture.
        private void SetConnectorImage(string? relativePath) => LoadPictureBoxImage(picMachineArea, relativePath);

        // Loads an image file from Assets/img or the app's local Images folder into a PictureBox, or clears it if not found.
        private void LoadPictureBoxImage(PictureBox pic, string? relativePath)
        {
            var oldImage = pic.Image;
            pic.Image = null;
            oldImage?.Dispose();

            if (!string.IsNullOrWhiteSpace(relativePath))
            {
                string fullPath = string.Empty;

                // 1. Check relative path in application base directory
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string candidateBase = Path.Combine(baseDir, relativePath);
                if (File.Exists(candidateBase))
                {
                    fullPath = candidateBase;
                }
                else if (File.Exists(relativePath))
                {
                    fullPath = Path.GetFullPath(relativePath);
                }
                else
                {
                    // 2. Fallback to AppData Images folder
                    string imagesFolder = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "AssemblyLineTester", "Images");
                    string candidateAppData = Path.Combine(imagesFolder, relativePath);
                    if (File.Exists(candidateAppData))
                    {
                        fullPath = candidateAppData;
                    }
                }

                if (!string.IsNullOrEmpty(fullPath) && File.Exists(fullPath))
                {
                    pic.SizeMode = PictureBoxSizeMode.Zoom;
                    pic.Image = Image.FromFile(fullPath);
                }
            }
            pic.Invalidate();
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
            if (ctrl is PictureBox pic && pic.Image != null) return; // real image already rendered by the control itself
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
            if (ctrl is PictureBox pic && pic.Image != null) return; // real image already rendered by the control itself
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