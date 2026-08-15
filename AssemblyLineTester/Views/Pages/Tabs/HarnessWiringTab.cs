using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using AssemblyLineTester.Services;

namespace AssemblyLineTester.Views.Pages.Tabs
{
    public partial class HarnessWiringTab : UserControl
    {
        private HashSet<int> highlightedRows = new HashSet<int>();

        public HarnessWiringTab()
        {
            InitializeComponent();

            picDiagram.DiagramRowClicked += PicDiagram_DiagramRowClicked;
            gridWiring.SelectionChanged += GridWiring_SelectionChanged;
            gridWiring.CellValueChanged += GridWiring_CellValueChanged;
            gridWiring.RowsAdded += GridWiring_RowsChanged;
            gridWiring.RowsRemoved += GridWiring_RowsChanged;
            gridWiring.DataError += GridWiring_DataError;
            
            // Add valid items to the ComboBox column before adding rows
            colTestType.Items.Add("Continuity");
            colTestType.Items.Add("Resistance");
            colTestType.Items.Add("Hipot");
            colTestType.Items.Add("IR");

            // Add some dummy data to preview
            gridWiring.Rows.Add("NEW_NET", "1", "2", "Continuity", "Pass");
            gridWiring.Rows.Add("NET_2", "3", "4", "Resistance", "0.5");
        }

        private void GridWiring_DataError(object? sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void GridWiring_SelectionChanged(object? sender, EventArgs e)
        {
            highlightedRows.Clear();
            foreach (DataGridViewRow row in gridWiring.SelectedRows)
            {
                if (!row.IsNewRow)
                    highlightedRows.Add(row.Index);
            }
            UpdateDiagram();
        }

        private void GridWiring_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            UpdateDiagram();
        }

        private void GridWiring_RowsChanged(object? sender, DataGridViewRowsAddedEventArgs e)
        {
            UpdateDiagram();
        }

        private void GridWiring_RowsChanged(object? sender, DataGridViewRowsRemovedEventArgs e)
        {
            UpdateDiagram();
        }

        private void PicDiagram_DiagramRowClicked(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < gridWiring.Rows.Count && !gridWiring.Rows[rowIndex].IsNewRow)
            {
                gridWiring.ClearSelection();
                gridWiring.Rows[rowIndex].Selected = true;
                gridWiring.FirstDisplayedScrollingRowIndex = rowIndex;
            }
        }

        private void UpdateDiagram()
        {
            var nets = new List<AssemblyLineTester.Views.Controls.DiagramNetDefinition>();

            for (int i = 0; i < gridWiring.Rows.Count; i++)
            {
                DataGridViewRow row = gridWiring.Rows[i];
                if (row.IsNewRow) continue;

                string fromAddr = row.Cells["colFromAddr"].Value?.ToString() ?? "0";
                string toAddr = row.Cells["colToAddr"].Value?.ToString() ?? "0";

                nets.Add(new AssemblyLineTester.Views.Controls.DiagramNetDefinition
                {
                    RowIndex = i,
                    FromAddress = fromAddr,
                    FromLabel = fromAddr,
                    ToAddress = toAddr,
                    ToLabel = toAddr,
                    IsError = false
                });
            }

            picDiagram.RenderDiagram(nets, TestSettingService.GetDefaults().TotalTestPointsAvailable);
            picDiagram.SetHighlightedRows(highlightedRows);
        }


    }
}
