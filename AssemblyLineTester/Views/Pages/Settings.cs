using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace AssemblyLineTester.Views.Pages
{
    public partial class Settings : UserControl
    {
        private TableLayoutPanel? tblUcContainer;
        private Label? lblUnderConstructionTitle;
        private SettingsTestParametersPage? testParametersPage;
        private Panel? pnlPageContainer;
        private Label? activeTabLabel;
        private bool isLoaded = false;

        public Settings()
        {
            InitializeComponent();
            EnableDoubleBuffering();
            if (cmbLang.Items.Count > 0) cmbLang.SelectedIndex = 0;
            if (cmbDateFormat.Items.Count > 0) cmbDateFormat.SelectedIndex = 0;
            if (cmbTimeFormat.Items.Count > 0) cmbTimeFormat.SelectedIndex = 0;
            if (cmbAutoLogout.Items.Count > 0) cmbAutoLogout.SelectedIndex = 0;
            if (cmbTestType.Items.Count > 0) cmbTestType.SelectedIndex = 0;
            if (cmbDefaultArea.Items.Count > 0) cmbDefaultArea.SelectedIndex = 0;
            if (cmbDefaultOp.Items.Count > 0) cmbDefaultOp.SelectedIndex = 0;
            if (cmbRefresh.Items.Count > 0) cmbRefresh.SelectedIndex = 0;
            if (cmbTimeZone.Items.Count > 0) cmbTimeZone.SelectedIndex = 0;
            SetupEvents();
        }

        private void EnableDoubleBuffering()
        {
            try
            {
                this.DoubleBuffered = true;
                typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(tblMain, true, null);
                typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(tblContent, true, null);
                typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(flpTabs, true, null);
                typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(tblLeftColumn, true, null);
            }
            catch { /* ignore */ }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (isLoaded) return;
            if (!DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                isLoaded = true;
                this.SuspendLayout();
                tblMain.SuspendLayout();
                try
                {
                    SetupInteractiveTabs();
                }
                finally
                {
                    tblMain.ResumeLayout(true);
                    this.ResumeLayout(true);
                }
            }
        }



        private void SetupInteractiveTabs()
        {
            Label[] tabLabels = [lblTabGen, lblTabTest, lblTabHw, lblTabUsers, lblTabNotif, lblTabBackup, lblTabSysInfo];

            activeTabLabel = lblTabGen;

            // Setup General Tab click handler
            lblTabGen.Cursor = Cursors.Hand;
            tabGeneral.Cursor = Cursors.Hand;
            lblTabGen.Click += (s, ev) => Tab_Click(lblTabGen, ev);
            tabGeneral.Click += (s, ev) => Tab_Click(lblTabGen, ev);

            flpTabs.SuspendLayout();
            try
            {
                foreach (Label lbl in tabLabels)
                {
                    if (lbl == lblTabGen) continue;
                    lbl.Cursor = Cursors.Hand;
                    lbl.Click += (s, ev) => Tab_Click(lbl, ev);
                    lbl.Paint += (s, ev) =>
                    {
                        if (lbl == activeTabLabel)
                        {
                            using var brush = new SolidBrush(Color.FromArgb(245, 158, 11));
                            ev.Graphics.FillRectangle(brush, 0, lbl.Height - 6, lbl.Width, 6);
                        }
                    };
                }
            }
            finally
            {
                flpTabs.ResumeLayout(true);
            }

            // Create page container to isolate Z-order and eliminate multiple controls occupying the same TableLayoutPanel cell
            pnlPageContainer = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                BackColor = Color.FromArgb(244, 245, 247)
            };

            if (tblMain.Controls.Contains(tblContent))
            {
                tblMain.Controls.Remove(tblContent);
            }
            tblContent.Dock = DockStyle.Fill;
            pnlPageContainer.Controls.Add(tblContent);

            // Build Under Construction panel container
            tblUcContainer = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 3,
                BackColor = Color.FromArgb(244, 245, 247),
                Visible = false
            };
            tblUcContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblUcContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 680F));
            tblUcContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblUcContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            tblUcContainer.RowStyles.Add(new RowStyle(SizeType.Absolute, 300F));
            tblUcContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));

            Panel cardUc = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardUc, 12, Color.FromArgb(34, 40, 49), 2.5f);

            TableLayoutPanel tblUcCardContent = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3
            };
            tblUcCardContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblUcCardContent.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
            tblUcCardContent.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tblUcCardContent.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));

            Label lblUcIcon = new Label
            {
                Text = "🚧",
                Font = AssemblyLineTester.Helpers.UIStyleHelper.Font48Regular,
                TextAlign = ContentAlignment.BottomCenter,
                Dock = DockStyle.Fill
            };

            lblUnderConstructionTitle = new Label
            {
                Text = "Under Construction",
                Font = AssemblyLineTester.Helpers.UIStyleHelper.Font18Bold,
                ForeColor = Color.FromArgb(30, 41, 59),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                AutoSize = true,
                Margin = new Padding(0, 10, 0, 5)
            };

            Label lblUcSub = new Label
            {
                Text = "This settings module is currently under active development.\nPlease check back in a future application update.",
                Font = AssemblyLineTester.Helpers.UIStyleHelper.Font11Regular,
                ForeColor = Color.FromArgb(100, 116, 139),
                TextAlign = ContentAlignment.TopCenter,
                Dock = DockStyle.Fill
            };

            tblUcCardContent.Controls.Add(lblUcIcon, 0, 0);
            tblUcCardContent.Controls.Add(lblUnderConstructionTitle, 0, 1);
            tblUcCardContent.Controls.Add(lblUcSub, 0, 2);

            cardUc.Controls.Add(tblUcCardContent);
            tblUcContainer.Controls.Add(cardUc, 1, 1);

            pnlPageContainer.Controls.Add(tblUcContainer);
            tblMain.Controls.Add(pnlPageContainer, 0, 2);
        }

        private void Tab_Click(Label clickedLabel, EventArgs e)
        {
            if (pnlPageContainer == null) return;
            pnlPageContainer.SuspendLayout();
            flpTabs.SuspendLayout();
            try
            {
                Label[] tabLabels = [lblTabGen, lblTabTest, lblTabHw, lblTabUsers, lblTabNotif, lblTabBackup, lblTabSysInfo];
                activeTabLabel = clickedLabel;

                // Reset visual states of all tabs
                foreach (Label lbl in tabLabels)
                {
                    lbl.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font105Regular;
                    lbl.ForeColor = Color.FromArgb(100, 116, 139);
                }

                // Highlight active tab
                clickedLabel.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font105Bold;
                clickedLabel.ForeColor = Color.FromArgb(30, 41, 59);

                // Move bottom yellow indicator for General tab vs others
                if (pnlTabIndicator != null)
                {
                    pnlTabIndicator.Visible = (clickedLabel == lblTabGen);
                }
                flpTabs.Invalidate(true);

                // Switch content view cleanly inside pnlPageContainer
                if (clickedLabel == lblTabGen)
                {
                    tblContent.Visible = true;
                    tblContent.BringToFront();
                    if (testParametersPage != null) testParametersPage.Visible = false;
                    if (tblUcContainer != null) tblUcContainer.Visible = false;
                }
                else if (clickedLabel == lblTabTest)
                {
                    tblContent.Visible = false;
                    if (tblUcContainer != null) tblUcContainer.Visible = false;
                    if (testParametersPage == null)
                    {
                        testParametersPage = new SettingsTestParametersPage
                        {
                            Dock = DockStyle.Fill,
                            Visible = true,
                            Margin = new Padding(0)
                        };
                        pnlPageContainer.Controls.Add(testParametersPage);
                    }
                    testParametersPage.Visible = true;
                    testParametersPage.BringToFront();
                }
                else
                {
                    tblContent.Visible = false;
                    if (testParametersPage != null) testParametersPage.Visible = false;
                    if (tblUcContainer != null && lblUnderConstructionTitle != null)
                    {
                        lblUnderConstructionTitle.Text = $"{clickedLabel.Text.Trim()} — Under Construction";
                        tblUcContainer.Visible = true;
                        tblUcContainer.BringToFront();
                    }
                }
            }
            finally
            {
                flpTabs.ResumeLayout(true);
                pnlPageContainer.ResumeLayout(true);
            }
        }

        private void SetupEvents()
        {
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardGeneral, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardTestSettings, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardFileSettings, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardAppSettings, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardSystemStatus, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardLicenseInfo, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardSystemTime, 12, Color.FromArgb(34, 40, 49), 2.5f);

            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnBrowseData, 8, Color.FromArgb(241, 245, 249), Color.FromArgb(226, 232, 240), Color.FromArgb(203, 213, 225), Color.Transparent);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnBrowseReport, 8, Color.FromArgb(241, 245, 249), Color.FromArgb(226, 232, 240), Color.FromArgb(203, 213, 225), Color.Transparent);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnManageLicense, 10, Color.FromArgb(250, 204, 21), Color.FromArgb(254, 216, 53), Color.FromArgb(234, 186, 10), Color.Transparent);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnSyncTime, 10, Color.FromArgb(250, 204, 21), Color.FromArgb(254, 216, 53), Color.FromArgb(234, 186, 10), Color.Transparent);
        }
    }
}
