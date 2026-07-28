using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using AssemblyLineTester.Helpers;

namespace AssemblyLineTester.Views.Modals
{
    // Shown after sign-in when the account still carries a password somebody else chose.
    public class SecureAccountPromptForm : Form
    {
        private readonly System.Windows.Forms.Timer _fadeTimer = new() { Interval = 15 };
        private readonly float _scale;

        public SecureAccountPromptForm(float scale)
        {
            _scale = scale <= 0 ? 1f : scale;
            BuildLayout();
        }

        private int S(int value) => Math.Max(1, (int)Math.Round(value * _scale));

        private void BuildLayout()
        {
            Text = "Secure your account";
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterParent;
            ShowInTaskbar = false;
            BackColor = Color.White;
            ClientSize = new Size(S(470), S(340));
            Opacity = 0;
            DoubleBuffered = true;

            var icon = new Label
            {
                Text = "🔐",
                Font = new Font("Segoe UI Emoji", 34F * _scale),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(S(88), S(88)),
                Location = new Point((ClientSize.Width - S(88)) / 2, S(38)),
                BackColor = Color.FromArgb(254, 249, 216)
            };
            UIStyleHelper.SetupCardRoundedEdges(icon, 44, Color.Transparent, 0f);
            Controls.Add(icon);

            var heading = new Label
            {
                Text = "One last step",
                Font = UIStyleHelper.ScaleFont(UIStyleHelper.Font18Bold, _scale),
                ForeColor = Color.FromArgb(15, 23, 42),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(ClientSize.Width, S(40)),
                Location = new Point(0, S(142))
            };
            Controls.Add(heading);

            var body = new Label
            {
                Text = "This account still uses the password it was set up with.\r\n" +
                       "Anyone with the same installer knows it.\r\n\r\n" +
                       "Pick a password only you know to finish setting up.",
                Font = UIStyleHelper.ScaleFont(UIStyleHelper.Font105Regular, _scale),
                ForeColor = Color.FromArgb(100, 116, 139),
                TextAlign = ContentAlignment.TopCenter,
                Size = new Size(ClientSize.Width - S(60), S(90)),
                Location = new Point(S(30), S(186))
            };
            Controls.Add(body);

            var proceed = new Button
            {
                Text = "Choose my password",
                Size = new Size(S(260), S(46)),
                Location = new Point((ClientSize.Width - S(260)) / 2, S(268)),
                Font = UIStyleHelper.ScaleFont(UIStyleHelper.Font105Bold, _scale),
                ForeColor = Color.FromArgb(15, 23, 42),
                DialogResult = DialogResult.OK
            };
            UIStyleHelper.SetupButtonRoundedEdges(proceed, 10,
                Color.FromArgb(250, 204, 21), Color.FromArgb(254, 216, 53),
                Color.FromArgb(234, 186, 10), Color.Transparent);
            Controls.Add(proceed);

            AcceptButton = proceed;

            UIStyleHelper.SetupCardRoundedEdges(this, 14, Color.FromArgb(226, 232, 240), 1.5f);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            _fadeTimer.Tick += (s, args) =>
            {
                Opacity += 0.12;
                if (Opacity >= 1) _fadeTimer.Stop();
            };
            _fadeTimer.Start();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Alt+F4 and the taskbar are refused; the button is the only way out.
            if (DialogResult != DialogResult.OK && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                return;
            }

            base.OnFormClosing(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _fadeTimer.Dispose();
            base.Dispose(disposing);
        }
    }
}
