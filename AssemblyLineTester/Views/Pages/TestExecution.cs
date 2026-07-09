using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace AssemblyLineTester.Views.Pages
{
    public partial class TestExecution : UserControl
    {
        public TestExecution()
        {
            InitializeComponent();
            LoadDummyData();
            SetupDiagramPointers();
            btnLoadProgram.Click += BtnLoadProgram_Click;
        }

        private void BtnLoadProgram_Click(object? sender, EventArgs e)
        {
            using var modal = new AssemblyLineTester.Views.Modals.AutomatedTestConfigModalForm();
            modal.ShowDialog(this);
        }

        private void LoadDummyData()
        {
            // Populate ComboBox filters
            cmbProductModel.Items.AddRange(new object[] { "6060", "6070", "8080", "9090" });
            cmbProductModel.SelectedIndex = 0;

            cmbProductType.Items.AddRange(new object[] { "AC", "DC", "Hybrid" });
            cmbProductType.SelectedIndex = 0;

            cmbArea.Items.AddRange(new object[] { "Engine Control", "Cabin Interior", "Chassis", "Dashboard" });
            cmbArea.SelectedIndex = 0;

            cmbPartNumber.Items.AddRange(new object[] { "WH-ENG-001", "WH-ENG-002", "WH-CAB-005", "WH-CHS-010" });
            cmbPartNumber.SelectedIndex = 0;

            cmbEcnNo.Items.AddRange(new object[] { "123.R001", "123.R002", "124.R000" });
            cmbEcnNo.SelectedIndex = 0;

            // Setup Live Test Details DataGridView
            SetupGridLiveDetails();
        }

        private void SetupGridLiveDetails()
        {
            gridLiveDetails.Columns.Clear();

            gridLiveDetails.Columns.Add("StepNo", "#");
            gridLiveDetails.Columns.Add("TestPoint", "Test Point");
            gridLiveDetails.Columns.Add("TestType", "Test Type");
            gridLiveDetails.Columns.Add("Expected", "Expected Value");
            gridLiveDetails.Columns.Add("Measured", "Measured Value");
            gridLiveDetails.Columns.Add("Status", "Status");

            gridLiveDetails.AllowUserToResizeColumns = true;
            gridLiveDetails.Columns["StepNo"]!.Width = 55;
            gridLiveDetails.Columns["TestPoint"]!.Width = 240;
            gridLiveDetails.Columns["TestType"]!.Width = 180;
            gridLiveDetails.Columns["Expected"]!.Width = 240;
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

            // Add dummy test execution rows
            object[][] rows = new object[][]
            {
                new object[] { "1", "J1-01 ➔ J2-05", "Continuity", "< 1.00 Ω", "0.15 Ω", "PASS" },
                new object[] { "2", "J1-02 ➔ J2-06", "Continuity", "< 1.00 Ω", "0.18 Ω", "PASS" },
                new object[] { "3", "J1-03 ➔ J2-07", "Continuity", "< 1.00 Ω", "0.14 Ω", "PASS" },
                new object[] { "4", "J1-04 ➔ GND", "Short Circuit", "> 10.0 MΩ", "> 500 MΩ", "PASS" },
                new object[] { "5", "J1-05 ➔ GND", "Short Circuit", "> 10.0 MΩ", "> 500 MΩ", "PASS" },
                new object[] { "6", "J1-10 ➔ J3-02", "Insulation", "> 100 MΩ", "450 MΩ", "PASS" },
                new object[] { "7", "J1-11 ➔ J3-03", "Insulation", "> 100 MΩ", "480 MΩ", "PASS" },
                new object[] { "8", "J1-23 ➔ J3-16", "Functional", "< 1.00 Ω", "0.23 Ω", "TESTING" },
                new object[] { "9", "J1-24 ➔ J3-17", "Functional", "12.0 V ± 0.5 V", "Pending...", "PENDING" },
                new object[] { "10", "J2-01 ➔ ECU-P1", "Functional", "5.0 V ± 0.2 V", "Pending...", "PENDING" },
                new object[] { "11", "J2-02 ➔ ECU-P2", "Final Verify", "Signal OK", "Pending...", "PENDING" },
                new object[] { "12", "SHIELD ➔ GND", "Final Verify", "< 0.50 Ω", "Pending...", "PENDING" }
            };

            foreach (var r in rows)
            {
                int idx = gridLiveDetails.Rows.Add(r);
                DataGridViewRow row = gridLiveDetails.Rows[idx];
                string status = r[5]?.ToString() ?? "";

                if (status == "PASS")
                {
                    row.Cells["Status"].Style.BackColor = Color.FromArgb(209, 231, 221);
                    row.Cells["Status"].Style.ForeColor = Color.FromArgb(15, 81, 50);
                    row.Cells["Status"].Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
                else if (status == "TESTING")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 251, 235);
                    row.Cells["Status"].Style.BackColor = Color.FromArgb(254, 240, 138);
                    row.Cells["Status"].Style.ForeColor = Color.FromArgb(133, 77, 14);
                    row.Cells["Status"].Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
                else if (status == "PENDING")
                {
                    row.Cells["Status"].Style.BackColor = Color.FromArgb(241, 245, 249);
                    row.Cells["Status"].Style.ForeColor = Color.FromArgb(100, 116, 139);
                }
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

            // Background
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(248, 250, 252)))
            {
                g.FillRectangle(brush, r);
            }

            // Draw a subtle border
            using (Pen borderPen = new Pen(Color.FromArgb(226, 232, 240), 1))
            {
                g.DrawRectangle(borderPen, 0, 0, r.Width - 1, r.Height - 1);
            }

            // Draw info rows in Area Detail card
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

                    // Draw key
                    g.DrawString(key, fKey, bKey, 30, startY + (i * rowHeight));

                    // Draw val (with premium blue color for harness model)
                    using (Brush bVal = new SolidBrush(i == 2 ? Color.FromArgb(13, 110, 253) : Color.FromArgb(15, 23, 42)))
                    {
                        g.DrawString(val, fVal, bVal, 220, startY + (i * rowHeight));
                    }

                    // Draw a separator line
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

            // Background
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(248, 250, 252)))
            {
                g.FillRectangle(brush, r);
            }

            // Draw a subtle grid to look schematic
            using (Pen gridPen = new Pen(Color.FromArgb(241, 245, 249), 1))
            {
                for (int x = 20; x < r.Width; x += 20)
                    g.DrawLine(gridPen, x, 0, x, r.Height);
                for (int y = 20; y < r.Height; y += 20)
                    g.DrawLine(gridPen, 0, y, r.Width, y);
            }

            // Draw a smooth curved multi-wire harness cable bundle
            using (Pen cablePen = new Pen(Color.FromArgb(13, 110, 253), 5))
            {
                using (Pen wireInnerPen = new Pen(Color.FromArgb(147, 197, 253), 1))
                {
                    float startX = r.Width * 0.25f;
                    float startY = r.Height * 0.45f;
                    float endX = r.Width * 0.75f;
                    float endY = r.Height * 0.45f;

                    // Stepped routing for standard schematic feel
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

                    // Offset lines for multi-wire bundles
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

            // Draw J1 Connector Node (circular plug style)
            RectangleF n1 = new RectangleF(r.Width * 0.25f - 40, r.Height * 0.45f - 40, 80, 80);
            using (SolidBrush connBrush = new SolidBrush(Color.FromArgb(30, 41, 59)))
            {
                g.FillEllipse(connBrush, n1);
            }
            using (Pen borderPen = new Pen(Color.FromArgb(71, 85, 105), 3))
            {
                g.DrawEllipse(borderPen, n1);
            }

            // Draw J3 Connector Node
            RectangleF n2 = new RectangleF(r.Width * 0.75f - 40, r.Height * 0.45f - 40, 80, 80);
            using (SolidBrush connBrush = new SolidBrush(Color.FromArgb(30, 41, 59)))
            {
                g.FillEllipse(connBrush, n2);
            }
            using (Pen borderPen = new Pen(Color.FromArgb(71, 85, 105), 3))
            {
                g.DrawEllipse(borderPen, n2);
            }

            // Draw connector node text labels
            using (Font f = new Font("Segoe UI", 11F, FontStyle.Bold))
            using (Brush textBrush = new SolidBrush(Color.White))
            {
                StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString("J1", f, textBrush, n1, sf);
                g.DrawString("J3", f, textBrush, n2, sf);
            }

            // Draw pin descriptions below nodes
            using (Font detailFont = new Font("Segoe UI", 8.5F, FontStyle.Regular))
            using (Brush darkGrayBrush = new SolidBrush(Color.FromArgb(100, 116, 139)))
            {
                StringFormat sfTop = new StringFormat { Alignment = StringAlignment.Center };
                g.DrawString("ECU Input Plug\n(24-Pin Male)", detailFont, darkGrayBrush, r.Width * 0.25f, r.Height * 0.45f + 45, sfTop);
                g.DrawString("Main Harness\n(24-Pin Female)", detailFont, darkGrayBrush, r.Width * 0.75f, r.Height * 0.45f + 45, sfTop);
            }

            // Draw a stylish legend panel on bottom
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
