using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace AssemblyLineTester.Views.Modals
{
    public partial class ImagePreviewModalForm : Form
    {
        private List<string> _imagePaths = new List<string>();
        private int _currentIndex = 0;

        public ImagePreviewModalForm(List<string> imagePaths, string title = "IMAGE PREVIEW")
        {
            InitializeComponent();
            lblCustomTitle.Text = title.ToUpper();
            
            if (imagePaths != null && imagePaths.Count > 0)
            {
                _imagePaths = imagePaths;
            }

            SetupEvents();
            ApplyFormRounding();
            UpdateImageDisplay();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
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

        private void ApplyFormRounding()
        {
            if (Width <= 0 || Height <= 0) return;
            int radius = 24;
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            using GraphicsPath path = new();
            int d = radius * 2;
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            this.Region = new Region(path);
        }

        private void SetupEvents()
        {
            btnCloseModal.Click += (s, e) => Close();
            btnNext.Click += BtnNext_Click;
            btnPrev.Click += BtnPrev_Click;
            btnMaximize.Click += (s, e) => ToggleMaximize();

            btnMaximize.MouseEnter += (s, e) => { btnMaximize.BackColor = Color.FromArgb(30, 41, 59); btnMaximize.IconColor = Color.White; };
            btnMaximize.MouseLeave += (s, e) => { btnMaximize.BackColor = Color.Transparent; btnMaximize.IconColor = Color.FromArgb(203, 213, 225); };

            pnlCustomHeader.MouseDown += TitleBar_MouseDown;
            iconLogo.MouseDown += TitleBar_MouseDown;
            lblCustomTitle.MouseDown += TitleBar_MouseDown;

            pnlCustomHeader.DoubleClick += (s, e) => ToggleMaximize();
            iconLogo.DoubleClick += (s, e) => ToggleMaximize();
            lblCustomTitle.DoubleClick += (s, e) => ToggleMaximize();
        }

        private void BtnPrev_Click(object? sender, EventArgs e)
        {
            if (_imagePaths.Count <= 1) return;
            _currentIndex--;
            if (_currentIndex < 0) _currentIndex = _imagePaths.Count - 1;
            UpdateImageDisplay();
        }

        private void BtnNext_Click(object? sender, EventArgs e)
        {
            if (_imagePaths.Count <= 1) return;
            _currentIndex++;
            if (_currentIndex >= _imagePaths.Count) _currentIndex = 0;
            UpdateImageDisplay();
        }

        private void UpdateImageDisplay()
        {
            if (_imagePaths.Count == 0)
            {
                lblImageCounter.Text = "No images available";
                picPreview.Image = null;
                btnNext.Visible = false;
                btnPrev.Visible = false;
                return;
            }

            try
            {
                string path = _imagePaths[_currentIndex];
                if (System.IO.File.Exists(path))
                {
                    picPreview.Image = Image.FromFile(path);
                }
                else
                {
                    // Fallback to placeholder or null if not found
                    picPreview.Image = null;
                }
            }
            catch
            {
                picPreview.Image = null;
            }

            lblImageCounter.Text = $"{_currentIndex + 1} of {_imagePaths.Count}";
            
            // Hide buttons if only 1 image
            if (_imagePaths.Count <= 1)
            {
                btnNext.Visible = false;
                btnPrev.Visible = false;
            }
            else
            {
                btnNext.Visible = true;
                btnPrev.Visible = true;
            }
        }

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
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

        private void ToggleMaximize()
        {
            ActiveControl = pnlCustomHeader;
            ValidateChildren();

            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
                btnMaximize.IconChar = IconChar.WindowMaximize;
                Padding = new Padding(1);
            }
            else
            {
                MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
                WindowState = FormWindowState.Maximized;
                btnMaximize.IconChar = IconChar.WindowRestore;
                Padding = new Padding(0);
            }
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x0084;
            const int HTLEFT = 10;
            const int HTRIGHT = 11;
            const int HTTOP = 12;
            const int HTTOPLEFT = 13;
            const int HTTOPRIGHT = 14;
            const int HTBOTTOM = 15;
            const int HTBOTTOMLEFT = 16;
            const int HTBOTTOMRIGHT = 17;

            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST && (int)m.Result == 1 && WindowState == FormWindowState.Normal)
            {
                Point pt = PointToClient(Cursor.Position);
                int border = 6;
                bool overButtons = pt.X >= btnMaximize.Left && pt.Y <= pnlCustomHeader.Height;

                if (overButtons) return;

                if (pt.X <= border && pt.Y <= border) m.Result = (IntPtr)HTTOPLEFT;
                else if (pt.X >= ClientSize.Width - border && pt.Y <= border) m.Result = (IntPtr)HTTOPRIGHT;
                else if (pt.X <= border && pt.Y >= ClientSize.Height - border) m.Result = (IntPtr)HTBOTTOMLEFT;
                else if (pt.X >= ClientSize.Width - border && pt.Y >= ClientSize.Height - border) m.Result = (IntPtr)HTBOTTOMRIGHT;
                else if (pt.X <= border) m.Result = (IntPtr)HTLEFT;
                else if (pt.X >= ClientSize.Width - border) m.Result = (IntPtr)HTRIGHT;
                else if (pt.Y <= border) m.Result = (IntPtr)HTTOP;
                else if (pt.Y >= ClientSize.Height - border) m.Result = (IntPtr)HTBOTTOM;
            }
        }
    }
}
