using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using AssemblyLineTester.Helpers;
using AssemblyLineTester.Models;
using AssemblyLineTester.Services;

namespace AssemblyLineTester.Views.Modals
{
    public partial class LoadProgramModalForm : Form
    {
        private readonly TestConfigService _configService;
        private List<TestConfiguration> _allConfigs = new();

        public TestConfiguration? SelectedConfiguration { get; private set; }
        public string? LoadedFilePath { get; private set; }

        public LoadProgramModalForm(TestConfigService configService)
        {
            _configService = configService ?? new TestConfigService();

            InitializeComponent();
            SetupStyles();
            SetupGridColumns();
            SetupEvents();

            RefreshConfigs();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                UIStyleHelper.ScaleExplicitFonts(this, UIStyleHelper.GetHostScaleFactor(this));
            }
        }

        private void SetupStyles()
        {
            UIStyleHelper.SetupCardRoundedEdges(pnlGridCard, 12, Color.FromArgb(226, 232, 240), 1.5f);
            UIStyleHelper.SetupButtonRoundedEdges(btnLoad, 10, Color.FromArgb(22, 163, 74), Color.FromArgb(34, 197, 94), Color.FromArgb(21, 128, 61), Color.Transparent);
            UIStyleHelper.SetupButtonRoundedEdges(btnBrowseFile, 10, Color.FromArgb(14, 165, 233), Color.FromArgb(56, 189, 248), Color.FromArgb(2, 132, 199), Color.Transparent);
            UIStyleHelper.SetupButtonRoundedEdges(btnCancel, 10, Color.FromArgb(226, 232, 240), Color.FromArgb(241, 245, 249), Color.FromArgb(203, 213, 225), Color.Transparent);
            UIStyleHelper.SetupButtonRoundedEdges(btnSearch, 8, Color.FromArgb(15, 23, 42), Color.FromArgb(30, 41, 59), Color.FromArgb(2, 6, 23), Color.Transparent);
            UIStyleHelper.SetupButtonRoundedEdges(btnClear, 8, Color.FromArgb(226, 232, 240), Color.FromArgb(241, 245, 249), Color.FromArgb(203, 213, 225), Color.Transparent);

            ApplyFormRounding();
        }

        private void SetupGridColumns()
        {
            gridConfigs.Columns.Clear();
            gridConfigs.Columns.Add("ConfigName", "Config Name");
            gridConfigs.Columns.Add("ProductModel", "Product Model");
            gridConfigs.Columns.Add("CreatedAt", "Created At");
            gridConfigs.Columns.Add("UpdatedAt", "Updated At");
            gridConfigs.Columns.Add("IsActive", "Active");
            gridConfigs.Columns.Add("NetCount", "Nets");

            if (gridConfigs.Columns["ConfigName"] != null) gridConfigs.Columns["ConfigName"]!.FillWeight = 140;
            if (gridConfigs.Columns["ProductModel"] != null) gridConfigs.Columns["ProductModel"]!.FillWeight = 120;
            if (gridConfigs.Columns["CreatedAt"] != null) gridConfigs.Columns["CreatedAt"]!.FillWeight = 110;
            if (gridConfigs.Columns["UpdatedAt"] != null) gridConfigs.Columns["UpdatedAt"]!.FillWeight = 110;
            if (gridConfigs.Columns["IsActive"] != null) gridConfigs.Columns["IsActive"]!.FillWeight = 60;
            if (gridConfigs.Columns["NetCount"] != null) gridConfigs.Columns["NetCount"]!.FillWeight = 60;

            gridConfigs.EnableHeadersVisualStyles = false;
            gridConfigs.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249);
            gridConfigs.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(51, 65, 85);
            gridConfigs.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            gridConfigs.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            gridConfigs.DefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);
            gridConfigs.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 242, 254);
            gridConfigs.DefaultCellStyle.SelectionForeColor = Color.FromArgb(14, 116, 144);
            gridConfigs.RowTemplate.Height = 36;
        }

        private void SetupEvents()
        {
            btnClose.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            btnClose.MouseEnter += (s, e) => { btnClose.BackColor = Color.FromArgb(225, 29, 72); btnClose.IconColor = Color.White; };
            btnClose.MouseLeave += (s, e) => { btnClose.BackColor = Color.Transparent; btnClose.IconColor = Color.FromArgb(203, 213, 225); };

            pnlCustomHeader.MouseDown += TitleBar_MouseDown;
            lblHeaderTitle.MouseDown += TitleBar_MouseDown;
            iconLogo.MouseDown += TitleBar_MouseDown;

            btnLoad.Click += BtnLoad_Click;
            btnBrowseFile.Click += BtnBrowseFile_Click;
            btnSearch.Click += (s, e) => FilterGrid();
            btnClear.Click += (s, e) => { txtSearch.Clear(); FilterGrid(); };
            txtSearch.TextChanged += (s, e) => FilterGrid();

            gridConfigs.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    LoadSelectedConfig();
                }
            };
        }

        private void RefreshConfigs()
        {
            try
            {
                _allConfigs = _configService.GetConfigurations(null).ToList();
                FilterGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading configurations: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FilterGrid()
        {
            string query = txtSearch.Text?.Trim() ?? "";
            var filtered = string.IsNullOrWhiteSpace(query)
                ? _allConfigs
                : _allConfigs.Where(c => c.ConfigName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                                         c.ProductModel.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();

            PopulateGrid(filtered);
        }

        private void PopulateGrid(IEnumerable<TestConfiguration> list)
        {
            gridConfigs.Rows.Clear();
            foreach (var c in list)
            {
                int idx = gridConfigs.Rows.Add(
                    c.ConfigName,
                    c.ProductModel,
                    AppTime.ToLocal(c.CreatedAt).ToString("g"),
                    c.UpdatedAt?.ToString("g") ?? "-",
                    c.IsActive ? "Yes" : "No",
                    c.Nets?.Count ?? 0
                );
                gridConfigs.Rows[idx].Tag = c.ConfigName;
            }
        }

        private void BtnLoad_Click(object? sender, EventArgs e)
        {
            LoadSelectedConfig();
        }

        private void LoadSelectedConfig()
        {
            if (gridConfigs.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a test program configuration from the table to load.", "Load Program", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string name = gridConfigs.SelectedRows[0].Tag as string ?? gridConfigs.SelectedRows[0].Cells[0].Value?.ToString() ?? string.Empty;
            var cfg = _configService.GetConfigurationByName(name);
            if (cfg == null)
            {
                MessageBox.Show("Selected program configuration could not be loaded (it may have been deleted).", "Load Program", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SelectedConfiguration = cfg;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnBrowseFile_Click(object? sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Title = "Select Test Program File",
                Filter = "Test Program Files (*.json;*.xml;*.txt)|*.json;*.xml;*.txt|All Files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                LoadedFilePath = ofd.FileName;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyFormRounding();
        }

        private void ApplyFormRounding()
        {
            if (Width <= 0 || Height <= 0) return;
            int radius = 24;
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            using var path = new GraphicsPath();
            int d = radius * 2;
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            this.Region = new Region(path);
        }


        private void TitleBar_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
            NativeMethods.DragWindow(Handle);
            }
        }
    }
}
