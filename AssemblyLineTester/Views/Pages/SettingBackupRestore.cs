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
        public SettingBackupRestore()
        {
            InitializeComponent();
            
            // Set default value for combo box if needed
            if (cboAutoRecap.Items.Count > 0)
            {
                cboAutoRecap.SelectedIndex = 0; // Default to "Everyday"
            }

            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardBackupRestore, 12, Color.FromArgb(34, 40, 49), 2.5f);
        }

        private void btnResetDefault_Click(object sender, EventArgs e)
        {
            // Reset to defaults
            if (cboAutoRecap.Items.Count > 0)
            {
                cboAutoRecap.SelectedIndex = 0;
            }
            txtManualRecapPath.Text = string.Empty;
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

        private void btnRecap_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtManualRecapPath.Text))
            {
                MessageBox.Show("Please select a folder for the manual recap first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // TODO: Implement actual manual recap / backup logic here
            MessageBox.Show($"Manual Recap will be saved to: {txtManualRecapPath.Text}", "Manual Recap", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBrowseRollback_Click(object sender, EventArgs e)
        {
            // Rollback selects a File
            using (var ofd = new OpenFileDialog())
            {
                ofd.Title = "Select a Backup File for Rollback";
                ofd.Filter = "Backup Files (*.bak;*.zip)|*.bak;*.zip|All Files (*.*)|*.*";
                
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtRollbackPath.Text = string.Empty; // Temporary clear
                    txtRollbackPath.Text = ofd.FileName;
                }
            }
        }

        private void btnRollback_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRollbackPath.Text))
            {
                MessageBox.Show("Please select a file to rollback to.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // TODO: Implement actual rollback logic here
            MessageBox.Show($"System will rollback using file: {txtRollbackPath.Text}", "Rollback", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
