using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AssemblyLineTester.Models;
using AssemblyLineTester.Services;

namespace AssemblyLineTester.Views.Modals
{
    public partial class HarnessModalForm : Form
    {
        private const int MaxFileNameDisplayLength = 30;

        private readonly HarnessService _harnessService;
        private readonly ConnectorService _connectorService;
        private readonly ProductHarness? _editingHarness;
        private readonly List<string> _selectedImagePaths = new();
        private readonly List<Connector> _selectedConnectors = new();
        private ToolStripDropDown? _connectorDropdown;
        private CheckedListBox? _connectorCheckList;
        private TextBox? _connectorSearchBox;
        private List<Connector> _allConnectorsCache = new();

        public HarnessModalForm(HarnessService harnessService, ConnectorService connectorService)
        {
            _harnessService = harnessService;
            _connectorService = connectorService;
            _editingHarness = null;

            InitializeComponent();
            ConfigureModeUI(isEditMode: false);
            WireEvents();
            ApplyFormRounding();
        }

        public HarnessModalForm(HarnessService harnessService, ConnectorService connectorService, ProductHarness harnessToEdit)
        {
            _harnessService = harnessService;
            _connectorService = connectorService;
            _editingHarness = harnessToEdit;

            InitializeComponent();
            ConfigureModeUI(isEditMode: true);
            LoadHarnessForEdit(harnessToEdit);
            WireEvents();
            ApplyFormRounding();
        }

        private void ConfigureModeUI(bool isEditMode)
        {
            if (isEditMode)
            {
                lblCustomTitle.Text = "EDIT HARNESS";
                lblTitle.Text = "Edit Product Harness";
                btnSave.Text = "Save Changes";
            }
            else
            {
                lblCustomTitle.Text = "ADD HARNESS";
                lblTitle.Text = "Add New Harness";
                btnSave.Text = "Add Harness";
            }
        }

        private void LoadHarnessForEdit(ProductHarness h)
        {
            txtName.Text = h.Name;
            txtProductModel.Text = h.ProductModel;
            txtProductType.Text = h.ProductType;
            txtArea.Text = h.Area;

            _selectedConnectors.Clear();
            flpConnectorChips.Controls.Clear();
            if (h.ConnectorList != null)
            {
                foreach (var conn in h.ConnectorList)
                {
                    AddConnectorChip(conn);
                }
            }

            _selectedImagePaths.Clear();
            lstImages.Items.Clear();
            foreach (string path in h.ImagePaths)
            {
                _selectedImagePaths.Add(path);
                lstImages.Items.Add(TruncateFileName(Path.GetFileName(path)));
            }
        }

        private void WireEvents()
        {
            btnSave.Click += BtnSave_Click;
            btnChooseImage.Click += BtnChooseImage_Click;
            btnRemoveImage.Click += BtnRemoveImage_Click;

            // Clicking anywhere on the picker box (background or arrow) opens the multi-select
            // popup. Clicks on an individual chip's delete button don't bubble up to these, since
            // WinForms Click events aren't routed - so this is safe.
            pnlConnectorPicker.Click += (s, e) => OpenConnectorPicker();
            flpConnectorChips.Click += (s, e) => OpenConnectorPicker();
            lblConnectorArrow.Click += (s, e) => OpenConnectorPicker();

            btnClose.Click += (s, e) => this.Close();
            btnClose.MouseEnter += (s, e) => { btnClose.BackColor = Color.FromArgb(225, 29, 72); btnClose.IconColor = Color.White; };
            btnClose.MouseLeave += (s, e) => { btnClose.BackColor = Color.Transparent; btnClose.IconColor = Color.FromArgb(203, 213, 225); };

            pnlCustomHeader.MouseDown += TitleBar_MouseDown;
            iconLogo.MouseDown += TitleBar_MouseDown;
        }

        // Builds the popup checklist once and reuses it; the ToolStripDropDown itself persists,
        // only the CheckedListBox's Items get refreshed on every open (so newly added connectors
        // elsewhere in the app show up without reopening the form).
        private void EnsureConnectorPickerBuilt()
        {
            if (_connectorDropdown != null) return;

            int width = pnlConnectorPicker.Width;

            _connectorSearchBox = new TextBox
            {
                Width = width - 10,
                Font = AssemblyLineTester.Helpers.UIStyleHelper.Font9,
                PlaceholderText = "Search connector..."
            };
            _connectorSearchBox.TextChanged += ConnectorSearchBox_TextChanged;

            _connectorCheckList = new CheckedListBox
            {
                Width = width,
                CheckOnClick = true,
                Font = AssemblyLineTester.Helpers.UIStyleHelper.Font9,
                BorderStyle = BorderStyle.None,
                DisplayMember = "PartNumber",
                IntegralHeight = true // snaps height so the last visible row never gets cut in half
            };
            // Only 5 rows show at once; anything beyond that scrolls (built into CheckedListBox).
            _connectorCheckList.Height = (_connectorCheckList.ItemHeight * 5) + 4;

            var searchHost = new ToolStripControlHost(_connectorSearchBox)
            {
                AutoSize = false,
                Width = width,
                Height = _connectorSearchBox.Height + 10,
                Margin = new Padding(4, 4, 4, 2),
                Padding = new Padding(0)
            };

            var listHost = new ToolStripControlHost(_connectorCheckList)
            {
                AutoSize = false,
                Width = _connectorCheckList.Width,
                Height = _connectorCheckList.Height,
                Margin = new Padding(0, 0, 0, 4),
                Padding = new Padding(0)
            };

            _connectorDropdown = new ToolStripDropDown
            {
                AutoClose = true,
                DropShadowEnabled = true,
                Padding = new Padding(2)
            };
            _connectorDropdown.Items.Add(searchHost);
            _connectorDropdown.Items.Add(listHost);
        }

        // Opens the popup with every connector in the shared catalog, checkbox pre-ticked for
        // whatever's already attached to this harness. A connector can be attached to several
        // harnesses at once, so picking one here never affects any other harness.
        private void OpenConnectorPicker()
        {
            EnsureConnectorPickerBuilt();

            _allConnectorsCache = _connectorService.GetAllConnectors();

            _connectorSearchBox!.TextChanged -= ConnectorSearchBox_TextChanged;
            _connectorSearchBox.Text = string.Empty;
            _connectorSearchBox.TextChanged += ConnectorSearchBox_TextChanged;

            PopulateCheckList(string.Empty);

            _connectorDropdown!.Show(pnlConnectorPicker, new Point(0, pnlConnectorPicker.Height));
            _connectorSearchBox.Focus();
        }

        private void ConnectorSearchBox_TextChanged(object? sender, EventArgs e) => PopulateCheckList(_connectorSearchBox!.Text);

        // Filters the checklist by part number (case-insensitive). Runs on every keystroke in the
        // search box, and once with an empty term each time the popup opens.
        private void PopulateCheckList(string searchTerm)
        {
            _connectorCheckList!.ItemCheck -= ConnectorCheckList_ItemCheck;
            _connectorCheckList.Items.Clear();

            var filtered = string.IsNullOrWhiteSpace(searchTerm)
                ? _allConnectorsCache
                : _allConnectorsCache.Where(c => c.PartNumber.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();

            foreach (var conn in filtered)
            {
                int index = _connectorCheckList.Items.Add(conn);
                bool isChecked = _selectedConnectors.Any(c => c.ConnectorId == conn.ConnectorId);
                _connectorCheckList.SetItemChecked(index, isChecked);
            }

            _connectorCheckList.ItemCheck += ConnectorCheckList_ItemCheck;
        }

        private void ConnectorCheckList_ItemCheck(object? sender, ItemCheckEventArgs e)
        {
            if (_connectorCheckList!.Items[e.Index] is not Connector connector) return;

            // ItemCheck fires before the box's own state updates, so defer to after this call returns.
            BeginInvoke(new Action(() =>
            {
                if (e.NewValue == CheckState.Checked)
                    AddConnectorChip(connector);
                else
                    RemoveConnectorChip(connector.ConnectorId);
            }));
        }

        // Adds a removable "chip" (part number + delete button) inside pnlConnectorPicker.
        // Ignores connectors already picked.
        private void AddConnectorChip(Connector connector)
        {
            if (_selectedConnectors.Any(c => c.ConnectorId == connector.ConnectorId)) return;
            _selectedConnectors.Add(connector);

            var chip = new Panel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = Color.FromArgb(241, 245, 249),
                Margin = new Padding(0, 0, 6, 6),
                Padding = new Padding(8, 4, 4, 4),
                Tag = connector.ConnectorId
            };

            var flow = new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            var lbl = new Label
            {
                Text = connector.PartNumber,
                Font = AssemblyLineTester.Helpers.UIStyleHelper.Font9,
                ForeColor = Color.FromArgb(51, 65, 85),
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Margin = new Padding(0, 2, 6, 0)
            };

            var btnDelete = new Button
            {
                Text = "\u2715", // ✕
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                Size = new Size(18, 18),
                Cursor = Cursors.Hand,
                Margin = new Padding(0),
                TabStop = false
            };
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Click += (s, e) => RemoveConnectorChip(connector.ConnectorId);

            flow.Controls.Add(lbl);
            flow.Controls.Add(btnDelete);
            chip.Controls.Add(flow);

            flpConnectorChips.Controls.Add(chip);
        }

        // Removes a connector's chip and, if the picker popup is open/built, unchecks it there too
        // so both stay in sync.
        private void RemoveConnectorChip(int connectorId)
        {
            _selectedConnectors.RemoveAll(c => c.ConnectorId == connectorId);

            var chip = flpConnectorChips.Controls.OfType<Panel>().FirstOrDefault(p => p.Tag is int id && id == connectorId);
            if (chip != null)
            {
                flpConnectorChips.Controls.Remove(chip);
                chip.Dispose();
            }

            if (_connectorCheckList != null)
            {
                for (int i = 0; i < _connectorCheckList.Items.Count; i++)
                {
                    if (_connectorCheckList.Items[i] is Connector c && c.ConnectorId == connectorId)
                    {
                        _connectorCheckList.ItemCheck -= ConnectorCheckList_ItemCheck;
                        _connectorCheckList.SetItemChecked(i, false);
                        _connectorCheckList.ItemCheck += ConnectorCheckList_ItemCheck;
                        break;
                    }
                }
            }
        }

        private void BtnChooseImage_Click(object? sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png",
                Title = "Select Harness Images",
                Multiselect = true
            };

            if (ofd.ShowDialog() != DialogResult.OK) return;

            foreach (string fileName in ofd.FileNames)
            {
                var fileInfo = new FileInfo(fileName);
                if (fileInfo.Length > 2 * 1024 * 1024)
                {
                    MessageBox.Show($"'{fileInfo.Name}' exceeds the 2MB limit and was skipped.", "File Too Large", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    continue;
                }

                if (_selectedImagePaths.Contains(fileName)) continue;

                _selectedImagePaths.Add(fileName);
                lstImages.Items.Add(TruncateFileName(fileInfo.Name));
            }
        }

        private void BtnRemoveImage_Click(object? sender, EventArgs e)
        {
            if (lstImages.SelectedIndex < 0)
            {
                MessageBox.Show("Select an image in the list to remove.", "No Image Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int index = lstImages.SelectedIndex;
            _selectedImagePaths.RemoveAt(index);
            lstImages.Items.RemoveAt(index);
        }

        private static string TruncateFileName(string fileName)
        {
            return fileName.Length > MaxFileNameDisplayLength
                ? fileName.Substring(0, MaxFileNameDisplayLength) + "..."
                : fileName;
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter Harness Name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtProductModel.Text))
            {
                MessageBox.Show("Please enter Product Model.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProductModel.Focus();
                return;
            }

            // Order matters here: it's what HarnessService uses to number the connectors "J1", "J2", ...
            var selectedConnectors = new List<Connector>(_selectedConnectors);

            var harness = new ProductHarness
            {
                ProductHarnessId = _editingHarness?.ProductHarnessId ?? 0,
                Name = txtName.Text.Trim(),
                ProductModel = txtProductModel.Text.Trim(),
                ProductType = txtProductType.Text.Trim(),
                Area = txtArea.Text.Trim(),
                ImagePaths = new List<string>(_selectedImagePaths),
                LastUpdate = DateTime.UtcNow,
                ConnectorList = selectedConnectors
            };

            try
            {
                var (success, error) = _editingHarness == null
                    ? _harnessService.AddHarness(harness)
                    : _harnessService.UpdateHarness(harness);

                if (!success)
                {
                    MessageBox.Show(error ?? "Failed to save harness.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show($"Harness '{harness.Name}' saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving harness: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFormRounding()
        {
            if (Width <= 0 || Height <= 0) return;
            int radius = 24;
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            using var path = new System.Drawing.Drawing2D.GraphicsPath();
            int d = radius * 2;
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            this.Region = new Region(path);
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        private void TitleBar_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }
    }
}