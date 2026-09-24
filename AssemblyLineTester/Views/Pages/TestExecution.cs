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
using System.Text.Json;
using System.Text.RegularExpressions;
using AssemblyLineTester.Helpers;
using static AssemblyLineTester.Models.SystemLog;

using Timer = System.Windows.Forms.Timer;



namespace AssemblyLineTester.Views.Pages
{
    public partial class TestExecution : UserControl
    {
        private readonly InstrumentService _instrument = new();
        private List<LiveTestDetailRowDto> _loadedRows = [];
        private List<string> _resultColumns = [];
        private readonly TestConfigService _configService = new TestConfigService();
        private string _customTestText = string.Empty;
        private double _continuityResistance;
        private int? _currentReportId;
        private System.Threading.CancellationTokenSource? _testCts;
        private readonly Stopwatch _testStopwatch = new Stopwatch();
        private readonly Timer _elapsedTimer = new Timer();
        private DateTime _startTime;
        private DateTime _endTime;
        private readonly SystemLogService logService = new();
        private TestConfiguration config = new();
        private DefaultTestSetting defaultTest = TestSettingService.GetDefaults();
        private int defaultTestPoint;

        public TestExecution()
        {
            InitializeComponent();
            SetupGridLiveDetails();
            SetupDiagramPointers();
            SetupProgramInfoArea();
            btnLoadProgram.Click += BtnLoadProgram_Click;
            SetupEvents();
            SetupTimer();
            this.VisibleChanged += TestExecution_VisibleChanged;
        }

        private void TestExecution_VisibleChanged(object? sender, EventArgs e)
        {
            if (this.Visible && !DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                RefreshActiveConfiguration();
            }
        }

        private void RefreshActiveConfiguration()
        {
            var configService = new TestConfigService();
            var activeConfig = configService.GetActiveConfig();
            if (activeConfig != null)
            {
                if (config == null || config.ConfigName != activeConfig.ConfigName || config.UpdatedAt != activeConfig.UpdatedAt)
                {
                    config = activeConfig;
                    _customTestText = configService.ConvertToProgramText(activeConfig, defaultTestPoint);
                    lblPageHeader.Text = $"TEST EXECUTION - {activeConfig.ConfigName}";
                    var resNet = activeConfig.Nets?.FirstOrDefault(n => n.UpperLimit.HasValue);
                    if (resNet != null && resNet.UpperLimit.HasValue)
                    {
                        _continuityResistance = resNet.UpperLimit.Value;
                    }
                    UpdateProgramInfoDisplay(activeConfig);
                    LoadConfigurationIntoGrid(activeConfig);
                    Debug.WriteLine($"Refreshed active config on visible: {activeConfig.ConfigName}");
                }
            }
            else if (config != null)
            {
                config = null;
                lblPageHeader.Text = "TEST EXECUTION - No Active Program Loaded";
                UpdateProgramInfoDisplay(null);
                LoadConfigurationIntoGrid(null);
            }

            // Outside the config-changed guard above: the thumbnails come from the harness and its
            // connectors, which the Harness Database can edit without ever touching the config.
            LoadImagesFromConfig();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                RefreshActiveConfiguration();
                loadDefaultParameter();
            }
        }

        
        private void loadDefaultParameter()
        {
            defaultTest = TestSettingService.GetDefaults();
            defaultTestPoint = defaultTest.TotalTestPointsAvailable;
        }


        private void SetupTimer()
        {
            // Set timer to trigger every 1 second (1000 ms)
            _elapsedTimer.Interval = 1000;
            _elapsedTimer.Tick += ElapsedTimer_Tick;
        }
        private void ElapsedTimer_Tick(object sender, EventArgs e)
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

        // Rebuilds the live grid header after the result-column ticks were saved in Settings.
        public void ReloadResultColumns()
        {
            SetupGridLiveDetails();
            if (config != null && config.Nets != null && config.Nets.Count > 0)
            {
                LoadConfigurationIntoGrid(config);
            }
        }

        // Sets up grid columns and styling only. No rows are added here -
        // the grid stays empty until a config file is loaded via Load Program.
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

                double? lower = net.LowerLimit;
                double? upper = net.UpperLimit;
                if (testTypeName.Equals("CAPACITOR", StringComparison.OrdinalIgnoreCase) || testTypeName.Equals("RESISTOR", StringComparison.OrdinalIgnoreCase))
                {
                    if ((!lower.HasValue || lower.Value == 0) && (!upper.HasValue || upper.Value == 0) && net.ExpectedValue.HasValue && net.Tolerance.HasValue)
                    {
                        double tol = Math.Abs(net.Tolerance.Value);
                        lower = net.ExpectedValue.Value * (1.0 - (tol / 100.0));
                        upper = net.ExpectedValue.Value * (1.0 + (tol / 100.0));
                    }
                }

                string expValStr = "-";
                if (testTypeName.Equals("DIODE", StringComparison.OrdinalIgnoreCase))
                {
                    double fwdExp = net.ExpectedValue ?? 0.70;
                    double revExp = net.ExpectedValue2 ?? 6.00;
                    expValStr = $"{FormatLimitValue(fwdExp, testTypeName)}, > {FormatLimitValue(revExp, testTypeName)}";
                }
                else if (net.ExpectedValue.HasValue)
                {
                    expValStr = testTypeName.Equals("CAPACITOR", StringComparison.OrdinalIgnoreCase) || (net.ExpectedValue.Value > 0 && net.ExpectedValue.Value < 0.01)
                        ? net.ExpectedValue.Value.ToString("0.##########", CultureInfo.InvariantCulture)
                        : net.ExpectedValue.Value.ToString(CultureInfo.InvariantCulture);
                }

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
                    LowerLimit = lower,
                    UpperLimit = upper,
                    LowerLimit2 = net.LowerLimit2,
                    UpperLimit2 = net.UpperLimit2,
                    ExpectedValue = expValStr,
                    Tolerance = net.Tolerance,
                    Tolerance2 = net.Tolerance2,
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
                row.Tolerance = net.Tolerance;
                row.Tolerance2 = net.Tolerance2;
                row.Unit = net.MeasurementUnit;
                row.Color = net.Color;
                row.Gauge = net.Gauge;

                double? lower = net.LowerLimit;
                double? upper = net.UpperLimit;
                string testTypeName = net.TestType?.TestTypeName ?? row.TestType;
                if (testTypeName.Equals("CAPACITOR", StringComparison.OrdinalIgnoreCase) || testTypeName.Equals("RESISTOR", StringComparison.OrdinalIgnoreCase))
                {
                    if ((!lower.HasValue || lower.Value == 0) && (!upper.HasValue || upper.Value == 0) && net.ExpectedValue.HasValue && net.Tolerance.HasValue)
                    {
                        double tol = Math.Abs(net.Tolerance.Value);
                        lower = net.ExpectedValue.Value * (1.0 - (tol / 100.0));
                        upper = net.ExpectedValue.Value * (1.0 + (tol / 100.0));
                    }
                }
                row.LowerLimit = lower;
                row.UpperLimit = upper;
                row.LowerLimit2 = net.LowerLimit2;
                row.UpperLimit2 = net.UpperLimit2;

                if (testTypeName.Equals("DIODE", StringComparison.OrdinalIgnoreCase))
                {
                    double fwdExp = net.ExpectedValue ?? 0.70;
                    double revExp = net.ExpectedValue2 ?? 6.00;
                    row.ExpectedValue = $"{FormatLimitValue(fwdExp, testTypeName)}, > {FormatLimitValue(revExp, testTypeName)}";
                }
                else if (net.ExpectedValue.HasValue)
                {
                    row.ExpectedValue = testTypeName.Equals("CAPACITOR", StringComparison.OrdinalIgnoreCase) || (net.ExpectedValue.Value > 0 && net.ExpectedValue.Value < 0.01)
                        ? net.ExpectedValue.Value.ToString("0.##########", CultureInfo.InvariantCulture)
                        : net.ExpectedValue.Value.ToString(CultureInfo.InvariantCulture);
                }
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
                    _customTestText = configService.ConvertToProgramText(dlg.SelectedConfiguration, defaultTestPoint);
                    var resNet = dlg.SelectedConfiguration.Nets?.FirstOrDefault(n => n.UpperLimit.HasValue);
                    config = configService.GetConfigurationByName(dlg.SelectedConfiguration.ConfigName) ?? new();

                    if (resNet != null && resNet.UpperLimit.HasValue)
                    {
                        _continuityResistance = resNet.UpperLimit.Value;
                    }

                    UpdateProgramInfoDisplay(config);
                    lblPageHeader.Text = $"TEST EXECUTION - {config.ConfigName}";
                    LoadConfigurationIntoGrid(config);
                    LoadImagesFromConfig();

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
                            _customTestText = configService.ConvertToProgramText(tempConfig, defaultTestPoint);
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
        private async void BtnStartTest_Click(object? sender, EventArgs e)
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
                listBoxTestSummary.Items.Clear();

                if (_loadedRows != null && _loadedRows.Count > 0)
                {
                    foreach (var row in _loadedRows)
                    {
                        row.MeasuredValue = null;
                        row.MeasurementPrefix = null;
                        row.Status = LiveTestStatus.Pending;
                    }
                    PopulateLiveDetailsGrid(_loadedRows);
                }

                _testStopwatch.Reset();
                _testStopwatch.Start();
                _elapsedTimer.Start();
                await Task.Run(() => ExecuteTest(activeConfig, token), token);

                if (token.IsCancellationRequested) return;

                StopTestTiming();

                PopulateLiveDetailsGrid(_loadedRows);
                try
                {
                    bool allPass = _loadedRows.Count > 0 && _loadedRows.TrueForAll(r => r.Status == LiveTestStatus.Pass);
                    var getErrorsResp = _instrument.GetTestErrors();
                    if (getErrorsResp?.ObjectReturnValue?.ErrorList != null && getErrorsResp.ObjectReturnValue.ErrorList.Count > 0)
                    {
                        allPass = false;
                    }
                    string overallResult = allPass ? "PASS" : "FAIL";

                    ShowSummary(_loadedRows, overallResult);

                    string summaryText = string.Join("\r\n", listBoxTestSummary.Items.Cast<object>().Select(x => x.ToString()));

                    DateTime startUtc = AppTime.ToUtc(_startTime);
                    DateTime endUtc = AppTime.ToUtc(_endTime);
                    if (endUtc <= startUtc && _testStopwatch.Elapsed.TotalSeconds > 0)
                    {
                        endUtc = startUtc.Add(_testStopwatch.Elapsed);
                    }

                    var reportService = new TestReportService();
                    _currentReportId = reportService.SaveDraftLiveTestReport(
                        activeConfig.ProductModelName, "N/A", "N/A", "N/A", "1.0",
                        "N/A", startUtc, _loadedRows, overallResult, errorSummary: summaryText, endDateTime: endUtc);

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
                _customTestText = _configService.ConvertToProgramText(config, defaultTestPoint);
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

                // Call RunSingleTest to execute the program on hardware so CTA populates test errors (shorts, opens, etc.)
                var testResult = _instrument.RunSingleTest(false, config.ShortTest);
                if (testResult != null && testResult.DllCallResult == CtaCommandResultCode.Success)
                {
                    logService.Log(LogSeverity.Info, "Instrument", "Run Test Program Success.",
                        $"Success running Test Program with test result: {testResult.TestResultCode}", SessionManager.CurrentUser?.Username ?? "Unknown");

                }
                else
                {
                    string errorText = _instrument.GetLastError()?.Result ?? "";
                    logService.Log(LogSeverity.Warning, "Instrument", "Run Test Program Completed with Warnings/Errors.",
                        $"Run Single Test finished with code: {testResult?.DllCallResult.ToString() ?? "Unknown"}, {errorText}", SessionManager.CurrentUser?.Username ?? "Unknown");
                }

                if (token.IsCancellationRequested) return;

                // Retrieve all measured values in batch
                var measuredValuesResult = _instrument.GetMeasuredValues();
                if (measuredValuesResult != null && measuredValuesResult.DllCallResult == CtaCommandResultCode.Success && measuredValuesResult.ObjectReturnValue != null)
                {
                    var list = measuredValuesResult.ObjectReturnValue.MeasuredValueList ?? [];
                    int count = Math.Min(_loadedRows.Count, list.Count);

                    for (int i = 0; i < count; i++)
                    {
                        if (token.IsCancellationRequested) return;

                        var row = _loadedRows[i];
                        var measurement = list[i];
                        Debug.WriteLine(JsonSerializer.Serialize(measurement));

                        // 1. Verification check between row pin addresses and measurement Point1/Point2
                        CheckMeasurementRowMatch(row, measurement, i);

                        // 2. Extract measured value and prefix
                        double val = (double)measurement.Value1;
                        row.MeasuredValue = val;
                        string status1 = measurement.Status1?.Trim() ?? "";
                        if (status1.Contains(">")) row.MeasurementPrefix = "> ";
                        else if (status1.Contains("<")) row.MeasurementPrefix = "< ";
                        else row.MeasurementPrefix = null;

                        double val2 = (double)measurement.Value2;
                        row.MeasuredValue2 = val2;
                        string status2 = measurement.Status2?.Trim() ?? "";
                        if (status2.Contains(">")) row.MeasurementPrefix2 = "> ";
                        else if (status2.Contains("<")) row.MeasurementPrefix2 = "< ";
                        else row.MeasurementPrefix2 = null;

                        // 3. Evaluate Pass/Fail using software limits
                        string testType = (row.TestType ?? "WIRE").Trim().ToUpperInvariant();
                        bool isWire = testType.Equals("WIRE", StringComparison.OrdinalIgnoreCase) || testType.Equals("4WIRE", StringComparison.OrdinalIgnoreCase);
                        bool isSignatureWire = testType.Equals("SIGNATURE WIRE", StringComparison.OrdinalIgnoreCase);
                        bool isDiode = testType.Equals("DIODE", StringComparison.OrdinalIgnoreCase) || testType.Equals("SIGNATURE DIODE", StringComparison.OrdinalIgnoreCase);

                        if (isWire)
                        {
                            double maxWireRes = config.MaxWireResistance > 0 ? config.MaxWireResistance : _continuityResistance;
                            row.Status = (val <= maxWireRes) ? LiveTestStatus.Pass : LiveTestStatus.Fail;
                        }
                        else if (isSignatureWire)
                        {
                            double maxWireRes = (row.UpperLimit.HasValue && row.UpperLimit.Value > 0)
                                ? row.UpperLimit.Value
                                : (config.MaxWireResistance > 0 ? config.MaxWireResistance : _continuityResistance);
                            row.Status = (val <= maxWireRes) ? LiveTestStatus.Pass : LiveTestStatus.Fail;
                        }
                        else if (testType == "TWISTED PAIR")
                        {
                            row.Status = (val >= 0.5 || string.Equals(measurement.Status1, "PASS", StringComparison.OrdinalIgnoreCase))
                                ? LiveTestStatus.Pass
                                : LiveTestStatus.Fail;
                        }
                        else if (isDiode)
                        {
                            // Forward Voltage check: LowerLimit <= val <= UpperLimit
                            bool fwdPass = (!row.LowerLimit.HasValue || val >= row.LowerLimit.Value) &&
                                           (!row.UpperLimit.HasValue || val <= row.UpperLimit.Value);

                            // Reverse Voltage check: val2 >= LowerLimit2 or status2 indicates over range
                            bool revPass = (!row.LowerLimit2.HasValue || val2 >= row.LowerLimit2.Value || status2.Contains(">"));

                            row.Status = (fwdPass && revPass) ? LiveTestStatus.Pass : LiveTestStatus.Fail;
                        }
                        else
                        {
                            // RESISTOR, CAPACITOR, etc.
                            if (row.LowerLimit.HasValue && row.UpperLimit.HasValue)
                            {
                                row.Status = (val >= row.LowerLimit.Value && val <= row.UpperLimit.Value)
                                    ? LiveTestStatus.Pass
                                    : LiveTestStatus.Fail;
                            }
                            else
                            {
                                row.Status = LiveTestStatus.Pass;
                            }
                        }

                        UpdateLiveDetailsGridRow(i, row);
                    }

                    // If instrument returned fewer measurements than loaded rows, mark remaining as Fail
                    if (count < _loadedRows.Count)
                    {
                        for (int i = count; i < _loadedRows.Count; i++)
                        {
                            _loadedRows[i].Status = LiveTestStatus.Fail;
                            UpdateLiveDetailsGridRow(i, _loadedRows[i]);
                        }
                        logService.Log(LogSeverity.Warning, "Instrument", "Incomplete Measured Values",
                            $"Received {count} measured values but expected {_loadedRows.Count}.", SessionManager.CurrentUser?.Username ?? "Unknown");
                        ShowMessageBoxOnUI($"Instrument returned {count} of {_loadedRows.Count} measurements. Unmeasured connections were marked as FAIL.", "Measurement Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    // Failed to retrieve batch measurements: mark all rows as Fail
                    for (int i = 0; i < _loadedRows.Count; i++)
                    {
                        _loadedRows[i].Status = LiveTestStatus.Fail;
                        UpdateLiveDetailsGridRow(i, _loadedRows[i]);
                    }
                    string errStr = measuredValuesResult?.DllCallResult.ToString() ?? "Unknown";
                    logService.Log(LogSeverity.Error, "Instrument", "Get Measured Values Failed",
                        $"Failed to retrieve measured values: {errStr}", SessionManager.CurrentUser?.Username ?? "Unknown");
                    ShowMessageBoxOnUI($"Failed to retrieve measured values from instrument. Error: {errStr}\nAll rows marked as FAIL.", "Measurement Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            gridRow.Cells[colIdx].Value = FormatMeasuredRowValue(row);
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
                 Pass = g.Count(r => string.Equals(r.Status.ToString(), "PASS", StringComparison.OrdinalIgnoreCase)),
                 Fail = g.Count(r => string.Equals(r.Status.ToString(), "FAIL", StringComparison.OrdinalIgnoreCase))
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
                        var allErrors = errorResult.ErrorList.ToList();
                        var shortErrors = allErrors.Where(e => IsShortError(e)).ToList();
                        var otherErrors = allErrors.Except(shortErrors).ToList();

                        if (shortErrors.Count > 0 && !summaryList.Any(s => string.Equals(s.TestType, "SHORT", StringComparison.OrdinalIgnoreCase)))
                        {
                            listBoxTestSummary.Items.Add($"✖ SHORT ({shortErrors.Count})");
                        }

                        if (shortErrors.Count > 0)
                        {
                            listBoxTestSummary.Items.Add("");
                            listBoxTestSummary.Items.Add("SHORT CONNECTION ERRORS");
                            listBoxTestSummary.Items.Add("");
                            foreach (var error in shortErrors)
                            {
                                listBoxTestSummary.Items.Add(FormatErrorMessage(error));
                            }
                        }

                        if (otherErrors.Count > 0)
                        {
                            listBoxTestSummary.Items.Add("");
                            listBoxTestSummary.Items.Add("TEST ERRORS");
                            listBoxTestSummary.Items.Add("");
                            foreach (var error in otherErrors)
                            {
                                listBoxTestSummary.Items.Add(FormatErrorMessage(error));
                            }
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

        private static bool IsShortError(dynamic error)
        {
            try
            {
                string id = ((string?)error.ErrorID) ?? "";
                string msg = ((string?)error.Message) ?? "";
                return id == "3" || id == "156" || msg.Contains("SHORT", StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        private static string ConvertPointToPinNumber(string? point)
        {
            if (string.IsNullOrWhiteSpace(point)) return "";
            string clean = point.Replace("$", "").Trim().ToUpperInvariant();
            var match = Regex.Match(clean, @"^J(\d+)-(\d+)$");
            if (match.Success &&
                int.TryParse(match.Groups[1].Value, out int conn) &&
                int.TryParse(match.Groups[2].Value, out int pin) &&
                conn > 0 && pin > 0)
            {
                int address = (conn - 1) * 64 + pin;
                return $"Pin {address}";
            }
            if (int.TryParse(clean, out int directAddress) && directAddress > 0)
            {
                return $"Pin {directAddress}";
            }
            return point.Trim();
        }

        private static string FormatShortErrorMessage(dynamic error) => FormatErrorMessage(error);

        private static string FormatErrorMessage(dynamic error)
        {
            try
            {
                string rawP1 = ((string?)error.Point1)?.Replace("$", "").Trim() ?? "";
                string rawP2 = ((string?)error.Point2)?.Replace("$", "").Trim() ?? "";
                string p1 = ConvertPointToPinNumber(rawP1);
                string p2 = ConvertPointToPinNumber(rawP2);
                string msg = ((string?)error.Message) ?? "";

                string cleaned = Regex.Replace(msg, @"^#\d+\s*", "").Trim();

                // First, convert any connector-style addresses (J1-3, etc.) anywhere in cleaned message
                cleaned = Regex.Replace(cleaned, @"\bJ(\d+)-(\d+)\b", m =>
                {
                    if (int.TryParse(m.Groups[1].Value, out int conn) &&
                        int.TryParse(m.Groups[2].Value, out int pin) &&
                        conn > 0 && pin > 0)
                    {
                        return $"Pin {(conn - 1) * 64 + pin}";
                    }
                    return m.Value;
                }, RegexOptions.IgnoreCase);

                // If raw points were numbers or something else, replace them if present
                if (!string.IsNullOrEmpty(rawP1) && !string.IsNullOrEmpty(p1) && cleaned.Contains(rawP1))
                {
                    cleaned = Regex.Replace(cleaned, $@"\b{Regex.Escape(rawP1)}\b", p1);
                }
                if (!string.IsNullOrEmpty(rawP2) && !string.IsNullOrEmpty(p2) && cleaned.Contains(rawP2))
                {
                    cleaned = Regex.Replace(cleaned, $@"\b{Regex.Escape(rawP2)}\b", p2);
                }

                // If message had Net names instead of pin points (e.g. "Short Net2 Net6 0.1 Ohm" or "Open Net1 - Net2")
                // and neither p1 nor p2 is in cleaned yet:
                if (!string.IsNullOrEmpty(p1) && !string.IsNullOrEmpty(p2) &&
                    !cleaned.Contains(p1) && !cleaned.Contains(p2))
                {
                    // Pattern: <Command/Type> <Point1> [-] <Point2> [<Rest>]
                    var netPattern = new Regex(@"^([A-Za-z]+)\s+([^\s\-]+)\s*(?:-\s*)?([^\s]+)(.*)$");
                    var match = netPattern.Match(cleaned);
                    if (match.Success)
                    {
                        string cmd = match.Groups[1].Value;
                        string rest = match.Groups[4].Value.Trim();
                        cleaned = string.IsNullOrEmpty(rest) ? $"{cmd} {p1} {p2}" : $"{cmd} {p1} {p2} {rest}";
                    }
                    else if (cleaned.Equals("Short", StringComparison.OrdinalIgnoreCase) ||
                             cleaned.Equals("Open", StringComparison.OrdinalIgnoreCase) ||
                             cleaned.Equals("Miswire", StringComparison.OrdinalIgnoreCase))
                    {
                        cleaned = $"{cleaned} {p1} {p2}";
                    }
                    else if (string.IsNullOrWhiteSpace(cleaned))
                    {
                        cleaned = $"Error {p1} {p2}";
                    }
                }

                // Remove unwanted hyphens between pins (e.g. "Pin 3 - Pin 4" -> "Pin 3 Pin 4")
                cleaned = Regex.Replace(cleaned, @"(Pin\s+\d+)\s*-\s*(Pin\s+\d+)", "$1 $2");

                // If it's a short error and has no resistance value in text, fallback to Value1 if available
                if (cleaned.StartsWith("Short", StringComparison.OrdinalIgnoreCase) &&
                    !Regex.IsMatch(cleaned, @"(?:mOhm|Ohm|kOhm|MOhm|Ω|mΩ|kΩ|MΩ)", RegexOptions.IgnoreCase))
                {
                    try
                    {
                        double val = (double)error.Value1;
                        if (val > 0)
                        {
                            string resStr = val >= 1e6 ? (val / 1e6).ToString("0.#", CultureInfo.InvariantCulture) + " MOhm"
                                          : val >= 1e3 ? (val / 1e3).ToString("0.#", CultureInfo.InvariantCulture) + " kOhm"
                                          : val.ToString("0.#", CultureInfo.InvariantCulture) + " Ohm";
                            cleaned += $" {resStr}";
                        }
                    }
                    catch { }
                }

                // Normalize spaces
                cleaned = Regex.Replace(cleaned, @"[ \t]+", " ").Trim();

                if (!string.IsNullOrEmpty(cleaned)) return cleaned;
                if (!string.IsNullOrEmpty(p1) && !string.IsNullOrEmpty(p2)) return $"Error {p1} {p2}";
            }
            catch { }

            return error?.Message ?? "Test error";
        }
        private async void BtnStopTest_Click(object sender, EventArgs e)
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
                    listBoxTestSummary.Items.Clear();
                    if (_loadedRows != null && _loadedRows.Count > 0)
                    {
                        foreach (var row in _loadedRows)
                        {
                            row.MeasuredValue = null;
                            row.MeasurementPrefix = null;
                            row.Status = LiveTestStatus.Pending;
                        }
                        PopulateLiveDetailsGrid(_loadedRows);
                    }

                    MessageBox.Show("Test operation successfully aborted.", "Abort Test", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    logService.Log(LogSeverity.Info, "Text Execution", "Stop Test Program Success.",
                    $"Success stop test Program with test result : {abortResult}", SessionManager.CurrentUser?.Username ?? "Unknown");
                }
                else
                {
                    errorText = _instrument.GetLastError()?.Result ?? "";
                    logService.Log(LogSeverity.Error, "Text Execution", "Stop Test Failed",
                    $"Stop Test Failed with error message {abortResult}, {errorText}", SessionManager.CurrentUser?.Username ?? "Unknown");
                    MessageBox.Show($"Stop Test Failed with error message {abortResult}\n{errorText}", "Test Configuration Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private async void BtnSelfTest_Click(object? sender, EventArgs e)
        {
            if (_testCts != null)
            {
                MessageBox.Show("A test is currently running. Please stop the test before running a self test.", "Self Test Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnLoadProgram.Enabled = false;
                btnStartTest.Enabled = false;
                btnSelfTest.Enabled = false;
                btnSaveReport.Enabled = false;
                lblStatusRunning.Text = "RUNNING SELF TEST...";
                
                await System.Threading.Tasks.Task.Run(() =>
                {
                    // Simulated self test logic
                    System.Threading.Thread.Sleep(2000);
                    
                    if (!_instrument.IsConnected)
                    {
                        throw new Exception("Instrument not connected.");
                    }
                    
                    var res = _instrument.GetServerInformation();
                    if (res.DllCallResult != CtaDotNet.Enums.CtaCommandResultCode.Success)
                    {
                        throw new Exception("Failed to query instrument server information.");
                    }
                });
                
                MessageBox.Show("Self Test completed successfully.\nInstrument is functioning normally.", "Self Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Self Test Failed: {ex.Message}", "Self Test Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLoadProgram.Enabled = true;
                btnStartTest.Enabled = true;
                btnSelfTest.Enabled = true;
                btnSaveReport.Enabled = true;
                lblStatusRunning.Text = "READY";
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

            bool hasFailInSummary = listBoxTestSummary.Items.Cast<object>().Any(x =>
                x.ToString()?.Contains("TEST RESULT = FAIL", StringComparison.OrdinalIgnoreCase) == true ||
                x.ToString()?.Contains("✖") == true ||
                x.ToString()?.Equals("TEST ERRORS", StringComparison.OrdinalIgnoreCase) == true ||
                x.ToString()?.Equals("SHORT CONNECTION ERRORS", StringComparison.OrdinalIgnoreCase) == true);
            bool allPass = _loadedRows != null && _loadedRows.Count > 0 && _loadedRows.TrueForAll(r => r.Status == LiveTestStatus.Pass) && !hasFailInSummary;
            string overallResult = allPass ? "PASS" : "FAIL";

            string summaryText = string.Join("\r\n", listBoxTestSummary.Items.Cast<object>().Select(x => x.ToString()));

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
                _startTime != default ? _startTime : DateTime.Now,
                _loadedRows ?? new List<LiveTestDetailRowDto>(),
                _currentReportId,
                txtNotes.Text,
                summaryText
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
            "Lower Limit" => (string.Equals(row.TestType, "DIODE", StringComparison.OrdinalIgnoreCase) && row.LowerLimit2.HasValue)
                ? $"{FormatLimitValue(row.LowerLimit, row.TestType)}, > {FormatLimitValue(row.LowerLimit2, row.TestType)}"
                : FormatLimitValue(row.LowerLimit, row.TestType),
            "Upper Limit" => (string.Equals(row.TestType, "DIODE", StringComparison.OrdinalIgnoreCase) && row.UpperLimit2.HasValue)
                ? $"{FormatLimitValue(row.UpperLimit, row.TestType)}, > {FormatLimitValue(row.UpperLimit2, row.TestType)}"
                : FormatLimitValue(row.UpperLimit, row.TestType),
            "Expected Value" => FormatExpectedValue(row.ExpectedValue, row.TestType),
            // Wire is plain continuity, not a component with a rated value - tolerance means nothing here.
            "Tolerance" => string.Equals(row.TestType, "WIRE", StringComparison.OrdinalIgnoreCase)
                ? "-"
                : (string.Equals(row.TestType, "DIODE", StringComparison.OrdinalIgnoreCase) && row.Tolerance2.HasValue)
                    ? $"±{row.Tolerance?.ToString("0.##", CultureInfo.InvariantCulture)}%, ±{row.Tolerance2?.ToString("0.##", CultureInfo.InvariantCulture)}%"
                    : row.Tolerance.HasValue ? $"±{row.Tolerance.Value.ToString("0.##", CultureInfo.InvariantCulture)}%" : "",
            "Measurement" => FormatMeasuredRowValue(row),
            "Measured Value" => FormatMeasuredRowValue(row),
            "Measured" => FormatMeasuredRowValue(row),
            _ => ""
        };

        private static string FormatMeasuredRowValue(LiveTestDetailRowDto row)
        {
            string testType = (row.TestType ?? "").Trim().ToUpperInvariant();
            bool isDiode = testType.Equals("DIODE", StringComparison.OrdinalIgnoreCase) || testType.Equals("SIGNATURE DIODE", StringComparison.OrdinalIgnoreCase);

            if (isDiode && (row.MeasuredValue2.HasValue || row.MeasurementPrefix2 != null))
            {
                string fwdStr = FormatMeasuredValue(row.MeasuredValue, row.MeasurementPrefix);
                string revStr = FormatMeasuredValue(row.MeasuredValue2, row.MeasurementPrefix2);
                return $"{fwdStr}, {revStr}";
            }

            return FormatMeasuredValue(row.MeasuredValue, row.MeasurementPrefix);
        }

        private static string FormatMeasuredValue(double? val, string? prefix = null)
        {
            if (!val.HasValue) return "-";
            double v = val.Value;
            string numStr;
            if (Math.Abs(v) < 1e-12) numStr = "0.00";
            else if (Math.Abs(v) >= 0.01) numStr = v.ToString("F2", CultureInfo.InvariantCulture);
            else numStr = v.ToString("0.##########", CultureInfo.InvariantCulture);

            return (prefix ?? "") + numStr;
        }

        private static bool CheckMeasurementRowMatch(LiveTestDetailRowDto row, CtaDotNet.Models.CtaJsonResult measurement, int index)
        {
            string expectedFromPin = FormatPinAddress(row.FromPoint);
            string expectedToPin = FormatPinAddress(row.ToPoint);

            string actualP1 = NormalizePin(measurement.Point1);
            string actualP2 = NormalizePin(measurement.Point2);
            string expFrom = NormalizePin(expectedFromPin);
            string expTo = NormalizePin(expectedToPin);

            bool pinMatch = (!string.IsNullOrEmpty(actualP1) && !string.IsNullOrEmpty(actualP2)) &&
                            ((expFrom == actualP1 && expTo == actualP2) || (expFrom == actualP2 && expTo == actualP1));

            bool labelMatch = (!string.IsNullOrEmpty(row.FromLabel) &&
                              (row.FromLabel.Equals(measurement.Point1, StringComparison.OrdinalIgnoreCase) ||
                               row.FromLabel.Equals(measurement.NetName, StringComparison.OrdinalIgnoreCase)));

            bool isMatch = pinMatch || labelMatch;

            Debug.WriteLine($"[Row-Check #{index + 1}] Step {row.StepNo} ({expectedFromPin} -> {expectedToPin}) <=> CTA ({measurement.Point1} -> {measurement.Point2}, Net: {measurement.NetName}) => {(isMatch ? "MATCH OK" : "MISMATCH / VERIFY ORDER")}");

            return isMatch;
        }

        private static string FormatPinAddress(string? point)
        {
            if (string.IsNullOrWhiteSpace(point)) return "";
            if (int.TryParse(point, out int addr) && addr > 0)
            {
                int conn = ((addr - 1) / 64) + 1;
                int pin = ((addr - 1) % 64) + 1;
                return $"J{conn}-{pin}";
            }
            return point.Trim();
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

        private static string FormatNumber(double? value, string format = "0.##") =>
            value?.ToString(format, CultureInfo.InvariantCulture) ?? "";

        private static string FormatLimitValue(double? val, string testType)
        {
            if (!val.HasValue) return "";
            double v = val.Value;
            if (string.Equals(testType, "CAPACITOR", StringComparison.OrdinalIgnoreCase) || (Math.Abs(v) > 0 && Math.Abs(v) < 0.01))
            {
                return v.ToString("0.##########", CultureInfo.InvariantCulture);
            }
            if (string.Equals(testType, "DIODE", StringComparison.OrdinalIgnoreCase))
            {
                string s = v.ToString("0.####", CultureInfo.InvariantCulture);
                if (!s.Contains('.')) return s + ".00";
                var parts = s.Split('.');
                if (parts[1].Length < 2) return parts[0] + "." + parts[1].PadRight(2, '0');
                return s;
            }
            return v.ToString("0.##", CultureInfo.InvariantCulture);
        }

        private static string FormatExpectedValue(string? valStr, string testType)
        {
            if (string.IsNullOrWhiteSpace(valStr) || valStr == "-") return "-";
            if (double.TryParse(valStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double dVal))
            {
                if (string.Equals(testType, "CAPACITOR", StringComparison.OrdinalIgnoreCase) || (dVal > 0 && dVal < 0.01))
                {
                    return dVal.ToString("0.##########", CultureInfo.InvariantCulture);
                }
            }
            return valStr;
        }

        private static void ApplyStatusStyle(DataGridViewRow row, LiveTestStatus status)
        {
            switch (status)
            {
                case LiveTestStatus.Pass:
                    row.Cells["Status"].Style.BackColor = Color.FromArgb(209, 231, 221);
                    row.Cells["Status"].Style.ForeColor = Color.FromArgb(15, 81, 50);
                    break;
                case LiveTestStatus.Fail:
                    row.Cells["Status"].Style.BackColor = Color.FromArgb(248, 215, 218);
                    row.Cells["Status"].Style.ForeColor = Color.FromArgb(132, 32, 41);
                    break;
                case LiveTestStatus.Testing:
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 251, 235);
                    row.Cells["Status"].Style.BackColor = Color.FromArgb(254, 240, 138);
                    row.Cells["Status"].Style.ForeColor = Color.FromArgb(133, 77, 14);
                    break;
                default: // Pending
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

                using Brush textBrush = new SolidBrush(Color.FromArgb(148, 163, 184));
                StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString("No Image Available", AssemblyLineTester.Helpers.UIStyleHelper.Font10Bold, textBrush, r, sf);
            }
        }

        // The harness the two thumbnails and their modals are built from: whichever ProductHarness
        // matches the active config's product model. One query, called from every place that needs
        // it, so a change to the matching rule only has to happen here.
        private static ProductHarness? GetHarnessForActiveConfig()
        {
            var activeConfig = new TestConfigService().GetActiveConfig();
            if (activeConfig == null) return null;

            return new AssemblyLineTester.Services.HarnessService().GetAllHarnesses()
                .FirstOrDefault(h => h.ProductModel == activeConfig.ProductModelName || h.Name == activeConfig.ConfigName);
        }

        private void LoadImagesFromConfig()
        {
            var harness = GetHarnessForActiveConfig();
            if (harness == null) return;

            string? harnessImage = harness.ImagePaths?
                .Select(AssemblyLineTester.Services.HarnessService.ResolveImagePath)
                .FirstOrDefault(p => p != null);
            AssemblyLineTester.Helpers.ImageLoader.SetImage(picMachineOverview, harnessImage);

            // J1's own first photo, matching the order the modal below walks the full set in.
            var connectors = AssemblyLineTester.Services.ConnectorService.GetConnectorsByHarness(harness.ProductHarnessId);
            string? firstConnectorImage = connectors
                .SelectMany(c => AssemblyLineTester.Services.ConnectorService.GetConnectorImagePaths(c))
                .FirstOrDefault();
            AssemblyLineTester.Helpers.ImageLoader.SetImage(picHarnessConn, firstConnectorImage);
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

            using Brush bKey = new SolidBrush(Color.FromArgb(71, 85, 105));
            float startY = 30;
            float rowHeight = (r.Height - 60) / 5f;

            for (int i = 0; i < 5; i++)
            {
                string key = details[i, 0];
                string val = details[i, 1];

                g.DrawString(key, AssemblyLineTester.Helpers.UIStyleHelper.Font95Bold, bKey, 30, startY + (i * rowHeight));

                using Brush bVal = new SolidBrush(i == 2 ? Color.FromArgb(13, 110, 253) : Color.FromArgb(15, 23, 42));
                g.DrawString(val, AssemblyLineTester.Helpers.UIStyleHelper.Font95Regular, bVal, 220, startY + (i * rowHeight));

                if (i < 4)
                {
                    using Pen sepPen = new Pen(Color.FromArgb(241, 245, 249), 1);
                    g.DrawLine(sepPen, 20, startY + (i * rowHeight) + rowHeight - 5, r.Width - 20, startY + (i * rowHeight) + rowHeight - 5);
                }
            }
        }

        private void PicMachineOverview_Click(object? sender, EventArgs e)
        {
            var harness = GetHarnessForActiveConfig();
            var paths = harness?.ImagePaths?
                .Select(AssemblyLineTester.Services.HarnessService.ResolveImagePath)
                .Where(p => p != null).Select(p => p!).ToList() ?? new List<string>();

            using var modal = new AssemblyLineTester.Views.Modals.ImagePreviewModalForm(paths, "HARNESS PREVIEW");
            modal.ShowDialog(this);
        }

        private void PicHarnessConn_Click(object? sender, EventArgs e)
        {
            var harness = GetHarnessForActiveConfig();
            var paths = new List<string>();
            if (harness != null)
            {
                // Every photo the connector at J1 has, then J2's, and so on - GetConnectorsByHarness
                // already returns them in attach position order.
                var connectors = AssemblyLineTester.Services.ConnectorService.GetConnectorsByHarness(harness.ProductHarnessId);
                foreach (var conn in connectors)
                {
                    paths.AddRange(AssemblyLineTester.Services.ConnectorService.GetConnectorImagePaths(conn));
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
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnSelfTest, 12, Color.FromArgb(13, 148, 136), Color.FromArgb(45, 212, 191), Color.FromArgb(15, 118, 110), Color.Transparent);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnSaveReport, 12, Color.FromArgb(37, 99, 235), Color.FromArgb(59, 130, 246), Color.FromArgb(29, 78, 216), Color.Transparent);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnViewPinMap, 8, Color.FromArgb(37, 99, 235), Color.FromArgb(59, 130, 246), Color.FromArgb(29, 78, 216), Color.Transparent);
            AssemblyLineTester.Helpers.UIStyleHelper.SetupButtonRoundedEdges(btnViewWiringDiagram, 8, Color.FromArgb(37, 99, 235), Color.FromArgb(59, 130, 246), Color.FromArgb(29, 78, 216), Color.Transparent);

            btnViewWiringDiagram.Click += BtnViewWiringDiagram_Click;
            btnViewPinMap.Click += BtnViewPinMap_Click;
            btnSaveReport.Click += BtnSaveReport_Click;
        }

        private void BtnViewWiringDiagram_Click(object? sender, EventArgs e)
        {
            using var modal = new AssemblyLineTester.Views.Modals.WiringPreviewModalForm(_loadedRows, false);
            modal.ShowDialog(this);
        }

        private void BtnViewPinMap_Click(object? sender, EventArgs e)
        {
            using var modal = new AssemblyLineTester.Views.Modals.WiringPreviewModalForm(_loadedRows, true);
            modal.ShowDialog(this);
        }

        private TableLayoutPanel tblProgramInfo = new();
        private readonly Label lblConfigNameVal = new();
        private readonly Label lblProductModelVal = new();
        private readonly Label lblCreatedByVal = new();
        private readonly Label lblUpdatedAtVal = new();
        private readonly Label lblTotalNetsVal = new();
        private readonly Label lblTestTypesVal = new();
        private readonly Label lblMaxWireResVal = new();
        private readonly Label lblMinCompResVal = new();
        private readonly Label lblMinInsResVal = new();
        private readonly Label lblHipotVoltageVal = new();
        private readonly Label lblShortTestVal = new();
        private readonly Label lblDescriptionVal = new();

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
            AddProgramInfoRow("Short Test:", lblShortTestVal);
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
                lblHipotVoltageVal.Text = "-";
                lblShortTestVal.Text = "-";
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

            lblHipotVoltageVal.Text = cfg.HipotVoltage ? "Enabled" : "Disabled";
            lblShortTestVal.Text = cfg.ShortTest ? "Enabled" : "Disabled";
            lblDescriptionVal.Text = !string.IsNullOrWhiteSpace(cfg.Description) ? cfg.Description : "-";
        }
    }
}