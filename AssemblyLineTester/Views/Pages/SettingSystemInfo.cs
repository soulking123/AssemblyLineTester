using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AssemblyLineTester.Helpers;

namespace AssemblyLineTester.Views.Pages
{
    public partial class SettingSystemInfo : UserControl
    {
        public SettingSystemInfo()
        {
            InitializeComponent();
            SetupEvents();
            if (cmbTimeZone.Items.Count > 0) cmbTimeZone.SelectedIndex = 0;
        }

        public void SetupEvents()
        {
            UIStyleHelper.SetupCardRoundedEdges(cardSystemStatus, 12, Color.FromArgb(34, 40, 49), 2.5f);
            UIStyleHelper.SetupCardRoundedEdges(cardLicenseInfo, 12, Color.FromArgb(34, 40, 49), 2.5f);
            UIStyleHelper.SetupCardRoundedEdges(cardSystemTime, 12, Color.FromArgb(34, 40, 49), 2.5f);
            UIStyleHelper.SetupButtonRoundedEdges(btnManageLicense, 6, Color.FromArgb(241, 245, 249), Color.FromArgb(226, 232, 240), Color.FromArgb(203, 213, 225), Color.Transparent);
            UIStyleHelper.SetupButtonRoundedEdges(btnSyncTime, 6, Color.FromArgb(241, 245, 249), Color.FromArgb(226, 232, 240), Color.FromArgb(203, 213, 225), Color.Transparent);
        }
    }
}
