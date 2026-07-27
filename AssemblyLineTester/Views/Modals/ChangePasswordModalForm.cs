using System;
using System.Drawing;
using System.Windows.Forms;
using AssemblyLineTester.Helpers;
using AssemblyLineTester.Services;

namespace AssemblyLineTester.Views.Modals
{
    /// <summary>
    /// Lets the signed-in user change their own password.
    /// </summary>
    /// <remarks>
    /// Built in code rather than with a designer file so the whole dialog is one file. It is small
    /// and static, and a hand-written .Designer.cs is a liability nobody can safely reopen in the
    /// designer afterwards.
    /// </remarks>
    public class ChangePasswordModalForm : Form
    {
        private readonly UserService _userService;
        private readonly bool _mandatory;
        private readonly float _scale;

        private readonly TextBox _txtCurrent = new();
        private readonly TextBox _txtNew = new();
        private readonly TextBox _txtConfirm = new();
        private readonly Label _lblHint = new();
        private readonly Button _btnSave = new();

        public ChangePasswordModalForm(UserService userService, bool mandatory = false)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mandatory = mandatory;
            _scale = UIStyleHelper.GetHostScaleFactor(Form.ActiveForm ?? this);
            if (_scale <= 0) _scale = 1f;

            BuildLayout();
        }

        private int S(int value) => Math.Max(1, (int)Math.Round(value * _scale));

        private Font Scaled(Font font) => UIStyleHelper.ScaleFont(font, _scale);

        private void BuildLayout()
        {
            Text = "Change password";
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterParent;
            ShowInTaskbar = false;
            BackColor = Color.White;
            DoubleBuffered = true;
            ClientSize = new Size(S(470), S(520));

            int left = S(38);
            int fieldWidth = ClientSize.Width - (left * 2);
            int y = S(34);

            var heading = new Label
            {
                Text = "Choose a new password",
                Font = Scaled(UIStyleHelper.Font16Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                AutoSize = true,
                Location = new Point(left, y)
            };
            Controls.Add(heading);
            y += S(38);

            var subtitle = new Label
            {
                Text = $"At least {UserService.MinimumPasswordLength} characters. Longer beats complicated.",
                Font = Scaled(UIStyleHelper.Font95Regular),
                ForeColor = Color.FromArgb(100, 116, 139),
                Size = new Size(fieldWidth, S(22)),
                Location = new Point(left, y)
            };
            Controls.Add(subtitle);
            y += S(38);

            y = AddField("Current password", _txtCurrent, left, fieldWidth, y);
            y = AddField("New password", _txtNew, left, fieldWidth, y);
            y = AddField("Confirm new password", _txtConfirm, left, fieldWidth, y);

            var show = new CheckBox
            {
                Text = "Show passwords",
                Font = Scaled(UIStyleHelper.Font9Regular),
                ForeColor = Color.FromArgb(100, 116, 139),
                AutoSize = true,
                Location = new Point(left, y)
            };
            show.CheckedChanged += (s, e) =>
            {
                // Typing a password you cannot see, twice, is where most retries come from.
                foreach (TextBox box in new[] { _txtCurrent, _txtNew, _txtConfirm })
                {
                    box.UseSystemPasswordChar = !show.Checked;
                }
            };
            Controls.Add(show);
            y += S(34);

            _lblHint.Font = Scaled(UIStyleHelper.Font9Regular);
            _lblHint.Size = new Size(fieldWidth, S(36));
            _lblHint.Location = new Point(left, y);
            Controls.Add(_lblHint);
            y += S(44);

            _btnSave.Text = "Save new password";
            _btnSave.Size = new Size(S(220), S(44));
            _btnSave.Location = new Point(ClientSize.Width - left - S(220), y);
            _btnSave.Font = Scaled(UIStyleHelper.Font105Bold);
            _btnSave.ForeColor = Color.FromArgb(15, 23, 42);
            _btnSave.Click += BtnSave_Click;
            UIStyleHelper.SetupButtonRoundedEdges(_btnSave, 10,
                Color.FromArgb(250, 204, 21), Color.FromArgb(254, 216, 53),
                Color.FromArgb(234, 186, 10), Color.Transparent);
            Controls.Add(_btnSave);

            if (!_mandatory)
            {
                var cancel = new Button
                {
                    Text = "Cancel",
                    Size = new Size(S(110), S(44)),
                    Location = new Point(left, y),
                    Font = Scaled(UIStyleHelper.Font105Regular),
                    ForeColor = Color.FromArgb(71, 85, 105),
                    DialogResult = DialogResult.Cancel
                };
                UIStyleHelper.SetupButtonRoundedEdges(cancel, 10,
                    Color.White, Color.FromArgb(248, 250, 252),
                    Color.FromArgb(241, 245, 249), Color.FromArgb(203, 213, 225));
                Controls.Add(cancel);

                CancelButton = cancel;
            }

            AcceptButton = _btnSave;

            // Live feedback while typing, so the first press of Save is not the first news of a
            // mismatch.
            _txtNew.TextChanged += (s, e) => UpdateHint();
            _txtConfirm.TextChanged += (s, e) => UpdateHint();

            UIStyleHelper.SetupCardRoundedEdges(this, 14, Color.FromArgb(226, 232, 240), 1.5f);
        }

        private int AddField(string caption, TextBox box, int left, int width, int y)
        {
            var label = new Label
            {
                Text = caption,
                Font = Scaled(UIStyleHelper.Font9Bold),
                ForeColor = Color.FromArgb(71, 85, 105),
                AutoSize = true,
                Location = new Point(left, y)
            };
            Controls.Add(label);

            box.UseSystemPasswordChar = true;
            box.BorderStyle = BorderStyle.None;
            box.Font = Scaled(UIStyleHelper.Font105Regular);
            box.Size = new Size(width - S(20), S(24));
            box.Location = new Point(left + S(10), y + S(28));
            Controls.Add(box);

            // A drawn box rather than FixedSingle, so the border can pick up focus and match the
            // rest of the application's rounded surfaces.
            var frame = new Panel
            {
                Size = new Size(width, S(42)),
                Location = new Point(left, y + S(20)),
                BackColor = Color.White
            };
            frame.Paint += (s, e) =>
            {
                Color edge = box.Focused ? Color.FromArgb(250, 204, 21) : Color.FromArgb(203, 213, 225);
                using var pen = new Pen(edge, box.Focused ? 1.6f * _scale : 1f * _scale);
                using var path = UIStyleHelper.GetRoundedPath(
                    new Rectangle(0, 0, frame.Width - 1, frame.Height - 1), Math.Max(1, S(8)));
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.DrawPath(pen, path);
            };
            Controls.Add(frame);
            frame.SendToBack();

            box.Enter += (s, e) => frame.Invalidate();
            box.Leave += (s, e) => frame.Invalidate();

            return y + S(76);
        }

        private void UpdateHint()
        {
            if (_txtNew.TextLength == 0)
            {
                _lblHint.Text = string.Empty;
                return;
            }

            string? invalid = UserService.ValidatePassword(_txtNew.Text);
            if (invalid is not null)
            {
                SetHint($"{_txtNew.TextLength} of {UserService.MinimumPasswordLength} characters", warning: true);
                return;
            }

            if (_txtConfirm.TextLength > 0 && _txtNew.Text != _txtConfirm.Text)
            {
                SetHint("The two entries do not match yet", warning: true);
                return;
            }

            SetHint(_txtConfirm.TextLength > 0 ? "Ready to save" : "Long enough - confirm it below", warning: false);
        }

        private void SetHint(string text, bool warning)
        {
            _lblHint.Text = text;
            _lblHint.ForeColor = warning ? Color.FromArgb(146, 64, 14) : Color.FromArgb(21, 128, 61);
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (_txtNew.Text != _txtConfirm.Text)
            {
                SetHint("The two new passwords do not match.", warning: true);
                return;
            }

            // The service owns the rules, so the dialog does not get its own copy to drift from.
            string? invalid = UserService.ValidatePassword(_txtNew.Text);
            if (invalid is not null)
            {
                SetHint(invalid, warning: true);
                return;
            }

            try
            {
                _btnSave.Enabled = false;
                _userService.ChangeOwnPassword(_txtCurrent.Text, _txtNew.Text);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                // Shown inline rather than in a message box: the user is mid-form and has to correct
                // a field, not acknowledge an event.
                SetHint(ex.Message, warning: true);
                FileLogService.Warn("ChangePasswordModalForm", $"Password change refused: {ex.Message}");
            }
            finally
            {
                _btnSave.Enabled = true;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Alt+F4 and the taskbar can still close a borderless form.
            if (_mandatory && DialogResult != DialogResult.OK && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                SetHint("A new password is required before you can continue.", warning: true);
                return;
            }

            base.OnFormClosing(e);
        }
    }
}
