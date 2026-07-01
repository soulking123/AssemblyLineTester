using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AssemblyLineTester.Views.Pages
{
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            InitializeComponent();
            InitializeCustomStyles();
            LoadDummyData();
        }

        private void InitializeCustomStyles()
        {
            if (cmbTimeRange.Items.Count > 0)
            {
                cmbTimeRange.SelectedIndex = 0;
            }

            // DataGridView Modern Styling
            gridRecent.BorderStyle = BorderStyle.None;
            gridRecent.BackgroundColor = Color.White;
            gridRecent.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            gridRecent.GridColor = Color.FromArgb(241, 245, 249);
            gridRecent.RowHeadersVisible = false;
            gridRecent.AllowUserToAddRows = false;
            gridRecent.AllowUserToDeleteRows = false;
            gridRecent.ReadOnly = true;
            gridRecent.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridRecent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridRecent.EnableHeadersVisualStyles = false;

            // Header Style
            gridRecent.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            gridRecent.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(71, 85, 105);
            gridRecent.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gridRecent.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(248, 250, 252);
            gridRecent.ColumnHeadersDefaultCellStyle.Padding = new Padding(8);
            gridRecent.ColumnHeadersHeight = 40;

            // Row Style
            gridRecent.DefaultCellStyle.BackColor = Color.White;
            gridRecent.DefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);
            gridRecent.DefaultCellStyle.SelectionBackColor = Color.FromArgb(241, 245, 249);
            gridRecent.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);
            gridRecent.DefaultCellStyle.Padding = new Padding(6);
            gridRecent.RowTemplate.Height = 36;

            gridRecent.CellFormatting += GridRecent_CellFormatting;

            // Attach PostPaint for center text in Doughnut Chart
            chartFailReason.PostPaint += ChartFailReason_PostPaint;
        }

        private void LoadDummyData()
        {
            // ----------------------------------------------------
            // 1. Setup Bar Chart (History)
            // ----------------------------------------------------
            chartHistory.ChartAreas.Clear();
            chartHistory.Series.Clear();
            chartHistory.Legends.Clear();

            ChartArea areaBar = new ChartArea("BarArea");
            areaBar.BackColor = Color.White;
            areaBar.AxisX.MajorGrid.Enabled = false;
            areaBar.AxisX.LabelStyle.Font = new Font("Segoe UI", 8.5F);
            areaBar.AxisX.LabelStyle.ForeColor = Color.FromArgb(100, 116, 139);
            areaBar.AxisX.LineColor = Color.FromArgb(226, 232, 240);

            areaBar.AxisY.MajorGrid.LineColor = Color.FromArgb(241, 245, 249);
            areaBar.AxisY.LabelStyle.Font = new Font("Segoe UI", 8.5F);
            areaBar.AxisY.LabelStyle.ForeColor = Color.FromArgb(100, 116, 139);
            areaBar.AxisY.LineColor = Color.Transparent;
            areaBar.AxisY.Maximum = 250;
            areaBar.AxisY.Interval = 50;
            chartHistory.ChartAreas.Add(areaBar);

            Legend legBar = new Legend("BarLegend");
            legBar.Docking = Docking.Top;
            legBar.Alignment = StringAlignment.Center;
            legBar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            chartHistory.Legends.Add(legBar);

            Series sPass = new Series("Pass");
            sPass.ChartType = SeriesChartType.Column;
            sPass.Color = Color.FromArgb(76, 175, 80);
            sPass.LegendText = "Pass";
            sPass["PointWidth"] = "0.45";

            Series sFail = new Series("Fail");
            sFail.ChartType = SeriesChartType.Column;
            sFail.Color = Color.FromArgb(244, 67, 54);
            sFail.LegendText = "Fail";
            sFail["PointWidth"] = "0.45";

            string[] dates = { "09 Mar", "10 Mar", "11 Mar", "12 Mar", "13 Mar", "14 Mar", "15 Mar" };
            int[] passVals = { 180, 192, 205, 198, 210, 208, 215 };
            int[] failVals = { 4, 3, 2, 4, 2, 3, 3 };

            for (int i = 0; i < dates.Length; i++)
            {
                int pIdx = sPass.Points.AddXY(dates[i], passVals[i]);
                sPass.Points[pIdx].Label = passVals[i].ToString();
                sPass.Points[pIdx].Font = new Font("Segoe UI", 8F, FontStyle.Bold);

                int fIdx = sFail.Points.AddXY(dates[i], failVals[i]);
                sFail.Points[fIdx].Label = failVals[i].ToString();
                sFail.Points[fIdx].Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            }

            chartHistory.Series.Add(sPass);
            chartHistory.Series.Add(sFail);

            // ----------------------------------------------------
            // 2. Setup Doughnut Chart (Fail By Reason)
            // ----------------------------------------------------
            chartFailReason.ChartAreas.Clear();
            chartFailReason.Series.Clear();
            chartFailReason.Legends.Clear();

            ChartArea areaPie = new ChartArea("PieArea");
            areaPie.BackColor = Color.White;
            chartFailReason.ChartAreas.Add(areaPie);

            Legend legPie = new Legend("PieLegend");
            legPie.Docking = Docking.Right;
            legPie.Alignment = StringAlignment.Center;
            legPie.Font = new Font("Segoe UI", 8.5F);
            chartFailReason.Legends.Add(legPie);

            Series sReason = new Series("FailReason");
            sReason.ChartType = SeriesChartType.Doughnut;
            sReason["DoughnutRadius"] = "65";

            int p1 = sReason.Points.AddY(2);
            sReason.Points[p1].Color = Color.FromArgb(244, 67, 54);
            sReason.Points[p1].LegendText = "Continuity Fail   2 (66.7%)";

            int p2 = sReason.Points.AddY(1);
            sReason.Points[p2].Color = Color.FromArgb(255, 152, 0);
            sReason.Points[p2].LegendText = "Insulation Fail   1 (33.3%)";

            int p3 = sReason.Points.AddY(0);
            sReason.Points[p3].Color = Color.FromArgb(33, 150, 243);
            sReason.Points[p3].LegendText = "Short Circuit Fail 0 (0%)";
            sReason.Points[p3].IsEmpty = true;

            chartFailReason.Series.Add(sReason);

            // ----------------------------------------------------
            // 3. Setup DataGridView (Recent Test History)
            // ----------------------------------------------------
            gridRecent.Columns.Clear();
            gridRecent.Columns.Add("ColNo", "No.");
            gridRecent.Columns.Add("ColTime", "Test Time");
            gridRecent.Columns.Add("ColPart", "Part Number");
            gridRecent.Columns.Add("ColSerial", "Serial Number");
            gridRecent.Columns.Add("ColResult", "Result");
            gridRecent.Columns.Add("ColOperator", "Operator");

            var colNo = gridRecent.Columns["ColNo"];
            var colTime = gridRecent.Columns["ColTime"];
            var colPart = gridRecent.Columns["ColPart"];
            var colSerial = gridRecent.Columns["ColSerial"];
            var colResult = gridRecent.Columns["ColResult"];
            var colOperator = gridRecent.Columns["ColOperator"];

            if (colNo != null) { colNo.FillWeight = 30; colNo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; }
            if (colTime != null) colTime.FillWeight = 60;
            if (colPart != null) colPart.FillWeight = 75;
            if (colSerial != null) colSerial.FillWeight = 70;
            if (colResult != null) { colResult.FillWeight = 50; colResult.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; }
            if (colOperator != null) colOperator.FillWeight = 55;

            object[][] rows = new object[][]
            {
                new object[] { "1", "10:30:12", "WH-ENG-001", "SN12350", "PASS", "Fally" },
                new object[] { "2", "10:28:56", "WH-ENG-001", "SN12349", "PASS", "Fally" },
                new object[] { "3", "10:27:40", "WH-ENG-001", "SN12348", "FAIL", "Fally" },
                new object[] { "4", "10:26:18", "WH-ENG-001", "SN12347", "PASS", "Fally" },
                new object[] { "5", "10:25:05", "WH-ENG-001", "SN12346", "PASS", "Fally" },
                new object[] { "6", "10:23:51", "WH-ENG-001", "SN12345", "PASS", "Fally" },
                new object[] { "7", "10:22:33", "WH-ENG-001", "SN12344", "PASS", "Fally" },
                new object[] { "8", "10:21:20", "WH-ENG-001", "SN12343", "PASS", "Fally" }
            };

            foreach (var r in rows)
            {
                gridRecent.Rows.Add(r);
            }
        }

        private void GridRecent_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (gridRecent.Columns[e.ColumnIndex].Name == "ColResult" && e.Value != null && e.CellStyle != null)
            {
                string? val = e.Value.ToString();
                if (val == "PASS")
                {
                    e.CellStyle.BackColor = Color.FromArgb(220, 252, 231);
                    e.CellStyle.ForeColor = Color.FromArgb(22, 101, 52);
                    e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
                else if (val == "FAIL")
                {
                    e.CellStyle.BackColor = Color.FromArgb(254, 226, 226);
                    e.CellStyle.ForeColor = Color.FromArgb(153, 27, 27);
                    e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
            }
        }

        private void ChartFailReason_PostPaint(object? sender, ChartPaintEventArgs e)
        {
            if (e.ChartElement is ChartArea area)
            {
                string numText = "3";
                string subText = "Total Fail";

                using (Font fontNum = new Font("Segoe UI", 20F, FontStyle.Bold))
                using (Font fontSub = new Font("Segoe UI", 9F, FontStyle.Regular))
                using (SolidBrush brushNum = new SolidBrush(Color.FromArgb(30, 41, 59)))
                using (SolidBrush brushSub = new SolidBrush(Color.FromArgb(100, 116, 139)))
                using (StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                {
                    RectangleF chartRect = e.ChartGraphics.GetAbsoluteRectangle(area.Position.ToRectangleF());
                    float centerX = chartRect.Left + (chartRect.Width * 0.42f);
                    float centerY = chartRect.Top + (chartRect.Height * 0.5f);

                    e.ChartGraphics.Graphics.DrawString(numText, fontNum, brushNum, centerX, centerY - 12, sf);
                    e.ChartGraphics.Graphics.DrawString(subText, fontSub, brushSub, centerX, centerY + 16, sf);
                }
            }
        }
    }
}