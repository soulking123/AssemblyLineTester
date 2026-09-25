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
        private List<string> _imageTitles = new List<string>();
        private string _defaultTitle = "IMAGE PREVIEW";
        private int _currentIndex = 0;

        public ImagePreviewModalForm(List<string> imagePaths, string title = "IMAGE PREVIEW", int initialIndex = 0, List<string>? imageTitles = null)
        {
            InitializeComponent();
            _defaultTitle = title;
            lblCustomTitle.Text = title.ToUpper();

            if (imageTitles != null)
            {
                _imageTitles = imageTitles;
            }
            
            if (imagePaths != null && imagePaths.Count > 0)
            {
                _imagePaths = imagePaths;
                if (initialIndex >= 0 && initialIndex < _imagePaths.Count)
                {
                    _currentIndex = initialIndex;
                }
            }

            SetupEvents();
            ApplyFormRounding();
            UpdateCounterBounds();
            UpdateImageDisplay();
        }

        private void UpdateCounterBounds()
        {
            lblImageCounter.Bounds = new Rectangle(0, pnlFooter.Padding.Top, pnlFooter.ClientSize.Width, Math.Max(1, pnlFooter.ClientSize.Height - pnlFooter.Padding.Top));
            lblImageCounter.SendToBack();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyFormRounding();
            UpdateCounterBounds();
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
            pnlFooter.Resize += (s, e) => UpdateCounterBounds();
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
            if (_currentIndex <= 0) return;
            _currentIndex--;
            UpdateImageDisplay();
        }

        private void BtnNext_Click(object? sender, EventArgs e)
        {
            if (_currentIndex >= _imagePaths.Count - 1) return;
            _currentIndex++;
            UpdateImageDisplay();
        }

        private void UpdateImageDisplay()
        {
            if (_imagePaths.Count == 0)
            {
                lblCustomTitle.Text = _defaultTitle.ToUpper();
                lblImageCounter.Text = "No images available";
                AssemblyLineTester.Helpers.ImageLoader.SetImage(picPreview, null);
                btnNext.Visible = false;
                btnPrev.Visible = false;
                return;
            }

            if (_currentIndex >= 0 && _currentIndex < _imageTitles.Count && !string.IsNullOrWhiteSpace(_imageTitles[_currentIndex]))
            {
                lblCustomTitle.Text = _imageTitles[_currentIndex].ToUpper();
            }
            else
            {
                lblCustomTitle.Text = _defaultTitle.ToUpper();
            }

            AssemblyLineTester.Helpers.ImageLoader.SetImage(picPreview, _imagePaths[_currentIndex]);

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
                btnPrev.Enabled = (_currentIndex > 0);
                btnNext.Enabled = (_currentIndex < _imagePaths.Count - 1);
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
