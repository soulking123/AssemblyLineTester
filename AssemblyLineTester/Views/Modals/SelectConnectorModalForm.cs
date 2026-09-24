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
    public class SelectConnectorModalForm : Form
    {
        private readonly HashSet<int> _alreadyAttachedIds;
        private List<Connector> _allAvailableConnectors = new();
        private readonly HashSet<int> _selectedConnectorIds = new();

        public List<Connector> SelectedConnectors { get; private set; } = new();

        // Controls
        private Panel pnlHeader = null!;
        private Label lblTitle = null!;
        private Button btnClose = null!;
        private TextBox txtSearch = null!;
        private DataGridView dgvConnectors = null!;
        private Panel pnlBottom = null!;
        private Button btnAttach = null!;
        private Button btnCancel = null!;

        public SelectConnectorModalForm(List<int>? alreadyAttachedConnectorIds = null)
        {
            _alreadyAttachedIds = alreadyAttachedConnectorIds != null
                ? new HashSet<int>(alreadyAttachedConnectorIds)
                : new HashSet<int>();

            InitializeComponents();
            ApplyStyles();
            LoadConnectors();
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
                Text = "ATTACH CONNECTORS",
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
                PlaceholderText = "Search by connector name, part number, or type..."
            };
            txtSearch.TextChanged += (s, e) => FilterConnectors();
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

            dgvConnectors = new DataGridView
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
                HeaderText = "Connector Name",
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

            var colPins = new DataGridViewTextBoxColumn
            {
                HeaderText = "Pins",
                Width = 60,
                ReadOnly = true,
                Name = "colPins"
            };

            var colType = new DataGridViewTextBoxColumn
            {
                HeaderText = "Type",
                Width = 80,
                ReadOnly = true,
                Name = "colType"
            };

            dgvConnectors.Columns.AddRange(colCheck, colName, colPart, colPins, colType);
            dgvConnectors.CellContentClick += DgvConnectors_CellContentClick;
            dgvConnectors.CellDoubleClick += DgvConnectors_CellDoubleClick;

            pnlGrid.Controls.Add(dgvConnectors);

            Controls.Add(pnlGrid);
            Controls.Add(pnlBottom);
            Controls.Add(pnlSearch);
            Controls.Add(pnlHeader);
        }

        private void ApplyStyles()
        {
            UIStyleHelper.StyleDataGridView(dgvConnectors);
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

        private void LoadConnectors()
        {
            // Only list connectors that are not already attached
            _allAvailableConnectors = ConnectorService.GetAllConnectors()
                .Where(c => !_alreadyAttachedIds.Contains(c.Id))
                .ToList();

            FilterConnectors();
        }

        private void FilterConnectors()
        {
            string term = txtSearch.Text.Trim().ToLowerInvariant();

            var filtered = string.IsNullOrEmpty(term)
                ? _allAvailableConnectors
                : _allAvailableConnectors.Where(c =>
                    (c.Name != null && c.Name.ToLowerInvariant().Contains(term)) ||
                    (c.PartNumber != null && c.PartNumber.ToLowerInvariant().Contains(term)) ||
                    (c.Type != null && c.Type.ToLowerInvariant().Contains(term))
                ).ToList();

            dgvConnectors.Rows.Clear();
            foreach (var c in filtered)
            {
                bool isChecked = _selectedConnectorIds.Contains(c.Id);
                int rowIndex = dgvConnectors.Rows.Add(
                    isChecked,
                    c.Name,
                    c.PartNumber,
                    c.TotalPin,
                    c.Type
                );
                dgvConnectors.Rows[rowIndex].Tag = c;
            }
        }

        private void DgvConnectors_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0) return;

            if (dgvConnectors.Rows[e.RowIndex].Tag is Connector c)
            {
                bool current = Convert.ToBoolean(dgvConnectors.Rows[e.RowIndex].Cells[0].Value);
                bool next = !current;
                dgvConnectors.Rows[e.RowIndex].Cells[0].Value = next;

                if (next) _selectedConnectorIds.Add(c.Id);
                else _selectedConnectorIds.Remove(c.Id);
            }
        }

        private void DgvConnectors_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvConnectors.Rows[e.RowIndex].Tag is Connector c)
            {
                bool current = Convert.ToBoolean(dgvConnectors.Rows[e.RowIndex].Cells[0].Value);
                bool next = !current;
                dgvConnectors.Rows[e.RowIndex].Cells[0].Value = next;

                if (next) _selectedConnectorIds.Add(c.Id);
                else _selectedConnectorIds.Remove(c.Id);
            }
        }

        private void BtnAttach_Click(object? sender, EventArgs e)
        {
            // Gather all checked connectors
            SelectedConnectors = _allAvailableConnectors
                .Where(c => _selectedConnectorIds.Contains(c.Id))
                .ToList();

            if (SelectedConnectors.Count == 0)
            {
                MessageBox.Show("Please select at least one connector to attach.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
