using AssemblyLineTester.Data;
using AssemblyLineTester.Models;
using AssemblyLineTester.Models.Dto;
using AssemblyLineTester.Services;
using CtaDotNet.Enums;
using DocumentFormat.OpenXml.Office2010.CustomUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using AssemblyLineTester.Helpers;
using static AssemblyLineTester.Models.SystemLog;

using Timer = System.Windows.Forms.Timer;



namespace AssemblyLineTester.Views.Pages
{
    public partial class TestExecution : UserControl
    {
        private readonly InstrumentService _instrument = new();
        private List<LiveTestDetailRowDto> _loadedRows = new();
        private List<string> _resultColumns = new();
        private readonly TestConfigService _configService = new TestConfigService();
        private string _customTestText = string.Empty;
        private double _continuityResistance;
        private int? _currentReportId;
        private System.Threading.CancellationTokenSource? _testCts;
        private Stopwatch _testStopwatch = new Stopwatch();
        private Timer _elapsedTimer = new Timer();
        private DateTime _startTime;
        private DateTime _endTime;
        private readonly SystemLogService logService = new();

        public TestExecution()
        {
            InitializeComponent();
            SetupFilterDropdowns();
            SetupGridLiveDetails();
            SetupDiagramPointers();
            btnLoadProgram.Click += BtnLoadProgram_Click;
            SetupEvents();
            SetupTimer();

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

        private void SetupTimer()
        {
            _elapsedTimer.Interval = 1000;
        }

        private void OnTestFinishedSuccessfully()
        {
            lblStatusRunning.Text = "Finished";
            lblStatusRunning.ForeColor = Color.Green;
            StopTestTiming();
        }

        // Stops the clock and writes the final end time and elapsed total to the labels.
        private void StopTestTiming()
        {
            _testStopwatch.Stop();
            _elapsedTimer.Stop();

            // Shown on screen, never stored, so local time is the right choice here.
            _endTime = DateTime.Now;
            lblEndTimeVal.Text = _endTime.ToString("dd MMM yyyy hh:mm:ss tt");
        }

        // Fills the Product Model, Type, Area, Part Number and ECN dropdowns.
        private void SetupFilterDropdowns()
        {
            cmbProductModel.Items.Clear();
            cmbProductModel.Items.AddRange(new object[] { "6060", "6070", "8080", "9090" });
            cmbProductModel.SelectedIndex = 0;

            cmbProductType.Items.Clear();
            cmbProductType.Items.AddRange(new object[] { "AC", "DC", "Hybrid" });
            cmbProductType.SelectedIndex = 0;

            cmbArea.Items.Clear();
            cmbArea.Items.Clear();
            cmbArea.Items.AddRange(new object[] { "Engine Control", "Cabin Interior", "Chassis", "Dashboard" });
            cmbArea.SelectedIndex = 0;

            cmbPartNumber.Items.Clear();
            cmbPartNumber.Items.AddRange(new object[] { "WH-ENG-001", "WH-ENG-002", "WH-CAB-005", "WH-CHS-010" });
            cmbPartNumber.SelectedIndex = 0;

            cmbEcnNo.Items.Clear();
            cmbEcnNo.Items.AddRange(new object[] { "123.R001", "123.R002", "124.R000" });
            cmbEcnNo.SelectedIndex = 0;
        }

        // Rebuilds the live grid header after the result-column ticks were saved in Settings.
        public void ReloadResultColumns() => SetupGridLiveDetails();

        // Columns and styling only; rows arrive when a program is loaded.
        private void SetupGridLiveDetails()
        {
            gridLiveDetails.Columns.Clear();
            _resultColumns = TestSettingService.GetEnabledResultColumns();

            gridLiveDetails.Columns.Add("StepNo", "#");
            foreach (string column in _resultColumns)
            {
                gridLiveDetails.Columns.Add(column, column);
                gridLiveDetails.Columns[column]!.MinimumWidth = 110;
            }
            gridLiveDetails.Columns.Add("Status", "Status");

            gridLiveDetails.AllowUserToResizeColumns = true;
            gridLiveDetails.Columns["StepNo"]!.Width = 55;
            gridLiveDetails.Columns["Status"]!.MinimumWidth = 140;
            gridLiveDetails.Columns["Status"]!.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            AssemblyLineTester.Helpers.UIStyleHelper.StyleDataGridView(gridLiveDetails);
            gridLiveDetails.RowTemplate.Height = 36;
            gridLiveDetails.ColumnHeadersHeight = 40;
        }

        // The instrument reports measurements but knows nothing about labels, limits or tolerance;
        // those live on the loaded program, so each row is filled from its net.
        //
        // ponytail: matched by position, which assumes the instrument returns measurements in program
        // order. Swap for a pin-address match if that assumption ever breaks.
        private void ApplyConfigNets(TestConfiguration config)
        {
            var nets = config.Nets?.OrderBy(n => n.SequenceNo).ToList();
            if (nets == null || nets.Count != _loadedRows.Count) return;

            for (int i = 0; i < _loadedRows.Count; i++)
            {
                var row = _loadedRows[i];
                var net = nets[i];

                row.FromLabel = net.FromLabel;
                row.ToLabel = net.ToLabel;
                row.LowerLimit = net.LowerLimit;
                row.Tolerance = net.Tolerance;
                row.Unit = net.MeasurementUnit;
                if (net.UpperLimit.HasValue) row.UpperLimit = net.UpperLimit;
                if (net.ExpectedValue.HasValue) row.ExpectedValue = net.ExpectedValue.Value.ToString(CultureInfo.InvariantCulture);
                if (string.IsNullOrWhiteSpace(row.NetId)) row.NetId = net.NetId;
                if (string.IsNullOrWhiteSpace(row.HarnessGroup)) row.HarnessGroup = net.HarnessGroup;
            }
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
                                Nets = netsDto.Select((d, idx) => new Models.TestConfigNet
                                {
                                    SequenceNo = idx + 1,
                                    FromAddress = d.FromAddress,
                                    ToAddress = d.ToAddress,
                                    UpperLimit = d.UpperLimit
                                }).ToList()
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
      
        private async void btnStartTest_Click(object? sender, EventArgs e)
        {
            _testCts?.Cancel();
            _testCts = new System.Threading.CancellationTokenSource();
            var token = _testCts.Token;

            btnLoadProgram.Enabled = false;
            btnStartTest.Enabled = false;
            btnSaveReport.Enabled = false;
            txtNotes.Clear();

            try
            {
                // GetActiveConfig returns null when no program is loaded, and ExecuteTest would
                // have dereferenced it. Refused here, with the reason, rather than crashed.
                TestConfiguration? activeConfig = _configService.GetActiveConfig();
                if (activeConfig is null)
                {
                    MessageBox.Show(this,
                        "No test program is active. Load a program before starting a test.",
                        "No Active Program", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                lblStatusRunning.Text = "Running";
                lblStatusRunning.ForeColor = Color.Yellow;
                _startTime = DateTime.Now;
                lblStartTimeVal.Text = _startTime.ToString("dd MMM yyyy hh:mm:ss tt");
                lblEndTimeVal.Text = "-";

                _testStopwatch.Reset();
                _testStopwatch.Start();
                _elapsedTimer.Start();
                await Task.Run(() => ExecuteTest(activeConfig), token);

                if (token.IsCancellationRequested) return;


                PopulateLiveDetailsGrid(_loadedRows);
                try
                {
                    bool allPass = _loadedRows.Count > 0 && _loadedRows.TrueForAll(r => r.Status == LiveTestStatus.Pass);
                    string overallResult = allPass ? "PASS" : "FAIL";

                    var reportService = new TestReportService();
                    _currentReportId = reportService.SaveDraftLiveTestReport(
                        cmbProductModel.Text, cmbProductType.Text, cmbArea.Text, cmbEcnNo.Text, "1.0",
                        cmbPartNumber.Text, AppTime.UtcNow, _loadedRows, overallResult);

                    //ShowSummary(_loadedRows, overallResult);

                    Debug.WriteLine($"Draft report auto-saved to Test History as ReportId={_currentReportId}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to auto-save draft report: {ex.Message}");
                    MessageBox.Show(
                        "Test completed but failed to record it to Test History automatically.\nYou can still save it manually via Save Report.",
                        "Auto-Save Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (token.IsCancellationRequested) return;
                lblStatusRunning.Text = "Finished";
                lblStatusRunning.ForeColor = Color.Green;

                OnTestFinishedSuccessfully();


            }
            catch (OperationCanceledException)
            {
                // Aborted from btnStopTest, nothing left to report.
            }
            finally 
            {
                btnLoadProgram.Enabled = true;
                btnStartTest.Enabled = true;
                btnSaveReport.Enabled = true;
            }
        }

        private void ExecuteTest(TestConfiguration config ,System.Threading.CancellationToken token = default)
        {
            try
            {
                if (token.IsCancellationRequested) return;

                bool isConnected = _instrument.Connect();
                if (!isConnected)
                {
                    logService.Log(LogSeverity.Error, "Instrument", "Cirris Tester API Is Disconnected",
                        $"Failed to start test becauses Cirris Tester API is disconnected", SessionManager.CurrentUser?.Username ?? "Unknown");
                    MessageBox.Show("Failed to connect to instrument.", "Connection Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (token.IsCancellationRequested) return;
                _customTestText = _configService.ConvertToProgramText(config);
                Debug.WriteLine(_customTestText);

                if (token.IsCancellationRequested) return;

                // ======================= put test =======================
                var putResult = _instrument.PutTestProgram(string.Empty, _customTestText, true);
                string errorText = "";
                try
                {

                    if (putResult == CtaCommandResultCode.Success)
                    {
                        logService.Log(LogSeverity.Info, "Instrument", "Put Test Program Success.",
                        $"Generated Program is valid", SessionManager.CurrentUser?.Username ?? "Unknown");
                    }
                    else
                    {
                        errorText = _instrument.GetLastError()?.Result ?? "";
                        logService.Log(LogSeverity.Warning, "Instrument", "Put Test Program Failed.",
                        $"Generated Program is invalid with error message {putResult.ToString()}, {errorText}", SessionManager.CurrentUser?.Username ?? "Unknown");
                        MessageBox.Show($"Generated Program is invalid. Error message : {putResult.ToString()}\n{errorText}", "Test Configuration Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (Exception)
                {
                    logService.Log(LogSeverity.Warning, "Instrument", "Convert Program Failed.",
                        $"Generated Program is invalid with error message {putResult.ToString()}, {errorText}", SessionManager.CurrentUser?.Username ?? "Unknown");
                    MessageBox.Show($"Generated Program is invalid. Error message : {putResult.ToString()}\n{errorText}", "Test Configuration Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (token.IsCancellationRequested) return;

                // ======================= single test =======================
                var testResult = _instrument.RunSingleTest(false, true);
                try
                {
                    if (testResult.DllCallResult == CtaCommandResultCode.Success)
                    {
                        logService.Log(LogSeverity.Info, "Instrument", "Run Test Program Success.",
                        $"Success running Test Program with test result : {testResult.TestResultCode}", SessionManager.CurrentUser?.Username ?? "Unknown");
                    }
                    else
                    {
                        errorText = _instrument.GetLastError()?.Result ?? "";
                        logService.Log(LogSeverity.Warning, "Instrument", "Run Test Program Failed.",
                        $"Failed running Test Program with error message {testResult.DllCallResult.ToString()}, {errorText}", SessionManager.CurrentUser?.Username ?? "Unknown");
                        if (errorText != "ctaSingleTest Command Failed with Error: Command Aborted")
                        {
                            MessageBox.Show($"Failed running Test Program with error message {testResult.DllCallResult.ToString()}\n{errorText}", "Test Configuration Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                        return;
                    }
                }
                catch (Exception)
                {
                    logService.Log(LogSeverity.Warning, "Instrument", "Run Test Program Failed.",
                        $"Failed running Test Program with error message {testResult.DllCallResult.ToString()}, {errorText}", SessionManager.CurrentUser?.Username ?? "Unknown");
                    MessageBox.Show($"Failed running Test Program with error message2 {testResult.DllCallResult.ToString()}\n{errorText}", "Test Configuration Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (token.IsCancellationRequested) return;

                // ======================= Measured Values =======================
                var measuredValuesResult = _instrument.GetMeasuredValues();
                _loadedRows = new List<LiveTestDetailRowDto>();
                try
                {
                    if (measuredValuesResult.DllCallResult == CtaCommandResultCode.Success && measuredValuesResult.ObjectReturnValue != null)
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
                                    FromPoint = $"{measurement.Point1}",
                                    ToPoint = $"{measurement.Point2}",
                                    TestType = rowTestType,
                                    MeasuredValue = Math.Round(measurement.Value1, 3),
                                    UpperLimit = _continuityResistance,
                                    Status = status
                                });
                            }
                        }

                        ApplyConfigNets(config);

                        logService.Log(LogSeverity.Info, "Instrument", "Get Test Masurement Success.",
                        $"Success getting test measurement with test result", SessionManager.CurrentUser?.Username ?? "Unknown");
                    }
                    else
                    {
                        errorText = _instrument.GetLastError()?.Result ?? "";
                        logService.Log(LogSeverity.Warning, "Instrument", "Get Test Masurement Failed.",
                        $"Failed getting test measurement with error message {measuredValuesResult.DllCallResult.ToString()}, {errorText}", SessionManager.CurrentUser?.Username ?? "Unknown");
                        MessageBox.Show($"Failed getting test measurement with error message {measuredValuesResult.DllCallResult.ToString()}, {errorText}", "Get Measurement Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (Exception)
                {
                    logService.Log(LogSeverity.Warning, "Instrument", "Get Test Masurement Failed.",
                        $"Failed getting test measurement with error message {measuredValuesResult.DllCallResult.ToString()}, {errorText}", SessionManager.CurrentUser?.Username ?? "Unknown");
                    MessageBox.Show($"Failed getting test measurement with error message {measuredValuesResult.DllCallResult.ToString()}, {errorText}", "Get Measurement Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                logService.Log(LogSeverity.Info, "Text Execution", "Start Test Success",
                        $"Success execute Start Test", SessionManager.CurrentUser?.Username ?? "Unknown");

                try 
                {
                    var errorResult = _instrument.GetTestErrors().ObjectReturnValue;
                    if (errorResult != null && errorResult.DllCallResult == CtaCommandResultCode.Success && errorResult.ErrorList != null)
                    {
                        var regex = new Regex(@"Net\s+(?<net>.+?):\s+(?<type>SHORT|OPEN)\s+(?<p1>\S+)\s+to\s+(?<p2>\S+)", RegexOptions.IgnoreCase);
                        foreach (var error in errorResult.ErrorList)
                        {
                            var match = regex.Match(error.Message);
                            if (match.Success)
                            {
                                string p1 = match.Groups["p1"].Value;
                                string p2 = match.Groups["p2"].Value;
                                
                                bool exists = _loadedRows.Any(r => 
                                    (r.FromPoint == p1 && r.ToPoint == p2) || 
                                    (r.FromPoint == p2 && r.ToPoint == p1));
                                
                                if (!exists)
                                {
                                    _loadedRows.Add(new LiveTestDetailRowDto
                                    {
                                        StepNo = _loadedRows.Count > 0 ? _loadedRows.Max(r => r.StepNo) + 1 : 1,
                                        NetId = match.Groups["net"].Value,
                                        TestPoint = $"{p1} \u2192 {p2}",
                                        FromPoint = p1,
                                        ToPoint = p2,
                                        TestType = match.Groups["type"].Value.ToUpper(),
                                        MeasuredValue = null,
                                        Status = LiveTestStatus.Fail
                                    });
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to parse test errors: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                logService.Log(LogSeverity.Error, "Text Execution", "Start Test Failed",
                        $"Start Test Failed with error message {ex.Message}", SessionManager.CurrentUser?.Username ?? "Unknown");
                MessageBox.Show($"Start Test Failed with error message {ex.Message}", "Start Test Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
        }

        private void ShowSummary(List<LiveTestDetailRowDto> rows, string overallResultStatus)
        {
            var summaryList = rows
             .GroupBy(r => r.TestType)
             .Select(g => new
             {
                 TestType = g.Key,
                 Total = g.Count(),
                 Pass = g.Count(r => r.Status.ToString().ToUpper() == "PASS"),
                 Fail = g.Count(r => r.Status.ToString().ToUpper() == "FAIL")
             })
             .ToList();


            listBoxTestSummary.Items.Clear();
            listBoxTestSummary.Items.Add($"TEST RESULT = {overallResultStatus}");

            var errorResult = _instrument.GetTestErrors().ObjectReturnValue;
            string errorText = "";
            try
            {
                if (errorResult.DllCallResult == CtaCommandResultCode.Success)
                {
                    foreach (var summary in summaryList)
                    {
                        if (summary.Pass == summary.Total)
                        {
                            listBoxTestSummary.Items.Add($"✔ {summary.TestType} ({summary.Pass}/{summary.Total})");
                        }
                        else
                        {
                            listBoxTestSummary.Items.Add($"✖ {summary.TestType} ({summary.Pass}/{summary.Total})");
                        }
                    }

                    if (errorResult?.ErrorList != null && errorResult.ErrorList.Count > 0)
                    {


                        listBoxTestSummary.Items.Add("");
                        listBoxTestSummary.Items.Add("TEST ERRORS");
                        listBoxTestSummary.Items.Add("");
                        foreach (var error in errorResult.ErrorList)
                        {
                            listBoxTestSummary.Items.Add(error.Message);
                        }
                    }

                    logService.Log(LogSeverity.Info, "Instrument", "Get Test Errors Success.",
                    $"Success getting Test Error", SessionManager.CurrentUser?.Username ?? "Unknown");
                }
                else
                {
                    errorText = _instrument.GetLastError()?.Result ?? "";
                    logService.Log(LogSeverity.Warning, "Instrument", "Get Test Errors Failed.",
                    $"Failed getting Test Errors with error message {errorResult.DllCallResult.ToString()}, {errorText}", SessionManager.CurrentUser?.Username ?? "Unknown");
                    MessageBox.Show($"Failed getting Test Errors with error message {errorResult.DllCallResult.ToString()}, {errorText}", "Get Test Errors Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            catch (Exception Ex)
            {
                logService.Log(LogSeverity.Error, "Text Execution", "Display Test Execute Result Failed",
                        $"Display Test Execute Result Failed with error message {Ex.Message}", SessionManager.CurrentUser?.Username ?? "Unknown");
                MessageBox.Show($"Display Test Execute Result Failed with error message {Ex.Message}", "Display Test Execute Result Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
        }
        private async void btnStopTest_Click(object sender, EventArgs e)
        {
            _testCts?.Cancel();

            var abortResult = await Task.Run(() => _instrument.Abort());
            string errorText = "";
            try
            {
                if (abortResult == CtaCommandResultCode.Success)
                {
                    lblStatusRunning.Text = "Aborted";
                    lblStatusRunning.ForeColor = Color.Red;
                    StopTestTiming();
                    lblStartTimeVal.Text = "-";
                    lblEndTimeVal.Text = "-";
                    listBoxTestSummary.Items.Clear();
                    gridLiveDetails.Rows.Clear();
                    _loadedRows.Clear();

                    MessageBox.Show("Test operation successfully aborted.", "Abort Test", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    logService.Log(LogSeverity.Info, "Text Execution", "Stop Test Program Success.",
                    $"Success stop test Program with test result : {abortResult}", SessionManager.CurrentUser?.Username ?? "Unknown");
                }
                else
                {
                    errorText = _instrument.GetLastError()?.Result ?? "";
                    logService.Log(LogSeverity.Error, "Text Execution", "Stop Test Failed",
                    $"Stop Test Failed with error message {abortResult.ToString()}, {errorText}", SessionManager.CurrentUser?.Username ?? "Unknown");
                    MessageBox.Show($"Stop Test Failed with error message {abortResult.ToString()}\n{errorText}", "Test Configuration Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                logService.Log(LogSeverity.Error, "Text Execution", "Stop Test Failed",
                        $"Stop Test Failed with error message {ex.Message}", SessionManager.CurrentUser?.Username ?? "Unknown");
                MessageBox.Show($"Stop Test Failed with error message {ex.Message}", "Start Test Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
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
                SessionManager.CurrentUser?.FullName ?? "Unknown",
                cmbPartNumber.Text,
                $"SN-{DateTime.Now:HHmmss}",
                overallResult,
                AppTime.UtcNow,
                _loadedRows ?? new List<LiveTestDetailRowDto>(),
                _currentReportId,
                txtNotes.Text
            );

            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                lblStatusRunning.Text = "Idle";
                lblStatusRunning.ForeColor = Color.Gray;
                listBoxTestSummary.Items.Clear();
                txtNotes.Clear();

                MessageBox.Show("Test Report saved successfully to the database!\nIt can now be viewed in Reports and Test History.", "Report Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void PopulateLiveDetailsGrid(List<LiveTestDetailRowDto> rows)
        {

            gridLiveDetails.Rows.Clear();

            Debug.WriteLine("Update Grid 2");

            foreach (var row in rows)
            {
                var values = new List<string> { row.StepNo.ToString() };
                values.AddRange(_resultColumns.Select(column => ResultCellValue(row, column)));
                values.Add(row.Status.ToString().ToUpperInvariant());

                int idx = gridLiveDetails.Rows.Add(values.ToArray());

                ApplyStatusStyle(gridLiveDetails.Rows[idx], row.Status);

                //Debug.WriteLine(row.MeasuredValue);
            }
            gridLiveDetails.Refresh();
        }

        // Maps a configurable column name onto the live row it is rendered from.
        private static string ResultCellValue(LiveTestDetailRowDto row, string column) => column switch
        {
            "Net-ID" => row.NetId,
            "Harness Group" => row.HarnessGroup,
            "From" => row.FromPoint,
            "Net From Label" => row.FromLabel,
            "To" => row.ToPoint,
            "Net To Label" => row.ToLabel,
            "Test Type" => row.TestType,
            "Lower Limit" => FormatNumber(row.LowerLimit),
            "Upper Limit" => FormatNumber(row.UpperLimit),
            "Expected Value" => row.ExpectedValue,
            "Tolerance" => row.Tolerance.HasValue ? $"±{row.Tolerance.Value.ToString("0.##", CultureInfo.InvariantCulture)}%" : "",
            "Measurement" => FormatNumber(row.MeasuredValue, "F3"),
            _ => ""
        };

        private static string FormatNumber(double? value, string format = "0.##") =>
            value?.ToString(format, CultureInfo.InvariantCulture) ?? "";
      
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
                default:
                    row.Cells["Status"].Style.BackColor = Color.FromArgb(241, 245, 249);
                    row.Cells["Status"].Style.ForeColor = Color.FromArgb(100, 116, 139);
                    break;
            }
        }
     
        private void SetupDiagramPointers()
        {
            picAreaDetail.Paint += PicAreaDetail_Paint;
            
            picMachineOverview.Click += PicMachineOverview_Click;
            picHarnessConn.Click += PicHarnessConn_Click;
            picMachineOverview.Cursor = Cursors.Hand;
            picHarnessConn.Cursor = Cursors.Hand;
            
            picMachineOverview.Paint += Pic_Paint_NoImage;
            picHarnessConn.Paint += Pic_Paint_NoImage;

            LoadImagesFromConfig();
        }

        private void Pic_Paint_NoImage(object? sender, PaintEventArgs e)
        {
            if (sender is PictureBox pb && pb.Image == null)
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle r = pb.ClientRectangle;

                using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(241, 245, 249)))
                {
                    g.FillRectangle(bgBrush, r);
                }

                using (Brush textBrush = new SolidBrush(Color.FromArgb(148, 163, 184)))
                {
                    StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    g.DrawString("No Image Available", AssemblyLineTester.Helpers.UIStyleHelper.Font10Bold, textBrush, r, sf);
                }
            }
        }

        private void LoadImagesFromConfig()
        {
            var configService = new TestConfigService();
            var activeConfig = configService.GetActiveConfig();
            if (activeConfig != null)
            {
                var harnessService = new AssemblyLineTester.Services.HarnessService();
                var harness = System.Linq.Enumerable.FirstOrDefault(harnessService.GetAllHarnesses(), h => h.ProductModel == activeConfig.ProductModel || h.Name == activeConfig.ConfigName);
                if (harness != null && harness.ImagePaths != null && harness.ImagePaths.Count > 0)
                {
                    try
                    {
                        if (System.IO.File.Exists(harness.ImagePaths[0]))
                            picMachineOverview.Image = Image.FromFile(harness.ImagePaths[0]);
                    } catch { }
                }

                if (harness != null)
                {
                    var connectors = AssemblyLineTester.Services.ConnectorService.GetConnectorsByHarness(harness.ProductHarnessId);
                    if (connectors != null && connectors.Count > 0)
                    {
                        var firstConn = System.Linq.Enumerable.FirstOrDefault(connectors, c => !string.IsNullOrEmpty(c.ImagePath));
                        if (firstConn != null && !string.IsNullOrEmpty(firstConn.ImagePath))
                        {
                            try
                            {
                                if (System.IO.File.Exists(firstConn.ImagePath))
                                    picHarnessConn.Image = Image.FromFile(firstConn.ImagePath);
                            } catch { }
                        }
                    }
                }
            }
        }

        private void PicAreaDetail_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle r = picAreaDetail.ClientRectangle;

            using (SolidBrush brush = new SolidBrush(Color.FromArgb(248, 250, 252)))
            {
                g.FillRectangle(brush, r);
            }

            using (Pen borderPen = new Pen(Color.FromArgb(226, 232, 240), 1))
            {
                g.DrawRectangle(borderPen, 0, 0, r.Width - 1, r.Height - 1);
            }

            string[,] details = new string[,]
            {
                { "🔧 Assembly Line:", "Assembly Line B" },
                { "🏢 Station ID:", "STATION-04B (ECU Route)" },
                { "📋 Harness Model:", "WH-ENG-001 (Rev 3)" },
                { "👤 Operator Session:", "OP-402 (Standard Shift)" },
                { "💻 Calibration Ver:", "ALT-ECU-v3.4.1" }
            };

            using (Brush bKey = new SolidBrush(Color.FromArgb(71, 85, 105)))
            {
                float startY = 30;
                float rowHeight = (r.Height - 60) / 5f;

                for (int i = 0; i < 5; i++)
                {
                    string key = details[i, 0];
                    string val = details[i, 1];

                    g.DrawString(key, AssemblyLineTester.Helpers.UIStyleHelper.Font95Bold, bKey, 30, startY + (i * rowHeight));

                    using (Brush bVal = new SolidBrush(i == 2 ? Color.FromArgb(13, 110, 253) : Color.FromArgb(15, 23, 42)))
                    {
                        g.DrawString(val, AssemblyLineTester.Helpers.UIStyleHelper.Font95Regular, bVal, 220, startY + (i * rowHeight));
                    }

                    if (i < 4)
                    {
                        using (Pen sepPen = new Pen(Color.FromArgb(241, 245, 249), 1))
                        {
                            g.DrawLine(sepPen, 20, startY + (i * rowHeight) + rowHeight - 5, r.Width - 20, startY + (i * rowHeight) + rowHeight - 5);
                        }
                    }
                }
            }
        }

        private void PicMachineOverview_Click(object? sender, EventArgs e)
        {
            System.Collections.Generic.List<string> paths = new System.Collections.Generic.List<string>();
            var configService = new TestConfigService();
            var activeConfig = configService.GetActiveConfig();
            if (activeConfig != null)
            {
                var harnessService = new AssemblyLineTester.Services.HarnessService();
                var harness = System.Linq.Enumerable.FirstOrDefault(harnessService.GetAllHarnesses(), h => h.ProductModel == activeConfig.ProductModel || h.Name == activeConfig.ConfigName);
                if (harness != null && harness.ImagePaths != null)
                {
                    paths.AddRange(harness.ImagePaths);
                }
            }

            using var modal = new AssemblyLineTester.Views.Modals.ImagePreviewModalForm(paths, "HARNESS PREVIEW");
            modal.ShowDialog(this);
        }

        private void PicHarnessConn_Click(object? sender, EventArgs e)
        {
            System.Collections.Generic.List<string> paths = new System.Collections.Generic.List<string>();
            var configService = new TestConfigService();
            var activeConfig = configService.GetActiveConfig();
            if (activeConfig != null)
            {
                var harnessService = new AssemblyLineTester.Services.HarnessService();
                var harness = System.Linq.Enumerable.FirstOrDefault(harnessService.GetAllHarnesses(), h => h.ProductModel == activeConfig.ProductModel || h.Name == activeConfig.ConfigName);
                if (harness != null)
                {
                    var connectors = AssemblyLineTester.Services.ConnectorService.GetConnectorsByHarness(harness.ProductHarnessId);
                    if (connectors != null)
                    {
                        foreach(var conn in connectors)
                        {
                            if (!string.IsNullOrEmpty(conn.ImagePath))
                                paths.Add(conn.ImagePath);
                        }
                    }
                }
            }

            using var modal = new AssemblyLineTester.Views.Modals.ImagePreviewModalForm(paths, "CONNECTOR PREVIEW");
            modal.ShowDialog(this);
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

        private void BtnViewWiringDiagram_Click(object? sender, EventArgs e)
        {
            using var modal = new AssemblyLineTester.Views.Modals.WiringPreviewModalForm(_loadedRows);
            modal.ShowDialog(this);
        }

        
    }
}