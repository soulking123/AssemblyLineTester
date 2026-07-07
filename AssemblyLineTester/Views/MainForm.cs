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

        private readonly TestHistory pageTestHistory = new();
        private readonly SystemLog pageSystemLog = new();
        private readonly HarnessDatabase pageHarnessDb = new();
        private readonly Dashboard pageDashboard = new();
        private readonly TestExecution pageTestExecution = new();
        private readonly Reports pageReports = new();
        private readonly Settings pageSettings = new();
        //private readonly UserManagement pageUserManagement = new();
        private readonly UserManagement pageUserManagement;

        /// Initializes a new instance of the MainForm class.
        public MainForm()
        {
            pageUserManagement = new UserManagement(_userService);
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            expandedWidth = splitContainer1.SplitterDistance;
            collapsedWidth = this.LogicalToDeviceUnits(85);

            // Restrict access to User Management sidebar button to Administrators only
            btnUserManagement.Visible = (SessionManager.CurrentUser?.Role?.Name == "Administrator");

            ApplySidebarStyles();
            InitializeUserInfo();

            // Wire up header click events to auto-close dropdown when clicking outside
            pnlTopHeader.Click += CloseDropdownIfOpen;
            lblHeaderTitle.Click += CloseDropdownIfOpen;
            pnlRight.Click += CloseDropdownIfOpen;
            pnlDateTime.Click += CloseDropdownIfOpen;
            lblDateText.Click += CloseDropdownIfOpen;
            lblTimeText.Click += CloseDropdownIfOpen;

            SetActiveMenu(btnDashboard, pageDashboard);
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
            Button[] menuButtons = [btnDashboard, btnTestExecution, btnHarnessDatabase, btnTestHistory, btnReports, btnSystemLog, btnSettings, btnUserManagement];
            Font menuFont = new("Segoe UI", 11F, FontStyle.Regular);

            foreach (Button btn in menuButtons)
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

            btnDashboard.Text = "  📊    Dashboard";
            btnTestExecution.Text = "  ⚡    Test Execution";
            btnHarnessDatabase.Text = "  🗄️    Harness Database";
            btnTestHistory.Text = "  📜    Test History";
            btnReports.Text = "  📑    Reports";
            btnSystemLog.Text = "  🖥️    System Log";
            btnSettings.Text = "  ⚙️    Settings";
            btnUserManagement.Text = "  👥    User Management";
        }

        // IDE1006 FIX: Huruf awal kapital (PascalCase)
        private void BtnMenu_Click(object sender, EventArgs e)
        {
            if (sender is not Button clickedButton) return;

            UserControl? targetPage = clickedButton.Name switch
            {
                "btnDashboard" => pageDashboard,
                "btnTestExecution" => pageTestExecution,
                "btnHarnessDatabase" => pageHarnessDb,
                "btnTestHistory" => pageTestHistory,
                "btnReports" => pageReports,
                "btnSystemLog" => pageSystemLog,
                "btnSettings" => pageSettings,
                "btnUserManagement" => pageUserManagement,
                _ => null
            };

            SetActiveMenu(clickedButton, targetPage);
        }

        private void SetActiveMenu(Button activeBtn, UserControl? activePage)
        {
            if (pnlUserDropdown != null && pnlUserDropdown.Visible)
            {
                pnlUserDropdown.Visible = false;
                lblArrow.Text = "⌄";
            }

            Button[] allMenus = [btnDashboard, btnTestExecution, btnHarnessDatabase, btnTestHistory, btnReports, btnSystemLog, btnSettings, btnUserManagement];

            foreach (Button btn in allMenus)
            {
                btn.BackColor = Color.Transparent;
                btn.ForeColor = Color.White;
            }

            activeBtn.BackColor = Color.FromArgb(255, 193, 7);
            activeBtn.ForeColor = Color.Black;

            if (pnlContent == null) return;
            pnlContent.Controls.Clear();
            if (activePage != null)
            {
                activePage.Dock = DockStyle.Fill;
                pnlContent.Controls.Add(activePage);
            }
            else
            {
                Label lblPlaceholder = new Label
                {
                    Text = $"{activeBtn.Text.Trim()} (Under Construction)",
                    Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(100, 116, 139),
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.FromArgb(244, 245, 247)
                };
                pnlContent.Controls.Add(lblPlaceholder);
            }
        }

        // IDE1006 FIX: Huruf awal kapital (PascalCase)
        private void BtnToggleSidebar_Click(object sender, EventArgs e)
        {
            if (isSidebarExpanded)
            {
                splitContainer1.SplitterDistance = collapsedWidth;
                isSidebarExpanded = false;
                lblMenu.Visible = false;
            }
            else
            {
                splitContainer1.SplitterDistance = expandedWidth;
                isSidebarExpanded = true;
                lblMenu.Visible = true;
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
    }
}
