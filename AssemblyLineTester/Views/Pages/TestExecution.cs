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
        private TestConfiguration config = new();

        public TestExecution()
        {
            InitializeComponent();
            SetupGridLiveDetails();
            SetupDiagramPointers();
            SetupProgramInfoArea();
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
                    config = activeConfig;
                    _customTestText = configService.ConvertToProgramText(activeConfig);
                    lblPageHeader.Text = $"TEST EXECUTION - {activeConfig.ConfigName}";
                    var resNet = activeConfig.Nets?.FirstOrDefault(n => n.UpperLimit.HasValue);
                    if (resNet != null && resNet.UpperLimit.HasValue)
                    {
                        _continuityResistance = resNet.UpperLimit.Value;
                    }
                    UpdateProgramInfoDisplay(activeConfig);
                    LoadConfigurationIntoGrid(activeConfig);
                    Debug.WriteLine($"Loaded active config on startup: {activeConfig.ConfigName}");
                }
                else
                {
                    lblPageHeader.Text = "TEST EXECUTION - No Active Program Loaded";
                    UpdateProgramInfoDisplay(null);
                    LoadConfigurationIntoGrid(null);
                }
            }
        }

        private void SetupTimer()
        {
            _elapsedTimer.Interval = 1000;
            _elapsedTimer.Tick += ElapsedTimer_Tick;
        }

        private void ElapsedTimer_Tick(object? sender, EventArgs e)
        {
            lblElapsedTimeVal.Text = _testStopwatch.Elapsed.ToString(@"hh\:mm\:ss");
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
            lblElapsedTimeVal.Text = _testStopwatch.Elapsed.ToString(@"hh\:mm\:ss");
        }



        // Rebuilds the live grid header after the result-column ticks were saved in Settings.
        public void ReloadResultColumns()
        {
            SetupGridLiveDetails();
            if (config != null && config.Nets != null && config.Nets.Count > 0)
            {
                LoadConfigurationIntoGrid(config);
            }
        }

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

        private void LoadConfigurationIntoGrid(TestConfiguration? configToLoad)
        {
            if (configToLoad == null || configToLoad.Nets == null || configToLoad.Nets.Count == 0)
            {
                _loadedRows.Clear();
                gridLiveDetails.Rows.Clear();
                return;
            }

            _loadedRows = new List<LiveTestDetailRowDto>();
            var sortedNets = configToLoad.Nets.OrderBy(n => n.SequenceNo).ToList();

            for (int i = 0; i < sortedNets.Count; i++)
            {
                var net = sortedNets[i];
                string fromPin = TestConfigService.FormatPin(net.FromAddress);
                string toPin = TestConfigService.FormatPin(net.ToAddress);
                string testTypeName = net.TestType?.TestTypeName ?? "WIRE";

                var dto = new LiveTestDetailRowDto
                {
                    StepNo = i + 1,
                    HarnessGroup = net.HarnessGroup ?? "",
                    NetId = net.NetId ?? "",
                    FromPoint = net.FromAddress.ToString(),
                    //FromPoint = fromPin,
                    FromLabel = net.FromLabel ?? "",
                    ToPoint = net.ToAddress.ToString(),
                    //ToPoint = toPin,
                    ToLabel = net.ToLabel ?? "",
                    TestType = testTypeName,
                    Color = net.Color ?? "",
                    Gauge = net.Gauge ?? "",
                    LowerLimit = net.LowerLimit,
                    UpperLimit = net.UpperLimit,
                    ExpectedValue = net.ExpectedValue.HasValue ? net.ExpectedValue.Value.ToString(CultureInfo.InvariantCulture) : "-",
                    Tolerance = net.Tolerance,
                    Unit = net.MeasurementUnit ?? "",
                    MeasuredValue = null,
                    Status = LiveTestStatus.Pending
                };

                _loadedRows.Add(dto);
            }

            PopulateLiveDetailsGrid(_loadedRows);
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
                row.Color = net.Color;
                row.Gauge = net.Gauge;
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
                    config = configService.GetConfigurationByName(dlg.SelectedConfiguration.ConfigName);

                    if (resNet != null && resNet.UpperLimit.HasValue)
                    {
                        _continuityResistance = resNet.UpperLimit.Value;
                    }

                    UpdateProgramInfoDisplay(config);
                    lblPageHeader.Text = $"TEST EXECUTION - {config.ConfigName}";
                    LoadConfigurationIntoGrid(config);

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
                            config = tempConfig;
                            _customTestText = configService.ConvertToProgramText(tempConfig);
                            var resNet = tempConfig.Nets.FirstOrDefault(n => n.UpperLimit.HasValue);
                            if (resNet != null && resNet.UpperLimit.HasValue)
                            {
                                _continuityResistance = resNet.UpperLimit.Value;
                            }
                            UpdateProgramInfoDisplay(tempConfig);
                            lblPageHeader.Text = $"TEST EXECUTION - {tempConfig.ConfigName}";
                            LoadConfigurationIntoGrid(tempConfig);
                        }
                        else
                        {
                            _customTestText = System.IO.File.ReadAllText(dlg.LoadedFilePath);
                            var textConfig = configService.ConvertToTestConfig(_customTestText);
                            textConfig.ConfigName = System.IO.Path.GetFileNameWithoutExtension(dlg.LoadedFilePath);
                            config = textConfig;
                            UpdateProgramInfoDisplay(textConfig);
                            lblPageHeader.Text = $"TEST EXECUTION - {textConfig.ConfigName}";
                            LoadConfigurationIntoGrid(textConfig);
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
                        activeConfig.ProductModelName, "N/A", "N/A", "N/A", "1.0",
                        "N/A", AppTime.UtcNow, _loadedRows, overallResult);

                    ShowSummary(_loadedRows, overallResult);

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
                // Aborted from btnStopTest, nothing left to report.
            }
            finally 
            {
                btnLoadProgram.Enabled = true;
                btnStartTest.Enabled = true;
                btnSaveReport.Enabled = true;
            }
        }

        private void ExecuteTest(TestConfiguration config, System.Threading.CancellationToken token = default)
        {
            try
            {
                if (token.IsCancellationRequested) return;

                bool isConnected = _instrument.Connect();
                if (!isConnected)
                {
                    logService.Log(LogSeverity.Error, "Instrument", "Cirris Tester API Is Disconnected",
                        $"Failed to start test because Cirris Tester API is disconnected", SessionManager.CurrentUser?.Username ?? "Unknown");
                    ShowMessageBoxOnUI("Failed to connect to instrument.", "Connection Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (token.IsCancellationRequested) return;
                _customTestText = _configService.ConvertToProgramText(config);
                Debug.WriteLine(_customTestText);

                if (token.IsCancellationRequested) return;

                // Upload test program
                var putResult = _instrument.PutTestProgram(string.Empty, _customTestText, true);
                if (putResult != CtaCommandResultCode.Success)
                {
                    string errorText = _instrument.GetLastError()?.Result ?? "";
                    logService.Log(LogSeverity.Warning, "Instrument", "Put Test Program Failed.",
                        $"Generated Program is invalid with error message {putResult}, {errorText}", SessionManager.CurrentUser?.Username ?? "Unknown");
                    ShowMessageBoxOnUI($"Generated Program is invalid. Error message: {putResult}\n{errorText}", "Test Configuration Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                logService.Log(LogSeverity.Info, "Instrument", "Put Test Program Success.",
                    $"Generated Program is valid", SessionManager.CurrentUser?.Username ?? "Unknown");

                if (token.IsCancellationRequested) return;

                // Point-by-point measurements row by row
                for (int i = 0; i < _loadedRows.Count; i++)
                {
                    if (token.IsCancellationRequested) return;

                    var row = _loadedRows[i];
                    int.TryParse(row.FromPoint, out int fromPinInt);
                    int.TryParse(row.ToPoint, out int toPinInt);
                    string fromPin = TestConfigService.FormatPin(fromPinInt);
                    string toPin = TestConfigService.FormatPin(toPinInt);
                    //string toPin = row.ToPoint;
                    //string fromPin = row.FromPoint;
                    //string toPin = row.ToPoint;
                    string testType = (row.TestType ?? "WIRE").Trim().ToUpperInvariant();

                    bool isWire = testType.Equals("WIRE", StringComparison.OrdinalIgnoreCase) || testType.Equals("4WIRE", StringComparison.OrdinalIgnoreCase);

                    if (testType == "CAPACITOR")
                    {
                        dynamic res = _instrument.GetCapacitance(fromPin, toPin);
                        if (res != null && res.DllCallResult == CtaCommandResultCode.Success)
                        {
                            double val = (double)res.MeasuredValue;
                            row.MeasuredValue = Math.Round(val, 3);
                            if (row.LowerLimit.HasValue && row.UpperLimit.HasValue)
                            {
                                row.Status = (val >= row.LowerLimit.Value && val <= row.UpperLimit.Value) ? LiveTestStatus.Pass : LiveTestStatus.Fail;
                            }
                            else
                            {
                                row.Status = LiveTestStatus.Pass;
                            }
                        }
                        else
                        {
                            row.Status = LiveTestStatus.Fail;
                        }
                    }
                    else if (testType == "DIODE" || testType == "SIGNATURE DIODE")
                    {
                        dynamic res = _instrument.GetDiode(fromPin, toPin);
                        if (res != null && res.DllCallResult == CtaCommandResultCode.Success)
                        {
                            double val = (double)res.ForwardVoltage;
                            row.MeasuredValue = Math.Round(val, 3);
                            if (row.LowerLimit.HasValue && row.UpperLimit.HasValue)
                            {
                                row.Status = (val >= row.LowerLimit.Value && val <= row.UpperLimit.Value) ? LiveTestStatus.Pass : LiveTestStatus.Fail;
                            }
                            else
                            {
                                row.Status = LiveTestStatus.Pass;
                            }
                        }
                        else
                        {
                            row.Status = LiveTestStatus.Fail;
                        }
                    }
                    else if (testType == "TWISTED PAIR")
                    {
                        dynamic res = _instrument.GetTwistedPair(fromPin, toPin);
                        if (res != null && res.DllCallResult == CtaCommandResultCode.Success)
                        {
                            bool pass = (bool)res.Value;
                            row.Status = pass ? LiveTestStatus.Pass : LiveTestStatus.Fail;
                        }
                        else
                        {
                            row.Status = LiveTestStatus.Fail;
                        }
                    }
                    else
                    {
                        // Default: WIRE, 4WIRE, RESISTOR, 4RESISTOR, SIGNATURE WIRE
                        dynamic res = _instrument.GetRessistance(fromPin, toPin);
                        if (res != null && res.DllCallResult == CtaCommandResultCode.Success)
                        {
                            double val = (double)res.MeasuredValue;
                            row.MeasuredValue = Math.Round(val, 3);

                            if (isWire)
                            {
                                double maxWireRes = config.MaxWireResistance > 0 ? config.MaxWireResistance : _continuityResistance;
                                row.Status = (val <= maxWireRes) ? LiveTestStatus.Pass : LiveTestStatus.Fail;
                            }
                            else
                            {
                                if (row.LowerLimit.HasValue && row.UpperLimit.HasValue)
                                {
                                    row.Status = (val >= row.LowerLimit.Value && val <= row.UpperLimit.Value) ? LiveTestStatus.Pass : LiveTestStatus.Fail;
                                }
                                else
                                {
                                    row.Status = LiveTestStatus.Pass;
                                }
                            }
                        }
                        else
                        {
                            row.Status = LiveTestStatus.Fail;
                        }
                    }

                    UpdateLiveDetailsGridRow(i, row);
                }

                logService.Log(LogSeverity.Info, "Text Execution", "Start Test Success",
                    $"Success execute Start Test", SessionManager.CurrentUser?.Username ?? "Unknown");
            }
            catch (Exception ex)
            {
                logService.Log(LogSeverity.Error, "Text Execution", "Start Test Failed",
                    $"Start Test Failed with error message {ex.Message}", SessionManager.CurrentUser?.Username ?? "Unknown");
                ShowMessageBoxOnUI($"Start Test Failed with error message {ex.Message}", "Start Test Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateLiveDetailsGridRow(int rowIndex, LiveTestDetailRowDto row)
        {
            void Action()
            {
                if (rowIndex >= 0 && rowIndex < gridLiveDetails.Rows.Count)
                {
                    var gridRow = gridLiveDetails.Rows[rowIndex];
                    for (int colIdx = 0; colIdx < gridLiveDetails.Columns.Count; colIdx++)
                    {
                        string header = gridLiveDetails.Columns[colIdx].HeaderText;
                        if (header == "Status")
                        {
                            gridRow.Cells[colIdx].Value = row.Status.ToString().ToUpperInvariant();
                        }
                        else if (header == "Measured Value" || header == "Measurement" || header == "Measured")
                        {
                            gridRow.Cells[colIdx].Value = row.MeasuredValue.HasValue ? row.MeasuredValue.Value.ToString("F2", CultureInfo.InvariantCulture) : "-";
                        }
                        else if (header == "#")
                        {
                            gridRow.Cells[colIdx].Value = row.StepNo.ToString();
                        }
                        else
                        {
                            gridRow.Cells[colIdx].Value = ResultCellValue(row, header);
                        }
                    }

                    ApplyStatusStyle(gridRow, row.Status);
                    gridLiveDetails.InvalidateRow(rowIndex);
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

            var getErrorsResponse = _instrument.GetTestErrors();
            var errorResult = getErrorsResponse?.ObjectReturnValue;
            string errorText = "";
            try
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

                if (getErrorsResponse != null && getErrorsResponse.DllCallResult == CtaCommandResultCode.Success)
                {
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
                    string callResultStr = getErrorsResponse?.DllCallResult.ToString() ?? "NullResponse";
                    logService.Log(LogSeverity.Warning, "Instrument", "Get Test Errors Failed.",
                    $"Failed getting Test Errors with error message {callResultStr}, {errorText}", SessionManager.CurrentUser?.Username ?? "Unknown");
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

            TestConfiguration? activeConfig = _configService.GetActiveConfig();
            string productModel = activeConfig?.ProductModelName ?? "N/A";

            using var modal = new AssemblyLineTester.Views.Modals.SaveTestReportModalForm(
                productModel,
                "N/A",
                "N/A",
                "1.0",
                "N/A",
                SessionManager.CurrentUser?.FullName ?? "Unknown",
                "N/A",
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
            "Color" => row.Color,
            "Gauge" => row.Gauge,
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
                var harness = System.Linq.Enumerable.FirstOrDefault(harnessService.GetAllHarnesses(), h => h.ProductModel == activeConfig.ProductModelName || h.Name == activeConfig.ConfigName);
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
                var harness = System.Linq.Enumerable.FirstOrDefault(harnessService.GetAllHarnesses(), h => h.ProductModel == activeConfig.ProductModelName || h.Name == activeConfig.ConfigName);
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
                var harness = System.Linq.Enumerable.FirstOrDefault(harnessService.GetAllHarnesses(), h => h.ProductModel == activeConfig.ProductModelName || h.Name == activeConfig.ConfigName);
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

        private TableLayoutPanel tblProgramInfo = new();
        private Label lblConfigNameVal = new();
        private Label lblProductModelVal = new();
        private Label lblCreatedByVal = new();
        private Label lblUpdatedAtVal = new();
        private Label lblTotalNetsVal = new();
        private Label lblTestTypesVal = new();
        private Label lblMaxWireResVal = new();
        private Label lblMinCompResVal = new();
        private Label lblMinInsResVal = new();
        private Label lblHipotVoltageVal = new();
        private Label lblDescriptionVal = new();

        private void SetupProgramInfoArea()
        {
            lblAreaDetailTitle.Text = "📋 TEST PROGRAM INFORMATION";

            if (picAreaDetail != null)
            {
                picAreaDetail.Visible = false;
            }

            var pnlContainer = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(8),
                BackColor = Color.White
            };

            tblProgramInfo = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 2,
                Padding = new Padding(4)
            };
            tblProgramInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48F));
            tblProgramInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52F));

            AddProgramInfoRow("Config Name:", lblConfigNameVal);
            AddProgramInfoRow("Product Model:", lblProductModelVal);
            AddProgramInfoRow("Created By:", lblCreatedByVal);
            AddProgramInfoRow("Updated At:", lblUpdatedAtVal);
            AddProgramInfoRow("Total Nets:", lblTotalNetsVal);
            AddProgramInfoRow("Test Types:", lblTestTypesVal);
            AddProgramInfoRow("Max Wire Res.:", lblMaxWireResVal);
            AddProgramInfoRow("Min Component Res.:", lblMinCompResVal);
            AddProgramInfoRow("Min Insulation Res.:", lblMinInsResVal);
            AddProgramInfoRow("Hipot Voltage:", lblHipotVoltageVal);
            AddProgramInfoRow("Description:", lblDescriptionVal);

            pnlContainer.Controls.Add(tblProgramInfo);
            cardAreaDetail.Controls.Add(pnlContainer);
            pnlContainer.BringToFront();
        }

        private void AddProgramInfoRow(string key, Label valLabel)
        {
            var keyLabel = new Label
            {
                Text = key,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(71, 85, 105),
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Margin = new Padding(2, 4, 2, 4)
            };

            valLabel.Text = "-";
            valLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            valLabel.ForeColor = Color.FromArgb(30, 41, 59);
            valLabel.AutoSize = true;
            valLabel.Anchor = AnchorStyles.Left;
            valLabel.Margin = new Padding(2, 4, 2, 4);

            int row = tblProgramInfo.RowCount++;
            tblProgramInfo.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tblProgramInfo.Controls.Add(keyLabel, 0, row);
            tblProgramInfo.Controls.Add(valLabel, 1, row);
        }

        private void UpdateProgramInfoDisplay(TestConfiguration? cfg)
        {
            if (cfg == null)
            {
                lblConfigNameVal.Text = "-";
                lblProductModelVal.Text = "-";
                lblCreatedByVal.Text = "-";
                lblUpdatedAtVal.Text = "-";
                lblTotalNetsVal.Text = "0";
                lblTestTypesVal.Text = "-";
                lblMaxWireResVal.Text = "-";
                lblMinCompResVal.Text = "-";
                lblMinInsResVal.Text = "-";
                lblHipotVoltageVal.Text = "OFF";
                lblDescriptionVal.Text = "-";
                return;
            }

            lblConfigNameVal.Text = !string.IsNullOrWhiteSpace(cfg.ConfigName) ? cfg.ConfigName : "-";
            lblProductModelVal.Text = !string.IsNullOrWhiteSpace(cfg.ProductModelName) ? cfg.ProductModelName : "-";
            lblCreatedByVal.Text = !string.IsNullOrWhiteSpace(cfg.CreatedBy) ? cfg.CreatedBy : "-";
            lblUpdatedAtVal.Text = cfg.UpdatedAt.HasValue 
                ? cfg.UpdatedAt.Value.ToString("dd MMM yyyy HH:mm") 
                : cfg.CreatedAt.ToString("dd MMM yyyy HH:mm");
            lblTotalNetsVal.Text = (cfg.Nets?.Count ?? 0).ToString();

            if (cfg.TestTypes != null && cfg.TestTypes.Count > 0)
            {
                lblTestTypesVal.Text = string.Join(", ", cfg.TestTypes.Select(t => t.TestTypeName).Where(t => !string.IsNullOrWhiteSpace(t)).Distinct());
            }
            else if (cfg.Nets != null && cfg.Nets.Count > 0)
            {
                var types = cfg.Nets
                    .Select(n => n.TestType?.TestTypeName)
                    .Where(t => !string.IsNullOrWhiteSpace(t))
                    .Distinct();
                lblTestTypesVal.Text = types.Any() ? string.Join(", ", types) : "WIRE";
            }
            else
            {
                lblTestTypesVal.Text = "-";
            }

            lblMaxWireResVal.Text = cfg.MaxWireResistance > 0 ? $"{cfg.MaxWireResistance.ToString("0.##", CultureInfo.InvariantCulture)} ohm" : "-";
            lblMinCompResVal.Text = cfg.MinComponentResistance > 0 ? $"{cfg.MinComponentResistance.ToString("0.##", CultureInfo.InvariantCulture)} ohm" : "-";

            if (cfg.MinInsulationResistance >= 1e6)
                lblMinInsResVal.Text = $"{(cfg.MinInsulationResistance / 1e6).ToString("0.##", CultureInfo.InvariantCulture)} M ohm";
            else if (cfg.MinInsulationResistance >= 1e3)
                lblMinInsResVal.Text = $"{(cfg.MinInsulationResistance / 1e3).ToString("0.##", CultureInfo.InvariantCulture)} K ohm";
            else if (cfg.MinInsulationResistance > 0)
                lblMinInsResVal.Text = $"{cfg.MinInsulationResistance.ToString("0.##", CultureInfo.InvariantCulture)} ohm";
            else
                lblMinInsResVal.Text = "-";

            lblHipotVoltageVal.Text = cfg.HipotVoltage ? "ON" : "OFF";
            lblDescriptionVal.Text = !string.IsNullOrWhiteSpace(cfg.Description) ? cfg.Description : "-";
        }
    }
}