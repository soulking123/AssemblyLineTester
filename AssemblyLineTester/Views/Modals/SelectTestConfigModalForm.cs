using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AssemblyLineTester.Data;
using AssemblyLineTester.Models;
using AssemblyLineTester.Helpers;

namespace AssemblyLineTester.Views.Modals
{
    public partial class SelectTestConfigModalForm : Form
    {
        private readonly AppDbContext _db;
        public TestConfiguration? SelectedConfiguration { get; private set; }

        public SelectTestConfigModalForm(AppDbContext db)
        {
            _db = db;
            InitializeComponent();

            btnCancel.Click += (s, e) => this.Close();
            btnSelect.Click += BtnSelect_Click;
            gridConfigs.CellDoubleClick += GridConfigs_CellDoubleClick;

            pnlCustomHeader.MouseDown += TitleBar_MouseDown;
            iconLogo.MouseDown += TitleBar_MouseDown;

            LoadConfigurations();
            ApplyFormRounding();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                AssemblyLineTester.Helpers.UIStyleHelper.ScaleExplicitFonts(this, AssemblyLineTester.Helpers.UIStyleHelper.GetHostScaleFactor(this));
            }
        }

        private void LoadConfigurations()
        {
            try
            {
                var list = _db.TestConfigurations
                    .OrderByDescending(c => c.UpdatedAt ?? c.CreatedAt)
                    .Select(c => new
                    {
                        c.ConfigId,
                        ConfigName = c.ConfigName,
                        ProductModel = c.ProductModelName,
                        LastUpdate = AppTime.ToLocal(c.UpdatedAt ?? c.CreatedAt).ToString("yyyy-MM-dd HH:mm:ss")
                    })
                    .ToList();

                gridConfigs.DataSource = list;

                // Style Grid Columns
                if (gridConfigs.Columns["ConfigId"] != null)
                {
                    gridConfigs.Columns["ConfigId"]!.Visible = false;
                }

                if (gridConfigs.Columns["ConfigName"] != null)
                {
                    gridConfigs.Columns["ConfigName"]!.HeaderText = "Configuration Name";
                }

                if (gridConfigs.Columns["ProductModel"] != null)
                {
                    gridConfigs.Columns["ProductModel"]!.HeaderText = "Product Model";
                }

                if (gridConfigs.Columns["LastUpdate"] != null)
                {
                    gridConfigs.Columns["LastUpdate"]!.HeaderText = "Last Update";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading configurations: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSelect_Click(object? sender, EventArgs e)
        {
            ConfirmSelection();
        }

        private void GridConfigs_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ConfirmSelection();
            }
        }

        private void ConfirmSelection()
        {
            if (gridConfigs.CurrentRow != null)
            {
                int configId = Convert.ToInt32(gridConfigs.CurrentRow.Cells["ConfigId"].Value);
                SelectedConfiguration = _db.TestConfigurations.FirstOrDefault(c => c.ConfigId == configId);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select a test configuration from the list.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            int radius = 16;
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            using (var path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                int d = radius * 2;
                path.StartFigure();
                path.AddArc(rect.X, rect.Y, d, d, 180, 90);
                path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
                path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
                path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
                path.CloseFigure();
                this.Region = new Region(path);
            }
        }


        private void TitleBar_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
            NativeMethods.DragWindow(Handle);
            }
        }
    }
}
