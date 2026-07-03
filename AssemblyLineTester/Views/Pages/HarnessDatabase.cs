using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssemblyLineTester.Views.Pages
{
    public partial class HarnessDatabase : UserControl
    {
        public HarnessDatabase()
        {
            InitializeComponent();
            panelFilters.Paint += PanelFilters_Paint;
            tableLayoutPanelLeft.Paint += Card_Paint;
            panelDetailCard.Paint += Card_Paint;
            panelCardSpecs.Paint += Card_Paint;
            panelCardTestSummary.Paint += Card_Paint;
            picHarnessImage.Paint += PicHarnessImage_Paint;
            picMachineArea.Paint += PicMachineArea_Paint;
            dataGridViewList.CellPainting += DataGridViewList_CellPainting;
            SetupPagination();
            PopulateDummyData();
        }

        private void PanelFilters_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using var pen = new Pen(Color.FromArgb(226, 232, 240), 1);
            int rad = 8;
            var rect = new Rectangle(0, 0, panelFilters.Width - 1, panelFilters.Height - 1);
            using var path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, rad * 2, rad * 2, 180, 90);
            path.AddArc(rect.Right - rad * 2, rect.Y, rad * 2, rad * 2, 270, 90);
            path.AddArc(rect.Right - rad * 2, rect.Bottom - rad * 2, rad * 2, rad * 2, 0, 90);
            path.AddArc(rect.X, rect.Bottom - rad * 2, rad * 2, rad * 2, 90, 90);
            path.CloseFigure();
            e.Graphics.DrawPath(pen, path);
        }

        private void Card_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Control ctrl) return;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using var pen = new Pen(Color.FromArgb(226, 232, 240), 1);
            int rad = 8;
            var rect = new Rectangle(0, 0, ctrl.Width - 1, ctrl.Height - 1);
            using var path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, rad * 2, rad * 2, 180, 90);
            path.AddArc(rect.Right - rad * 2, rect.Y, rad * 2, rad * 2, 270, 90);
            path.AddArc(rect.Right - rad * 2, rect.Bottom - rad * 2, rad * 2, rad * 2, 0, 90);
            path.AddArc(rect.X, rect.Bottom - rad * 2, rad * 2, rad * 2, 90, 90);
            path.CloseFigure();
            e.Graphics.DrawPath(pen, path);
        }

        private void DataGridViewList_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.Graphics == null) return;
            if (e.RowIndex >= 0 && dataGridViewList.Columns[e.ColumnIndex].Name == "colStatus")
            {
                e.PaintBackground(e.ClipBounds, true);
                string val = e.Value?.ToString() ?? "";
                if (!string.IsNullOrEmpty(val))
                {
                    Color backCol = val == "Active" ? Color.FromArgb(220, 252, 231) : Color.FromArgb(241, 245, 249);
                    Color textCol = val == "Active" ? Color.FromArgb(22, 101, 52) : Color.FromArgb(71, 85, 105);

                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    int pillWidth = 72;
                    int pillHeight = 24;
                    int x = e.CellBounds.X + (e.CellBounds.Width - pillWidth) / 2;
                    int y = e.CellBounds.Y + (e.CellBounds.Height - pillHeight) / 2;
                    var pillRect = new Rectangle(x, y, pillWidth, pillHeight);

                    using var brush = new SolidBrush(backCol);
                    using var path = new GraphicsPath();
                    int r = 6;
                    path.AddArc(pillRect.X, pillRect.Y, r * 2, r * 2, 180, 90);
                    path.AddArc(pillRect.Right - r * 2, pillRect.Y, r * 2, r * 2, 270, 90);
                    path.AddArc(pillRect.Right - r * 2, pillRect.Bottom - r * 2, r * 2, r * 2, 0, 90);
                    path.AddArc(pillRect.X, pillRect.Bottom - r * 2, r * 2, r * 2, 90, 90);
                    path.CloseFigure();
                    e.Graphics.FillPath(brush, path);

                    using var font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
                    TextRenderer.DrawText(e.Graphics, val, font, pillRect, textCol, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }
                e.Handled = true;
            }
        }

        private void SetupPagination()
        {
            flpPages.Controls.Clear();
            string[] pages = { "<", "1", "2", "3", "4", "5", "...", "6", ">|" };
            foreach (var p in pages)
            {
                var btn = new Button
                {
                    Text = p,
                    Size = new Size(34, 32),
                    FlatStyle = FlatStyle.Flat,
                    Margin = new Padding(2, 10, 2, 0),
                    Font = new Font("Segoe UI", 8.5F, p == "1" ? FontStyle.Bold : FontStyle.Regular),
                    Cursor = p == "..." ? Cursors.Default : Cursors.Hand
                };
                btn.FlatAppearance.BorderSize = 0;
                if (p == "1")
                {
                    btn.BackColor = Color.FromArgb(250, 204, 21);
                    btn.ForeColor = Color.FromArgb(15, 23, 42);
                }
                else if (p == "...")
                {
                    btn.BackColor = Color.Transparent;
                    btn.ForeColor = Color.FromArgb(100, 116, 139);
                    btn.Enabled = false;
                }
                else
                {
                    btn.BackColor = Color.FromArgb(241, 245, 249);
                    btn.ForeColor = Color.FromArgb(51, 65, 85);
                }
                flpPages.Controls.Add(btn);
            }
            if (cmbShowEntries.Items.Count > 0) cmbShowEntries.SelectedIndex = 0;
        }

        private void PopulateDummyData()
        {
            dataGridViewList.Rows.Clear();
            // Exact 12 rows matching the layout picture
            dataGridViewList.Rows.Add("1", "6060", "AC", "Engine Control", "WH-ENG-001", "123.R001", "A", "Active", "14 Mar 2026");
            dataGridViewList.Rows.Add("2", "6060", "AC", "Cabin", "WH-CAB-002", "123.R001", "A", "Active", "14 Mar 2026");
            dataGridViewList.Rows.Add("3", "6060", "AC", "Main Frame", "WH-MF-003", "123.R001", "B", "Active", "13 Mar 2026");
            dataGridViewList.Rows.Add("4", "6060", "AC", "Boom", "WH-BOOM-004", "124.R000", "A", "Active", "13 Mar 2026");
            dataGridViewList.Rows.Add("5", "6060", "AC", "Power Module", "WH-PM-005", "124.R000", "A", "Active", "12 Mar 2026");
            dataGridViewList.Rows.Add("6", "6060", "AC", "Lighting", "WH-LTG-006", "124.R001", "A", "Active", "12 Mar 2026");
            dataGridViewList.Rows.Add("7", "6060", "AC", "Hydraulic Control", "WH-HYD-007", "125.R000", "B", "Active", "11 Mar 2026");
            dataGridViewList.Rows.Add("8", "6060", "AC", "Monitoring System", "WH-MON-008", "125.R000", "A", "Active", "11 Mar 2026");
            dataGridViewList.Rows.Add("9", "6060", "AC", "Battery Power", "WH-BP-009", "125.R001", "A", "Active", "10 Mar 2026");
            dataGridViewList.Rows.Add("10", "6060", "AC", "Fuel System", "WH-FUEL-010", "126.R000", "A", "Inactive", "10 Mar 2026");
            dataGridViewList.Rows.Add("11", "6060", "AC", "Attachment Control", "WH-ATT-011", "126.R001", "A", "Active", "09 Mar 2026");
            dataGridViewList.Rows.Add("12", "6060", "AC", "Telematics", "WH-TEL-012", "126.R001", "A", "Active", "09 Mar 2026");

            // Populate ComboBoxes
            cmbProductModel.Items.Clear();
            cmbProductModel.Items.AddRange(["All", "6060"]);
            cmbProductModel.SelectedIndex = 0;

            cmbProductType.Items.Clear();
            cmbProductType.Items.AddRange(["All", "AC"]);
            cmbProductType.SelectedIndex = 0;

            cmbArea.Items.Clear();
            cmbArea.Items.AddRange(["All", "Engine Control", "Cabin", "Main Frame", "Boom", "Power Module", "Lighting", "Hydraulic Control", "Monitoring System", "Battery Power", "Fuel System", "Attachment Control", "Telematics"]);
            cmbArea.SelectedIndex = 0;

            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(["All", "Active", "Inactive"]);
            cmbStatus.SelectedIndex = 0;

            PopulateGeneralInfo();
            PopulateBottomCards();
        }

        private void PopulateGeneralInfo()
        {
            tblGenInfo.Controls.Clear();
            var data = new (string Key, string Val, bool IsStatus)[]
            {
                ("Part Number", ": WH-ENG-001", false),
                ("Product Model", ": 6060", false),
                ("Product Type", ": AC", false),
                ("Area", ": Engine Control", false),
                ("ECN No.", ": 123.R001", false),
                ("Revision", ": A", false),
                ("Status", ": Active", true),
                ("Description", ": Main engine control harness routing through the firewall.\r\n  Requires high-temperature resistant sleeving.", false),
                ("Created By", ": Fally Kusuma", false),
                ("Created Date", ": 10 Mar 2026 09:15 AM", false),
                ("Last Updated By", ": Fally Kusuma", false),
                ("Last Updated Date", ": 14 Mar 2026 03:22 PM", false)
            };

            for (int i = 0; i < data.Length; i++)
            {
                var lblKey = new Label
                {
                    Text = data[i].Key,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(71, 85, 105),
                    AutoSize = true,
                    Anchor = AnchorStyles.Left | AnchorStyles.Top,
                    Margin = new Padding(4, 6, 4, 0)
                };
                tblGenInfo.Controls.Add(lblKey, 0, i);

                if (data[i].IsStatus)
                {
                    var pnlStatus = new FlowLayoutPanel
                    {
                        AutoSize = true,
                        FlowDirection = FlowDirection.LeftToRight,
                        Margin = new Padding(0, 4, 0, 0)
                    };
                    var lblColon = new Label
                    {
                        Text = ": ",
                        Font = new Font("Segoe UI", 9F),
                        ForeColor = Color.FromArgb(51, 65, 85),
                        AutoSize = true,
                        Margin = new Padding(0, 2, 4, 0)
                    };
                    var lblBadge = new Label
                    {
                        Text = "Active",
                        Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                        ForeColor = Color.FromArgb(22, 101, 52),
                        BackColor = Color.FromArgb(220, 252, 231),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Size = new Size(68, 24),
                        Margin = new Padding(0)
                    };
                    pnlStatus.Controls.Add(lblColon);
                    pnlStatus.Controls.Add(lblBadge);
                    tblGenInfo.Controls.Add(pnlStatus, 1, i);
                }
                else
                {
                    var lblVal = new Label
                    {
                        Text = data[i].Val,
                        Font = new Font("Segoe UI", 9F),
                        ForeColor = Color.FromArgb(15, 23, 42),
                        AutoSize = true,
                        Anchor = AnchorStyles.Left | AnchorStyles.Top,
                        Margin = new Padding(0, 6, 4, 0)
                    };
                    tblGenInfo.Controls.Add(lblVal, 1, i);
                }
            }
        }

        private void PopulateBottomCards()
        {
            tblSpecsContent.Controls.Clear();
            var leftSpecs = new (string Key, string Val)[]
            {
                ("Total Connector", ": 6"),
                ("Total Pin", ": 48"),
                ("Total Wire", ": 62"),
                ("Total Branch", ": 12")
            };
            var rightSpecs = new (string Key, string Val)[]
            {
                ("Min Wire Size", ": 0.50 mm²"),
                ("Max Wire Size", ": 4.00 mm²"),
                ("Nominal Voltage", ": 24 VDC"),
                ("Operating Temperature", ": -40 ~ 85 °C")
            };

            for (int i = 0; i < 4; i++)
            {
                var lblLeftKey = new Label
                {
                    Text = leftSpecs[i].Key,
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(71, 85, 105),
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    Margin = new Padding(0)
                };
                var lblLeftVal = new Label
                {
                    Text = leftSpecs[i].Val,
                    Font = new Font("Segoe UI", 9.5F),
                    ForeColor = Color.FromArgb(15, 23, 42),
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    Margin = new Padding(0, 0, 30, 0)
                };
                tblSpecsContent.Controls.Add(lblLeftKey, 0, i);
                tblSpecsContent.Controls.Add(lblLeftVal, 1, i);

                var lblRightKey = new Label
                {
                    Text = rightSpecs[i].Key,
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(71, 85, 105),
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    Margin = new Padding(0)
                };
                var lblRightVal = new Label
                {
                    Text = rightSpecs[i].Val,
                    Font = new Font("Segoe UI", 9.5F),
                    ForeColor = Color.FromArgb(15, 23, 42),
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    Margin = new Padding(0)
                };
                tblSpecsContent.Controls.Add(lblRightKey, 2, i);
                tblSpecsContent.Controls.Add(lblRightVal, 3, i);
            }

            tblTestSummaryContent.Controls.Clear();
            var summaryData = new (string Key, string Val, Color? ColorOverride)[]
            {
                ("Total Tested", ": 215", null),
                ("Pass", ": 212 (98.60%)", Color.FromArgb(22, 101, 52)),
                ("Fail", ": 3 (1.40%)", Color.FromArgb(239, 68, 68)),
                ("Last Tested", ": 15 Mar 2026 10:30 AM", null)
            };

            for (int i = 0; i < summaryData.Length; i++)
            {
                var lblKey = new Label
                {
                    Text = summaryData[i].Key,
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(71, 85, 105),
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    Margin = new Padding(0)
                };
                var lblVal = new Label
                {
                    Text = summaryData[i].Val,
                    Font = summaryData[i].ColorOverride.HasValue ? new Font("Segoe UI", 9.5F, FontStyle.Bold) : new Font("Segoe UI", 9.5F),
                    ForeColor = summaryData[i].ColorOverride ?? Color.FromArgb(15, 23, 42),
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    Margin = new Padding(0)
                };
                tblTestSummaryContent.Controls.Add(lblKey, 0, i);
                tblTestSummaryContent.Controls.Add(lblVal, 1, i);
            }
        }

        private void PicHarnessImage_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Control ctrl || e.Graphics == null) return;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = ctrl.ClientRectangle;
            if (rect.Width <= 1 || rect.Height <= 1) return;

            using var penBorder = new Pen(Color.FromArgb(226, 232, 240), 1);
            e.Graphics.DrawRectangle(penBorder, 0, 0, rect.Width - 1, rect.Height - 1);

            using var penCable = new Pen(Color.FromArgb(51, 65, 85), 4);
            using var penBranch1 = new Pen(Color.FromArgb(239, 68, 68), 3);
            using var penBranch2 = new Pen(Color.FromArgb(59, 130, 246), 3);

            int cy = rect.Height / 2;
            e.Graphics.DrawLine(penCable, rect.Width * 0.15f, cy, rect.Width * 0.7f, cy);
            e.Graphics.DrawLine(penBranch1, rect.Width * 0.45f, cy, rect.Width * 0.65f, cy - (rect.Height * 0.25f));
            e.Graphics.DrawLine(penBranch2, rect.Width * 0.55f, cy, rect.Width * 0.75f, cy + (rect.Height * 0.25f));

            using var brushConn = new SolidBrush(Color.FromArgb(30, 41, 59));
            e.Graphics.FillRectangle(brushConn, rect.Width * 0.1f, cy - 12, 36, 24);
            e.Graphics.FillRectangle(brushConn, rect.Width * 0.68f, cy - 10, 28, 20);
            e.Graphics.FillRectangle(brushConn, rect.Width * 0.63f, cy - (rect.Height * 0.25f) - 10, 24, 18);
            e.Graphics.FillRectangle(brushConn, rect.Width * 0.73f, cy + (rect.Height * 0.25f) - 8, 24, 18);
        }

        private void PicMachineArea_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Control ctrl || e.Graphics == null) return;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = ctrl.ClientRectangle;
            if (rect.Width <= 1 || rect.Height <= 1) return;

            using var penBorder = new Pen(Color.FromArgb(226, 232, 240), 1);
            e.Graphics.DrawRectangle(penBorder, 0, 0, rect.Width - 1, rect.Height - 1);

            using var penBox = new Pen(Color.FromArgb(148, 163, 184), 2);
            penBox.DashStyle = DashStyle.Dash;
            var areaRect = new Rectangle((int)(rect.Width * 0.2f), (int)(rect.Height * 0.2f), (int)(rect.Width * 0.6f), (int)(rect.Height * 0.6f));
            e.Graphics.DrawRectangle(penBox, areaRect);

            using var brushHighlight = new SolidBrush(Color.FromArgb(60, 250, 204, 21));
            e.Graphics.FillRectangle(brushHighlight, (int)(rect.Width * 0.4f), (int)(rect.Height * 0.35f), (int)(rect.Width * 0.25f), (int)(rect.Height * 0.3f));

            using var font = new Font("Segoe UI", 9F, FontStyle.Bold);
            TextRenderer.DrawText(e.Graphics, "ENGINE ROOM - ZONE A2", font, areaRect, Color.FromArgb(71, 85, 105), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }
}
