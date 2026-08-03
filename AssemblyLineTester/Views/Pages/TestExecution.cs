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
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using static AssemblyLineTester.Models.SystemLog;

using Timer = System.Windows.Forms.Timer;



namespace AssemblyLineTester.Views.Pages
{
    public partial class TestExecution : UserControl
    {
        private readonly InstrumentService _instrument = new();
        private List<LiveTestDetailRowDto> _loadedRows = new();
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
            // Set timer to trigger every 1 second (1000 ms)
            _elapsedTimer.Interval = 1000;
            _elapsedTimer.Tick += ElapsedTimer_Tick;
        }
        private void ElapsedTimer_Tick(object? sender, EventArgs e)
        {
            // Update Elapsed Time display every 1 second (HH:mm:ss format)
            lblElapsedTimeVal.Text = _testStopwatch.Elapsed.ToString(@"hh\:mm\:ss");
        }

        private void OnTestFinishedSuccessfully()
        {
            // Update Status
            lblStatusRunning.Text = "Finished";
            lblStatusRunning.ForeColor = Color.Green;
            // Stop Stopwatch & Timer
            StopTestTiming();
        }
        private void StopTestTiming()
        {
            // 1. Stop Timer & Stopwatch
            _testStopwatch.Stop();
            _elapsedTimer.Stop();
            // 2. Record & Display End Time
            _endTime = DateTime.Now;
            lblEndTimeVal.Text = _endTime.ToString("dd MMM yyyy hh:mm:ss tt");
            // 3. Ensure final exact elapsed time is displayed
            lblElapsedTimeVal.Text = _testStopwatch.Elapsed.ToString(@"hh\:mm\:ss");
        }

        // Populates the filter dropdowns at the top of the page (Product Model, Type, Area, etc).
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

            try
            {
                TestConfiguration? activeConfig = _configService.GetActiveConfig();
                if (activeConfig != null)
                {
                    lblStatusRunning.Text = "Running";
                    lblStatusRunning.ForeColor = Color.Yellow;
                    _startTime = DateTime.Now;
                    lblStartTimeVal.Text = _startTime.ToString("dd MMM yyyy hh:mm:ss tt");
                    lblEndTimeVal.Text = "-";

                    _testStopwatch.Reset();
                    _testStopwatch.Start();
                    _elapsedTimer.Start();
                    await Task.Run(() => ExecuteTest(activeConfig), token);
                }
                if (token.IsCancellationRequested) return;


                PopulateLiveDetailsGrid(_loadedRows);
                try
                {
                    bool allPass = _loadedRows.Count > 0 && _loadedRows.TrueForAll(r => r.Status == LiveTestStatus.Pass);
                    string overallResult = allPass ? "PASS" : "FAIL";

                    var reportService = new TestReportService();
                    _currentReportId = reportService.SaveDraftLiveTestReport(
                        cmbProductModel.Text, cmbProductType.Text, cmbArea.Text, cmbEcnNo.Text, "1.0",
                        cmbPartNumber.Text, DateTime.Now, _loadedRows, overallResult);

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
                lblElapsedTimeVal.Text = "00:00:00";

                OnTestFinishedSuccessfully();


            }
            catch (OperationCanceledException)
            {
                // Task was canceled via btnAbort
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
                                    Status = status
                                });
                            }
                        }

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

            // 2. Send abort command to CTA instrument
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
                    lblElapsedTimeVal.Text = "-";
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
                "OP-402",
                cmbPartNumber.Text,
                $"SN-{DateTime.Now:HHmmss}",
                overallResult,
                DateTime.Now,
                _loadedRows ?? new List<LiveTestDetailRowDto>(),
                _currentReportId
            );

            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                lblStatusRunning.Text = "Idle";
                lblStatusRunning.ForeColor = Color.Gray;
                listBoxTestSummary.Items.Clear();

                MessageBox.Show("Test Report saved successfully to the database!\nIt can now be viewed in Reports and Test History.", "Report Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                //Debug.WriteLine(row.MeasuredValue);
            }
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

            using (LinearGradientBrush brush = new LinearGradientBrush(r, Color.FromArgb(241, 245, 249), Color.FromArgb(226, 232, 240), 45F))
            {
                g.FillRectangle(brush, r);
            }

            using (Pen pen = new Pen(Color.FromArgb(148, 163, 184), 2))
            {
                g.DrawRectangle(pen, 20, 20, r.Width - 40, r.Height - 40);
            }

            using (Brush b = new SolidBrush(Color.FromArgb(71, 85, 105)))
            {
                StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString("🖥️ Assembly Test Station #4\n[Online & Active]", AssemblyLineTester.Helpers.UIStyleHelper.Font11Bold, b, r, sf);
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

        private void PicHarnessConn_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle r = picHarnessConn.ClientRectangle;

            using (SolidBrush brush = new SolidBrush(Color.FromArgb(241, 245, 249)))
            {
                g.FillRectangle(brush, r);
            }

            using (Brush b = new SolidBrush(Color.FromArgb(51, 65, 85)))
            {
                StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString("🔌 Connector Pinout: 24-Pin ECU Harness\nActive Pin: J1-23 ➔ J3-16", AssemblyLineTester.Helpers.UIStyleHelper.Font10Bold, b, r, sf);
            }
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