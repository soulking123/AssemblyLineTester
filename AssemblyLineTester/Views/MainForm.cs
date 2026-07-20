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

        /// Initializes a new instance of the MainForm class.
        public MainForm()
        {
            InitializeComponent();
            EnableDoubleBuffering();

            // Load CAT logo image into sidebar menu header
            try
            {
                string logoPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "cat_logo.png");
                if (System.IO.File.Exists(logoPath))
                {
                    lblMenu.Image = Image.FromFile(logoPath);
                }
            }
            catch { /* fallback silently */ }
            this.WindowState = FormWindowState.Maximized;
            expandedWidth = splitContainer1.SplitterDistance;
            collapsedWidth = this.LogicalToDeviceUnits(85);

            // Apply dynamic Role-Based Access Control permissions to sidebar buttons
            ApplyRolePermissions();

            ApplySidebarStyles();
            InitializeUserInfo();

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
            string fullName = SessionManager.CurrentUser?.FullName ?? "Fally Kusuma";
            if (!string.IsNullOrWhiteSpace(fullName)) lblUserName.Text = fullName;
            string roleName = SessionManager.CurrentUser?.Role?.Name ?? "Administrator";
            if (!string.IsNullOrWhiteSpace(roleName)) lblUserRole.Text = roleName;
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

        private void BtnToggleSidebar_Click(object sender, EventArgs e)
        {
            var menuButtons = new AssemblyLineTester.Views.Controls.MenuButton[] { btnDashboard, btnTestExecution, btnManualTesting, btnHarnessDatabase, btnTestHistory, btnReports, btnSystemLog, btnSettings, btnUserManagement };

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
                    pnlLogoHeader.Size = new Size(collapsedWidth, 85);
                    lblMenu.Size = new Size(65, 65);
                    lblMenu.Location = new Point((collapsedWidth - 65) / 2, 10);
                    btnToggleSidebar.Location = new Point(0, 88);
                    btnToggleSidebar.Size = new Size(collapsedWidth, 66);

                    int currentY = 180;
                    foreach (var btn in menuButtons)
                    {
                        btn.SetMinimized(true);
                        btn.Location = new Point(0, currentY);
                        btn.Width = collapsedWidth;
                        currentY += btn.Height + 12;
                    }
                }
                else
                {
                    splitContainer1.SplitterDistance = expandedWidth;
                    isSidebarExpanded = true;

                    pnlLogoHeader.IsMinimized = false;
                    pnlLogoHeader.Size = new Size(300, 130);
                    lblMenu.Size = new Size(200, 90);
                    lblMenu.Location = new Point(16, 20);
                    btnToggleSidebar.Location = new Point(320, 24);
                    btnToggleSidebar.Size = new Size(88, 88);

                    int currentY = 160;
                    foreach (var btn in menuButtons)
                    {
                        btn.SetMinimized(false);
                        btn.Location = new Point(32, currentY);
                        btn.Width = expandedWidth - 55;
                        currentY += btn.Height + 16;
                    }
                }
            }
            finally
            {
                splitContainer1.Panel2.ResumeLayout(true);
                splitContainer1.Panel1.ResumeLayout(true);
                splitContainer1.ResumeLayout(true);
                this.ResumeLayout(true);
                SendMessage(this.Handle, 0x000B, 1, 0); // WM_SETREDRAW true
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

        private void ToggleUserDropdown(object? sender, EventArgs e)
        {
            if (pnlUserDropdown == null) return;
            if (pnlUserDropdown.Visible)
            {
                pnlUserDropdown.Visible = false;
                lblArrow.Text = "⌄";
            }
            else
            {
                // Align compact logout dropdown cleanly under the right side of pnlUser (near arrow)
                Point screenPos = pnlUser.PointToScreen(new Point(pnlUser.Width - pnlUserDropdown.Width - 15, pnlUser.Height));
                Point containerPos = this.PointToClient(screenPos);

                pnlUserDropdown.Location = containerPos;
                pnlUserDropdown.Visible = true;
                pnlUserDropdown.BringToFront();
                lblArrow.Text = "⌃";
            }
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

        private void BtnLogOut_Click(object? sender, EventArgs e)
        {
            if (pnlUserDropdown != null) pnlUserDropdown.Visible = false;
            lblArrow.Text = "⌄";
            if (MessageBox.Show("Are you sure you want to log out of Assembly Line Tester?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                IsLoggingOut = true;
                this.Close();
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            Application.Exit();
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
