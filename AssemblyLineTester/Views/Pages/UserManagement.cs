using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using AssemblyLineTester.Models;
using AssemblyLineTester.Services;
using AssemblyLineTester.Views.Modals;

namespace AssemblyLineTester.Views.Pages
{
    /// Represents the User Management page control for managing system users.
    public partial class UserManagement : UserControl
    {
        private readonly UserService _userService;
        private List<User> _allUsers = [];
        private Dictionary<string, int> _roleCounts = [];
        private int _totalUserCount;
        private int _currentPage = 1;
        private int _pageSize = 10;
        private readonly ComboBox cmbPageSize = new();

        /// Initializes a new instance of the UserManagement class.
        public UserManagement(UserService userService)
        {
            _userService = userService;
            InitializeComponent();
            InitializeFilters();
            SetupGridColumns();
            InitializePaginationControls();

            // Wire up event listeners for filters and controls
            txtSearch.TextChanged += FilterChanged;
            cmbFilterStatus.SelectedIndexChanged += FilterChanged;
            cmbFilterRole.SelectedIndexChanged += FilterChanged;
            cmbFilterDept.SelectedIndexChanged += FilterChanged;
            btnRefresh.Click += BtnRefresh_Click;
            btnAddUser.Click += BtnAddUser_Click;

            gridUsers.CellFormatting += GridUsers_CellFormatting;
            gridUsers.CellPainting += GridUsers_CellPainting;
            gridUsers.SelectionChanged += GridUsers_SelectionChanged;

            pnlRoleChart.Paint += PnlRoleChart_Paint;

            // Wire up the Load event to fetch database records only when the page is displayed on screen
            this.Load += UserManagement_Load;
            SetupEvents();
        }

        private void UserManagement_Load(object? sender, EventArgs e)
        {
            LoadDatabaseData();
        }

        private void InitializePaginationControls()
        {
            FlowLayoutPanel flpPageSize = new()
            {
                Dock = DockStyle.Fill,
                WrapContents = false,
                FlowDirection = FlowDirection.LeftToRight
            };

            Label lblShow = new()
            {
                Text = "Show",
                AutoSize = true,
                Margin = new Padding(0, 14, 5, 0),
                ForeColor = Color.FromArgb(71, 85, 105)
            };

            cmbPageSize.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPageSize.Items.AddRange(["10", "25", "50", "100"]);
            cmbPageSize.SelectedIndex = 0;
            cmbPageSize.Size = new Size(65, 30);
            cmbPageSize.Margin = new Padding(0, 10, 0, 0);
            cmbPageSize.SelectedIndexChanged += (s, e) =>
            {
                if (int.TryParse(cmbPageSize.SelectedItem?.ToString(), out int size))
                {
                    _pageSize = size;
                    _currentPage = 1;
                    PopulateUserGrid();
                }
            };

            Label lblEntries = new()
            {
                Text = "entries",
                AutoSize = true,
                Margin = new Padding(5, 14, 0, 0),
                ForeColor = Color.FromArgb(71, 85, 105)
            };

            flpPageSize.Controls.Add(lblShow);
            flpPageSize.Controls.Add(cmbPageSize);
            flpPageSize.Controls.Add(lblEntries);

            tblPagination.Controls.Add(flpPageSize, 1, 0);
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
                // Fetch using the injected service, excluding system admin account from user list display
                _allUsers = [.. _userService.GetAllUsers().Where(u => u.Username != "admin").OrderBy(u => u.UserId)];

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

            string searchText = txtSearch.Text.Trim();
            string filterStatus = cmbFilterStatus.SelectedItem?.ToString() ?? "Status: All";
            string filterRole = cmbFilterRole.SelectedItem?.ToString() ?? "Role: All";
            string filterDept = cmbFilterDept.SelectedItem?.ToString() ?? "Department: All";

            var filtered = _allUsers.AsEnumerable();

            if (!string.IsNullOrEmpty(searchText))
            {
                filtered = filtered.Where(u => 
                    u.Username.Contains(searchText, StringComparison.OrdinalIgnoreCase) || 
                    u.FullName.Contains(searchText, StringComparison.OrdinalIgnoreCase) || 
                    $"{u.Username}@caterpillar.com".Contains(searchText, StringComparison.OrdinalIgnoreCase));
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

            List<User> filteredList = [.. filtered];
            int totalFiltered = filteredList.Count;

            lblUserListTitle.Text = $"USER LIST (Total: {totalFiltered})";

            int totalPages = (int)Math.Ceiling(totalFiltered / (double)_pageSize);
            if (totalPages < 1) totalPages = 1;
            if (_currentPage > totalPages) _currentPage = totalPages;

            UpdatePaginationUI(totalFiltered, totalPages);

            var pageItems = filteredList.Skip((_currentPage - 1) * _pageSize).Take(_pageSize);

            int index = (_currentPage - 1) * _pageSize + 1;
            foreach (var user in pageItems)
            {
                gridUsers.Rows.Add(
                    index++,
                    user.Username,
                    user.FullName,
                    $"{user.Username}@caterpillar.com",
                    user.Role.Name,
                    user.Department,
                    user.Status.Name,
                    user.LastLogin?.ToString("dd MMM yyyy\r\nhh:mm:ss tt") ?? "-",
                    user.CreatedAt.ToString("dd MMM yyyy\r\nhh:mm:ss tt") ?? "-",
                    "✏   🔒   ⋮"
                );
            }
        }

        private void UpdatePaginationUI(int totalFiltered, int totalPages)
        {
            if (totalFiltered == 0)
            {
                lblPaginationInfo.Text = "Showing 0 to 0 of 0 users";
            }
            else
            {
                int start = (_currentPage - 1) * _pageSize + 1;
                int end = Math.Min(_currentPage * _pageSize, totalFiltered);
                lblPaginationInfo.Text = $"Showing {start} to {end} of {totalFiltered} users";
            }

            flpPaginationButtons.Controls.Clear();

            var btnFirst = CreatePageButton("«", false, _currentPage > 1, (s, e) => { _currentPage = 1; PopulateUserGrid(); });
            flpPaginationButtons.Controls.Add(btnFirst);

            var btnPrev = CreatePageButton("‹", false, _currentPage > 1, (s, e) => { if (_currentPage > 1) { _currentPage--; PopulateUserGrid(); } });
            flpPaginationButtons.Controls.Add(btnPrev);

            for (int p = 1; p <= totalPages; p++)
            {
                if (p == 1 || p == totalPages || (p >= _currentPage - 1 && p <= _currentPage + 1))
                {
                    int pageNum = p;
                    var btnPage = CreatePageButton(pageNum.ToString(), pageNum == _currentPage, true, (s, e) => { _currentPage = pageNum; PopulateUserGrid(); });
                    flpPaginationButtons.Controls.Add(btnPage);
                }
                else if (p == _currentPage - 2 || p == _currentPage + 2)
                {
                    var btnDots = CreatePageButton("...", false, false, null);
                    flpPaginationButtons.Controls.Add(btnDots);
                }
            }

            var btnNext = CreatePageButton("›", false, _currentPage < totalPages, (s, e) => { if (_currentPage < totalPages) { _currentPage++; PopulateUserGrid(); } });
            flpPaginationButtons.Controls.Add(btnNext);

            var btnLast = CreatePageButton("»", false, _currentPage < totalPages, (s, e) => { _currentPage = totalPages; PopulateUserGrid(); });
            flpPaginationButtons.Controls.Add(btnLast);
        }

        private static Button CreatePageButton(string text, bool isActive, bool isEnabled, EventHandler? onClick)
        {
            Button btn = new()
            {
                Text = text,
                Size = new Size(38, 40),
                Margin = new Padding(2, 6, 2, 6),
                FlatStyle = FlatStyle.Flat,
                Enabled = isEnabled,
                Cursor = isEnabled ? Cursors.Hand : Cursors.Default,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(0, 0, 0, 2),
                UseCompatibleTextRendering = true
            };
            btn.FlatAppearance.BorderSize = 1;

            if (text == "...")
            {
                btn.BackColor = Color.Transparent;
                btn.ForeColor = Color.FromArgb(100, 116, 139);
                btn.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font95Bold;
                btn.FlatAppearance.BorderSize = 0;
                btn.Enabled = false;
                btn.Cursor = Cursors.Default;
                AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btn, 8, Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent);
            }
            else if (isActive)
            {
                btn.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font95Bold;
                AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btn, 8, Color.FromArgb(250, 204, 21), Color.FromArgb(254, 216, 53), Color.FromArgb(234, 186, 10), Color.Transparent);
            }
            else if (!isEnabled)
            {
                btn.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font95Regular;
                AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btn, 8, Color.FromArgb(248, 250, 252), Color.FromArgb(248, 250, 252), Color.FromArgb(248, 250, 252), Color.Transparent);
            }
            else
            {
                btn.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font95Regular;
                AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btn, 8, Color.White, Color.FromArgb(248, 250, 252), Color.FromArgb(241, 245, 249), Color.Transparent);
            }

            if (onClick is not null)
            {
                btn.Click += onClick;
            }

            return btn;
        }

        /// Event handler to refresh grid when search criteria or dropdown options change.
        private void FilterChanged(object? sender, EventArgs e)
        {
            _currentPage = 1;
            PopulateUserGrid();
        }

        /// Event handler to query fresh database data when refresh button is clicked.
        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            LoadDatabaseData();
        }

        /// Opens the Add User modal dialog and refreshes database records if a new user was created.
        private void BtnAddUser_Click(object? sender, EventArgs e)
        {
            using var modal = new AddUserModalForm(_userService);
            if (modal.ShowDialog(this.FindForm()) == DialogResult.OK)
            {
                LoadDatabaseData();
            }
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
                
                lblValLogin.Text = ": " + (row.Cells["colLastLogin"].Value?.ToString() ?? "-").Replace("\r\n", " ");
                lblValCreated.Text = ": " + (row.Cells["colCreated"].Value?.ToString() ?? "-").Replace("\r\n", " ");
            }
        }

        /// Builds user management grid columns dynamically on page initialization.
        private void SetupGridColumns()
        {
            gridUsers.Columns.Clear();

            gridUsers.BackgroundColor = Color.White;
            gridUsers.BorderStyle = BorderStyle.FixedSingle;
            gridUsers.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            gridUsers.GridColor = Color.FromArgb(203, 213, 225);
            gridUsers.RowHeadersVisible = false;
            gridUsers.AllowUserToAddRows = false;
            gridUsers.AllowUserToDeleteRows = false;
            gridUsers.AllowUserToResizeRows = false;
            gridUsers.ReadOnly = true;
            gridUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridUsers.MultiSelect = false;
            AssemblyLineTester.Helpers.UIStyleHelper.StyleDataGridView(gridUsers);
            gridUsers.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            gridUsers.ColumnHeadersHeight = 46;
            gridUsers.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            gridUsers.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            gridUsers.RowTemplate.Height = 56;

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
            gridUsers.Columns[cStatus].FillWeight = 75F;
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
            if (e.ColumnIndex >= 0 && gridUsers.Columns[e.ColumnIndex].Name == "colAction" && e.CellStyle is not null)
            {
                e.CellStyle.ForeColor = Color.FromArgb(71, 85, 105);
                e.CellStyle.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font11Regular;
            }
        }

        private void GridUsers_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && gridUsers.Columns[e.ColumnIndex].Name == "colStatus" && e.Value is not null && e.Graphics is not null)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                
                string status = e.Value.ToString() ?? "";
                Color pillColor = status == "Active" ? Color.FromArgb(220, 252, 231) : (status == "Locked" ? Color.FromArgb(254, 226, 226) : Color.FromArgb(226, 232, 240));
                Color textColor = status == "Active" ? Color.FromArgb(22, 101, 52) : (status == "Locked" ? Color.FromArgb(153, 27, 27) : Color.FromArgb(71, 85, 105));
                
                Size textSize = TextRenderer.MeasureText(status, AssemblyLineTester.Helpers.UIStyleHelper.Font85Bold);
                int badgeWidth = Math.Max(72, textSize.Width + 20);
                int badgeHeight = 26;
                int x = e.CellBounds.X + (e.CellBounds.Width - badgeWidth) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - badgeHeight) / 2;
                Rectangle badgeRect = new(x, y, badgeWidth, badgeHeight);

                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using var brush = new SolidBrush(pillColor);
                using var path = AssemblyLineTester.Helpers.UIStyleHelper.GetRoundedRectPath(badgeRect, 6);
                e.Graphics.FillPath(brush, path);

                TextRenderer.DrawText(e.Graphics, status, AssemblyLineTester.Helpers.UIStyleHelper.Font85Bold, badgeRect, textColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                e.Handled = true;
            }
        }



        /// Draws the Doughnut Chart dynamically based on the proportion of user roles in the database.
        private void PnlRoleChart_Paint(object? sender, PaintEventArgs e)
        {
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

            int innerSize = (int)(size * 0.85);
            Rectangle innerRect = new(chartRect.X + (size - innerSize) / 2, chartRect.Y + (size - innerSize) / 2, innerSize, innerSize);
            using SolidBrush bCenter = new(Color.White);
            g.FillEllipse(bCenter, innerRect);

            using SolidBrush bTxt = new(Color.FromArgb(30, 41, 59));
            using StringFormat sf = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g.DrawString($"{_totalUserCount}" + Environment.NewLine + "TOTAL", AssemblyLineTester.Helpers.UIStyleHelper.Font16Bold, bTxt, chartRect, sf);
        }

        private void SetupEvents()
        {
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardKpi1, 10, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardKpi2, 10, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardKpi3, 10, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardKpi4, 10, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardKpi5, 10, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardKpi6, 10, Color.FromArgb(34, 40, 49), 2.5f);

            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(pnlFilterCard, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardUserList, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardUserDetails, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardRoleSummary, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardUserActivity, 12, Color.FromArgb(34, 40, 49), 2.5f);

            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnExport, 10, Color.White, Color.FromArgb(248, 250, 252), Color.FromArgb(241, 245, 249), Color.Transparent);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnRefresh, 10, Color.White, Color.FromArgb(248, 250, 252), Color.FromArgb(241, 245, 249), Color.Transparent);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnAddUser, 10, Color.FromArgb(250, 204, 21), Color.FromArgb(254, 216, 53), Color.FromArgb(234, 186, 10), Color.Transparent);
        }


    }
}
