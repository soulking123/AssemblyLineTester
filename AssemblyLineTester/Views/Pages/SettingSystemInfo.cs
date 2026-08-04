using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using AssemblyLineTester.Helpers;
using AssemblyLineTester.Services;

namespace AssemblyLineTester.Views.Pages
{
    public partial class SettingSystemInfo : UserControl
    {
        private System.Windows.Forms.Timer? _clock;

        private static readonly DateTime _processStart = Process.GetCurrentProcess().StartTime;
        private static readonly Color Ok = Color.FromArgb(22, 163, 74);
        private static readonly Color Bad = Color.FromArgb(220, 38, 38);

        public SettingSystemInfo()
        {
            InitializeComponent();
            SetupEvents();
            LoadLicenseInfo();
            LoadSystemStatus();
            StartClock();
        }

        public void SetupEvents()
        {
            UIStyleHelper.SetupCardRoundedEdges(cardSystemStatus, 12, Color.FromArgb(34, 40, 49), 2.5f);
            UIStyleHelper.SetupCardRoundedEdges(cardLicenseInfo, 12, Color.FromArgb(34, 40, 49), 2.5f);
            UIStyleHelper.SetupCardRoundedEdges(cardSystemTime, 12, Color.FromArgb(34, 40, 49), 2.5f);
            UIStyleHelper.SetupButtonRoundedEdges(btnManageLicense, 6, Color.FromArgb(241, 245, 249), Color.FromArgb(226, 232, 240), Color.FromArgb(203, 213, 225), Color.Transparent);
            UIStyleHelper.SetupButtonRoundedEdges(btnSyncTime, 6, Color.FromArgb(241, 245, 249), Color.FromArgb(226, 232, 240), Color.FromArgb(203, 213, 225), Color.Transparent);

            btnSyncTime.Click += (s, e) => { UpdateClock(); SelectCurrentTimeZone(); };
            btnManageLicense.Click += (s, e) => { LoadLicenseInfo(); LoadSystemStatus(); };
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
                Debug.WriteLine($"SettingSystemInfo.DescribeDatabase failed: {ex}");
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
                Debug.WriteLine($"SettingSystemInfo.DescribeFileStorage failed: {ex}");
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
    }
}
