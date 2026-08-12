using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using AssemblyLineTester.Helpers;
using AssemblyLineTester.Models;
using AssemblyLineTester.Services;
using AssemblyLineTester.Data;

namespace AssemblyLineTester.Views.Pages
{
    public partial class Settings : UserControl
    {
        private TableLayoutPanel? tblUcContainer;
        private Label? lblUnderConstructionTitle;
        private SettingsUsersAndRolesPage? _usersAndRolesPage;
        private SettingBackupRestore? _backupRestorePage;

        private readonly List<(int Id, CheckBox Box)> _typeBoxes = new();
        private readonly List<(int Id, CheckBox Box)> _learnBoxes = new();
        private readonly List<(int Id, CheckBox Box)> _resultBoxes = new();
        private Button? _btnEditTicks;
        private Button? _btnSaveTicks;
        private Button? _btnCancelTicks;

        // Jumps to the General tab, which carries the maintenance countdown.
        public void OpenGeneralTab()
        {
            if (lblTabGen != null)
            {
                Tab_Click(lblTabGen, EventArgs.Empty);
            }
        }

        // Jumps straight to the Users & Roles tab.
        public void OpenUsersAndRolesTab()
        {
            if (lblTabUsers != null)
            {
                Tab_Click(lblTabUsers, EventArgs.Empty);
            }
        }
        private Panel? pnlPageContainer;
        private Label? activeTabLabel;
        private bool isLoaded = false;

        private AssemblyLineTester.Models.SettingsPreloadData? _preloadData;

        public Settings() : this(null)
        {
        }

        public Settings(AssemblyLineTester.Models.SettingsPreloadData? preloadData)
        {
            _preloadData = preloadData;
            InitializeComponent();
            this.SuspendLayout();
            if (tblMain != null) tblMain.SuspendLayout();
            SetupInteractiveTabs();
            EnableDoubleBuffering();
            if (cmbRefresh.Items.Count > 0) cmbRefresh.SelectedIndex = 0;
            SetupEvents();
            LoadFileSettings();
            if (tblMain != null) tblMain.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // These came from the mockup. Only the folders exist in code, so they are shown read-only
        // rather than pretending an edit here changes anything.
        private void LoadFileSettings()
        {
            txtDataPath.Text = Data.AppDbContext.DataFolder;
            txtReportPath.Text = AppInfo.LogDirectory;

            lblReportStorage.Text = "Log Storage Path";

            foreach (TextBox box in new[] { txtDataPath, txtReportPath })
            {
                box.ReadOnly = true;
                box.BackColor = Color.FromArgb(241, 245, 249);
            }
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
            if (control is TableLayoutPanel || control is Panel || control is FlowLayoutPanel || control is SplitContainer)
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (isLoaded) return;
            if (!DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                AssemblyLineTester.Helpers.UIStyleHelper.ScaleExplicitFonts(this, AssemblyLineTester.Helpers.UIStyleHelper.GetHostScaleFactor(this));
                
                tblTestCard.SuspendLayout();
                cardTestSettings.SuspendLayout();
                PopulateLearnConnections();
                BuildTickListEditButtons();
                SetTickListsEditable(false);
                LayoutTickLists();
                cardTestSettings.ResumeLayout(true);
                tblTestCard.ResumeLayout(true);
                
                isLoaded = true;
                _preloadData = null;
            }
        }

        // Boxes added in code never went through the form's autoscale pass, so their margins stay
        // at the unscaled designer value while the ones placed in the designer have shrunk. Copying
        // the margin from a designer box is what makes all three lists share one row pitch.
        private void MatchTickMarginsToDesigner()
        {
            foreach (var (_, box) in _typeBoxes.Concat(_learnBoxes))
            {
                box.Margin = chkResNetId.Margin;
                box.Font = chkResNetId.Font;
            }
        }

        // Keeps the designer's column height and widens the panel to the columns that height
        // produces. The old MaximumSize width cut the last column off mid-label, which is why the
        // third Test Type column rendered as checkboxes with no text.
        private void LayoutTickLists()
        {
            // The panels sit inside a chain of AutoSize containers, so every MaximumSize write
            // relayouts the whole page. Suspending the page turns three cascades into one.
            SuspendLayout();
            try
            {
                MatchTickMarginsToDesigner();

                foreach (var flow in new[] { flpTestTypeAvail, flpLearnConn, flpTestResultCol })
                {
                    var boxes = flow.Controls.Cast<Control>().ToList();
                    if (boxes.Count == 0 || flow.MaximumSize.Height <= 0) continue;

                    // PreferredSize answers from the control itself. Reading Width/Height instead
                    // meant forcing a layout pass first, three times, on an AutoSize chain.
                    int widest = boxes.Max(b => b.PreferredSize.Width + b.Margin.Horizontal);
                    int pitch = boxes.Max(b => b.PreferredSize.Height + b.Margin.Vertical);

                    int perColumn = Math.Max(1, flow.MaximumSize.Height / Math.Max(1, pitch));
                    int columns = (int)Math.Ceiling(boxes.Count / (double)perColumn);

                    flow.MaximumSize = new Size(columns * widest, flow.MaximumSize.Height);
                }
            }
            finally
            {
                ResumeLayout(true);
            }
        }


        private void SetupInteractiveTabs()
        {
            Label[] tabLabels = [lblTabGen, lblTabTest, lblTabHw, lblTabUsers, lblTabNotif, lblTabBackup, lblTabSysInfo];

            activeTabLabel = lblTabGen;
            lblTabUsers.Visible = SessionManager.HasPermission("Page.UserManagement");

            flpTabs.SuspendLayout();
            try
            {
                foreach (Label lbl in tabLabels)
                {
                    lbl.Cursor = Cursors.Hand;
                    lbl.Padding = new Padding(0, 0, 0, 10);
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

            // Own container, so tab pages never share a TableLayoutPanel cell.
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
            if (clickedLabel == lblTabUsers && !SessionManager.HasPermission("Page.UserManagement")) return;
            pnlPageContainer.SuspendLayout();
            flpTabs.SuspendLayout();
            try
            {
                Label[] tabLabels = [lblTabGen, lblTabTest, lblTabHw, lblTabUsers, lblTabNotif, lblTabBackup, lblTabSysInfo];
                activeTabLabel = clickedLabel;

                foreach (Label lbl in tabLabels)
                {
                    lbl.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font105Regular;
                    lbl.ForeColor = Color.FromArgb(100, 116, 139);
                }

                clickedLabel.Font = AssemblyLineTester.Helpers.UIStyleHelper.Font105Bold;
                clickedLabel.ForeColor = Color.FromArgb(30, 41, 59);

                flpTabs.Invalidate(true);

                if (clickedLabel == lblTabGen)
                {
                    tblContent.Visible = true;
                    tblContent.BringToFront();
                    if (_usersAndRolesPage != null) _usersAndRolesPage.Visible = false;
                    if (_backupRestorePage != null) _backupRestorePage.Visible = false;
                    if (tblUcContainer != null) tblUcContainer.Visible = false;
                    if (settingSystemInfo != null) settingSystemInfo.Visible = false;
                }
                else if (clickedLabel == lblTabUsers)
                {
                    tblContent.Visible = false;
                    if (tblUcContainer != null) tblUcContainer.Visible = false;
                    if (_backupRestorePage != null) _backupRestorePage.Visible = false;
                    if (_usersAndRolesPage == null)
                    {
                        _usersAndRolesPage = new SettingsUsersAndRolesPage
                        {
                            Dock = DockStyle.Fill,
                            Visible = true,
                            Margin = new Padding(0)
                        };
                        pnlPageContainer.Controls.Add(_usersAndRolesPage);
                    }
                    _usersAndRolesPage.Visible = true;
                    _usersAndRolesPage.BringToFront();
                    if (settingSystemInfo != null) settingSystemInfo.Visible = false;
                }
                else if (clickedLabel == lblTabSysInfo)
                {
                    tblContent.Visible = false;
                    if (tblUcContainer != null) tblUcContainer.Visible = false;
                    if (_usersAndRolesPage != null) _usersAndRolesPage.Visible = false;
                    if (_backupRestorePage != null) _backupRestorePage.Visible = false;

                    settingSystemInfo ??= new SettingSystemInfo
                    {
                        Dock = DockStyle.Fill,
                        Margin = new Padding(0)
                    };
                    if (!pnlPageContainer.Controls.Contains(settingSystemInfo))
                    {
                        pnlPageContainer.Controls.Add(settingSystemInfo);
                    }
                    settingSystemInfo.Visible = true;
                    settingSystemInfo.BringToFront();
                }
                else if (clickedLabel == lblTabBackup)
                {
                    tblContent.Visible = false;
                    if (tblUcContainer != null) tblUcContainer.Visible = false;
                    if (_usersAndRolesPage != null) _usersAndRolesPage.Visible = false;
                    if (settingSystemInfo != null) settingSystemInfo.Visible = false;

                    if (_backupRestorePage == null)
                    {
                        _backupRestorePage = new SettingBackupRestore
                        {
                            Dock = DockStyle.Fill,
                            Visible = true,
                            Margin = new Padding(0)
                        };
                        pnlPageContainer.Controls.Add(_backupRestorePage);
                    }
                    _backupRestorePage.Visible = true;
                    _backupRestorePage.BringToFront();
                }
                else
                {
                    tblContent.Visible = false;
                    if (_usersAndRolesPage != null) _usersAndRolesPage.Visible = false;
                    if (_backupRestorePage != null) _backupRestorePage.Visible = false;
                    if (settingSystemInfo != null) settingSystemInfo.Visible = false;
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
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardMaintenanceState, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardTestSettings, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardAppSettings, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnBrowseData, 8, Color.FromArgb(241, 245, 249), Color.FromArgb(226, 232, 240), Color.FromArgb(203, 213, 225), Color.Transparent);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnBrowseReport, 8, Color.FromArgb(241, 245, 249), Color.FromArgb(226, 232, 240), Color.FromArgb(203, 213, 225), Color.Transparent);

            txtTotalTestPoint.KeyPress += NumericTextBox_KeyPress;
            txtDefResistorVal.KeyPress += NumericTextBox_KeyPress;
            txtDefMaxWireRes.KeyPress += NumericTextBox_KeyPress;
            txtDefResistorTol.KeyPress += NumericTextBox_KeyPress;
            txtDefWireInsRes.KeyPress += NumericTextBox_KeyPress;
            txtDefCapacitorVal.KeyPress += NumericTextBox_KeyPress;
            txtDefCapacitorTol.KeyPress += NumericTextBox_KeyPress;

            PopulateTestTypes();
            PopulateTestResultColumns();
            LoadDefaultTestSettings();
            SetupMaintenanceEvents();
        }

        private static bool HasMaintenancePermission()
        {
            if (SessionManager.HasPermission("Action.Maintenance.Reset")) return true;
            MessageBox.Show("You do not have permission to change the maintenance countdown.",
                "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        private void SetMaintenanceEditable(bool editable)
        {
            txtRemainingDays.ReadOnly = !editable;
            txtRemainingHours.ReadOnly = !editable;
            txtRemainingDays.BackColor = editable ? Color.White : Color.FromArgb(241, 245, 249);
            txtRemainingHours.BackColor = editable ? Color.White : Color.FromArgb(241, 245, 249);

            btnCancelMaintenance.Visible = editable;
            btnUpdateMaintenance.Visible = editable;
            lblEditHint.Visible = editable;

            // Reset would overwrite the figures being typed with the stored interval, so the two
            // actions cannot be offered at the same time.
            btnResetMaintenance.Enabled = !editable;
        }

        // Single path to the countdown, so every change is permission-checked and logged once.
        private bool ApplyMaintenance(int days, double hours)
        {
            if (!MaintenanceService.SetRemaining(days, hours))
            {
                MessageBox.Show("The maintenance countdown could not be saved. Check the log and try again.",
                    "Maintenance", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            BindMaintenanceState();
            return true;
        }

        private void SetupMaintenanceEvents()
        {
            BindMaintenanceState();
            SetMaintenanceEditable(false);

            btnEditMaintenance.Click += (s, e) =>
            {
                if (HasMaintenancePermission()) SetMaintenanceEditable(true);
            };

            btnCancelMaintenance.Click += (s, e) =>
            {
                SetMaintenanceEditable(false);
                BindMaintenanceState();
            };

            btnResetMaintenance.Click += (s, e) =>
            {
                if (!HasMaintenancePermission()) return;

                if (MessageBox.Show(
                        $"Reset the maintenance countdown to the interval set here?\r\n\r\n" +
                        $"{MaintenanceService.IntervalDays} day(s) or {MaintenanceService.FormatHours(MaintenanceService.IntervalHours)} of operating time.",
                        "Reset Maintenance", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                if (!MaintenanceService.ResetToInterval())
                {
                    MessageBox.Show("The maintenance countdown could not be reset. Check the log and try again.",
                        "Maintenance", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                BindMaintenanceState();
            };

            btnUpdateMaintenance.Click += (s, e) =>
            {
                if (!HasMaintenancePermission()) return;

                if (!int.TryParse(txtRemainingDays.Text, out int days) || days < 0 ||
                    !MaintenanceService.TryParseHours(txtRemainingHours.Text, out double hours))
                {
                    MessageBox.Show("Enter days as a whole number, and time as HH:MM:SS (a plain number of hours also works).",
                        "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (ApplyMaintenance(days, hours)) SetMaintenanceEditable(false);
            };
            
            var refreshTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            refreshTimer.Tick += (s, e) => 
            {
                if (this.Visible)
                {
                    TimeSpan timeToday = TimeSpan.FromSeconds(MaintenanceService.ActiveOperatingSeconds);
                    lblOperatingTimeTodayVal.Text = timeToday.ToString(@"hh\:mm\:ss");
                }
            };
            refreshTimer.Start();
        }

        private void BindMaintenanceState()
        {
            txtRemainingDays.Text = MaintenanceService.RemainingDays.ToString();
            txtRemainingHours.Text = MaintenanceService.FormatHoursPadded(MaintenanceService.RemainingHours);
            lblLastUsageVal.Text = MaintenanceService.LastUsageDate.ToString("dd MMM yyyy");
            TimeSpan timeToday = TimeSpan.FromSeconds(MaintenanceService.ActiveOperatingSeconds);
            lblOperatingTimeTodayVal.Text = timeToday.ToString(@"hh\:mm\:ss");
        }

        // Gives the card a real header row - title left, actions right, one baseline - instead of
        // dropping the buttons into a designer cell whose 50px absolute height clipped them.
        private void BuildTickListEditButtons()
        {
            _btnEditTicks = BuildCardButton("✏️  Edit", Color.FromArgb(250, 204, 21), Color.FromArgb(30, 41, 59));
            _btnSaveTicks = BuildCardButton("💾  Save", Color.FromArgb(250, 204, 21), Color.FromArgb(30, 41, 59));
            _btnCancelTicks = BuildCardButton("Cancel", Color.FromArgb(241, 245, 249), Color.FromArgb(71, 85, 105));

            _btnEditTicks.Click += (s, e) =>
            {
                if (HasSettingsEditPermission()) SetTickListsEditable(true);
            };
            _btnCancelTicks.Click += (s, e) => { ReloadTickLists(); SetTickListsEditable(false); };
            _btnSaveTicks.Click += BtnSaveTicks_Click;

            var actions = new FlowLayoutPanel
            {
                Anchor = AnchorStyles.Right,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.White,
                Margin = new Padding(0)
            };
            actions.Controls.Add(_btnCancelTicks);
            actions.Controls.Add(_btnSaveTicks);
            actions.Controls.Add(_btnEditTicks);

            var header = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.White,
                Padding = new Padding(15, 14, 24, 10),
                Margin = new Padding(0)
            };
            header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            header.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            header.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // The title moves out of the grid so it shares a row with the actions; its old row is
            // collapsed rather than left behind as a 50px gap.
            tblTestCard.Controls.Remove(lblTestTitle);
            lblTestTitle.Dock = DockStyle.None;
            lblTestTitle.Anchor = AnchorStyles.Left;
            lblTestTitle.AutoSize = true;
            lblTestTitle.Margin = new Padding(0);
            tblTestCard.RowStyles[0] = new RowStyle(SizeType.Absolute, 0F);

            header.Controls.Add(lblTestTitle, 0, 0);
            header.Controls.Add(actions, 1, 0);

            cardTestSettings.Controls.Add(header);
            header.SendToBack();
        }

        // AutoSize rather than a fixed width: ScaleExplicitFonts grows the caption at Load and a
        // hardcoded button would clip it, which is what hid these buttons in the first place.
        private static Button BuildCardButton(string text, Color back, Color fore)
        {
            var button = new Button
            {
                Text = text,
                Font = UIStyleHelper.Font9Bold,
                ForeColor = fore,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(16, 7, 16, 7),
                Margin = new Padding(8, 0, 0, 0)
            };
            UIStyleHelper.SetupButtonRoundedEdges(button, 8, back, ControlPaint.Light(back, 0.3f), ControlPaint.Dark(back, 0.05f), Color.Transparent);
            return button;
        }

        private TextBox[] DefaultValueBoxes => new[]
        {
            txtTotalTestPoint, txtDefMaxWireRes, txtDefWireInsRes, txtDefResistorVal,
            txtDefResistorTol, txtDefCapacitorVal, txtDefCapacitorTol
        };

        private void SetTickListsEditable(bool editable)
        {
            foreach (var (_, box) in _typeBoxes.Concat(_learnBoxes).Concat(_resultBoxes))
            {
                box.Enabled = editable;
            }

            // ReadOnly rather than Enabled=false, so the values stay legible while locked.
            foreach (var box in DefaultValueBoxes)
            {
                box.ReadOnly = !editable;
                box.BackColor = editable ? Color.White : Color.FromArgb(241, 245, 249);
            }

            cmbDefTestType.Enabled = editable;

            if (_btnEditTicks is not null) _btnEditTicks.Visible = !editable;
            if (_btnSaveTicks is not null) _btnSaveTicks.Visible = editable;
            if (_btnCancelTicks is not null) _btnCancelTicks.Visible = editable;
        }

        private void ReloadTickLists()
        {
            PopulateTestTypes();
            PopulateLearnConnections();
            PopulateTestResultColumns();
            LoadDefaultTestSettings();

            float scale = AssemblyLineTester.Helpers.UIStyleHelper.GetHostScaleFactor(this);
            AssemblyLineTester.Helpers.UIStyleHelper.ScaleExplicitFonts(flpTestTypeAvail, scale);
            AssemblyLineTester.Helpers.UIStyleHelper.ScaleExplicitFonts(flpLearnConn, scale);
            LayoutTickLists();
        }

        // Writes all three tick lists, then pushes them into the pages already built from them.
        private static bool HasSettingsEditPermission()
        {
            if (SessionManager.HasPermission("Action.Settings.Edit")) return true;
            MessageBox.Show("You do not have permission to change the default test settings.",
                "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        private void BtnSaveTicks_Click(object? sender, EventArgs e)
        {
            if (!HasSettingsEditPermission()) return;

            var defaults = ReadDefaultsFromCard();

            // Checked before anything is written: the tick lists save first, so rejecting the
            // defaults afterwards would leave the card half-saved.
            if (defaults.TotalTestPointsAvailable % 2 != 0)
            {
                MessageBox.Show(
                    $"Total Test Point Available must be an even number.\r\n\r\n" +
                    $"{defaults.TotalTestPointsAvailable} is odd. Test points are wired in pairs, " +
                    $"so one point would be left with nothing to test against.",
                    "Default Test Settings", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtTotalTestPoint.Focus();
                txtTotalTestPoint.SelectAll();
                return;
            }

            bool allSaved = true;

            allSaved &= TestSettingService.SaveTestTypeChecks(_typeBoxes.Select(t => (t.Id, t.Box.Checked)));
            allSaved &= TestSettingService.SaveLearnConnectionChecks(_learnBoxes.Select(t => (t.Id, t.Box.Checked)));
            allSaved &= TestSettingService.SaveTestResultChecks(_resultBoxes.Select(t => (t.Id, t.Box.Checked)));
            allSaved &= TestSettingService.SaveDefaults(defaults);

            if (!allSaved)
            {
                MessageBox.Show("Some settings could not be saved. Check the log and try again.",
                    "Default Test Settings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Reloaded before the dropdown is read back: unticking a type must drop it from the
            // default list too, and that can change which type is selected.
            PopulateDefaultTestType();

            Application.OpenForms.OfType<MainForm>().FirstOrDefault()?.ApplyTestSettings();
            SetTickListsEditable(false);

            MessageBox.Show("Default test settings saved.", "Default Test Settings",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Reads the card back into an entity; blank or half-typed numbers count as zero rather
        // than blocking the save on a field the operator never filled in.
        private DefaultTestSetting ReadDefaultsFromCard()
        {
            static double Number(TextBox box) =>
                double.TryParse(box.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double value) ? value : 0;

            string selectedType = cmbDefTestType.SelectedItem?.ToString() ?? "";
            int typeId = TestSettingService.GetTestTypes()
                .FirstOrDefault(t => t.TestTypeName == selectedType)?.Id ?? 0;

            return new DefaultTestSetting
            {
                TotalTestPointsAvailable = (int)Number(txtTotalTestPoint),
                DefaultTestTypeId = typeId,
                DefaultMaxWireResistance = Number(txtDefMaxWireRes),
                DefaultWireInstallationResistance = Number(txtDefWireInsRes),
                DefaultResistorValue = Number(txtDefResistorVal),
                DefaultResistorTolerance = (int)Number(txtDefResistorTol),
                DefaultCapacitorValue = Number(txtDefCapacitorVal),
                DefaultCapacitorTolerance = (int)Number(txtDefCapacitorTol)
            };
        }

        private void NumericTextBox_KeyPress(object? sender, KeyPressEventArgs e)
        {
            // Allow control characters (backspace, etc.) and digits.
            // Also allow a single decimal point if not already present.
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && sender is TextBox txt && txt.Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void PopulateTestTypes()
        {
            flpTestTypeAvail.SuspendLayout();
            try
            {
                UIStyleHelper.ClearAndDispose(flpTestTypeAvail.Controls);
                _typeBoxes.Clear();
                var testTypeChecks = _preloadData?.TestTypeChecks ?? TestSettingService.GetTestTypeChecks();
                foreach (var check in testTypeChecks)
                {
                    var box = BuildCheck(check.DisplayName, check.IsChecked);
                    flpTestTypeAvail.Controls.Add(box);
                    _typeBoxes.Add((check.Id, box));
                }
            }
            finally
            {
                flpTestTypeAvail.ResumeLayout(false);
            }

            PopulateDefaultTestType();
        }

        // Only ticked types are offered: defaulting to a type that Test Type Available has switched
        // off would put a value in the Test Parameter grid that its own dropdown cannot hold.
        private void PopulateDefaultTestType()
        {
            var enabled = TestSettingService.GetEnabledTestTypeNames();
            if (enabled.Count == 0) 
            {
                var testTypes = _preloadData?.TestTypes ?? TestSettingService.GetTestTypes();
                enabled = testTypes.Select(t => t.TestTypeName).ToList();
            }

            cmbDefTestType.Items.Clear();
            cmbDefTestType.Items.AddRange(enabled.Cast<object>().ToArray());

            string stored = TestSettingService.GetDefaultTestTypeName();
            int index = cmbDefTestType.Items.IndexOf(stored);
            if (cmbDefTestType.Items.Count > 0) cmbDefTestType.SelectedIndex = index >= 0 ? index : 0;
        }

        private void LoadDefaultTestSettings()
        {
            var defaults = TestSettingService.GetDefaults();

            txtTotalTestPoint.Text = defaults.TotalTestPointsAvailable.ToString(CultureInfo.InvariantCulture);
            txtDefMaxWireRes.Text = defaults.DefaultMaxWireResistance.ToString(CultureInfo.InvariantCulture);
            txtDefWireInsRes.Text = defaults.DefaultWireInstallationResistance.ToString(CultureInfo.InvariantCulture);
            txtDefResistorVal.Text = defaults.DefaultResistorValue.ToString(CultureInfo.InvariantCulture);
            txtDefResistorTol.Text = defaults.DefaultResistorTolerance.ToString(CultureInfo.InvariantCulture);
            txtDefCapacitorVal.Text = defaults.DefaultCapacitorValue.ToString(CultureInfo.InvariantCulture);
            txtDefCapacitorTol.Text = defaults.DefaultCapacitorTolerance.ToString(CultureInfo.InvariantCulture);
        }

        private void PopulateLearnConnections()
        {
            flpLearnConn.SuspendLayout();
            try
            {
                UIStyleHelper.ClearAndDispose(flpLearnConn.Controls);
                _learnBoxes.Clear();
                var connections = _preloadData?.LearnConnections ?? TestSettingService.GetLearnConnections();
                foreach (var conn in connections)
                {
                    var box = BuildCheck(conn.DisplayName, conn.IsChecked);
                    flpLearnConn.Controls.Add(box);
                    _learnBoxes.Add((conn.Id, box));
                }
            }
            finally
            {
                flpLearnConn.ResumeLayout(false);
            }
        }

        // The twelve boxes stay in the designer for their layout; only their state comes from the table.
        private void PopulateTestResultColumns()
        {
            var checks = _preloadData?.TestResultChecks ?? TestSettingService.GetTestResultChecks();
            _resultBoxes.Clear();
            flpTestResultCol.SuspendLayout();

            foreach (Control control in flpTestResultCol.Controls)
            {
                if (control is not CheckBox box) continue;

                var check = checks.FirstOrDefault(c => c.TestResultName == box.Text);
                if (check == null)
                {
                    box.Enabled = false;
                    continue;
                }

                box.Checked = check.IsChecked;
                _resultBoxes.Add((check.Id, box));
            }
            flpTestResultCol.ResumeLayout(false);
        }

        private static CheckBox BuildCheck(string text, bool isChecked)
        {
            return new CheckBox
            {
                Text = text,
                Checked = isChecked,
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(30, 41, 59),
                Margin = new Padding(0, 5, 0, 5),
                UseVisualStyleBackColor = true
            };
        }
    }
}
