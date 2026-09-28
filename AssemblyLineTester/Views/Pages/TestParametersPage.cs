
using AssemblyLineTester.Models;
using AssemblyLineTester.Models.Dto;
using AssemblyLineTester.Services;
using CtaDotNet.Enums;
using FontAwesome.Sharp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;

namespace AssemblyLineTester.Views.Pages
{
    public partial class TestParametersPage : UserControl
    {
        private readonly HashSet<int> highlightedRows = new();

        private readonly TestConfigService _configService = new TestConfigService();
        private readonly InstrumentService _instrument = new InstrumentService();
        private string _customTestText = string.Empty;
        private System.Threading.CancellationTokenSource? _testCts;
        private string? _loadedConfigName = null;
        private TestConfiguration config = new();
        private DefaultTestSetting defaultTest = TestSettingService.GetDefaults();
        private int defaultTestPoint;
        private string? _lastCellEditValue = null;



        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                AssemblyLineTester.Helpers.UIStyleHelper.ScaleExplicitFonts(this, AssemblyLineTester.Helpers.UIStyleHelper.GetHostScaleFactor(this));
                UpdateTotalNetCount();
                UpdateDiagramSize();
                loadDefaultParameter();
                UpdateDiagram();
            }
        }
        
        private void loadDefaultParameter()
        {
            defaultTest = TestSettingService.GetDefaults();
            defaultTestPoint = defaultTest.TotalTestPointsAvailable;
            txtConnectionResistance.Text = defaultTest.DefaultMaxWireResistance.ToString(System.Globalization.CultureInfo.InvariantCulture);
            txtInsulationResistance.Text = defaultTest.DefaultMinInulationResistance.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }

        private double GetCurrentConnectionResistance()
        {
            if (double.TryParse(txtConnectionResistance.Text?.Trim(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double cr) && cr > 0)
            {
                return Math.Clamp(cr, 0.1, 100.0);
            }
            if (double.TryParse(txtConnectionResistance.Text?.Trim(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out cr) && cr > 0)
            {
                return Math.Clamp(cr, 0.1, 100.0);
            }
            double def = defaultTest?.DefaultMaxWireResistance ?? 0.1;
            return Math.Clamp(def > 0 ? def : 0.1, 0.1, 100.0);
        }

        private void NumericTextBox_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && sender is TextBox txt && txt.Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void ValidateAndClampConnectionResistance()
        {
            string text = txtConnectionResistance.Text.Trim();
            if (string.IsNullOrWhiteSpace(text))
            {
                txtConnectionResistance.Text = "0.1";
                RecalculateAllRows();
                return;
            }

            bool parsed = double.TryParse(text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double val) ||
                          double.TryParse(text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out val);

            const double minVal = 0.1;
            const double maxVal = 100.0;
            string minStr = minVal.ToString("0.##########", System.Globalization.CultureInfo.InvariantCulture);
            string maxStr = maxVal.ToString("0.##########", System.Globalization.CultureInfo.InvariantCulture);

            if (!parsed || val < minVal)
            {
                txtConnectionResistance.Text = minStr;
                RecalculateAllRows();
                MessageBox.Show(
                    $"Continuity resistance must be between {minStr} and {maxStr} Ω.\r\n\r\nAutomatically adjusted to {minStr}.",
                    "Test Parameters", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (val > maxVal)
            {
                txtConnectionResistance.Text = maxStr;
                RecalculateAllRows();
                MessageBox.Show(
                    $"Continuity resistance must be between {minStr} and {maxStr} Ω.\r\n\r\nAutomatically adjusted to {maxStr}.",
                    "Test Parameters", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string formatted = val.ToString(System.Globalization.CultureInfo.InvariantCulture);
                if (text != formatted)
                {
                    txtConnectionResistance.Text = formatted;
                }
            }
        }

        private void SetupEvents()
        {
            gridConfig.CellValueChanged += GridConfig_CellValueChanged;
            gridConfig.CellValidating += GridConfig_CellValidating;
            gridConfig.CellEndEdit += GridConfig_CellEndEdit;
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
                    return;
                }
                if (e.RowIndex >= 0 && e.RowIndex < gridConfig.Rows.Count && e.ColumnIndex >= 0)
                {
                    _lastCellEditValue = gridConfig.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
                }
            };

            txtConnectionResistance.KeyPress += NumericTextBox_KeyPress;
            txtConnectionResistance.Leave += (s, e) => ValidateAndClampConnectionResistance();
            txtConnectionResistance.TextChanged += (s, e) => RecalculateAllRows();
            txtProductModel.DropDown += (s, e) => LoadProductModels();

            picPreview.DiagramRowClicked += PicPreview_DiagramRowClicked;
            pnlPreviewScroll.Resize += (s, e) => { UpdateDiagramSize(); UpdateDiagram(); };
            splitContainerMain.SplitterMoved += (s, e) => { UpdateDiagramSize(); UpdateDiagram(); };

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
            colUpperLimit.ReadOnly = false;
            EnableDoubleBuffering();
            SetupEvents();
            this.VisibleChanged += TestParameterPage_VisibleChanged;

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
                var models = ProductModelService.GetAllProductModels()
                    .Select(p => p.Name)
                    .Where(m => !string.IsNullOrWhiteSpace(m))
                    .Distinct()
                    .OrderBy(m => m)
                    .ToArray();
                string current = txtProductModel.Text;
                txtProductModel.Items.Clear();
                txtProductModel.Items.AddRange(models);
                if (!string.IsNullOrEmpty(current) && txtProductModel.Items.Contains(current))
                {
                    txtProductModel.SelectedItem = current;
                }
                else if (txtProductModel.Items.Count > 0)
                {
                    txtProductModel.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                FileLogService.Error("TestParametersPage.LoadProductModels", "Failed to load product models", ex);
            }
        }
        private void TestParameterPage_VisibleChanged(object? sender, EventArgs e)
        {
            if (this.Visible && !DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                loadDefaultParameter();
                LoadProductModels();
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

        private static string NormalizeHeader(string header) =>
            header.Replace(" ", "").Replace("-", "").Replace("_", "");

        public static string GetImportValue(Dictionary<string, string> values, string targetHeader)
        {
            if (values.TryGetValue(targetHeader, out string? v) && !string.IsNullOrEmpty(v))
                return v;

            string normalizedTarget = NormalizeHeader(targetHeader);
            foreach (var kvp in values)
            {
                if (string.Equals(NormalizeHeader(kvp.Key), normalizedTarget, StringComparison.OrdinalIgnoreCase))
                {
                    return kvp.Value;
                }
            }

            return values.TryGetValue(targetHeader, out string? fallback) ? fallback : "";
        }

        // Everything wrong with one workbook row, named by its spreadsheet row number.
        internal static List<string> ValidateImportRow(Dictionary<string, string> values, int rowNumber, List<string> allowedTestTypes)
        {
            var problems = new List<string>();

            foreach (var required in RequiredImportColumns)
            {
                string value = GetImportValue(values, required);
                if (string.IsNullOrWhiteSpace(value))
                    problems.Add($"Row {rowNumber}: {required} is empty.");
            }

            string testType = GetImportValue(values, "Test Type");
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
                
                sheet.Cell(8, i + 1).Value = RequiredImportColumns[i];
            }

            Debug.Assert(FindHeaderRow(sheet) == 8, "the header must be found below the summary block, not assumed on row 1");
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
        private static int FindHeaderRow(ClosedXML.Excel.IXLWorksheet sheet)
        {
            int lastRow = Math.Min(sheet.LastRowUsed()?.RowNumber() ?? 1, 40);

            for (int r = 1; r <= lastRow; r++)
            {
                var texts = sheet.Row(r).CellsUsed()
                    .Select(c => c.GetString().Trim())
                    .Where(t => t.Length > 0)
                    .ToList();

                bool allFound = RequiredImportColumns.All(req =>
                    texts.Any(t => string.Equals(t, req, StringComparison.OrdinalIgnoreCase) ||
                                   string.Equals(NormalizeHeader(t), NormalizeHeader(req), StringComparison.OrdinalIgnoreCase)));

                if (allFound) return r;
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

            void Apply(Control box, string primaryLabel, string? fallbackLabel = null)
            {
                if (found.TryGetValue(primaryLabel, out string? value) && !string.IsNullOrWhiteSpace(value))
                {
                    box.Text = value;
                }
                else if (fallbackLabel != null && found.TryGetValue(fallbackLabel, out string? fallbackValue) && !string.IsNullOrWhiteSpace(fallbackValue))
                {
                    box.Text = fallbackValue;
                }
            }

            Apply(txtConfigName, SummaryConfigName);
            Apply(txtProductModel, SummaryProductModel);
            Apply(txtConnectionResistance, SummaryConnectionResistance, SummaryConnectionResistanceOld);
            if (found.TryGetValue(SummaryEnableShortTest, out string? shortVal) && !string.IsNullOrWhiteSpace(shortVal))
            {
                chkEnableShortTest.Checked = shortVal.Equals("Yes", StringComparison.OrdinalIgnoreCase) || shortVal.Equals("True", StringComparison.OrdinalIgnoreCase) || shortVal == "1";
            }
            Apply(txtInsulationResistance, SummaryInsulationResistance);
            Apply(txtDescription, SummaryDescription);
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

            var missingHeaders = RequiredImportColumns.Where(req =>
                !columnAt.Keys.Any(k => string.Equals(k, req, StringComparison.OrdinalIgnoreCase) ||
                                       string.Equals(NormalizeHeader(k), NormalizeHeader(req), StringComparison.OrdinalIgnoreCase))).ToList();
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
                    cells[i] = column.Name == "colNo" ? "" : GetImportValue(values, header);
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
            UpdateDiagram();

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
        private const string SummaryConnectionResistance = "Connection Resistance (Ohm)";
        private const string SummaryConnectionResistanceOld = "Connection Resistance (mOhm)";
        private const string SummaryEnableShortTest = "Enable Short Test";
        private const string SummaryInsulationResistance = "Insulation Resistance (MOhm)";
        private const string SummaryDescription = "Description";

        // Mirrors the Config Summary panel above the grid, and returns the row the table starts on.
        private int WriteConfigSummary(ClosedXML.Excel.IXLWorksheet sheet, int columnCount, bool isTemplate = false)
        {
            var title = sheet.Cell(1, 1);
            title.Value = "CONFIG SUMMARY";
            title.Style.Font.Bold = true;
            title.Style.Font.FontSize = 12;
            title.Style.Font.FontColor = ClosedXML.Excel.XLColor.FromArgb(30, 41, 59);
            sheet.Range(1, 1, 1, Math.Max(4, columnCount)).Merge();

            string configName = txtConfigName.Text?.Trim() ?? "";
            string productModel = txtProductModel.Text?.Trim() ?? "";
            string connRes = txtConnectionResistance.Text?.Trim() ?? "";

            if (isTemplate)
            {
                if (string.IsNullOrWhiteSpace(configName)) configName = "Default 6060";
                if (string.IsNullOrWhiteSpace(productModel)) productModel = "6060";
                if (string.IsNullOrWhiteSpace(connRes)) connRes = "10.00";
            }

            var fields = new (string Label, string Value)[]
            {
                (SummaryConfigName, configName),
                (SummaryProductModel, productModel),
                (SummaryConnectionResistance, connRes),
                (SummaryEnableShortTest, chkEnableShortTest.Checked ? "Yes" : "No"),
                (SummaryInsulationResistance, txtInsulationResistance.Text?.Trim() ?? ""),
                (SummaryDescription, txtDescription.Text?.Trim() ?? "")
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
                valueCell.Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
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
                WriteWorkbookToFile(dialog.FileName, includeRows);
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

        public void WriteWorkbookToFile(string filePath, bool includeRows)
        {
            var columns = ExportableColumns();

            using var workbook = new ClosedXML.Excel.XLWorkbook();
            var sheet = workbook.Worksheets.Add("Test Parameters");

            int headerRow = WriteConfigSummary(sheet, columns.Count, isTemplate: !includeRows);

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
            else
            {
                // Add 1 sample row as an example for the template matching Default 6060
                int sampleRow = headerRow + 1;
                for (int i = 0; i < columns.Count; i++)
                {
                    string header = columns[i].HeaderText.Replace("\n", " ").Trim();
                    sheet.Cell(sampleRow, i + 1).Value = GetTemplateSampleValue(header);
                }
                sheet.Range(sampleRow, 1, sampleRow, columns.Count)
                    .Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Hair;
            }

            sheet.Row(headerRow).Height = 22;
            sheet.Columns(1, columns.Count).AdjustToContents(minWidth: 10, maxWidth: 28);
            sheet.SheetView.FreezeRows(headerRow);
            sheet.Range(headerRow, 1, headerRow, columns.Count).SetAutoFilter();
            workbook.SaveAs(filePath);
        }

        public static void GenerateDefaultTemplateFile(string filePath)
        {
            string[] templateHeaders =
            {
                "Harness-Group", "Color", "Gauge", "Net-ID", "From", "Net-From Label", "Net-From Connector", "To",
                "Net-To Label", "Net-To Connector", "Test Type", "Lower Limit", "Upper Limit", "Expected Value", "Tolerance"
            };

            using var workbook = new ClosedXML.Excel.XLWorkbook();
            var sheet = workbook.Worksheets.Add("Test Parameters");

            var title = sheet.Cell(1, 1);
            title.Value = "CONFIG SUMMARY";
            title.Style.Font.Bold = true;
            title.Style.Font.FontSize = 12;
            title.Style.Font.FontColor = ClosedXML.Excel.XLColor.FromArgb(30, 41, 59);
            sheet.Range(1, 1, 1, Math.Max(4, templateHeaders.Length)).Merge();

            var fields = new (string Label, string Value)[]
            {
                (SummaryConfigName, "Default 6060"),
                (SummaryProductModel, "6060"),
                (SummaryConnectionResistance, "10.00"),
                (SummaryEnableShortTest, "Yes"),
                (SummaryInsulationResistance, ""),
                (SummaryDescription, "")
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
                valueCell.Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                valueCell.Style.Border.OutsideBorderColor = ClosedXML.Excel.XLColor.FromArgb(203, 213, 225);
                sheet.Range(row, 2, row, 4).Merge();

                row++;
            }

            int headerRow = row + 1;
            for (int i = 0; i < templateHeaders.Length; i++)
            {
                string header = templateHeaders[i];
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

                cell.GetComment().AddText(required
                    ? "Required. The import fails if this is empty."
                    : "Optional.");
            }

            int sampleRow = headerRow + 1;
            for (int i = 0; i < templateHeaders.Length; i++)
            {
                sheet.Cell(sampleRow, i + 1).Value = GetTemplateSampleValue(templateHeaders[i]);
            }
            sheet.Range(sampleRow, 1, sampleRow, templateHeaders.Length)
                .Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Hair;

            sheet.Row(headerRow).Height = 22;
            sheet.Columns(1, templateHeaders.Length).AdjustToContents(minWidth: 10, maxWidth: 28);
            sheet.SheetView.FreezeRows(headerRow);
            sheet.Range(headerRow, 1, headerRow, templateHeaders.Length).SetAutoFilter();
            workbook.SaveAs(filePath);
        }

        private static string GetTemplateSampleValue(string header)
        {
            return header switch
            {
                "Harness-Group" or "Harness Group" => "CF-C5",
                "Color" => "GN",
                "Gauge" => "18",
                "Net-ID" or "Net ID" => "",
                "From" => "1",
                "Net-From Label" or "Net From Label" => "A525-CF119",
                "Net-From Connector" or "Net From Connector" => "CF119",
                "To" => "163",
                "Net-To Label" or "Net To Label" => "A525-CM101",
                "Net-To Connector" or "Net To Connector" => "CM101",
                "Test Type" => "WIRE",
                "Lower Limit" => "0",
                "Upper Limit" => "10.00",
                "Expected Value" => "-",
                "Tolerance" => "-",
                _ => ""
            };
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
                var existingRowTypes = gridConfig.Rows.Cast<DataGridViewRow>()
                    .Where(r => !r.IsNewRow && r.Cells["colTestType"].Value != null)
                    .Select(r => r.Cells["colTestType"].Value?.ToString() ?? "")
                    .Where(s => !string.IsNullOrWhiteSpace(s));

                var allItems = names.Union(existingRowTypes, StringComparer.OrdinalIgnoreCase).ToList();

                testTypeCol.Items.Clear();
                testTypeCol.Items.AddRange(allItems.Cast<object>().ToArray());
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

            defaultTest = TestSettingService.GetDefaults();
            int newIdx = gridConfig.Rows.Count + 1;
            string defaultType = ResolveDefaultTestType();
            string defaultUnit = UnitConversionHelper.GetBaseUnit(defaultType);
            int tolerance = defaultType == "RESISTOR"
                ? (defaultTest.DefaultResistorTolerance >= 2 && defaultTest.DefaultResistorTolerance <= 93 ? defaultTest.DefaultResistorTolerance : 10)
                : defaultType == "CAPACITOR" ? defaultTest.DefaultCapacitorTolerance : 0;
            double expectedValue = defaultType == "RESISTOR"
                ? (defaultTest.DefaultResistorValue > 0 ? defaultTest.DefaultResistorValue : 100.0)
                : defaultType == "CAPACITOR" ? defaultTest.DefaultCapacitorValue : 5;

            if (defaultType == "RESISTOR")
            {
                double connRes = GetCurrentConnectionResistance();
                double lower = expectedValue * (1.0 - (tolerance / 100.0));
                if (lower < connRes + 1.0)
                {
                    double minVal = Math.Ceiling((connRes + 1.0) / (1.0 - (tolerance / 100.0)));
                    if (minVal <= 500000) expectedValue = minVal;
                }
            }

            double lowerLimit = expectedValue - (tolerance * expectedValue / 100);
            double upperLimit = expectedValue + (tolerance * expectedValue / 100);

            string lowerStr = defaultType == "CAPACITOR" ? FormatCapacitorLimit(lowerLimit) : lowerLimit.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string upperStr = defaultType == "CAPACITOR" ? FormatCapacitorLimit(upperLimit) : upperLimit.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string expStr = defaultType == "CAPACITOR" ? expectedValue.ToString("0.##########", System.Globalization.CultureInfo.InvariantCulture) : expectedValue.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string tolStr = defaultType == "CAPACITOR" ? $"±{tolerance}%" : (defaultType == "RESISTOR" ? $"±{tolerance}%" : tolerance.ToString());

            if (defaultType == "SIGNATURE WIRE")
            {
                expStr = "-";
                tolStr = "-";
                lowerStr = "0";
                upperStr = GetCurrentConnectionResistance().ToString(System.Globalization.CultureInfo.InvariantCulture);
            }

            int nextFromAddr = GetNextAvailableAddress();
            int nextToAddr = nextFromAddr + defaultTestPoint/2;
            int idx = gridConfig.Rows.Add(
                newIdx.ToString(), 
                "",                             // harness group
                "",                             // color
                "",                             // gauge
                "",                             // Net-ID
                nextFromAddr.ToString(),                                    // From
                "J" + ((nextFromAddr - 1) / 64 + 1).ToString() + "-" + ((nextFromAddr - 1) % 64 + 1).ToString("D3"),                   // Net-From Label
                "J" + ((nextFromAddr - 1) / 64 + 1).ToString(),                                                                         // Net-From Connector
                nextToAddr.ToString(),                                      // To
                "J" + ((nextToAddr - 1) / 64 + 1).ToString() + "-" + ((nextToAddr - 1) % 64 + 1).ToString("D3"),                     // Net-To Label
                "J" + ((nextToAddr - 1) / 64 + 1).ToString(),                                                                         // Net-To Connector
                defaultType,                                                // Test Type
                lowerStr,                                                   // Lower Limit
                upperStr,                                                   // Upper Limit
                expStr,                                                     // Expected Value
                tolStr,                                                     // Tolerance
                defaultUnit);                                               // Measurement

            UpdateRowUnitDropdown(idx);
            gridConfig.Rows[idx].Cells["colUnit"].Value = defaultUnit;
            RecalculateRowLimits(gridConfig.Rows[idx]);
            if (defaultType == "RESISTOR")
            {
                CheckAndClampSignatureWires();
            }

            RenumberRows();
            UpdateTotalNetCount();
            UpdateDiagramSize();
            UpdateDiagram();
        }

        // Next address unused in either column; From and To share one pool.
        private int GetNextAvailableAddress()
        {
            int max = -0;
            foreach (DataGridViewRow row in gridConfig.Rows)
            {
                if (row.IsNewRow) continue;
                if (int.TryParse(row.Cells["colFromAddr"].Value?.ToString(), out int fromVal) && fromVal > max) max = fromVal;
                //if (int.TryParse(row.Cells["colToAddr"].Value?.ToString(), out int toVal) && toVal > max) max = toVal;
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
            UpdateDiagram();
        }

        private void BtnReset_Click(object? sender, EventArgs e)
        {
            if (!CheckEditPermission()) return;
            if (MessageBox.Show("Are you sure you want to delete all rows and reset the configuration?", "Confirm Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                gridConfig.Rows.Clear();
                highlightedRows.Clear();
                txtConfigName.Text = "";
                txtProductModel.SelectedIndex = -1;
                txtProductModel.Text = "";
                txtConnectionResistance.Text = "";
                txtInsulationResistance.Text = "";
                chkEnableShortTest.Checked = true;
                txtDescription.Text = "";
                _loadedConfigName = null;
                loadDefaultParameter();
                UpdateTotalNetCount();
                UpdateDiagramSize();
                UpdateDiagram();
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

            txtConfigName.Text = config.ConfigName ?? "";
            txtProductModel.Text = config.ProductModelName ?? "";
            txtConnectionResistance.Text = config.MaxWireResistance > 0 
                ? config.MaxWireResistance.ToString(System.Globalization.CultureInfo.InvariantCulture) 
                : "";
            txtInsulationResistance.Text = config.MinInsulationResistance > 0 
                ? config.MinInsulationResistance.ToString(System.Globalization.CultureInfo.InvariantCulture) 
                : "";
            chkEnableShortTest.Checked = config.ShortTest;
            txtDescription.Text = config.Description ?? "";

            foreach (var net in config.Nets.OrderBy(n => n.SequenceNo))
            {
                string testTypeName = net.TestType?.TestTypeName ?? "";
                bool isCapacitor = testTypeName.Equals("CAPACITOR", StringComparison.OrdinalIgnoreCase);
                bool isDiode = testTypeName.Equals("DIODE", StringComparison.OrdinalIgnoreCase);

                if (gridConfig.Columns["colTestType"] is DataGridViewComboBoxColumn testTypeCol)
                {
                    if (!string.IsNullOrWhiteSpace(testTypeName) && !testTypeCol.Items.Contains(testTypeName))
                    {
                        testTypeCol.Items.Add(testTypeName);
                    }
                }

                string expected;
                string tolerance;
                string lowerStr;
                string upperStr;

                if (isDiode)
                {
                    double fwdExp = net.ExpectedValue ?? 0.70;
                    double revExp = net.ExpectedValue2 ?? 6.00;
                    double fwdTol = net.Tolerance ?? 20.0;
                    double revTol = net.Tolerance2 ?? 20.0;
                    double fwdLower = net.LowerLimit ?? (fwdExp * (1.0 - fwdTol / 100.0));
                    double fwdUpper = net.UpperLimit ?? (fwdExp * (1.0 + fwdTol / 100.0));
                    double revLower = net.LowerLimit2 ?? (revExp * (1.0 - revTol / 100.0));
                    double revUpper = net.UpperLimit2 ?? revExp;

                    expected = $"{FormatDiodeValue(fwdExp)}, > {FormatDiodeValue(revExp)}";
                    tolerance = $"±{fwdTol.ToString("0", System.Globalization.CultureInfo.InvariantCulture)}%, ±{revTol.ToString("0", System.Globalization.CultureInfo.InvariantCulture)}%";
                    lowerStr = $"{FormatDiodeValue(fwdLower)}, > {FormatDiodeValue(revLower)}";
                    upperStr = $"{FormatDiodeValue(fwdUpper)}, > {FormatDiodeValue(revUpper)}";
                }
                else
                {
                    expected = net.ExpectedValue.HasValue 
                        ? (isCapacitor ? FormatCapacitorLimit(net.ExpectedValue.Value) : FormatLimit(net.ExpectedValue.Value)) 
                        : "-";
                    tolerance = net.Tolerance.HasValue ? $"±{net.Tolerance.Value.ToString("0", System.Globalization.CultureInfo.InvariantCulture)}%" : "±0%";
                    lowerStr = isCapacitor ? FormatCapacitorLimit(net.LowerLimit ?? 0) : FormatLimit(net.LowerLimit ?? 0);
                    upperStr = isCapacitor ? FormatCapacitorLimit(net.UpperLimit ?? 0) : FormatLimit(net.UpperLimit ?? 0);
                }

                int idx = gridConfig.Rows.Add(
                    net.SequenceNo.ToString(),
                    net.HarnessGroup,
                    net.Color,
                    net.Gauge,
                    net.NetId,
                    net.FromAddress.ToString(),
                    net.FromLabel,
                    TestDetailDto.ExtractConnector(net.FromLabel, net.FromAddress.ToString()),
                    net.ToAddress.ToString(),
                    net.ToLabel,
                    TestDetailDto.ExtractConnector(net.ToLabel, net.ToAddress.ToString()),
                    testTypeName,
                    lowerStr,
                    upperStr,
                    expected,
                    tolerance,
                    isDiode ? (string.IsNullOrEmpty(net.MeasurementUnit) ? "V" : net.MeasurementUnit) : (net.MeasurementUnit ?? ""));

                UpdateRowUnitDropdown(idx);
                gridConfig.Rows[idx].Cells["colUnit"].Value = isDiode ? (string.IsNullOrEmpty(net.MeasurementUnit) ? "V" : net.MeasurementUnit) : (net.MeasurementUnit ?? "");
                RecalculateRowLimits(gridConfig.Rows[idx]);
            }

            RenumberRows();
            UpdateTotalNetCount();
            UpdateDiagramSize();
            UpdateDiagram();

            _loadedConfigName = config.ConfigName;
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
            List<TestType> presentTestTypes = GetSelectedTestTypesFromGrid();
            //foreach (var tt in presentTestTypes)
            //{
            //    Debug.WriteLine(tt.TestTypeName);
            //    Debug.WriteLine(tt.TestTypeValue);
            //}

            var config = BuildConfigurationFromGrid(txtConfigName.Text?.Trim() ?? "", txtProductModel.Text?.Trim() ?? "", txtDescription.Text?.Trim() ?? "");
            if (config == null)
            {
                MessageBox.Show("Failed to build configuration from the grid. Please check entries.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            config.TestTypes = presentTestTypes;
            config.CreatedBy = SessionManager.CurrentUser?.Username ?? "Unknown";

            if (string.IsNullOrWhiteSpace(config.ConfigName))
            {
                MessageBox.Show("Config Name is required.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(config.ProductModelName))
            {
                MessageBox.Show("Product Model is required.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            UpdateDiagram();
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
                    Debug.WriteLine(e.ColumnIndex);
                    UpdateRowUnitDropdown(e.RowIndex);
                    
                    string selectedType = gridConfig.Rows[e.RowIndex].Cells["colTestType"].Value?.ToString() ?? "";
                    if (selectedType.Equals("CAPACITOR", StringComparison.OrdinalIgnoreCase))
                    {
                        defaultTest = TestSettingService.GetDefaults();
                        gridConfig.Rows[e.RowIndex].Cells["colExpectedValue"].Value = defaultTest.DefaultCapacitorValue.ToString("0.##########", System.Globalization.CultureInfo.InvariantCulture);
                        gridConfig.Rows[e.RowIndex].Cells["colTolerance"].Value = $"±{defaultTest.DefaultCapacitorTolerance}%";
                        gridConfig.Rows[e.RowIndex].Cells["colUnit"].Value = "F";
                    }
                    else if (selectedType.Equals("RESISTOR", StringComparison.OrdinalIgnoreCase))
                    {
                        defaultTest = TestSettingService.GetDefaults();
                        double defVal = defaultTest.DefaultResistorValue > 0 ? defaultTest.DefaultResistorValue : 100.0;
                        int defTol = (defaultTest.DefaultResistorTolerance >= 2 && defaultTest.DefaultResistorTolerance <= 93) ? defaultTest.DefaultResistorTolerance : 10;
                        double connRes = GetCurrentConnectionResistance();
                        double lower = defVal * (1.0 - (defTol / 100.0));
                        if (lower < connRes + 1.0)
                        {
                            double minVal = Math.Ceiling((connRes + 1.0) / (1.0 - (defTol / 100.0)));
                            if (minVal <= 500000) defVal = minVal;
                        }
                        gridConfig.Rows[e.RowIndex].Cells["colExpectedValue"].Value = defVal.ToString(System.Globalization.CultureInfo.InvariantCulture);
                        gridConfig.Rows[e.RowIndex].Cells["colTolerance"].Value = $"±{defTol}%";
                        gridConfig.Rows[e.RowIndex].Cells["colUnit"].Value = "Ω";
                    }
                    else if (selectedType.Equals("SIGNATURE WIRE", StringComparison.OrdinalIgnoreCase))
                    {
                        double connRes = GetCurrentConnectionResistance();
                        gridConfig.Rows[e.RowIndex].Cells["colExpectedValue"].Value = "-";
                        gridConfig.Rows[e.RowIndex].Cells["colTolerance"].Value = "-";
                        gridConfig.Rows[e.RowIndex].Cells["colLowerLimit"].Value = "0";
                        gridConfig.Rows[e.RowIndex].Cells["colUpperLimit"].Value = connRes.ToString(System.Globalization.CultureInfo.InvariantCulture);
                        gridConfig.Rows[e.RowIndex].Cells["colUnit"].Value = "Ω";
                    }
                    else if (selectedType.Equals("DIODE", StringComparison.OrdinalIgnoreCase))
                    {
                        gridConfig.Rows[e.RowIndex].Cells["colExpectedValue"].Value = "0.72, > 6.00";
                        gridConfig.Rows[e.RowIndex].Cells["colTolerance"].Value = "±20%, ±20%";
                        gridConfig.Rows[e.RowIndex].Cells["colUnit"].Value = "V";
                    }
                    else
                    {
                        gridConfig.Rows[e.RowIndex].Cells["colExpectedValue"].Value = "0";
                        gridConfig.Rows[e.RowIndex].Cells["colTolerance"].Value = "0";
                    }
                    RecalculateRowLimits(gridConfig.Rows[e.RowIndex]);
                    if (selectedType.Equals("RESISTOR", StringComparison.OrdinalIgnoreCase))
                    {
                        CheckAndClampSignatureWires();
                    }
                }
                else if ((colExpected != null && e.ColumnIndex == colExpected.Index) ||
                         (colTolerance != null && e.ColumnIndex == colTolerance.Index))
                {
                    RecalculateRowLimits(gridConfig.Rows[e.RowIndex]);
                }
            }
            UpdateTotalNetCount();
            UpdateDiagramSize();
            UpdateDiagram();
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
                if (testType.Equals("CAPACITOR", StringComparison.OrdinalIgnoreCase)) return;
                if (testType.Equals("RESISTOR", StringComparison.OrdinalIgnoreCase)) return;
                if (testType.Equals("DIODE", StringComparison.OrdinalIgnoreCase)) return;

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

        private void GridConfig_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= gridConfig.Rows.Count) return;
            var row = gridConfig.Rows[e.RowIndex];
            if (row.IsNewRow) return;

            string testType = row.Cells["colTestType"].Value?.ToString() ?? "";

            if (testType.Equals("DIODE", StringComparison.OrdinalIgnoreCase))
            {
                RecalculateRowLimits(row);
                return;
            }

            if (testType.Equals("RESISTOR", StringComparison.OrdinalIgnoreCase))
            {
                var colExpected = gridConfig.Columns["colExpectedValue"];
                var colTolerance = gridConfig.Columns["colTolerance"];

                if (colExpected != null && e.ColumnIndex == colExpected.Index)
                {
                    string text = row.Cells["colExpectedValue"].Value?.ToString()?.Trim() ?? "";
                    bool parsed = double.TryParse(text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double val) ||
                                  double.TryParse(text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out val);

                    if (!parsed || val <= 0)
                    {
                        RevertResistorExpectedValue(row, e.RowIndex);
                        MessageBox.Show(
                            $"Row {e.RowIndex + 1}: Resistor value must be a positive number greater than 0.\r\n\r\nReverted to previous value.",
                            "Test Parameters", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (val > 500000)
                    {
                        RevertResistorExpectedValue(row, e.RowIndex);
                        MessageBox.Show(
                            $"Row {e.RowIndex + 1}: Resistor value cannot exceed 500,000 Ω (500k).\r\n\r\nReverted to previous value.",
                            "Test Parameters", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string tolText = row.Cells["colTolerance"].Value?.ToString() ?? "";
                    double? tolVal = ParseTolerancePercent(tolText);
                    double tolPct = (tolVal.HasValue && tolVal.Value >= 2 && tolVal.Value <= 93)
                        ? tolVal.Value
                        : (defaultTest.DefaultResistorTolerance >= 2 && defaultTest.DefaultResistorTolerance <= 93 ? defaultTest.DefaultResistorTolerance : 10);

                    double lower = val * (1.0 - (Math.Abs(tolPct) / 100.0));
                    double connRes = GetCurrentConnectionResistance();
                    if (lower < connRes + 1.0 - 1e-6)
                    {
                        RevertResistorExpectedValue(row, e.RowIndex);
                        MessageBox.Show(
                            $"Row {e.RowIndex + 1}: Resistor lower limit ({lower:F2} Ω) must be at least 1 Ω above continuity resistance ({connRes:F2} Ω).\r\n\r\nReverted to previous value.",
                            "Test Parameters", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string formatted = val.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    if (text != formatted)
                    {
                        row.Cells["colExpectedValue"].Value = formatted;
                    }
                    RecalculateRowLimits(row);
                    CheckAndClampSignatureWires();
                }
                else if (colTolerance != null && e.ColumnIndex == colTolerance.Index)
                {
                    string text = row.Cells["colTolerance"].Value?.ToString()?.Trim() ?? "";
                    string clean = text.Replace("±", "").Replace("+", "").Replace("%", "").Replace("-", "").Trim();
                    bool parsed = double.TryParse(clean, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double val) ||
                                  double.TryParse(clean, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out val);

                    const int minTol = 2;
                    const int maxTol = 93;

                    if (!parsed || val < minTol || val > maxTol)
                    {
                        RevertResistorTolerance(row, e.RowIndex);
                        MessageBox.Show(
                            $"Row {e.RowIndex + 1}: Resistor tolerance must be between {minTol}% and {maxTol}%.\r\n\r\nReverted to previous value.",
                            "Test Parameters", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string expText = row.Cells["colExpectedValue"].Value?.ToString() ?? "";
                    if (!double.TryParse(expText, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double expVal) || expVal <= 0)
                    {
                        double.TryParse(expText, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out expVal);
                    }

                    double lower = expVal * (1.0 - (val / 100.0));
                    double connRes = GetCurrentConnectionResistance();
                    if (lower < connRes + 1.0 - 1e-6)
                    {
                        RevertResistorTolerance(row, e.RowIndex);
                        MessageBox.Show(
                            $"Row {e.RowIndex + 1}: Resistor lower limit ({lower:F2} Ω) must be at least 1 Ω above continuity resistance ({connRes:F2} Ω).\r\n\r\nReverted to previous value.",
                            "Test Parameters", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int intVal = (int)Math.Round(val);
                    string formatted = $"±{intVal}%";
                    if (text != formatted)
                    {
                        row.Cells["colTolerance"].Value = formatted;
                    }
                    RecalculateRowLimits(row);
                    CheckAndClampSignatureWires();
                }
                return;
            }

            if (testType.Equals("SIGNATURE WIRE", StringComparison.OrdinalIgnoreCase))
            {
                var colUpper = gridConfig.Columns["colUpperLimit"];
                if (colUpper != null && e.ColumnIndex == colUpper.Index)
                {
                    string text = row.Cells["colUpperLimit"].Value?.ToString()?.Trim() ?? "";
                    bool parsed = double.TryParse(text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double val) ||
                                  double.TryParse(text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out val);

                    double maxAllowed = GetMaxAllowedSignatureWireUpperLimit();
                    const double minAllowed = 0.1;

                    if (!parsed || val < minAllowed - 1e-6)
                    {
                        RevertSignatureWireUpperLimit(row, e.RowIndex);
                        MessageBox.Show(
                            $"Row {e.RowIndex + 1}: Signature Wire upper limit must be at least {minAllowed.ToString(System.Globalization.CultureInfo.InvariantCulture)} Ω.\r\n\r\nReverted to previous value.",
                            "Test Parameters", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (val > maxAllowed + 1e-6)
                    {
                        RevertSignatureWireUpperLimit(row, e.RowIndex);
                        string reason = (maxAllowed < 500000.0)
                            ? $"cannot exceed {maxAllowed:F2} Ω (at least 1 Ω below the smallest resistor lower limit of {maxAllowed + 1.0:F2} Ω)."
                            : "cannot exceed 500,000 Ω (500k).";
                        MessageBox.Show(
                            $"Row {e.RowIndex + 1}: Signature Wire upper limit {reason}\r\n\r\nReverted to previous value.",
                            "Test Parameters", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string formatted = val.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    if (text != formatted)
                    {
                        row.Cells["colUpperLimit"].Value = formatted;
                    }
                }
                return;
            }

            if (!testType.Equals("CAPACITOR", StringComparison.OrdinalIgnoreCase)) return;

            var colCapExpected = gridConfig.Columns["colExpectedValue"];
            var colCapTolerance = gridConfig.Columns["colTolerance"];

            if (colCapExpected != null && e.ColumnIndex == colCapExpected.Index)
            {
                string text = row.Cells["colExpectedValue"].Value?.ToString()?.Trim() ?? "";
                bool parsed = double.TryParse(text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double val);

                const double minVal = 0.0000000001;
                const double maxVal = 0.001;
                string minStr = minVal.ToString("0.##########", System.Globalization.CultureInfo.InvariantCulture);
                string maxStr = maxVal.ToString("0.##########", System.Globalization.CultureInfo.InvariantCulture);

                if (!parsed || val < minVal)
                {
                    row.Cells["colExpectedValue"].Value = minStr;
                    RecalculateRowLimits(row);
                    MessageBox.Show(
                        $"Row {e.RowIndex + 1}: Capacitor value must be between {minStr} and {maxStr}.\r\n\r\nAutomatically adjusted to {minStr}.",
                        "Test Parameters", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (val > maxVal)
                {
                    row.Cells["colExpectedValue"].Value = maxStr;
                    RecalculateRowLimits(row);
                    MessageBox.Show(
                        $"Row {e.RowIndex + 1}: Capacitor value must be between {minStr} and {maxStr}.\r\n\r\nAutomatically adjusted to {maxStr}.",
                        "Test Parameters", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string formatted = val.ToString("0.##########", System.Globalization.CultureInfo.InvariantCulture);
                    if (text != formatted)
                    {
                        row.Cells["colExpectedValue"].Value = formatted;
                        RecalculateRowLimits(row);
                    }
                }
            }
            else if (colCapTolerance != null && e.ColumnIndex == colCapTolerance.Index)
            {
                string text = row.Cells["colTolerance"].Value?.ToString()?.Trim() ?? "";
                string clean = text.Replace("±", "").Replace("+", "").Replace("%", "").Replace("-", "").Trim();
                bool parsed = double.TryParse(clean, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double val) ||
                              double.TryParse(clean, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out val);

                const int minTol = 10;
                const int maxTol = 99;

                if (!parsed || val < minTol)
                {
                    row.Cells["colTolerance"].Value = $"±{minTol}%";
                    RecalculateRowLimits(row);
                    MessageBox.Show(
                        $"Row {e.RowIndex + 1}: Capacitor tolerance must be between {minTol}% and {maxTol}%.\r\n\r\nAutomatically adjusted to ±{minTol}%.",
                        "Test Parameters", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (val > maxTol)
                {
                    row.Cells["colTolerance"].Value = $"±{maxTol}%";
                    RecalculateRowLimits(row);
                    MessageBox.Show(
                        $"Row {e.RowIndex + 1}: Capacitor tolerance must be between {minTol}% and {maxTol}%.\r\n\r\nAutomatically adjusted to ±{maxTol}%.",
                        "Test Parameters", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    int intVal = (int)Math.Round(val);
                    string formatted = $"±{intVal}%";
                    if (text != formatted)
                    {
                        row.Cells["colTolerance"].Value = formatted;
                        RecalculateRowLimits(row);
                    }
                }
            }
        }

        private void RevertResistorExpectedValue(DataGridViewRow row, int rowIndex)
        {
            double connRes = GetCurrentConnectionResistance();
            string? candidate = _lastCellEditValue?.Trim();
            bool candidateValid = false;
            if (!string.IsNullOrWhiteSpace(candidate) &&
                (double.TryParse(candidate, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double prevVal) ||
                 double.TryParse(candidate, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out prevVal)))
            {
                if (prevVal > 0 && prevVal <= 500000)
                {
                    double? tolVal = ParseTolerancePercent(row.Cells["colTolerance"].Value?.ToString());
                    double tolPct = (tolVal.HasValue && tolVal.Value >= 2 && tolVal.Value <= 93) ? tolVal.Value : 10;
                    double lower = prevVal * (1.0 - (tolPct / 100.0));
                    if (lower >= connRes + 1.0 - 1e-6)
                    {
                        candidateValid = true;
                        row.Cells["colExpectedValue"].Value = prevVal.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
            }

            if (!candidateValid)
            {
                defaultTest = TestSettingService.GetDefaults();
                double fallbackVal = defaultTest.DefaultResistorValue > 0 ? defaultTest.DefaultResistorValue : 100.0;
                int defTol = (defaultTest.DefaultResistorTolerance >= 2 && defaultTest.DefaultResistorTolerance <= 93) ? defaultTest.DefaultResistorTolerance : 10;
                double lower = fallbackVal * (1.0 - (defTol / 100.0));
                if (lower < connRes + 1.0)
                {
                    double minRequired = Math.Ceiling((connRes + 1.0) / (1.0 - (defTol / 100.0)));
                    if (minRequired <= 500000) fallbackVal = minRequired;
                }
                row.Cells["colExpectedValue"].Value = fallbackVal.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }
            RecalculateRowLimits(row);
        }

        private void RevertResistorTolerance(DataGridViewRow row, int rowIndex)
        {
            double connRes = GetCurrentConnectionResistance();
            string? candidate = _lastCellEditValue?.Trim();
            bool candidateValid = false;
            if (!string.IsNullOrWhiteSpace(candidate))
            {
                double? prevTol = ParseTolerancePercent(candidate);
                if (prevTol.HasValue && prevTol.Value >= 2 && prevTol.Value <= 93)
                {
                    string expText = row.Cells["colExpectedValue"].Value?.ToString() ?? "";
                    if (double.TryParse(expText, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double expVal) && expVal > 0)
                    {
                        double lower = expVal * (1.0 - (prevTol.Value / 100.0));
                        if (lower >= connRes + 1.0 - 1e-6)
                        {
                            candidateValid = true;
                            row.Cells["colTolerance"].Value = $"±{(int)Math.Round(prevTol.Value)}%";
                        }
                    }
                }
            }

            if (!candidateValid)
            {
                defaultTest = TestSettingService.GetDefaults();
                int defTol = (defaultTest.DefaultResistorTolerance >= 2 && defaultTest.DefaultResistorTolerance <= 93) ? defaultTest.DefaultResistorTolerance : 10;
                row.Cells["colTolerance"].Value = $"±{defTol}%";
            }
            RecalculateRowLimits(row);
        }

        private double? GetResistorLowerLimit(DataGridViewRow row)
        {
            if (row.IsNewRow) return null;
            string type = row.Cells["colTestType"].Value?.ToString() ?? "";
            if (!type.Equals("RESISTOR", StringComparison.OrdinalIgnoreCase)) return null;

            string lowerStr = row.Cells["colLowerLimit"].Value?.ToString() ?? "";
            if ((double.TryParse(lowerStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double lowerVal) ||
                 double.TryParse(lowerStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out lowerVal)) && lowerVal > 0)
            {
                return lowerVal;
            }

            string expStr = row.Cells["colExpectedValue"].Value?.ToString() ?? "";
            if ((double.TryParse(expStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double expVal) ||
                 double.TryParse(expStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out expVal)) && expVal > 0)
            {
                double? tolVal = ParseTolerancePercent(row.Cells["colTolerance"].Value?.ToString());
                double tolPct = (tolVal.HasValue && tolVal.Value >= 2 && tolVal.Value <= 93) ? tolVal.Value : 10;
                return expVal * (1.0 - (tolPct / 100.0));
            }
            return null;
        }

        private double GetMaxAllowedSignatureWireUpperLimit()
        {
            double minResistorLower = double.MaxValue;
            bool hasResistor = false;

            foreach (DataGridViewRow row in gridConfig.Rows)
            {
                if (row.IsNewRow) continue;
                double? lower = GetResistorLowerLimit(row);
                if (lower.HasValue)
                {
                    hasResistor = true;
                    if (lower.Value < minResistorLower)
                    {
                        minResistorLower = lower.Value;
                    }
                }
            }

            if (hasResistor)
            {
                // Must be at least 1 Ω below the smallest resistor lower limit
                return Math.Max(0.1, minResistorLower - 1.0);
            }
            else
            {
                return 500000.0;
            }
        }

        private void RevertSignatureWireUpperLimit(DataGridViewRow row, int rowIndex)
        {
            double maxAllowed = GetMaxAllowedSignatureWireUpperLimit();
            string? candidate = _lastCellEditValue?.Trim();
            bool candidateValid = false;
            if (!string.IsNullOrWhiteSpace(candidate) &&
                (double.TryParse(candidate, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double prevVal) ||
                 double.TryParse(candidate, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out prevVal)))
            {
                if (prevVal >= 0.1 - 1e-6 && prevVal <= maxAllowed + 1e-6)
                {
                    candidateValid = true;
                    row.Cells["colUpperLimit"].Value = prevVal.ToString(System.Globalization.CultureInfo.InvariantCulture);
                }
            }

            if (!candidateValid)
            {
                double fallbackVal = GetCurrentConnectionResistance();
                if (fallbackVal > maxAllowed) fallbackVal = maxAllowed;
                if (fallbackVal < 0.1) fallbackVal = 0.1;
                row.Cells["colUpperLimit"].Value = fallbackVal.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        private void CheckAndClampSignatureWires()
        {
            double maxAllowed = GetMaxAllowedSignatureWireUpperLimit();
            var clampedRows = new List<int>();

            for (int i = 0; i < gridConfig.Rows.Count; i++)
            {
                var row = gridConfig.Rows[i];
                if (row.IsNewRow) continue;

                string testType = row.Cells["colTestType"].Value?.ToString() ?? "";
                if (!testType.Equals("SIGNATURE WIRE", StringComparison.OrdinalIgnoreCase)) continue;

                string upperStr = row.Cells["colUpperLimit"].Value?.ToString()?.Trim() ?? "";
                if (double.TryParse(upperStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double currentUpper) ||
                    double.TryParse(upperStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out currentUpper))
                {
                    if (currentUpper > maxAllowed + 1e-6)
                    {
                        row.Cells["colUpperLimit"].Value = FormatLimit(maxAllowed);
                        clampedRows.Add(i + 1);
                    }
                }
            }

            if (clampedRows.Count > 0)
            {
                string rowsText = string.Join(", ", clampedRows.Select(r => $"Row {r}"));
                string reason = (maxAllowed < 500000.0)
                    ? $"exceeded the allowed maximum ({maxAllowed:F2} Ω, at least 1 Ω below the smallest resistor lower limit of {maxAllowed + 1.0:F2} Ω)."
                    : $"exceeded 500,000 Ω (500k).";
                MessageBox.Show(
                    $"{rowsText}: Signature Wire upper limit {reason}\r\n\r\nAutomatically adjusted to {maxAllowed:F2} Ω.",
                    "Test Parameters", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            CheckAndClampSignatureWires();
        }

        private void RecalculateRowLimits(DataGridViewRow row)
        {
            if (row == null || row.IsNewRow) return;

            string testType = row.Cells["colTestType"].Value?.ToString() ?? "";


            if (testType == "WIRE")
            {
                row.Cells["colExpectedValue"].Value = "-";
                row.Cells["colExpectedValue"].ReadOnly = true;

                row.Cells["colTolerance"].Value = "-";
                row.Cells["colTolerance"].ReadOnly = true;

                row.Cells["colLowerLimit"].Value = "0";
                row.Cells["colLowerLimit"].ReadOnly = true;

                // Standard WIRE: locked to global connection resistance
                row.Cells["colUpperLimit"].Value = txtConnectionResistance.Text?.Trim() ?? "0";
                row.Cells["colUpperLimit"].ReadOnly = true;
            }
            else if (testType == "SIGNATURE WIRE")
            {
                row.Cells["colExpectedValue"].Value = "-";
                row.Cells["colExpectedValue"].ReadOnly = true;

                row.Cells["colTolerance"].Value = "-";
                row.Cells["colTolerance"].ReadOnly = true;

                // Lower limit remains 0 and locked
                row.Cells["colLowerLimit"].Value = "0";
                row.Cells["colLowerLimit"].ReadOnly = true;

                // Upper limit is EDITABLE for SIGNATURE WIRE
                row.Cells["colUpperLimit"].ReadOnly = false;

                // Only default to connection resistance if it hasn't been set yet
                string currentUpper = row.Cells["colUpperLimit"].Value?.ToString()?.Trim() ?? "";
                if (string.IsNullOrEmpty(currentUpper) || currentUpper == "-" || currentUpper == "0")
                {
                    row.Cells["colUpperLimit"].Value = GetCurrentConnectionResistance().ToString(System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            else if (testType.Equals("DIODE", StringComparison.OrdinalIgnoreCase))
            {
                row.Cells["colExpectedValue"].ReadOnly = false;
                row.Cells["colTolerance"].ReadOnly = false;
                row.Cells["colLowerLimit"].ReadOnly = true;
                row.Cells["colUpperLimit"].ReadOnly = true;

                string expStr = row.Cells["colExpectedValue"].Value?.ToString() ?? "";
                string tolStr = row.Cells["colTolerance"].Value?.ToString() ?? "";

                var expParts = expStr.Split(',').Select(p => p.Trim()).ToArray();
                var tolParts = tolStr.Split(',').Select(p => p.Trim()).ToArray();

                string fwdExpRaw = expParts.Length > 0 ? expParts[0] : "";
                string revExpRaw = expParts.Length > 1 ? expParts[1] : "";

                string fwdTolRaw = tolParts.Length > 0 ? tolParts[0] : "";
                string revTolRaw = tolParts.Length > 1 ? tolParts[1] : "";

                double fwdExp = 0.70;
                string cleanFwdExp = fwdExpRaw.TrimStart('>', '<', ' ');
                if (double.TryParse(cleanFwdExp, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double pFwdExp) && pFwdExp > 0)
                {
                    fwdExp = pFwdExp;
                }

                double revExp = 6.00;
                string cleanRevExp = revExpRaw.TrimStart('>', '<', ' ');
                if (double.TryParse(cleanRevExp, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double pRevExp) && pRevExp > 0)
                {
                    revExp = pRevExp;
                }

                (double minFwdTol, double maxFwdTol, string formattedFwdTol) = ParseClampAndFormatTolerance(fwdTolRaw);
                (double minRevTol, double maxRevTol, string formattedRevTol) = ParseClampAndFormatTolerance(revTolRaw);

                if (string.IsNullOrWhiteSpace(fwdTolRaw) || fwdTolRaw == "-" || fwdTolRaw == "±0%")
                {
                    formattedFwdTol = "±20%";
                    minFwdTol = -20;
                    maxFwdTol = 20;
                }
                if (string.IsNullOrWhiteSpace(revTolRaw) || revTolRaw == "-" || revTolRaw == "±0%")
                {
                    formattedRevTol = "±20%";
                    minRevTol = -20;
                    maxRevTol = 20;
                }

                row.Cells["colTolerance"].Value = $"{formattedFwdTol}, {formattedRevTol}";
                row.Cells["colExpectedValue"].Value = $"{FormatDiodeValue(fwdExp)}, > {FormatDiodeValue(revExp)}";

                double fwdLower = fwdExp * (1.0 + (minFwdTol / 100.0));
                double fwdUpper = fwdExp * (1.0 + (maxFwdTol / 100.0));
                if (fwdLower > fwdUpper) (fwdLower, fwdUpper) = (fwdUpper, fwdLower);

                double revLower = revExp * (1.0 + (minRevTol / 100.0));
                double revUpper = revExp;

                row.Cells["colLowerLimit"].Value = $"{FormatDiodeValue(fwdLower)}, > {FormatDiodeValue(revLower)}";
                row.Cells["colUpperLimit"].Value = $"{FormatDiodeValue(fwdUpper)}, > {FormatDiodeValue(revUpper)}";
            }
            else
            {
                row.Cells["colExpectedValue"].ReadOnly = false;
                row.Cells["colTolerance"].ReadOnly = false;

                // Calculated types (RESISTOR, CAPACITOR, etc.): limits are calculated, so keep them read-only
                row.Cells["colLowerLimit"].ReadOnly = true;
                row.Cells["colUpperLimit"].ReadOnly = true;


                //string expStr = row.Cells["colExpectedValue"].ToString();
                //string tolStr = row.Cells["colTolerance"].ToString();
                string expStr = row.Cells["colExpectedValue"].Value?.ToString() ?? "0";
                string tolStr = row.Cells["colTolerance"].Value?.ToString() ?? "±0%";

                if (double.TryParse(expStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double expectedVal))
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

                    if (testType.Equals("CAPACITOR", StringComparison.OrdinalIgnoreCase))
                    {
                        row.Cells["colLowerLimit"].Value = FormatCapacitorLimit(lower);
                        row.Cells["colUpperLimit"].Value = FormatCapacitorLimit(upper);
                    }
                    else
                    {
                        row.Cells["colLowerLimit"].Value = FormatLimit(lower);
                        row.Cells["colUpperLimit"].Value = FormatLimit(upper);
                    }
                }
                else
                {
                    Debug.WriteLine("fail");
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
            if (double.TryParse(clean, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double val) ||
                double.TryParse(clean, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out val))
            {
                if (val > 100.0) val = 100.0;
                if (val < -100.0) val = -100.0;

                string numStr = Math.Abs(val).ToString("0", System.Globalization.CultureInfo.InvariantCulture);
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

        private static string FormatDiodeValue(double val)
        {
            string s = val.ToString("0.####", System.Globalization.CultureInfo.InvariantCulture);
            if (!s.Contains('.')) return s + ".00";
            var parts = s.Split('.');
            if (parts[1].Length < 2) return parts[0] + "." + parts[1].PadRight(2, '0');
            return s;
        }

        private static string FormatLimit(double val)
        {
            return val.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
        }

        private static string FormatCapacitorLimit(double val)
        {
            return val.ToString("0.##########", System.Globalization.CultureInfo.InvariantCulture);
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
            UpdateDiagram();
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



        private TestConfiguration? BuildConfigurationFromGrid(string configName, string productModel, string? description)
        {
            //var testTypes = GetSelectedTestTypesFromGrid();

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

                double? expected1 = null;
                double? expected2 = null;
                double? lower1 = null;
                double? lower2 = null;
                double? upper1 = null;
                double? upper2 = null;
                double? tol1 = null;
                double? tol2 = null;

                string testTypeName = row.Cells["colTestType"].Value?.ToString() ?? "";
                if (testTypeName.Equals("DIODE", StringComparison.OrdinalIgnoreCase))
                {
                    string expText = row.Cells["colExpectedValue"].Value?.ToString() ?? "";
                    string tolText = row.Cells["colTolerance"].Value?.ToString() ?? "";
                    string lowText = row.Cells["colLowerLimit"].Value?.ToString() ?? "";
                    string uppText = row.Cells["colUpperLimit"].Value?.ToString() ?? "";

                    var expParts = expText.Split(',').Select(p => p.Trim().TrimStart('>', '<', ' ')).ToArray();
                    var tolParts = tolText.Split(',').Select(p => p.Trim()).ToArray();
                    var lowParts = lowText.Split(',').Select(p => p.Trim().TrimStart('>', '<', ' ')).ToArray();
                    var uppParts = uppText.Split(',').Select(p => p.Trim().TrimStart('>', '<', ' ')).ToArray();

                    if (expParts.Length > 0 && double.TryParse(expParts[0], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double e1)) expected1 = e1;
                    if (expParts.Length > 1 && double.TryParse(expParts[1], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double e2)) expected2 = e2;

                    if (lowParts.Length > 0 && double.TryParse(lowParts[0], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double l1)) lower1 = l1;
                    if (lowParts.Length > 1 && double.TryParse(lowParts[1], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double l2)) lower2 = l2;

                    if (uppParts.Length > 0 && double.TryParse(uppParts[0], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double u1)) upper1 = u1;
                    if (uppParts.Length > 1 && double.TryParse(uppParts[1], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double u2)) upper2 = u2;

                    if (tolParts.Length > 0) tol1 = ParseTolerancePercent(tolParts[0]);
                    if (tolParts.Length > 1) tol2 = ParseTolerancePercent(tolParts[1]);

                    expected1 ??= 0.70;
                    tol1 ??= 20.0;
                    lower1 ??= (expected1.Value * (1.0 - tol1.Value / 100.0));
                    upper1 ??= (expected1.Value * (1.0 + tol1.Value / 100.0));

                    expected2 ??= 6.00;
                    tol2 ??= 20.0;
                    lower2 ??= (expected2.Value * (1.0 - tol2.Value / 100.0));
                    upper2 ??= expected2.Value;
                }
                else
                {
                    if (double.TryParse(row.Cells["colExpectedValue"].Value?.ToString(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double expVal))
                        expected1 = expVal;
                    if (double.TryParse(row.Cells["colLowerLimit"].Value?.ToString(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double lowVal))
                        lower1 = lowVal;
                    if (double.TryParse(row.Cells["colUpperLimit"].Value?.ToString(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double uppVal))
                        upper1 = uppVal;
                    tol1 = ParseTolerancePercent(row.Cells["colTolerance"].Value?.ToString());
                }

                double expected = expected1 ?? 0;
                double lower = lower1 ?? 0;
                double upper = upper1 ?? 0;

                if (testTypeName.Equals("CAPACITOR", StringComparison.OrdinalIgnoreCase))
                {
                    if (expected < 0.0000000001 || expected > 0.001)
                    {
                        MessageBox.Show($"Row {i + 1}: Capacitor value must be between 0.0000000001 and 0.001.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                    double? tolVal = ParseTolerancePercent(row.Cells["colTolerance"].Value?.ToString());
                    if (!tolVal.HasValue || Math.Abs(tolVal.Value) < 10 || Math.Abs(tolVal.Value) > 99)
                    {
                        MessageBox.Show($"Row {i + 1}: Capacitor tolerance must be between 10% and 99%.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                }
                else if (testTypeName.Equals("RESISTOR", StringComparison.OrdinalIgnoreCase))
                {
                    double connRes = GetCurrentConnectionResistance();
                    if (expected <= 0 || expected > 500000)
                    {
                        MessageBox.Show($"Row {i + 1}: Resistor value must be between 0 and 500,000 Ω (500k).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                    double? tolVal = ParseTolerancePercent(row.Cells["colTolerance"].Value?.ToString());
                    if (!tolVal.HasValue || Math.Abs(tolVal.Value) < 2 || Math.Abs(tolVal.Value) > 93)
                    {
                        MessageBox.Show($"Row {i + 1}: Resistor tolerance must be between 2% and 93%.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                    if (lower < connRes + 1.0 - 1e-6)
                    {
                        MessageBox.Show($"Row {i + 1}: Resistor lower limit ({lower:F2} Ω) must be at least 1 Ω above continuity resistance ({connRes:F2} Ω).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                }
                else if (testTypeName.Equals("SIGNATURE WIRE", StringComparison.OrdinalIgnoreCase))
                {
                    double maxAllowed = GetMaxAllowedSignatureWireUpperLimit();
                    if (upper < 0.1 - 1e-6 || upper > maxAllowed + 1e-6)
                    {
                        string reason = (maxAllowed < 500000.0)
                            ? $"between 0.1 and {maxAllowed:F2} Ω (at least 1 Ω below the smallest resistor lower limit of {maxAllowed + 1.0:F2} Ω)."
                            : "between 0.1 and 500,000 Ω (500k).";
                        MessageBox.Show($"Row {i + 1}: Signature Wire upper limit must be {reason}", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                }
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
                    TestType = tt,
                    ExpectedValue = expected1,
                    LowerLimit = lower1,
                    UpperLimit = upper1,
                    Tolerance = tol1,
                    ExpectedValue2 = expected2,
                    LowerLimit2 = lower2,
                    UpperLimit2 = upper2,
                    Tolerance2 = tol2,
                    MeasurementUnit = GetValidMeasurementUnit(testTypeName, row.Cells["colUnit"].Value?.ToString()),
                    Color = row.Cells["colColor"].Value?.ToString() ?? "",
                    Gauge = row.Cells["colGauge"].Value?.ToString() ?? ""
                });
            }

            double.TryParse(txtConnectionResistance.Text?.Trim(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double maxWireRes);
            if (maxWireRes < 0.1 || maxWireRes > 100)
            {
                MessageBox.Show("Continuity resistance must be between 0.1 and 100 Ω.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            double.TryParse(txtInsulationResistance.Text?.Trim(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double minInsRes);

            return new TestConfiguration
            {
                ConfigName = configName,
                ProductModelName = productModel,
                Description = description,
                MaxWireResistance = maxWireRes,
                MinInsulationResistance = minInsRes,
                MinComponentResistance = 0,
                ShortTest = chkEnableShortTest.Checked,
                //TestTypes = testTypes,
                Nets = nets
            };
        }

        public List<TestType> GetSelectedTestTypesFromGrid()
        {
            var selectedList = new List<TestType>();
            try
            {
                using var db = new Data.AppDbContext();
                var allTestTypes = db.TestTypes.ToList();

                var gridTestTypeNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                for (int i = 0; i < gridConfig.Rows.Count; i++)
                {
                    var row = gridConfig.Rows[i];
                    if (row.IsNewRow) continue;

                    string typeName = row.Cells["colTestType"].Value?.ToString()?.Trim() ?? "";
                    if (!string.IsNullOrEmpty(typeName))
                    {
                        gridTestTypeNames.Add(typeName);
                    }
                }

                foreach (var tt in allTestTypes)
                {
                    bool isPresent = gridTestTypeNames.Any(gt =>
                        string.Equals(gt, tt.TestTypeName, StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(gt, tt.DisplayName, StringComparison.OrdinalIgnoreCase) ||
                        (tt.TestTypeName.Equals("RESISTOR", StringComparison.OrdinalIgnoreCase) && (gt.Equals("RESIS", StringComparison.OrdinalIgnoreCase) || gt.Equals("4RESISTOR", StringComparison.OrdinalIgnoreCase))) ||
                        (tt.TestTypeName.Equals("WIRE", StringComparison.OrdinalIgnoreCase) && (gt.Equals("4WIRE", StringComparison.OrdinalIgnoreCase) || gt.Equals("SIGNATURE WIRE", StringComparison.OrdinalIgnoreCase))) ||
                        (tt.TestTypeName.Equals("CAPACITOR", StringComparison.OrdinalIgnoreCase) && gt.Equals("CAP", StringComparison.OrdinalIgnoreCase)) ||
                        (tt.TestTypeName.Equals("DIODE", StringComparison.OrdinalIgnoreCase) && gt.Equals("SIGNATURE DIODE", StringComparison.OrdinalIgnoreCase))
                    );

                    if (isPresent)
                    {
                        selectedList.Add(tt);
                    }
                }
            }
            catch (Exception ex)
            {
                FileLogService.Error("TestParametersPage.GetSelectedTestTypesFromGrid", "Failed to resolve grid test types", ex);
            }

            return selectedList;
        }

        private static double? ParseTolerancePercent(string? tolStr)
        {
            if (string.IsNullOrWhiteSpace(tolStr) || tolStr == "-") return null;
            string clean = tolStr.Replace("±", "").Replace("+", "").Replace("%", "").Trim();
            if (double.TryParse(clean, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double val) ||
                double.TryParse(clean, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out val))
            {
                return val;
            }
            return null;
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
                        seqNo.ToString(),       // No
                        "",                     // Harness Group
                        "",                     // Color
                        "",                     // Gauge
                        "",                     // Net-ID
                        fromAddr.ToString(),    // from
                        "",                     // from label
                        TestDetailDto.ExtractConnector("", fromPin), // from connector
                        toAddr.ToString(),      // to
                        "",                     // to label
                        TestDetailDto.ExtractConnector("", toPin),   // to connector
                        rowTestType,            // test type
                        "",                     // lower limit
                        "",                     // upper limit
                        "",                     // expected value
                        ""                      // tolerance
                    );
                }
            }

            RenumberRows();
            UpdateTotalNetCount();
            UpdateDiagramSize();
            UpdateDiagram();
        }

        private void PicPreview_DiagramRowClicked(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < gridConfig.Rows.Count && !gridConfig.Rows[rowIndex].IsNewRow)
            {
                gridConfig.ClearSelection();
                gridConfig.Rows[rowIndex].Selected = true;
                gridConfig.FirstDisplayedScrollingRowIndex = rowIndex;
            }
        }

        private void UpdateDiagram()
        {
            var nets = new List<AssemblyLineTester.Views.Controls.DiagramNetDefinition>();
            var highlighted = new HashSet<int>();

            for (int i = 0; i < gridConfig.Rows.Count; i++)
            {
                var row = gridConfig.Rows[i];
                if (row.IsNewRow) continue;

                nets.Add(new AssemblyLineTester.Views.Controls.DiagramNetDefinition
                {
                    RowIndex = i,
                    FromAddress = row.Cells["colFromAddr"].Value?.ToString() ?? "0",
                    FromLabel = row.Cells["colFromLabel"].Value?.ToString() ?? $"X1-0{i + 1}",
                    ToAddress = row.Cells["colToAddr"].Value?.ToString() ?? "0",
                    ToLabel = row.Cells["colToLabel"].Value?.ToString() ?? $"Y1-0{i + 1}",
                    IsError = false
                });
            }

            picPreview.RenderDiagram(nets, defaultTestPoint);
            picPreview.SetHighlightedRows(highlightedRows);
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
            List<LearnConnection> selectedConnections;
            byte testLearnValue = 0;

            // Show the Learn Connection Modal Options
            using (var modal = new AssemblyLineTester.Views.Modals.LearnConnectionModalForm())
            {
                if (modal.ShowDialog(this) != DialogResult.OK)
                {
                    return; // User cancelled
                }
                selectedConnections = modal.SelectedTypes;

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
            foreach (LearnConnection conn in selectedConnections)
            {
                byte val = (byte)(conn.TestType?.TestTypeValue ?? conn.TestTypeValue);
                if (val == 0)
                {
                    string name = conn.DisplayName?.Trim().ToUpperInvariant() ?? "";
                    if (name.Contains("RESIST")) val = 1;
                    else if (name.Contains("CAPACIT")) val = 2;
                    else if (name.Contains("DIODE")) val = 4;
                    else if (name.Contains("TWIST")) val = 8;
                }
                testLearnValue += val;
            }
            //Debug.WriteLine("====================================================================");
            //Debug.WriteLine(testLearnValue);

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

                var learnResult = testLearnValue == 0
                    ? await Task.Run(() => _instrument.LearnConnection(), token)
                    : await Task.Run(() => _instrument.LearnComplexConnection("", testLearnValue), token);

                if (token.IsCancellationRequested) return;
                Debug.WriteLine($"test learn value = {testLearnValue}");

                if (learnResult != CtaDotNet.Enums.CtaCommandResultCode.Success)
                {
                    string errorMessage = "";
                    if (_instrument.GetLastError().DllCallResult == CtaCommandResultCode.Success)
                    {
                        errorMessage = _instrument.GetLastError().Result;
                    }

                    // Fallback: If the API completely refuses to give the error, force a generic message
                    if (string.IsNullOrWhiteSpace(errorMessage) || errorMessage == "No Error")
                    {
                        errorMessage = "Hardware Learn Aborted. Please check the actual error message from Instrument for further clarification.";
                    }
                    MessageBox.Show(this, $"learn operation failed.\n\nResult Code: {learnResult}\nMessage: {errorMessage}", "Learn Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //return;
                }

                var getTestResult = await Task.Run(() => _instrument.GetLearnedConnection(), token);
                if (token.IsCancellationRequested) return;

                if (getTestResult == null || string.IsNullOrWhiteSpace(getTestResult.Result))
                {

                    MessageBox.Show(this, "Failed to retrieve learned test program text from instrument.", "Learn Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _customTestText = getTestResult.Result;
                Debug.WriteLine(_customTestText);
                //ParseAndPopulateGridFromProgramText(_customTestText, learnTestTypes);

                MessageBox.Show(this, "Connection learned and table updated successfully!", "Learn Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                config = _configService.ConvertToTestConfig(getTestResult.Result);
                //Debug.WriteLine(_customTestText);
                //foreach (var net in config.Nets)
                //{
                //    Debug.WriteLine($"learn btn {net.FromAddress} to {net.ToAddress}");
                //}
                if (config != null)
                {
                    LoadConfigurationIntoGrid(config);
                }
                string checkResult = _configService.ConvertToProgramText(config, defaultTestPoint);
                txtConnectionResistance.Text = config.MaxWireResistance.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
                //txtInsulationResistance.Text = config.MinInsulationResistance.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);


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

        /*
        private async void btnLearnConnection_Click(object? sender, EventArgs e)
        {
            List<string> learnTestTypes;
            List<LearnConnection> selectedConnections;
            byte testLearnValue = 0;

            // Show the Learn Connection Modal Options
            using (var modal = new AssemblyLineTester.Views.Modals.LearnConnectionModalForm())
            {
                if (modal.ShowDialog(this) != DialogResult.OK)
                {
                    return; // User cancelled
                }
                selectedConnections = modal.SelectedTypes;

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
            foreach (LearnConnection conn in selectedConnections)
            {
                testLearnValue += (byte)(conn.TestType?.TestTypeValue ?? conn.TestTypeValue);
            }
            //Debug.WriteLine("====================================================================");
            //Debug.WriteLine(testLearnValue);

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
                //bool isConnected = _instrument.Connect();
                //if (!isConnected)
                //{
                //    MessageBox.Show("Failed to connect to instrument.", "Connection Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                //  WIRE = 0
                //  RESISTORS = 1
                //  CAPACITORS = 2  
                //  DIODES = 4
                //  TWISTED PAIRS = 8

                var learnResult = testLearnValue == 0
                    ? await Task.Run(() => _instrument.LearnConnection(), token)
                    : await Task.Run(() => _instrument.LearnComplexConnection("", testLearnValue), token);

                if (token.IsCancellationRequested) return;

                if (learnResult != CtaDotNet.Enums.CtaCommandResultCode.Success)
                {
                    string errorMessage = "No Error";

                    // Fallback: If the API completely refuses to give the error, force a generic message
                    if (string.IsNullOrWhiteSpace(errorMessage) || errorMessage == "No Error")
                    {
                        errorMessage = "Hardware Learn Aborted. Please check the physical instrument display for details (e.g., resistance too high).";
                    }
                    MessageBox.Show(this, $"Failed to execute learn operation on instrument.\n\nResult Code: {learnResult}\nMessage: {errorMessage}", "Learn Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var getTestResult = await Task.Run(() => _instrument.GetLearnedConnection(), token);
                if (token.IsCancellationRequested) return;

                if (getTestResult == null || string.IsNullOrWhiteSpace(getTestResult.Result))
                {

                    MessageBox.Show(this, "Failed to retrieve learned test program text from instrument.", "Learn Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _customTestText = getTestResult.Result;
                Debug.WriteLine(_customTestText);
                //ParseAndPopulateGridFromProgramText(_customTestText, learnTestTypes);

                MessageBox.Show(this, "Connection learned and table updated successfully!", "Learn Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                config = _configService.ConvertToTestConfig(getTestResult.Result);
                //Debug.WriteLine(_customTestText);
                //foreach (var net in config.Nets)
                //{
                //    Debug.WriteLine($"learn btn {net.FromAddress} to {net.ToAddress}");
                //}
                if (config != null)
                {
                    LoadConfigurationIntoGrid(config);
                }
                string checkResult = _configService.ConvertToProgramText(config);
                txtConnectionResistance.Text = config.MaxWireResistance.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
                txtInsulationResistance.Text = config.MinInsulationResistance.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);


                //Debug.WriteLine("Convert  = ");
                //Debug.WriteLine(checkResult);
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
        */
        private async void btnStartAll_Click(object? sender, EventArgs e)
        {
            if (!CheckRunPermission()) return;

            _testCts?.Cancel();
            _testCts = new System.Threading.CancellationTokenSource();
            var token = _testCts.Token;

            InstrumentService.StopMonitoring();
            string originalText = btnStartAll.Text;
            btnStartAll.Text = "Loading.....";
            btnLearnConnection.Enabled = false;
            btnStartAll.Enabled = false;
            btnStartSelected.Enabled = false;
            btnAbort.Enabled = true;

            try
            {
                await StartAllV2(token);
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

                _testCts?.Dispose();
                _testCts = null;

                InstrumentService.StartMonitoring();
            }
        }

        private async void btnStartSelected_Click(object? sender, EventArgs e)
        {
            if (!CheckRunPermission()) return;

            _testCts?.Cancel();
            _testCts = new System.Threading.CancellationTokenSource();
            var token = _testCts.Token;

            InstrumentService.StopMonitoring();
            string originalText = btnStartSelected.Text;
            btnStartSelected.Text = "Loading.....";
            btnLearnConnection.Enabled = false;
            btnStartAll.Enabled = false;
            btnStartSelected.Enabled = false;
            btnAbort.Enabled = true;

            try
            {
                await StartSelectedV2(token);
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

                _testCts?.Dispose();
                _testCts = null;

                InstrumentService.StartMonitoring();
            }
        }

        public async Task StartAllV2(System.Threading.CancellationToken token = default)
        {
            var selectedRows = GetAllRowIndices();
            if (selectedRows.Count == 0)
            {
                MessageBox.Show(this, "Empty Row to Start.", "No Row Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var config = BuildConfigurationFromGrid("Temporary Test", "Temporary Test", null);
            if (config == null || config.Nets.Count == 0)
            {
                MessageBox.Show(this, "Failed to build configuration from grid.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _configService.SaveConfig(config);

            _customTestText = _configService.ConvertToProgramText(config, defaultTestPoint);

            Debug.WriteLine(_customTestText);
            //foreach (var net in config.Nets)
            //{
            //    Debug.WriteLine($"start btn {net.FromAddress} to {net.ToAddress}");
            //}

            if (string.IsNullOrWhiteSpace(_customTestText))
            {
                MessageBox.Show(this, "Generated program text is empty.", "Program Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool isConnected = _instrument.Connect();
            if (!isConnected)
            {
                MessageBox.Show(this, "Failed to connect to instrument.", "Connection Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            var putResult = await Task.Run(() => _instrument.PutTestProgram(string.Empty, _customTestText, true), token);
            if (putResult != CtaDotNet.Enums.CtaCommandResultCode.Success)
            {
                MessageBox.Show(this, $"Failed to upload custom test program. Error: {putResult}", "Upload Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            await Task.Run(() => ExecuteTestForRows(selectedRows, config, token));

            //await Task.Run(() => ExecuteTestForRowsV2(selectedRows, token), token);

            //if (!token.IsCancellationRequested)
            //{
            //    MessageBox.Show(this, "Finished testing all connections (V2)", "Test Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        public async Task StartSelectedV2(System.Threading.CancellationToken token = default)
        {
            var selectedRows = GetSelectedOrHighlightedRowIndices();
            if (selectedRows.Count == 0)
            {
                MessageBox.Show(this, "Please click or select at least one row to test.", "No Row Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var config = BuildConfigurationForRows(selectedRows, "Temporary Test", "Temporary Test", null);
            if (config == null || config.Nets.Count == 0)
            {
                MessageBox.Show(this, "Failed to build configuration for selected rows.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _configService.SaveConfig(config);

            _customTestText = _configService.ConvertToProgramText(config, defaultTestPoint);
            //Debug.WriteLine(_customTestText);
            //foreach (var net in config.Nets)
            //{
            //    Debug.WriteLine($"start btn {net.FromAddress} to {net.ToAddress}");
            //}
            //Debug.WriteLine(_customTestText);
            if (string.IsNullOrWhiteSpace(_customTestText))
            {
                MessageBox.Show(this, "Generated program text is empty.", "Program Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool isConnected = _instrument.Connect();
            if (!isConnected)
            {
                MessageBox.Show(this, "Failed to connect to instrument.", "Connection Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var putResult = await Task.Run(() => _instrument.PutTestProgram(string.Empty, _customTestText, true), token);
            if (putResult != CtaDotNet.Enums.CtaCommandResultCode.Success)
            {
                MessageBox.Show(this, $"Failed to upload custom test program. Error: {putResult}", "Upload Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            await Task.Run(() => ExecuteTestForRows(selectedRows,config, token));

            //if (!token.IsCancellationRequested)
            //{
            //    MessageBox.Show(this, "Finished testing selected connections (V2)", "Test Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}


        }

        private void ExecuteTestForRowsV2(List<int> targetRowIndices, System.Threading.CancellationToken token = default)
        {
            if (token.IsCancellationRequested) return;

            if (targetRowIndices.Count == 0)
            {
                ShowMessageBoxOnUI("No valid rows selected for testing.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate that all target rows have colFromLabel and colToLabel filled
            for (int k = 0; k < targetRowIndices.Count; k++)
            {
                int rowIndex = targetRowIndices[k];
                if (rowIndex < 0 || rowIndex >= gridConfig.Rows.Count) continue;
                var row = gridConfig.Rows[rowIndex];
                if (row.IsNewRow) continue;

                string fromLabel = row.Cells["colFromLabel"].Value?.ToString()?.Trim() ?? "";
                string toLabel = row.Cells["colToLabel"].Value?.ToString()?.Trim() ?? "";

                if (string.IsNullOrWhiteSpace(fromLabel) || string.IsNullOrWhiteSpace(toLabel))
                {
                    ShowMessageBoxOnUI($"Row {rowIndex + 1}: Net-From Label and Net-To Label must be filled before testing.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            bool isConnected = _instrument.Connect();
            if (!isConnected)
            {
                ShowMessageBoxOnUI("Failed to connect to instrument.", "Connection Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (token.IsCancellationRequested) return;

            for (int k = 0; k < targetRowIndices.Count; k++)
            {
                if (token.IsCancellationRequested) return;

                int rowIndex = targetRowIndices[k];
                if (rowIndex < 0 || rowIndex >= gridConfig.Rows.Count) continue;
                var row = gridConfig.Rows[rowIndex];
                if (row.IsNewRow) continue;

                int.TryParse(row.Cells["colFromAddr"].Value?.ToString(), out int nextFromAddr);
                int.TryParse(row.Cells["colToAddr"].Value?.ToString(), out int nextToAddr);
                string from = "J" + ((nextFromAddr - 1) / 64 + 1).ToString() + "-" + ((nextFromAddr - 1) % 64 + 1).ToString();
                string to = "J" + ((nextToAddr - 1) / 64 + 1).ToString() + "-" + ((nextToAddr - 1) % 64 + 1).ToString();
                //if (defaultTestPoint == 128)
                //{
                //    if (nextFromAddr > 64 )
                //    {
                //        nextFromAddr += 64;
                //    }
                //    if (nextToAddr > 64)
                //    {
                //        nextToAddr += 64;
                //    }
                //    from = "J" + ((nextFromAddr - 1) / 64 + 1).ToString() + "-" + ((nextFromAddr - 1) % 64 + 1).ToString("D3");
                //    to = "J" + ((nextToAddr - 1) / 64 + 1).ToString() + "-" + ((nextToAddr - 1) % 64 + 1).ToString("D3");
                    
                //}

                string fromLabel = row.Cells["colFromLabel"].Value?.ToString()?.Trim() ?? "";
                string toLabel = row.Cells["colToLabel"].Value?.ToString()?.Trim() ?? "";
                string testTypeName = row.Cells["colTestType"].Value?.ToString()?.Trim()?.ToUpperInvariant() ?? "";

                Debug.WriteLine($"from {from}");
                Debug.WriteLine($"from label {fromLabel}");

                Debug.WriteLine($"to {to}");
                Debug.WriteLine($"to label {toLabel}");

                string resultValueStr = "";

                if (testTypeName == "CAPACITOR")
                {
                    resultValueStr = _instrument.GetCapacitance(from, to).MeasuredValue.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
                }
                else if (testTypeName == "DIODE" || testTypeName == "SIGNATURE DIODE")
                {
                    dynamic res = _instrument.GetDiode(from, to);
                    resultValueStr = ExtractDiodeValue(res);
                }
                else if (testTypeName == "TWISTED PAIR")
                {
                    dynamic res = _instrument.GetTwistedPair(from, to);
                    resultValueStr = ExtractTwistedPairValue(res);
                }
                else
                {
                    // Default: WIRE, 4WIRE, RESISTOR, 4RESISTOR, SIGNATURE WIRE
                    resultValueStr = _instrument.GetRessistance(from, to).MeasuredValue.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
                }
                Debug.WriteLine($"values = {resultValueStr}");

                UpdateGridRowUnitValue(rowIndex, resultValueStr);
            }
        }

        

        private static string ExtractDiodeValue(dynamic res)
        {
            if (res == null) return "Error";
            try
            {
                if (res.DllCallResult != CtaDotNet.Enums.CtaCommandResultCode.Success)
                {
                    return res.DllCallResult.ToString();
                }
            }
            catch { }

            try
            {
                double fwd = (double)res.ForwardVoltage;
                double rev = (double)res.ReverseVoltage;
                byte fvStatus = 0;
                byte rvStatus = 0;
                try { fvStatus = (byte)res.FVStatus; } catch { }
                try { rvStatus = (byte)res.RVStatus; } catch { }

                string fwdPrefix = (fvStatus == 2) ? "> " : ((fvStatus == 1) ? "< " : "");
                string revPrefix = (rvStatus == 2) ? "> " : ((rvStatus == 1) ? "< " : "");

                return $"{fwdPrefix}{fwd.ToString("F2", System.Globalization.CultureInfo.InvariantCulture)}, {revPrefix}{rev.ToString("F2", System.Globalization.CultureInfo.InvariantCulture)}";
            }
            catch { }

            try { return ((double)res.ForwardVoltage).ToString("F2", System.Globalization.CultureInfo.InvariantCulture); } catch { }
            try { return ((double)res.MeasuredValue).ToString("F2", System.Globalization.CultureInfo.InvariantCulture); } catch { }
            try { return ((double)res.Value).ToString("F2", System.Globalization.CultureInfo.InvariantCulture); } catch { }

            return "0.00";
        }

        private static string ExtractTwistedPairValue(dynamic res)
        {
            if (res == null) return "Error";
            try
            {
                if (res.DllCallResult != CtaDotNet.Enums.CtaCommandResultCode.Success)
                {
                    return res.DllCallResult.ToString();
                }
            }
            catch { }

            try { return ((bool)res.Value) ? "Pass" : "Failed"; } catch { }
            try { return ((bool)res.FunctionResult) ? "Pass" : "Failed"; } catch { }
            try { return ((bool)res.Result) ? "Pass" : "Failed"; } catch { }

            return "Failed";
        }

        private void UpdateGridRowUnitValue(int rowIndex, string valStr)
        {
            void Action()
            {
                if (rowIndex >= 0 && rowIndex < gridConfig.Rows.Count)
                {
                    var row = gridConfig.Rows[rowIndex];
                    row.Cells["colUnit"].Value = valStr;

                    int colIdx = gridConfig.Columns["colUnit"]?.Index ?? -1;
                    if (colIdx >= 0)
                    {
                        gridConfig.UpdateCellValue(colIdx, rowIndex);
                    }
                    gridConfig.InvalidateRow(rowIndex);
                }
            }

            if (this.InvokeRequired)
            {
                this.Invoke(new Action(Action));
            }
            else
            {
                Action();
            }
        }

        private async void btnAbort_Click(object? sender, EventArgs e)
        {
            // 1. Cancel software task execution loop
            _testCts?.Cancel();
            btnAbort.Enabled = false;

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

        private void ExecuteTestForRows(List<int> targetRowIndices, TestConfiguration config, System.Threading.CancellationToken token = default)
        {
            if (token.IsCancellationRequested) return;

            if (targetRowIndices.Count == 0 || config == null || config.Nets.Count == 0)
            {
                ShowMessageBoxOnUI("No valid rows selected for testing.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool isConnected = _instrument.Connect();
            if (!isConnected)
            {
                ShowMessageBoxOnUI("Failed to connect to instrument.", "Connection Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (token.IsCancellationRequested) return;

            _customTestText = _configService.ConvertToProgramText(config,defaultTestPoint);
            //Debug.WriteLine(_customTestText);

            if (token.IsCancellationRequested) return;

            try
            {
                if (!string.IsNullOrEmpty(_customTestText))
                {
                    var putResult = _instrument.PutTestProgram(string.Empty, _customTestText, true);
                    if (putResult != CtaDotNet.Enums.CtaCommandResultCode.Success)
                    {
                        ShowMessageBoxOnUI($"Failed to upload custom test program. Error: {putResult}", "Upload Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    ShowMessageBoxOnUI("Generated test program text is empty.", "Program Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                ShowMessageBoxOnUI($"Error converting configuration to program text:\n{ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (token.IsCancellationRequested) return;

            Debug.WriteLine("Run Test");
            var testResult = _instrument.RunSingleTest(false, false);

            if (token.IsCancellationRequested) return;

            if (testResult == null || testResult.DllCallResult != CtaDotNet.Enums.CtaCommandResultCode.Success)
            {
                ShowMessageBoxOnUI($"Test execution failed. Error: {testResult?.DllCallResult.ToString() ?? "Unknown"}", "Execution Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        var measurement = list[k];
                        Debug.WriteLine(JsonSerializer.Serialize(measurement));

                        // 1. Temporary verification: check if Point1 and Point2 match row From and To sequentially
                        if (rowIndex >= 0 && rowIndex < gridConfig.Rows.Count)
                        {
                            CheckMeasurementRowMatch(gridConfig.Rows[rowIndex], measurement, k);
                        }

                        // 2. Format measured value (F2 for >= 0.01, full precision for small values like capacitance) with > / < prefix
                        string rowTestType = "";
                        if (rowIndex >= 0 && rowIndex < gridConfig.Rows.Count)
                        {
                            rowTestType = gridConfig.Rows[rowIndex].Cells["colTestType"].Value?.ToString()?.Trim()?.ToUpperInvariant() ?? "";
                        }
                        bool isDiode = rowTestType.Equals("DIODE", StringComparison.OrdinalIgnoreCase) ||
                                       rowTestType.Equals("SIGNATURE DIODE", StringComparison.OrdinalIgnoreCase) ||
                                       string.Equals(measurement.Type, "DIODE", StringComparison.OrdinalIgnoreCase);

                        string valStr;
                        if (isDiode)
                        {
                            string fwdStr = FormatMeasuredValue(measurement.Value1, measurement.Status1);
                            string revStr = FormatMeasuredValue(measurement.Value2, measurement.Status2);
                            valStr = $"{fwdStr}, {revStr}";
                        }
                        else
                        {
                            valStr = FormatMeasuredValue(measurement.Value1, measurement.Status1);
                        }

                        // 3. Update the Measurement column (colUnit) in the grid
                        UpdateGridRowUnitValue(rowIndex, valStr);
                    }
                }
                else
                {
                    ShowMessageBoxOnUI("No measured values returned from instrument.", "Test Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowMessageBoxOnUI($"Failed to retrieve measured values. Error: {measuredValuesResult?.DllCallResult.ToString() ?? "Unknown"}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Formats measurement value:
        /// - Prepends "> " or "< " if Status1 contains '>' or '<'
        /// - Normal values (>= 0.01): rounded to 2 decimals (e.g. 1.233945 -> "1.23")
        /// - Small values (< 0.01, e.g. Farads 0.000124): prints full decimal value
        /// </summary>
        private static string FormatMeasuredValue(float val, string? status1 = null)
        {
            string prefix = "";
            if (!string.IsNullOrWhiteSpace(status1))
            {
                string trimmed = status1.Trim();
                if (trimmed.Contains(">")) prefix = "> ";
                else if (trimmed.Contains("<")) prefix = "< ";
            }

            string numStr;
            if (Math.Abs(val) < 1e-12f)
            {
                numStr = "0.00";
            }
            else if (Math.Abs(val) >= 0.01f)
            {
                numStr = val.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                numStr = val.ToString("0.##########", System.Globalization.CultureInfo.InvariantCulture);
            }

            return prefix + numStr;
        }

        /// <summary>
        /// Temporary check to verify whether CTA measurement Point1/Point2 match the row From/To sequentially.
        /// Outputs the result to Debug.WriteLine.
        /// </summary>
        private bool CheckMeasurementRowMatch(DataGridViewRow row, CtaDotNet.Models.CtaJsonResult measurement, int index)
        {
            int.TryParse(row.Cells["colFromAddr"].Value?.ToString(), out int fromAddr);
            int.TryParse(row.Cells["colToAddr"].Value?.ToString(), out int toAddr);

            string fromLabel = row.Cells["colFromLabel"].Value?.ToString()?.Trim() ?? "";
            string toLabel = row.Cells["colToLabel"].Value?.ToString()?.Trim() ?? "";

            string expectedFromPin = FormatPinAddress(fromAddr);
            string expectedToPin = FormatPinAddress(toAddr);

            string actualP1 = NormalizePin(measurement.Point1);
            string actualP2 = NormalizePin(measurement.Point2);
            string expFrom = NormalizePin(expectedFromPin);
            string expTo = NormalizePin(expectedToPin);

            bool pinMatch = (!string.IsNullOrEmpty(actualP1) && !string.IsNullOrEmpty(actualP2)) &&
                            ((expFrom == actualP1 && expTo == actualP2) || (expFrom == actualP2 && expTo == actualP1));

            bool labelMatch = (!string.IsNullOrEmpty(fromLabel) &&
                              (fromLabel.Equals(measurement.Point1, StringComparison.OrdinalIgnoreCase) ||
                               fromLabel.Equals(measurement.NetName, StringComparison.OrdinalIgnoreCase)));

            bool isMatch = pinMatch || labelMatch;

            Debug.WriteLine($"[Row-Check #{index + 1}] Row {row.Index + 1} ({expectedFromPin} -> {expectedToPin}) <=> CTA ({measurement.Point1} -> {measurement.Point2}, Net: {measurement.NetName}) => {(isMatch ? "MATCH OK" : "MISMATCH / VERIFY ORDER")}");

            return isMatch;
        }

        private static string FormatPinAddress(int address)
        {
            if (address <= 0) return "";
            int conn = ((address - 1) / 64) + 1;
            int pin = ((address - 1) % 64) + 1;
            return $"J{conn}-{pin}";
        }

        private static string NormalizePin(string? pin)
        {
            if (string.IsNullOrWhiteSpace(pin)) return "";
            string clean = pin.Replace("$", "").Trim().ToUpperInvariant();
            var parts = clean.Split('-');
            if (parts.Length == 2 && int.TryParse(parts[1], out int pinNum))
            {
                return $"{parts[0]}-{pinNum}";
            }
            return clean;
        }

        private void ShowMessageBoxOnUI(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => MessageBox.Show(this, message, caption, buttons, icon)));
            }
            else
            {
                MessageBox.Show(this, message, caption, buttons, icon);
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

                double? expected1 = null;
                double? expected2 = null;
                double? lower1 = null;
                double? lower2 = null;
                double? upper1 = null;
                double? upper2 = null;
                double? tol1 = null;
                double? tol2 = null;

                string testTypeName = row.Cells["colTestType"].Value?.ToString() ?? "";
                if (testTypeName.Equals("DIODE", StringComparison.OrdinalIgnoreCase))
                {
                    string expText = row.Cells["colExpectedValue"].Value?.ToString() ?? "";
                    string tolText = row.Cells["colTolerance"].Value?.ToString() ?? "";
                    string lowText = row.Cells["colLowerLimit"].Value?.ToString() ?? "";
                    string uppText = row.Cells["colUpperLimit"].Value?.ToString() ?? "";

                    var expParts = expText.Split(',').Select(p => p.Trim().TrimStart('>', '<', ' ')).ToArray();
                    var tolParts = tolText.Split(',').Select(p => p.Trim()).ToArray();
                    var lowParts = lowText.Split(',').Select(p => p.Trim().TrimStart('>', '<', ' ')).ToArray();
                    var uppParts = uppText.Split(',').Select(p => p.Trim().TrimStart('>', '<', ' ')).ToArray();

                    if (expParts.Length > 0 && double.TryParse(expParts[0], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double e1)) expected1 = e1;
                    if (expParts.Length > 1 && double.TryParse(expParts[1], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double e2)) expected2 = e2;

                    if (lowParts.Length > 0 && double.TryParse(lowParts[0], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double l1)) lower1 = l1;
                    if (lowParts.Length > 1 && double.TryParse(lowParts[1], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double l2)) lower2 = l2;

                    if (uppParts.Length > 0 && double.TryParse(uppParts[0], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double u1)) upper1 = u1;
                    if (uppParts.Length > 1 && double.TryParse(uppParts[1], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double u2)) upper2 = u2;

                    if (tolParts.Length > 0) tol1 = ParseTolerancePercent(tolParts[0]);
                    if (tolParts.Length > 1) tol2 = ParseTolerancePercent(tolParts[1]);

                    expected1 ??= 0.70;
                    tol1 ??= 20.0;
                    lower1 ??= (expected1.Value * (1.0 - tol1.Value / 100.0));
                    upper1 ??= (expected1.Value * (1.0 + tol1.Value / 100.0));

                    expected2 ??= 6.00;
                    tol2 ??= 20.0;
                    lower2 ??= (expected2.Value * (1.0 - tol2.Value / 100.0));
                    upper2 ??= expected2.Value;
                }
                else
                {
                    if (double.TryParse(row.Cells["colExpectedValue"].Value?.ToString(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double expVal))
                        expected1 = expVal;
                    if (double.TryParse(row.Cells["colLowerLimit"].Value?.ToString(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double lowVal))
                        lower1 = lowVal;
                    if (double.TryParse(row.Cells["colUpperLimit"].Value?.ToString(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double uppVal))
                        upper1 = uppVal;
                    tol1 = ParseTolerancePercent(row.Cells["colTolerance"].Value?.ToString());
                }

                double expected = expected1 ?? 0;
                double lower = lower1 ?? 0;
                double upper = upper1 ?? 0;

                if (testTypeName.Equals("CAPACITOR", StringComparison.OrdinalIgnoreCase))
                {
                    if (expected < 0.0000000001 || expected > 0.001)
                    {
                        MessageBox.Show($"Row {rowIndex + 1}: Capacitor value must be between 0.0000000001 and 0.001.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                    double? tolVal = ParseTolerancePercent(row.Cells["colTolerance"].Value?.ToString());
                    if (!tolVal.HasValue || Math.Abs(tolVal.Value) < 10 || Math.Abs(tolVal.Value) > 99)
                    {
                        MessageBox.Show($"Row {rowIndex + 1}: Capacitor tolerance must be between 10% and 99%.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                }
                else if (testTypeName.Equals("RESISTOR", StringComparison.OrdinalIgnoreCase))
                {
                    double connRes = GetCurrentConnectionResistance();
                    if (expected <= 0 || expected > 500000)
                    {
                        MessageBox.Show($"Row {rowIndex + 1}: Resistor value must be between 0 and 500,000 Ω (500k).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                    double? tolVal = ParseTolerancePercent(row.Cells["colTolerance"].Value?.ToString());
                    if (!tolVal.HasValue || Math.Abs(tolVal.Value) < 2 || Math.Abs(tolVal.Value) > 93)
                    {
                        MessageBox.Show($"Row {rowIndex + 1}: Resistor tolerance must be between 2% and 93%.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                    if (lower < connRes + 1.0 - 1e-6)
                    {
                        MessageBox.Show($"Row {rowIndex + 1}: Resistor lower limit ({lower:F2} Ω) must be at least 1 Ω above continuity resistance ({connRes:F2} Ω).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                }
                else if (testTypeName.Equals("SIGNATURE WIRE", StringComparison.OrdinalIgnoreCase))
                {
                    double maxAllowed = GetMaxAllowedSignatureWireUpperLimit();
                    if (upper < 0.1 - 1e-6 || upper > maxAllowed + 1e-6)
                    {
                        string reason = (maxAllowed < 500000.0)
                            ? $"between 0.1 and {maxAllowed:F2} Ω (at least 1 Ω below the smallest resistor lower limit of {maxAllowed + 1.0:F2} Ω)."
                            : "between 0.1 and 500,000 Ω (500k).";
                        MessageBox.Show($"Row {rowIndex + 1}: Signature Wire upper limit must be {reason}", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                }
                var tt = _configService.GetTestTypeByName(testTypeName);
                nets.Add(new TestConfigNet
                {
                    SequenceNo = nets.Count + 1,
                    NetId = row.Cells["colNetId"].Value?.ToString() ?? "",
                    HarnessGroup = row.Cells["colHarnessGroup"].Value?.ToString() ?? "",
                    FromAddress = fromAddr,
                    FromLabel = row.Cells["colFromLabel"].Value?.ToString() ?? "",
                    ToAddress = toAddr,
                    ToLabel = row.Cells["colToLabel"].Value?.ToString() ?? "",
                    TestTypeId = tt?.Id ?? 0,
                    TestType = tt,
                    ExpectedValue = expected1,
                    LowerLimit = lower1,
                    UpperLimit = upper1,
                    Tolerance = tol1,
                    ExpectedValue2 = expected2,
                    LowerLimit2 = lower2,
                    UpperLimit2 = upper2,
                    Tolerance2 = tol2,
                    MeasurementUnit = GetValidMeasurementUnit(testTypeName, row.Cells["colUnit"].Value?.ToString()),
                    Color = row.Cells["colColor"].Value?.ToString() ?? "",
                    Gauge = row.Cells["colGauge"].Value?.ToString() ?? ""
                });
            }

            double.TryParse(txtConnectionResistance.Text?.Trim(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double maxWireRes);
            if (maxWireRes < 0.1 || maxWireRes > 100)
            {
                MessageBox.Show("Continuity resistance must be between 0.1 and 100 Ω.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            double.TryParse(txtInsulationResistance.Text?.Trim(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double minInsRes);

            return new TestConfiguration
            {
                ConfigName = configName,
                ProductModelName = productModel,
                Description = description,
                MaxWireResistance = maxWireRes,
                MinInsulationResistance = minInsRes,
                MinComponentResistance = 0,
                ShortTest = chkEnableShortTest.Checked,
                Nets = nets
            };
        }

        private static string GetValidMeasurementUnit(string testTypeName, string? rawUnit)
        {
            string unit = rawUnit?.Trim() ?? "";
            string key = string.IsNullOrWhiteSpace(testTypeName) ? "WIRE" : testTypeName.Trim().ToUpperInvariant();
            var allowedUnits = UnitConversionHelper.GetUnitsForTestType(key);
            if (allowedUnits.Contains(unit)) return unit;
            return UnitConversionHelper.GetBaseUnit(key);
        }

        private bool ValidateRowsForZeroValues(List<int> targetRowIndices)
        {
            foreach (int rowIndex in targetRowIndices)
            {
                if (rowIndex < 0 || rowIndex >= gridConfig.Rows.Count) continue;
                var row = gridConfig.Rows[rowIndex];
                if (row.IsNewRow) continue;

                string testTypeName = row.Cells["colTestType"].Value?.ToString() ?? "";
                string upperType = testTypeName.Trim().ToUpperInvariant();

                if (upperType == "CAPACITOR")
                {
                    double.TryParse(row.Cells["colExpectedValue"].Value?.ToString(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double expected);
                    double? tolerance = ParseTolerancePercent(row.Cells["colTolerance"].Value?.ToString());

                    if (expected < 0.0000000001 || expected > 0.001)
                    {
                        MessageBox.Show(this, $"Row {rowIndex + 1} ({testTypeName}): Capacitor value must be between 0.0000000001 and 0.001.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (!tolerance.HasValue || Math.Abs(tolerance.Value) < 10 || Math.Abs(tolerance.Value) > 99)
                    {
                        MessageBox.Show(this, $"Row {rowIndex + 1} ({testTypeName}): Capacitor tolerance must be between 10% and 99%.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                else if (upperType == "RESISTOR")
                {
                    double.TryParse(row.Cells["colExpectedValue"].Value?.ToString(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double expected);
                    double.TryParse(row.Cells["colLowerLimit"].Value?.ToString(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double lower);
                    double? tolerance = ParseTolerancePercent(row.Cells["colTolerance"].Value?.ToString());
                    double connRes = GetCurrentConnectionResistance();

                    if (expected <= 0 || expected > 500000)
                    {
                        MessageBox.Show(this, $"Row {rowIndex + 1} ({testTypeName}): Resistor value must be between 0 and 500,000 Ω (500k).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (!tolerance.HasValue || Math.Abs(tolerance.Value) < 2 || Math.Abs(tolerance.Value) > 93)
                    {
                        MessageBox.Show(this, $"Row {rowIndex + 1} ({testTypeName}): Resistor tolerance must be between 2% and 93%.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (lower < connRes + 1.0 - 1e-6)
                    {
                        MessageBox.Show(this, $"Row {rowIndex + 1} ({testTypeName}): Resistor lower limit ({lower:F2} Ω) must be at least 1 Ω above continuity resistance ({connRes:F2} Ω).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                else if (upperType == "SIGNATURE WIRE")
                {
                    double.TryParse(row.Cells["colUpperLimit"].Value?.ToString(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double upper);
                    double maxAllowed = GetMaxAllowedSignatureWireUpperLimit();
                    if (upper < 0.1 - 1e-6 || upper > maxAllowed + 1e-6)
                    {
                        string reason = (maxAllowed < 500000.0)
                            ? $"between 0.1 and {maxAllowed:F2} Ω (at least 1 Ω below the smallest resistor lower limit of {maxAllowed + 1.0:F2} Ω)."
                            : "between 0.1 and 500,000 Ω (500k).";
                        MessageBox.Show(this, $"Row {rowIndex + 1} ({testTypeName}): Upper limit must be {reason}", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                else if (upperType == "DIODE")
                {
                    string expText = row.Cells["colExpectedValue"].Value?.ToString() ?? "";
                    string tolText = row.Cells["colTolerance"].Value?.ToString() ?? "";
                    var expParts = expText.Split(',').Select(p => p.Trim().TrimStart('>', '<', ' ')).ToArray();
                    var tolParts = tolText.Split(',').Select(p => p.Trim()).ToArray();

                    double exp1 = 0, exp2 = 0;
                    if (expParts.Length > 0) double.TryParse(expParts[0], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out exp1);
                    if (expParts.Length > 1) double.TryParse(expParts[1], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out exp2);

                    double? tol1 = tolParts.Length > 0 ? ParseTolerancePercent(tolParts[0]) : null;
                    double? tol2 = tolParts.Length > 1 ? ParseTolerancePercent(tolParts[1]) : null;

                    if (exp1 <= 0 || (expParts.Length > 1 && exp2 <= 0))
                    {
                        MessageBox.Show(this, $"Row {rowIndex + 1} ({testTypeName}): Expected Value must be greater than 0.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (!tol1.HasValue || tol1.Value <= 0 || (tolParts.Length > 1 && (!tol2.HasValue || tol2.Value <= 0)))
                    {
                        MessageBox.Show(this, $"Row {rowIndex + 1} ({testTypeName}): Tolerance must be greater than 0.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                else if (!string.IsNullOrWhiteSpace(upperType) && !ContinuityLikeTypes.Contains(upperType, StringComparer.OrdinalIgnoreCase) && upperType != "CONTINUITY")
                {
                    double.TryParse(row.Cells["colExpectedValue"].Value?.ToString(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double expected);
                    double? tolerance = ParseTolerancePercent(row.Cells["colTolerance"].Value?.ToString());

                    if (expected <= 0)
                    {
                        MessageBox.Show(this, $"Row {rowIndex + 1} ({testTypeName}): Expected Value must be greater than 0.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (!tolerance.HasValue || tolerance.Value <= 0)
                    {
                        MessageBox.Show(this, $"Row {rowIndex + 1} ({testTypeName}): Tolerance must be greater than 0.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
