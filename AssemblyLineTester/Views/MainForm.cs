using AssemblyLineTester.Views.Pages;

namespace AssemblyLineTester.Views
{
    public partial class MainForm : Form
    {
        private bool isSidebarExpanded = true;
        private readonly int expandedWidth = 380;
        private readonly int collapsedWidth = 90;

        private readonly TestHistory pageTestHistory = new();
        private readonly SystemLog pageSystemLog = new();
        private readonly HarnessDatabase pageHarnessDb = new();
        private readonly Dashboard pageDashboard = new();
        private readonly TestExecution pageTestExecution = new();
        private readonly Reports pageReports = new();
        private readonly Settings pageSettings = new();
        private readonly UserManagement pageUserManagement = new();

        public MainForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            ApplySidebarStyles();
            SetActiveMenu(btnDashboard, pageDashboard);
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
        }

        // IDE1006 FIX: Huruf awal kapital (PascalCase)
        private void BtnMenu_Click(object sender, EventArgs e)
        {
            if (sender is Button clickedButton)
            {
                if (clickedButton == btnDashboard)
                {
                    SetActiveMenu(btnDashboard, pageDashboard);
                }
                else if (clickedButton == btnTestExecution)
                {
                    SetActiveMenu(btnTestExecution, pageTestExecution);
                }
                else if (clickedButton == btnTestHistory)
                {
                    SetActiveMenu(btnTestHistory, pageTestHistory);
                }
                else if (clickedButton == btnSystemLog)
                {
                    SetActiveMenu(btnSystemLog, pageSystemLog);
                }
                else if (clickedButton == btnHarnessDatabase)
                {
                    SetActiveMenu(btnHarnessDatabase, pageHarnessDb);
                }
                else if (clickedButton == btnReports)
                {
                    SetActiveMenu(btnReports, pageReports);
                }
                else if (clickedButton == btnUserManagement)
                {
                    SetActiveMenu(btnUserManagement, pageUserManagement);
                }
                else if (clickedButton == btnSettings)
                {
                    SetActiveMenu(clickedButton, pageSettings);
                }
            }
        }

        private void SetActiveMenu(Button activeBtn, UserControl? activePage)
        {
            Button[] allMenus = [btnDashboard, btnTestExecution, btnHarnessDatabase, btnTestHistory, btnReports, btnSystemLog, btnSettings, btnUserManagement];

            foreach (Button btn in allMenus)
            {
                btn.BackColor = Color.Transparent;
                btn.ForeColor = Color.White;
            }

            activeBtn.BackColor = Color.FromArgb(255, 193, 7);
            activeBtn.ForeColor = Color.Black;

            splitContainer1.Panel2.Controls.Clear();
            if (activePage != null)
            {
                activePage.Dock = DockStyle.Fill;
                splitContainer1.Panel2.Controls.Add(activePage);
            }
            else
            {
                Label lblPlaceholder = new Label
                {
                    Text = $"⚙️  {activeBtn.Text.Trim()} (Under Construction)",
                    Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(100, 116, 139),
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.FromArgb(244, 245, 247)
                };
                splitContainer1.Panel2.Controls.Add(lblPlaceholder);
            }
        }

        // IDE1006 FIX: Huruf awal kapital (PascalCase)
        private void BtnToggleSidebar_Click(object sender, EventArgs e)
        {
            if (isSidebarExpanded)
            {
                splitContainer1.SplitterDistance = collapsedWidth;
                isSidebarExpanded = false;
            }
            else
            {
                splitContainer1.SplitterDistance = expandedWidth;
                isSidebarExpanded = true;
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            Application.Exit();
        }
    }
}