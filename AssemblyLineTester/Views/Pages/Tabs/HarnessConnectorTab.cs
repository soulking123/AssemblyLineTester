using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AssemblyLineTester.Models;
using AssemblyLineTester.Services;

namespace AssemblyLineTester.Views.Pages.Tabs
{
    public partial class HarnessConnectorTab : UserControl
    {
        private ConnectorService? _connectorService;
        private Dictionary<int, Connector> _connectorCache = new();
        private List<string> _connectorImagePaths = new();
        private int _connectorImageIndex = 0;
        private List<Connector> _allConnectorsForHarness = new();
        private List<Connector> _filteredConnectorsForHarness = new();
        private Connector? _selectedConnector;

        public HarnessConnectorTab()
        {
            InitializeComponent();
            picConnectorImage.Paint += PicConnectorImage_Paint;
            picConnectorImage.MouseClick += PicConnectorImage_MouseClick;
        }

        public void Initialize(ConnectorService connectorService)
        {
            _connectorService = connectorService;
            InitializeConnectorGrid();
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

            RefreshConnectorGrid();
        }

        public void RefreshConnectorGrid()
        {
            if (_connectorService == null) return;
            _allConnectorsForHarness = _connectorService.GetAllConnectors();
            txtConnectorSearch.Text = string.Empty;
            ApplyConnectorFilter();
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

        private bool HasConnectorEditPermission()
        {
            if (!SessionManager.HasPermission("Action.Database.Backup") && SessionManager.CurrentUser?.Role?.Name != "Administrator")
            {
                MessageBox.Show("You do not have permission to modify connector records.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void BtnAddConnector_Click(object? sender, EventArgs e)
        {
            if (!HasConnectorEditPermission()) return;
            using var modal = new AssemblyLineTester.Views.Modals.ConnectorModalForm(_connectorService!);
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

            var (success, error) = _connectorService!.DeleteConnector(connectorId);
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
            tblConnectorInfo.Controls.Clear();
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

        private void SetConnectorGalleryImages(Connector? connector)
        {
            if (_connectorService == null) return;
            _connectorImagePaths = _connectorService.GetConnectorImagePaths(connector);
            _connectorImageIndex = 0;
            LoadGalleryImage(picConnectorImage, _connectorImagePaths, _connectorImageIndex);
        }

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
                    FileLogService.Warn("HarnessConnectorTab.ShowImage", $"Image '{paths[index]}' could not be loaded: {ex.Message}");
                }
            }

            pic.Invalidate();
        }

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
            g.DrawLines(arrowPen, new Point[] { new Point(leftX + arrowSize, cy - arrowSize), new Point(leftX, cy), new Point(leftX + arrowSize, cy + arrowSize) });

            int rightX = rect.Width - 20 - arrowSize;
            g.DrawLines(arrowPen, new Point[] { new Point(rightX, cy - arrowSize), new Point(rightX + arrowSize, cy), new Point(rightX, cy + arrowSize) });

            string counter = $"{index + 1} / {count}";
            var counterFont = AssemblyLineTester.Helpers.UIStyleHelper.Font9Bold;
            var counterSize = g.MeasureString(counter, counterFont);
            var counterRect = new RectangleF(rect.Width - counterSize.Width - 22, rect.Height - counterSize.Height - 16, counterSize.Width + 12, counterSize.Height + 6);
            using var counterBg = new SolidBrush(Color.FromArgb(160, 15, 23, 42));
            g.FillRectangle(counterBg, counterRect);
            g.DrawString(counter, counterFont, Brushes.White, counterRect.X + 6, counterRect.Y + 3);
        }

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
    }
}
