using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AssemblyLineTester.Models;
using AssemblyLineTester.Services;
using AssemblyLineTester.Helpers;

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

        private readonly HarnessService _harnessService = new();

        private readonly ConnectorService _connectorService = new();
        private readonly Dictionary<int, Connector> _connectorCache = new();
        private List<string> _connectorImagePaths = [];
        private int _connectorImageIndex = 0;
        private List<Connector> _allConnectorsForHarness = [];
        private List<Connector> _filteredConnectorsForHarness = [];
        private Label? _lblConnPartNumber, _lblConnContactType, _lblConnContactQty, _lblConnWireSize, _lblConnCurrentRating, _lblConnMountingStyle;
        private Label? _lblPageInfo;
        private Image? _customHarnessImage = null;
        private Image? _customMachineImage = null;

        private List<string> _harnessImagePaths = [];
        private int _harnessImageIndex = 0;
        private List<string> _genConnectorImagePaths = [];
        private int _genConnectorImageIndex = 0;

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
            dataGridViewList.CellClick += DataGridViewList_CellClick;
            this.Load += HarnessDatabase_Load;
            picHarnessImage.MouseClick += PicHarnessImage_MouseClick;
            picMachineArea.MouseClick += PicMachineArea_MouseClick;
            InitializeCustomStyles();
            SetupEvents();
            SetupPagination();
            InitializeConnectorGrid();
            PopulateHarnessData();

            DoubleBuffered = true;
        }

        // Loads the clicked harness into the General tab.
        private void DataGridViewList_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dataGridViewList.Rows[e.RowIndex].Tag is not int harnessId) return;

            var harness = _allHarnessEntities.FirstOrDefault(h => h.ProductHarnessId == harnessId);
            if (harness != null)
            {
                RenderHarnessSelected(harness);
                tabControlDetail.SelectedIndex = 0;
            }
        }

        private void HarnessDatabase_Load(object? sender, EventArgs e)
        {
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
                if (!HasConnectorEditPermission()) return;

                using var modal = new AssemblyLineTester.Views.Modals.HarnessModalForm(_harnessService, _connectorService);
                if (modal.ShowDialog(this.FindForm()) == DialogResult.OK)
                {
                    PopulateHarnessData();
                    var created = _allHarnessEntities.OrderByDescending(h => h.ProductHarnessId).FirstOrDefault();
                    if (created != null)
                    {
                        RenderHarnessSelected(created);
                        tabControlDetail.SelectedIndex = 0;
                    }
                }
            };

            btnEditGeneral.Click += (s, e) =>
            {
                if (!HasConnectorEditPermission()) return;

                if (_selectedHarness == null)
                {
                    MessageBox.Show("Please select a harness from the list first.", "No Harness Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using var modal = new AssemblyLineTester.Views.Modals.HarnessModalForm(_harnessService, _connectorService, _selectedHarness);
                if (modal.ShowDialog(this.FindForm()) == DialogResult.OK)
                {
                    int editedId = _selectedHarness.ProductHarnessId;
                    PopulateHarnessData();
                    var refreshed = _allHarnessEntities.FirstOrDefault(h => h.ProductHarnessId == editedId);
                    if (refreshed != null) RenderHarnessSelected(refreshed);
                }
            };

            btnDeleteHarness.Click += (s, e) =>
            {
                if (!HasConnectorEditPermission()) return;

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
            };
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

        // Applies the four filter dropdowns and reloads the grid from page 1.
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
                ShowNoHarnessSelectedState();
                PopulateGrid();
            }
            finally
            {
                ResumeLayout(true);
            }
        }

        private void InitializeConnectorGrid()
        {
            dgvConnectors.Columns.Clear();
            dgvConnectors.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPerf", HeaderText = "Perf", FillWeight = 50, Visible = false });
            dgvConnectors.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPartNumber", HeaderText = "Part Number", FillWeight = 100 });
            dgvConnectors.Columns.Add(new DataGridViewTextBoxColumn { Name = "colContactType", HeaderText = "Type", FillWeight = 70 });
            dgvConnectors.Columns.Add(new DataGridViewTextBoxColumn { Name = "colContactQuantity", HeaderText = "Qty", FillWeight = 60 });
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

            dgvConnectors.SelectionChanged += DgvConnectors_SelectionChanged;

            btnAddConnector.Click += BtnAddConnector_Click;
            btnEditConnector.Click += BtnEditConnector_Click;
            btnDeleteConnector.Click += BtnDeleteConnector_Click;
            btnConnectorSearch.Click += (s, e) => ApplyConnectorFilter();
            txtConnectorSearch.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; ApplyConnectorFilter(); } };

            txtConnectorSearch.PlaceholderText = "Search by part number, type, wire size, or mounting...";
            txtConnectorSearch.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font9;
            btnConnectorSearch.Text = "Search";
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnConnectorSearch, 8, Color.FromArgb(250, 204, 21), Color.FromArgb(254, 216, 53), Color.FromArgb(234, 186, 10), Color.Transparent);

            picConnectorImage.Paint += PicConnectorImage_Paint;
            picConnectorImage.MouseClick += PicConnectorImage_MouseClick;

            RefreshConnectorGrid();
        }

        private void ApplyConnectorFilter()
        {
            string query = txtConnectorSearch.Text.Trim().ToLowerInvariant();
            _filteredConnectorsForHarness = string.IsNullOrEmpty(query)
                ? new List<Connector>(_allConnectorsForHarness)
                : _allConnectorsForHarness.Where(c =>
                    c.PartNumber.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    c.ContactType.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    c.WireSize.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    c.MountingStyle.Contains(query, StringComparison.OrdinalIgnoreCase)
                  ).ToList();
            RefreshConnectorGridFiltered();
        }

        private void RefreshConnectorGridFiltered()
        {
            dgvConnectors.Rows.Clear();
            _connectorCache.Clear();

            var display = _filteredConnectorsForHarness;
            foreach (var c in display)
            {
                int rowIndex = dgvConnectors.Rows.Add(c.Perf, c.PartNumber, c.ContactType, c.ContactQuantity, c.WireSize, $"{c.CurrentRating} A", c.MountingStyle);
                dgvConnectors.Rows[rowIndex].Tag = c.ConnectorId;
                _connectorCache[c.ConnectorId] = c;
            }

            string query = txtConnectorSearch.Text.Trim();
            string status = string.IsNullOrEmpty(query)
                ? $"Showing all {display.Count} connectors"
                : $"Found {display.Count} of {_allConnectorsForHarness.Count} connectors";

            btnConnectorSearch.Text = display.Count > 0 ? $"Search ({display.Count})" : "Search (0)";

            if (dgvConnectors.Rows.Count > 0)
            {
                dgvConnectors.ClearSelection();
                dgvConnectors.Rows[0].Selected = true;
                DgvConnectors_SelectionChanged(dgvConnectors, EventArgs.Empty);
            }
            else
            {
                PopulateConnectorInfo(null);
                SetConnectorGalleryImages(null);
            }
        }

        // Reloads the whole connector catalogue, independent of the selected harness.
        private void RefreshConnectorGrid()
        {
            _allConnectorsForHarness = _connectorService.GetAllConnectors();
            txtConnectorSearch.Text = string.Empty;
            ApplyConnectorFilter();
        }

        // The catalogue is harness specification data, so it is gated on Action.TestConfig.Edit.
        private bool HasConnectorEditPermission()
        {
            if (!SessionManager.HasPermission("Action.TestConfig.Edit"))
            {
                MessageBox.Show("You do not have permission to modify connector records.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void BtnAddConnector_Click(object? sender, EventArgs e)
        {
            if (!HasConnectorEditPermission()) return;

            using var modal = new AssemblyLineTester.Views.Modals.ConnectorModalForm(_connectorService);
            if (modal.ShowDialog(this.FindForm()) == DialogResult.OK)
            {
                RefreshConnectorGrid();
            }
        }

        private void BtnEditConnector_Click(object? sender, EventArgs e)
        {
            if (!HasConnectorEditPermission()) return;

            if (dgvConnectors.SelectedRows.Count == 0 ||
                dgvConnectors.SelectedRows[0].Tag is not int connectorId ||
                !_connectorCache.TryGetValue(connectorId, out var connector))
            {
                MessageBox.Show("Please select a connector to edit.", "No Connector Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var modal = new AssemblyLineTester.Views.Modals.ConnectorModalForm(_connectorService, connector);
            if (modal.ShowDialog(this.FindForm()) == DialogResult.OK)
            {
                RefreshConnectorGrid();
            }
        }

        private void BtnDeleteConnector_Click(object? sender, EventArgs e)
        {
            if (!HasConnectorEditPermission()) return;

            if (dgvConnectors.SelectedRows.Count == 0 || dgvConnectors.SelectedRows[0].Tag is not int connectorId)
            {
                MessageBox.Show("Please select a connector to delete.", "No Connector Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string partNumber = dgvConnectors.SelectedRows[0].Cells["colPartNumber"].Value?.ToString() ?? "this connector";
            var confirm = MessageBox.Show(
                $"Delete connector '{partNumber}'? It will be removed from every harness that uses it, and its saved images will also be removed. This action cannot be undone.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            var (success, error) = _connectorService.DeleteConnector(connectorId);
            if (!success)
            {
                MessageBox.Show(error ?? "Failed to delete connector.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            RefreshConnectorGrid();
        }

        private void DgvConnectors_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvConnectors.SelectedRows.Count > 0)
            {
                var row = dgvConnectors.SelectedRows[0];
                PopulateConnectorInfo(row);

                if (row.Tag is int connectorId && _connectorCache.TryGetValue(connectorId, out var conn))
                {
                    _selectedConnector = conn;
                    SetConnectorGalleryImages(conn);
                }
                else
                {
                    _selectedConnector = null;
                    SetConnectorGalleryImages(null);
                }
            }
            else
            {
                _selectedConnector = null;
                PopulateConnectorInfo(null);
                SetConnectorGalleryImages(null);
            }
        }

        private void PopulateConnectorInfo(DataGridViewRow? row)
        {
            UIStyleHelper.ClearAndDispose(tblConnectorInfo.Controls);
            if (row == null) return;

            var data = new (string Key, string Val)[]
            {
                ("Perf", row.Cells["colPerf"].Value?.ToString() ?? ""),
                ("Part Number", row.Cells["colPartNumber"].Value?.ToString() ?? ""),
                ("Contact Type", row.Cells["colContactType"].Value?.ToString() ?? ""),
                ("Contact Quantity", row.Cells["colContactQuantity"].Value?.ToString() ?? ""),
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

        // Loads a connector's images and shows the first one.
        private void SetConnectorGalleryImages(Connector? connector)
        {
            _connectorImagePaths = _connectorService.GetConnectorImagePaths(connector);
            _connectorImageIndex = 0;
            LoadGalleryImage(picConnectorImage, _connectorImagePaths, _connectorImageIndex);
        }

        // Shared by all three image galleries on this page.
        private static void LoadGalleryImage(PictureBox pic, List<string> paths, int index)
        {
            var oldImage = pic.Image;
            pic.Image = null;
            oldImage?.Dispose();

            if (paths.Count > 0 && index >= 0 && index < paths.Count)
            {
                try
                {
                    pic.SizeMode = PictureBoxSizeMode.Zoom;
                    pic.Image = Image.FromFile(paths[index]);
                }
                catch (Exception ex)
                {
                    FileLogService.Warn("HarnessDatabase.ShowImage",
                        $"Image '{paths[index]}' could not be loaded: {ex.Message}");
                }
            }

            pic.Invalidate();
        }

        // Advances the gallery index when the click lands on an arrow; false if it did not.
        private static bool HandleGalleryClick(Point location, int controlWidth, int count, ref int index)
        {
            const int arrowZoneWidth = 50;
            if (location.X <= arrowZoneWidth)
            {
                index = (index - 1 + count) % count;
                return true;
            }
            if (location.X >= controlWidth - arrowZoneWidth)
            {
                index = (index + 1) % count;
                return true;
            }
            return false;
        }

        // Draws paging arrows and a counter badge when a gallery holds more than one image.
        private static void DrawGalleryUI(Graphics g, Rectangle rect, int index, int count)
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

            string counter = $"{index + 1} / {count}";
            var counterFont = AssemblyLineTester.Helpers.UIStyleHelper.Font9Bold;
            var counterSize = g.MeasureString(counter, counterFont);
            var counterRect = new RectangleF(rect.Width - counterSize.Width - 22, rect.Height - counterSize.Height - 16, counterSize.Width + 12, counterSize.Height + 6);
            using var counterBg = new SolidBrush(Color.FromArgb(160, 15, 23, 42));
            g.FillRectangle(counterBg, counterRect);
            g.DrawString(counter, counterFont, Brushes.White, counterRect.X + 6, counterRect.Y + 3);
        }

        // Placeholder when there is no image, paging arrows when there is more than one.
        private void PicConnectorImage_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Control ctrl || e.Graphics == null) return;
            var rect = ctrl.ClientRectangle;
            if (rect.Width <= 1 || rect.Height <= 1) return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            if (picConnectorImage.Image == null)
            {
                var font = AssemblyLineTester.Helpers.UIStyleHelper.Font10Bold;
                using var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                string message = _selectedConnector == null ? "Select a connector to view its images." : "No images uploaded for this connector.";
                e.Graphics.DrawString(message, font, Brushes.Gray, rect, sf);
                return;
            }

            if (_connectorImagePaths.Count > 1)
                DrawGalleryUI(e.Graphics, rect, _connectorImageIndex, _connectorImagePaths.Count);
        }

        private void PicConnectorImage_MouseClick(object? sender, MouseEventArgs e)
        {
            if (_connectorImagePaths.Count <= 1) return;
            if (!HandleGalleryClick(e.Location, picConnectorImage.Width, _connectorImagePaths.Count, ref _connectorImageIndex)) return;

            LoadGalleryImage(picConnectorImage, _connectorImagePaths, _connectorImageIndex);
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
                var harnessService = new HarnessService();
                _allHarnessEntities = harnessService.GetAllHarnesses();

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

        // Shown before any harness has been picked.
        private void ShowNoHarnessSelectedState()
        {
            _selectedHarness = null;
            _selectedConnector = null;

            UIStyleHelper.ClearAndDispose(tblGenInfo.Controls);
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

            SetHarnessImage([]);
            SetConnectorImage([]);
            RefreshConnectorGrid();
        }

        // Fills the General tab; connector fields stay blank until one is picked from the dropdown.
        private void RenderHarnessSelected(ProductHarness harness)
        {
            _selectedHarness = harness;
            _selectedConnector = null;
            _genInfoRowIndex = 0;

            UIStyleHelper.ClearAndDispose(tblGenInfo.Controls);
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

            SetHarnessImage(harness.ImagePaths);
            SetConnectorImage([]);
            RefreshConnectorGrid();
        }

        // Adds a key/value row to tblGenInfo and returns the value Label for later updates.
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

        // Names and connectors are built together in the same order, so zipping by index is safe.
        private sealed record ConnectorOption(string Label, Connector Connector)
        {
            public override string ToString() => Label;
        }

        // Dropdown lists attach positions "J1", "J2", ... rather than part numbers.
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

            if (connectors.Count == 0)
            {
                cmb.Items.Add("No connector found for this harness");
                cmb.SelectedIndex = 0;
                cmb.Enabled = false;
            }
            else
            {
                for (int i = 0; i < connectors.Count; i++)
                    cmb.Items.Add(new ConnectorOption(connectorNames[i], connectors[i]));

                cmb.SelectedIndexChanged += (s, e) =>
                {
                    if (s is ComboBox combo && combo.SelectedItem is ConnectorOption opt)
                        OnConnectorSelected(opt.Connector);
                };
            }

            tblGenInfo.Controls.Add(cmb, 1, _genInfoRowIndex);
            _genInfoRowIndex++;
        }

        // Fills the connector fields and image for the picked dropdown entry.
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
                SetConnectorImage(_connectorService.GetConnectorImagePaths(conn));
            }
            else
            {
                _lblConnPartNumber!.Text = "-";
                _lblConnContactType!.Text = "-";
                _lblConnContactQty!.Text = "-";
                _lblConnWireSize!.Text = "-";
                _lblConnCurrentRating!.Text = "-";
                _lblConnMountingStyle!.Text = "-";
                SetConnectorImage([]);
            }
        }

        // Top gallery: the harness's own pictures.
        private void SetHarnessImage(List<string> relativePaths)
        {
            _harnessImagePaths = relativePaths.Select(ResolveHarnessImagePath).Where(p => p != null).Select(p => p!).ToList();
            _harnessImageIndex = 0;
            LoadGalleryImage(picHarnessImage, _harnessImagePaths, _harnessImageIndex);
        }

        // Bottom gallery: the selected connector's pictures.
        private void SetConnectorImage(List<string> fullPaths)
        {
            _genConnectorImagePaths = fullPaths;
            _genConnectorImageIndex = 0;
            LoadGalleryImage(picMachineArea, _genConnectorImagePaths, _genConnectorImageIndex);
        }

        // Tries the app base dir, the current dir, then AppData Images. Null when nowhere.
        private static string? ResolveHarnessImagePath(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath)) return null;

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string candidateBase = Path.Combine(baseDir, relativePath);
            if (File.Exists(candidateBase)) return candidateBase;

            if (File.Exists(relativePath)) return Path.GetFullPath(relativePath);

            string imagesFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "AssemblyLineTester", "Images");
            string candidateAppData = Path.Combine(imagesFolder, relativePath);
            return File.Exists(candidateAppData) ? candidateAppData : null;
        }

        private void PopulateBottomCards()
        {
            UIStyleHelper.ClearAndDispose(tblSpecsContent.Controls);
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

        private void PicHarnessImage_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Control ctrl || e.Graphics == null) return;
            if (ctrl is PictureBox pic && pic.Image != null)
            {
                if (_harnessImagePaths.Count > 1)
                    DrawGalleryUI(e.Graphics, ctrl.ClientRectangle, _harnessImageIndex, _harnessImagePaths.Count);
                return;
            }
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
            if (ctrl is PictureBox pic && pic.Image != null)
            {
                if (_genConnectorImagePaths.Count > 1)
                    DrawGalleryUI(e.Graphics, ctrl.ClientRectangle, _genConnectorImageIndex, _genConnectorImagePaths.Count);
                return;
            }
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

            if (picHarnessImage.Image != null)
            {
                if (_harnessImagePaths.Count <= 1) return;
                if (!HandleGalleryClick(e.Location, ctrl.Width, _harnessImagePaths.Count, ref _harnessImageIndex)) return;
                LoadGalleryImage(picHarnessImage, _harnessImagePaths, _harnessImageIndex);
                return;
            }

            HandleCarouselClick(e.Location, ctrl.Width, ref _currentHarnessSlide);
            ctrl.Invalidate();
        }

        private void PicMachineArea_MouseClick(object? sender, MouseEventArgs e)
        {
            if (sender is not Control ctrl) return;

            if (picMachineArea.Image != null)
            {
                if (_genConnectorImagePaths.Count <= 1) return;
                if (!HandleGalleryClick(e.Location, ctrl.Width, _genConnectorImagePaths.Count, ref _genConnectorImageIndex)) return;
                LoadGalleryImage(picMachineArea, _genConnectorImagePaths, _genConnectorImageIndex);
                return;
            }

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