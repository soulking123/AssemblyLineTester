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
