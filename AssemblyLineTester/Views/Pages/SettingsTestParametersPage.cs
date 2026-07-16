using AssemblyLineTester.Models;
using AssemblyLineTester.Services;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace AssemblyLineTester.Views.Pages
{
    public partial class SettingsTestParametersPage : UserControl
    {
        private readonly HashSet<int> highlightedRows = [];
        private readonly List<DiagramNode> leftNodes = [];
        private readonly List<DiagramNode> rightNodes = [];
        private readonly List<DiagramConnection> connections = [];
        private readonly TestConfigService _configService = new();

        private class DiagramNode
        {
            public int RowIndex { get; set; }
            public string Address { get; set; } = "";
            public string Label { get; set; } = "";
            public RectangleF NodeRect { get; set; }
            public RectangleF LabelRect { get; set; }
            public bool IsLeft { get; set; }
        }

        private class DiagramConnection
        {
            public int RowIndex { get; set; }
            public PointF StartPoint { get; set; }
            public PointF EndPoint { get; set; }
            public PointF Control1 { get; set; }
            public PointF Control2 { get; set; }
            public DiagramNode LeftNode { get; set; } = null!;
            public DiagramNode RightNode { get; set; } = null!;
        }

        public SettingsTestParametersPage()
        {
            InitializeComponent();
            SetupEvents();
            if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime && !DesignMode)
            {
                LoadDefaultData();
                UpdateTotalNetCount();
                UpdateDiagramSize();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                LoadDefaultData();
                UpdateTotalNetCount();
                UpdateDiagramSize();
                picPreview.Invalidate();
            }
        }

        private void SetupEvents()
        {
            btnAdd.Click += BtnAdd_Click;
            btnRemove.Click += BtnRemove_Click;
            btnReset.Click += BtnReset_Click;
            btnImport.Click += BtnImport_Click;
            btnExport.Click += BtnExport_Click;

            gridConfig.CellValueChanged += GridConfig_CellValueChanged;
            gridConfig.CellValidating += GridConfig_CellValidating;
            gridConfig.RowsRemoved += GridConfig_RowsChanged;
            gridConfig.RowsAdded += GridConfig_RowsChanged;
            gridConfig.CurrentCellDirtyStateChanged += GridConfig_CurrentCellDirtyStateChanged;
            gridConfig.EditingControlShowing += GridConfig_EditingControlShowing;
            gridConfig.DataError += (s, e) => { e.ThrowException = false; };
            gridConfig.CellClick += GridConfig_CellClick;
            gridConfig.Leave += GridConfig_Leave;

            txtConnectionResistance.TextChanged += (s, e) => RecalculateAllRows();

            picPreview.Paint += PicPreview_Paint;
            picPreview.MouseDown += PicPreview_MouseDown;
            picPreview.Resize += (s, e) => picPreview.Invalidate();
            pnlPreviewScroll.Resize += (s, e) => { UpdateDiagramSize(); picPreview.Invalidate(); };
            splitContainerMain.SplitterMoved += (s, e) => { UpdateDiagramSize(); picPreview.Invalidate(); };
        }

        private static string[] GetUnitsForTestType(string testType)
        {
            return testType switch
            {
                "Resistance" => ["Ω", "mΩ", "kΩ", "MΩ", "GΩ"],
                "Capacitance" => ["F", "μF", "nF", "pF"],
                "Voltage" => ["V", "mV", "kV"],
                "Continuity" => ["Ω", "mΩ"],
                "Insulation" => ["MΩ", "GΩ", "kΩ", "Ω"],
                "Short Circuit" => ["MΩ", "GΩ", "kΩ", "Ω"],
                "Functional" => ["V", "mV", "A", "mA", "Ω", "Hz", "ms"],
                _ => ["Ω", "mΩ", "kΩ", "MΩ", "GΩ", "F", "μF", "nF", "pF", "V", "mV", "kV", "A", "mA", "Hz", "ms"]
            };
        }

        private void UpdateRowUnitDropdown(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= gridConfig.Rows.Count) return;
            DataGridViewRow row = gridConfig.Rows[rowIndex];
            string testType = row.Cells["colTestType"].Value?.ToString() ?? "";
            string[] allowedUnits = GetUnitsForTestType(testType);

            if (row.Cells["colUnit"] is DataGridViewComboBoxCell unitCell)
            {
                string currentVal = unitCell.Value?.ToString() ?? "";
                unitCell.Items.Clear();
                unitCell.Items.AddRange(allowedUnits);

                if (!allowedUnits.Contains(currentVal))
                {
                    unitCell.Value = allowedUnits.Length > 0 ? allowedUnits[0] : "";
                }
            }
        }


        private void LoadDefaultData()
        {
            gridConfig.Rows.Clear();
            highlightedRows.Clear();

            string[] defaultRows =
            [
                "1|Elec-Control-001|Low Voltage Supply|0|X1-01|7|Y1-01|Resistance|0|5|2.5|±5%|Ω",
                "2|Elec-Control-001|ECM-Main Connector|1|X2-01|5|Y2-01|Resistance|0|5|2.5|±5%|Ω",
                "3|Elec-Control-001|ECM-Main Connector|2|X2-02|6|Y2-02|Capacitance|0|100|50|±10%|μF",
                "4|Elec-Control-001|Low Voltage Supply|3|X1-02|4|Y1-02|Resistance|0|5|2.5|±5%|Ω"
            ];

            foreach (string rowStr in defaultRows)
            {
                string[] parts = rowStr.Split('|');
                int idx = gridConfig.Rows.Add(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5], parts[6], parts[7], parts[8], parts[9], parts[10], parts[11], parts[12]);
                UpdateRowUnitDropdown(idx);
                gridConfig.Rows[idx].Cells["colUnit"].Value = parts[12];
                RecalculateRowLimits(gridConfig.Rows[idx]);
            }

            RenumberRows();
            UpdateTotalNetCount();
            UpdateDiagramSize();
            picPreview.Invalidate();
        }

        private void RenumberRows()
        {
            for (int i = 0; i < gridConfig.Rows.Count; i++)
            {
                gridConfig.Rows[i].Cells["colNo"].Value = (i + 1).ToString();
            }
        }

        private void UpdateTotalNetCount()
        {
            btnTotalNet.Text = $"Total Net: {gridConfig.Rows.Count}";
        }

        private void UpdateDiagramSize()
        {
            int rowCount = gridConfig.Rows.Count;
            float minRowSpacing = 55f;
            int reqHeight = (int)(60 + Math.Max(rowCount, 1) * minRowSpacing + 60);
            int finalHeight = Math.Max(pnlPreviewScroll.ClientSize.Height, reqHeight);
            int finalWidth = Math.Max(pnlPreviewScroll.ClientSize.Width - (reqHeight > pnlPreviewScroll.ClientSize.Height ? 25 : 0), 500);

            if (picPreview.Width != finalWidth || picPreview.Height != finalHeight)
            {
                picPreview.Size = new Size(finalWidth, finalHeight);
            }
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            int newIdx = gridConfig.Rows.Count + 1;
            int idx = gridConfig.Rows.Add(newIdx.ToString(), "Elec-Control-001", "Low Voltage Supply", "0", $"X1-0{newIdx}", "0", $"Y1-0{newIdx}", "Resistance", "0", "5", "2.5", "±5%", "Ω");
            UpdateRowUnitDropdown(idx);
            RecalculateRowLimits(gridConfig.Rows[idx]);
            RenumberRows();
            UpdateTotalNetCount();
            UpdateDiagramSize();
            picPreview.Invalidate();
        }

        private void BtnRemove_Click(object? sender, EventArgs e)
        {
            if (gridConfig.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in gridConfig.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        highlightedRows.Remove(row.Index);
                        gridConfig.Rows.Remove(row);
                    }
                }
            }
            else if (gridConfig.CurrentRow != null && !gridConfig.CurrentRow.IsNewRow)
            {
                highlightedRows.Remove(gridConfig.CurrentRow.Index);
                gridConfig.Rows.Remove(gridConfig.CurrentRow);
            }

            RenumberRows();
            UpdateTotalNetCount();
            UpdateDiagramSize();
            picPreview.Invalidate();
        }

        private void BtnReset_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete all rows and reset the configuration?", "Confirm Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                gridConfig.Rows.Clear();
                highlightedRows.Clear();
                UpdateTotalNetCount();
                UpdateDiagramSize();
                picPreview.Invalidate();
            }
        }

        private void BtnImport_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog ofd = new();
            ofd.Filter = "Config Files (*.json;*.xml;*.csv)|*.json;*.xml;*.csv|All Files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Configuration file imported successfully!", "Import Config", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnExport_Click(object? sender, EventArgs e)
        {
            //using SaveFileDialog sfd = new();
            //sfd.Filter = "JSON Config (*.json)|*.json|CSV Config (*.csv)|*.csv";
            //sfd.FileName = "AutomatedTestConfig.json";
            //if (sfd.ShowDialog() == DialogResult.OK)
            //{
            //    MessageBox.Show("Configuration exported successfully!", "Export Config", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //};
            double conectionResistance = double.Parse(txtConnectionResistance.Text.Trim());
            double insulationResistance = double.Parse(txtInsulationResistance.Text.Trim());

            var config = new TestConfiguration
            {
                ConfigName = txtConfigName.Text.Trim(),
                ProductModel = txtProductModel.Text.Trim(),
                ConfigCode = "CFG-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                CreatedBy = SessionManager.CurrentUser?.Username ?? "System",
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                Nets = new List<TestConfigNet>()
            };

            foreach (DataGridViewRow row in gridConfig.Rows)
            {
                if (row.IsNewRow) continue;

                // Safely parse double values
                double? ParseDouble(string cellName)
                {
                    var val = row.Cells[cellName].Value?.ToString();
                    return double.TryParse(val, out double res) ? res : (double?)null;
                }

                // Safely parse integer values
                int ParseInt(string cellName)
                {
                    var val = row.Cells[cellName].Value?.ToString();
                    return int.TryParse(val, out int res) ? res : 0;
                }

                var net = new TestConfigNet
                {
                    SequenceNo = ParseInt("colNo"),
                    NetId = row.Cells["colNetId"].Value?.ToString() ?? "",
                    HarnessGroup = row.Cells["colHarnessGroup"].Value?.ToString() ?? "",

                    FromAddress = ParseInt("colFromAddr"),
                    FromLabel = row.Cells["colFromLabel"].Value?.ToString() ?? "",

                    ToAddress = ParseInt("colToAddr"),
                    ToLabel = row.Cells["colToLabel"].Value?.ToString() ?? "",

                    MeasurementUnit = row.Cells["colUnit"].Value?.ToString() ?? "",
                    LowerLimit = ParseDouble("colLowerLimit"),
                    UpperLimit = ParseDouble("colUpperLimit"),

                    // Dynamic TestTypeId mapping
                    TestTypeId = (row.Cells["colTestType"].Value?.ToString() == "Resistance") ? 11 : 15 // 11 = RESISTOR, 15 = WIRE
                };

                config.Nets.Add(net);
            }
            try
            {
                _configService.SaveConfig(config);
                _configService.SetActiveConfig(config.ConfigName);

                var options = new System.Text.Json.JsonSerializerOptions
                {
                    WriteIndented = true,
                    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
                };
                // 2. Convert the object to a JSON string
                string json = System.Text.Json.JsonSerializer.Serialize(config, options);
                // 3. Print the JSON string to your Debug console
                Debug.WriteLine($"Instance:\n{json}");

                string customText = _configService.ConvertToProgramText(config);
                Debug.WriteLine($"Program Text:\n{customText}");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving configuration: {ex.Message}", "Export Config", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void GridConfig_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
        {
            if (gridConfig.IsCurrentCellDirty &&
               (gridConfig.CurrentCell is DataGridViewComboBoxCell || gridConfig.CurrentCell is DataGridViewCheckBoxCell))
            {
                gridConfig.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void GridConfig_Leave(object? sender, EventArgs e)
        {
            CommitGridIfDirty();
        }

        private void CommitGridIfDirty()
        {
            if (gridConfig.IsCurrentCellInEditMode || gridConfig.IsCurrentCellDirty)
            {
                gridConfig.CommitEdit(DataGridViewDataErrorContexts.Commit);
                gridConfig.EndEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void GridConfig_EditingControlShowing(object? sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var colUnit = gridConfig.Columns["colUnit"];
            if (colUnit != null && gridConfig.CurrentCell?.ColumnIndex == colUnit.Index && e.Control is ComboBox combo)
            {
                string testType = gridConfig.CurrentRow?.Cells["colTestType"]?.Value?.ToString() ?? "";
                string[] allowedUnits = GetUnitsForTestType(testType);
                string currentSelection = combo.Text;
                combo.Items.Clear();
                combo.Items.AddRange(allowedUnits);
                if (allowedUnits.Contains(currentSelection))
                {
                    combo.SelectedItem = currentSelection;
                }
                else if (allowedUnits.Length > 0)
                {
                    combo.SelectedIndex = 0;
                }
            }
        }

        private void GridConfig_RowsChanged(object? sender, EventArgs e)
        {
            UpdateTotalNetCount();
            UpdateDiagramSize();
            picPreview.Invalidate();
        }

        private void GridConfig_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < gridConfig.Rows.Count)
            {
                var colTestType = gridConfig.Columns["colTestType"];
                var colExpected = gridConfig.Columns["colExpectedValue"];
                var colTolerance = gridConfig.Columns["colTolerance"];

                if (colTestType != null && e.ColumnIndex == colTestType.Index)
                {
                    UpdateRowUnitDropdown(e.RowIndex);
                    RecalculateRowLimits(gridConfig.Rows[e.RowIndex]);
                }
                else if ((colExpected != null && e.ColumnIndex == colExpected.Index) ||
                         (colTolerance != null && e.ColumnIndex == colTolerance.Index))
                {
                    RecalculateRowLimits(gridConfig.Rows[e.RowIndex]);
                }
            }
            UpdateTotalNetCount();
            UpdateDiagramSize();
            picPreview.Invalidate();
        }

        private void GridConfig_CellValidating(object? sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= gridConfig.Rows.Count) return;
            var row = gridConfig.Rows[e.RowIndex];
            if (row.IsNewRow) return;

            var colTolerance = gridConfig.Columns["colTolerance"];
            if (colTolerance != null && e.ColumnIndex == colTolerance.Index)
            {
                string testType = row.Cells["colTestType"].Value?.ToString() ?? "";
                if (testType.Equals("Continuity", StringComparison.OrdinalIgnoreCase)) return;

                string input = e.FormattedValue?.ToString() ?? "";
                if (!string.IsNullOrWhiteSpace(input) && input != "-")
                {
                    string clean = input.Replace("±", "").Replace("+", "").Replace("%", "").Trim();
                    if (double.TryParse(clean, out double val))
                    {
                        if (val > 100.0 || val < -100.0)
                        {
                            MessageBox.Show("Tolerance must be limited between -100% and +100%.", "Tolerance Limit Exceeded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
        }

        private void RecalculateAllRows()
        {
            foreach (DataGridViewRow row in gridConfig.Rows)
            {
                if (!row.IsNewRow)
                {
                    RecalculateRowLimits(row);
                }
            }
        }

        private void RecalculateRowLimits(DataGridViewRow row)
        {
            if (row == null || row.IsNewRow) return;

            string testType = row.Cells["colTestType"].Value?.ToString() ?? "";

            if (testType.Equals("Continuity", StringComparison.OrdinalIgnoreCase))
            {
                row.Cells["colExpectedValue"].Value = "0";
                row.Cells["colExpectedValue"].ReadOnly = true;

                row.Cells["colTolerance"].Value = "-";
                row.Cells["colTolerance"].ReadOnly = true;

                row.Cells["colLowerLimit"].Value = "0";
                row.Cells["colUpperLimit"].Value = txtConnectionResistance.Text?.Trim() ?? "0";
            }
            else
            {
                row.Cells["colExpectedValue"].ReadOnly = false;
                row.Cells["colTolerance"].ReadOnly = false;

                string expStr = row.Cells["colExpectedValue"].Value?.ToString() ?? "0";
                string tolStr = row.Cells["colTolerance"].Value?.ToString() ?? "±0%";

                if (double.TryParse(expStr, out double expectedVal))
                {
                    (double minTolPct, double maxTolPct, string formattedTol) = ParseClampAndFormatTolerance(tolStr);
                    
                    if (row.Cells["colTolerance"].Value?.ToString() != formattedTol)
                    {
                        row.Cells["colTolerance"].Value = formattedTol;
                    }

                    double lower = expectedVal * (1.0 + (minTolPct / 100.0));
                    double upper = expectedVal * (1.0 + (maxTolPct / 100.0));

                    if (lower > upper)
                    {
                        (lower, upper) = (upper, lower);
                    }

                    row.Cells["colLowerLimit"].Value = FormatLimit(lower);
                    row.Cells["colUpperLimit"].Value = FormatLimit(upper);
                }
                else
                {
                    row.Cells["colLowerLimit"].Value = "0";
                    row.Cells["colUpperLimit"].Value = "0";
                }
            }
        }

        private static (double minTolPct, double maxTolPct, string formattedTol) ParseClampAndFormatTolerance(string tolStr)
        {
            if (string.IsNullOrWhiteSpace(tolStr) || tolStr == "-")
            {
                return (0, 0, "±0%");
            }

            string clean = tolStr.Replace("±", "").Replace("+", "").Replace("%", "").Trim();
            if (double.TryParse(clean, out double val))
            {
                if (val > 100.0) val = 100.0;
                if (val < -100.0) val = -100.0;

                string numStr = FormatLimit(Math.Abs(val));
                if (tolStr.Contains("±") || (!tolStr.StartsWith("-") && !tolStr.StartsWith("+")))
                {
                    return (-Math.Abs(val), Math.Abs(val), $"±{numStr}%");
                }
                else if (tolStr.StartsWith("-"))
                {
                    return (-Math.Abs(val), 0, $"-{numStr}%");
                }
                else if (tolStr.StartsWith("+"))
                {
                    return (0, Math.Abs(val), $"+{numStr}%");
                }
            }
            return (0, 0, "±0%");
        }

        private static string FormatLimit(double val)
        {
            if (Math.Abs(val - Math.Round(val)) < 1e-6)
            {
                return ((long)Math.Round(val)).ToString();
            }
            return val.ToString("0.###");
        }

        private void GridConfig_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= gridConfig.Rows.Count) return;

            if (e.ColumnIndex == 0 || e.ColumnIndex == -1)
            {
                ToggleHighlight(e.RowIndex);
            }
        }

        private void ToggleHighlight(int rowIndex)
        {
            if (!highlightedRows.Remove(rowIndex))
            {
                highlightedRows.Add(rowIndex);
            }

            UpdateGridHighlights();
            picPreview.Invalidate();
        }

        private void UpdateGridHighlights()
        {
            for (int i = 0; i < gridConfig.Rows.Count; i++)
            {
                DataGridViewRow row = gridConfig.Rows[i];
                if (highlightedRows.Contains(i))
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(254, 249, 195);
                    row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(253, 224, 71);
                    row.DefaultCellStyle.SelectionForeColor = Color.Black;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                    row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(226, 232, 240);
                    row.DefaultCellStyle.SelectionForeColor = Color.Black;
                }
            }
        }

        private void PicPreview_MouseDown(object? sender, MouseEventArgs e)
        {
            PointF pt = e.Location;

            foreach (var node in leftNodes.Concat(rightNodes).OrderByDescending(n => highlightedRows.Contains(n.RowIndex)))
            {
                if (IsPointInRect(pt, node.NodeRect) || IsPointInRect(pt, node.LabelRect))
                {
                    ToggleHighlight(node.RowIndex);
                    return;
                }
            }

            foreach (var conn in connections.OrderByDescending(c => highlightedRows.Contains(c.RowIndex)))
            {
                if (IsPointNearBezier(pt, conn.StartPoint, conn.Control1, conn.Control2, conn.EndPoint, 12f))
                {
                    ToggleHighlight(conn.RowIndex);
                    return;
                }
            }
        }

        private static bool IsPointInRect(PointF pt, RectangleF rect)
        {
            return pt.X >= rect.Left && pt.X <= rect.Right && pt.Y >= rect.Top && pt.Y <= rect.Bottom;
        }

        private static bool IsPointNearBezier(PointF pt, PointF p0, PointF p1, PointF p2, PointF p3, float threshold)
        {
            int steps = 40;
            for (int i = 0; i <= steps; i++)
            {
                float t = i / (float)steps;
                float u = 1 - t;
                float tt = t * t;
                float uu = u * u;
                float uuu = uu * u;
                float ttt = tt * t;

                float x = uuu * p0.X + 3 * uu * t * p1.X + 3 * u * tt * p2.X + ttt * p3.X;
                float y = uuu * p0.Y + 3 * uu * t * p1.Y + 3 * u * tt * p2.Y + ttt * p3.Y;

                float dx = pt.X - x;
                float dy = pt.Y - y;
                if (Math.Sqrt(dx * dx + dy * dy) <= threshold)
                {
                    return true;
                }
            }
            return false;
        }

        private void PicPreview_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Rectangle r = picPreview.ClientRectangle;

            using SolidBrush bgBrush = new(Color.FromArgb(248, 250, 252));
            g.FillRectangle(bgBrush, r);

            using SolidBrush dotBrush = new(Color.FromArgb(226, 232, 240));
            for (int x = 20; x < r.Width; x += 25)
            {
                for (int y = 20; y < r.Height; y += 25)
                {
                    g.FillEllipse(dotBrush, x - 1, y - 1, 2, 2);
                }
            }

            leftNodes.Clear();
            rightNodes.Clear();
            connections.Clear();

            int rowCount = gridConfig.Rows.Count;
            if (rowCount == 0) return;

            float availHeight = r.Height - 100;
            float startY = 50f;
            float rowSpacing = Math.Max(55f, availHeight / Math.Max(rowCount, 1));

            List<DiagramNode> rawLeftNodes = [];
            List<DiagramNode> rawRightNodes = [];

            for (int i = 0; i < rowCount; i++)
            {
                DataGridViewRow row = gridConfig.Rows[i];
                string fromAddr = row.Cells["colFromAddr"].Value?.ToString() ?? "0";
                string fromLabel = row.Cells["colFromLabel"].Value?.ToString() ?? $"X1-0{i + 1}";
                string toAddr = row.Cells["colToAddr"].Value?.ToString() ?? "0";
                string toLabel = row.Cells["colToLabel"].Value?.ToString() ?? $"Y1-0{i + 1}";

                DiagramNode lNode = new()
                {
                    RowIndex = i,
                    Address = fromAddr,
                    Label = fromLabel,
                    IsLeft = true
                };
                rawLeftNodes.Add(lNode);

                DiagramNode rNode = new()
                {
                    RowIndex = i,
                    Address = toAddr,
                    Label = toLabel,
                    IsLeft = false
                };
                rawRightNodes.Add(rNode);
            }

            var sortedLeftNodes = rawLeftNodes.OrderBy(n => int.TryParse(n.Address, out int val) ? val : n.Address.GetHashCode()).ToList();
            for (int idx = 0; idx < sortedLeftNodes.Count; idx++)
            {
                float yPos = startY + idx * rowSpacing;
                var lNode = sortedLeftNodes[idx];
                lNode.NodeRect = new(185, yPos - 22, 50, 44);
                lNode.LabelRect = new(45, yPos - 18, 132, 36);
                leftNodes.Add(lNode);
            }

            var sortedRightNodes = rawRightNodes.OrderBy(n => int.TryParse(n.Address, out int val) ? val : n.Address.GetHashCode()).ToList();
            for (int idx = 0; idx < sortedRightNodes.Count; idx++)
            {
                float yPos = startY + idx * rowSpacing;
                var rNode = sortedRightNodes[idx];
                rNode.NodeRect = new(r.Width - 235, yPos - 22, 50, 44);
                rNode.LabelRect = new(r.Width - 177, yPos - 18, 132, 36);
                rightNodes.Add(rNode);
            }

            for (int i = 0; i < rowCount; i++)
            {
                var lNode = leftNodes.FirstOrDefault(n => n.RowIndex == i);
                var rNode = rightNodes.FirstOrDefault(n => n.RowIndex == i);

                if (lNode != null && rNode != null)
                {
                    PointF startPt = new(lNode.NodeRect.Right, lNode.NodeRect.Top + lNode.NodeRect.Height / 2);
                    PointF endPt = new(rNode.NodeRect.Left, rNode.NodeRect.Top + rNode.NodeRect.Height / 2);
                    float offset = (endPt.X - startPt.X) * 0.45f;

                    connections.Add(new()
                    {
                        RowIndex = i,
                        StartPoint = startPt,
                        EndPoint = endPt,
                        Control1 = new(startPt.X + offset, startPt.Y),
                        Control2 = new(endPt.X - offset, endPt.Y),
                        LeftNode = lNode,
                        RightNode = rNode
                    });
                }
            }

            using Pen normalPen = new(Color.FromArgb(14, 165, 233), 3f) { StartCap = LineCap.Round, EndCap = LineCap.Round };
            using Pen darkPen = new(Color.FromArgb(15, 23, 42), 3f) { StartCap = LineCap.Round, EndCap = LineCap.Round };
            foreach (var conn in connections)
            {
                if (!highlightedRows.Contains(conn.RowIndex))
                {
                    Pen p = (conn.RowIndex % 2 == 0) ? normalPen : darkPen;
                    g.DrawBezier(p, conn.StartPoint, conn.Control1, conn.Control2, conn.EndPoint);
                }
            }

            using Font fontLabel = new("Segoe UI", 9.5F, FontStyle.Bold);
            using Font fontAddr = new("Segoe UI", 10.5F, FontStyle.Bold);
            using StringFormat sfLabel = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoWrap, Trimming = StringTrimming.EllipsisCharacter };
            using StringFormat sfAddr = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.NoClip };

            foreach (var node in leftNodes.Concat(rightNodes))
            {
                if (!highlightedRows.Contains(node.RowIndex))
                {
                    DrawNodeAndLabel(g, node, false, fontLabel, fontAddr, sfLabel, sfAddr);
                }
            }

            using Pen glowPen = new(Color.FromArgb(254, 240, 138), 11f) { StartCap = LineCap.Round, EndCap = LineCap.Round };
            using Pen innerPen = new(Color.FromArgb(13, 110, 253), 3.5f) { StartCap = LineCap.Round, EndCap = LineCap.Round };
            foreach (var conn in connections)
            {
                if (highlightedRows.Contains(conn.RowIndex))
                {
                    g.DrawBezier(glowPen, conn.StartPoint, conn.Control1, conn.Control2, conn.EndPoint);
                    g.DrawBezier(innerPen, conn.StartPoint, conn.Control1, conn.Control2, conn.EndPoint);
                }
            }

            foreach (var node in leftNodes.Concat(rightNodes))
            {
                if (highlightedRows.Contains(node.RowIndex))
                {
                    DrawNodeAndLabel(g, node, true, fontLabel, fontAddr, sfLabel, sfAddr);
                }
            }
        }

        private static void DrawNodeAndLabel(Graphics g, DiagramNode node, bool isHighlight, Font fontLabel, Font fontAddr, StringFormat sfLabel, StringFormat sfAddr)
        {
            if (isHighlight)
            {
                RectangleF haloRect = RectangleF.Inflate(node.NodeRect, 6f, 6f);
                using SolidBrush haloFill = new(Color.FromArgb(254, 240, 138));
                using Pen haloBorder = new(Color.FromArgb(245, 158, 11), 3f);
                DrawRoundedRectangle(g, haloFill, haloBorder, haloRect, 24f);
            }

            using SolidBrush labelBg = new(isHighlight ? Color.FromArgb(254, 249, 195) : Color.FromArgb(226, 232, 240));
            using Pen labelBorder = new(isHighlight ? Color.FromArgb(245, 158, 11) : Color.FromArgb(203, 213, 225), 1.5f);
            using SolidBrush labelText = new(Color.FromArgb(30, 41, 59));
            DrawRoundedRectangle(g, labelBg, labelBorder, node.LabelRect, 8f);
            g.DrawString(node.Label, fontLabel, labelText, node.LabelRect, sfLabel);

            Color nodeColor = node.IsLeft ? Color.FromArgb(0, 180, 216) : Color.FromArgb(74, 222, 128);
            using SolidBrush nodeFill = new(nodeColor);
            using Pen nodeBorder = new(Color.FromArgb(15, 23, 42), 2f);
            using SolidBrush addrText = new(Color.FromArgb(15, 23, 42));
            DrawRoundedRectangle(g, nodeFill, nodeBorder, node.NodeRect, 20f);

            float dynamicSize = node.Address.Length >= 4 ? 7.5F : (node.Address.Length == 3 ? 8.5F : (node.Address.Length == 2 ? 9.5F : 10.5F));
            using Font dynamicAddrFont = new("Segoe UI", dynamicSize, FontStyle.Bold);
            g.DrawString(node.Address, dynamicAddrFont, addrText, node.NodeRect, sfAddr);
        }

        private static void DrawRoundedRectangle(Graphics g, Brush brush, Pen pen, RectangleF rect, float radius)
        {
            using GraphicsPath path = new();
            float d = radius * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            g.FillPath(brush, path);
            g.DrawPath(pen, path);
        }
    }
}
