using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AssemblyLineTester.Data;
using AssemblyLineTester.Helpers;
using AssemblyLineTester.Models;
using AssemblyLineTester.Services;
using AssemblyLineTester.Views.Modals;

namespace AssemblyLineTester.Views.Pages.ProductDatabaseSubPages
{
    public partial class HarnessAssemblySubPage : UserControl
    {
        private List<HarnessAssembly> _allHarnesses = new();
        private List<HarnessAssembly> _filteredHarnesses = new();
        private HarnessAssembly? _selectedHarness = null;

        // Connectors attached to the active harness in the form
        private List<Connector> _attachedConnectors = new();

        // Image tracking for the active harness in the form (preserves exact order)
        private List<string> _orderedImagePaths = new();
        private List<bool> _orderedReportInclusions = new();
        private int _currentImageIndex = -1;
        private bool _isUpdatingCheckbox = false;

        private bool _isPopulatingForm = false;

        public HarnessAssemblySubPage()
        {
            InitializeComponent();
            ApplyCustomStyles();
            SetupEvents();

            DoubleBuffered = true;
            UIStyleHelper.EnableDoubleBuffering(dgvHarnessList);
            UIStyleHelper.EnableDoubleBuffering(dgvAttachedConnectors);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                LoadHarnessData();
            }
        }

        private void ApplyCustomStyles()
        {
            UIStyleHelper.StyleDataGridView(dgvHarnessList);
            UIStyleHelper.StyleDataGridView(dgvAttachedConnectors);

            UIStyleHelper.SetupButtonRoundedEdges(btnNewHarness, 8,
                Color.FromArgb(245, 158, 11), Color.FromArgb(217, 119, 6), Color.FromArgb(180, 83, 9), Color.Transparent);

            UIStyleHelper.SetupButtonRoundedEdges(btnAddPicture, 6,
                Color.FromArgb(16, 185, 129), Color.FromArgb(5, 150, 105), Color.FromArgb(4, 120, 87), Color.Transparent);

            UIStyleHelper.SetupButtonRoundedEdges(btnDeletePicture, 6,
                Color.FromArgb(239, 68, 68), Color.FromArgb(220, 38, 38), Color.FromArgb(185, 28, 28), Color.Transparent);

            UIStyleHelper.SetupButtonRoundedEdges(btnPrevPicture, 6,
                Color.FromArgb(71, 85, 105), Color.FromArgb(51, 65, 85), Color.FromArgb(30, 41, 59), Color.Transparent);

            UIStyleHelper.SetupButtonRoundedEdges(btnNextPicture, 6,
                Color.FromArgb(71, 85, 105), Color.FromArgb(51, 65, 85), Color.FromArgb(30, 41, 59), Color.Transparent);

            UIStyleHelper.SetupButtonRoundedEdges(btnAttach, 6,
                Color.FromArgb(16, 185, 129), Color.FromArgb(5, 150, 105), Color.FromArgb(4, 120, 87), Color.Transparent);

            UIStyleHelper.SetupButtonRoundedEdges(btnRemove, 6,
                Color.FromArgb(239, 68, 68), Color.FromArgb(220, 38, 38), Color.FromArgb(185, 28, 28), Color.Transparent);

            UIStyleHelper.SetupButtonRoundedEdges(btnSave, 8,
                Color.FromArgb(16, 185, 129), Color.FromArgb(5, 150, 105), Color.FromArgb(4, 120, 87), Color.Transparent);

            UIStyleHelper.SetupButtonRoundedEdges(btnDelete, 8,
                Color.FromArgb(239, 68, 68), Color.FromArgb(220, 38, 38), Color.FromArgb(185, 28, 28), Color.Transparent);

            UIStyleHelper.SetupCardRoundedEdges(pnlRightContainer, 10, Color.FromArgb(226, 232, 240), 1f);

            picBox.SizeMode = PictureBoxSizeMode.Zoom;
            picBox.Cursor = Cursors.Hand;
        }

        private void SetupEvents()
        {
            btnNewHarness.Click += (s, e) => ClearForm();
            txtSearch.TextChanged += (s, e) => ApplyFilter();
            dgvHarnessList.SelectionChanged += DgvHarnessList_SelectionChanged;

            btnAddPicture.Click += BtnAddPicture_Click;
            btnDeletePicture.Click += BtnDeletePicture_Click;
            btnPrevPicture.Click += BtnPrevPicture_Click;
            btnNextPicture.Click += BtnNextPicture_Click;
            picBox.Click += PicBox_Click;

            chkIncludeInReport.CheckedChanged += (s, e) =>
            {
                if (_isUpdatingCheckbox) return;
                if (_currentImageIndex >= 0 && _currentImageIndex < _orderedReportInclusions.Count)
                {
                    if (chkIncludeInReport.Checked)
                    {
                        bool alreadyHasIncluded = false;
                        for (int i = 0; i < _orderedReportInclusions.Count; i++)
                        {
                            if (i != _currentImageIndex && _orderedReportInclusions[i])
                            {
                                alreadyHasIncluded = true;
                                break;
                            }
                        }

                        if (alreadyHasIncluded)
                        {
                            MessageBox.Show("Only 1 picture can be included to report", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _isUpdatingCheckbox = true;
                            chkIncludeInReport.Checked = false;
                            _isUpdatingCheckbox = false;
                            return;
                        }
                    }

                    _orderedReportInclusions[_currentImageIndex] = chkIncludeInReport.Checked;
                }
            };

            btnAttach.Click += BtnAttach_Click;
            btnRemove.Click += BtnRemove_Click;

            btnSave.Click += BtnSave_Click;
            btnDelete.Click += BtnDelete_Click;
        }

        public void LoadHarnessData()
        {
            _allHarnesses = HarnessService.GetAllHarnesses();
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            string term = txtSearch.Text.Trim().ToLowerInvariant();

            _filteredHarnesses = string.IsNullOrEmpty(term)
                ? _allHarnesses.ToList()
                : _allHarnesses.Where(h =>
                    (h.Name != null && h.Name.ToLowerInvariant().Contains(term)) ||
                    (h.PartNumber != null && h.PartNumber.ToLowerInvariant().Contains(term))
                ).ToList();

            _isPopulatingForm = true;
            dgvHarnessList.Rows.Clear();

            for (int i = 0; i < _filteredHarnesses.Count; i++)
            {
                var h = _filteredHarnesses[i];
                int rowIndex = dgvHarnessList.Rows.Add(
                    i + 1,
                    h.Name,
                    h.PartNumber,
                    h.Connectors?.Count ?? 0
                );
                dgvHarnessList.Rows[rowIndex].Tag = h.Id;
            }
            _isPopulatingForm = false;

            if (_filteredHarnesses.Count > 0)
            {
                SelectRowByHarnessId(_selectedHarness?.Id ?? _filteredHarnesses[0].Id);
            }
            else
            {
                ClearForm();
            }
        }

        private void DgvHarnessList_SelectionChanged(object? sender, EventArgs e)
        {
            if (_isPopulatingForm) return;

            if (dgvHarnessList.SelectedRows.Count > 0)
            {
                var row = dgvHarnessList.SelectedRows[0];
                if (row.Tag is int harnessId)
                {
                    var found = _allHarnesses.FirstOrDefault(h => h.Id == harnessId);
                    if (found != null)
                    {
                        PopulateForm(found);
                    }
                }
            }
        }

        private void PopulateForm(HarnessAssembly harness)
        {
            _selectedHarness = harness;

            txtName.Text = harness.Name;
            txtPartNumber.Text = harness.PartNumber;
            txtDescription.Text = harness.Description;

            // Load attached connectors
            _attachedConnectors = harness.Connectors != null
                ? new List<Connector>(harness.Connectors)
                : new List<Connector>();
            RefreshAttachedConnectorsGrid();

            // Load attached images (preserving stored order)
            _orderedImagePaths = HarnessService.GetHarnessImagePaths(harness);
            _orderedReportInclusions = harness.IncludedForReport != null
                ? new List<bool>(harness.IncludedForReport)
                : new List<bool>();
            while (_orderedReportInclusions.Count < _orderedImagePaths.Count)
            {
                _orderedReportInclusions.Add(false);
            }
            if (_orderedReportInclusions.Count > _orderedImagePaths.Count)
            {
                _orderedReportInclusions = _orderedReportInclusions.Take(_orderedImagePaths.Count).ToList();
            }
            bool foundIncluded = false;
            for (int i = 0; i < _orderedReportInclusions.Count; i++)
            {
                if (_orderedReportInclusions[i])
                {
                    if (foundIncluded) _orderedReportInclusions[i] = false;
                    else foundIncluded = true;
                }
            }
            _currentImageIndex = _orderedImagePaths.Count > 0 ? 0 : -1;

            UpdateImageDisplay();
            btnDelete.Enabled = true;
        }

        private void ClearForm()
        {
            _selectedHarness = null;

            txtName.Clear();
            txtPartNumber.Clear();
            txtDescription.Clear();

            _attachedConnectors.Clear();
            RefreshAttachedConnectorsGrid();

            _orderedImagePaths.Clear();
            _orderedReportInclusions.Clear();
            _currentImageIndex = -1;

            UpdateImageDisplay();
            btnDelete.Enabled = false;

            if (dgvHarnessList.SelectedRows.Count > 0)
            {
                _isPopulatingForm = true;
                dgvHarnessList.ClearSelection();
                _isPopulatingForm = false;
            }

            txtName.Focus();
        }

        private void RefreshAttachedConnectorsGrid()
        {
            dgvAttachedConnectors.Rows.Clear();
            for (int i = 0; i < _attachedConnectors.Count; i++)
            {
                var c = _attachedConnectors[i];
                int rowIndex = dgvAttachedConnectors.Rows.Add(
                    i + 1,
                    c.Name,
                    c.PartNumber,
                    c.TotalPin,
                    c.Type
                );
                dgvAttachedConnectors.Rows[rowIndex].Tag = c;
            }
        }

        private void BtnAttach_Click(object? sender, EventArgs e)
        {
            var alreadyAttachedIds = _attachedConnectors.Select(c => c.Id).ToList();
            using var modal = new SelectConnectorModalForm(alreadyAttachedIds);
            if (modal.ShowDialog(this) == DialogResult.OK && modal.SelectedConnectors.Count > 0)
            {
                foreach (var c in modal.SelectedConnectors)
                {
                    if (!_attachedConnectors.Any(existing => existing.Id == c.Id))
                    {
                        _attachedConnectors.Add(c);
                    }
                }
                RefreshAttachedConnectorsGrid();
            }
        }

        private void BtnRemove_Click(object? sender, EventArgs e)
        {
            if (dgvAttachedConnectors.SelectedRows.Count == 0) return;

            var row = dgvAttachedConnectors.SelectedRows[0];
            if (row.Tag is Connector c)
            {
                _attachedConnectors.RemoveAll(item => item.Id == c.Id);
                RefreshAttachedConnectorsGrid();
            }
        }

        private void UpdateImageDisplay()
        {
            if (_orderedImagePaths.Count == 0)
            {
                _currentImageIndex = -1;
                picBox.Image = null;
                lblPicture.Text = "Picture (0/0)";
                btnDeletePicture.Enabled = false;
                btnPrevPicture.Enabled = false;
                btnNextPicture.Enabled = false;
                chkIncludeInReport.Visible = false;
                return;
            }

            if (_currentImageIndex < 0) _currentImageIndex = 0;
            if (_currentImageIndex >= _orderedImagePaths.Count) _currentImageIndex = _orderedImagePaths.Count - 1;

            string currentFile = _orderedImagePaths[_currentImageIndex];
            ImageLoader.SetImage(picBox, currentFile);

            lblPicture.Text = $"Picture ({_currentImageIndex + 1}/{_orderedImagePaths.Count})";
            chkIncludeInReport.Left = lblPicture.Right + 14;
            chkIncludeInReport.Top = lblPicture.Top - 1;
            chkIncludeInReport.Visible = true;

            _isUpdatingCheckbox = true;
            chkIncludeInReport.Checked = _currentImageIndex >= 0 &&
                                         _currentImageIndex < _orderedReportInclusions.Count &&
                                         _orderedReportInclusions[_currentImageIndex];
            _isUpdatingCheckbox = false;

            btnDeletePicture.Enabled = true;
            btnPrevPicture.Enabled = _currentImageIndex > 0;
            btnNextPicture.Enabled = _currentImageIndex < _orderedImagePaths.Count - 1;
        }

        private void PicBox_Click(object? sender, EventArgs e)
        {
            if (_orderedImagePaths.Count == 0 || _currentImageIndex < 0) return;

            string title = !string.IsNullOrWhiteSpace(txtPartNumber.Text)
                ? $"HARNESS: {txtPartNumber.Text.Trim()}"
                : "HARNESS PREVIEW";

            using var modal = new ImagePreviewModalForm(_orderedImagePaths, title, _currentImageIndex);
            modal.ShowDialog(this);
        }

        private void BtnPrevPicture_Click(object? sender, EventArgs e)
        {
            if (_currentImageIndex > 0)
            {
                _currentImageIndex--;
                UpdateImageDisplay();
            }
        }

        private void BtnNextPicture_Click(object? sender, EventArgs e)
        {
            if (_currentImageIndex < _orderedImagePaths.Count - 1)
            {
                _currentImageIndex++;
                UpdateImageDisplay();
            }
        }

        private void BtnAddPicture_Click(object? sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png",
                Multiselect = true,
                Title = "Select Harness Assembly Images"
            };

            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                int insertPos = _currentImageIndex >= 0 ? _currentImageIndex + 1 : 0;
                if (insertPos > _orderedImagePaths.Count) insertPos = _orderedImagePaths.Count;

                int firstInsertedIndex = -1;
                foreach (var file in ofd.FileNames)
                {
                    if (new FileInfo(file).Length > 2 * 1024 * 1024)
                    {
                        MessageBox.Show($"File '{Path.GetFileName(file)}' exceeds the 2MB size limit.", "File Too Large", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    if (!_orderedImagePaths.Contains(file))
                    {
                        _orderedImagePaths.Insert(insertPos, file);
                        _orderedReportInclusions.Insert(insertPos, false);
                        if (firstInsertedIndex == -1) firstInsertedIndex = insertPos;
                        insertPos++;
                    }
                }

                if (firstInsertedIndex != -1)
                {
                    _currentImageIndex = firstInsertedIndex;
                    UpdateImageDisplay();
                }
            }
        }

        private void BtnDeletePicture_Click(object? sender, EventArgs e)
        {
            if (_currentImageIndex < 0 || _currentImageIndex >= _orderedImagePaths.Count) return;

            _orderedImagePaths.RemoveAt(_currentImageIndex);
            if (_currentImageIndex < _orderedReportInclusions.Count)
            {
                _orderedReportInclusions.RemoveAt(_currentImageIndex);
            }

            if (_currentImageIndex >= _orderedImagePaths.Count && _currentImageIndex > 0)
            {
                _currentImageIndex = _orderedImagePaths.Count - 1;
            }
            if (_orderedImagePaths.Count == 0)
            {
                _currentImageIndex = -1;
            }

            UpdateImageDisplay();
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string partNumber = txtPartNumber.Text.Trim();

            if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(partNumber))
            {
                MessageBox.Show("Please enter a Harness Assembly Name or Part Number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            var connectorIds = _attachedConnectors.Select(c => c.Id).ToList();

            if (_selectedHarness == null)
            {
                // Create
                var newHarness = new HarnessAssembly
                {
                    Name = name,
                    PartNumber = partNumber,
                    Description = txtDescription.Text.Trim()
                };

                var (created, error) = HarnessService.AddHarness(newHarness, connectorIds, _orderedImagePaths, _orderedReportInclusions);
                if (created == null)
                {
                    MessageBox.Show(error ?? "Failed to create harness assembly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show($"Harness Assembly '{created.PartNumber}' created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadHarnessData();
                SelectRowByHarnessId(created.Id);
            }
            else
            {
                // Update
                _selectedHarness.Name = name;
                _selectedHarness.PartNumber = partNumber;
                _selectedHarness.Description = txtDescription.Text.Trim();

                var (success, error) = HarnessService.UpdateHarnessOrdered(_selectedHarness, connectorIds, _orderedImagePaths, _orderedReportInclusions);
                if (!success)
                {
                    MessageBox.Show(error ?? "Failed to update harness assembly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show($"Harness Assembly '{_selectedHarness.PartNumber}' updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                int savedId = _selectedHarness.Id;
                LoadHarnessData();
                SelectRowByHarnessId(savedId);
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (_selectedHarness == null) return;

            // Check if harness is used by any ProductModel
            using var db = new AppDbContext();
            var usingProducts = db.ProductModels
                .Where(p => p.HarnessAssemblies.Any(h => h.Id == _selectedHarness.Id))
                .Select(p => $"{p.Name} ({p.PartNumber})")
                .ToList();

            if (usingProducts.Count > 0)
            {
                var displayList = usingProducts.Take(5).ToList();
                string listText = "• " + string.Join("\n• ", displayList);
                if (usingProducts.Count > 5)
                {
                    listText += $"\n...and {usingProducts.Count - 5} more";
                }

                MessageBox.Show(
                    $"Cannot delete harness assembly '{_selectedHarness.Name}' because it is currently used in the following Product Models:\n\n{listText}\n\nPlease detach it from these product models before deleting.",
                    "Cannot Delete Harness Assembly",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"Are you sure you want to delete harness assembly '{_selectedHarness.Name}'?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            var (success, error) = HarnessService.DeleteHarness(_selectedHarness.Id);
            if (!success)
            {
                MessageBox.Show(error ?? "Failed to delete harness assembly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Harness Assembly deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearForm();
            LoadHarnessData();
        }

        private void SelectRowByHarnessId(int id)
        {
            for (int i = 0; i < dgvHarnessList.Rows.Count; i++)
            {
                if (dgvHarnessList.Rows[i].Tag is int harnessId && harnessId == id)
                {
                    _isPopulatingForm = true;
                    dgvHarnessList.ClearSelection();
                    dgvHarnessList.Rows[i].Selected = true;
                    _isPopulatingForm = false;

                    var found = _allHarnesses.FirstOrDefault(h => h.Id == id);
                    if (found != null)
                    {
                        PopulateForm(found);
                    }
                    return;
                }
            }
        }
    }
}
