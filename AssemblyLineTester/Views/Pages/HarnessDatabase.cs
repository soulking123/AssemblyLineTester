using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssemblyLineTester.Views.Pages
{
    public partial class HarnessDatabase : UserControl
    {
        public HarnessDatabase()
        {
            InitializeComponent();
            PopulateDummyData();
        }

        private void PopulateDummyData()
        {
            // Populate DataGridView
            dataGridViewList.Rows.Add("1", "6060", "AC", "Engine Control", "WH-ENG-001", "123.R001", "A", "Active", "14 Mar 2026");
            dataGridViewList.Rows.Add("2", "6060", "AC", "Cabin", "WH-CAB-002", "123.R001", "A", "Active", "14 Mar 2026");
            dataGridViewList.Rows.Add("3", "6060", "AC", "Main Frame", "WH-MF-003", "123.R001", "B", "Active", "13 Mar 2026");
            dataGridViewList.Rows.Add("4", "6060", "AC", "Boom", "WH-BOOM-004", "124.R000", "A", "Active", "13 Mar 2026");
            dataGridViewList.Rows.Add("5", "6060", "AC", "Power Module", "WH-PM-005", "124.R000", "A", "Active", "12 Mar 2026");

            // Populate ComboBoxes
            cmbProductModel.Items.AddRange(["All", "6060"]);
            cmbProductModel.SelectedIndex = 0;
            cmbProductType.Items.AddRange(["All", "AC"]);
            cmbProductType.SelectedIndex = 0;
            cmbArea.Items.AddRange(["All", "Engine Control", "Cabin", "Main Frame", "Boom", "Power Module"]);
            cmbArea.SelectedIndex = 0;
            cmbStatus.Items.AddRange(["All", "Active", "Inactive"]);
            cmbStatus.SelectedIndex = 0;

            // Populate General Info
            lblGenInfo.Text = "Harness Detail - General Information\n\n" +
                              "Product Model: 6060\n" +
                              "Product Type: AC\n" +
                              "Area: Engine Control\n" +
                              "Part Number: WH-ENG-001\n" +
                              "ECN No.: 123.R001\n" +
                              "Revision: A\n" +
                              "Status: Active\n" +
                              "Last Updated: 14 Mar 2026\n" +
                              "\n" +
                              "Description:\n" +
                              "Main engine control harness routing through the firewall.\n" +
                              "Requires high-temperature resistant sleeving.";
        }

    }
}
