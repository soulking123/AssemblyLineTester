using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AssemblyLineTester.Models;
using AssemblyLineTester.Services;

namespace AssemblyLineTester.Views.Pages
{
    /// Represents the User Management page control for managing system users.
    public partial class UserManagement : UserControl
    {
        private readonly UserService _userService;
        private List<User> _allUsers = new();
        private Dictionary<string, int> _roleCounts = new();
        private int _totalUserCount = 0;

        /// Initializes a new instance of the UserManagement class.
        public UserManagement(UserService userService)
        {
            _userService = userService;
            InitializeComponent();
            InitializeFilters();
            SetupGridColumns();

            // Wire up event listeners for filters and controls
            txtSearch.TextChanged += FilterChanged;
            cmbFilterStatus.SelectedIndexChanged += FilterChanged;
            cmbFilterRole.SelectedIndexChanged += FilterChanged;
            cmbFilterDept.SelectedIndexChanged += FilterChanged;
            btnRefresh.Click += BtnRefresh_Click;

            gridUsers.CellFormatting += GridUsers_CellFormatting;
            gridUsers.SelectionChanged += GridUsers_SelectionChanged;

            if (pnlRoleChart is not null)
            {
                pnlRoleChart.Paint += PnlRoleChart_Paint;
            }

            // Wire up the Load event to fetch database records only when the page is displayed on screen
            this.Load += UserManagement_Load;
        }

        private void UserManagement_Load(object? sender, EventArgs e)
        {
            LoadDatabaseData();
        }

        /// Configures initial items for the Status, Role, and Department filter dropdowns.
        private void InitializeFilters()
        {
            cmbFilterStatus.Items.Clear();
            cmbFilterStatus.Items.AddRange(["Status: All", "Active", "Inactive", "Locked"]);
            cmbFilterStatus.SelectedIndex = 0;

            cmbFilterRole.Items.Clear();
            cmbFilterRole.Items.AddRange(["Role: All", "Administrator", "Engineer", "Operator", "Technician", "Quality", "Supervisor"]);
            cmbFilterRole.SelectedIndex = 0;

            cmbFilterDept.Items.Clear();
            cmbFilterDept.Items.AddRange(["Department: All", "Engineering", "Production", "Maintenance", "Quality Assurance"]);
            cmbFilterDept.SelectedIndex = 0;
        }

        /// Fetches user accounts from the SQLite database and updates KPIs, chart, and grid.
        private void LoadDatabaseData()
        {
            try
            {
                // Fetch using the injected service
                _allUsers = _userService.GetAllUsers();

                _totalUserCount = _allUsers.Count;
                int activeUsers = _allUsers.Count(u => u.Status.Name == "Active");
                int inactiveUsers = _allUsers.Count(u => u.Status.Name == "Inactive");
                int lockedUsers = _allUsers.Count(u => u.Status.Name == "Locked");
                int newUsersThisMonth = _allUsers.Count(u => u.CreatedAt.Month == DateTime.Now.Month && u.CreatedAt.Year == DateTime.Now.Year);
                var latestUser = _allUsers.Where(u => u.LastLogin.HasValue).OrderByDescending(u => u.LastLogin).FirstOrDefault();

                // Update KPI Cards
                lblKpiVal1.Text = _totalUserCount.ToString();
                lblKpiVal2.Text = activeUsers.ToString();
                lblKpiSub2.Text = $"Users ({(_totalUserCount > 0 ? (activeUsers * 100.0 / _totalUserCount) : 0):0.0}%)";
                lblKpiVal3.Text = inactiveUsers.ToString();
                lblKpiSub3.Text = $"Users ({(_totalUserCount > 0 ? (inactiveUsers * 100.0 / _totalUserCount) : 0):0.0}%)";
                lblKpiVal4.Text = lockedUsers.ToString();
                lblKpiSub4.Text = $"Users ({(_totalUserCount > 0 ? (lockedUsers * 100.0 / _totalUserCount) : 0):0.0}%)";
                lblKpiVal5.Text = latestUser?.FullName ?? "-";
                lblKpiSub5.Text = latestUser?.LastLogin?.ToString("dd MMM yyyy hh:mm:ss tt") ?? "No logins";
                lblKpiVal6.Text = newUsersThisMonth.ToString();

                // Calculate users per role
                _roleCounts = _allUsers
                    .GroupBy(u => u.Role.Name)
                    .ToDictionary(g => g.Key, g => g.Count());

                // Update Role Summary Legend Numbers
                lblLegendVal1.Text = _roleCounts.GetValueOrDefault("Administrator", 0).ToString();
                lblLegendVal2.Text = _roleCounts.GetValueOrDefault("Engineer", 0).ToString();
                lblLegendVal3.Text = _roleCounts.GetValueOrDefault("Operator", 0).ToString();
                lblLegendVal4.Text = _roleCounts.GetValueOrDefault("Technician", 0).ToString();
                lblLegendVal5.Text = _roleCounts.GetValueOrDefault("Quality", 0).ToString();
                lblLegendVal6.Text = _roleCounts.GetValueOrDefault("Supervisor", 0).ToString();

                pnlRoleChart?.Invalidate(); // Force redrawing the Doughnut Chart
                PopulateUserGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load user database:\n\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// Refreshes the DataGridView rows by applying search parameters and combobox filter selections.
        private void PopulateUserGrid()
        {
            gridUsers.Rows.Clear();

            string searchText = txtSearch.Text.Trim().ToLower();
            string filterStatus = cmbFilterStatus.SelectedItem?.ToString() ?? "Status: All";
            string filterRole = cmbFilterRole.SelectedItem?.ToString() ?? "Role: All";
            string filterDept = cmbFilterDept.SelectedItem?.ToString() ?? "Department: All";

            var filtered = _allUsers.AsEnumerable();

            if (!string.IsNullOrEmpty(searchText))
            {
                filtered = filtered.Where(u => 
                    u.Username.ToLower().Contains(searchText) || 
                    u.FullName.ToLower().Contains(searchText) || 
                    u.Department.ToLower().Contains(searchText));
            }

            if (filterStatus != "Status: All")
            {
                filtered = filtered.Where(u => u.Status.Name.Equals(filterStatus, StringComparison.OrdinalIgnoreCase));
            }

            if (filterRole != "Role: All")
            {
                filtered = filtered.Where(u => u.Role.Name.Equals(filterRole, StringComparison.OrdinalIgnoreCase));
            }

            if (filterDept != "Department: All")
            {
                filtered = filtered.Where(u => u.Department.Equals(filterDept, StringComparison.OrdinalIgnoreCase));
            }

            int index = 1;
            foreach (var user in filtered)
            {
                gridUsers.Rows.Add(
                    index++,
                    user.Username,
                    user.FullName,
                    $"{user.Username}@caterpillar.com",
                    user.Role.Name,
                    user.Department,
                    user.Status.Name,
                    user.LastLogin?.ToString("dd MMM yyyy hh:mm:ss tt") ?? "-",
                    user.CreatedAt.ToString("dd MMM yyyy hh:mm:ss tt") ?? "-",
                    "✏  🔒"
                );
            }
        }

        /// Event handler to refresh grid when search criteria or dropdown options change.
        private void FilterChanged(object? sender, EventArgs e)
        {
            PopulateUserGrid();
        }

        /// Event handler to query fresh database data when refresh button is clicked.
        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            LoadDatabaseData();
        }

        /// Handles displaying user details on the side panel when a grid row is selected.
        private void GridUsers_SelectionChanged(object? sender, EventArgs e)
        {
            if (gridUsers.SelectedRows.Count > 0)
            {
                var row = gridUsers.SelectedRows[0];
                lblValUser.Text = ": " + (row.Cells["colUsername"].Value?.ToString() ?? "-");
                lblValName.Text = ": " + (row.Cells["colFullName"].Value?.ToString() ?? "-");
                lblValEmail.Text = ": " + (row.Cells["colEmail"].Value?.ToString() ?? "-");
                lblValRole.Text = ": " + (row.Cells["colRole"].Value?.ToString() ?? "-");
                lblValDept.Text = ": " + (row.Cells["colDept"].Value?.ToString() ?? "-");
                
                string status = row.Cells["colStatus"].Value?.ToString() ?? "-";
                lblValStatus.Text = status;
                lblValStatus.BackColor = status == "Active" ? Color.FromArgb(209, 231, 221) : Color.FromArgb(226, 232, 240);
                lblValStatus.ForeColor = status == "Active" ? Color.FromArgb(15, 81, 50) : Color.FromArgb(71, 85, 105);
                
                lblValLogin.Text = ": " + (row.Cells["colLastLogin"].Value?.ToString() ?? "-");
                lblValCreated.Text = ": " + (row.Cells["colCreated"].Value?.ToString() ?? "-");
            }
        }

        /// Builds user management grid columns dynamically on page initialization.
        private void SetupGridColumns()
        {
            gridUsers.Columns.Clear();

            var cNo = gridUsers.Columns.Add("colNo", "No.");
            gridUsers.Columns[cNo].FillWeight = 35F;

            var cUser = gridUsers.Columns.Add("colUsername", "Username");
            gridUsers.Columns[cUser].FillWeight = 80F;

            var cName = gridUsers.Columns.Add("colFullName", "Full Name");
            gridUsers.Columns[cName].FillWeight = 95F;

            var cEmail = gridUsers.Columns.Add("colEmail", "Email");
            gridUsers.Columns[cEmail].FillWeight = 140F;

            var cRole = gridUsers.Columns.Add("colRole", "Role");
            gridUsers.Columns[cRole].FillWeight = 85F;

            var cDept = gridUsers.Columns.Add("colDept", "Department");
            gridUsers.Columns[cDept].FillWeight = 95F;

            var cStatus = gridUsers.Columns.Add("colStatus", "Status");
            gridUsers.Columns[cStatus].FillWeight = 65F;
            gridUsers.Columns[cStatus].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            var cLogin = gridUsers.Columns.Add("colLastLogin", "Last Login");
            gridUsers.Columns[cLogin].FillWeight = 110F;

            var cCreated = gridUsers.Columns.Add("colCreated", "Created Date");
            gridUsers.Columns[cCreated].FillWeight = 110F;

            var cAction = gridUsers.Columns.Add("colAction", "Action");
            gridUsers.Columns[cAction].FillWeight = 55F;
            gridUsers.Columns[cAction].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        /// Configures custom status cell background and text styling based on active state.
        private void GridUsers_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && gridUsers.Columns[e.ColumnIndex].Name == "colStatus" && e.Value is not null && e.CellStyle is not null)
            {
                string? status = e.Value.ToString();
                if (status == "Active")
                {
                    e.CellStyle.BackColor = Color.FromArgb(209, 231, 221);
                    e.CellStyle.ForeColor = Color.FromArgb(15, 81, 50);
                    e.CellStyle.Font = new("Segoe UI", 9F, FontStyle.Bold);
                }
                else if (status == "Inactive")
                {
                    e.CellStyle.BackColor = Color.FromArgb(226, 232, 240);
                    e.CellStyle.ForeColor = Color.FromArgb(71, 85, 105);
                    e.CellStyle.Font = new("Segoe UI", 9F, FontStyle.Bold);
                }
            }
        }

        /// Draws the Doughnut Chart dynamically based on the proportion of user roles in the database.
        private void PnlRoleChart_Paint(object? sender, PaintEventArgs e)
        {
            if (pnlRoleChart is null) return;
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Rectangle rect = new(5, 5, pnlRoleChart.Width - 10, pnlRoleChart.Height - 10);
            if (rect.Width <= 0 || rect.Height <= 0) return;
            int size = Math.Min(rect.Width, rect.Height);
            Rectangle chartRect = new(rect.X + (rect.Width - size) / 2, rect.Y + (rect.Height - size) / 2, size, size);

            float startAngle = 270f;
            if (_totalUserCount > 0)
            {
                var roleColors = new Dictionary<string, Color>
                {
                    { "Administrator", Color.FromArgb(250, 204, 21) },
                    { "Engineer", Color.FromArgb(34, 197, 94) },
                    { "Operator", Color.FromArgb(59, 130, 246) },
                    { "Technician", Color.FromArgb(168, 85, 247) },
                    { "Quality", Color.FromArgb(239, 68, 68) },
                    { "Supervisor", Color.FromArgb(148, 163, 184) }
                };

                foreach (var role in roleColors)
                {
                    int count = _roleCounts.GetValueOrDefault(role.Key, 0);
                    if (count > 0)
                    {
                        float sweepAngle = (count * 360f) / _totalUserCount;
                        using SolidBrush brush = new(role.Value);
                        g.FillPie(brush, chartRect, startAngle, sweepAngle);
                        startAngle += sweepAngle;
                    }
                }
            }
            else
            {
                using SolidBrush placeholderBrush = new(Color.FromArgb(226, 232, 240));
                g.FillEllipse(placeholderBrush, chartRect);
            }

            int innerSize = (int)(size * 0.62);
            Rectangle innerRect = new(chartRect.X + (size - innerSize) / 2, chartRect.Y + (size - innerSize) / 2, innerSize, innerSize);
            using SolidBrush bCenter = new(Color.White);
            g.FillEllipse(bCenter, innerRect);

            using Font fNum = new("Segoe UI", 16F, FontStyle.Bold);
            using SolidBrush bTxt = new(Color.FromArgb(30, 41, 59));
            using StringFormat sf = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g.DrawString($"{_totalUserCount}" + Environment.NewLine + "TOTAL", fNum, bTxt, chartRect, sf);
        }
    }
}
