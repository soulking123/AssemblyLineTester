using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AssemblyLineTester.Models;
using AssemblyLineTester.Services;

namespace AssemblyLineTester.Views.Modals
{
    public partial class ConnectorModalForm : Form
    {
        private readonly ConnectorService _connectorService;
        private readonly Connector? _editingConnector;

        private readonly System.Collections.Generic.List<ImageEntry> _imageEntries = new();
        private readonly System.Collections.Generic.List<string> _removedExistingFileNames = new();
        private readonly ToolTip _fileNameToolTip = new();

        private sealed class ImageEntry
        {
            public required string FileName;
            public string? SourcePath;
            public override string ToString() => TruncateFileName(FileName);
        }


        public ConnectorModalForm(ConnectorService connectorService)
        {
            _connectorService = connectorService;
            _editingConnector = null;

            InitializeComponent();
            ConfigureModeUI(isEditMode: false);
            WireEvents();
            ApplyFormRounding();
        }

        // Edit mode: prefill fields and existing images from the given connector. Editing modifies
        // the shared catalog entry, so it's visible from every harness that uses this connector.
        public ConnectorModalForm(ConnectorService connectorService, Connector connectorToEdit)
        {
            _connectorService = connectorService;
            _editingConnector = connectorToEdit;

            InitializeComponent();
            ConfigureModeUI(isEditMode: true);
            LoadConnectorForEdit(connectorToEdit);
            WireEvents();
            ApplyFormRounding();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                AssemblyLineTester.Helpers.UIStyleHelper.ScaleExplicitFonts(this, AssemblyLineTester.Helpers.UIStyleHelper.GetHostScaleFactor(this));
            }
        }

        private void ConfigureModeUI(bool isEditMode)
        {
            if (isEditMode)
            {
                lblCustomTitle.Text = "EDIT CONNECTOR";
                lblTitle.Text = "Edit Connector";
                btnSave.Text = "Save Changes";
            }
            else
            {
                lblCustomTitle.Text = "ADD CONNECTOR";
                lblTitle.Text = "Add New Connector";
                btnSave.Text = "Add Connector";
            }
        }

        private void WireEvents()
        {
            btnSave.Click += BtnSave_Click;

            btnClose.Click += (s, e) => this.Close();
            btnClose.MouseEnter += (s, e) => { btnClose.BackColor = Color.FromArgb(225, 29, 72); btnClose.IconColor = Color.White; };
            btnClose.MouseLeave += (s, e) => { btnClose.BackColor = Color.Transparent; btnClose.IconColor = Color.FromArgb(203, 213, 225); };

            btnChooseImage.Click += BtnChooseImage_Click;
            btnRemoveImage.Click += BtnRemoveImage_Click;
            lstImages.MouseMove += LstImages_MouseMove;

            pnlCustomHeader.MouseDown += TitleBar_MouseDown;
            iconLogo.MouseDown += TitleBar_MouseDown;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyFormRounding();
        }

        private void ApplyFormRounding()
        {
            if (Width <= 0 || Height <= 0) return;
            int radius = 24;
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            using (var path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                int d = radius * 2;
                path.StartFigure();
                path.AddArc(rect.X, rect.Y, d, d, 180, 90);
                path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
                path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
                path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
                path.CloseFigure();
                this.Region = new Region(path);
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        private void TitleBar_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                ActiveControl = pnlCustomHeader;
                ValidateChildren();

                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        // Prefills the form from an existing connector and loads its saved images into the list.
        private void LoadConnectorForEdit(Connector c)
        {
            txtPerf.Text = c.Perf;
            txtPartNumber.Text = c.PartNumber;
            txtContactType.Text = c.ContactType;
            txtContactQuantity.Text = c.ContactQuantity.ToString();
            txtWireSize.Text = c.WireSize;
            txtCurrentRating.Text = FormatCurrentRating(c.CurrentRating);
            txtMountingStyle.Text = c.MountingStyle;

            foreach (var path in _connectorService.GetConnectorImagePaths(c))
            {
                _imageEntries.Add(new ImageEntry { FileName = Path.GetFileName(path), SourcePath = null });
            }
            RefreshImageList();
        }

        private static string FormatCurrentRating(double amps)
        {
            string number = amps % 1 == 0 ? ((int)amps).ToString() : amps.ToString("0.#");
            return number + "A";
        }

        private void BtnChooseImage_Click(object? sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png",
                Title = "Select Connector Images",
                Multiselect = true
            };

            if (ofd.ShowDialog() != DialogResult.OK) return;

            foreach (var filePath in ofd.FileNames)
            {
                try
                {
                    var fileInfo = new FileInfo(filePath);
                    if (fileInfo.Length > 2 * 1024 * 1024)
                    {
                        MessageBox.Show($"'{fileInfo.Name}' is larger than 2MB and was skipped.", "File Too Large", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    if (_imageEntries.Any(i => i.SourcePath == filePath)) continue;

                    _imageEntries.Add(new ImageEntry { FileName = fileInfo.Name, SourcePath = filePath });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error reading image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            RefreshImageList();
        }

        private void BtnRemoveImage_Click(object? sender, EventArgs e)
        {
            if (lstImages.SelectedItem is not ImageEntry entry) return;

            _imageEntries.Remove(entry);
            if (entry.SourcePath == null)
                _removedExistingFileNames.Add(entry.FileName);

            RefreshImageList();
        }

        private void RefreshImageList()
        {
            lstImages.Items.Clear();
            foreach (var entry in _imageEntries) lstImages.Items.Add(entry);

            lblImage.Text = _imageEntries.Count == 0
                ? "Images (multiple allowed, max 2MB each, JPG/PNG)"
                : $"Images ({_imageEntries.Count} selected)";

            if (_imageEntries.Count > 0)
            {
                lstImages.SelectedIndex = 0;
            }
        }

        private void LstImages_MouseMove(object? sender, MouseEventArgs e)
        {
            int index = lstImages.IndexFromPoint(e.Location);
            if (index < 0 || index >= _imageEntries.Count) return;

            string full = _imageEntries[index].FileName;
            if (_fileNameToolTip.GetToolTip(lstImages) != full)
                _fileNameToolTip.SetToolTip(lstImages, full);
        }

        private static string TruncateFileName(string fileName, int maxLength = 35)
        {
            if (fileName.Length <= maxLength) return fileName;

            string ext = Path.GetExtension(fileName);
            string nameOnly = Path.GetFileNameWithoutExtension(fileName);
            int keep = Math.Max(maxLength - ext.Length - 3, 3);
            if (keep >= nameOnly.Length) return fileName;

            return nameOnly.Substring(0, keep) + "..." + ext;
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPerf.Text))
            {
                MessageBox.Show("Please enter Perf.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPerf.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPartNumber.Text))
            {
                MessageBox.Show("Please enter a Part Number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPartNumber.Focus();
                return;
            }

            int.TryParse(txtContactQuantity.Text.Trim(), out int contactQty);

            var connector = new Connector
            {
                ConnectorId = _editingConnector?.ConnectorId ?? 0,
                Perf = txtPerf.Text.Trim(),
                PartNumber = txtPartNumber.Text.Trim(),
                ContactType = txtContactType.Text.Trim(),
                ContactQuantity = contactQty,
                WireSize = txtWireSize.Text.Trim(),
                CurrentRating = ParseCurrentRating(txtCurrentRating.Text),
                MountingStyle = txtMountingStyle.Text.Trim()
            };

            var newImagePaths = _imageEntries.Where(i => i.SourcePath != null).Select(i => i.SourcePath!).ToList();

            try
            {
                if (_editingConnector == null)
                {
                    var (saved, error) = _connectorService.AddConnector(connector, newImagePaths);
                    if (saved == null)
                    {
                        MessageBox.Show(error ?? "Failed to save connector.", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    var (success, error) = _connectorService.UpdateConnector(connector, newImagePaths, _removedExistingFileNames);
                    if (!success)
                    {
                        MessageBox.Show(error ?? "Failed to save connector.", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while saving connector: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show($"Connector '{connector.PartNumber}' has been successfully saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private static double ParseCurrentRating(string text)
        {
            string digitsOnly = new string(text.Where(ch => char.IsDigit(ch) || ch == '.').ToArray());
            return double.TryParse(digitsOnly, out double value) ? value : 0;
        }
    }
}