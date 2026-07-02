using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AssemblyLineTester.Views.Pages
{
    public partial class Settings : UserControl
    {
        private TableLayoutPanel? tblUcContainer;
        private Label? lblUnderConstructionTitle;

        public Settings()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                InitializeDefaults();
                SetupInteractiveTabs();
            }
        }

        private void InitializeDefaults()
        {
            if (cmbLang.Items.Count > 0) cmbLang.SelectedIndex = 0;
            if (cmbDateFormat.Items.Count > 0) cmbDateFormat.SelectedIndex = 0;
            if (cmbTimeFormat.Items.Count > 0) cmbTimeFormat.SelectedIndex = 0;
            if (cmbAutoLogout.Items.Count > 0) cmbAutoLogout.SelectedIndex = 0;
            if (cmbTestType.Items.Count > 0) cmbTestType.SelectedIndex = 0;
            if (cmbDefaultArea.Items.Count > 0) cmbDefaultArea.SelectedIndex = 0;
            if (cmbDefaultOp.Items.Count > 0) cmbDefaultOp.SelectedIndex = 0;
            if (cmbRefresh.Items.Count > 0) cmbRefresh.SelectedIndex = 0;
            if (cmbTimeZone.Items.Count > 0) cmbTimeZone.SelectedIndex = 0;
        }

        private void SetupInteractiveTabs()
        {
            Label[] tabLabels = [lblTabGen, lblTabTest, lblTabHw, lblTabUsers, lblTabNotif, lblTabBackup, lblTabSysInfo];

            // Setup General Tab click handler
            lblTabGen.Cursor = Cursors.Hand;
            tabGeneral.Cursor = Cursors.Hand;
            lblTabGen.Click += (s, ev) => Tab_Click(lblTabGen, ev);
            tabGeneral.Click += (s, ev) => Tab_Click(lblTabGen, ev);

            // Dynamically wrap other tab labels in TableLayoutPanel containers so they can host the yellow indicator line
            foreach (Label lbl in tabLabels)
            {
                if (lbl == lblTabGen) continue;

                lbl.Cursor = Cursors.Hand;
                int idx = flpTabs.Controls.IndexOf(lbl);
                if (idx < 0) continue;

                TableLayoutPanel tblTab = new TableLayoutPanel
                {
                    AutoSize = true,
                    ColumnCount = 1,
                    RowCount = 2,
                    Margin = lbl.Margin,
                    Cursor = Cursors.Hand
                };
                tblTab.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
                tblTab.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
                tblTab.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));

                lbl.Margin = new Padding(0, 13, 0, 0); // Reset inner cell margin
                flpTabs.Controls.RemoveAt(idx);
                tblTab.Controls.Add(lbl, 0, 0);
                flpTabs.Controls.Add(tblTab);
                flpTabs.Controls.SetChildIndex(tblTab, idx);

                lbl.Click += (s, ev) => Tab_Click(lbl, ev);
                tblTab.Click += (s, ev) => Tab_Click(lbl, ev);
            }

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
                Font = new Font("Segoe UI", 48F),
                TextAlign = ContentAlignment.BottomCenter,
                Dock = DockStyle.Fill
            };

            lblUnderConstructionTitle = new Label
            {
                Text = "Under Construction",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                AutoSize = true,
                Margin = new Padding(0, 10, 0, 5)
            };

            Label lblUcSub = new Label
            {
                Text = "This settings module is currently under active development.\nPlease check back in a future application update.",
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(100, 116, 139),
                TextAlign = ContentAlignment.TopCenter,
                Dock = DockStyle.Fill
            };

            tblUcCardContent.Controls.Add(lblUcIcon, 0, 0);
            tblUcCardContent.Controls.Add(lblUnderConstructionTitle, 0, 1);
            tblUcCardContent.Controls.Add(lblUcSub, 0, 2);

            cardUc.Controls.Add(tblUcCardContent);
            tblUcContainer.Controls.Add(cardUc, 1, 1);

            tblMain.Controls.Add(tblUcContainer, 0, 2);
        }

        private void Tab_Click(Label clickedLabel, EventArgs e)
        {
            Label[] tabLabels = [lblTabGen, lblTabTest, lblTabHw, lblTabUsers, lblTabNotif, lblTabBackup, lblTabSysInfo];

            // Reset visual states of all tabs
            foreach (Label lbl in tabLabels)
            {
                lbl.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular);
                lbl.ForeColor = Color.FromArgb(100, 116, 139);
            }

            // Highlight active tab
            clickedLabel.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            clickedLabel.ForeColor = Color.FromArgb(30, 41, 59);

            // Move bottom yellow indicator line
            if (clickedLabel.Parent is TableLayoutPanel parentTbl)
            {
                if (pnlTabIndicator.Parent != null)
                {
                    pnlTabIndicator.Parent.Controls.Remove(pnlTabIndicator);
                }
                parentTbl.Controls.Add(pnlTabIndicator, 0, 1);
            }

            // Switch content view
            if (clickedLabel == lblTabGen)
            {
                tblContent.Visible = true;
                if (tblUcContainer != null) tblUcContainer.Visible = false;
            }
            else
            {
                tblContent.Visible = false;
                if (tblUcContainer != null && lblUnderConstructionTitle != null)
                {
                    lblUnderConstructionTitle.Text = $"{clickedLabel.Text.Trim()} — Under Construction";
                    tblUcContainer.Visible = true;
                }
            }
        }

    }
}