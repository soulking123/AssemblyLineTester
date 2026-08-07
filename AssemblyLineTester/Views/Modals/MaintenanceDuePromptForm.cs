using System;
using System.Drawing;
using System.Windows.Forms;
using AssemblyLineTester.Helpers;
using AssemblyLineTester.Services;

namespace AssemblyLineTester.Views.Modals
{
    // Blocks the application once either maintenance countdown reaches zero.
    public class MaintenanceDuePromptForm : Form
    {
        private readonly float _scale;
        private readonly bool _canReset;

        public MaintenanceDuePromptForm(float scale)
        {
            _scale = scale <= 0 ? 1f : scale;
            _canReset = SessionManager.HasPermission("Action.Maintenance.Reset");
            BuildLayout();
        }

        private int S(int value) => Math.Max(1, (int)Math.Round(value * _scale));

        private void BuildLayout()
        {
            Text = "Maintenance due";
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterParent;
            ShowInTaskbar = false;
            BackColor = Color.White;
            ClientSize = new Size(S(480), S(_canReset ? 380 : 350));
            DoubleBuffered = true;

            // No close box and Escape does nothing: the rig is out of its service window, and a
            // dialog that can be dismissed is the same as no dialog at all.
            ControlBox = false;

            var icon = new Label
            {
                Text = "🛠",
                Font = new Font("Segoe UI Emoji", 34F * _scale),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(S(88), S(88)),
                Location = new Point((ClientSize.Width - S(88)) / 2, S(34)),
                BackColor = Color.FromArgb(254, 243, 199)
            };
            UIStyleHelper.SetupCardRoundedEdges(icon, 44, Color.Transparent, 0f);
            Controls.Add(icon);

            var heading = new Label
            {
                Text = "Maintenance is due",
                Font = UIStyleHelper.ScaleFont(UIStyleHelper.Font18Bold, _scale),
                ForeColor = Color.FromArgb(15, 23, 42),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(ClientSize.Width, S(40)),
                Location = new Point(0, S(136))
            };
            Controls.Add(heading);

            string detail = _canReset
                ? "Reset the countdown once the service has been carried out."
                : "Ask a supervisor or engineer to reset the countdown before testing continues.";

            var body = new Label
            {
                Text = $"{MaintenanceService.DueReason}\r\n\r\n" +
                       $"{MaintenanceService.RemainingDays} day(s) and " +
                       $"{MaintenanceService.FormatHours(MaintenanceService.RemainingHoursLive)} of operating time remain.\r\n\r\n" +
                       detail,
                Font = UIStyleHelper.ScaleFont(UIStyleHelper.Font9Regular, _scale),
                ForeColor = Color.FromArgb(71, 85, 105),
                TextAlign = ContentAlignment.TopCenter,
                Size = new Size(ClientSize.Width - S(60), S(110)),
                Location = new Point(S(30), S(180))
            };
            Controls.Add(body);

            if (_canReset)
            {
                var reset = new Button
                {
                    Text = "Reset maintenance",
                    Font = UIStyleHelper.ScaleFont(UIStyleHelper.Font9Bold, _scale),
                    Size = new Size(S(220), S(40)),
                    Location = new Point((ClientSize.Width - S(220)) / 2, S(300)),
                    DialogResult = DialogResult.OK
                };
                UIStyleHelper.SetupButtonRoundedEdges(reset, 10,
                    Color.FromArgb(250, 204, 21), Color.FromArgb(254, 216, 53),
                    Color.FromArgb(234, 186, 10), Color.Transparent);
                Controls.Add(reset);
                AcceptButton = reset;
            }
            else
            {
                // Without the permission there is nothing here to act on, so signing out is the
                // only way forward. A Close would hand back an application that stays blocked.
                var signOut = new Button
                {
                    Text = "Log out",
                    Font = UIStyleHelper.ScaleFont(UIStyleHelper.Font9Bold, _scale),
                    Size = new Size(S(180), S(40)),
                    Location = new Point((ClientSize.Width - S(180)) / 2, S(285)),
                    DialogResult = DialogResult.Abort
                };
                UIStyleHelper.SetupButtonRoundedEdges(signOut, 10,
                    Color.FromArgb(250, 204, 21), Color.FromArgb(254, 216, 53),
                    Color.FromArgb(234, 186, 10), Color.Transparent);
                Controls.Add(signOut);

                AcceptButton = signOut;
            }
        }
    }
}
