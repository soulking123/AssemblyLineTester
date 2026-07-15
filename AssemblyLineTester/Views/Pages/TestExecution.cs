using AssemblyLineTester.Models.Dto;
using AssemblyLineTester.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using System.Diagnostics;

namespace AssemblyLineTester.Views.Pages
{
    public partial class TestExecution : UserControl
    {
        private readonly InstrumentService _instrument = new();
        private List<LiveTestDetailRowDto> _loadedRows = new();
        private string _customTestText = string.Empty;
        private double _continuityResistance ;


        public TestExecution()
        {
            InitializeComponent();
            SetupFilterDropdowns();
            SetupGridLiveDetails();
            SetupDiagramPointers();
            btnLoadProgram.Click += BtnLoadProgram_Click;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                var configService = new TestConfigService();
                var activeConfig = configService.GetActiveConfig();
                //_customTestText = configService.ConvertToProgramText(activeConfig);
                //Debug.WriteLine(_customTestText);


                //if (activeConfig != null)
                //{
                //    var options = new System.Text.Json.JsonSerializerOptions
                //    {
                //        WriteIndented = true,
                //        ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
                //    };
                //    string json = System.Text.Json.JsonSerializer.Serialize(activeConfig, options);
                //    Debug.WriteLine("=== Active Config Found on Startup ===");
                //    Debug.WriteLine(json);
                //}
                //else
                //{
                //    Debug.WriteLine("=== No Active Config Found on Startup ===");
                //}
            }
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

            gridLiveDetails.RowTemplate.Height = 36;
            gridLiveDetails.ColumnHeadersHeight = 40;
            gridLiveDetails.EnableHeadersVisualStyles = false;
            gridLiveDetails.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249);
            gridLiveDetails.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);
            gridLiveDetails.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            gridLiveDetails.DefaultCellStyle.Font = new Font("Segoe UI", 9.2F);
        }

        private void BtnLoadProgram_Click(object? sender, EventArgs e)
        {
            using var modal = new Views.Modals.AutomatedTestConfigModalForm();
            modal.ShowDialog(this);
        }

        private void btnStartTest_Click(object? sender, EventArgs e)
        {
            // 1. Connect to the instrument
            bool isConnected = _instrument.Connect();
            if (!isConnected)
            {
                MessageBox.Show("Failed to connect to instrument.", "Connection Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string loadedProgramName = "1107061216";
            string testProgramDetails = string.Empty;

            // 2. Load the test program
            Debug.WriteLine(_customTestText);
            if (!string.IsNullOrEmpty(_customTestText))
            {
                // Upload custom test text without saving to database (using empty string as testName)
                var putResult = _instrument.PutTestProgram(string.Empty, _customTestText, true);
                if (putResult != CtaDotNet.Enums.CtaCommandResultCode.Success)
                {
                    _instrument.Disconnect();
                    MessageBox.Show($"Failed to upload custom test program. Error: {putResult}", "Upload Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                loadedProgramName = "Custom Wirelist (Active)";
                testProgramDetails = _customTestText;
                _continuityResistance = _instrument.GetContinuityResistance();

                Debug.WriteLine($"Nilai Continuity Resistance saat ini: {_continuityResistance}");
            }
            else
            {
                // Fallback: Fetch/load the specified test program "1107061216"
                //var getTestResult = _instrument.GetTestProgram("1107061216");
                //if (getTestResult != null && getTestResult.DllCallResult == CtaDotNet.Enums.CtaCommandResultCode.Success)
                //{
                //    testProgramDetails = getTestResult.Result;
                //}
            }

            // 3. Run Single Test (Hipot=false, Shorts=true)
            var testResult = _instrument.RunSingleTest(false, true);
            if (testResult == null || testResult.DllCallResult != CtaDotNet.Enums.CtaCommandResultCode.Success)
            {
                _instrument.Disconnect();
                MessageBox.Show($"Test run failed to execute. Error: {testResult?.DllCallResult.ToString() ?? "Unknown"}", "Test Run Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 4. Get Measured Values
            var measuredValuesResult = _instrument.GetMeasuredValues();
            _loadedRows = new List<LiveTestDetailRowDto>();

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
                        if (measurement.Value1 <= _continuityResistance )
                        {
                            status = LiveTestStatus.Pass;
                        }
                        else 
                        {
                            status = LiveTestStatus.Fail;
                        }

                        _loadedRows.Add(new LiveTestDetailRowDto
                        {
                            StepNo = step++,
                            NetId = measurement.Instruction ?? "",
                            TestPoint = $"{measurement.Point1} \u2192 {measurement.Point2}",
                            TestType = measurement.Instruction ?? "",
                            MeasuredValue = Math.Round(measurement.Value1, 3),
                            Status = status
                        });
                    }
                }
            }

            // 5. Repopulate the GridView to show the results
            PopulateLiveDetailsGrid(_loadedRows);

            // 6. Disconnect after test run
            _instrument.Disconnect();

            // 7. Show summary MessageBox
            string testStatusText = testResult.TestResultCode == CtaDotNet.Enums.extEnumTTesterAccessTestResult.ctatrPassed 
                ? "PASSED" 
                : "FAILED or INCOMPLETE";

        }

        private void PopulateLiveDetailsGrid(List<LiveTestDetailRowDto> rows)
        {
            gridLiveDetails.Rows.Clear();

            foreach (var row in rows)
            {
                int idx = gridLiveDetails.Rows.Add(
                    row.StepNo.ToString(),
                    row.TestPoint,
                    row.TestType,
                    row.MeasuredValue?.ToString("F3", System.Globalization.CultureInfo.InvariantCulture) ?? "",
                    row.Status.ToString().ToUpperInvariant());

                ApplyStatusStyle(gridLiveDetails.Rows[idx], row.Status);
            }
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
            pnlWiringPlaceholder.Paint += PnlWiringPlaceholder_Paint;

            // Hide the mock placeholder text label so we can paint directly on the panel
            lblWiringMockText.Visible = false;
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

            using (Font f = new Font("Segoe UI", 11F, FontStyle.Bold))
            using (Brush b = new SolidBrush(Color.FromArgb(71, 85, 105)))
            {
                StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString("🖥️ Assembly Test Station #4\n[Online & Active]", f, b, r, sf);
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

            using (Font fKey = new Font("Segoe UI", 9.5F, FontStyle.Bold))
            using (Font fVal = new Font("Segoe UI", 9.5F, FontStyle.Regular))
            using (Brush bKey = new SolidBrush(Color.FromArgb(71, 85, 105)))
            {
                float startY = 30;
                float rowHeight = (r.Height - 60) / 5f;

                for (int i = 0; i < 5; i++)
                {
                    string key = details[i, 0];
                    string val = details[i, 1];

                    g.DrawString(key, fKey, bKey, 30, startY + (i * rowHeight));

                    using (Brush bVal = new SolidBrush(i == 2 ? Color.FromArgb(13, 110, 253) : Color.FromArgb(15, 23, 42)))
                    {
                        g.DrawString(val, fVal, bVal, 220, startY + (i * rowHeight));
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

            using (Font f = new Font("Segoe UI", 10F, FontStyle.Bold))
            using (Brush b = new SolidBrush(Color.FromArgb(51, 65, 85)))
            {
                StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString("🔌 Connector Pinout: 24-Pin ECU Harness\nActive Pin: J1-23 ➔ J3-16", f, b, r, sf);
            }
        }

        private void PnlWiringPlaceholder_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle r = pnlWiringPlaceholder.ClientRectangle;

            using (SolidBrush brush = new SolidBrush(Color.FromArgb(248, 250, 252)))
            {
                g.FillRectangle(brush, r);
            }

            using (Pen gridPen = new Pen(Color.FromArgb(241, 245, 249), 1))
            {
                for (int x = 20; x < r.Width; x += 20)
                    g.DrawLine(gridPen, x, 0, x, r.Height);
                for (int y = 20; y < r.Height; y += 20)
                    g.DrawLine(gridPen, 0, y, r.Width, y);
            }

            using (Pen cablePen = new Pen(Color.FromArgb(13, 110, 253), 5))
            {
                using (Pen wireInnerPen = new Pen(Color.FromArgb(147, 197, 253), 1))
                {
                    float startX = r.Width * 0.25f;
                    float startY = r.Height * 0.45f;
                    float endX = r.Width * 0.75f;
                    float endY = r.Height * 0.45f;

                    PointF[] points = new PointF[]
                    {
                        new PointF(startX, startY),
                        new PointF(startX + (endX - startX) * 0.3f, startY),
                        new PointF(startX + (endX - startX) * 0.3f, startY + 60),
                        new PointF(startX + (endX - startX) * 0.7f, startY + 60),
                        new PointF(startX + (endX - startX) * 0.7f, endY),
                        new PointF(endX, endY)
                    };
                    g.DrawLines(cablePen, points);

                    for (int i = -3; i <= 3; i += 3)
                    {
                        if (i == 0) continue;
                        PointF[] offsetPoints = new PointF[points.Length];
                        for (int j = 0; j < points.Length; j++)
                        {
                            offsetPoints[j] = new PointF(points[j].X, points[j].Y + i);
                        }
                        g.DrawLines(wireInnerPen, offsetPoints);
                    }
                }
            }

            RectangleF n1 = new RectangleF(r.Width * 0.25f - 40, r.Height * 0.45f - 40, 80, 80);
            using (SolidBrush connBrush = new SolidBrush(Color.FromArgb(30, 41, 59)))
            {
                g.FillEllipse(connBrush, n1);
            }
            using (Pen borderPen = new Pen(Color.FromArgb(71, 85, 105), 3))
            {
                g.DrawEllipse(borderPen, n1);
            }

            RectangleF n2 = new RectangleF(r.Width * 0.75f - 40, r.Height * 0.45f - 40, 80, 80);
            using (SolidBrush connBrush = new SolidBrush(Color.FromArgb(30, 41, 59)))
            {
                g.FillEllipse(connBrush, n2);
            }
            using (Pen borderPen = new Pen(Color.FromArgb(71, 85, 105), 3))
            {
                g.DrawEllipse(borderPen, n2);
            }

            using (Font f = new Font("Segoe UI", 11F, FontStyle.Bold))
            using (Brush textBrush = new SolidBrush(Color.White))
            {
                StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString("J1", f, textBrush, n1, sf);
                g.DrawString("J3", f, textBrush, n2, sf);
            }

            using (Font detailFont = new Font("Segoe UI", 8.5F, FontStyle.Regular))
            using (Brush darkGrayBrush = new SolidBrush(Color.FromArgb(100, 116, 139)))
            {
                StringFormat sfTop = new StringFormat { Alignment = StringAlignment.Center };
                g.DrawString("ECU Input Plug\n(24-Pin Male)", detailFont, darkGrayBrush, r.Width * 0.25f, r.Height * 0.45f + 45, sfTop);
                g.DrawString("Main Harness\n(24-Pin Female)", detailFont, darkGrayBrush, r.Width * 0.75f, r.Height * 0.45f + 45, sfTop);
            }

            using (Font legendFont = new Font("Segoe UI", 9F, FontStyle.Bold))
            using (Brush greenBrush = new SolidBrush(Color.FromArgb(40, 167, 69)))
            using (Brush blueBrush = new SolidBrush(Color.FromArgb(13, 110, 253)))
            {
                g.DrawString("● Connected: J1 ➔ J3 Active Route", legendFont, greenBrush, 30, r.Height - 65);
                g.DrawString("● Active Loop: J1-23 to J3-16", legendFont, blueBrush, 30, r.Height - 40);
            }
        }

        
    }
}