using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AssemblyLineTester.Views
{
    public partial class SettingBackupRestore : UserControl
    {
        // Dropdown order must match SettingBackupRestore.Designer: Everyday, 7 days, 2 weeks, 1 month.
        private static readonly int[] IntervalHours = { 24, 168, 336, 720 };

        public SettingBackupRestore()
        {
            InitializeComponent();

            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardBackupRestore, 12, Color.FromArgb(34, 40, 49), 2.5f);

            ShowStoragePaths();
            LoadInterval();
            cboAutoRecap.SelectedIndexChanged += CboAutoRecap_SelectedIndexChanged;
        }

        // The operator cannot copy a backup out, or hand the database to support, without knowing
        // where either of them actually lives.
        private void ShowStoragePaths()
        {
            txtDatabasePath.Text = AssemblyLineTester.Services.DatabaseBackupService.DbPath;
            txtDatabasePath.ReadOnly = true;
            txtDatabasePath.BackColor = Color.FromArgb(241, 245, 249);

            txtManualRecapPath.Text = AssemblyLineTester.Services.DatabaseBackupService.AutoBackupFolder;
        }

        private void LoadInterval()
        {
            int stored = AssemblyLineTester.Services.DatabaseBackupService.AutoBackupIntervalHours;
            int index = Array.IndexOf(IntervalHours, stored);
            cboAutoRecap.SelectedIndex = index >= 0 ? index : 0;
        }

        private void CboAutoRecap_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cboAutoRecap.SelectedIndex < 0 || cboAutoRecap.SelectedIndex >= IntervalHours.Length) return;

            try
            {
                AssemblyLineTester.Services.DatabaseBackupService.AutoBackupIntervalHours = IntervalHours[cboAutoRecap.SelectedIndex];
            }
            catch (Exception ex)
            {
                AssemblyLineTester.Services.FileLogService.Error("SettingBackupRestore.CboAutoRecap_SelectedIndexChanged",
                    "Could not save the backup interval.", ex);
                MessageBox.Show($"Could not save the schedule: {ex.Message}", "Auto Recap",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnResetDefault_Click(object sender, EventArgs e)
        {
            cboAutoRecap.SelectedIndex = 0;
            ShowStoragePaths();
            txtRollbackPath.Text = string.Empty;
        }

        private void btnBrowseRecap_Click(object sender, EventArgs e)
        {
            // Manual Recap selects a Folder
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Select a folder for Manual Recap Backup";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtManualRecapPath.Text = fbd.SelectedPath;
                }
            }
        }

        private static bool HasBackupPermission()
        {
            if (AssemblyLineTester.Services.SessionManager.HasPermission("Action.Database.Backup")) return true;
            MessageBox.Show("You do not have permission to back up or restore the database.",
                "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        private void btnRecap_Click(object sender, EventArgs e)
        {
            if (!HasBackupPermission()) return;

            if (string.IsNullOrWhiteSpace(txtManualRecapPath.Text))
            {
                MessageBox.Show("Please select a folder for the manual recap first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string destination = System.IO.Path.Combine(
                txtManualRecapPath.Text,
                $"manual-backup-{DateTime.Now:yyyyMMdd-HHmmss}.db");

            Cursor previous = Cursor;
            Cursor = Cursors.WaitCursor;
            try
            {
                AssemblyLineTester.Services.DatabaseBackupService.Backup(destination);
                MessageBox.Show($"Backup saved to:\r\n{destination}", "Manual Recap",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                AssemblyLineTester.Services.FileLogService.Error("SettingBackupRestore.btnRecap_Click", "Database backup failed.", ex);
                MessageBox.Show($"Backup failed: {ex.Message}", "Manual Recap", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = previous;
            }
        }

        private void btnBrowseRollback_Click(object sender, EventArgs e)
        {
            // Rollback selects a File
            using (var ofd = new OpenFileDialog())
            {
                ofd.Title = "Select a Backup File for Rollback";
                ofd.Filter = "Database backup (*.db)|*.db|All Files (*.*)|*.*";
                ofd.InitialDirectory = AssemblyLineTester.Services.DatabaseBackupService.AutoBackupFolder;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtRollbackPath.Text = string.Empty; // Temporary clear
                    txtRollbackPath.Text = ofd.FileName;
                }
            }
        }

        private void btnRollback_Click(object sender, EventArgs e)
        {
            if (!HasBackupPermission()) return;

            if (string.IsNullOrWhiteSpace(txtRollbackPath.Text))
            {
                MessageBox.Show("Please select a file to rollback to.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Asked with the filename in view, because a restore overwrites everything and a wrong
            // file picked from a folder of similar names cannot be undone.
            if (MessageBox.Show(
                    $"This replaces all current data - users, harnesses and test reports - with the contents of:\r\n\r\n" +
                    $"{txtRollbackPath.Text}\r\n\r\nThe current database is saved aside as a pre-restore file first, and the " +
                    $"application will restart. Continue?",
                    "Rollback",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }

            Cursor previous = Cursor;
            Cursor = Cursors.WaitCursor;
            try
            {
                AssemblyLineTester.Services.DatabaseBackupService.Restore(txtRollbackPath.Text);
            }
            catch (Exception ex)
            {
                AssemblyLineTester.Services.FileLogService.Error("SettingBackupRestore.btnRollback_Click", "Database restore failed.", ex);
                MessageBox.Show($"Rollback failed: {ex.Message}", "Rollback", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                Cursor = previous;
            }

            // Everything on screen now describes a database that is gone; only a restart is honest.
            MessageBox.Show("Rollback complete. The application will now restart so you can sign in again.",
                "Rollback", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Restart();
        }
    }
}
