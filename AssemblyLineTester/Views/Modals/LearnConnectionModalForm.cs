using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AssemblyLineTester.Models;
using AssemblyLineTester.Services;

namespace AssemblyLineTester.Views.Modals
{
    public partial class LearnConnectionModalForm : Form
    {
        private readonly List<(LearnConnection Connection, CheckBox Box)> _options = new();
        private FlowLayoutPanel? _optionsFlow;

        // The types the operator left ticked when they pressed Proceed.
        public List<LearnConnection> SelectedTypes =>
            _options.Where(o => o.Box.Checked).Select(o => o.Connection).ToList();

        public LearnConnectionModalForm()
        {
            InitializeComponent();
            BuildOptions();

            btnCancel.Click += (s, e) => this.Close();
            btnProceed.Click += BtnProceed_Click;

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

        // Only the types enabled in Settings are offered here; their tick state seeds the boxes.
        private void BuildOptions()
        {
            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.Transparent,
                Padding = new Padding(56, 43, 12, 12)
            };

            foreach (var connection in TestSettingService.GetLearnConnections().Where(c => c.IsChecked))
            {
                var box = new CheckBox
                {
                    Text = connection.DisplayName,
                    Checked = true,
                    AutoSize = true,
                    BackColor = Color.Transparent,
                    Font = new Font("Segoe UI", 12F),
                    ForeColor = Color.Black,
                    Margin = new Padding(6),
                    UseVisualStyleBackColor = false
                };
                flow.Controls.Add(box);
                _options.Add((connection, box));
            }

            if (_options.Count == 0)
            {
                flow.Controls.Add(new Label
                {
                    Text = "No connection types are enabled. Turn them on under Settings > Test.",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 12F),
                    ForeColor = Color.FromArgb(100, 116, 139),
                    Margin = new Padding(6)
                });
                btnProceed.Enabled = false;
            }

            pnlContent.Controls.Add(flow);
            _optionsFlow = flow;
        }

        // Shrinks the dialog to the number of enabled options, so a short list is not a half-empty
        // box. The designer height stays the ceiling - past it the flow scrolls instead of growing.
        private void FitHeightToOptions()
        {
            if (_optionsFlow == null) return;

            int contentHeight = _optionsFlow.Padding.Vertical +
                _optionsFlow.Controls.Cast<Control>().Sum(c => c.PreferredSize.Height + c.Margin.Vertical);

            int wanted = Math.Max(contentHeight + 15, 120);
            ClientSize = new Size(ClientSize.Width, pnlCustomHeader.Height + wanted + pnlBottom.Height);
        }

        private void BtnProceed_Click(object? sender, EventArgs e)
        {
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
