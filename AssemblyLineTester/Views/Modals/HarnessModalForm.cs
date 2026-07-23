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
        private readonly HarnessService _harnessService;
        private readonly ConnectorService _connectorService;
        private readonly ProductHarness? _editingHarness;
        private string? _selectedImagePath;

        // Constructor Mode Add
        public HarnessModalForm(HarnessService harnessService, ConnectorService connectorService)
        {
            _harnessService = harnessService;
            _connectorService = connectorService;
            _editingHarness = null;

            InitializeComponent();
            ConfigureModeUI(isEditMode: false);
            LoadConnectorsDropdown();
            WireEvents();
            ApplyFormRounding();
        }

        // Constructor Mode Edit
        public HarnessModalForm(HarnessService harnessService, ConnectorService connectorService, ProductHarness harnessToEdit)
        {
            _harnessService = harnessService;
            _connectorService = connectorService;
            _editingHarness = harnessToEdit;

            InitializeComponent();
            ConfigureModeUI(isEditMode: true);
            LoadConnectorsDropdown();
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

        private void LoadConnectorsDropdown()
        {
            lstConnectors.Items.Clear();

            // Mendapatkan data connector dari ConnectorService.
            // Jika method di ConnectorService Anda bernama lain (misal GetConnectors() atau GetAll()), 
            // ganti _connectorService.GetAllConnectors() dengan method tersebut.
            var connectors = _connectorService.GetAllConnectors();

            foreach (var conn in connectors)
            {
                // Menambahkan object connector ke CheckedListBox
                lstConnectors.Items.Add(conn);
            }

            // Tampilkan nomor Part / Name di list
            lstConnectors.DisplayMember = "PartNumber";
        }

        private void LoadHarnessForEdit(ProductHarness h)
        {
            txtName.Text = h.Name;
            txtProductModel.Text = h.ProductModel;
            txtProductType.Text = h.ProductType;
            txtArea.Text = h.Area;
            _selectedImagePath = h.ImagePath;

            if (!string.IsNullOrEmpty(_selectedImagePath))
            {
                lblImageStatus.Text = Path.GetFileName(_selectedImagePath);
            }

            // Centang connector yang sudah terpilih sebelumnya (Multiple Selection)
            for (int i = 0; i < lstConnectors.Items.Count; i++)
            {
                if (lstConnectors.Items[i] is Connector conn)
                {
                    bool isSelected = h.ConnectorList != null && h.ConnectorList.Any(c => c.ConnectorId == conn.ConnectorId);
                    if (isSelected)
                    {
                        lstConnectors.SetItemChecked(i, true);
                    }
                }
            }
        }

        private void WireEvents()
        {
            btnSave.Click += BtnSave_Click;
            btnChooseImage.Click += BtnChooseImage_Click;

            btnClose.Click += (s, e) => this.Close();
            btnClose.MouseEnter += (s, e) => { btnClose.BackColor = Color.FromArgb(225, 29, 72); btnClose.IconColor = Color.White; };
            btnClose.MouseLeave += (s, e) => { btnClose.BackColor = Color.Transparent; btnClose.IconColor = Color.FromArgb(203, 213, 225); };

            pnlCustomHeader.MouseDown += TitleBar_MouseDown;
            iconLogo.MouseDown += TitleBar_MouseDown;
        }

        private void BtnChooseImage_Click(object? sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png",
                Title = "Select Harness Image"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var fileInfo = new FileInfo(ofd.FileName);
                if (fileInfo.Length > 2 * 1024 * 1024)
                {
                    MessageBox.Show("Selected image exceeds 2MB limit.", "File Too Large", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                _selectedImagePath = ofd.FileName;
                lblImageStatus.Text = fileInfo.Name;
            }
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

            // Mengambil SELURUH connector yang dicentang (Multiple selection support)
            var selectedConnectors = lstConnectors.CheckedItems.OfType<Connector>().ToList();

            var harness = new ProductHarness
            {
                ProductHarnessId = _editingHarness?.ProductHarnessId ?? 0,
                Name = txtName.Text.Trim(),
                ProductModel = txtProductModel.Text.Trim(),
                ProductType = txtProductType.Text.Trim(),
                Area = txtArea.Text.Trim(),
                ImagePath = _selectedImagePath,
                LastUpdate = DateTime.UtcNow,
                ConnectorList = selectedConnectors,
                ConnectorListName = selectedConnectors.Select(c => c.PartNumber).ToList()
            };

            try
            {
                // Panggil method add/update sesuai service Anda
                if (_editingHarness == null)
                {
                    _harnessService.AddHarness(harness);
                }
                else
                {
                    _harnessService.UpdateHarness(harness);
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