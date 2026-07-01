using System;
using System.Drawing;
using System.Windows.Forms;

namespace AssemblyLineTester.Views.Pages
{
    public partial class UserManagement : UserControl
    {
        public UserManagement()
        {
            InitializeComponent();
            InitializeFilters();
            LoadDummyData();
        }

        private void InitializeFilters()
        {
            cmbFilterStatus.Items.Clear();
            cmbFilterStatus.Items.AddRange(["Status: All", "Active", "Inactive"]);
            cmbFilterStatus.SelectedIndex = 0;

            cmbFilterRole.Items.Clear();
            cmbFilterRole.Items.AddRange(["Role: All", "Administrator", "Engineer", "Operator", "Technician", "Quality", "Supervisor"]);
            cmbFilterRole.SelectedIndex = 0;

            cmbFilterDept.Items.Clear();
            cmbFilterDept.Items.AddRange(["Department: All", "Engineering", "Production", "Maintenance", "Quality Assurance"]);
            cmbFilterDept.SelectedIndex = 0;
        }

        private void LoadDummyData()
        {
            SetupGridColumns();

            object[][] rows = [
                [1, "fally.kusuma", "Fally Kusuma", "fally.kusuma@caterpillar.com", "Administrator", "Engineering", "Active", "15 Mar 2026 10:28:41 AM", "10 Mar 2026 09:15:22 AM", "✏  🔒"],
                [2, "susan.widjaja", "Susan Widjaja", "susan.widjaja@caterpillar.com", "Engineer", "Engineering", "Active", "15 Mar 2026 09:41:12 AM", "11 Mar 2026 08:23:15 AM", "✏  🔒"],
                [3, "doni.pratama", "Doni Pratama", "doni.pratama@caterpillar.com", "Operator", "Production", "Active", "15 Mar 2026 09:55:33 AM", "12 Mar 2026 02:10:05 PM", "✏  🔒"],
                [4, "rian.hidayat", "Rian Hidayat", "rian.hidayat@caterpillar.com", "Technician", "Maintenance", "Active", "14 Mar 2026 04:22:18 PM", "12 Mar 2026 03:45:30 PM", "✏  🔒"],
                [5, "maria.sari", "Maria Sari", "maria.sari@caterpillar.com", "Operator", "Production", "Active", "15 Mar 2026 08:17:09 AM", "13 Mar 2026 09:02:44 AM", "✏  🔒"],
                [6, "agus.setiawan", "Agus Setiawan", "agus.setiawan@caterpillar.com", "Engineer", "Engineering", "Active", "15 Mar 2026 07:58:23 AM", "13 Mar 2026 11:21:36 AM", "✏  🔒"],
                [7, "lina.putri", "Lina Putri", "lina.putri@caterpillar.com", "Quality", "Quality Assurance", "Active", "14 Mar 2026 05:11:05 PM", "14 Mar 2026 10:05:17 AM", "✏  🔒"],
                [8, "yudi.rahman", "Yudi Rahman", "yudi.rahman@caterpillar.com", "Technician", "Maintenance", "Active", "14 Mar 2026 03:33:44 PM", "14 Mar 2026 01:17:08 PM", "✏  🔒"],
                [9, "budi.santoso", "Budi Santoso", "budi.santoso@caterpillar.com", "Supervisor", "Production", "Inactive", "08 Mar 2026 02:12:33 PM", "05 Mar 2026 09:40:10 AM", "✏  🔒"],
                [10, "lisa.kartika", "Lisa Kartika", "lisa.kartika@caterpillar.com", "Operator", "Production", "Inactive", "07 Mar 2026 11:07:15 AM", "06 Mar 2026 02:56:31 PM", "✏  🔒"]
            ];

            foreach (var r in rows)
            {
                gridUsers.Rows.Add(r);
            }

            gridUsers.CellFormatting += GridUsers_CellFormatting;
            gridUsers.SelectionChanged += GridUsers_SelectionChanged;

            if (pnlRoleChart is not null)
            {
                pnlRoleChart.Paint += PnlRoleChart_Paint;
            }
        }

        private void GridUsers_SelectionChanged(object? sender, EventArgs e)
        {
            if (gridUsers.SelectedRows.Count > 0)
            {
                var row = gridUsers.SelectedRows[0];
                lblValUser.Text = ": " + (row.Cells["colUsername"].Value?.ToString() ?? "-");
                lblValName.Text = ": " + (row.Cells["colFullName"].Value?.ToString() ?? "-");
                lblValEmail.Text = ": " + (row.Cells["colEmail"].Value?.ToString() ?? "-");
                lblValRole.Text = ": " + (row.Cells["colRole"].Value?.ToString() ?? "-");
                lblValDept.Text = ": " + (row.Cells["colDept"].Value?.ToString() ?? "-");
                string status = row.Cells["colStatus"].Value?.ToString() ?? "-";
                lblValStatus.Text = status;
                lblValStatus.BackColor = status == "Active" ? Color.FromArgb(209, 231, 221) : Color.FromArgb(226, 232, 240);
                lblValStatus.ForeColor = status == "Active" ? Color.FromArgb(15, 81, 50) : Color.FromArgb(71, 85, 105);
                lblValLogin.Text = ": " + (row.Cells["colLastLogin"].Value?.ToString() ?? "-");
                lblValCreated.Text = ": " + (row.Cells["colCreated"].Value?.ToString() ?? "-");
            }
        }

        private void SetupGridColumns()
        {
            gridUsers.Columns.Clear();

            var cNo = gridUsers.Columns.Add("colNo", "No.");
            gridUsers.Columns[cNo].FillWeight = 35F;

            var cUser = gridUsers.Columns.Add("colUsername", "Username");
            gridUsers.Columns[cUser].FillWeight = 80F;

            var cName = gridUsers.Columns.Add("colFullName", "Full Name");
            gridUsers.Columns[cName].FillWeight = 95F;

            var cEmail = gridUsers.Columns.Add("colEmail", "Email");
            gridUsers.Columns[cEmail].FillWeight = 140F;

            var cRole = gridUsers.Columns.Add("colRole", "Role");
            gridUsers.Columns[cRole].FillWeight = 85F;

            var cDept = gridUsers.Columns.Add("colDept", "Department");
            gridUsers.Columns[cDept].FillWeight = 95F;

            var cStatus = gridUsers.Columns.Add("colStatus", "Status");
            gridUsers.Columns[cStatus].FillWeight = 65F;
            gridUsers.Columns[cStatus].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            var cLogin = gridUsers.Columns.Add("colLastLogin", "Last Login");
            gridUsers.Columns[cLogin].FillWeight = 110F;

            var cCreated = gridUsers.Columns.Add("colCreated", "Created Date");
            gridUsers.Columns[cCreated].FillWeight = 110F;

            var cAction = gridUsers.Columns.Add("colAction", "Action");
            gridUsers.Columns[cAction].FillWeight = 55F;
            gridUsers.Columns[cAction].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void GridUsers_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && gridUsers.Columns[e.ColumnIndex].Name == "colStatus" && e.Value is not null && e.CellStyle is not null)
            {
                string? status = e.Value.ToString();
                if (status == "Active")
                {
                    e.CellStyle.BackColor = Color.FromArgb(209, 231, 221);
                    e.CellStyle.ForeColor = Color.FromArgb(15, 81, 50);
                    e.CellStyle.Font = new("Segoe UI", 9F, FontStyle.Bold);
                }
                else if (status == "Inactive")
                {
                    e.CellStyle.BackColor = Color.FromArgb(226, 232, 240);
                    e.CellStyle.ForeColor = Color.FromArgb(71, 85, 105);
                    e.CellStyle.Font = new("Segoe UI", 9F, FontStyle.Bold);
                }
            }
        }

        private void PnlRoleChart_Paint(object? sender, PaintEventArgs e)
        {
            if (pnlRoleChart is null) return;
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Rectangle rect = new(5, 5, pnlRoleChart.Width - 10, pnlRoleChart.Height - 10);
            if (rect.Width <= 0 || rect.Height <= 0) return;
            int size = Math.Min(rect.Width, rect.Height);
            Rectangle chartRect = new(rect.X + (rect.Width - size) / 2, rect.Y + (rect.Height - size) / 2, size, size);

            // Administrator : 2 (12.5% = 45 deg) Gold
            using SolidBrush b1 = new(Color.FromArgb(250, 204, 21));
            g.FillPie(b1, chartRect, 270, 45);

            // Engineer : 4 (25.0% = 90 deg) Green
            using SolidBrush b2 = new(Color.FromArgb(34, 197, 94));
            g.FillPie(b2, chartRect, 315, 90);

            // Operator : 6 (37.5% = 135 deg) Blue
            using SolidBrush b3 = new(Color.FromArgb(59, 130, 246));
            g.FillPie(b3, chartRect, 45, 135);

            // Technician : 2 (12.5% = 45 deg) Purple
            using SolidBrush b4 = new(Color.FromArgb(168, 85, 247));
            g.FillPie(b4, chartRect, 180, 45);

            // Quality : 1 (6.3% = 22.5 deg) Red
            using SolidBrush b5 = new(Color.FromArgb(239, 68, 68));
            g.FillPie(b5, chartRect, 225, 22.5f);

            // Supervisor : 1 (6.3% = 22.5 deg) Gray
            using SolidBrush b6 = new(Color.FromArgb(148, 163, 184));
            g.FillPie(b6, chartRect, 247.5f, 22.5f);

            // Inner circle for Doughnut
            int innerSize = (int)(size * 0.62);
            Rectangle innerRect = new(chartRect.X + (size - innerSize) / 2, chartRect.Y + (size - innerSize) / 2, innerSize, innerSize);
            using SolidBrush bCenter = new(Color.White);
            g.FillEllipse(bCenter, innerRect);

            // Draw center text "16 TOTAL"
            using Font fNum = new("Segoe UI", 16F, FontStyle.Bold);
            using SolidBrush bTxt = new(Color.FromArgb(30, 41, 59));
            using StringFormat sf = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g.DrawString("16" + Environment.NewLine + "TOTAL", fNum, bTxt, chartRect, sf);
        }
    }
}
