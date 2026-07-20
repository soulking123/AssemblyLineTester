using AssemblyLineTester.Data;
using AssemblyLineTester.Models.Dto;
using AssemblyLineTester.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AssemblyLineTester.Views.Pages
{
    public partial class TestExecution : UserControl
    {
        private readonly InstrumentService _instrument = new();
        private List<LiveTestDetailRowDto> _loadedRows = [];
        private readonly TestConfigService _configService = new();
        private string _customTestText = string.Empty;
        private double _continuityResistance ;
        private int? _currentReportId;


        public TestExecution()
        {
            InitializeComponent();
            SetupFilterDropdowns();
            SetupGridLiveDetails();
            SetupDiagramPointers();
            btnLoadProgram.Click += BtnLoadProgram_Click;
            SetupEvents();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                var configService = new TestConfigService();
                var activeConfig = configService.GetActiveConfig();
                if (activeConfig != null)
                {

                    _customTestText = configService.ConvertToProgramText(activeConfig);
                    lblPageHeader.Text = $"TEST EXECUTION - {activeConfig.ConfigName}";
                    var resNet = activeConfig.Nets?.FirstOrDefault(n => n.UpperLimit.HasValue);
                    if (resNet != null && resNet.UpperLimit.HasValue)
                    {
                        _continuityResistance = resNet.UpperLimit.Value;
                    }
                    Debug.WriteLine($"Loaded active config on startup: {activeConfig.ConfigName}");
                }
                else
                {
                    lblPageHeader.Text = "TEST EXECUTION - No Active Program Loaded";
                    
                }
            }
        }

        // Populates the filter dropdowns at the top of the page (Product Model, Type, Area, etc).
        private void SetupFilterDropdowns()
        {
            cmbProductModel.Items.Clear();
            cmbProductModel.Items.AddRange(["6060", "6070", "8080", "9090"]);
            cmbProductModel.SelectedIndex = 0;

            cmbProductType.Items.Clear();
            cmbProductType.Items.AddRange(["AC", "DC", "Hybrid"]);
            cmbProductType.SelectedIndex = 0;

            cmbArea.Items.Clear();
            cmbArea.Items.Clear();
            cmbArea.Items.AddRange(["Engine Control", "Cabin Interior", "Chassis", "Dashboard"]);
            cmbArea.SelectedIndex = 0;

            cmbPartNumber.Items.Clear();
            cmbPartNumber.Items.AddRange(["WH-ENG-001", "WH-ENG-002", "WH-CAB-005", "WH-CHS-010"]);
            cmbPartNumber.SelectedIndex = 0;

            cmbEcnNo.Items.Clear();
            cmbEcnNo.Items.AddRange(["123.R001", "123.R002", "124.R000"]);
            cmbEcnNo.SelectedIndex = 0;
        }

        // Sets up grid columns and styling only. No rows are added here -
        // the grid stays empty until a config file is loaded via Load Program.
        private void SetupGridLiveDetails()
        {
            gridLiveDetails.Columns.Clear();

            gridLiveDetails.Columns.Add("StepNo", "#");
            gridLiveDetails.Columns.Add("TestPoint", "Test Point");
            gridLiveDetails.Columns.Add("TestType", "Test Type");
            gridLiveDetails.Columns.Add("Measured", "Measured Value");
            gridLiveDetails.Columns.Add("Status", "Status");

            gridLiveDetails.AllowUserToResizeColumns = true;
            gridLiveDetails.Columns["StepNo"]!.Width = 55;
            gridLiveDetails.Columns["TestPoint"]!.Width = 240;
            gridLiveDetails.Columns["TestType"]!.Width = 180;
            gridLiveDetails.Columns["Measured"]!.Width = 240;
            gridLiveDetails.Columns["Status"]!.MinimumWidth = 140;
            gridLiveDetails.Columns["Status"]!.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            AssemblyLineTester.Helpers.UIStyleHelper.StyleDataGridView(gridLiveDetails);
            gridLiveDetails.RowTemplate.Height = 36;
            gridLiveDetails.ColumnHeadersHeight = 40;
        }

        private void BtnLoadProgram_Click(object? sender, EventArgs e)
        {
            var configService = new TestConfigService();
            using var dlg = new AssemblyLineTester.Views.Modals.LoadProgramModalForm(configService);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (dlg.SelectedConfiguration != null)
                {
                    configService.SetActiveConfig(dlg.SelectedConfiguration.ConfigName);
                    _customTestText = configService.ConvertToProgramText(dlg.SelectedConfiguration);
                    var resNet = dlg.SelectedConfiguration.Nets?.FirstOrDefault(n => n.UpperLimit.HasValue);
                    if (resNet != null && resNet.UpperLimit.HasValue)
                    {
                        _continuityResistance = resNet.UpperLimit.Value;
                    }

                    MessageBox.Show($"Test program configuration '{dlg.SelectedConfiguration.ConfigName}' loaded successfully and set as Active.", "Program Loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Debug.WriteLine($"Loaded program from DB: {dlg.SelectedConfiguration.ConfigName}\n{_customTestText}");
                }
                else if (!string.IsNullOrEmpty(dlg.LoadedFilePath))
                {
                    try
                    {
                        if (dlg.LoadedFilePath.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                        {
                            var netsDto = configService.LoadConfigFile(dlg.LoadedFilePath);
                            var tempConfig = new Models.TestConfiguration
                            {
                                ConfigName = System.IO.Path.GetFileNameWithoutExtension(dlg.LoadedFilePath),
                                Nets = [.. netsDto.Select((d, idx) => new Models.TestConfigNet
                                {
                                    SequenceNo = idx + 1,
                                    FromAddress = d.FromAddress,
                                    ToAddress = d.ToAddress,
                                    UpperLimit = d.UpperLimit
                                })]
                            };
                            _customTestText = configService.ConvertToProgramText(tempConfig);
                            var resNet = tempConfig.Nets.FirstOrDefault(n => n.UpperLimit.HasValue);
                            if (resNet != null && resNet.UpperLimit.HasValue)
                            {
                                _continuityResistance = resNet.UpperLimit.Value;
                            }
                        }
                        else
                        {
                            _customTestText = System.IO.File.ReadAllText(dlg.LoadedFilePath);
                        }

                        MessageBox.Show($"Loaded test program file:\n{dlg.LoadedFilePath}", "Program Loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Debug.WriteLine($"Loaded program from file: {dlg.LoadedFilePath}\n{_customTestText}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading file '{dlg.LoadedFilePath}':\n{ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        //private void ShowEditConfigsDialog()
        //{
        //    var configs = _configService.GetAllConfigurations();

        //    using Form dlg = new()
        //    {
        //        Text = "Edit Config",
        //        Width = 900,
        //        Height = 520,
        //        StartPosition = FormStartPosition.CenterParent,
        //        FormBorderStyle = FormBorderStyle.Sizable
        //    };

        //    var grid = new DataGridView()
        //    {
        //        Dock = DockStyle.Fill,
        //        ReadOnly = true,
        //        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        //        AllowUserToAddRows = false,
        //        AllowUserToDeleteRows = false,
        //        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        //    };

        //    grid.Columns.Add("ConfigName", "Config Name");
        //    grid.Columns.Add("ProductModel", "Product Model");
        //    grid.Columns.Add("CreatedAt", "Created At");
        //    grid.Columns.Add("UpdatedAt", "Updated At");
        //    grid.Columns.Add("IsActive", "Active");
        //    grid.Columns.Add("NetCount", "Nets");

        //    foreach (var c in configs)
        //    {
        //        int idx = grid.Rows.Add(c.ConfigName, c.ProductModel, c.CreatedAt.ToString("g"), c.UpdatedAt?.ToString("g") ?? "-", c.IsActive ? "Yes" : "No", c.Nets?.Count ?? 0);
        //        grid.Rows[idx].Tag = c.ConfigName;
        //    }

        //    var btnPanel = new Panel() { Dock = DockStyle.Bottom, Height = 56 };
        //    var btnLoad = new Button() { Text = "Load", Width = 100, Height = 36, Left = 8, Top = 10 };
        //    var btnDelete = new Button() { Text = "Delete", Width = 100, Height = 36, Left = 116, Top = 10 };
        //    var btnClose = new Button() { Text = "Close", Width = 100, Height = 36, Left = 224, Top = 10 };

        //    btnLoad.Click += (s, e) =>
        //    {
        //        if (grid.SelectedRows.Count == 0)
        //        {
        //            MessageBox.Show("Please select a configuration to load.", "Edit Config", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }

        //        string name = grid.SelectedRows[0].Tag as string ?? grid.SelectedRows[0].Cells[0].Value?.ToString() ?? string.Empty;
        //        var cfg = _configService.GetConfigurationByName(name);
        //        if (cfg == null)
        //        {
        //            MessageBox.Show("Selected configuration could not be loaded (it may have been deleted).", "Edit Config", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }

        //        LoadConfigurationIntoGrid(cfg);
        //        dlg.Close();
        //    };



        //    btnDelete.Click += (s, e) =>
        //    {
        //        if (grid.SelectedRows.Count == 0)
        //        {
        //            MessageBox.Show("Please select a configuration to delete.", "Edit Config", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }

        //        string name = grid.SelectedRows[0].Tag as string ?? grid.SelectedRows[0].Cells[0].Value?.ToString() ?? string.Empty;
        //        if (MessageBox.Show($"Delete configuration '{name}'? This cannot be undone.", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
        //            return;

        //        try
        //        {
        //            bool ok = _configService.DeleteConfiguration(name);
        //            if (ok)
        //            {
        //                // remove row and refresh
        //                grid.Rows.Remove(grid.SelectedRows[0]);
        //                MessageBox.Show("Configuration deleted.", "Edit Config", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                // if the deleted config was loaded in the editor, clear loaded state
        //                if (string.Equals(_loadedConfigName, name, StringComparison.OrdinalIgnoreCase))
        //                {
        //                    _loadedConfigName = null;
        //                }
        //            }
        //            else
        //            {
        //                MessageBox.Show("Failed to delete configuration.", "Edit Config", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show($"Error deleting configuration:\n{ex.Message}", "Edit Config", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    };

        //    btnClose.Click += (s, e) => dlg.Close();

        //    btnPanel.Controls.Add(btnLoad);
        //    btnPanel.Controls.Add(btnDelete);
        //    btnPanel.Controls.Add(btnClose);

        //    dlg.Controls.Add(grid);
        //    dlg.Controls.Add(btnPanel);

        //    // double-click to load
        //    grid.CellDoubleClick += (s, e) =>
        //    {
        //        if (e.RowIndex >= 0)
        //        {
        //            grid.ClearSelection();
        //            grid.Rows[e.RowIndex].Selected = true;
        //            btnLoad.PerformClick();
        //        }
        //    };

        //    dlg.ShowDialog(this);
        //}

        private void BtnStartTest_Click(object? sender, EventArgs e)
        {
            // 1. Temporarily pause background monitoring to prevent concurrent socket calls
            //InstrumentService.StopMonitoring();
            //_configService.GetConfigurationByName(name);

            try
            {
                // 2. Connect to the instrument
                bool isConnected = _instrument.Connect();
                if (!isConnected)
                {
                    MessageBox.Show("Failed to connect to instrument.", "Connection Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 2. Load the test program
                //Debug.WriteLine(_customTestText);.

                 var activeConfig = _configService.GetActiveConfig();
                 if (activeConfig == null)
                 {
                     MessageBox.Show("No active test configuration found. Please load a test program first.", "Missing Configuration", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     _instrument.Disconnect();
                     return;
                 }
                 _customTestText = _configService.ConvertToProgramText(activeConfig);

                Debug.WriteLine("Convert to Tesxt");
                Debug.WriteLine(_customTestText);
                try
                {

                    if (!string.IsNullOrEmpty(_customTestText))
                    {
                        // Upload custom test text without saving to database (using empty string as testName)
                        Debug.WriteLine("before put");
                        var putResult = _instrument.PutTestProgram(string.Empty, _customTestText, true);
                        Debug.WriteLine("after put");
                        if (putResult != CtaDotNet.Enums.CtaCommandResultCode.Success)
                        {
                            _instrument.Disconnect();
                            MessageBox.Show($"Failed to upload custom test program. Error: {putResult}", "Upload Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        _continuityResistance = _instrument.GetContinuityResistance();

                        Debug.WriteLine($"Nilai Continuity Resistance saat ini: {_continuityResistance}");
                    }
                    else
                    {
                        Debug.WriteLine("error syntax");
                            // Fallback: Fetch/load the specified test program "1107061216"
                            //var getTestResult = _instrument.GetTestProgram("1107061216");
                            //if (getTestResult != null && getTestResult.DllCallResult == CtaDotNet.Enums.CtaCommandResultCode.Success)
                            //{
                            //    testProgramDetails = getTestResult.Result;
                            //}
                        }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error convert to text file :\n{ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //Debug.WriteLine($"Active Program: {loadedProgramName}");
                //_ = testProgramDetails;

                //3.Run Single Test(Hipot= false, Shorts= true)

                Debug.WriteLine("Run Test");
                var testResult = _instrument.RunSingleTest(false, true);
                if (testResult == null || testResult.DllCallResult != CtaDotNet.Enums.CtaCommandResultCode.Success)
                {
                    _instrument.Disconnect();
                    MessageBox.Show($"Test run failed to execute. Error: {testResult?.DllCallResult.ToString() ?? "Unknown"}", "Test Run Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 4. Get Measured Values
                Debug.WriteLine("Get Measured Value");
                var measuredValuesResult = _instrument.GetMeasuredValues();
                _loadedRows = [];

                if (measuredValuesResult != null && measuredValuesResult.DllCallResult == CtaDotNet.Enums.CtaCommandResultCode.Success && measuredValuesResult.ObjectReturnValue != null)
                {
                    var list = measuredValuesResult.ObjectReturnValue.MeasuredValueList;
                    if (list != null)
                    {
                        int step = 1;
                        foreach (var measurement in list)
                        {
                            var status = LiveTestStatus.Pending;
                            string statusStr = measurement.Status1?.Trim() ?? "";
                            if (measurement.Value1 <= _continuityResistance)
                            {
                                status = LiveTestStatus.Pass;
                            }
                            else
                            {
                                status = LiveTestStatus.Fail;
                            }

                            string rowTestType = "WIRE";
                            if (!string.IsNullOrEmpty(measurement.Instruction))
                            {
                                string trimmed = measurement.Instruction.Trim().ToUpperInvariant();
                                if (trimmed.Contains("CONNECTION") || trimmed.Contains("RESIS") || trimmed == "WIRE")
                                {
                                    rowTestType = "WIRE";
                                }
                                else if (trimmed.Contains("INSULATION") || trimmed.Contains("HIPOT") || trimmed == "OPEN")
                                {
                                    rowTestType = "OPEN";
                                }
                                else
                                {
                                    rowTestType = measurement.Instruction;
                                }
                            }

                            _loadedRows.Add(new LiveTestDetailRowDto
                            {
                                StepNo = step++,
                                NetId = measurement.Instruction ?? "",
                                TestPoint = $"{measurement.Point1} \u2192 {measurement.Point2}",
                                TestType = rowTestType,
                                MeasuredValue = Math.Round(measurement.Value1, 3),
                                Status = status
                            });
                        }
                    }
                }

                //5.Repopulate the GridView to show the results

                Debug.WriteLine("Update Grid");

                PopulateLiveDetailsGrid(_loadedRows);

                // 7. Show summary MessageBox
                //string testStatusText = testResult.TestResultCode == CtaDotNet.Enums.extEnumTTesterAccessTestResult.ctatrPassed
                //    ? "PASSED"
                //    : "FAILED or INCOMPLETE";
                try
                {
                    bool allPass = _loadedRows.Count > 0 && _loadedRows.TrueForAll(r => r.Status == LiveTestStatus.Pass);
                    string overallResult = allPass ? "PASS" : "FAIL";

                    var reportService = new TestReportService();
                    _currentReportId = reportService.SaveDraftLiveTestReport(
                        cmbProductModel.Text, cmbProductType.Text, cmbArea.Text, cmbEcnNo.Text, "1.0",
                        cmbPartNumber.Text, DateTime.Now, _loadedRows, overallResult);

                    Debug.WriteLine($"Draft report auto-saved to Test History as ReportId={_currentReportId}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to auto-save draft report: {ex.Message}");
                    MessageBox.Show(
                        "Test completed but failed to record it to Test History automatically.\nYou can still save it manually via Save Report.",
                        "Auto-Save Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // 7. Show summary MessageBox
                string testStatusText = testResult.TestResultCode == CtaDotNet.Enums.extEnumTTesterAccessTestResult.ctatrPassed
                ? "PASSED"
                : "FAILED or INCOMPLETE";

                }
            finally
            {
                // 3. Resume background monitoring
                //InstrumentService.StartMonitoring();
            }
        }

        private void PopulateLiveDetailsGrid(List<LiveTestDetailRowDto> rows)
        {

            gridLiveDetails.Rows.Clear();

            Debug.WriteLine("Update Grid 2");

            foreach (var row in rows)
            {
                int idx = gridLiveDetails.Rows.Add(
                    row.StepNo.ToString(),
                    row.TestPoint,
                    row.TestType,
                    row.MeasuredValue?.ToString("F3", System.Globalization.CultureInfo.InvariantCulture) ?? "",
                    row.Status.ToString().ToUpperInvariant());

                ApplyStatusStyle(gridLiveDetails.Rows[idx], row.Status);

                Debug.WriteLine(row.MeasuredValue);
            }

            Debug.WriteLine("Update Grid 3");
            gridLiveDetails.Refresh();
        }

        private static void ApplyStatusStyle(DataGridViewRow row, LiveTestStatus status)
        {
            switch (status)
            {
                case LiveTestStatus.Pass:
                    row.Cells["Status"].Style.BackColor = Color.FromArgb(209, 231, 221);
                    row.Cells["Status"].Style.ForeColor = Color.FromArgb(15, 81, 50);
                    break;
                case LiveTestStatus.Testing:
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 251, 235);
                    row.Cells["Status"].Style.BackColor = Color.FromArgb(254, 240, 138);
                    row.Cells["Status"].Style.ForeColor = Color.FromArgb(133, 77, 14);
                    break;
                default: // Pending, Fail
                    row.Cells["Status"].Style.BackColor = Color.FromArgb(241, 245, 249);
                    row.Cells["Status"].Style.ForeColor = Color.FromArgb(100, 116, 139);
                    break;
            }
        }

        private void SetupDiagramPointers()
        {
            picMachineOverview.Paint += PicMachineOverview_Paint;
            picAreaDetail.Paint += PicAreaDetail_Paint;
            picHarnessConn.Paint += PicHarnessConn_Paint;
        }

        private void PicMachineOverview_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle r = picMachineOverview.ClientRectangle;

            using LinearGradientBrush brush = new(r, Color.FromArgb(241, 245, 249), Color.FromArgb(226, 232, 240), 45F);
            g.FillRectangle(brush, r);

            using Pen pen = new(Color.FromArgb(148, 163, 184), 2);
            g.DrawRectangle(pen, 20, 20, r.Width - 40, r.Height - 40);

            using SolidBrush b = new(Color.FromArgb(71, 85, 105));
            StringFormat sf = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g.DrawString("🖥️ Assembly Test Station #4\n[Online & Active]", AssemblyLineTester.Helpers.UIStyleHelper.Font11Bold, b, r, sf);
        }

        private void PicAreaDetail_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle r = picAreaDetail.ClientRectangle;

            using SolidBrush brush = new(Color.FromArgb(248, 250, 252));
            g.FillRectangle(brush, r);

            using Pen borderPen = new(Color.FromArgb(226, 232, 240), 1);
            g.DrawRectangle(borderPen, 0, 0, r.Width - 1, r.Height - 1);

            string[,] details = new string[,]
            {
                { "🔧 Assembly Line:", "Assembly Line B" },
                { "🏢 Station ID:", "STATION-04B (ECU Route)" },
                { "📋 Harness Model:", "WH-ENG-001 (Rev 3)" },
                { "👤 Operator Session:", "OP-402 (Standard Shift)" },
                { "💻 Calibration Ver:", "ALT-ECU-v3.4.1" }
            };

            using SolidBrush bKey = new(Color.FromArgb(71, 85, 105));
            float startY = 30;
            float rowHeight = (r.Height - 60) / 5f;

            for (int i = 0; i < 5; i++)
            {
                string key = details[i, 0];
                string val = details[i, 1];

                g.DrawString(key, AssemblyLineTester.Helpers.UIStyleHelper.Font95Bold, bKey, 30, startY + (i * rowHeight));

                using SolidBrush bVal = new(i == 2 ? Color.FromArgb(13, 110, 253) : Color.FromArgb(15, 23, 42));
                g.DrawString(val, AssemblyLineTester.Helpers.UIStyleHelper.Font95Regular, bVal, 220, startY + (i * rowHeight));

                if (i < 4)
                {
                    using Pen sepPen = new(Color.FromArgb(241, 245, 249), 1);
                    g.DrawLine(sepPen, 20, startY + (i * rowHeight) + rowHeight - 5, r.Width - 20, startY + (i * rowHeight) + rowHeight - 5);
                }
            }
        }

        private void PicHarnessConn_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle r = picHarnessConn.ClientRectangle;

            using SolidBrush brush = new(Color.FromArgb(241, 245, 249));
            g.FillRectangle(brush, r);

            using SolidBrush b = new(Color.FromArgb(51, 65, 85));
            StringFormat sf = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g.DrawString("🔌 Connector Pinout: 24-Pin ECU Harness\nActive Pin: J1-23 ➔ J3-16", AssemblyLineTester.Helpers.UIStyleHelper.Font10Bold, b, r, sf);
        }

        private void SetupEvents()
        {
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(pnlFilterCard, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardMachineOverview, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardAreaDetail, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardHarnessConn, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardTestStatus, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardLiveDetails, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardTestSummary, 12, Color.FromArgb(34, 40, 49), 2.5f);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupCardRoundedEdges(cardNotes, 12, Color.FromArgb(34, 40, 49), 2.5f);

            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnLoadProgram, 10, Color.FromArgb(250, 204, 21), Color.FromArgb(254, 216, 53), Color.FromArgb(234, 186, 10), Color.Transparent);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnStartTest, 12, Color.FromArgb(56, 142, 60), Color.FromArgb(76, 175, 80), Color.FromArgb(46, 125, 50), Color.Transparent);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnStopTest, 12, Color.FromArgb(211, 47, 47), Color.FromArgb(239, 83, 80), Color.FromArgb(198, 40, 40), Color.Transparent);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnSaveReport, 12, Color.FromArgb(37, 99, 235), Color.FromArgb(59, 130, 246), Color.FromArgb(29, 78, 216), Color.Transparent);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnViewPinMap, 8, Color.FromArgb(37, 99, 235), Color.FromArgb(59, 130, 246), Color.FromArgb(29, 78, 216), Color.Transparent);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnViewWiringDiagram, 8, Color.FromArgb(37, 99, 235), Color.FromArgb(59, 130, 246), Color.FromArgb(29, 78, 216), Color.Transparent);

            btnViewWiringDiagram.Click += BtnViewWiringDiagram_Click;
            btnViewPinMap.Click += BtnViewWiringDiagram_Click;
            btnSaveReport.Click += BtnSaveReport_Click;
        }

        private void BtnSaveReport_Click(object? sender, EventArgs e)
        {
            if (_loadedRows == null || _loadedRows.Count == 0)
            {
                if (MessageBox.Show("No active test data in grid. Do you want to open the save report modal anyway?", "No Test Results", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            bool allPass = _loadedRows != null && _loadedRows.Count > 0 && _loadedRows.TrueForAll(r => r.Status == LiveTestStatus.Pass);
            string overallResult = allPass ? "PASS" : "FAIL";

            using var modal = new AssemblyLineTester.Views.Modals.SaveTestReportModalForm(
                cmbProductModel.Text,
                cmbEcnNo.Text,
                cmbProductType.Text,
                "1.0",
                cmbArea.Text,
                "OP-402",
                cmbPartNumber.Text,
                $"SN-{DateTime.Now:HHmmss}",
                overallResult,
                DateTime.Now,
                _loadedRows ?? new List<LiveTestDetailRowDto>()
            );

            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                MessageBox.Show("Test Report saved successfully to the database!\nIt can now be viewed in Reports and Test History.", "Report Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnViewWiringDiagram_Click(object? sender, EventArgs e)
        {
            using var modal = new AssemblyLineTester.Views.Modals.WiringPreviewModalForm();
            modal.ShowDialog(this);
        }
    }
}