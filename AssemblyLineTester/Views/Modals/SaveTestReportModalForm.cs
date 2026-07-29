using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using AssemblyLineTester.Helpers;
using AssemblyLineTester.Models.Dto;
using AssemblyLineTester.Services;
using System.Diagnostics; 

namespace AssemblyLineTester.Views.Modals
{
    public partial class SaveTestReportModalForm : Form
    {
        private readonly TestReportService _reportService = new();
        private readonly List<LiveTestDetailRowDto> _rows;
        private readonly string _productModel;
        private readonly string _ecnNumber;
        private readonly string _productType;
        private readonly string _revision;
        private readonly string _area;
        private readonly string _partNumber;
        private readonly string _overallResult;
        private readonly DateTime _testTime;
        private readonly int? _existingReportId;

        public SaveTestReportModalForm(
            string productModel,
            string ecnNumber,
            string productType,
            string revision,
            string area,
            string operatorName,
            string partNumber,
            string serialNumber,
            string overallResult,
            DateTime testTime,
            List<LiveTestDetailRowDto> rows,
            int? existingReportId = null)
        {
            _productModel = productModel;
            _ecnNumber = ecnNumber;
            _productType = productType;
            _revision = revision;
            _area = area;
            _partNumber = partNumber;
            _overallResult = overallResult;
            _testTime = testTime;
            _rows = rows;
            _existingReportId = existingReportId;

            InitializeComponent();

            lblGenModelVal.Text = _productModel;
            lblGenEcnVal.Text = _ecnNumber;
            lblGenTypeVal.Text = _productType;
            lblGenRevVal.Text = _revision;
            lblGenAreaVal.Text = _area;
            lblGenPartVal.Text = _partNumber;
            lblGenTimeVal.Text = _testTime.ToString("dd MMM yyyy HH:mm:ss");
            txtGenSnVal.Text = string.IsNullOrEmpty(serialNumber) ? $"SN-{_testTime:HHmmss}" : serialNumber;
            lblGenResVal.Text = _overallResult;
            txtGenOpVal.Text = SessionManager.CurrentUser?.FullName ?? "Unknown";
            txtGenOpVal.ReadOnly = true;
            txtGenOpVal.Enabled = false;
            txtGenOpVal.BackColor = Color.FromArgb(241, 245, 249); 
            txtGenOpVal.TabStop = false;

            if (string.Equals(_overallResult, "PASS", StringComparison.OrdinalIgnoreCase))
            {
                lblGenResVal.ForeColor = Color.FromArgb(4, 120, 87);
            }
            else
            {
                lblGenResVal.ForeColor = Color.FromArgb(220, 38, 38);
            }

            btnClose.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            btnConfirmSave.Click += BtnConfirmSave_Click;

            btnClose.MouseEnter += (s, e) => { btnClose.BackColor = Color.FromArgb(225, 29, 72); btnClose.ForeColor = Color.White; };
            btnClose.MouseLeave += (s, e) => { btnClose.BackColor = Color.Transparent; btnClose.ForeColor = Color.FromArgb(203, 213, 225); };

            pnlCustomHeader.MouseDown += TitleBar_MouseDown;
            lblHeaderTitle.MouseDown += TitleBar_MouseDown;
            iconLogo.MouseDown += TitleBar_MouseDown;

            UIStyleHelper.SetupCardRoundedEdges(pnlGenInfoBox, 12, Color.FromArgb(226, 232, 240), 1.5f);
            UIStyleHelper.SetupButtonRoundedEdges(btnConfirmSave, 10, Color.FromArgb(22, 163, 74), Color.FromArgb(34, 197, 94), Color.FromArgb(21, 128, 61), Color.Transparent);
            UIStyleHelper.SetupButtonRoundedEdges(btnCancel, 10, Color.FromArgb(226, 232, 240), Color.FromArgb(241, 245, 249), Color.FromArgb(203, 213, 225), Color.Transparent);

            ApplyFormRounding();
            UpdateFooterButtonPositions();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                UIStyleHelper.ScaleExplicitFonts(this, UIStyleHelper.GetHostScaleFactor(this));
            }
        }

        private void BtnConfirmSave_Click(object? sender, EventArgs e)
        {
            string finalOp = SessionManager.CurrentUser?.FullName ?? "Unknown";
            string finalSn = txtGenSnVal.Text.Trim();

            if (string.IsNullOrEmpty(finalSn))
            {
                MessageBox.Show("Please provide a Serial Number before saving.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGenSnVal.Focus();
                return;
            }

            try
            {
                if (_existingReportId.HasValue)
                {
                    _reportService.ConfirmDraft(_existingReportId.Value, finalSn);
                }
                else
                {
                    _reportService.SaveLiveTestReport(
                        _productModel, _productType, _area, _ecnNumber, _revision, _partNumber,
                        finalSn, finalOp, _testTime, _rows, _overallResult);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save test report:\n{ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyFormRounding();
            UpdateFooterButtonPositions();
        }

        private void UpdateFooterButtonPositions()
        {
            if (pnlFooter == null || btnConfirmSave == null || btnCancel == null) return;
            int marginRight = 45;
            int gap = 19;
            int btnY = (pnlFooter.Height - btnConfirmSave.Height) / 2;

            btnConfirmSave.Location = new Point(pnlFooter.Width - btnConfirmSave.Width - marginRight, btnY);
            btnCancel.Location = new Point(btnConfirmSave.Left - btnCancel.Width - gap, btnY);
        }

        private void ApplyFormRounding()
        {
            if (Width <= 0 || Height <= 0) return;
            int radius = 24;
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            using (var path = new GraphicsPath())
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
            if (e.Button == MouseButtons.Left)
            {
            NativeMethods.DragWindow(Handle);
            }
        }
    }
}
