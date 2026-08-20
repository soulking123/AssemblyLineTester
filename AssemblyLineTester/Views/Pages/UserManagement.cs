using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using AssemblyLineTester.Models;
using AssemblyLineTester.Services;
using AssemblyLineTester.Views.Modals;
using AssemblyLineTester.Helpers;

namespace AssemblyLineTester.Views.Pages
{
    public partial class UserManagement : UserControl
    {
        private readonly UserService _userService;
        private List<User> _allUsers = [];

        // The rows currently listed, after search and the three filter dropdowns.
        private List<User> _visibleUsers = [];
        private Dictionary<string, int> _roleCounts = [];
        private int _totalUserCount;
        private int _currentPage = 1;
        private int _pageSize = 10;
        private readonly ComboBox cmbPageSize = new();

        private const string ActivateLabel = "Activate";
        private const string DeactivateLabel = "Deactivate";

        // Row whose action button the pointer is over, or -1.
        private int _hoveredActionRow = -1;

        // Pill text is drawn, so it never passes through the page's AutoScale pass.
        private Font _pillFont = AssemblyLineTester.Helpers.UIStyleHelper.Font85Bold;
        private float _uiScale = 1f;

        // Height the filter inputs settled on, so the action buttons can match them.
        private int _filterInputHeight;

        public UserManagement(UserService userService)
        {
            _userService = userService;
            InitializeComponent();
            InitializeFilters();
            SetupGridColumns();
            InitializePaginationControls();

            txtSearch.TextChanged += FilterChanged;
            cmbFilterStatus.SelectedIndexChanged += FilterChanged;
            cmbFilterRole.SelectedIndexChanged += FilterChanged;
            cmbFilterDept.SelectedIndexChanged += FilterChanged;
            btnRefresh.Click += BtnRefresh_Click;
            btnAddUser.Click += BtnAddUser_Click;
            btnExport.Click += BtnExport_Click;
            btnManageRolePermissions.Click += BtnManageRolePermissions_Click;

            gridUsers.CellFormatting += GridUsers_CellFormatting;
            gridUsers.CellPainting += GridUsers_CellPainting;
            gridUsers.SelectionChanged += GridUsers_SelectionChanged;
            gridUsers.CellClick += GridUsers_CellClick;
            gridUsers.CellMouseMove += GridUsers_CellMouseMove;
            gridUsers.MouseLeave += GridUsers_MouseLeave;

            chartRole.PostPaint += ChartRole_PostPaint;

            // Loaded on display, not in the constructor.
            this.Load += UserManagement_Load;
            SetupEvents();
        }

        private void UserManagement_Load(object? sender, EventArgs e)
        {
            if (!DesignMode)
            {
                _uiScale = AssemblyLineTester.Helpers.UIStyleHelper.GetHostScaleFactor(this);
                AssemblyLineTester.Helpers.UIStyleHelper.ScaleExplicitFonts(this, _uiScale);
                _pillFont = AssemblyLineTester.Helpers.UIStyleHelper.ScaleFont(AssemblyLineTester.Helpers.UIStyleHelper.Font85Bold, _uiScale);
            }
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

            float scaleX = this.CurrentAutoScaleDimensions.Width / 13f;
            float scaleY = this.CurrentAutoScaleDimensions.Height / 32f;

            Label lblShow = new()
            {
                Text = "Show",
                AutoSize = true,
                Margin = new Padding(0, (int)(14 * scaleY), (int)(5 * scaleX), 0),
                ForeColor = Color.FromArgb(71, 85, 105)
            };

            cmbPageSize.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPageSize.Items.AddRange(["10", "25", "50", "100"]);
            cmbPageSize.SelectedIndex = 0;
            cmbPageSize.Size = new Size((int)(65 * scaleX), (int)(30 * scaleY));
            cmbPageSize.Margin = new Padding(0, (int)(10 * scaleY), 0, 0);
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
                Margin = new Padding((int)(5 * scaleX), (int)(14 * scaleY), 0, 0),
                ForeColor = Color.FromArgb(71, 85, 105)
            };

            flpPageSize.Controls.Add(lblShow);
            flpPageSize.Controls.Add(cmbPageSize);
            flpPageSize.Controls.Add(lblEntries);

            tblPagination.Controls.Add(flpPageSize, 1, 0);
        }

        // Fills the Status, Role and Department filter dropdowns.
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

        // Reloads accounts and refreshes the KPIs, chart and grid.
        private void LoadDatabaseData()
        {
            try
            {
                // The admin account is listed like any other, or its shipped password is unreachable.
                _allUsers = [.. _userService.GetAllUsers().OrderBy(u => u.UserId)];

                _totalUserCount = _allUsers.Count;
                // Counted by seeded id; the names are editable and renaming one zeroed these.
                int activeUsers = _allUsers.Count(u => u.StatusId == UserStatus.Active);
                int inactiveUsers = _allUsers.Count(u => u.StatusId == UserStatus.Inactive);
                int lockedUsers = _allUsers.Count(u => u.StatusId == UserStatus.Locked);
                // Local on both sides; CreatedAt is UTC and drifted across the month boundary.
                int newUsersThisMonth = _allUsers.Count(u =>
                    AppTime.ToLocal(u.CreatedAt).Month == DateTime.Now.Month &&
                    AppTime.ToLocal(u.CreatedAt).Year == DateTime.Now.Year);
                var latestUser = _allUsers.Where(u => u.LastLogin.HasValue).OrderByDescending(u => u.LastLogin).FirstOrDefault();

                lblKpiVal1.Text = _totalUserCount.ToString();
                lblKpiVal2.Text = activeUsers.ToString();
                lblKpiSub2.Text = $"Users ({(_totalUserCount > 0 ? (activeUsers * 100.0 / _totalUserCount) : 0):0.0}%)";
                lblKpiVal3.Text = inactiveUsers.ToString();
                lblKpiSub3.Text = $"Users ({(_totalUserCount > 0 ? (inactiveUsers * 100.0 / _totalUserCount) : 0):0.0}%)";
                lblKpiVal4.Text = lockedUsers.ToString();
                lblKpiSub4.Text = $"Users ({(_totalUserCount > 0 ? (lockedUsers * 100.0 / _totalUserCount) : 0):0.0}%)";
                lblKpiVal5.Text = latestUser?.FullName ?? "-";
                lblKpiSub5.Text = AppTime.ToLocal(latestUser?.LastLogin)?.ToString("dd MMM yyyy hh:mm:ss tt") ?? "No logins";
                lblKpiVal6.Text = newUsersThisMonth.ToString();

                _roleCounts = _allUsers
                    .GroupBy(u => u.Role.Name)
                    .ToDictionary(g => g.Key, g => g.Count());

                lblLegendVal1.Text = _roleCounts.GetValueOrDefault("Administrator", 0).ToString();
                lblLegendVal2.Text = _roleCounts.GetValueOrDefault("Engineer", 0).ToString();
                lblLegendVal3.Text = _roleCounts.GetValueOrDefault("Operator", 0).ToString();
                lblLegendVal4.Text = _roleCounts.GetValueOrDefault("Technician", 0).ToString();
                lblLegendVal5.Text = _roleCounts.GetValueOrDefault("Quality", 0).ToString();
                lblLegendVal6.Text = _roleCounts.GetValueOrDefault("Supervisor", 0).ToString();

                var actStats = _userService.GetMonthlyActivityStats();
                lblActVal1.Text = actStats.NewUsers.ToString();
                lblActVal2.Text = actStats.ActiveLogins.ToString();
                lblActVal3.Text = actStats.PasswordChanges.ToString();
                lblActVal4.Text = actStats.FailedLoginAttempts.ToString();

                LoadRoleChart();
                PopulateUserGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load user database:\n\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Reapplies the search box and filter dropdowns, then repaints the grid.
        private void PopulateUserGrid()
        {
            gridUsers.Rows.Clear();
            _hoveredActionRow = -1;

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

            // Kept so Export writes what the user is looking at, not the whole company.
            _visibleUsers = filteredList;

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
                int rowIndex = gridUsers.Rows.Add(
                    index++,
                    user.Username,
                    user.FullName,
                    $"{user.Username}@caterpillar.com",
                    user.Role.Name,
                    user.Department,
                    user.Status.Name,
                    // Both converted from storage; these two used to sit seven hours apart.
                    AppTime.ToLocal(user.LastLogin)?.ToString("dd MMM yyyy\r\nhh:mm:ss tt") ?? "-",
                    AppTime.ToLocal(user.CreatedAt).ToString("dd MMM yyyy\r\nhh:mm:ss tt"),
                    user.StatusId == UserStatus.Active ? DeactivateLabel : ActivateLabel
                );

                // Carries the account through paging, so no handler maps a row index back.
                gridUsers.Rows[rowIndex].Tag = user;
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

            UIStyleHelper.ClearAndDispose(flpPaginationButtons.Controls);

            float scaleX = this.CurrentAutoScaleDimensions.Width / 13f;
            float scaleY = this.CurrentAutoScaleDimensions.Height / 32f;

            var btnFirst = CreatePageButton("«", false, _currentPage > 1, (s, e) => { _currentPage = 1; PopulateUserGrid(); }, scaleX, scaleY);
            flpPaginationButtons.Controls.Add(btnFirst);

            var btnPrev = CreatePageButton("‹", false, _currentPage > 1, (s, e) => { if (_currentPage > 1) { _currentPage--; PopulateUserGrid(); } }, scaleX, scaleY);
            flpPaginationButtons.Controls.Add(btnPrev);

            for (int p = 1; p <= totalPages; p++)
            {
                if (p == 1 || p == totalPages || (p >= _currentPage - 1 && p <= _currentPage + 1))
                {
                    int pageNum = p;
                    var btnPage = CreatePageButton(pageNum.ToString(), pageNum == _currentPage, true, (s, e) => { _currentPage = pageNum; PopulateUserGrid(); }, scaleX, scaleY);
                    flpPaginationButtons.Controls.Add(btnPage);
                }
                else if (p == _currentPage - 2 || p == _currentPage + 2)
                {
                    var btnDots = CreatePageButton("...", false, false, null, scaleX, scaleY);
                    flpPaginationButtons.Controls.Add(btnDots);
                }
            }

            var btnNext = CreatePageButton("›", false, _currentPage < totalPages, (s, e) => { if (_currentPage < totalPages) { _currentPage++; PopulateUserGrid(); } }, scaleX, scaleY);
            flpPaginationButtons.Controls.Add(btnNext);

            var btnLast = CreatePageButton("»", false, _currentPage < totalPages, (s, e) => { _currentPage = totalPages; PopulateUserGrid(); }, scaleX, scaleY);
            flpPaginationButtons.Controls.Add(btnLast);
        }

        private static Button CreatePageButton(string text, bool isActive, bool isEnabled, EventHandler? onClick, float scaleX, float scaleY)
        {
            Button btn = new()
            {
                Text = text,
                Size = new Size((int)(38 * scaleX), (int)(40 * scaleY)),
                Margin = new Padding((int)(2 * scaleX), (int)(6 * scaleY), (int)(2 * scaleX), (int)(6 * scaleY)),
                FlatStyle = FlatStyle.Flat,
                Enabled = isEnabled,
                Cursor = isEnabled ? Cursors.Hand : Cursors.Default,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(0, 0, 0, (int)(2 * scaleY)),
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

        private void FilterChanged(object? sender, EventArgs e)
        {
            _currentPage = 1;
            PopulateUserGrid();
        }

        // Activates or deactivates the clicked account, granting or withdrawing access.
        private void GridUsers_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (gridUsers.Columns[e.ColumnIndex].Name != "colAction") return;
            if (gridUsers.Rows[e.RowIndex].Tag is not User user) return;

            bool activating = user.StatusId != UserStatus.Active;
            int targetStatus = activating ? UserStatus.Active : UserStatus.Inactive;

            string question = activating
                ? $"Allow '{user.FullName}' to sign in again?"
                : $"Block '{user.FullName}' from signing in?\r\n\r\nA session they already have open stays active until they log out.";

            var answer = MessageBox.Show(
                question,
                activating ? "Activate account" : "Deactivate account",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (answer != DialogResult.Yes) return;

            try
            {
                _userService.UpdateUserStatus(user.UserId, targetStatus);
                LoadDatabaseData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cannot change status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void GridUsers_CellMouseMove(object? sender, DataGridViewCellMouseEventArgs e)
        {
            bool onAction = e.RowIndex >= 0
                && e.ColumnIndex >= 0
                && gridUsers.Columns[e.ColumnIndex].Name == "colAction";

            gridUsers.Cursor = onAction ? Cursors.Hand : Cursors.Default;
            SetHoveredActionRow(onAction ? e.RowIndex : -1);
        }

        private void GridUsers_MouseLeave(object? sender, EventArgs e)
        {
            gridUsers.Cursor = Cursors.Default;
            SetHoveredActionRow(-1);
        }

        // Repaints only the rows entering and leaving hover, not the whole grid.
        private void SetHoveredActionRow(int rowIndex)
        {
            if (rowIndex == _hoveredActionRow) return;

            int previous = _hoveredActionRow;
            _hoveredActionRow = rowIndex;

            if (previous >= 0 && previous < gridUsers.Rows.Count) gridUsers.InvalidateRow(previous);
            if (rowIndex >= 0 && rowIndex < gridUsers.Rows.Count) gridUsers.InvalidateRow(rowIndex);
        }

        // Writes the accounts currently listed to an .xlsx.
        private void BtnExport_Click(object? sender, EventArgs e)
        {
            using var dialog = new SaveFileDialog
            {
                Filter = "Excel workbook (*.xlsx)|*.xlsx",
                FileName = $"user-accounts-{DateTime.Now:yyyyMMdd-HHmmss}.xlsx"
            };

            if (dialog.ShowDialog() != DialogResult.OK) return;

            System.Windows.Forms.Cursor previous = Cursor;
            Cursor = Cursors.WaitCursor;
            try
            {
                using var wb = new ClosedXML.Excel.XLWorkbook();
                var ws = wb.Worksheets.Add("Users");

                string[] headers = ["No.", "Username", "Full Name", "Role", "Department", "Status", "Last Login", "Created"];
                for (int c = 0; c < headers.Length; c++)
                {
                    var cell = ws.Cell(1, c + 1);
                    cell.Value = headers[c];
                    cell.Style.Font.Bold = true;
                    cell.Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.FromArgb(230, 230, 230);
                    cell.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                }

                int r = 2;
                foreach (var user in _visibleUsers)
                {
                    ws.Cell(r, 1).Value = r - 1;
                    ws.Cell(r, 2).Value = user.Username;
                    ws.Cell(r, 3).Value = user.FullName;
                    ws.Cell(r, 4).Value = user.Role.Name;
                    ws.Cell(r, 5).Value = user.Department;
                    ws.Cell(r, 6).Value = user.Status.Name;

                    // Real DateTime values, so the column sorts by time.
                    if (user.LastLogin.HasValue)
                    {
                        ws.Cell(r, 7).Value = AppTime.ToLocal(user.LastLogin.Value);
                        ws.Cell(r, 7).Style.DateFormat.Format = "yyyy-mm-dd hh:mm";
                    }
                    else
                    {
                        ws.Cell(r, 7).Value = "never";
                    }

                    ws.Cell(r, 8).Value = AppTime.ToLocal(user.CreatedAt);
                    ws.Cell(r, 8).Style.DateFormat.Format = "yyyy-mm-dd hh:mm";
                    r++;
                }

                ws.Columns().AdjustToContents();
                ws.SheetView.FreezeRows(1);
                if (_visibleUsers.Count > 0) ws.Range(1, 1, r - 1, headers.Length).SetAutoFilter();

                wb.SaveAs(dialog.FileName);

                new SystemLogService().Log(Models.SystemLog.LogSeverity.Info, "User Management",
                    "Export Users", $"{_visibleUsers.Count} user accounts exported to Excel.");

                MessageBox.Show($"{_visibleUsers.Count} accounts exported.", "Export complete",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                FileLogService.Error("UserManagement.BtnExport_Click", "User export failed.", ex);
                MessageBox.Show($"Export failed: {ex.Message}", "Export", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = previous;
            }
        }

        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            LoadDatabaseData();
        }

        // Opens the Add User modal and reloads if an account was created.
        private void BtnAddUser_Click(object? sender, EventArgs e)
        {
            using var modal = new AddUserModalForm(_userService);
            if (modal.ShowDialog(this.FindForm()) == DialogResult.OK)
            {
                LoadDatabaseData();
            }
        }

        // Fills the side panel from the selected row.
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

        // Builds the grid columns in code.
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
            gridUsers.Columns[cAction].FillWeight = 95F;
            gridUsers.Columns[cAction].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        // Colours the status pill from the account state.
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
                
                Size textSize = TextRenderer.MeasureText(status, _pillFont);
                int badgeWidth = Math.Max(Scaled(72), textSize.Width + Scaled(20));
                int badgeHeight = Scaled(26);
                int x = e.CellBounds.X + (e.CellBounds.Width - badgeWidth) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - badgeHeight) / 2;
                Rectangle badgeRect = new(x, y, badgeWidth, badgeHeight);

                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using var brush = new SolidBrush(pillColor);
                using var path = AssemblyLineTester.Helpers.UIStyleHelper.GetRoundedRectPath(badgeRect, Scaled(6));
                e.Graphics.FillPath(brush, path);

                TextRenderer.DrawText(e.Graphics, status, _pillFont, badgeRect, textColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.NoPadding);

                e.Handled = true;
                return;
            }

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && gridUsers.Columns[e.ColumnIndex].Name == "colAction" && e.Value is not null && e.Graphics is not null)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                string label = e.Value.ToString() ?? string.Empty;
                if (label.Length == 0)
                {
                    e.Handled = true;
                    return;
                }

                // Outlined, so a pressable control reads apart from the solid status pill beside it.
                bool activates = label == ActivateLabel;
                bool hovered = e.RowIndex == _hoveredActionRow;

                Color borderColor = activates ? Color.FromArgb(134, 239, 172) : Color.FromArgb(252, 165, 165);
                Color labelColor = activates ? Color.FromArgb(21, 128, 61) : Color.FromArgb(185, 28, 28);
                Color fillColor = hovered
                    ? (activates ? Color.FromArgb(220, 252, 231) : Color.FromArgb(254, 226, 226))
                    : Color.White;

                Size labelSize = TextRenderer.MeasureText(label, _pillFont);
                int btnHeight = Scaled(28);
                int btnWidth = Math.Min(e.CellBounds.Width - Scaled(8), labelSize.Width + Scaled(28));
                int bx = e.CellBounds.X + (e.CellBounds.Width - btnWidth) / 2;
                int by = e.CellBounds.Y + (e.CellBounds.Height - btnHeight) / 2;
                Rectangle btnRect = new(bx, by, btnWidth, btnHeight);

                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var fill = new SolidBrush(fillColor))
                using (var shape = AssemblyLineTester.Helpers.UIStyleHelper.GetRoundedRectPath(btnRect, Scaled(6)))
                {
                    e.Graphics.FillPath(fill, shape);
                    using var pen = new Pen(borderColor, 1.4f * _uiScale);
                    e.Graphics.DrawPath(pen, shape);
                }

                TextRenderer.DrawText(e.Graphics, label, _pillFont, btnRect, labelColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.NoPadding);

                e.Handled = true;
            }
        }

        // Converts a design-time pixel measurement into this machine's pixels.
        private int Scaled(int value) => Math.Max(1, (int)Math.Round(value * _uiScale));


        private void LoadRoleChart()
        {
            chartRole.ChartAreas.Clear();
            chartRole.Series.Clear();
            chartRole.Legends.Clear();
            chartRole.BackColor = Color.White;

            ChartArea areaPie = new("PieArea") { BackColor = Color.White };
            chartRole.ChartAreas.Add(areaPie);

            Series sRole = new("Roles") { ChartType = SeriesChartType.Doughnut };
            // Make the Doughnut ring relatively thin to match previous visual style
            sRole["DoughnutRadius"] = "25";

            var roleColors = new Dictionary<string, Color>
            {
                { "Administrator", Color.FromArgb(250, 204, 21) },
                { "Engineer", Color.FromArgb(34, 197, 94) },
                { "Operator", Color.FromArgb(59, 130, 246) },
                { "Technician", Color.FromArgb(168, 85, 247) },
                { "Quality", Color.FromArgb(239, 68, 68) },
                { "Supervisor", Color.FromArgb(148, 163, 184) }
            };

            if (_totalUserCount == 0)
            {
                int emptyIdx = sRole.Points.AddY(1);
                sRole.Points[emptyIdx].Color = Color.FromArgb(226, 232, 240);
                sRole.Points[emptyIdx].IsVisibleInLegend = false;
            }
            else
            {
                foreach (var role in roleColors)
                {
                    int count = _roleCounts.GetValueOrDefault(role.Key, 0);
                    if (count > 0)
                    {
                        int idx = sRole.Points.AddY(count);
                        sRole.Points[idx].Color = role.Value;
                    }
                }
            }

            chartRole.Series.Add(sRole);
        }

        private void ChartRole_PostPaint(object? sender, ChartPaintEventArgs e)
        {
            if (e.ChartElement is ChartArea area)
            {
                string numText = _totalUserCount.ToString();
                string subText = "TOTAL";

                using SolidBrush brushNum = new(Color.FromArgb(30, 41, 59));
                using SolidBrush brushSub = new(Color.FromArgb(100, 116, 139));
                using StringFormat sf = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

                RectangleF chartRect = e.ChartGraphics.GetAbsoluteRectangle(area.Position.ToRectangleF());
                float centerX = chartRect.Left + (chartRect.Width * 0.50f);
                float centerY = chartRect.Top + (chartRect.Height * 0.5f);

                e.ChartGraphics.Graphics.DrawString(numText, AssemblyLineTester.Helpers.UIStyleHelper.Font16Bold, brushNum, centerX, centerY - 14, sf);
                e.ChartGraphics.Graphics.DrawString(subText, AssemblyLineTester.Helpers.UIStyleHelper.Font85Bold, brushSub, centerX, centerY + 14, sf);
            }
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

            StyleFilterSection();
            StyleActionBar();
        }

        // One treatment for the whole filter row, measured through the DPI scale.
        private void StyleFilterSection()
        {
            float scale = AssemblyLineTester.Helpers.UIStyleHelper.GetHostScaleFactor(this);
            int S(int v) => Math.Max(1, (int)Math.Round(v * scale));

            Font labelFont = AssemblyLineTester.Helpers.UIStyleHelper.ScaleFont(
                AssemblyLineTester.Helpers.UIStyleHelper.Font9Bold, scale);
            Font inputFont = AssemblyLineTester.Helpers.UIStyleHelper.ScaleFont(
                AssemblyLineTester.Helpers.UIStyleHelper.Font105Regular, scale);

            Color labelInk = Color.FromArgb(71, 85, 105);
            Color inputInk = Color.FromArgb(30, 41, 59);
            Color surface = Color.FromArgb(248, 250, 252);
            int inputHeight = S(46);

            var lblFilterSearch = new Label
            {
                Text = "SEARCH",
                Font = labelFont,
                ForeColor = labelInk,
                AutoSize = true,
                Margin = lblFilterStatus.Margin,
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom
            };
            tblFilterInputs.Controls.Add(lblFilterSearch, 0, 0);

            lblFilterStatus.Text = "STATUS";
            lblFilterRole.Text = "ROLE";
            lblFilterDept.Text = "DEPARTMENT";

            foreach (Label caption in new[] { lblFilterStatus, lblFilterRole, lblFilterDept })
            {
                caption.Font = labelFont;
                caption.ForeColor = labelInk;
                caption.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            }

            foreach (ComboBox combo in new[] { cmbFilterStatus, cmbFilterRole, cmbFilterDept })
            {
                combo.Font = inputFont;
                combo.ForeColor = inputInk;
                combo.BackColor = surface;
                combo.FlatStyle = FlatStyle.Flat;
                combo.DropDownStyle = ComboBoxStyle.DropDownList;
                combo.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            }

            // A ComboBox refuses a Height, so the text box matches the combos instead.
            inputHeight = cmbFilterStatus.Height;

            txtSearch.Font = inputFont;
            txtSearch.ForeColor = inputInk;
            txtSearch.BackColor = surface;
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.PlaceholderText = "Username, full name or email";
            txtSearch.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            txtSearch.Height = inputHeight;

            _filterInputHeight = inputHeight;
        }

        // One filled button, the rest outlined; every width comes from the measured caption.
        private void StyleActionBar()
        {
            float scale = AssemblyLineTester.Helpers.UIStyleHelper.GetHostScaleFactor(this);
            int S(int v) => Math.Max(1, (int)Math.Round(v * scale));

            Font label = AssemblyLineTester.Helpers.UIStyleHelper.ScaleFont(
                AssemblyLineTester.Helpers.UIStyleHelper.Font95Bold, scale);

            btnExport.Text = "Export";
            btnRefresh.Text = "Refresh";
            btnManageRolePermissions.Text = "Role Permissions";
            btnAddUser.Text = "＋  Add New User";

            // Height comes from the inputs beside them, so the row shares one baseline.
            int height = _filterInputHeight > 0 ? _filterInputHeight : S(46);
            var bar = new[] { btnExport, btnRefresh, btnManageRolePermissions, btnAddUser };

            foreach (Button button in bar)
            {
                button.Font = label;
                button.Cursor = Cursors.Hand;
                button.Margin = new Padding(0, 0, S(10), 0);

                int textWidth = TextRenderer.MeasureText(button.Text, label).Width;
                button.Size = new Size(textWidth + S(46), height);
            }

            btnAddUser.Margin = new Padding(0);

            flpActionButtons.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            flpActionButtons.Margin = new Padding(S(20), 0, 0, 0);
            flpActionButtons.Padding = new Padding(0, S(31), 0, 0);

            foreach (Button button in new[] { btnExport, btnRefresh, btnManageRolePermissions })
            {
                button.ForeColor = Color.FromArgb(51, 65, 85);
                AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(button, 10,
                    Color.White,
                    Color.FromArgb(248, 250, 252),
                    Color.FromArgb(241, 245, 249),
                    Color.FromArgb(203, 213, 225));
            }

            btnAddUser.ForeColor = Color.FromArgb(15, 23, 42);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnAddUser, 10,
                Color.FromArgb(250, 204, 21),
                Color.FromArgb(254, 216, 53),
                Color.FromArgb(234, 186, 10),
                Color.Transparent);
        }

        private void BtnManageRolePermissions_Click(object? sender, EventArgs e)
        {
            var mainForm = this.FindForm() as MainForm;
            if (mainForm != null)
            {
                mainForm.OpenRolePermissionsSettings();
            }
            else
            {
                MessageBox.Show("Please navigate to Settings -> Users and Roles to configure permissions.", "Role Permissions", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
