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
            }
        }

        private void SetActiveMenu(Button activeBtn, UserControl activePage)
        {
            Button[] allMenus = [btnDashboard, btnTestExecution, btnHarnessDatabase, btnTestHistory, btnReports, btnSystemLog, btnSettings, btnUserManagement];

            foreach (Button btn in allMenus)
            {
                btn.BackColor = Color.Transparent;
                btn.ForeColor = Color.White;
            }

            activeBtn.BackColor = Color.FromArgb(255, 193, 7);
            activeBtn.ForeColor = Color.Black;

            if (activePage != null)
            {
                activePage.Dock = DockStyle.Fill;
                splitContainer1.Panel2.Controls.Clear();
                splitContainer1.Panel2.Controls.Add(activePage);
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
    }
}