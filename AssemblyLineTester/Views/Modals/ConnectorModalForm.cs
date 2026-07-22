using System;
using System.Drawing;
using System.Windows.Forms;

namespace AssemblyLineTester.Views.Modals
{
    public partial class ConnectorModalForm : Form
    {
        public ConnectorModalForm(bool isEditMode = false)
        {
            InitializeComponent();
            InitializeDropdowns();
            
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

            btnSave.Click += BtnSave_Click;

            btnClose.Click += (s, e) => this.Close();
            btnClose.MouseEnter += (s, e) => { btnClose.BackColor = Color.FromArgb(225, 29, 72); btnClose.IconColor = Color.White; };
            btnClose.MouseLeave += (s, e) => { btnClose.BackColor = Color.Transparent; btnClose.IconColor = Color.FromArgb(203, 213, 225); };

            btnChooseImage.Click += BtnChooseImage_Click;

            pnlCustomHeader.MouseDown += TitleBar_MouseDown;
            iconLogo.MouseDown += TitleBar_MouseDown;

            ApplyFormRounding();
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

        private void InitializeDropdowns()
        {
            cmbContactType.Items.Clear();
            cmbContactType.Items.AddRange(new string[] { "Pin", "Socket", "Blade", "Receptacle" });
            cmbContactType.SelectedIndex = 0;

            cmbContactQuality.Items.Clear();
            cmbContactQuality.Items.AddRange(new string[] { "Standard", "Premium", "Gold Plated", "Silver Plated" });
            cmbContactQuality.SelectedIndex = 0;

            cmbWireSize.Items.Clear();
            cmbWireSize.Items.AddRange(new string[] { "0.50 mm²", "0.75 mm²", "1.00 mm²", "1.50 mm²", "2.00 mm²", "4.00 mm²" });
            cmbWireSize.SelectedIndex = 0;

            cmbCurrentRating.Items.Clear();
            cmbCurrentRating.Items.AddRange(new string[] { "5A", "10A", "15A", "20A", "30A", "50A" });
            cmbCurrentRating.SelectedIndex = 1;

            cmbMountingStyle.Items.Clear();
            cmbMountingStyle.Items.AddRange(new string[] { "Panel Mount", "Cable Mount", "PCB Mount", "Surface Mount" });
            cmbMountingStyle.SelectedIndex = 0;
        }

        private void BtnChooseImage_Click(object? sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png";
                ofd.Title = "Select Connector Image";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var fileInfo = new System.IO.FileInfo(ofd.FileName);
                        if (fileInfo.Length > 2 * 1024 * 1024)
                        {
                            MessageBox.Show("Image size must be less than 2MB.", "File Too Large", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        picPreview.Image = Image.FromFile(ofd.FileName);
                        lblImagePath.Text = ofd.FileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
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

            // Perform Save Action...
            MessageBox.Show($"Connector '{txtPartNumber.Text}' has been successfully saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
