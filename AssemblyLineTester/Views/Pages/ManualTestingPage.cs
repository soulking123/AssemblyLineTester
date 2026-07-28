using AssemblyLineTester.Models;
using AssemblyLineTester.Services;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace AssemblyLineTester.Views.Pages
{
    public partial class ManualTestingPage : UserControl
    {
        private readonly HashSet<int> highlightedRows = new();
        private readonly List<DiagramNode> leftNodes = new();
        private readonly List<DiagramNode> rightNodes = new();
        private readonly List<DiagramConnection> connections = new();
        private readonly TestConfigService _configService = new TestConfigService();
        private readonly InstrumentService _instrument = new InstrumentService();
        private string _customTestText = string.Empty;
        private System.Threading.CancellationTokenSource? _testCts;

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
                AssemblyLineTester.Helpers.UIStyleHelper.ScaleExplicitFonts(this, AssemblyLineTester.Helpers.UIStyleHelper.GetHostScaleFactor(this));
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
            btnExport.Click += (s, e) => MessageBox.Show("Export feature not implemented yet.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnImport.Click += (s, e) => MessageBox.Show("Import feature not implemented yet.", "Import", MessageBoxButtons.OK, MessageBoxIcon.Information);

            gridConfig.CellValueChanged += GridConfig_CellValueChanged;
            gridConfig.RowsRemoved += GridConfig_RowsChanged;
            gridConfig.RowsAdded += GridConfig_RowsChanged;
            gridConfig.CellClick += GridConfig_CellClick;

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


        public ManualTestingPage()
        {
            InitializeComponent();
            EnableDoubleBuffering();
            SetupEvents();
        }

        private void EnableDoubleBuffering()
        {
            try
            {
                typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(gridConfig, true, null);
                typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(splitContainerMain, true, null);
                typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(picPreview, true, null);
            }
            catch { /* ignore */ }
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
            string defaultTestType = "WIRE";
            string defaultUnit = UnitConversionHelper.GetBaseUnit(defaultTestType);

            int idx = gridConfig.Rows.Add(newIdx.ToString(), $"{newIdx}", $"{newIdx + 64}", defaultTestType, defaultUnit);
            UpdateRowUnitDropdown(idx);
            gridConfig.Rows[idx].Cells["colUnit"].Value = defaultUnit;

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
            if (MessageBox.Show("Are you sure you want to delete all rows?", "Confirm Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                gridConfig.Rows.Clear();
                highlightedRows.Clear();
                UpdateTotalNetCount();
                UpdateDiagramSize();
                picPreview.Invalidate();
            }
        }
        
        private void ParseAndPopulateGridFromProgramText(string programText)
        {
            if (string.IsNullOrWhiteSpace(programText)) return;

            gridConfig.Rows.Clear();
            highlightedRows.Clear();

            var lines = programText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                string trimmed = line.Trim();

                // Comments, header metadata, parameters and the closing bracket.
                if (trimmed.StartsWith("*") || trimmed.StartsWith("(") || trimmed.StartsWith(")") ||
                    trimmed.StartsWith("J1 ") || trimmed.StartsWith("J3 ") ||
                    trimmed.StartsWith("CONNECTION RESIS", StringComparison.OrdinalIgnoreCase) ||
                    trimmed.StartsWith("LV INSULATION", StringComparison.OrdinalIgnoreCase) ||
                    trimmed.StartsWith("HIPOT VOLTAGE", StringComparison.OrdinalIgnoreCase) ||
                    trimmed.StartsWith("TEST METHOD", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var tokens = trimmed.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                // A net line reads: SeqNo FromPin ToPin, e.g. 1 J1-040 J3-040.
                if (tokens.Length >= 3 && int.TryParse(tokens[0], out int seqNo))
                {
                    string fromPin = tokens[1];
                    string toPin = tokens[2];

                    int fromAddr = ParsePinAddress(fromPin);
                    int toAddr = ParsePinAddress(toPin);

                    gridConfig.Rows.Add(
                        seqNo.ToString(),
                        fromAddr.ToString(),
                        toAddr.ToString(),
                        "WIRE",
                        "-"
                    );
                }
            }

            RenumberRows();
            UpdateTotalNetCount();
            UpdateDiagramSize();
            picPreview.Invalidate();
        }

        private static int ParsePinAddress(string pinStr)
        {
            if (string.IsNullOrWhiteSpace(pinStr)) return 0;
            string trimmed = pinStr.Trim();
            var parts = trimmed.Split('-');
            if (parts.Length == 2 && int.TryParse(parts[1], out int pinNum))
            {
                if (parts[0].Equals("J3", StringComparison.OrdinalIgnoreCase))
                {
                    return pinNum + 64;
                }
                return pinNum;
            }

            if (int.TryParse(trimmed, out int directAddr))
            {
                return directAddr;
            }
            return 0;
        }

        private List<int> GetAllRowIndices()
        {
            var list = new List<int>();
            for (int i = 0; i < gridConfig.Rows.Count; i++)
            {
                if (!gridConfig.Rows[i].IsNewRow)
                {
                    list.Add(i);
                }
            }
            return list;
        }

        private List<int> GetSelectedOrHighlightedRowIndices()
        {
            var set = new HashSet<int>(highlightedRows);
            foreach (DataGridViewRow row in gridConfig.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    set.Add(row.Index);
                }
            }
            return set.OrderBy(idx => idx).ToList();
        }
        private async void btnLearnConnection_Click(object sender, EventArgs e)
        {
            _testCts?.Cancel();
            _testCts = new System.Threading.CancellationTokenSource();
            var token = _testCts.Token;

            btnLearnConnection.Enabled = false;
            btnStartAll.Enabled = false;
            btnStartSelected.Enabled = false;

            
            try
            {
                bool isConnected = _instrument.Connect();
                if (!isConnected)
                {
                    MessageBox.Show("Failed to connect to instrument.", "Connection Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Debug.WriteLine("debuggin2");
                var learnResult = await Task.Run(() => _instrument.LearnConnection(), token);
                if (token.IsCancellationRequested) return;

                if (learnResult != CtaDotNet.Enums.CtaCommandResultCode.Success)
                {
                    MessageBox.Show(this, $"Failed to execute learn operation on instrument. Error: {learnResult}", "Learn Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var getTestResult = await Task.Run(() => _instrument.GetLearnedConnection(), token);
                if (token.IsCancellationRequested) return;
                Debug.WriteLine("debuggin");
                Debug.WriteLine(getTestResult);
                if (getTestResult == null || string.IsNullOrWhiteSpace(getTestResult.Result))
                {

                    MessageBox.Show(this, "Failed to retrieve learned test program text from instrument.", "Learn Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _customTestText = getTestResult.Result;
                ParseAndPopulateGridFromProgramText(_customTestText);

                MessageBox.Show(this, "Connection learned and table updated successfully!", "Learn Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OperationCanceledException)
            {
                // Aborted from btnAbort, nothing left to report.
            }
            finally
            {
                btnLearnConnection.Enabled = true;
                btnStartAll.Enabled = true;
                btnStartSelected.Enabled = true;
            }
        }

        private async void btnStartAll_Click(object sender, EventArgs e)
        {
            _testCts?.Cancel();
            _testCts = new System.Threading.CancellationTokenSource();
            var token = _testCts.Token;
            btnLearnConnection.Enabled = false;
            btnStartAll.Enabled = false;
            btnStartSelected.Enabled = false;

            try
            {
                var selectedRows = GetAllRowIndices();
                if (selectedRows.Count == 0)
                {
                    MessageBox.Show(this, "Empty Row to Start.", "No Row Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                await Task.Run(() => ExecuteTestForRows(selectedRows, token), token);

                if (!token.IsCancellationRequested)
                {
                    MessageBox.Show(this, "Finished testing all connections", "Test Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (OperationCanceledException)
            {
                // Aborted from btnAbort, nothing left to report.
            }
            finally
            {
                btnLearnConnection.Enabled = true;
                btnStartAll.Enabled = true;
                btnStartSelected.Enabled = true;
            }
        }

        private async void btnStartSelected_Click(object sender, EventArgs e)
        {
            _testCts?.Cancel();
            _testCts = new System.Threading.CancellationTokenSource();
            var token = _testCts.Token;

            btnLearnConnection.Enabled = false;
            btnStartAll.Enabled = false;
            btnStartSelected.Enabled = false;

            try
            {
                var selectedRows = GetSelectedOrHighlightedRowIndices();
                if (selectedRows.Count == 0)
                {
                    MessageBox.Show(this, "Please click or select at least one row to test.", "No Row Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                await Task.Run(() => ExecuteTestForRows(selectedRows, token), token);

                if (!token.IsCancellationRequested)
                {
                    MessageBox.Show(this, "Finished testing selected connections", "Test Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (OperationCanceledException)
            {
                // Aborted from btnAbort, nothing left to report.
            }
            finally
            {
                btnLearnConnection.Enabled = true;
                btnStartAll.Enabled = true;
                btnStartSelected.Enabled = true;
            }
        }

        private async void btnAbort_Click(object sender, EventArgs e)
        {
            _testCts?.Cancel();

            var abortResult = await Task.Run(() => _instrument.Abort());
            if (abortResult == CtaDotNet.Enums.CtaCommandResultCode.Success)
            {
                MessageBox.Show(this, "Test operation successfully aborted.", "Abort Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(this, $"Failed to send abort command to instrument. Error: {abortResult}", "Abort Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExecuteTestForRows(List<int> targetRowIndices, System.Threading.CancellationToken token = default)
        {
            if (token.IsCancellationRequested) return;

            if (targetRowIndices.Count == 0)
            {
                MessageBox.Show(this, "No valid rows selected for testing.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool isConnected = _instrument.Connect();
            if (!isConnected)
            {
                MessageBox.Show(this, "Failed to connect to instrument.", "Connection Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (token.IsCancellationRequested) return;

            var manualConfig = BuildConfigurationForRows(targetRowIndices, txtConfigName.Text?.Trim() ?? "", txtProductModel.Text?.Trim() ?? "", null);
            if (manualConfig == null || manualConfig.Nets.Count == 0) return;

            _customTestText = _configService.ConvertToProgramText(manualConfig);
            Debug.WriteLine(_customTestText);

            if (token.IsCancellationRequested) return;

            try
            {
                if (!string.IsNullOrEmpty(_customTestText))
                {
                    var putResult = _instrument.PutTestProgram(string.Empty, _customTestText, true);
                    if (putResult != CtaDotNet.Enums.CtaCommandResultCode.Success)
                    {
                        _instrument.Disconnect();
                        MessageBox.Show(this, $"Failed to upload custom test program. Error: {putResult}", "Upload Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(this, "Generated test program text is empty.", "Program Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Error converting configuration to program text:\n{ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (token.IsCancellationRequested) return;

            Debug.WriteLine("Run Test");
            var testResult = _instrument.RunSingleTest(false, true);

            if (token.IsCancellationRequested) return;

            if (testResult == null || testResult.DllCallResult != CtaDotNet.Enums.CtaCommandResultCode.Success)
            {
                MessageBox.Show(this, $"Test execution failed. Error: {testResult?.DllCallResult.ToString() ?? "Unknown"}", "Execution Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Debug.WriteLine("Get Measured Value");
            var measuredValuesResult = _instrument.GetMeasuredValues();

            if (token.IsCancellationRequested) return;

            if (measuredValuesResult != null && measuredValuesResult.DllCallResult == CtaDotNet.Enums.CtaCommandResultCode.Success && measuredValuesResult.ObjectReturnValue != null)
            {
                var list = measuredValuesResult.ObjectReturnValue.MeasuredValueList;
                if (list != null)
                {
                    int count = Math.Min(targetRowIndices.Count, list.Count);
                    for (int k = 0; k < count; k++)
                    {
                        if (token.IsCancellationRequested) return;
                        int rowIndex = targetRowIndices[k];
                        if (rowIndex >= 0 && rowIndex < gridConfig.Rows.Count)
                        {
                            var measurement = list[k];
                            string valStr = measurement.Value1.ToString("F3", System.Globalization.CultureInfo.InvariantCulture);
                            gridConfig.Rows[rowIndex].Cells["colUnit"].Value = valStr;
                        }
                    }
                }
                else
                {
                    MessageBox.Show(this, "No measured values returned from instrument.", "Test Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show(this, $"Failed to retrieve measured values. Error: {measuredValuesResult?.DllCallResult.ToString() ?? "Unknown"}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private TestConfiguration? BuildConfigurationForRows(List<int> targetRowIndices, string configName, string productModel, string? description)
        {
            var nets = new List<TestConfigNet>();

            for (int k = 0; k < targetRowIndices.Count; k++)
            {
                int rowIndex = targetRowIndices[k];
                if (rowIndex < 0 || rowIndex >= gridConfig.Rows.Count) continue;

                var row = gridConfig.Rows[rowIndex];
                if (row.IsNewRow) continue;

                if (!int.TryParse(row.Cells["colFromAddr"].Value?.ToString(), out int fromAddr))
                {
                    MessageBox.Show($"Row {rowIndex + 1}: Net-From Address must be a number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }
                if (!int.TryParse(row.Cells["colToAddr"].Value?.ToString(), out int toAddr))
                {
                    MessageBox.Show($"Row {rowIndex + 1}: Net-To Address must be a number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }

                string testTypeName = row.Cells["colTestType"].Value?.ToString() ?? "";
                var tt = _configService.GetTestTypeByName(testTypeName);
                nets.Add(new TestConfigNet
                {
                    SequenceNo = nets.Count + 1,
                    FromAddress = fromAddr,
                    ToAddress = toAddr,
                    TestTypeId = tt?.Id ?? 0,
                    TestType = null,
                    LowerLimit = 10,
                    UpperLimit = 10,
                    MeasurementUnit = row.Cells["colUnit"].Value?.ToString() ?? ""
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
        private void GridConfig_RowsChanged(object? sender, EventArgs e)
        {
            UpdateTotalNetCount();
            UpdateDiagramSize();
            picPreview.Invalidate();
        }

        private void GridConfig_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == gridConfig.Columns["colTestType"]?.Index)
            {
                UpdateRowUnitDropdown(e.RowIndex);
            }
            picPreview.Invalidate();
        }

        private void GridConfig_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
        {
            if (gridConfig.IsCurrentCellDirty &&
               (gridConfig.CurrentCell is DataGridViewComboBoxCell || gridConfig.CurrentCell is DataGridViewCheckBoxCell))
            {
                gridConfig.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
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
        }

        private static bool IsPointInRect(PointF pt, RectangleF rect)
        {
            return pt.X >= rect.Left && pt.X <= rect.Right && pt.Y >= rect.Top && pt.Y <= rect.Bottom;
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
                string fromLabel = fromAddr;
                string toAddr = row.Cells["colToAddr"].Value?.ToString() ?? "0";
                string toLabel = toAddr;

                DiagramNode lNode = new() { RowIndex = i, Address = fromAddr, Label = fromLabel, IsLeft = true };
                rawLeftNodes.Add(lNode);

                DiagramNode rNode = new() { RowIndex = i, Address = toAddr, Label = toLabel, IsLeft = false };
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

                    float dist = Math.Abs(endPt.X - startPt.X);
                    float ctlXOffset = Math.Max(dist * 0.4f, 40f);
                    PointF c1 = new(startPt.X + ctlXOffset, startPt.Y);
                    PointF c2 = new(endPt.X - ctlXOffset, endPt.Y);

                    connections.Add(new DiagramConnection
                    {
                        RowIndex = i,
                        StartPoint = startPt,
                        EndPoint = endPt,
                        Control1 = c1,
                        Control2 = c2,
                        LeftNode = lNode,
                        RightNode = rNode
                    });
                }
            }

            using Pen wirePen = new(Color.FromArgb(148, 163, 184), 2.5f);
            using Pen activeWirePen = new(Color.FromArgb(34, 197, 94), 3.5f);
            foreach (var conn in connections.OrderBy(c => highlightedRows.Contains(c.RowIndex)))
            {
                Pen p = highlightedRows.Contains(conn.RowIndex) ? activeWirePen : wirePen;
                g.DrawBezier(p, conn.StartPoint, conn.Control1, conn.Control2, conn.EndPoint);
            }

            using Font fontLabel = new("Segoe UI", 9F, FontStyle.Regular);
            using Font fontAddr = new("Segoe UI", 10.5F, FontStyle.Bold);
            using StringFormat sfLabel = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter };
            using StringFormat sfAddr = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

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
                DrawNodeAndLabel(g, node, highlightedRows.Contains(node.RowIndex), fontLabel, fontAddr, sfLabel, sfAddr);
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