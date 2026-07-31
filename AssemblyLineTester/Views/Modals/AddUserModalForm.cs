using System;
using System.Drawing;
using System.Windows.Forms;
using AssemblyLineTester.Models;
using AssemblyLineTester.Services;

namespace AssemblyLineTester.Views.Modals
{
    public partial class AddUserModalForm : Form
    {
        private readonly UserService _userService;

        public AddUserModalForm(UserService userService)
        {
            _userService = userService;
            InitializeComponent();
            InitializeDropdowns();

            btnSave.Click += BtnSave_Click;
            ReplacePasswordFieldsWithNotice();

            pnlCustomHeader.MouseDown += TitleBar_MouseDown;
            iconLogo.MouseDown += TitleBar_MouseDown;

            ApplyFormRounding();
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
            cmbRole.Items.Clear();
            cmbRole.Items.AddRange(["Administrator", "Operator", "Engineer", "Technician", "Quality", "Supervisor"]);
            cmbRole.SelectedIndex = 1; // Default to Operator

            cmbDepartment.Items.Clear();
            cmbDepartment.Items.AddRange(["Engineering", "Production", "Maintenance", "Quality Assurance"]);
            cmbDepartment.SelectedIndex = 1; // Default to Production

            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(["Active", "Inactive", "Locked"]);
            cmbStatus.SelectedIndex = 0; // Default to Active
        }

        /// <summary>
        /// Hides the password inputs and puts a notice in the space they occupied.
        /// </summary>
        /// <remarks>
        /// The administrator no longer chooses a password, so asking for one twice was two fields
        /// of work producing a value that gets replaced at first sign-in anyway. The controls are
        /// hidden rather than deleted from the designer file, which is Visual Studio's to own; the
        /// notice fills the gap that hiding them would otherwise leave.
        /// </remarks>
        private void ReplacePasswordFieldsWithNotice()
        {
            foreach (Control control in new Control[] { lblPassword, txtPassword, lblConfirmPassword, txtConfirmPassword })
            {
                control.Visible = false;
            }

            var notice = new Label
            {
                Text = $"🔑   Starting password:  {UserService.DefaultNewUserPassword}\r\n" +
                       "        They choose their own the first time they sign in.",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(71, 85, 105),
                BackColor = Color.FromArgb(248, 250, 252),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(18, 0, 0, 0),
                Location = lblPassword.Location,
                Size = new Size(
                    txtConfirmPassword.Right - lblPassword.Left,
                    txtPassword.Bottom - lblPassword.Top)
            };

            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(
                notice, 8, Color.FromArgb(226, 232, 240), 1.2f);

            Controls.Add(notice);
            notice.BringToFront();
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string companyId = txtCompanyId.Text.Trim();
            string fullName = txtFullName.Text.Trim();

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Please enter a username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(fullName))
            {
                MessageBox.Show("Please enter the user's full name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return;
            }

            try
            {
                int roleId = cmbRole.SelectedIndex + 1; // 1 to 6
                int statusId = cmbStatus.SelectedIndex + 1; // 1 to 3
                string dept = cmbDepartment.SelectedItem?.ToString() ?? "Production";

                var newUser = new User
                {
                    Username = username,
                    CompanyId = companyId,
                    FullName = fullName,
                    RoleId = roleId,
                    Department = dept,
                    StatusId = statusId
                };

                // No password is asked for: every account starts on the shared default and the
                // holder is made to replace it the first time they sign in.
                _userService.CreateUser(newUser);

                MessageBox.Show(
                    $"Account '{username}' is ready.\r\n\r\n" +
                    $"Starting password:  {UserService.DefaultNewUserPassword}\r\n\r\n" +
                    "They will be asked to choose their own the first time they sign in.\r\n" +
                    "Hand the account over soon - until they do, this password is all it takes to get in.",
                    "User created",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Creating User", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
