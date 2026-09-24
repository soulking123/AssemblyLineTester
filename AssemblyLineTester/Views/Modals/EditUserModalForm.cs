using System;
using System.Drawing;
using System.Windows.Forms;
using AssemblyLineTester.Models;
using AssemblyLineTester.Services;
using AssemblyLineTester.Helpers;

namespace AssemblyLineTester.Views.Modals
{
    public partial class EditUserModalForm : Form
    {
        private readonly UserService _userService;
        private readonly User _user;

        // Order matches the seeded Role ids (1-6), the same convention AddUserModalForm relies on.
        private static readonly string[] Roles = ["Administrator", "Operator", "Engineer", "Technician", "Quality", "Supervisor"];
        private static readonly string[] Statuses = ["Active", "Inactive"];

        public EditUserModalForm(UserService userService, User user)
        {
            _userService = userService;
            _user = user;
            InitializeComponent();
            InitializeDropdowns();
            LoadUserIntoForm();

            btnSave.Click += BtnSave_Click;

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
            cmbRole.Items.AddRange(Roles);

            cmbDepartment.Items.Clear();
            cmbDepartment.Items.AddRange(["Engineering", "Production", "Maintenance", "Quality Assurance"]);

            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(Statuses);
        }

        // The account editing itself cannot leave through this form either - same guard the
        // service enforces, surfaced here so the field reads locked instead of failing on Save.
        private void LoadUserIntoForm()
        {
            txtUsername.Text = _user.Username;
            txtCompanyId.Text = _user.CompanyId;
            txtFullName.Text = _user.FullName;

            cmbRole.SelectedIndex = Math.Clamp(_user.RoleId - 1, 0, Roles.Length - 1);

            int deptIndex = cmbDepartment.Items.IndexOf(_user.Department);
            cmbDepartment.SelectedIndex = deptIndex >= 0 ? deptIndex : 0;

            cmbStatus.SelectedIndex = Math.Clamp(_user.StatusId - 1, 0, Statuses.Length - 1);

            if (SessionManager.CurrentUser?.UserId == _user.UserId)
            {
                cmbStatus.Enabled = false;
            }
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
                _user.Username = username;
                _user.CompanyId = companyId;
                _user.FullName = fullName;
                _user.RoleId = cmbRole.SelectedIndex + 1;
                _user.Department = cmbDepartment.SelectedItem?.ToString() ?? _user.Department;
                _user.StatusId = cmbStatus.SelectedIndex + 1;

                _userService.UpdateUser(_user);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Updating User", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
