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

            btnClose.Click += (s, e) => this.Close();
            btnClose.MouseEnter += (s, e) => { btnClose.BackColor = Color.FromArgb(225, 29, 72); btnClose.IconColor = Color.White; };
            btnClose.MouseLeave += (s, e) => { btnClose.BackColor = Color.Transparent; btnClose.IconColor = Color.FromArgb(203, 213, 225); };

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

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string companyId = txtCompanyId.Text.Trim();
            string fullName = txtFullName.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

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

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter a password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match. Please re-enter.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Clear();
                txtConfirmPassword.Focus();
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

                _userService.CreateUser(newUser, password);

                MessageBox.Show($"User '{username}' has been successfully created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
