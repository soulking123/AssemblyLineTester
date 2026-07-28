using AssemblyLineTester.Views.Pages;
using AssemblyLineTester.Services;

namespace AssemblyLineTester.Views
{
    /// The main shell window for the application containing sidebar navigation and loaded subpages.
    public partial class MainForm : Form
    {
        private readonly UserService _userService = new();
        private bool isSidebarExpanded = true;
        private int expandedWidth = 380;
        private int collapsedWidth = 90;

        private Size _expandedLogoSize;
        private Point _expandedToggleLoc;
        private Size _expandedToggleSize;
        private Size _expandedLblMenuSize;
        private Point _expandedLblMenuLoc;
        private int _expandedMenuStartY;
        private int _expandedMenuX;
        private int _expandedMenuWidth;
        private int _expandedMenuGap;

        public bool IsLoggingOut { get; private set; } = false;

        private TestHistory? pageTestHistory;
        private SystemLog? pageSystemLog;
        private HarnessDatabase? pageHarnessDb;
        private Dashboard? pageDashboard;
        private TestExecution? pageTestExecution;
        private ManualTestingPage? pageManualTesting;
        private Reports? pageReports;
        private Settings? pageSettings;
        private UserManagement? pageUserManagement;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;

            AssemblyLineTester.Helpers.UIStyleHelper.ScaleExplicitFonts(this, AssemblyLineTester.Helpers.UIStyleHelper.GetHostScaleFactor(this));
            SetupUserDropdown();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (DesignMode) return;

            // After the window is on screen, not during Load: the user should see the application
            // they signed into behind the dialog, so it reads as a step in setting up rather than
            // as a login that failed.
            if (SessionManager.CurrentUser?.MustChangePassword == true)
            {
                PromptPasswordChange(mandatory: true);
            }
        }

        /// Initializes a new instance of the MainForm class.
        public MainForm()
        {
            InitializeComponent();
            EnableDoubleBuffering();

            // Load CAT logo image into sidebar menu header and app icon
            try
            {
                string logoPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "cat_logo.png");
                if (System.IO.File.Exists(logoPath))
                {
                    lblMenu.Image = Image.FromFile(logoPath);
                }

                string iconPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "cat_logo.ico");
                if (System.IO.File.Exists(iconPath))
                {
                    this.Icon = new Icon(iconPath);
                }
            }
            catch { /* fallback silently */ }
            this.WindowState = FormWindowState.Maximized;
            expandedWidth = splitContainer1.SplitterDistance;
            collapsedWidth = (int)(expandedWidth * (90f / 380f));

            _expandedLogoSize = pnlLogoHeader.Size;
            _expandedToggleLoc = btnToggleSidebar.Location;
            _expandedToggleSize = btnToggleSidebar.Size;
            _expandedLblMenuSize = lblMenu.Size;
            _expandedLblMenuLoc = lblMenu.Location;
            _expandedMenuStartY = btnDashboard.Location.Y;
            _expandedMenuX = btnDashboard.Location.X;
            _expandedMenuWidth = btnDashboard.Width;
            _expandedMenuGap = btnTestExecution.Location.Y - btnDashboard.Location.Y - btnDashboard.Height;

            // Apply dynamic Role-Based Access Control permissions to sidebar buttons
            ApplyRolePermissions();

            ApplySidebarStyles();
            InitializeUserInfo();

            this.Load += (s, e) => RepositionSidebarButtons();

            // Wire up header click events to auto-close dropdown when clicking outside
            pnlTopHeader.Click += CloseDropdownIfOpen;
            lblHeaderTitle.Click += CloseDropdownIfOpen;
            pnlRight.Click += CloseDropdownIfOpen;
            pnlDateTime.Click += CloseDropdownIfOpen;
            lblDateText.Click += CloseDropdownIfOpen;
            lblTimeText.Click += CloseDropdownIfOpen;

            // Wire up header dragging for borderless form
            pnlTopHeader.MouseDown += Header_MouseDown;
            lblHeaderTitle.MouseDown += Header_MouseDown;
            pnlRight.MouseDown += Header_MouseDown;
            pnlDateTime.MouseDown += Header_MouseDown;
            lblDateText.MouseDown += Header_MouseDown;
            lblTimeText.MouseDown += Header_MouseDown;

            btnMinimize.Click += (s, e) => { this.WindowState = FormWindowState.Minimized; };
            btnMaximize.Click += (s, e) => { ToggleMaximize(); };
            btnClose.Click += (s, e) => { this.Close(); };

            btnMinimize.MouseEnter += (s, e) => { btnMinimize.BackColor = Color.FromArgb(226, 232, 240); btnMinimize.ForeColor = Color.FromArgb(15, 23, 42); };
            btnMinimize.MouseLeave += (s, e) => { btnMinimize.BackColor = Color.Transparent; btnMinimize.ForeColor = Color.FromArgb(100, 116, 139); };

            btnMaximize.MouseEnter += (s, e) => { btnMaximize.BackColor = Color.FromArgb(226, 232, 240); btnMaximize.ForeColor = Color.FromArgb(15, 23, 42); };
            btnMaximize.MouseLeave += (s, e) => { btnMaximize.BackColor = Color.Transparent; btnMaximize.ForeColor = Color.FromArgb(100, 116, 139); };

            btnClose.MouseEnter += (s, e) => { btnClose.BackColor = Color.FromArgb(239, 68, 68); btnClose.ForeColor = Color.White; };
            btnClose.MouseLeave += (s, e) => { btnClose.BackColor = Color.Transparent; btnClose.ForeColor = Color.FromArgb(100, 116, 139); };

            // Fix explicitly set Designer fonts that do not auto-scale
            float scaleY = this.CurrentAutoScaleDimensions.Height / 32f;
            btnMaximize.Font = new Font(btnMaximize.Font.FontFamily, btnMaximize.Font.Size * scaleY, btnMaximize.Font.Style);
            btnMinimize.Font = new Font(btnMinimize.Font.FontFamily, btnMinimize.Font.Size * scaleY, btnMinimize.Font.Style);
            btnClose.Font = new Font(btnClose.Font.FontFamily, btnClose.Font.Size * scaleY, btnClose.Font.Style);

            this.Resize += MainForm_Resize;

            SetActiveMenu(btnDashboard, GetOrInitPage("btnDashboard"));
        }

        /// <summary>
        /// Updates sidebar navigation button visibility based on the active user's role and granular permissions.
        /// </summary>
        public void ApplyRolePermissions()
        {
            btnDashboard.Visible = SessionManager.HasPermission("Page.Dashboard");
            btnTestExecution.Visible = SessionManager.HasPermission("Page.TestExecution");
            btnManualTesting.Visible = SessionManager.HasPermission("Page.TestExecution");
            btnHarnessDatabase.Visible = SessionManager.HasPermission("Page.HarnessDatabase");
            btnTestHistory.Visible = SessionManager.HasPermission("Page.TestHistory");
            btnReports.Visible = SessionManager.HasPermission("Page.Reports");
            btnSystemLog.Visible = SessionManager.HasPermission("Page.SystemLog");
            btnSettings.Visible = SessionManager.HasPermission("Page.Settings");
            btnUserManagement.Visible = SessionManager.HasPermission("Page.UserManagement");

            RepositionSidebarButtons();
        }

        private void EnableDoubleBuffering()
        {
            try
            {
                this.DoubleBuffered = true;
                EnableDoubleBufferingRecursive(this);
            }
            catch { /* ignore */ }
        }

        private static void EnableDoubleBufferingRecursive(Control control)
        {
            if (control is SplitContainer || control is SplitterPanel || control is Panel || control is TableLayoutPanel || control is FlowLayoutPanel)
            {
                try
                {
                    typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(control, true, null);
                }
                catch { /* ignore */ }
            }
            foreach (Control child in control.Controls)
            {
                EnableDoubleBufferingRecursive(child);
            }
        }

        private void InitializeUserInfo()
        {
            // Neutral fallbacks. A session that failed to load its user should read as unknown;
            // naming a real person, or captioning the header "Administrator", tells whoever is at
            // the machine something about their access that is not true.
            string fullName = SessionManager.CurrentUser?.FullName ?? string.Empty;
            lblUserName.Text = string.IsNullOrWhiteSpace(fullName) ? "Unknown user" : fullName;
            string roleName = SessionManager.CurrentUser?.Role?.Name ?? string.Empty;
            lblUserRole.Text = string.IsNullOrWhiteSpace(roleName) ? "No role" : roleName;
            lblDateText.Text = DateTime.Now.ToString("dd MMM yyyy");
            lblTimeText.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        private void PnlTopHeader_Paint(object? sender, PaintEventArgs e)
        {
            using var pen = new Pen(Color.FromArgb(226, 232, 240), 1);
            e.Graphics.DrawLine(pen, 0, pnlTopHeader.Height - 1, pnlTopHeader.Width, pnlTopHeader.Height - 1);
        }

        private void ClockTimer_Tick(object? sender, EventArgs e)
        {
            lblDateText.Text = DateTime.Now.ToString("dd MMM yyyy");
            lblTimeText.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        private void ApplySidebarStyles()
        {
            AssemblyLineTester.Views.Controls.MenuButton[] menuButtons = [btnDashboard, btnTestExecution, btnManualTesting, btnHarnessDatabase, btnTestHistory, btnReports, btnSystemLog, btnSettings, btnUserManagement];
            Font menuFont = new("Segoe UI", 11F, FontStyle.Regular);

            foreach (var btn in menuButtons)
            {
                btn.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatStyle = FlatStyle.Flat;
                btn.Font = menuFont;
                btn.ForeColor = Color.White;
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.Padding = new Padding(10, 0, 0, 0);
                btn.BackColor = Color.Transparent;
            }

            btnDashboard.EmojiIcon = "📊";
            btnDashboard.FullText = "  📊    Dashboard";
            btnDashboard.Text = "  📊    Dashboard";

            btnTestExecution.EmojiIcon = "⚡";
            btnTestExecution.FullText = "  ⚡    Test Execution";
            btnTestExecution.Text = "  ⚡    Test Execution";

            btnManualTesting.EmojiIcon = "🕹️";
            btnManualTesting.FullText = "  🕹️    Manual Testing";
            btnManualTesting.Text = "  🕹️    Manual Testing";

            btnHarnessDatabase.EmojiIcon = "🗄️";
            btnHarnessDatabase.FullText = "  🗄️    Harness Database";
            btnHarnessDatabase.Text = "  🗄️    Harness Database";

            btnTestHistory.EmojiIcon = "📜";
            btnTestHistory.FullText = "  📜    Test History";
            btnTestHistory.Text = "  📜    Test History";

            btnReports.EmojiIcon = "📑";
            btnReports.FullText = "  📑    Reports";
            btnReports.Text = "  📑    Reports";

            btnSystemLog.EmojiIcon = "🖥️";
            btnSystemLog.FullText = "  🖥️    System Log";
            btnSystemLog.Text = "  🖥️    System Log";

            btnSettings.EmojiIcon = "⚙️";
            btnSettings.FullText = "  ⚙️    Settings";
            btnSettings.Text = "  ⚙️    Settings";

            btnUserManagement.EmojiIcon = "👥";
            btnUserManagement.FullText = "  👥    User Management";
            btnUserManagement.Text = "  👥    User Management";

            // Set Dashboard as initial active menu
            btnDashboard.IsActive = true;
        }

        // IDE1006 FIX: Huruf awal kapital (PascalCase)
        private UserControl? GetOrInitPage(string buttonName)
        {
            return buttonName switch
            {
                "btnDashboard" => pageDashboard ??= new Dashboard(),
                "btnTestExecution" => pageTestExecution ??= new TestExecution(),
                "btnManualTesting" => pageManualTesting ??= new ManualTestingPage(),
                "btnHarnessDatabase" => pageHarnessDb ??= new HarnessDatabase(),
                "btnTestHistory" => pageTestHistory ??= new TestHistory(),
                "btnReports" => pageReports ??= new Reports(),
                "btnSystemLog" => pageSystemLog ??= new SystemLog(),
                "btnSettings" => pageSettings ??= new Settings(),
                "btnUserManagement" => pageUserManagement ??= new UserManagement(_userService),
                _ => null
            };
        }

        private void BtnMenu_Click(object sender, EventArgs e)
        {
            if (sender is not AssemblyLineTester.Views.Controls.MenuButton clickedButton) return;

            UserControl? targetPage = GetOrInitPage(clickedButton.Name);
            SetActiveMenu(clickedButton, targetPage);
        }

        /// <summary>
        /// Programmatically navigates to Settings -> Users and Roles permissions tab.
        /// </summary>
        public void OpenRolePermissionsSettings()
        {
            if (btnSettings != null)
            {
                UserControl? targetPage = GetOrInitPage("btnSettings");
                SetActiveMenu(btnSettings, targetPage);
                if (pageSettings != null)
                {
                    pageSettings.OpenUsersAndRolesTab();
                }
            }
        }

        private void SetActiveMenu(AssemblyLineTester.Views.Controls.MenuButton activeBtn, UserControl? activePage)
        {
            if (pnlUserDropdown != null && pnlUserDropdown.Visible)
            {
                pnlUserDropdown.Visible = false;
                lblArrow.Text = "⌄";
            }

            AssemblyLineTester.Views.Controls.MenuButton[] allMenus = [btnDashboard, btnTestExecution, btnManualTesting, btnHarnessDatabase, btnTestHistory, btnReports, btnSystemLog, btnSettings, btnUserManagement];

            foreach (var btn in allMenus)
            {
                btn.IsActive = (btn == activeBtn);
            }

            if (pnlContent == null) return;
            SendMessage(pnlContent.Handle, 0x000B, 0, 0); // WM_SETREDRAW false
            pnlContent.SuspendLayout();
            try
            {
                if (activePage != null)
                {
                    if (!pnlContent.Controls.Contains(activePage))
                    {
                        activePage.Dock = DockStyle.Fill;
                        pnlContent.Controls.Add(activePage);
                    }
                    foreach (Control c in pnlContent.Controls)
                    {
                        if (c != activePage && c.Visible) c.Visible = false;
                    }
                    if (!activePage.Visible) activePage.Visible = true;
                    activePage.BringToFront();
                }
                else
                {
                    pnlContent.Controls.Clear();
                    Label lblPlaceholder = new Label
                    {
                        Text = $"{activeBtn.FullText.Trim()} (Under Construction)",
                        Font = AssemblyLineTester.Helpers.UIStyleHelper.Font16Bold,
                        ForeColor = Color.FromArgb(100, 116, 139),
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        BackColor = Color.FromArgb(244, 245, 247)
                    };
                    pnlContent.Controls.Add(lblPlaceholder);
                }
            }
            finally
            {
                pnlContent.ResumeLayout(true);
                SendMessage(pnlContent.Handle, 0x000B, 1, 0); // WM_SETREDRAW true
                pnlContent.Invalidate(true);
            }
        }

        private AssemblyLineTester.Views.Controls.MenuButton[] SidebarMenuButtons =>
            new AssemblyLineTester.Views.Controls.MenuButton[] { btnDashboard, btnTestExecution, btnManualTesting, btnHarnessDatabase, btnTestHistory, btnReports, btnSystemLog, btnSettings, btnUserManagement };

        /// Repositions visible sidebar buttons back-to-back, skipping hidden (no-permission) ones so no gap remains.
        private void RepositionSidebarButtons()
        {
            var menuButtons = SidebarMenuButtons;
            if (isSidebarExpanded)
            {
                int currentY = _expandedMenuStartY;
                foreach (var btn in menuButtons)
                {
                    if (!btn.Visible) continue;
                    btn.Location = new Point(_expandedMenuX, currentY);
                    btn.Width = _expandedMenuWidth;
                    currentY += btn.Height + _expandedMenuGap;
                }
            }
            else
            {
                int currentY = _expandedMenuStartY + (int)(_expandedToggleSize.Height * (20f / 88f));
                foreach (var btn in menuButtons)
                {
                    if (!btn.Visible) continue;
                    btn.Location = new Point(0, currentY);
                    btn.Width = collapsedWidth;
                    currentY += btn.Height + (int)(_expandedMenuGap * (12f / 16f));
                }
            }
        }

        private void BtnToggleSidebar_Click(object sender, EventArgs e)
        {
            var menuButtons = SidebarMenuButtons;

            SendMessage(this.Handle, 0x000B, 0, 0); // WM_SETREDRAW false
            this.SuspendLayout();
            splitContainer1.SuspendLayout();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            try
            {
                if (isSidebarExpanded)
                {
                    splitContainer1.SplitterDistance = collapsedWidth;
                    isSidebarExpanded = false;

                    pnlLogoHeader.IsMinimized = true;
                    pnlLogoHeader.Size = new Size(collapsedWidth, (int)(_expandedLogoSize.Height * (85f / 130f)));
                    
                    Size newLblMenuSize = new Size((int)(_expandedLblMenuSize.Width * (65f / 200f)), (int)(_expandedLblMenuSize.Height * (65f / 90f)));
                    lblMenu.Size = newLblMenuSize;
                    lblMenu.Location = new Point((collapsedWidth - newLblMenuSize.Width) / 2, (int)(_expandedLblMenuLoc.Y * (10f / 20f)));
                    
                    btnToggleSidebar.Location = new Point(0, _expandedToggleSize.Height);
                    btnToggleSidebar.Size = new Size(collapsedWidth, (int)(_expandedToggleSize.Height * (66f / 88f)));

                    int currentY = _expandedMenuStartY + (int)(_expandedToggleSize.Height * (20f / 88f));
                    foreach (var btn in menuButtons)
                    {
                        btn.SetMinimized(true);
                        if (!btn.Visible) continue;
                        btn.Location = new Point(0, currentY);
                        btn.Width = collapsedWidth;
                        currentY += btn.Height + (int)(_expandedMenuGap * (12f / 16f));
                    }
                }
                else
                {
                    splitContainer1.SplitterDistance = expandedWidth;
                    isSidebarExpanded = true;

                    pnlLogoHeader.IsMinimized = false;
                    pnlLogoHeader.Size = _expandedLogoSize;
                    lblMenu.Size = _expandedLblMenuSize;
                    lblMenu.Location = _expandedLblMenuLoc;
                    btnToggleSidebar.Location = _expandedToggleLoc;
                    btnToggleSidebar.Size = _expandedToggleSize;

                    int currentY = _expandedMenuStartY;
                    foreach (var btn in menuButtons)
                    {
                        btn.SetMinimized(false);
                        if (!btn.Visible) continue;
                        btn.Location = new Point(_expandedMenuX, currentY);
                        btn.Width = _expandedMenuWidth;
                        currentY += btn.Height + _expandedMenuGap;
                    }
                }
            }
            finally
            {
                splitContainer1.Panel2.ResumeLayout(true);
                splitContainer1.Panel1.ResumeLayout(true);
                splitContainer1.ResumeLayout(true);
                this.ResumeLayout(true);
                SendMessage(this.Handle, 0x000B, 1, 0); 
                this.Refresh();
            }
        }

        private void CloseDropdownIfOpen(object? sender, EventArgs e)
        {
            if (pnlUserDropdown != null && pnlUserDropdown.Visible)
            {
                pnlUserDropdown.Visible = false;
                lblArrow.Text = "⌄";
            }
        }

        /// Full height of the dropdown once open. The open animation grows towards this.
        private int _dropdownHeight;
        private readonly System.Windows.Forms.Timer _dropdownTimer = new() { Interval = 15 };

        private void ToggleUserDropdown(object? sender, EventArgs e)
        {
            if (pnlUserDropdown == null) return;

            if (pnlUserDropdown.Visible)
            {
                CloseUserDropdown();
                return;
            }

            // Align the dropdown under the right side of pnlUser, near the arrow.
            Point screenPos = pnlUser.PointToScreen(new Point(pnlUser.Width - pnlUserDropdown.Width - 15, pnlUser.Height));
            pnlUserDropdown.Location = this.PointToClient(screenPos);

            // Grows to full height over roughly six frames. Short enough to feel instant, long
            // enough that the menu reads as coming from the button rather than appearing on top.
            pnlUserDropdown.Height = 1;
            pnlUserDropdown.Visible = true;
            pnlUserDropdown.BringToFront();
            lblArrow.Text = "⌃";

            _dropdownTimer.Stop();
            _dropdownTimer.Tick -= GrowDropdown;
            _dropdownTimer.Tick += GrowDropdown;
            _dropdownTimer.Start();
        }

        private void GrowDropdown(object? sender, EventArgs e)
        {
            int step = Math.Max(6, _dropdownHeight / 5);
            int next = pnlUserDropdown.Height + step;

            if (next >= _dropdownHeight)
            {
                pnlUserDropdown.Height = _dropdownHeight;
                _dropdownTimer.Stop();
                return;
            }

            pnlUserDropdown.Height = next;
        }

        private void CloseUserDropdown()
        {
            _dropdownTimer.Stop();

            // Restored immediately rather than animated shut: the panel is about to be hidden, and
            // a closing animation only delays whatever the user clicked.
            pnlUserDropdown.Visible = false;
            pnlUserDropdown.Height = _dropdownHeight;
            lblArrow.Text = "⌄";
        }

        private void PnlUserDropdown_Paint(object? sender, PaintEventArgs e)
        {
            using var pen = new Pen(Color.FromArgb(203, 213, 225), 1);
            e.Graphics.DrawRectangle(pen, 0, 0, pnlUserDropdown.Width - 1, pnlUserDropdown.Height - 1);
        }

        private void BtnLogOut_MouseEnter(object? sender, EventArgs e)
        {
            btnLogOut.BackColor = Color.FromArgb(254, 242, 242);
        }

        private void BtnLogOut_MouseLeave(object? sender, EventArgs e)
        {
            btnLogOut.BackColor = Color.Transparent;
        }

        private Button btnChangePassword = null!;

        /// <summary>
        /// Rebuilds the user dropdown as a proper menu: Change Password above Log Out.
        /// </summary>
        /// <remarks>
        /// The panel is sized from the measured text rather than the designer's fixed 220px, which
        /// clipped "Change Password" outright, and every measurement goes through the DPI scale so
        /// it does not clip again on a machine with different display scaling.
        /// </remarks>
        private void SetupUserDropdown()
        {
            float scale = AssemblyLineTester.Helpers.UIStyleHelper.GetHostScaleFactor(this);
            int S(int v) => Math.Max(1, (int)Math.Round(v * scale));

            Font itemFont = AssemblyLineTester.Helpers.UIStyleHelper.ScaleFont(
                AssemblyLineTester.Helpers.UIStyleHelper.Font10Bold, scale);

            btnChangePassword = new Button
            {
                Text = "🔑    Change Password",
                ForeColor = Color.FromArgb(51, 65, 85),
                Tag = "menu"
            };

            btnLogOut.Text = "⏻    Log Out";
            btnLogOut.Font = itemFont;

            // Widest label decides the panel, so no item can ever be cut off.
            int widest = 0;
            foreach (Button item in new[] { btnChangePassword, btnLogOut })
            {
                widest = Math.Max(widest, TextRenderer.MeasureText(item.Text, itemFont).Width);
            }

            int padding = S(10);
            int rowHeight = S(46);
            int itemWidth = widest + S(52);
            int panelWidth = itemWidth + (padding * 2);

            foreach (Button item in new[] { btnChangePassword, btnLogOut })
            {
                item.Font = itemFont;
                item.BackColor = Color.Transparent;
                item.Cursor = Cursors.Hand;
                item.FlatStyle = FlatStyle.Flat;
                item.FlatAppearance.BorderSize = 0;
                item.TextAlign = ContentAlignment.MiddleLeft;
                item.Padding = new Padding(S(14), 0, 0, 0);
                item.UseVisualStyleBackColor = false;
                item.Size = new Size(itemWidth, rowHeight);
            }

            btnChangePassword.Location = new Point(padding, padding);
            btnLogOut.Location = new Point(padding, padding + rowHeight + S(9));

            HoverFill(btnChangePassword, Color.FromArgb(241, 245, 249));
            HoverFill(btnLogOut, Color.FromArgb(254, 226, 226));

            btnChangePassword.Click += (s, e) =>
            {
                CloseUserDropdown();
                PromptPasswordChange(mandatory: false);
            };

            pnlUserDropdown.Controls.Add(btnChangePassword);
            _dropdownHeight = (padding * 2) + (rowHeight * 2) + S(9);
            pnlUserDropdown.Size = new Size(panelWidth, _dropdownHeight);

            // A hairline separator reads as one menu with two groups, rather than two loose buttons.
            pnlUserDropdown.Paint += (s, e) =>
            {
                int y = btnLogOut.Top - S(5);
                using var pen = new Pen(Color.FromArgb(226, 232, 240), 1);
                e.Graphics.DrawLine(pen, padding + S(8), y, panelWidth - padding - S(8), y);
            };

            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(
                pnlUserDropdown, 10, Color.FromArgb(203, 213, 225), 1.2f);
        }

        private static void HoverFill(Button button, Color hover)
        {
            button.MouseEnter += (s, e) => button.BackColor = hover;
            button.MouseLeave += (s, e) => button.BackColor = Color.Transparent;
        }

        /// <summary>
        /// Opens the change-password dialog, optionally as a step the user cannot skip.
        /// </summary>
        /// <remarks>
        /// The mandatory path loops: the prompt reappears until the password is actually changed.
        /// Anything less and closing the dialog would leave the shipped password in place while the
        /// user is already inside the application.
        /// </remarks>
        private void PromptPasswordChange(bool mandatory)
        {
            float scale = AssemblyLineTester.Helpers.UIStyleHelper.GetHostScaleFactor(this);

            while (true)
            {
                if (mandatory)
                {
                    using var prompt = new AssemblyLineTester.Views.Modals.SecureAccountPromptForm(scale);
                    prompt.ShowDialog(this);
                }

                using var dialog = new AssemblyLineTester.Views.Modals.ChangePasswordModalForm(_userService, mandatory);
                DialogResult result = dialog.ShowDialog(this);

                if (!mandatory || result == DialogResult.OK) return;
            }
        }

        private void BtnLogOut_Click(object? sender, EventArgs e)
        {
            if (pnlUserDropdown != null) pnlUserDropdown.Visible = false;
            lblArrow.Text = "⌄";
            if (MessageBox.Show("Are you sure you want to log out of Assembly Line Tester?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Recorded before the session is torn down, while the username is still known.
                string? username = SessionManager.CurrentUser?.Username;
                new SystemLogService().Log(
                    Models.SystemLog.LogSeverity.Info, "Authentication", "Logout",
                    $"User '{username ?? "unknown"}' signed out.", username);

                IsLoggingOut = true;
                this.Close();
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            if (!IsLoggingOut) Application.Exit();
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        private void Header_MouseDown(object? sender, MouseEventArgs e)
        {
            CloseDropdownIfOpen(sender, e);
            if (e.Button == MouseButtons.Left)
            {
                if (e.Clicks == 2)
                {
                    ToggleMaximize();
                    return;
                }
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void ToggleMaximize()
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
            UpdateMaximizeButtonText();
        }

        private void MainForm_Resize(object? sender, EventArgs e)
        {
            UpdateMaximizeButtonText();
        }

        private void UpdateMaximizeButtonText()
        {
            if (btnMaximize != null)
            {
                btnMaximize.Text = (this.WindowState == FormWindowState.Maximized) ? "❐" : "□";
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x0084;
            const int resizeAreaSize = 10;

            if (m.Msg == WM_NCHITTEST && this.WindowState == FormWindowState.Normal)
            {
                base.WndProc(ref m);
                if ((int)m.Result == 1) // HTCLIENT
                {
                    Point screenPoint = new Point(m.LParam.ToInt32());
                    Point clientPoint = this.PointToClient(screenPoint);

                    bool left = clientPoint.X <= resizeAreaSize;
                    bool right = clientPoint.X >= this.ClientSize.Width - resizeAreaSize;
                    bool top = clientPoint.Y <= resizeAreaSize;
                    bool bottom = clientPoint.Y >= this.ClientSize.Height - resizeAreaSize;

                    if (top && left) m.Result = (IntPtr)13; // HTTOPLEFT
                    else if (top && right) m.Result = (IntPtr)14; // HTTOPRIGHT
                    else if (bottom && left) m.Result = (IntPtr)16; // HTBOTTOMLEFT
                    else if (bottom && right) m.Result = (IntPtr)17; // HTBOTTOMRIGHT
                    else if (left) m.Result = (IntPtr)10; // HTLEFT
                    else if (right) m.Result = (IntPtr)11; // HTRIGHT
                    else if (top) m.Result = (IntPtr)12; // HTTOP
                    else if (bottom) m.Result = (IntPtr)15; // HTBOTTOM
                }
                return;
            }

            base.WndProc(ref m);
        }
    }
}
