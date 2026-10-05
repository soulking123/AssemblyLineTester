using AssemblyLineTester.Views.Pages;
using AssemblyLineTester.Services;
using AssemblyLineTester.Helpers;

namespace AssemblyLineTester.Views
{
    // Shell window: sidebar navigation plus whichever page is loaded into pnlContent.
    public partial class MainForm : Form
    {
        private readonly UserService _userService = new();
        private bool isSidebarExpanded = true;
        private readonly int expandedWidth = 380;
        private readonly int collapsedWidth = 90;
        private readonly Size _expandedLogoSize;
        private readonly Point _expandedToggleLoc;
        private readonly Size _expandedToggleSize;
        private readonly Size _expandedLblMenuSize;
        private readonly Point _expandedLblMenuLoc;
        private readonly int _expandedMenuStartY;
        private readonly int _expandedMenuX;
        private readonly int _expandedMenuWidth;
        private readonly int _expandedMenuGap;

        public bool IsLoggingOut { get; private set; } = false;

        private TestHistory? pageTestHistory;
        private SystemLog? pageSystemLog;
        private ProductDatabase? pageProductDb;
        private Dashboard? pageDashboard;
        private TestExecution? pageTestExecution;
        private TestParametersPage? pageTestParameters;
        private Label? _lblPlaceholder;
        private Reports? pageReports;
        private Settings? pageSettings;
        private UserManagement? pageUserManagement;
        private System.Threading.Tasks.Task? _settingsPreloadTask;

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

            // After the window is on screen, so it reads as setup rather than a failed login.
            if (SessionManager.CurrentUser?.MustChangePassword == true)
            {
                PromptPasswordChange(mandatory: true);
            }

            StartMaintenanceWatch();

            // Start preloading the Settings page in the background
            _settingsPreloadTask = System.Threading.Tasks.Task.Run(async () =>
            {
                var data = new AssemblyLineTester.Models.SettingsPreloadData();
                var task1 = System.Threading.Tasks.Task.Run(() => data.TestTypes = AssemblyLineTester.Services.TestSettingService.GetTestTypes());
                var task2 = System.Threading.Tasks.Task.Run(() => data.TestTypeChecks = AssemblyLineTester.Services.TestSettingService.GetTestTypeChecks());
                var task3 = System.Threading.Tasks.Task.Run(() => data.LearnConnections = AssemblyLineTester.Services.TestSettingService.GetLearnConnections());
                var task4 = System.Threading.Tasks.Task.Run(() => data.TestResultChecks = AssemblyLineTester.Services.TestSettingService.GetTestResultChecks());
                await System.Threading.Tasks.Task.WhenAll(task1, task2, task3, task4);

                var tcs = new System.Threading.Tasks.TaskCompletionSource<bool>();
                this.BeginInvoke(new Action(() =>
                {
                    try
                    {
                        if (pageSettings == null)
                        {
                            pageSettings = new Settings(data);
                        }
                        tcs.SetResult(true);
                    }
                    catch (Exception ex)
                    {
                        tcs.SetException(ex);
                    }
                }));
                await tcs.Task;
            });
        }

        private System.Windows.Forms.Timer? _maintenanceWatch;
        private bool _maintenancePromptOpen;

        // Checked on a timer as well as at sign-in, because the allowance can run out mid-shift.
        private void StartMaintenanceWatch()
        {
            PromptMaintenanceIfDue();

            _maintenanceWatch = new System.Windows.Forms.Timer { Interval = 30000 };
            _maintenanceWatch.Tick += (s, e) => PromptMaintenanceIfDue();
            _maintenanceWatch.Start();
        }

        /// <summary>
        /// Holds the application on a modal prompt for as long as maintenance is overdue.
        /// </summary>
        /// <remarks>
        /// Whoever may reset gets a button that lands on the Settings tab holding the countdown;
        /// everyone else can only sign out, so an overdue rig cannot quietly keep testing.
        /// </remarks>
        private void PromptMaintenanceIfDue()
        {
            // A queued tick can still arrive after the window has gone, and ShowDialog on a disposed
            // form throws rather than doing nothing.
            if (IsDisposed || Disposing || !IsHandleCreated) return;

            if (_maintenancePromptOpen || !MaintenanceService.IsDue) return;

            // Settings is the one place the countdown can be cleared, so the prompt stays out of the
            // way while the operator is standing on it. Leaving that page brings the block back.
            bool mayReset = SessionManager.HasPermission("Action.Maintenance.Reset");
            if (mayReset && btnSettings.IsActive) return;

            _maintenancePromptOpen = true;
            try
            {
                float scale = UIStyleHelper.GetHostScaleFactor(this);

                using var prompt = new AssemblyLineTester.Views.Modals.MaintenanceDuePromptForm(scale);
                DialogResult result = prompt.ShowDialog(this);

                if (result == DialogResult.Abort)
                {
                    BtnLogOut_Click(this, EventArgs.Empty);
                    return;
                }

                if (mayReset && result == DialogResult.OK) OpenMaintenanceSettings();
            }
            finally
            {
                _maintenancePromptOpen = false;
            }
        }

        // Lands on the Settings page, where the maintenance card lives.
        private void OpenMaintenanceSettings()
        {
            if (btnSettings == null) return;

            UserControl? targetPage = GetOrInitPage("btnSettings");
            SetActiveMenu(btnSettings, targetPage);
            pageSettings?.OpenGeneralTab();
        }

        public MainForm()
        {
            InitializeComponent();
            EnableDoubleBuffering();

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
            this.MaximizedBounds = Rectangle.Empty;
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

            ApplyRolePermissions();

            ApplySidebarStyles();
            InitializeUserInfo();

            this.Load += (s, e) => RepositionSidebarButtons();

            pnlTopHeader.Click += CloseDropdownIfOpen;
            lblHeaderTitle.Click += CloseDropdownIfOpen;
            pnlRight.Click += CloseDropdownIfOpen;
            pnlDateTime.Click += CloseDropdownIfOpen;
            lblDateText.Click += CloseDropdownIfOpen;
            lblTimeText.Click += CloseDropdownIfOpen;

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

            // Designer fonts skip the AutoScale pass, so they are scaled explicitly.
            float scaleY = this.CurrentAutoScaleDimensions.Height / 32f;
            btnMaximize.Font = new Font(btnMaximize.Font.FontFamily, btnMaximize.Font.Size * scaleY, btnMaximize.Font.Style);
            btnMinimize.Font = new Font(btnMinimize.Font.FontFamily, btnMinimize.Font.Size * scaleY, btnMinimize.Font.Style);
            btnClose.Font = new Font(btnClose.Font.FontFamily, btnClose.Font.Size * scaleY, btnClose.Font.Style);

            this.Resize += MainForm_Resize;

            SetActiveMenu(btnDashboard, GetOrInitPage("btnDashboard"));
        }

        // Shows only the sidebar buttons the active role is allowed to reach.
        public void ApplyRolePermissions()
        {
            btnDashboard.Visible = SessionManager.HasPermission("Page.Dashboard");
            btnTestExecution.Visible = SessionManager.HasPermission("Page.TestExecution");
            btnManualTesting.Visible = SessionManager.HasPermission("Page.TestExecution");
            btnProductDatabase.Visible = SessionManager.HasPermission("Page.HarnessDatabase");
            btnTestHistory.Visible = SessionManager.HasPermission("Page.TestHistory");
            btnReports.Visible = SessionManager.HasPermission("Page.Reports");
            btnSystemLog.Visible = SessionManager.HasPermission("Page.SystemLog");
            btnSettings.Visible = SessionManager.HasPermission("Page.Settings");
            btnUserManagement.Visible = SessionManager.HasPermission("Page.UserManagement");

            LeavePageIfNoLongerAllowed();
            RepositionSidebarButtons();
        }

        // Hiding a sidebar button leaves its page on screen and still usable, so the view is moved
        // to the first page the role can still reach.
        // ponytail: a role left with no page at all keeps the one it is on; fix that at sign-in,
        // where an account that can open nothing should not get a session in the first place.
        private void LeavePageIfNoLongerAllowed()
        {
            var active = SidebarMenuButtons.FirstOrDefault(b => b.IsActive);
            if (active is null || active.Visible) return;

            var fallback = SidebarMenuButtons.FirstOrDefault(b => b.Visible);
            if (fallback is null) return;

            SetActiveMenu(fallback, GetOrInitPage(fallback.Name));
        }

        // Pushes saved Default Test Settings into the pages that were built from them. Both are
        // cached for the life of the session, so without this the ticks only take effect on restart.
        public void ApplyTestSettings()
        {
            pageTestExecution?.ReloadResultColumns();
            pageTestParameters?.LoadTestTypeColumnItems();
        }

        private void EnableDoubleBuffering()
        {
            this.DoubleBuffered = true;
            UIStyleHelper.EnableDoubleBuffering(this);
        }

        private void InitializeUserInfo()
        {
            // Neutral fallbacks: a failed session must not caption itself with a real name or role.
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

        private AssemblyLineTester.Views.Controls.CustomToolTipForm? _customToolTipForm;
        private System.Collections.Generic.Dictionary<Button, string> _tooltipTexts = new();

        private void ApplySidebarStyles()
        {
            AssemblyLineTester.Views.Controls.MenuButton[] menuButtons = [btnDashboard, btnTestExecution, btnManualTesting, btnProductDatabase, btnTestHistory, btnReports, btnSystemLog, btnSettings, btnUserManagement];
            Font menuFont = new("Segoe UI", 11F, FontStyle.Regular);

            _customToolTipForm ??= new AssemblyLineTester.Views.Controls.CustomToolTipForm();

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

                btn.MouseEnter -= SidebarBtn_MouseEnter;
                btn.MouseEnter += SidebarBtn_MouseEnter;
                btn.MouseLeave -= SidebarBtn_MouseLeave;
                btn.MouseLeave += SidebarBtn_MouseLeave;
            }

            btnDashboard.EmojiIcon = "📊";
            btnDashboard.FullText = "  📊    Dashboard";
            btnDashboard.Text = "  📊    Dashboard";
            _tooltipTexts[btnDashboard] = "Dashboard";

            btnTestExecution.EmojiIcon = "⚡";
            btnTestExecution.FullText = "  ⚡    Test Execution";
            btnTestExecution.Text = "  ⚡    Test Execution";
            _tooltipTexts[btnTestExecution] = "Test Execution";

            btnManualTesting.EmojiIcon = "🕹️";
            btnManualTesting.FullText = "  🕹️    Test Parameters";
            btnManualTesting.Text = "  🕹️    Test Parameters";
            _tooltipTexts[btnManualTesting] = "Test Parameters";

            btnProductDatabase.EmojiIcon = "📦";
            btnProductDatabase.FullText = "  📦    Product Database";
            btnProductDatabase.Text = "  📦    Product Database";
            _tooltipTexts[btnProductDatabase] = "Product Database";

            btnTestHistory.EmojiIcon = "📜";
            btnTestHistory.FullText = "  📜    Test History";
            btnTestHistory.Text = "  📜    Test History";
            _tooltipTexts[btnTestHistory] = "Test History";

            btnReports.EmojiIcon = "📑";
            btnReports.FullText = "  📑    Reports";
            btnReports.Text = "  📑    Reports";
            _tooltipTexts[btnReports] = "Reports";

            btnSystemLog.EmojiIcon = "🖥️";
            btnSystemLog.FullText = "  🖥️    System Log";
            btnSystemLog.Text = "  🖥️    System Log";
            _tooltipTexts[btnSystemLog] = "System Log";

            btnSettings.EmojiIcon = "⚙️";
            btnSettings.FullText = "  ⚙️    Settings";
            btnSettings.Text = "  ⚙️    Settings";
            _tooltipTexts[btnSettings] = "Settings";

            btnUserManagement.EmojiIcon = "👥";
            btnUserManagement.FullText = "  👥    User Management";
            btnUserManagement.Text = "  👥    User Management";
            _tooltipTexts[btnUserManagement] = "User Management";

            btnDashboard.IsActive = true;
        }

        private void SidebarBtn_MouseEnter(object? sender, EventArgs e)
        {
            if (sender is Button btn && !isSidebarExpanded)
            {
                if (_tooltipTexts.TryGetValue(btn, out string text))
                {
                    Point screenLoc = btn.PointToScreen(new Point(btn.Width, 0));
                    _customToolTipForm?.ShowToolTip(text, screenLoc, btn.Height);
                }
            }
        }

        private void SidebarBtn_MouseLeave(object? sender, EventArgs e)
        {
            _customToolTipForm?.HideToolTip();
        }

        private UserControl? GetOrInitPage(string buttonName)
        {
            return buttonName switch
            {
                "btnDashboard" => pageDashboard ??= new Dashboard(),
                "btnTestExecution" => pageTestExecution ??= new TestExecution(),
                "btnManualTesting" => pageTestParameters ??= new TestParametersPage(),
                "btnProductDatabase" => pageProductDb ??= new ProductDatabase(),
                "btnTestHistory" => pageTestHistory ??= new TestHistory(),
                "btnReports" => pageReports ??= new Reports(),
                "btnSystemLog" => pageSystemLog ??= new SystemLog(),
                "btnSettings" => pageSettings ??= new Settings(),
                "btnUserManagement" => pageUserManagement ??= new UserManagement(_userService),
                _ => null
            };
        }

        // ponytail: timing left in on purpose while page-switch lag is being chased. Remove the
        // three Stopwatch lines once the numbers stop being interesting.
        private async void BtnMenu_Click(object sender, EventArgs e)
        {
            if (sender is not AssemblyLineTester.Views.Controls.MenuButton clickedButton) return;

            bool firstVisit = GetCachedPage(clickedButton.Name) is null;

            var built = System.Diagnostics.Stopwatch.StartNew();
            UserControl? targetPage = GetOrInitPage(clickedButton.Name);
            built.Stop();

            var shown = System.Diagnostics.Stopwatch.StartNew();
            if (clickedButton.Name == "btnSettings" && pageSettings == null && _settingsPreloadTask != null)
            {
                try { await _settingsPreloadTask; } catch { }
            }
            SetActiveMenu(clickedButton, targetPage);
            shown.Stop();

            FileLogService.Info("Navigation",
                $"{clickedButton.Name,-22} construct {built.ElapsedMilliseconds,5} ms   show {shown.ElapsedMilliseconds,5} ms" +
                (firstVisit ? "   (first visit)" : ""));

            // Checked here as well as on the timer, or leaving Settings buys half a minute of
            // unblocked testing on a rig that is overdue.
            PromptMaintenanceIfDue();
        }

        // Whether a page has already been built, without building it as a side effect.
        private UserControl? GetCachedPage(string buttonName) => buttonName switch
        {
            "btnDashboard" => pageDashboard,
            "btnTestExecution" => pageTestExecution,
            "btnManualTesting" => pageTestParameters,
            "btnProductDatabase" => pageProductDb,
            "btnTestHistory" => pageTestHistory,
            "btnReports" => pageReports,
            "btnSystemLog" => pageSystemLog,
            "btnSettings" => pageSettings,
            "btnUserManagement" => pageUserManagement,
            _ => null
        };

        // Jumps to Settings, Users and Roles tab.
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

            AssemblyLineTester.Views.Controls.MenuButton[] allMenus = [btnDashboard, btnTestExecution, btnManualTesting, btnProductDatabase, btnTestHistory, btnReports, btnSystemLog, btnSettings, btnUserManagement];

            foreach (var btn in allMenus)
            {
                btn.IsActive = (btn == activeBtn);
            }

            if (pnlContent == null) return;
            AssemblyLineTester.Helpers.NativeMethods.SetRedraw(pnlContent.Handle, false);
            pnlContent.SuspendLayout();
            try
            {
                if (activePage != null)
                {
                    if (!pnlContent.Controls.Contains(activePage))
                    {
                        activePage.Dock = DockStyle.Fill;

                        // Pages are built on first navigation, long after the constructor ran its
                        // own pass, so this is the only moment their controls can be buffered.
                        UIStyleHelper.EnableDoubleBuffering(activePage);

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
                    // Pages are cached and reused, so they are hidden here rather than cleared.
                    // Disposing them would leave GetOrInitPage handing back a dead control.
                    _lblPlaceholder ??= new Label
                    {
                        Font = AssemblyLineTester.Helpers.UIStyleHelper.Font16Bold,
                        ForeColor = Color.FromArgb(100, 116, 139),
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        BackColor = Color.FromArgb(244, 245, 247)
                    };
                    _lblPlaceholder.Text = $"{activeBtn.FullText.Trim()} (Under Construction)";

                    if (!pnlContent.Controls.Contains(_lblPlaceholder))
                    {
                        pnlContent.Controls.Add(_lblPlaceholder);
                    }

                    foreach (Control c in pnlContent.Controls)
                    {
                        if (c != _lblPlaceholder && c.Visible) c.Visible = false;
                    }

                    _lblPlaceholder.Visible = true;
                    _lblPlaceholder.BringToFront();
                }
            }
            finally
            {
                pnlContent.ResumeLayout(true);
                NativeMethods.SetRedraw(pnlContent.Handle, true);
                pnlContent.Invalidate(true);
            }
        }

        private AssemblyLineTester.Views.Controls.MenuButton[] SidebarMenuButtons =>
            new AssemblyLineTester.Views.Controls.MenuButton[] { btnDashboard, btnTestExecution, btnManualTesting, btnProductDatabase, btnTestHistory, btnReports, btnSystemLog, btnSettings, btnUserManagement };

        // Closes the gap left by sidebar buttons the role cannot see.
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

            AssemblyLineTester.Helpers.NativeMethods.SetRedraw(splitContainer1.Handle, false); // WM_SETREDRAW false
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
                
                if (isSidebarExpanded)
                {
                    _customToolTipForm?.HideToolTip();
                }
            }
            finally
            {
                splitContainer1.Panel2.ResumeLayout(true);
                splitContainer1.Panel1.ResumeLayout(true);
                splitContainer1.ResumeLayout(true);
                this.ResumeLayout(true);
                AssemblyLineTester.Helpers.NativeMethods.SetRedraw(splitContainer1.Handle, true); // WM_SETREDRAW true
                
                // Remove focus from the toggle button so it doesn't stay highlighted
                lblMenu.Focus();
                splitContainer1.Refresh();
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

        // Full height of the dropdown once open; the animation grows towards this.
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

            Point screenPos = pnlUser.PointToScreen(new Point(pnlUser.Width - pnlUserDropdown.Width - 15, pnlUser.Height));
            pnlUserDropdown.Location = this.PointToClient(screenPos);

            // Roughly six frames, so the menu reads as coming from the button.
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

            // Restored, not animated shut: a closing animation only delays the click.
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

        // Sized from measured text through the DPI scale; the designer's fixed 220px clipped.
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

        // The mandatory path loops until the password actually changes; there is no dismiss.
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
            // A WinForms timer outlives the form that made it, so an unstopped one keeps firing
            // against a disposed window every 30 seconds until the process ends.
            if (_maintenanceWatch != null)
            {
                _maintenanceWatch.Stop();
                _maintenanceWatch.Dispose();
                _maintenanceWatch = null;
            }

            base.OnFormClosed(e);
            if (!IsLoggingOut)
            {
                InstrumentService.Shutdown();
                Application.Exit();
            }
        }


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
                NativeMethods.DragWindow(this.Handle);
            }
        }

        private void ToggleMaximize()
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                // Currently covering taskbar -> switch to showing taskbar
                this.WindowState = FormWindowState.Normal;
                this.Bounds = Screen.FromHandle(this.Handle).WorkingArea;
            }
            else
            {
                // Currently showing taskbar -> switch to covering taskbar
                this.MaximizedBounds = Rectangle.Empty;
                this.WindowState = FormWindowState.Maximized;
                
                // Force the window to render above the taskbar
                this.TopMost = true;
                this.TopMost = false;
                this.BringToFront();
            }
            UpdateMaximizeButtonText();
        }

        private FormWindowState _lastWindowState = FormWindowState.Normal;

        private void MainForm_Resize(object? sender, EventArgs e)
        {
            UpdateMaximizeButtonText();

            // Forces one clean repaint on restore instead of a staged one.
            if (_lastWindowState == FormWindowState.Minimized && WindowState != FormWindowState.Minimized)
            {
                NativeMethods.SetRedraw(this.Handle, false);
                NativeMethods.SetRedraw(this.Handle, true);
                this.Refresh();
            }

            _lastWindowState = WindowState;
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
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x0084;
            const int WM_SIZE = 0x0005;
            const int SIZE_RESTORED = 0;
            const int SIZE_MAXIMIZED = 2;
            const int resizeAreaSize = 10;

            // Fix visual glitching when restoring from minimized (alt+tab or taskbar click).
            // WS_EX_COMPOSITED's off-screen buffer goes stale while the window is minimized;
            // the DWM does not recompose it on restore for borderless forms, so we force a
            // full invalidation of every control in the tree.
            if (m.Msg == WM_SIZE)
            {
                int sizeType = m.WParam.ToInt32();
                if (sizeType == SIZE_RESTORED || sizeType == SIZE_MAXIMIZED)
                {
                    base.WndProc(ref m);
                    this.Invalidate(true);
                    this.Update();
                    return;
                }
            }

            if (m.Msg == WM_NCHITTEST && this.WindowState == FormWindowState.Normal)
            {
                base.WndProc(ref m);
                if ((int)m.Result == 1)
                {
                    Point screenPoint = new Point(m.LParam.ToInt32());
                    Point clientPoint = this.PointToClient(screenPoint);

                    bool left = clientPoint.X <= resizeAreaSize;
                    bool right = clientPoint.X >= this.ClientSize.Width - resizeAreaSize;
                    bool top = clientPoint.Y <= resizeAreaSize;
                    bool bottom = clientPoint.Y >= this.ClientSize.Height - resizeAreaSize;

                    if (top && left) m.Result = (IntPtr)13;
                    else if (top && right) m.Result = (IntPtr)14;
                    else if (bottom && left) m.Result = (IntPtr)16;
                    else if (bottom && right) m.Result = (IntPtr)17;
                    else if (left) m.Result = (IntPtr)10;
                    else if (right) m.Result = (IntPtr)11;
                    else if (top) m.Result = (IntPtr)12;
                    else if (bottom) m.Result = (IntPtr)15;
                }
                return;
            }

            base.WndProc(ref m);
        }
    }
}
