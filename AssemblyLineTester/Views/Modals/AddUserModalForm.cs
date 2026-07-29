using System;
using System.Drawing;
using System.Windows.Forms;
using AssemblyLineTester.Models;
using AssemblyLineTester.Services;
using AssemblyLineTester.Helpers;

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


        private void TitleBar_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                ActiveControl = pnlCustomHeader;
                ValidateChildren();

            NativeMethods.DragWindow(Handle);
            }
        }

        private void InitializeDropdowns()
        {
            cmbRole.Items.Clear();
            cmbRole.Items.AddRange(["Administrator", "Operator", "Engineer", "Technician", "Quality", "Supervisor"]);
            cmbRole.SelectedIndex = 1;

            cmbDepartment.Items.Clear();
            cmbDepartment.Items.AddRange(["Engineering", "Production", "Maintenance", "Quality Assurance"]);
            cmbDepartment.SelectedIndex = 1;

            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(["Active", "Inactive", "Locked"]);
            cmbStatus.SelectedIndex = 0;
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
                int roleId = cmbRole.SelectedIndex + 1;
                int statusId = cmbStatus.SelectedIndex + 1;
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

                // Every account starts on the shared default and must replace it at first sign-in.
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
