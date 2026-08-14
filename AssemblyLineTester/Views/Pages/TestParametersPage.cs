
using AssemblyLineTester.Services;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using AssemblyLineTester.Models;
using AssemblyLineTester.Models.Dto;
using System.Linq;

namespace AssemblyLineTester.Views.Pages
{
    public partial class TestParametersPage : UserControl
    {
        private readonly HashSet<int> highlightedRows = new();
        private readonly List<DiagramNode> leftNodes = new();
        private readonly List<DiagramNode> rightNodes = new();
        private readonly List<DiagramConnection> connections = new();
        private readonly TestConfigService _configService = new TestConfigService();
        private readonly InstrumentService _instrument = new InstrumentService();
        private string _customTestText = string.Empty;
        private System.Threading.CancellationTokenSource? _testCts;
        private string? _loadedConfigName = null;
        private TestConfiguration complexConfig = new();

        private class DiagramNode
        {
            public List<int> RowIndices { get; set; } = [];
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
            gridConfig.CellValueChanged += GridConfig_CellValueChanged;
            gridConfig.CellValidating += GridConfig_CellValidating;
            gridConfig.RowsRemoved += GridConfig_RowsChanged;
            gridConfig.RowsAdded += GridConfig_RowsChanged;
            gridConfig.CurrentCellDirtyStateChanged += GridConfig_CurrentCellDirtyStateChanged;
            gridConfig.EditingControlShowing += GridConfig_EditingControlShowing;
            gridConfig.DataError += (s, e) => { e.ThrowException = false; };
            gridConfig.CellClick += GridConfig_CellClick;
            gridConfig.Leave += GridConfig_Leave;
            gridConfig.CellBeginEdit += (s, e) =>
            {
                if (!SessionManager.HasPermission("Action.TestConfig.Edit"))
                {
                    e.Cancel = true;
                    MessageBox.Show("You do not have permission to edit test parameter configurations.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            txtConnectionResistance.TextChanged += (s, e) => RecalculateAllRows();

            picPreview.Paint += PicPreview_Paint;
            picPreview.MouseDown += PicPreview_MouseDown;
            picPreview.Resize += (s, e) => picPreview.Invalidate();
            pnlPreviewScroll.Resize += (s, e) => { UpdateDiagramSize(); picPreview.Invalidate(); };
            splitContainerMain.SplitterMoved += (s, e) => { UpdateDiagramSize(); picPreview.Invalidate(); };

            if (btnAdd != null) btnAdd.Click += BtnAdd_Click;
            if (btnRemove != null) btnRemove.Click += BtnRemove_Click;
            if (btnExport != null) btnExport.Click += BtnExport_Click;
            if (btnImport != null) btnImport.Click += BtnImport_Click;
            if (btnReset != null) btnReset.Click += BtnReset_Click;
            if (btnImportExcel != null) btnImportExcel.Click += BtnImportExcel_Click;
            if (btnTemplate != null) btnTemplate.Click += BtnTemplate_Click;

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

        public TestParametersPage()
        {
            InitializeComponent();
            EnableDoubleBuffering();
            SetupEvents();
            if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime && !DesignMode)
            {
                LoadTestTypeColumnItems();
                UpdateTotalNetCount();
                UpdateDiagramSize();
                LoadProductModels();
            }
        }

        private void LoadProductModels()
        {
            try
            {
                using var db = new Data.AppDbContext();
                var models = db.ProductHarnesses
                    .Select(p => p.ProductModel)
                    .Where(m => !string.IsNullOrEmpty(m))
                    .Distinct()
                    .OrderBy(m => m)
                    .ToArray();
                
                txtProductModel.Items.Clear();
                txtProductModel.Items.AddRange(models);
            }
            catch (Exception ex)
            {
                FileLogService.Error("TestParametersPage.LoadProductModels", "Failed to load product models", ex);
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
        // Columns a workbook row must fill in. Color and Gauge are deliberately absent: they are
        // new, so files written before they existed would otherwise all be rejected.
        private static readonly string[] RequiredImportColumns =
            { "From", "To", "Test Type", "Lower Limit", "Upper Limit", "Tolerance", "Expected Value" };

        // Everything wrong with one workbook row, named by its spreadsheet row number.
        internal static List<string> ValidateImportRow(Dictionary<string, string> values, int rowNumber, List<string> allowedTestTypes)
        {
            var problems = new List<string>();

            foreach (var required in RequiredImportColumns)
            {
                if (!values.TryGetValue(required, out string? value) || string.IsNullOrWhiteSpace(value))
                    problems.Add($"Row {rowNumber}: {required} is empty.");
            }

            values.TryGetValue("Test Type", out string? testType);
            if (!string.IsNullOrWhiteSpace(testType) && !allowedTestTypes.Contains(testType, StringComparer.OrdinalIgnoreCase))
            {
                problems.Add($"Row {rowNumber}: Test Type '{testType}' is not one of the types ticked under Settings.");
            }

            return problems;
        }

        /// <summary>Debug-only guard on the import rules.</summary>
        [Conditional("DEBUG")]
        public static void SelfCheck()
        {
            var allowed = new List<string> { "4WIRE", "OPEN" };

            Dictionary<string, string> Row(string from = "1", string type = "4WIRE") => new(StringComparer.OrdinalIgnoreCase)
            {
                ["From"] = from, ["To"] = "12", ["Test Type"] = type, ["Lower Limit"] = "0",
                ["Upper Limit"] = "1", ["Tolerance"] = "±5%", ["Expected Value"] = "0.5"
            };

            Debug.Assert(ValidateImportRow(Row(), 2, allowed).Count == 0, "a complete row must pass");
            Debug.Assert(ValidateImportRow(Row(from: " "), 2, allowed).Count == 1, "whitespace must not count as filled in");
            Debug.Assert(ValidateImportRow(Row(type: "PLASMA"), 7, allowed)[0].Contains("Row 7"), "the problem must name its row");
            Debug.Assert(ValidateImportRow(Row(type: "open"), 2, allowed).Count == 0, "test type must match regardless of case");
            Debug.Assert(ValidateImportRow(new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase), 2, allowed).Count
                         == RequiredImportColumns.Length, "a row missing every column must report every column");

            using var workbook = new ClosedXML.Excel.XLWorkbook();
            var sheet = workbook.Worksheets.Add("check");

            sheet.Cell(1, 1).Value = "CONFIG SUMMARY";
            sheet.Cell(2, 1).Value = SummaryConfigName;
            sheet.Cell(2, 2).Value = "Main Harness Test";

            Debug.Assert(FindHeaderRow(sheet) == 0, "a file with only a summary block has no header row");

            for (int i = 0; i < RequiredImportColumns.Length; i++)
            {
                
                sheet.Cell(7, i + 1).Value = RequiredImportColumns[i];
            }

            Debug.Assert(FindHeaderRow(sheet) == 7, "the header must be found below the summary block, not assumed on row 1");
        }

        private void BtnImportExcel_Click(object? sender, EventArgs e)
        {
            if (!CheckEditPermission()) return;

            using var dialog = new OpenFileDialog
            {
                Title = "Import Test Parameters",
                Filter = "Excel workbook (*.xlsx)|*.xlsx|All files (*.*)|*.*"
            };

            if (dialog.ShowDialog(this) != DialogResult.OK) return;

            try
            {
                ImportWorkbook(dialog.FileName);
            }
            catch (Exception ex)
            {
                FileLogService.Error("TestParametersPage.BtnImportExcel_Click", "Could not read the workbook.", ex);
                MessageBox.Show($"Could not read the file: {ex.Message}", "Import",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads a workbook into the grid, or reports every problem and loads nothing.
        /// </summary>
        /// <remarks>
        /// All-or-nothing on purpose: a partial import looks finished, and the operator would run a
        /// harness against a net list quietly missing the rows that failed.
        /// </remarks>
        // The first row that carries the required column names; 0 when the file has none.
        private static int FindHeaderRow(ClosedXML.Excel.IXLWorksheet sheet)
        {
            int lastRow = Math.Min(sheet.LastRowUsed()?.RowNumber() ?? 1, 40);

            for (int r = 1; r <= lastRow; r++)
            {
                var texts = sheet.Row(r).CellsUsed()
                    .Select(c => c.GetString().Trim())
                    .Where(t => t.Length > 0)
                    .ToHashSet(StringComparer.OrdinalIgnoreCase);

                if (RequiredImportColumns.All(texts.Contains)) return r;
            }

            return 0;
        }

        // Pulls the Config Summary block back into the page, when the file carries one.
        private void ReadConfigSummary(ClosedXML.Excel.IXLWorksheet sheet, int headerRowNumber)
        {
            var found = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            for (int r = 1; r < headerRowNumber; r++)
            {
                string label = sheet.Cell(r, 1).GetString().Trim();
                if (label.Length == 0) continue;
                found[label] = sheet.Cell(r, 2).GetString().Trim();
            }

            void Apply(Control box, string label)
            {
                if (found.TryGetValue(label, out string? value) && !string.IsNullOrWhiteSpace(value)) box.Text = value;
            }

            Apply(txtConfigName, SummaryConfigName);
            Apply(txtProductModel, SummaryProductModel);
            Apply(txtConnectionResistance, SummaryConnectionResistance);
            Apply(txtInsulationResistance, SummaryInsulationResistance);
        }

        private void ImportWorkbook(string path)
        {
            using var workbook = new ClosedXML.Excel.XLWorkbook(path);
            var sheet = workbook.Worksheets.First();

            // The header no longer sits on row 1: the Config Summary block is written above it, and
            // an operator may well add notes of their own, so the row is searched for rather than
            // assumed.
            int headerRowNumber = FindHeaderRow(sheet);
            if (headerRowNumber == 0)
            {
                MessageBox.Show(
                    "No header row was found. The file needs a row containing the column names.\r\n\r\n" +
                    "Use Template to get a file with the expected layout.",
                    "Import", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ReadConfigSummary(sheet, headerRowNumber);

            var headerRow = sheet.Row(headerRowNumber);

            // Matched by header text, not position, so a file with the columns rearranged still loads.
            var columnAt = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            for (int c = 1; c <= headerRow.LastCellUsed()?.Address.ColumnNumber; c++)
            {
                string header = headerRow.Cell(c).GetString().Trim();
                if (header.Length > 0 && !columnAt.ContainsKey(header)) columnAt[header] = c;
            }

            var missingHeaders = RequiredImportColumns.Where(h => !columnAt.ContainsKey(h)).ToList();
            if (missingHeaders.Count > 0)
            {
                MessageBox.Show(
                    $"These columns are missing from the file:\r\n\r\n{string.Join("\r\n", missingHeaders)}\r\n\r\n" +
                    "Use Template to get a file with the expected header row.",
                    "Import", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var allowedTestTypes = TestSettingService.GetEnabledTestTypeNames();
            var problems = new List<string>();
            var rows = new List<Dictionary<string, string>>();

            int lastRow = sheet.LastRowUsed()?.RowNumber() ?? headerRowNumber;
            for (int r = headerRowNumber + 1; r <= lastRow; r++)
            {
                var values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                foreach (var (header, index) in columnAt)
                {
                    values[header] = sheet.Cell(r, index).GetString().Trim();
                }

                if (values.Values.All(string.IsNullOrWhiteSpace)) continue;

                problems.AddRange(ValidateImportRow(values, r, allowedTestTypes));
                rows.Add(values);
            }

            if (rows.Count == 0)
            {
                MessageBox.Show("The file has a header row but no data.", "Import",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (problems.Count > 0)
            {
                const int shown = 12;
                string detail = string.Join("\r\n", problems.Take(shown));
                if (problems.Count > shown) detail += $"\r\n... and {problems.Count - shown} more.";

                MessageBox.Show(
                    $"{problems.Count} problem(s) found. Nothing has been imported.\r\n\r\n{detail}",
                    "Import", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (gridConfig.Rows.Count > 0 &&
                MessageBox.Show($"Replace the {gridConfig.Rows.Count} row(s) currently in the grid with {rows.Count} imported row(s)?",
                    "Import", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            gridConfig.Rows.Clear();
            foreach (var values in rows)
            {
                var cells = new object[gridConfig.Columns.Count];
                for (int i = 0; i < gridConfig.Columns.Count; i++)
                {
                    var column = gridConfig.Columns[i];
                    string header = column.HeaderText.Replace("\n", " ");
                    cells[i] = column.Name == "colNo" ? "" : (values.TryGetValue(header, out string? v) ? v : "");
                }

                int idx = gridConfig.Rows.Add(cells);

                // Measurement is not in the file, so it comes from the test type the row declares.
                string rowTestType = gridConfig.Rows[idx].Cells["colTestType"].Value?.ToString() ?? "";
                gridConfig.Rows[idx].Cells["colUnit"].Value = UnitConversionHelper.GetBaseUnit(rowTestType);

                UpdateRowUnitDropdown(idx);
                RecalculateRowLimits(gridConfig.Rows[idx]);
            }

            RenumberRows();
            UpdateTotalNetCount();
            UpdateDiagramSize();
            picPreview.Invalidate();

            MessageBox.Show($"{rows.Count} row(s) imported.", "Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Columns an Excel file never carries: "No." is a display counter, and "Measurement" is the
        // unit the test type already implies. Both are regenerated on import rather than typed in.
        private static readonly string[] NonExportedColumns = { "colNo", "colUnit" };

        private List<DataGridViewColumn> ExportableColumns() =>
            gridConfig.Columns.Cast<DataGridViewColumn>().Where(c => !NonExportedColumns.Contains(c.Name)).ToList();

        // Writes an empty workbook whose header row matches the grid, so an operator filling it in
        // cannot invent a column order the importer would then have to guess at.
        private void BtnTemplate_Click(object? sender, EventArgs e) =>
            WriteWorkbook("test-parameter-template.xlsx", "Template", includeRows: false);

        // The same layout as the template, with the current grid contents under the header.
        private void ExportGridToExcel()
        {
            string suggested = string.IsNullOrWhiteSpace(txtConfigName.Text)
                ? "test-parameters.xlsx"
                : $"{txtConfigName.Text.Trim()}.xlsx";

            WriteWorkbook(suggested, "Export", includeRows: true);
        }

        // Labels used for the Config Summary block, and read back by the importer.
        private const string SummaryConfigName = "Config Name";
        private const string SummaryProductModel = "Product Model";
        private const string SummaryConnectionResistance = "Connection Resistance (mOhm)";
        private const string SummaryInsulationResistance = "Insulation Resistance (MOhm)";

        // Mirrors the Config Summary panel above the grid, and returns the row the table starts on.
        private int WriteConfigSummary(ClosedXML.Excel.IXLWorksheet sheet, int columnCount)
        {
            var title = sheet.Cell(1, 1);
            title.Value = "CONFIG SUMMARY";
            title.Style.Font.Bold = true;
            title.Style.Font.FontSize = 12;
            title.Style.Font.FontColor = ClosedXML.Excel.XLColor.FromArgb(30, 41, 59);
            sheet.Range(1, 1, 1, Math.Max(4, columnCount)).Merge();

            var fields = new (string Label, string Value)[]
            {
                (SummaryConfigName, txtConfigName.Text?.Trim() ?? ""),
                (SummaryProductModel, txtProductModel.Text?.Trim() ?? ""),
                (SummaryConnectionResistance, txtConnectionResistance.Text?.Trim() ?? ""),
                (SummaryInsulationResistance, txtInsulationResistance.Text?.Trim() ?? "")
            };

            int row = 2;
            foreach (var (label, value) in fields)
            {
                var labelCell = sheet.Cell(row, 1);
                labelCell.Value = label;
                labelCell.Style.Font.Bold = true;
                labelCell.Style.Font.FontColor = ClosedXML.Excel.XLColor.FromArgb(71, 85, 105);

                var valueCell = sheet.Cell(row, 2);
                valueCell.Value = value;
                valueCell.Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.FromArgb(248, 250, 252);
                valueCell.Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Hair;
                valueCell.Style.Border.OutsideBorderColor = ClosedXML.Excel.XLColor.FromArgb(203, 213, 225);
                sheet.Range(row, 2, row, 4).Merge();

                row++;
            }

            // One blank row so the summary reads as its own block, then the table header.
            return row + 1;
        }

        private void WriteWorkbook(string suggestedFileName, string caption, bool includeRows)
        {
            using var dialog = new SaveFileDialog
            {
                Title = includeRows ? "Export Test Parameters" : "Save Test Parameter Template",
                Filter = "Excel workbook (*.xlsx)|*.xlsx",
                FileName = suggestedFileName
            };

            if (dialog.ShowDialog(this) != DialogResult.OK) return;

            try
            {
                var columns = ExportableColumns();

                using var workbook = new ClosedXML.Excel.XLWorkbook();
                var sheet = workbook.Worksheets.Add("Test Parameters");

                int headerRow = WriteConfigSummary(sheet, columns.Count);

                for (int i = 0; i < columns.Count; i++)
                {
                    string header = columns[i].HeaderText.Replace("\n", " ");
                    bool required = RequiredImportColumns.Contains(header, StringComparer.OrdinalIgnoreCase);

                    var cell = sheet.Cell(headerRow, i + 1);
                    cell.Value = header;
                    cell.Style.Font.Bold = true;
                    cell.Style.Font.FontColor = ClosedXML.Excel.XLColor.FromArgb(30, 41, 59);
                    cell.Style.Fill.BackgroundColor = required
                        ? ClosedXML.Excel.XLColor.FromArgb(203, 213, 225)
                        : ClosedXML.Excel.XLColor.FromArgb(241, 245, 249);
                    cell.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                    cell.Style.Alignment.Vertical = ClosedXML.Excel.XLAlignmentVerticalValues.Center;
                    cell.Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                    cell.Style.Border.OutsideBorderColor = ClosedXML.Excel.XLColor.FromArgb(148, 163, 184);

                    // The importer rejects the whole file over one blank required cell, so which
                    // columns those are has to be visible in the file itself, not only in a manual.
                    cell.GetComment().AddText(required
                        ? "Required. The import fails if this is empty."
                        : "Optional.");
                }

                if (includeRows)
                {
                    int row = headerRow + 1;
                    foreach (DataGridViewRow gridRow in gridConfig.Rows)
                    {
                        if (gridRow.IsNewRow) continue;

                        for (int i = 0; i < columns.Count; i++)
                        {
                            sheet.Cell(row, i + 1).Value = gridRow.Cells[columns[i].Index].Value?.ToString() ?? "";
                        }
                        row++;
                    }

                    if (row > headerRow + 1)
                    {
                        sheet.Range(headerRow + 1, 1, row - 1, columns.Count)
                            .Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Hair;
                    }
                }

                sheet.Row(headerRow).Height = 22;
                sheet.Columns(1, columns.Count).AdjustToContents(minWidth: 10, maxWidth: 28);
                sheet.SheetView.FreezeRows(headerRow);
                sheet.Range(headerRow, 1, headerRow, columns.Count).SetAutoFilter();
                workbook.SaveAs(dialog.FileName);

                MessageBox.Show($"Saved to:\r\n{dialog.FileName}", caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                FileLogService.Error($"TestParametersPage.WriteWorkbook({caption})", "Could not write the workbook.", ex);
                MessageBox.Show($"Could not save the file: {ex.Message}", caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // The Settings default, but only if the type column can actually hold it - a default that
        // Test Type Available has since switched off would make the new row's cell reject its value.
        private string ResolveDefaultTestType()
        {
            var column = gridConfig.Columns["colTestType"] as DataGridViewComboBoxColumn;
            var offered = column?.Items.Cast<object>().Select(i => i.ToString() ?? "").ToList() ?? new List<string>();

            string preferred = TestSettingService.GetDefaultTestTypeName();
            if (offered.Contains(preferred)) return preferred;

            return offered.FirstOrDefault() ?? "WIRE";
        }

        // Offers only the test types ticked under Settings, falling back to every type if none are.
        public void LoadTestTypeColumnItems()
        {
            var names = TestSettingService.GetEnabledTestTypeNames();
            if (names.Count == 0) names = _configService.GetTestTypeNames();

            if (gridConfig.Columns["colTestType"] is DataGridViewComboBoxColumn testTypeCol)
            {
                testTypeCol.Items.Clear();
                testTypeCol.Items.AddRange(names.Cast<object>().ToArray());
            }
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

        // Action.TestExecution.Run was in the permission matrix from the start but nothing read it,
        // so switching it off changed nothing. Both start paths route through here.
        private bool CheckRunPermission()
        {
            if (SessionManager.HasPermission("Action.TestExecution.Run")) return true;

            MessageBox.Show("You do not have permission to run tests.", "Access Denied",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        private bool CheckEditPermission()
        {
            if (!SessionManager.HasPermission("Action.TestConfig.Edit"))
            {
                MessageBox.Show("You do not have permission to edit test parameter configurations.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            if (!CheckEditPermission()) return;

            int newIdx = gridConfig.Rows.Count + 1;
            string defaultType = ResolveDefaultTestType();
            string defaultUnit = UnitConversionHelper.GetBaseUnit(defaultType);

            int nextFromAddr = GetNextAvailableAddress();
            int nextToAddr = nextFromAddr + 1;

            int idx = gridConfig.Rows.Add(
                newIdx.ToString(), "Elec-Control-001", "", "", "Low Voltage Supply",
                nextFromAddr.ToString(), $"X1-0{newIdx}", nextToAddr.ToString(), $"Y1-0{newIdx}",
                defaultType, "0", "0", "0", "-", defaultUnit);

            UpdateRowUnitDropdown(idx);
            gridConfig.Rows[idx].Cells["colUnit"].Value = defaultUnit;
            RecalculateRowLimits(gridConfig.Rows[idx]);

            RenumberRows();
            UpdateTotalNetCount();
            UpdateDiagramSize();
            picPreview.Invalidate();
        }

        // Next address unused in either column; From and To share one pool.
        private int GetNextAvailableAddress()
        {
            int max = -1;
            foreach (DataGridViewRow row in gridConfig.Rows)
            {
                if (row.IsNewRow) continue;
                if (int.TryParse(row.Cells["colFromAddr"].Value?.ToString(), out int fromVal) && fromVal > max) max = fromVal;
                if (int.TryParse(row.Cells["colToAddr"].Value?.ToString(), out int toVal) && toVal > max) max = toVal;
            }
            return max + 1;
        }

        // Next unused address for one column: the highest existing value plus one.
        private int GetNextAvailableAddress(string columnName)
        {
            int max = -1;
            foreach (DataGridViewRow row in gridConfig.Rows)
            {
                if (row.IsNewRow) continue;
                if (int.TryParse(row.Cells[columnName].Value?.ToString(), out int val) && val > max)
                {
                    max = val;
                }
            }
            return max + 1;
        }

        private void BtnRemove_Click(object? sender, EventArgs e)
        {
            if (!CheckEditPermission()) return;
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
            if (!CheckEditPermission()) return;
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
            if (!CheckEditPermission()) return;
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
                    net.HarnessGroup,
                    net.Color,
                    net.Gauge,
                    net.NetId,
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



            using var dlg = new Views.Modals.ExportOptionsModalForm();
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            if (dlg.ExportToDatabase)
            {
                if (!TrySaveConfigToDatabase()) return;
            }

            if (dlg.ExportToExcel)
            {
                ExportGridToExcel();
            }
        }

        // Returns false when the configuration was rejected or the operator backed out.
        private bool TrySaveConfigToDatabase()
        {
            var config = BuildConfigurationFromGrid(txtConfigName.Text?.Trim() ?? "", txtProductModel.Text?.Trim() ?? "", null);
            if (config == null)
            {
                MessageBox.Show("Failed to build configuration from the grid. Please check entries.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            config.CreatedBy = SessionManager.CurrentUser?.Username ?? "Unknown";

            if (string.IsNullOrWhiteSpace(config.ConfigName))
            {
                MessageBox.Show("Config Name is required.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            var existing = _configService.GetConfigurationByName(config.ConfigName);

            try
            {
                if (existing != null)
                {
                    // Overwrite only what the user explicitly loaded for editing.
                    if (!string.IsNullOrWhiteSpace(_loadedConfigName) && string.Equals(_loadedConfigName, config.ConfigName, StringComparison.OrdinalIgnoreCase))
                    {
                        var res = MessageBox.Show($"Overwrite existing configuration '{config.ConfigName}'?", "Confirm Overwrite", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (res != DialogResult.Yes) return false;

                        _configService.SaveConfig(config);
                        _loadedConfigName = config.ConfigName;
                        MessageBox.Show("Configuration overwritten successfully.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }

                    MessageBox.Show("A configuration with the same name already exists. Load it from Edit to overwrite or choose a different name.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                _configService.SaveConfig(config);
                _loadedConfigName = config.ConfigName;
                MessageBox.Show("Configuration saved successfully.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save configuration:\n{ex.Message}", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
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

        // Checks both columns across all rows, skipping only the cell being edited.
        private bool IsAddressUsedElsewhere(string value, int currentRowIndex, string currentColumnName)
        {
            for (int i = 0; i < gridConfig.Rows.Count; i++)
            {
                var row = gridConfig.Rows[i];
                if (row.IsNewRow) continue;

                string fromVal = row.Cells["colFromAddr"].Value?.ToString()?.Trim() ?? "";
                string toVal = row.Cells["colToAddr"].Value?.ToString()?.Trim() ?? "";

                bool isSameFromCell = i == currentRowIndex && currentColumnName == "colFromAddr";
                if (!isSameFromCell && string.Equals(fromVal, value, StringComparison.OrdinalIgnoreCase))
                    return true;

                bool isSameToCell = i == currentRowIndex && currentColumnName == "colToAddr";
                if (!isSameToCell && string.Equals(toVal, value, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        // Any address used more than once across both columns.
        private List<string> GetDuplicateAddresses()
        {
            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var duplicates = new List<string>();

            foreach (DataGridViewRow row in gridConfig.Rows)
            {
                if (row.IsNewRow) continue;

                string fromVal = row.Cells["colFromAddr"].Value?.ToString()?.Trim() ?? "";
                string toVal = row.Cells["colToAddr"].Value?.ToString()?.Trim() ?? "";

                foreach (var val in new[] { fromVal, toVal })
                {
                    if (string.IsNullOrWhiteSpace(val)) continue;
                    if (!seen.Add(val) && !duplicates.Contains(val, StringComparer.OrdinalIgnoreCase))
                    {
                        duplicates.Add(val);
                    }
                }
            }

            return duplicates;
        }

        // Whether the value already exists in another row of the same column.
        private bool IsAddressDuplicate(string columnName, string value, int currentRowIndex)
        {
            for (int i = 0; i < gridConfig.Rows.Count; i++)
            {
                if (i == currentRowIndex) continue;
                var row = gridConfig.Rows[i];
                if (row.IsNewRow) continue;

                string existing = row.Cells[columnName].Value?.ToString()?.Trim() ?? "";
                if (string.Equals(existing, value, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        // Catches duplicates CellValidating cannot see, such as rows loaded programmatically.
        private List<string> GetDuplicateAddresses(string columnName)
        {
            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var duplicates = new List<string>();

            foreach (DataGridViewRow row in gridConfig.Rows)
            {
                if (row.IsNewRow) continue;
                string val = row.Cells[columnName].Value?.ToString()?.Trim() ?? "";
                if (string.IsNullOrWhiteSpace(val)) continue;

                if (!seen.Add(val) && !duplicates.Contains(val, StringComparer.OrdinalIgnoreCase))
                {
                    duplicates.Add(val);
                }
            }

            return duplicates;
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

            if (ContinuityLikeTypes.Contains(testType, StringComparer.OrdinalIgnoreCase))
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
            return val.ToString("0.############");
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

            foreach (var node in leftNodes.Concat(rightNodes).OrderByDescending(n => n.RowIndices.Any(idx => highlightedRows.Contains(idx))))
            {
                if (IsPointInRect(pt, node.NodeRect) || IsPointInRect(pt, node.LabelRect))
                {
                    bool isHighlighted = node.RowIndices.Any(idx => highlightedRows.Contains(idx));
                    foreach (var idx in node.RowIndices)
                    {
                        if (isHighlighted)
                        {
                            highlightedRows.Remove(idx);
                        }
                        else
                        {
                            highlightedRows.Add(idx);
                        }
                    }
                    picPreview.Invalidate();
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

                var lNode = rawLeftNodes.FirstOrDefault(n => n.Address == fromAddr);
                if (lNode == null)
                {
                    lNode = new DiagramNode
                    {
                        RowIndices = [i],
                        Address = fromAddr,
                        Label = fromLabel,
                        IsLeft = true
                    };
                    rawLeftNodes.Add(lNode);
                }
                else
                {
                    lNode.RowIndices.Add(i);
                    if ((string.IsNullOrEmpty(lNode.Label) || lNode.Label == "NC" || lNode.Label == "Unexpected") && 
                        !string.IsNullOrEmpty(fromLabel) && fromLabel != "NC" && fromLabel != "Unexpected")
                    {
                        lNode.Label = fromLabel;
                    }
                }

                var rNode = rawRightNodes.FirstOrDefault(n => n.Address == toAddr);
                if (rNode == null)
                {
                    rNode = new DiagramNode
                    {
                        RowIndices = [i],
                        Address = toAddr,
                        Label = toLabel,
                        IsLeft = false
                    };
                    rawRightNodes.Add(rNode);
                }
                else
                {
                    rNode.RowIndices.Add(i);
                    if ((string.IsNullOrEmpty(rNode.Label) || rNode.Label == "NC" || rNode.Label == "Unexpected") && 
                        !string.IsNullOrEmpty(toLabel) && toLabel != "NC" && toLabel != "Unexpected")
                    {
                        rNode.Label = toLabel;
                    }
                }
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
                DataGridViewRow row = gridConfig.Rows[i];
                string fromAddr = row.Cells["colFromAddr"].Value?.ToString() ?? "0";
                string toAddr = row.Cells["colToAddr"].Value?.ToString() ?? "0";

                var lNode = leftNodes.FirstOrDefault(n => n.Address == fromAddr);
                var rNode = rightNodes.FirstOrDefault(n => n.Address == toAddr);

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

            using Pen blackPen = new(Color.Black, 3f) { StartCap = LineCap.Round, EndCap = LineCap.Round };
            foreach (var conn in connections)
            {
                if (!highlightedRows.Contains(conn.RowIndex))
                {
                    g.DrawBezier(blackPen, conn.StartPoint, conn.Control1, conn.Control2, conn.EndPoint);
                }
            }

            using Font fontLabel = new("Segoe UI", 9.5F, FontStyle.Bold);
            using Font fontAddr = new("Segoe UI", 10.5F, FontStyle.Bold);
            using StringFormat sfLabel = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoWrap, Trimming = StringTrimming.EllipsisCharacter };
            using StringFormat sfAddr = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.NoClip };

            foreach (var node in leftNodes.Concat(rightNodes))
            {
                if (!node.RowIndices.Any(idx => highlightedRows.Contains(idx)))
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
                if (node.RowIndices.Any(idx => highlightedRows.Contains(idx)))
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
                    TestTypeId = tt?.Id ?? 0,
                    TestType = null,
                    ExpectedValue = expected,
                    LowerLimit = lower,
                    UpperLimit = upper,
                    Tolerance = ParseTolerancePercent(row.Cells["colTolerance"].Value?.ToString()),
                    MeasurementUnit = row.Cells["colUnit"].Value?.ToString() ?? "",
                    Color = row.Cells["colColor"].Value?.ToString() ?? "",
                    Gauge = row.Cells["colGauge"].Value?.ToString() ?? ""
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
        private void ParseAndPopulateGridFromProgramText(string programText, List<string>? allowedTestTypes = null)
        {
            if (string.IsNullOrWhiteSpace(programText)) return;

            gridConfig.Rows.Clear();
            highlightedRows.Clear();

            var lines = programText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                string trimmed = line.Trim();

                // Skip comments, header metadata, parameters, and closing bracket
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

                // Expected format: [SeqNo, FromPin, ToPin]  (e.g., "1", "J1-040", "J3-040")
                if (tokens.Length >= 3 && int.TryParse(tokens[0], out int seqNo))
                {
                    // ponytail: the instrument's learned program only ever describes wire nets, so every
                    // parsed row is WIRE. Widen this once the program text carries a per-row type.
                    const string rowTestType = "WIRE";
                    if (allowedTestTypes != null &&
                        !allowedTestTypes.Contains(rowTestType, StringComparer.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    string fromPin = tokens[1];
                    string toPin = tokens[2];

                    int fromAddr = ParsePinAddress(fromPin);
                    int toAddr = ParsePinAddress(toPin);

                    gridConfig.Rows.Add(
                        seqNo.ToString(),       //No
                        "",                     //Harness Group
                        "",                     //Color
                        "",                     //Gauge
                        "",                     //Net-ID
                        fromAddr.ToString(),    // from
                        "",                     // from label
                        toAddr.ToString(),      // to
                        "",                     // to label
                        rowTestType,            // test type
                        "",                     // lower limit
                        "",                     // upper limit
                        "",                     // expected value
                        "",                     // tolerance
                        "-"
                    );
                }
            }

            RenumberRows();
            UpdateTotalNetCount();
            UpdateDiagramSize();
            picPreview.Invalidate();
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

        private async void btnLearnConnection_Click(object? sender, EventArgs e)
        {
            List<string> learnTestTypes;

            // Show the Learn Connection Modal Options
            using (var modal = new AssemblyLineTester.Views.Modals.LearnConnectionModalForm())
            {
                if (modal.ShowDialog(this) != DialogResult.OK)
                {
                    return; // User cancelled
                }

                learnTestTypes = modal.SelectedTypes
                    .Select(c => c.TestType?.TestTypeName ?? string.Empty)
                    .Where(name => name.Length > 0)
                    .ToList();
                if (learnTestTypes.Count == 0)
                {
                    MessageBox.Show(this, "Select at least one connection type to learn.",
                        "Nothing Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            _testCts?.Cancel();
            _testCts = new System.Threading.CancellationTokenSource();
            var token = _testCts.Token;

            string originalText = btnLearnConnection.Text;
            btnLearnConnection.Text = "Loading.....";
            btnLearnConnection.Enabled = false;
            btnStartAll.Enabled = false;
            btnStartSelected.Enabled = false;
            btnAbort.Enabled = true;


            try
            {
                //bool isConnected = _instrument.Connect();
                //if (!isConnected)
                //{
                //    MessageBox.Show("Failed to connect to instrument.", "Connection Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                var learnResult = await Task.Run(() => _instrument.LearnConnection(), token);
                if (token.IsCancellationRequested) return;

                if (learnResult != CtaDotNet.Enums.CtaCommandResultCode.Success)
                {
                    string errorMessage = "No Error";

                    // Fast loop: check 4 times, wait 300ms each, to beat the race condition 
                    // but not so long that the buffer clears itself!
                    for (int i = 0; i < 4; i++)
                    {
                        await Task.Delay(300, token);
                        var lastErrorObj = _instrument.GetLastError();
                        errorMessage = lastErrorObj?.Result;

                        if (!string.IsNullOrWhiteSpace(errorMessage) && errorMessage != "No Error")
                            break; // Caught it!
                    }

                    // Fallback: If the API completely refuses to give the error, force a generic message
                    if (string.IsNullOrWhiteSpace(errorMessage) || errorMessage == "No Error")
                    {
                        errorMessage = "Hardware Learn Aborted. Please check the physical instrument display for details (e.g., resistance too high).";
                    }

                    Debug.WriteLine(errorMessage);
                    MessageBox.Show(this, $"Failed to execute learn operation on instrument.\n\nResult Code: {learnResult}\nMessage: {errorMessage}", "Learn Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var getTestResult = await Task.Run(() => _instrument.GetLearnedConnection(), token);
                if (token.IsCancellationRequested) return;
                Debug.WriteLine(getTestResult);
                if (getTestResult == null || string.IsNullOrWhiteSpace(getTestResult.Result))
                {

                    MessageBox.Show(this, "Failed to retrieve learned test program text from instrument.", "Learn Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _customTestText = getTestResult.Result;
                Debug.WriteLine(_customTestText);
                ParseAndPopulateGridFromProgramText(_customTestText, learnTestTypes);

                MessageBox.Show(this, "Connection learned and table updated successfully!", "Learn Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                complexConfig = _configService.ConvertToTestConfig(getTestResult.Result);

                if (complexConfig != null)
                {
                    LoadConfigurationIntoGrid(complexConfig);
                }
                string checkResult = _configService.ConvertToProgramText(complexConfig);
                Debug.WriteLine("Convert  = ");
                Debug.WriteLine(checkResult);
            }
            catch (OperationCanceledException)
            {
                // Task was canceled via btnAbort
            }
            finally
            {
                btnLearnConnection.Text = originalText;
                btnLearnConnection.Enabled = true;
                btnStartAll.Enabled = true;
                btnStartSelected.Enabled = true;
                btnAbort.Enabled = false;
            }
        }

        private async void btnStartAll_Click(object? sender, EventArgs e)
        {
            if (!CheckRunPermission()) return;

            _testCts?.Cancel();
            _testCts = new System.Threading.CancellationTokenSource();
            var token = _testCts.Token;

            string originalText = btnStartAll.Text;
            btnStartAll.Text = "Loading.....";
            btnLearnConnection.Enabled = false;
            btnStartAll.Enabled = false;
            btnStartSelected.Enabled = false;
            btnAbort.Enabled = true;

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
                // Task was canceled via btnAbort
            }
            finally
            {
                btnStartAll.Text = originalText;
                btnLearnConnection.Enabled = true;
                btnStartAll.Enabled = true;
                btnStartSelected.Enabled = true;
                btnAbort.Enabled = false;
            }
        }

        private async void btnStartSelected_Click(object? sender, EventArgs e)
        {
            if (!CheckRunPermission()) return;

            _testCts?.Cancel();
            _testCts = new System.Threading.CancellationTokenSource();
            var token = _testCts.Token;

            string originalText = btnStartSelected.Text;
            btnStartSelected.Text = "Loading.....";
            btnLearnConnection.Enabled = false;
            btnStartAll.Enabled = false;
            btnStartSelected.Enabled = false;
            btnAbort.Enabled = true;

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
                // Task was canceled via btnAbort
            }
            finally
            {
                btnStartSelected.Text = originalText;
                btnLearnConnection.Enabled = true;
                btnStartAll.Enabled = true;
                btnStartSelected.Enabled = true;
                btnAbort.Enabled = false;
            }
        }

        private async void btnAbort_Click(object? sender, EventArgs e)
        {
            // 1. Cancel software task execution loop
            _testCts?.Cancel();

            // 2. Send abort command to CTA instrument
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
                        //_instrument.Disconnect();
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

        private async void btnComplex_Click(object sender, EventArgs e)
        {
            // Show the Learn Connection Modal Options
            //using (var modal = new AssemblyLineTester.Views.Modals.LearnConnectionModalForm())
            //{
            //    if (modal.ShowDialog(this) != DialogResult.OK)
            //    {
            //        return; // User cancelled
            //    }

            //    // Store options in local variables as requested
            //    bool learnWire = modal.IncludeWire;
            //    bool learnResistor = modal.IncludeResistor;
            //    bool learnCapacitor = modal.IncludeCapacitor;
            //    bool learnDiode = modal.IncludeDiode;
            //    bool learnTwistedPair = modal.IncludeTwistedPair;
            //}
            InstrumentService.StopMonitoring();
            _testCts?.Cancel();
            _testCts = new System.Threading.CancellationTokenSource();
            var token = _testCts.Token;

            string originalText = btnLearnConnection.Text;
            btnLearnConnection.Text = "Loading.....";
            btnLearnConnection.Enabled = false;
            btnStartAll.Enabled = false;
            btnStartSelected.Enabled = false;
            btnAbort.Enabled = true;


            try
            {
                bool isConnected = _instrument.Connect();
                if (!isConnected)
                {
                    MessageBox.Show("Failed to connect to instrument.", "Connection Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var learnResult = await Task.Run(() => _instrument.LearnComplexConnection("",7), token);
                if (token.IsCancellationRequested) return;

                if (learnResult == CtaDotNet.Enums.CtaCommandResultCode.Success)
                {
                    Debug.WriteLine("complex learn success");
                }
                else
                {
                    Debug.WriteLine("complex learn failed");
                }

                var getTestResult = await Task.Run(() =>  _instrument.GetLearnedConnection());
                if (token.IsCancellationRequested) return;
                Debug.WriteLine(getTestResult);
                Debug.WriteLine(getTestResult.Result);
                if (getTestResult == null || string.IsNullOrWhiteSpace(getTestResult.Result))
                {

                    MessageBox.Show(this, "Failed to retrieve learned test program text from instrument.", "Learn Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                complexConfig = _configService.ConvertToTestConfig(getTestResult.Result);
                Debug.WriteLine("Learned Test Configuration:");
                Debug.WriteLine(complexConfig);

                if (complexConfig != null)
                {
                    LoadConfigurationIntoGrid(complexConfig);
                }
                string checkResult = _configService.ConvertToProgramText(complexConfig);
                Debug.WriteLine("Convert  = ");
                Debug.WriteLine(checkResult);
                //_customTestText = getTestResult.Result;
                //Debug.WriteLine(_customTestText);

                MessageBox.Show(this, "Connection learned and table updated successfully!", "Learn Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OperationCanceledException)
            {
                // Task was canceled via btnAbort
            }
            finally
            {
                InstrumentService.StartMonitoring();
                btnLearnConnection.Text = originalText;
                btnLearnConnection.Enabled = true;
                btnStartAll.Enabled = true;
                btnStartSelected.Enabled = true;
                btnAbort.Enabled = false;
            }
        }
    }
}
