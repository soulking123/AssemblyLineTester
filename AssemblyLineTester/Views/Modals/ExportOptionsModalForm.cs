using System;
using System.Drawing;
using System.Windows.Forms;

namespace AssemblyLineTester.Views.Modals
{
    public partial class ExportOptionsModalForm : Form
    {
        public bool ExportToDatabase => chkDatabase.Checked;
        public bool ExportToExcel => chkExcel.Checked;

        public ExportOptionsModalForm()
        {
            InitializeComponent();
            
            btnCancel.Click += (s, e) => {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };
            btnProceed.Click += BtnProceed_Click;

            chkDatabase.CheckedChanged += Option_CheckedChanged;
            chkExcel.CheckedChanged += Option_CheckedChanged;

            pnlCustomHeader.MouseDown += TitleBar_MouseDown;
            iconLogo.MouseDown += TitleBar_MouseDown;
            lblTitle.MouseDown += TitleBar_MouseDown;

            ApplyFormRounding();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                AssemblyLineTester.Helpers.UIStyleHelper.ScaleExplicitFonts(this, AssemblyLineTester.Helpers.UIStyleHelper.GetHostScaleFactor(this));
                FitHeightToOptions();
            }
        }

        private void FitHeightToOptions()
        {
            int wanted = Math.Min(flowOptions.PreferredSize.Height, pnlContent.Height);
            ClientSize = new Size(ClientSize.Width, pnlCustomHeader.Height + wanted + pnlBottom.Height);
        }

        private void Option_CheckedChanged(object? sender, EventArgs e)
        {
            btnProceed.Enabled = chkDatabase.Checked || chkExcel.Checked;
            if (btnProceed.Enabled)
            {
                btnProceed.BackColor = Color.FromArgb(14, 165, 233);
                btnProceed.ForeColor = Color.Black;
            }
            else
            {
                btnProceed.BackColor = Color.FromArgb(148, 163, 184); // Disabled color
                btnProceed.ForeColor = Color.FromArgb(241, 245, 249);
            }
        }

        private void BtnProceed_Click(object? sender, EventArgs e)
        {
            if (!chkDatabase.Checked && !chkExcel.Checked)
            {
                MessageBox.Show("Please select at least one export option.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyFormRounding();
        }

        private void ApplyFormRounding()
        {
            if (Width <= 0 || Height <= 0) return;
            int radius = 16;
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
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }
    }
}
