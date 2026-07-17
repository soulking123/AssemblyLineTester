using AssemblyLineTester.Services;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using AssemblyLineTester.Models;
using AssemblyLineTester.Models.Dto;
using System.Linq;

namespace AssemblyLineTester.Views.Pages
{
    public partial class SettingsTestParametersPage : UserControl
    {
        private readonly HashSet<int> highlightedRows = new();
        private readonly List<DiagramNode> leftNodes = new();
        private readonly List<DiagramNode> rightNodes = new();
        private readonly List<DiagramConnection> connections = new();
        private readonly TestConfigService _configService = new TestConfigService();
        private string? _loadedConfigName = null;

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

        private static string[] GetUnitsForTestType(string testType) => UnitConversionHelper.GetUnitsForTestType(testType);

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

        public SettingsTestParametersPage()
        {
            InitializeComponent();
            EnableDoubleBuffering();
            SetupEvents();
            if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime && !DesignMode)
            {
                LoadTestTypeColumnItems();
                LoadDefaultData();
                UpdateTotalNetCount();
                UpdateDiagramSize();
            }
        }

        private void EnableDoubleBuffering()
        {
            try
            {
                typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(gridConfig, true, null);
                typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(splitContainerMain, true, null);
                typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(picPreview, true, null);
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

        private void LoadTestTypeColumnItems()
        {
            var names = _configService.GetTestTypeNames();

            if (gridConfig.Columns["colTestType"] is DataGridViewComboBoxColumn testTypeCol)
            {
                testTypeCol.Items.Clear();
                testTypeCol.Items.AddRange(names.Cast<object>().ToArray());
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
            // Open the Edit Config modal (lists saved configurations from DB)
            ShowEditConfigsDialog();
        }

        private void ShowEditConfigsDialog()
        {
            using var dlg = new AssemblyLineTester.Views.Modals.EditConfigsModalForm(_configService, _loadedConfigName);
            if (dlg.ShowDialog(this) == DialogResult.OK && dlg.SelectedConfiguration != null)
            {
                LoadConfigurationIntoGrid(dlg.SelectedConfiguration);
            }
            if (dlg.DeletedLoadedConfig)
            {
                _loadedConfigName = null;
            }
        }

        private void LoadConfigurationIntoGrid(TestConfiguration config)
        {
            if (config == null) return;

            gridConfig.Rows.Clear();
            highlightedRows.Clear();

            txtConfigName.Text = config.ConfigName;
            txtProductModel.Text = config.ProductModel;

            foreach (var net in config.Nets.OrderBy(n => n.SequenceNo))
            {
                string testTypeName = net.TestType?.TestTypeName ?? "";
                string expected = net.ExpectedValue.HasValue ? FormatLimit(net.ExpectedValue.Value) : "0";
                string tolerance = net.Tolerance.HasValue ? $"±{FormatLimit(net.Tolerance.Value)}%" : "±0%";

                int idx = gridConfig.Rows.Add(
                    net.SequenceNo.ToString(),
                    net.NetId,
                    net.HarnessGroup,
                    net.FromAddress.ToString(),
                    net.FromLabel,
                    net.ToAddress.ToString(),
                    net.ToLabel,
                    testTypeName,
                    FormatLimit(net.LowerLimit ?? 0),
                    FormatLimit(net.UpperLimit ?? 0),
                    expected,
                    tolerance,
                    net.MeasurementUnit ?? "");

                UpdateRowUnitDropdown(idx);
                gridConfig.Rows[idx].Cells["colUnit"].Value = net.MeasurementUnit ?? "";
                RecalculateRowLimits(gridConfig.Rows[idx]);
            }

            RenumberRows();
            UpdateTotalNetCount();
            UpdateDiagramSize();
            picPreview.Invalidate();

            _loadedConfigName = config.ConfigName;
        }

        private static (string expected, string tolerance) DeriveExpectedAndTolerance(double? lower, double? upper)
        {
            if (!lower.HasValue && !upper.HasValue) return ("0", "±0%");
            if (lower.HasValue && upper.HasValue)
            {
                double mid = (lower.Value + upper.Value) / 2.0;
                double tolPct = mid == 0 ? 0 : Math.Abs((upper.Value - mid) / mid * 100.0);
                return (FormatLimit(mid), $"±{FormatLimit(tolPct)}%");
            }
            double val = lower ?? upper!.Value;
            return (FormatLimit(val), "±0%");
        }

        private void BtnExport_Click(object? sender, EventArgs e)
        {
            // Save current grid configuration to the database.
            var config = BuildConfigurationFromGrid(txtConfigName.Text?.Trim() ?? "", txtProductModel.Text?.Trim() ?? "", null);
            if (config == null)
            {
                MessageBox.Show("Failed to build configuration from the grid. Please check entries.", "Save Config", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(config.ConfigName))
            {
                MessageBox.Show("Config Name is required.", "Save Config", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var existing = _configService.GetConfigurationByName(config.ConfigName);

            try
            {
                if (existing != null)
                {
                    // Allow overwrite only when the user explicitly loaded this config for editing
                    if (!string.IsNullOrWhiteSpace(_loadedConfigName) && string.Equals(_loadedConfigName, config.ConfigName, StringComparison.OrdinalIgnoreCase))
                    {
                        var res = MessageBox.Show($"Overwrite existing configuration '{config.ConfigName}'?", "Confirm Overwrite", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (res != DialogResult.Yes) return;

                        _configService.SaveConfig(config);
                        _loadedConfigName = config.ConfigName;
                        MessageBox.Show("Configuration overwritten successfully.", "Save Config", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    MessageBox.Show("A configuration with the same name already exists. Load it from Edit Config to overwrite or choose a different name.", "Save Config", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // New config
                _configService.SaveConfig(config);
                _loadedConfigName = config.ConfigName;
                MessageBox.Show("Configuration saved successfully.", "Save Config", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save configuration:\n{ex.Message}", "Save Config", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private static readonly string[] ContinuityLikeTypes = TestConfigService.ContinuityLikeTestTypes;

        private void GridConfig_CellValidating(object? sender, DataGridViewCellValidatingEventArgs e)
        {

            if (e.RowIndex < 0 || e.RowIndex >= gridConfig.Rows.Count) return;
            var row = gridConfig.Rows[e.RowIndex];
            if (row.IsNewRow) return;

            var colTolerance = gridConfig.Columns["colTolerance"];
            if (colTolerance != null && e.ColumnIndex == colTolerance.Index)
            {
                string testType = row.Cells["colTestType"].Value?.ToString() ?? "";
                if (ContinuityLikeTypes.Contains(testType, StringComparer.OrdinalIgnoreCase)) return;

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

            List<DiagramNode> rawLeftNodes = new List<DiagramNode>();
            List<DiagramNode> rawRightNodes = new List<DiagramNode>();

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

        private TestConfiguration? BuildConfigurationFromGrid(string configName, string productModel, string? description)
        {
            var nets = new List<TestConfigNet>();

            for (int i = 0; i < gridConfig.Rows.Count; i++)
            {
                var row = gridConfig.Rows[i];
                if (row.IsNewRow) continue;

                if (!int.TryParse(row.Cells["colFromAddr"].Value?.ToString(), out int fromAddr))
                {
                    MessageBox.Show($"Row {i + 1}: Net-From Address must be a number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }
                if (!int.TryParse(row.Cells["colToAddr"].Value?.ToString(), out int toAddr))
                {
                    MessageBox.Show($"Row {i + 1}: Net-To Address must be a number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }

                double.TryParse(row.Cells["colExpectedValue"].Value?.ToString(), out double expected);
                double.TryParse(row.Cells["colLowerLimit"].Value?.ToString(), out double lower);
                double.TryParse(row.Cells["colUpperLimit"].Value?.ToString(), out double upper);

                string testTypeName = row.Cells["colTestType"].Value?.ToString() ?? "";
                var tt = _configService.GetTestTypeByName(testTypeName);
                nets.Add(new TestConfigNet
                {
                    SequenceNo = i + 1,
                    NetId = row.Cells["colNetId"].Value?.ToString() ?? "",
                    HarnessGroup = row.Cells["colHarnessGroup"].Value?.ToString() ?? "",
                    FromAddress = fromAddr,
                    FromLabel = row.Cells["colFromLabel"].Value?.ToString() ?? "",
                    ToAddress = toAddr,
                    ToLabel = row.Cells["colToLabel"].Value?.ToString() ?? "",
                    // Resolve TestType from DB to avoid attaching transient entities to EF context
                    TestTypeId = tt?.Id ?? 0,
                    TestType = null,
                    ExpectedValue = expected,
                    LowerLimit = lower,
                    UpperLimit = upper,
                    Tolerance = ParseTolerancePercent(row.Cells["colTolerance"].Value?.ToString()),
                    MeasurementUnit = row.Cells["colUnit"].Value?.ToString() ?? "" // raw unit as entered - SaveConfig() converts this to base unit
                });
            }

            return new TestConfiguration
            {
                ConfigName = configName,
                ProductModel = productModel,
                Description = description,
                Nets = nets
            };
        }

        private static double? ParseTolerancePercent(string? tolStr)
        {
            if (string.IsNullOrWhiteSpace(tolStr) || tolStr == "-") return null;
            string clean = tolStr.Replace("±", "").Replace("+", "").Replace("%", "").Trim();
            return double.TryParse(clean, out double val) ? val : null;
        }
    }
}
