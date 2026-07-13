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

        public Settings()
        {
            InitializeComponent();
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                SetupInteractiveTabs();
                AdjustCardLayouts();
            }
        }

        private void AdjustCardLayouts()
        {
            TableLayoutPanel[] cards = [tblGenCard, tblTestCard, tblFileCard, tblAppCard];
            foreach (var card in cards)
            {
                if (card is not null)
                {
                    FixMiniTables(card);
                }
            }
        }

        private void FixMiniTables(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is TableLayoutPanel tbl && tbl.RowCount == 2 && tbl.Controls.Count >= 2)
                {
                    tbl.AutoSize = true;
                    tbl.Margin = new Padding(6, 2, 6, 2);
                    if (tbl.RowStyles.Count >= 2)
                    {
                        tbl.RowStyles[0] = new RowStyle(SizeType.AutoSize);
                        tbl.RowStyles[1] = new RowStyle(SizeType.AutoSize);
                    }
                }
                if (ctrl.HasChildren)
                {
                    FixMiniTables(ctrl);
                }
            }
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
            SetupCardRoundedEdges(cardUc, 12, Color.FromArgb(34, 40, 49), 2.5f);

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

        private void SetupEvents()
        {
            SetupCardRoundedEdges(cardGeneral, 12, Color.FromArgb(34, 40, 49), 2.5f);
            SetupCardRoundedEdges(cardTestSettings, 12, Color.FromArgb(34, 40, 49), 2.5f);
            SetupCardRoundedEdges(cardFileSettings, 12, Color.FromArgb(34, 40, 49), 2.5f);
            SetupCardRoundedEdges(cardAppSettings, 12, Color.FromArgb(34, 40, 49), 2.5f);
            SetupCardRoundedEdges(cardSystemStatus, 12, Color.FromArgb(34, 40, 49), 2.5f);
            SetupCardRoundedEdges(cardLicenseInfo, 12, Color.FromArgb(34, 40, 49), 2.5f);
            SetupCardRoundedEdges(cardSystemTime, 12, Color.FromArgb(34, 40, 49), 2.5f);

            SetupButtonRoundedEdges(btnBrowseData, 8, Color.FromArgb(241, 245, 249), Color.FromArgb(226, 232, 240), Color.FromArgb(203, 213, 225), Color.Transparent);
            SetupButtonRoundedEdges(btnBrowseReport, 8, Color.FromArgb(241, 245, 249), Color.FromArgb(226, 232, 240), Color.FromArgb(203, 213, 225), Color.Transparent);
            SetupButtonRoundedEdges(btnManageLicense, 10, Color.FromArgb(250, 204, 21), Color.FromArgb(254, 216, 53), Color.FromArgb(234, 186, 10), Color.Transparent);
            SetupButtonRoundedEdges(btnSyncTime, 10, Color.FromArgb(250, 204, 21), Color.FromArgb(254, 216, 53), Color.FromArgb(234, 186, 10), Color.Transparent);
        }

        private static void SetupCardRoundedEdges(Control card, int radius, Color borderColor, float borderWidth = 2.5f)
        {
            if (card is TableLayoutPanel || card.Padding.Left < 4 || card.Padding.Top < 4 || card.Padding.Right < 4 || card.Padding.Bottom < 4)
            {
                card.Padding = new Padding(
                    Math.Max(card.Padding.Left, 4),
                    Math.Max(card.Padding.Top, 4),
                    Math.Max(card.Padding.Right, 4),
                    Math.Max(card.Padding.Bottom, 4)
                );
            }

            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                Color parentBg = card.Parent?.BackColor != null && card.Parent.BackColor != Color.Transparent
                    ? card.Parent.BackColor
                    : Color.FromArgb(244, 245, 247);

                using (var parentBrush = new SolidBrush(parentBg))
                {
                    e.Graphics.FillRectangle(parentBrush, 0, 0, card.Width, card.Height);
                }

                var rectOuter = new Rectangle(0, 0, card.Width, card.Height);
                using var pathOuter = GetRoundedPath(rectOuter, radius);
                using (var cardBrush = new SolidBrush(card.BackColor))
                {
                    e.Graphics.FillPath(cardBrush, pathOuter);
                }

                var rectBorder = new Rectangle(1, 1, card.Width - 3, card.Height - 3);
                using var pathBorder = GetRoundedPath(rectBorder, radius);
                using var pen = new Pen(borderColor, borderWidth);
                e.Graphics.DrawPath(pen, pathBorder);
            };

            card.Resize += (s, e) =>
            {
                if (card.Width > 0 && card.Height > 0)
                {
                    var oldRegion = card.Region;
                    card.Region = new Region(GetRoundedPath(new Rectangle(0, 0, card.Width, card.Height), radius));
                    oldRegion?.Dispose();
                    card.Invalidate();
                }
            };

            if (card.Width > 0 && card.Height > 0)
            {
                card.Region = new Region(GetRoundedPath(new Rectangle(0, 0, card.Width, card.Height), radius));
            }
        }

        private static void SetupButtonRoundedEdges(Button btn, int radius, Color normColor, Color hoverColor, Color pressColor, Color borderColor)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = normColor;
            btn.Cursor = Cursors.Hand;

            btn.MouseEnter += (s, e) => { btn.BackColor = hoverColor; btn.Invalidate(); };
            btn.MouseLeave += (s, e) => { btn.BackColor = normColor; btn.Invalidate(); };
            btn.MouseDown += (s, e) => { btn.BackColor = pressColor; btn.Invalidate(); };
            btn.MouseUp += (s, e) => { btn.BackColor = hoverColor; btn.Invalidate(); };

            btn.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                Color parentBg = btn.Parent?.BackColor != null && btn.Parent.BackColor != Color.Transparent
                    ? btn.Parent.BackColor
                    : Color.White;

                using (var parentBrush = new SolidBrush(parentBg))
                {
                    e.Graphics.FillRectangle(parentBrush, 0, 0, btn.Width, btn.Height);
                }

                var rectOuter = new Rectangle(0, 0, btn.Width, btn.Height);
                using var pathOuter = GetRoundedPath(rectOuter, radius);
                using (var brush = new SolidBrush(btn.BackColor))
                {
                    e.Graphics.FillPath(brush, pathOuter);
                }

                if (borderColor != Color.Transparent)
                {
                    var rectBorder = new Rectangle(1, 1, btn.Width - 3, btn.Height - 3);
                    using var pathBorder = GetRoundedPath(rectBorder, radius);
                    using var pen = new Pen(borderColor, 1.5f);
                    e.Graphics.DrawPath(pen, pathBorder);
                }

                TextRenderer.DrawText(
                    e.Graphics,
                    btn.Text,
                    btn.Font,
                    rectOuter,
                    btn.ForeColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak
                );
            };

            btn.Resize += (s, e) =>
            {
                if (btn.Width > 0 && btn.Height > 0)
                {
                    var oldRegion = btn.Region;
                    btn.Region = new Region(GetRoundedPath(new Rectangle(0, 0, btn.Width, btn.Height), radius));
                    oldRegion?.Dispose();
                    btn.Invalidate();
                }
            };

            if (btn.Width > 0 && btn.Height > 0)
            {
                btn.Region = new Region(GetRoundedPath(new Rectangle(0, 0, btn.Width, btn.Height), radius));
            }
        }

        private static GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new();
            int d = radius * 2;
            if (rect.Width <= 0 || rect.Height <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }
            if (d > rect.Width) d = rect.Width;
            if (d > rect.Height) d = rect.Height;

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
