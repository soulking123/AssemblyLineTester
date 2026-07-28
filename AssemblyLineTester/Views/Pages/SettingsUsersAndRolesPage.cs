using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AssemblyLineTester.Helpers;
using AssemblyLineTester.Models;
using AssemblyLineTester.Services;

namespace AssemblyLineTester.Views.Pages
{
    public partial class SettingsUsersAndRolesPage : UserControl
    {
        private readonly RolePermissionService _permissionService = new();
        private List<Role> _roles = [];
        private int _selectedRoleId = 1;
        private readonly Dictionary<string, CheckBox> _toggleCheckboxes = new();
        private readonly Dictionary<int, Button> _roleButtons = new();

        public SettingsUsersAndRolesPage()
        {
            InitializeComponent();
            SetupStyles();
            SetupEvents();
            this.Load += SettingsUsersAndRolesPage_Load;
        }

        private void SetupStyles()
        {
        }

        private void SetupEvents()
        {
            btnSaveChanges.Click += BtnSaveChanges_Click;
            btnResetDefaults.Click += BtnResetDefaults_Click;
            tblCardsContainer.Resize += (s, ev) => AdjustCardSizes();
            flpRolesList.Resize += (s, ev) => AdjustCardSizes();
        }

        private async void SettingsUsersAndRolesPage_Load(object? sender, EventArgs e)
        {
            if (!DesignMode)
            {
                UIStyleHelper.ScaleExplicitFonts(this, UIStyleHelper.GetHostScaleFactor(this));
            }
            AdjustCardSizes();
            await LoadRolesAndPermissionsAsync();
        }

        private void AdjustCardSizes()
        {
            if (flpRolesList.ClientSize.Width > 0)
            {
                foreach (Control ctrl in flpRolesList.Controls)
                {
                    if (ctrl is Button btn)
                        btn.Width = flpRolesList.ClientSize.Width - 10;
                }
            }
        }

        private async Task LoadRolesAndPermissionsAsync()
        {
            try
            {
                _roles = await _permissionService.GetAllRolesAsync();
                _roleButtons.Clear();

                // Designer buttons carry their role id in Tag.
                foreach (Control ctrl in flpRolesList.Controls)
                {
                    if (ctrl is AssemblyLineTester.Views.Controls.RoundedButton btnRole && btnRole.Tag != null)
                    {
                        if (int.TryParse(btnRole.Tag.ToString(), out int roleId))
                        {
                            _roleButtons[roleId] = btnRole;
                            
                            btnRole.Click -= BtnRole_Click;
                            btnRole.Click += BtnRole_Click;
                        }
                    }
                }

                if (_roles.Count > 0)
                {
                    await SelectRoleAsync(_roles[0].Id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading roles: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnRole_Click(object? sender, EventArgs e)
        {
            if (sender is AssemblyLineTester.Views.Controls.RoundedButton btn && btn.Tag != null)
            {
                if (int.TryParse(btn.Tag.ToString(), out int roleId))
                {
                    await SelectRoleAsync(roleId);
                }
            }
        }

        private static string GetRoleIcon(string roleName) => roleName switch
        {
            "Administrator" => "👑",
            "Operator" => "🏭",
            "Engineer" => "📐",
            "Technician" => "🔧",
            "Quality" => "🔎",
            "Supervisor" => "📋",
            _ => "👤"
        };

        private async Task SelectRoleAsync(int roleId)
        {
            _selectedRoleId = roleId;
            var role = _roles.FirstOrDefault(r => r.Id == roleId);
            if (role == null) return;

            lblSelectedRoleTitle.Text = $"Role Permissions: {role.Name}";

            foreach (var kvp in _roleButtons)
            {
                if (kvp.Value is AssemblyLineTester.Views.Controls.RoundedButton rBtn)
                {
                    if (kvp.Key == roleId)
                    {
                        rBtn.NormalBackColor = Color.FromArgb(250, 204, 21);
                        rBtn.HoverBackColor = Color.FromArgb(250, 204, 21);
                        rBtn.ForeColor = Color.FromArgb(15, 23, 42);
                        rBtn.HoverForeColor = Color.FromArgb(15, 23, 42);
                    }
                    else
                    {
                        rBtn.NormalBackColor = Color.FromArgb(248, 250, 252);
                        rBtn.HoverBackColor = Color.FromArgb(254, 216, 53);
                        rBtn.ForeColor = Color.FromArgb(30, 41, 59);
                        rBtn.HoverForeColor = Color.FromArgb(15, 23, 42);
                    }
                }
            }

            var permissions = await _permissionService.GetPermissionsByRoleIdAsync(roleId);
            // Keyed by id, matching the service guard; a renamed role must not unlock its toggles.
            bool isAdmin = (role.Id == 1);

            var pagePerms = permissions.Where(p => p.PermissionGroup == "Page Access").ToList();
            var testPerms = permissions.Where(p => p.PermissionGroup == "Test Operations").ToList();
            var sysPerms = permissions.Where(p => p.PermissionGroup != "Page Access" && p.PermissionGroup != "Test Operations").ToList();

            RenderPermissions(roleId, pagePerms, testPerms, sysPerms, isAdmin);
        }

        private void RenderPermissions(int roleId, List<RolePermission> pagePerms, List<RolePermission> testPerms, List<RolePermission> sysPerms, bool isAdmin)
        {
            _toggleCheckboxes.Clear();

            PopulatePermissionGrid(tblPageAccessToggles, pagePerms, isAdmin);
            PopulatePermissionGrid(tblTestOpsToggles, testPerms, isAdmin);
            PopulatePermissionGrid(tblSystemLogsToggles, sysPerms, isAdmin);
        }

        private void PopulatePermissionGrid(TableLayoutPanel container, List<RolePermission> perms, bool isAdmin)
        {
            foreach (CheckBox chk in container.Controls.OfType<CheckBox>())
            {
                string permKey = chk.Tag?.ToString() ?? string.Empty;
                var rp = perms.FirstOrDefault(p => p.PermissionKey == permKey || p.Description == chk.Text);
                bool isGranted = rp?.IsGranted ?? false;

                chk.Checked = isAdmin || isGranted;
                chk.AutoCheck = !isAdmin;
                chk.Cursor = isAdmin ? Cursors.Default : Cursors.Hand;

                if (!string.IsNullOrEmpty(permKey))
                {
                    _toggleCheckboxes[permKey] = chk;
                }
            }
        }

        private void AdjustCardHeights()
        {
        }

        private async void BtnSaveChanges_Click(object? sender, EventArgs e)
        {
            try
            {
                Dictionary<string, bool> updatedToggles = new();
                foreach (var (key, chk) in _toggleCheckboxes)
                {
                    updatedToggles[key] = chk.Checked;
                }

                _permissionService.UpdatePermissions(_selectedRoleId, updatedToggles);

                var role = _roles.FirstOrDefault(r => r.Id == _selectedRoleId);
                MessageBox.Show($"Permissions successfully updated for role '{role?.Name ?? _selectedRoleId.ToString()}'!",
                    "Permissions Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                await SelectRoleAsync(_selectedRoleId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update permissions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnResetDefaults_Click(object? sender, EventArgs e)
        {
            var role = _roles.FirstOrDefault(r => r.Id == _selectedRoleId);
            var confirm = MessageBox.Show(
                $"Are you sure you want to reset all permissions for '{role?.Name}' to their default system settings?",
                "Confirm Reset",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    _permissionService.ResetRoleToDefaults(_selectedRoleId);
                    await SelectRoleAsync(_selectedRoleId);
                    MessageBox.Show($"Permissions for '{role?.Name}' have been reset to defaults.", "Reset Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to reset permissions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

}
