using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AssemblyLineTester.Helpers;
using AssemblyLineTester.Models;
using AssemblyLineTester.Services;

namespace AssemblyLineTester.Views.Modals
{
    public class SelectHarnessModalForm : Form
    {
        private readonly HashSet<int> _alreadyAttachedIds;
        private List<HarnessAssembly> _allAvailableHarnesses = new();
        private readonly HashSet<int> _selectedHarnessIds = new();

        public List<HarnessAssembly> SelectedHarnesses { get; private set; } = new();

        // Controls
        private Panel pnlHeader = null!;
        private Label lblTitle = null!;
        private Button btnClose = null!;
        private TextBox txtSearch = null!;
        private DataGridView dgvHarnesses = null!;
        private Panel pnlBottom = null!;
        private Button btnAttach = null!;
        private Button btnCancel = null!;

        public SelectHarnessModalForm(List<int>? alreadyAttachedHarnessIds = null)
        {
            _alreadyAttachedIds = alreadyAttachedHarnessIds != null
                ? new HashSet<int>(alreadyAttachedHarnessIds)
                : new HashSet<int>();

            InitializeComponents();
            ApplyStyles();
            LoadHarnesses();
        }

        private void InitializeComponents()
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(650, 520);
            BackColor = Color.FromArgb(244, 245, 247);

            // Header Panel
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 44,
                BackColor = Color.FromArgb(15, 23, 42)
            };

            lblTitle = new Label
            {
                Text = "ATTACH HARNESS ASSEMBLIES",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                Location = new Point(16, 12),
                AutoSize = true
            };

            btnClose = new Button
            {
                Text = "X",
                ForeColor = Color.FromArgb(203, 213, 225),
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Size = new Size(36, 36),
                Location = new Point(Width - 42, 4),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => Close();

            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(btnClose);
            pnlHeader.MouseDown += TitleBar_MouseDown;
            lblTitle.MouseDown += TitleBar_MouseDown;

            // Search Box
            var pnlSearch = new Panel
            {
                Dock = DockStyle.Top,
                Height = 46,
                Padding = new Padding(16, 10, 16, 6)
            };

            txtSearch = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F),
                PlaceholderText = "Search by harness name or part number..."
            };
            txtSearch.TextChanged += (s, e) => FilterHarnesses();
            pnlSearch.Controls.Add(txtSearch);

            // Bottom Bar
            pnlBottom = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 52,
                Padding = new Padding(16, 8, 16, 8),
                BackColor = Color.White
            };

            var pnlBottomButtons = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                FlowDirection = FlowDirection.RightToLeft,
                Width = 240,
                Height = 36
            };

            btnAttach = new Button
            {
                Text = "Attach Selected",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(16, 185, 129),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(130, 34),
                Cursor = Cursors.Hand
            };
            btnAttach.FlatAppearance.BorderSize = 0;
            btnAttach.Click += BtnAttach_Click;

            btnCancel = new Button
            {
                Text = "Cancel",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(71, 85, 105),
                BackColor = Color.FromArgb(241, 245, 249),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(90, 34),
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => Close();

            pnlBottomButtons.Controls.Add(btnAttach);
            pnlBottomButtons.Controls.Add(btnCancel);
            pnlBottom.Controls.Add(pnlBottomButtons);

            // Grid Container
            var pnlGrid = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(16, 4, 16, 8)
            };

            dgvHarnesses = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                MultiSelect = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            var colCheck = new DataGridViewCheckBoxColumn
            {
                HeaderText = "",
                Width = 35,
                Name = "colCheck"
            };

            var colName = new DataGridViewTextBoxColumn
            {
                HeaderText = "Harness Name",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true,
                Name = "colName"
            };

            var colPart = new DataGridViewTextBoxColumn
            {
                HeaderText = "Part Number",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true,
                Name = "colPart"
            };

            var colConnectors = new DataGridViewTextBoxColumn
            {
                HeaderText = "Total Connectors",
                Width = 110,
                ReadOnly = true,
                Name = "colConnectors"
            };

            dgvHarnesses.Columns.AddRange(colCheck, colName, colPart, colConnectors);
            dgvHarnesses.CellContentClick += DgvHarnesses_CellContentClick;
            dgvHarnesses.CellDoubleClick += DgvHarnesses_CellDoubleClick;

            pnlGrid.Controls.Add(dgvHarnesses);

            Controls.Add(pnlGrid);
            Controls.Add(pnlBottom);
            Controls.Add(pnlSearch);
            Controls.Add(pnlHeader);
        }

        private void ApplyStyles()
        {
            UIStyleHelper.StyleDataGridView(dgvHarnesses);
            UIStyleHelper.SetupButtonRoundedEdges(btnAttach, 6,
                Color.FromArgb(16, 185, 129), Color.FromArgb(5, 150, 105), Color.FromArgb(4, 120, 87), Color.Transparent);
            UIStyleHelper.SetupButtonRoundedEdges(btnCancel, 6,
                Color.FromArgb(241, 245, 249), Color.FromArgb(226, 232, 240), Color.FromArgb(203, 213, 225), Color.Transparent);

            ApplyFormRounding();
        }

        private void ApplyFormRounding()
        {
            if (Width <= 0 || Height <= 0) return;
            int radius = 16;
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            using GraphicsPath path = new();
            int d = radius * 2;
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            Region = new Region(path);
        }

        private void LoadHarnesses()
        {
            // Only list harness assemblies that are not already attached
            _allAvailableHarnesses = HarnessService.GetAllHarnesses()
                .Where(h => !_alreadyAttachedIds.Contains(h.Id))
                .ToList();

            FilterHarnesses();
        }

        private void FilterHarnesses()
        {
            string term = txtSearch.Text.Trim().ToLowerInvariant();

            var filtered = string.IsNullOrEmpty(term)
                ? _allAvailableHarnesses
                : _allAvailableHarnesses.Where(h =>
                    (h.Name != null && h.Name.ToLowerInvariant().Contains(term)) ||
                    (h.PartNumber != null && h.PartNumber.ToLowerInvariant().Contains(term))
                ).ToList();

            dgvHarnesses.Rows.Clear();
            foreach (var h in filtered)
            {
                bool isChecked = _selectedHarnessIds.Contains(h.Id);
                int rowIndex = dgvHarnesses.Rows.Add(
                    isChecked,
                    h.Name,
                    h.PartNumber,
                    h.Connectors?.Count ?? 0
                );
                dgvHarnesses.Rows[rowIndex].Tag = h;
            }
        }

        private void DgvHarnesses_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0) return;

            if (dgvHarnesses.Rows[e.RowIndex].Tag is HarnessAssembly h)
            {
                bool current = Convert.ToBoolean(dgvHarnesses.Rows[e.RowIndex].Cells[0].Value);
                bool next = !current;
                dgvHarnesses.Rows[e.RowIndex].Cells[0].Value = next;

                if (next) _selectedHarnessIds.Add(h.Id);
                else _selectedHarnessIds.Remove(h.Id);
            }
        }

        private void DgvHarnesses_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvHarnesses.Rows[e.RowIndex].Tag is HarnessAssembly h)
            {
                bool current = Convert.ToBoolean(dgvHarnesses.Rows[e.RowIndex].Cells[0].Value);
                bool next = !current;
                dgvHarnesses.Rows[e.RowIndex].Cells[0].Value = next;

                if (next) _selectedHarnessIds.Add(h.Id);
                else _selectedHarnessIds.Remove(h.Id);
            }
        }

        private void BtnAttach_Click(object? sender, EventArgs e)
        {
            // Gather all checked harness assemblies
            SelectedHarnesses = _allAvailableHarnesses
                .Where(h => _selectedHarnessIds.Contains(h.Id))
                .ToList();

            if (SelectedHarnesses.Count == 0)
            {
                MessageBox.Show("Please select at least one harness assembly to attach.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        private void TitleBar_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }
    }
}
