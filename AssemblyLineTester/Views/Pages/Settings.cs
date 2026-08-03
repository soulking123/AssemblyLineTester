using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Windows.Forms;
using AssemblyLineTester.Helpers;
using AssemblyLineTester.Services;

namespace AssemblyLineTester.Views.Pages
{
    public partial class Settings : UserControl
    {
        private TableLayoutPanel? tblUcContainer;
        private Label? lblUnderConstructionTitle;
        private SettingsUsersAndRolesPage? _usersAndRolesPage;
        private TableLayoutPanel? _backupPage;

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
        private System.Windows.Forms.Timer? _clock;

        private static readonly DateTime _processStart = Process.GetCurrentProcess().StartTime;
        private static readonly Color Ok = Color.FromArgb(22, 163, 74);
        private static readonly Color Bad = Color.FromArgb(220, 38, 38);

        public Settings()
        {
            InitializeComponent();
            SetupInteractiveTabs();
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
            LoadLicenseInfo();
            LoadSystemStatus();
            LoadFileSettings();
            StartClock();
        }

        // These four came from the mockup. Only the folders and the two retention windows exist in code,
        // so they are shown read-only rather than pretending an edit here changes anything.
        private void LoadFileSettings()
        {
            txtDataPath.Text = Data.AppDbContext.DataFolder;
            txtReportPath.Text = AppInfo.LogDirectory;
            txtLogRet.Text = SystemLogService.RetentionDays.ToString();
            txtMaxSize.Text = FileLogService.RetentionDays.ToString();

            lblReportStorage.Text = "Log Storage Path";
            lblLogRet.Text = "System Log Retention (Days)";
            lblMaxSize.Text = "File Log Retention (Days)";

            foreach (TextBox box in new[] { txtDataPath, txtReportPath, txtLogRet, txtMaxSize })
            {
                box.ReadOnly = true;
                box.BackColor = Color.FromArgb(241, 245, 249);
            }

            chkAutoDelete.Checked = true;
            chkAutoDelete.Enabled = false;
            chkAutoDelete.Text = "Auto delete logs older than retention (runs at startup)";
        }

        // Ticks the two values that go stale while the page is open: wall clock and uptime.
        private void StartClock()
        {
            UpdateClock();
            _clock = new System.Windows.Forms.Timer { Interval = 1000 };
            _clock.Tick += (s, e) => UpdateClock();
            _clock.Start();
            Disposed += (s, e) => _clock?.Dispose();
        }

        private void UpdateClock()
        {
            DateTime now = DateTime.Now;
            lblTimeValDate.Text = now.ToString("dd MMM yyyy");
            lblTimeValTime.Text = now.ToString("hh:mm:ss tt");

            TimeSpan up = now - _processStart;
            lblStValUptime.Text = $"{(int)up.TotalHours:D2}:{up.Minutes:D2}:{up.Seconds:D2}";
        }

        // Fills the status card from the process, the database file and the data folder.
        private void LoadSystemStatus()
        {
            lblStValApp.Text = $"Running {AppInfo.Version}";
            lblStValApp.ForeColor = Ok;

            var (dbText, dbOk) = DescribeDatabase();
            lblStValDb.Text = dbText;
            lblStValDb.ForeColor = dbOk ? Ok : Bad;

            var (fileText, fileOk) = DescribeFileStorage();
            lblStValFile.Text = fileText;
            lblStValFile.ForeColor = fileOk ? Ok : Bad;

            lblStValStart.Text = _processStart.ToString("dd MMM yyyy hh:mm:ss tt");

            SelectCurrentTimeZone();
        }

        private static (string Text, bool Ok) DescribeDatabase()
        {
            try
            {
                using var db = new Data.AppDbContext();
                if (!db.Database.CanConnect()) return ("Unreachable", false);

                var size = new FileInfo(Data.AppDbContext.DbPath).Length / (1024d * 1024d);
                return ($"Connected ({size:0.0} MB)", true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Settings.DescribeDatabase failed: {ex}");
                return ("Unreachable", false);
            }
        }

        // Free space matters here: the log writer and the report exporter both fail silently when the disk fills.
        private static (string Text, bool Ok) DescribeFileStorage()
        {
            try
            {
                if (!Directory.Exists(AppInfo.DataDirectory)) return ("Missing", false);

                var drive = new DriveInfo(Path.GetPathRoot(AppInfo.DataDirectory)!);
                double freeGb = drive.AvailableFreeSpace / (1024d * 1024d * 1024d);
                return freeGb < 1
                    ? ($"Low space ({freeGb:0.0} GB free)", false)
                    : ($"Healthy ({freeGb:0.0} GB free)", true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Settings.DescribeFileStorage failed: {ex}");
                return ("Unknown", false);
            }
        }

        // The list is a fixed set of offsets, so match on offset rather than on the zone's name.
        private void SelectCurrentTimeZone()
        {
            TimeSpan offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.Now);
            string tag = $"(UTC{(offset < TimeSpan.Zero ? "-" : "+")}{Math.Abs(offset.Hours):D2}:{Math.Abs(offset.Minutes):D2})";

            for (int i = 0; i < cmbTimeZone.Items.Count; i++)
            {
                if (cmbTimeZone.Items[i]?.ToString()?.StartsWith(tag, StringComparison.Ordinal) == true)
                {
                    cmbTimeZone.SelectedIndex = i;
                    return;
                }
            }

            cmbTimeZone.Items.Insert(0, $"{tag} {TimeZoneInfo.Local.Id}");
            cmbTimeZone.SelectedIndex = 0;
        }

        // Fills the licence card and the status row from the signed licence on disk.
        private void LoadLicenseInfo()
        {
            var (result, payload) = LicenseService.Validate();

            bool valid = result == LicenseValidationResult.Valid;
            lblStValLic.Text = DescribeLicenseStatus(result);
            lblStValLic.ForeColor = valid ? Color.FromArgb(22, 163, 74) : Color.FromArgb(220, 38, 38);

            lblLicValKey.Text = AppInfo.MachineIds.Count > 0
                ? AppInfo.FormatMachineId(AppInfo.PrimaryMachineId)
                : "No physical adapter found";

            if (payload == null)
            {
                lblLicValType.Text = "-";
                lblLicValFrom.Text = "-";
                lblLicValTo.Text = "-";
                lblLicValToOrg.Text = "-";
                lblLicValType.ForeColor = Color.FromArgb(220, 38, 38);
                return;
            }

            lblLicValType.Text = payload.ExpiryUtc.HasValue ? "Time-limited" : "Perpetual";
            lblLicValType.ForeColor = valid ? Color.FromArgb(16, 185, 129) : Color.FromArgb(220, 38, 38);
            lblLicValFrom.Text = payload.IssuedUtc.ToLocalTime().ToString("dd MMM yyyy");
            lblLicValTo.Text = payload.ExpiryUtc?.ToLocalTime().ToString("dd MMM yyyy") ?? "No expiry";
            lblLicValToOrg.Text = string.IsNullOrWhiteSpace(payload.Customer) ? "-" : payload.Customer;
        }

        private static string DescribeLicenseStatus(LicenseValidationResult result) => result switch
        {
            LicenseValidationResult.Valid => "Valid",
            LicenseValidationResult.MissingFile => "Not installed",
            LicenseValidationResult.InvalidSignature => "Invalid signature",
            LicenseValidationResult.Expired => "Expired",
            LicenseValidationResult.WrongMachine => "Wrong machine",
            LicenseValidationResult.ClockRolledBack => "Clock rolled back",
            LicenseValidationResult.CorruptState => "State unreadable",
            _ => "Unknown"
        };

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
                isLoaded = true;
            }
        }


        private void SetupInteractiveTabs()
        {
            Label[] tabLabels = [lblTabGen, lblTabTest, lblTabHw, lblTabUsers, lblTabNotif, lblTabBackup, lblTabSysInfo];

            activeTabLabel = lblTabGen;

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
                    if (_backupPage != null) _backupPage.Visible = false;
                    if (tblUcContainer != null) tblUcContainer.Visible = false;
                }
                else if (clickedLabel == lblTabUsers)
                {
                    tblContent.Visible = false;
                    if (tblUcContainer != null) tblUcContainer.Visible = false;
                    if (_backupPage != null) _backupPage.Visible = false;
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
                }
                else if (clickedLabel == lblTabBackup)
                {
                    tblContent.Visible = false;
                    if (tblUcContainer != null) tblUcContainer.Visible = false;
                    if (_usersAndRolesPage != null) _usersAndRolesPage.Visible = false;
                    if (_backupPage == null)
                    {
                        _backupPage = BuildBackupPage();
                        pnlPageContainer.Controls.Add(_backupPage);
                    }
                    _backupPage.Visible = true;
                    _backupPage.BringToFront();
                }
                else
                {
                    tblContent.Visible = false;
                    if (_usersAndRolesPage != null) _usersAndRolesPage.Visible = false;
                    if (_backupPage != null) _backupPage.Visible = false;
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

        // Backup & Restore tab, built in code because it is only two buttons.
        private TableLayoutPanel BuildBackupPage()
        {
            var container = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 3,
                BackColor = Color.FromArgb(244, 245, 247)
            };
            container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            container.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 760F));
            container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            container.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            container.RowStyles.Add(new RowStyle(SizeType.Absolute, 330F));
            container.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var card = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(30) };
            Helpers.UIStyleHelper.SetupCardRoundedEdges(card, 12, Color.FromArgb(34, 40, 49), 2.5f);

            var lblTitle = new Label
            {
                Text = "Backup & Restore",
                Font = Helpers.UIStyleHelper.Font18Bold,
                ForeColor = Color.FromArgb(30, 41, 59),
                AutoSize = true,
                Location = new Point(30, 26)
            };

            var lblBackupHint = new Label
            {
                Text = "Saves a complete copy of the database to a file you choose. Safe to run while the\n" +
                       "application is in use.",
                Font = Helpers.UIStyleHelper.Font11Regular,
                ForeColor = Color.FromArgb(100, 116, 139),
                AutoSize = true,
                Location = new Point(30, 84)
            };

            var btnBackup = new Button
            {
                Text = "Create Backup...",
                Font = Helpers.UIStyleHelper.Font11Bold,
                ForeColor = Color.FromArgb(30, 41, 59),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Size = new Size(240, 48),
                Location = new Point(30, 140)
            };
            Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnBackup, 10,
                Color.FromArgb(250, 204, 21), Color.FromArgb(254, 216, 53), Color.FromArgb(234, 186, 10), Color.Transparent);
            btnBackup.Click += BtnBackup_Click;

            var lblRestoreHint = new Label
            {
                Text = "Replaces everything currently in the database with the contents of a backup file.\n" +
                       "The current database is copied aside first, and the application restarts afterwards.",
                Font = Helpers.UIStyleHelper.Font11Regular,
                ForeColor = Color.FromArgb(100, 116, 139),
                AutoSize = true,
                Location = new Point(30, 210)
            };

            var btnRestore = new Button
            {
                Text = "Restore From Backup...",
                Font = Helpers.UIStyleHelper.Font11Bold,
                ForeColor = Color.FromArgb(30, 41, 59),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Size = new Size(240, 48),
                Location = new Point(30, 266)
            };
            Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnRestore, 10,
                Color.FromArgb(241, 245, 249), Color.FromArgb(226, 232, 240), Color.FromArgb(203, 213, 225), Color.Transparent);
            btnRestore.Click += BtnRestore_Click;

            card.Controls.AddRange([lblTitle, lblBackupHint, btnBackup, lblRestoreHint, btnRestore]);
            container.Controls.Add(card, 1, 1);
            return container;
        }

        private static bool HasBackupPermission()
        {
            if (Services.SessionManager.HasPermission("Action.Database.Backup")) return true;
            MessageBox.Show("You do not have permission to back up or restore the database.",
                "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        private void BtnBackup_Click(object? sender, EventArgs e)
        {
            if (!HasBackupPermission()) return;

            using var dialog = new SaveFileDialog
            {
                Filter = "Database backup (*.db)|*.db",
                FileName = $"assemblylinetester-backup-{DateTime.Now:yyyyMMdd-HHmmss}.db"
            };
            if (dialog.ShowDialog() != DialogResult.OK) return;

            Cursor previous = Cursor;
            Cursor = Cursors.WaitCursor;
            try
            {
                Services.DatabaseBackupService.Backup(dialog.FileName);
                MessageBox.Show($"Backup saved to:\r\n{dialog.FileName}", "Backup complete",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Services.FileLogService.Error("Settings.BtnBackup_Click", "Database backup failed.", ex);
                MessageBox.Show($"Backup failed: {ex.Message}", "Backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = previous;
            }
        }

        private void BtnRestore_Click(object? sender, EventArgs e)
        {
            if (!HasBackupPermission()) return;

            using var dialog = new OpenFileDialog
            {
                Filter = "Database backup (*.db)|*.db",
                CheckFileExists = true
            };
            if (dialog.ShowDialog() != DialogResult.OK) return;

            // Asked after the pick, so the operator sees which file is about to overwrite theirs.
            if (MessageBox.Show(
                    $"This replaces all current data - users, harnesses and test reports - with the contents of:\r\n\r\n" +
                    $"{dialog.FileName}\r\n\r\nThe current database is saved aside as a pre-restore file first, and the " +
                    $"application will restart. Continue?",
                    "Restore database",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }

            Cursor previous = Cursor;
            Cursor = Cursors.WaitCursor;
            try
            {
                Services.DatabaseBackupService.Restore(dialog.FileName);
            }
            catch (Exception ex)
            {
                Services.FileLogService.Error("Settings.BtnRestore_Click", "Database restore failed.", ex);
                MessageBox.Show($"Restore failed: {ex.Message}", "Restore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                Cursor = previous;
            }

            // Everything on screen now describes a database that is gone; only a restart is honest.
            MessageBox.Show("Restore complete. The application will now restart so you can sign in again.",
                "Restore", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Restart();
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

            btnSyncTime.Click += (s, e) => { UpdateClock(); SelectCurrentTimeZone(); };
            btnManageLicense.Click += (s, e) => { LoadLicenseInfo(); LoadSystemStatus(); };
        }
    }
}
