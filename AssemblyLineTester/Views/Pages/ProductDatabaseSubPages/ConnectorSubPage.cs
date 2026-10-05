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
    public partial class ConnectorSubPage : UserControl
    {
        private List<Connector> _allConnectors = new();
        private List<Connector> _filteredConnectors = new();
        private Connector? _selectedConnector = null;

        // Image tracking for the active connector in the form (preserves exact order)
        private List<string> _orderedImagePaths = new();
        private List<bool> _orderedReportInclusions = new();
        private int _currentImageIndex = -1;

        private bool _isPopulatingForm = false;
        private bool _isUpdatingCheckbox = false;

        public ConnectorSubPage()
        {
            InitializeComponent();
            ApplyCustomStyles();
            SetupEvents();

            DoubleBuffered = true;
            UIStyleHelper.EnableDoubleBuffering(dgvConnectorList);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                LoadConnectorData();
            }
        }

        private void ApplyCustomStyles()
        {
            UIStyleHelper.StyleDataGridView(dgvConnectorList);

            float scale = UIStyleHelper.GetHostScaleFactor(dgvConnectorList);
            colNo.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colNo.Width = (int)Math.Round(55 * scale);
            colTotalPin.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colTotalPin.Width = (int)Math.Round(90 * scale);
            colType.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colType.Width = (int)Math.Round(110 * scale);

            UIStyleHelper.SetupButtonRoundedEdges(btnNewConnector, 8,
                Color.FromArgb(245, 158, 11), Color.FromArgb(217, 119, 6), Color.FromArgb(180, 83, 9), Color.Transparent);

            UIStyleHelper.SetupButtonRoundedEdges(btnAddPicture, 6,
                Color.FromArgb(16, 185, 129), Color.FromArgb(5, 150, 105), Color.FromArgb(4, 120, 87), Color.Transparent);

            UIStyleHelper.SetupButtonRoundedEdges(btnDeletePicture, 6,
                Color.FromArgb(239, 68, 68), Color.FromArgb(220, 38, 38), Color.FromArgb(185, 28, 28), Color.Transparent);

            UIStyleHelper.SetupButtonRoundedEdges(btnPrevPicture, 6,
                Color.FromArgb(71, 85, 105), Color.FromArgb(51, 65, 85), Color.FromArgb(30, 41, 59), Color.Transparent);

            UIStyleHelper.SetupButtonRoundedEdges(btnNextPicture, 6,
                Color.FromArgb(71, 85, 105), Color.FromArgb(51, 65, 85), Color.FromArgb(30, 41, 59), Color.Transparent);

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
            btnNewConnector.Click += (s, e) => ClearForm();
            txtSearch.TextChanged += (s, e) => ApplyFilter();
            dgvConnectorList.SelectionChanged += DgvConnectorList_SelectionChanged;

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

            btnSave.Click += BtnSave_Click;
            btnDelete.Click += BtnDelete_Click;
        }

        public void LoadConnectorData()
        {
            _allConnectors = ConnectorService.GetAllConnectors();
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            string term = txtSearch.Text.Trim().ToLowerInvariant();

            _filteredConnectors = string.IsNullOrEmpty(term)
                ? _allConnectors.ToList()
                : _allConnectors.Where(c =>
                    (c.Name != null && c.Name.ToLowerInvariant().Contains(term)) ||
                    (c.PartNumber != null && c.PartNumber.ToLowerInvariant().Contains(term)) ||
                    (c.Type != null && c.Type.ToLowerInvariant().Contains(term))
                ).ToList();

            _isPopulatingForm = true;
            dgvConnectorList.Rows.Clear();

            for (int i = 0; i < _filteredConnectors.Count; i++)
            {
                var c = _filteredConnectors[i];
                int rowIndex = dgvConnectorList.Rows.Add(
                    i + 1,
                    c.Name,
                    c.PartNumber,
                    c.TotalPin,
                    c.Type
                );
                dgvConnectorList.Rows[rowIndex].Tag = c.Id;
            }
            _isPopulatingForm = false;

            if (_filteredConnectors.Count > 0)
            {
                SelectRowByConnectorId(_selectedConnector?.Id ?? _filteredConnectors[0].Id);
            }
            else
            {
                ClearForm();
            }
        }

        private void DgvConnectorList_SelectionChanged(object? sender, EventArgs e)
        {
            if (_isPopulatingForm) return;

            if (dgvConnectorList.SelectedRows.Count > 0)
            {
                var row = dgvConnectorList.SelectedRows[0];
                if (row.Tag is int connectorId)
                {
                    var found = _allConnectors.FirstOrDefault(c => c.Id == connectorId);
                    if (found != null)
                    {
                        PopulateForm(found);
                    }
                }
            }
        }

        private void PopulateForm(Connector connector)
        {
            _selectedConnector = connector;

            txtName.Text = connector.Name;
            txtPartNumber.Text = connector.PartNumber;
            txtTotalPins.Text = connector.TotalPin.ToString();
            txtType.Text = connector.Type;
            txtDescription.Text = connector.Description;

            // Load attached images (preserving exact stored order)
            _orderedImagePaths = ConnectorService.GetConnectorImagePaths(connector);
            _orderedReportInclusions = connector.IncludedForReport != null
                ? new List<bool>(connector.IncludedForReport)
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
            _selectedConnector = null;

            txtName.Clear();
            txtPartNumber.Clear();
            txtTotalPins.Clear();
            txtType.Clear();
            txtDescription.Clear();

            _orderedImagePaths.Clear();
            _orderedReportInclusions.Clear();
            _currentImageIndex = -1;

            UpdateImageDisplay();
            btnDelete.Enabled = false;

            if (dgvConnectorList.SelectedRows.Count > 0)
            {
                _isPopulatingForm = true;
                dgvConnectorList.ClearSelection();
                _isPopulatingForm = false;
            }

            txtName.Focus();
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
                ? $"CONNECTOR: {txtPartNumber.Text.Trim()}"
                : "CONNECTOR PREVIEW";

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
                Title = "Select Connector Images"
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
                MessageBox.Show("Please enter a Connector Name or Part Number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            int.TryParse(txtTotalPins.Text.Trim(), out int totalPins);

            if (_selectedConnector == null)
            {
                // Create
                var newConnector = new Connector
                {
                    Name = name,
                    PartNumber = partNumber,
                    TotalPin = totalPins,
                    Type = txtType.Text.Trim(),
                    Description = txtDescription.Text.Trim()
                };

                var (created, error) = ConnectorService.AddConnector(newConnector, _orderedImagePaths, _orderedReportInclusions);
                if (created == null)
                {
                    MessageBox.Show(error ?? "Failed to create connector.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show($"Connector '{created.PartNumber}' created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadConnectorData();
                SelectRowByConnectorId(created.Id);
            }
            else
            {
                // Update
                _selectedConnector.Name = name;
                _selectedConnector.PartNumber = partNumber;
                _selectedConnector.TotalPin = totalPins;
                _selectedConnector.Type = txtType.Text.Trim();
                _selectedConnector.Description = txtDescription.Text.Trim();

                var (success, error) = ConnectorService.UpdateConnectorOrdered(_selectedConnector, _orderedImagePaths, _orderedReportInclusions);
                if (!success)
                {
                    MessageBox.Show(error ?? "Failed to update connector.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show($"Connector '{_selectedConnector.PartNumber}' updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                int savedId = _selectedConnector.Id;
                LoadConnectorData();
                SelectRowByConnectorId(savedId);
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (_selectedConnector == null) return;

            // Check if connector is used by any harness assembly
            using var db = new AppDbContext();
            var usingHarnesses = db.HarnessAssemblies
                .Where(h => h.Connectors.Any(c => c.Id == _selectedConnector.Id))
                .Select(h => $"{h.Name} ({h.PartNumber})")
                .ToList();

            if (usingHarnesses.Count > 0)
            {
                var displayList = usingHarnesses.Take(5).ToList();
                string listText = "• " + string.Join("\n• ", displayList);
                if (usingHarnesses.Count > 5)
                {
                    listText += $"\n...and {usingHarnesses.Count - 5} more";
                }

                MessageBox.Show(
                    $"Cannot delete connector '{_selectedConnector.PartNumber}' because it is currently used in the following Harness Assemblies:\n\n{listText}\n\nPlease detach it from these harnesses before deleting.",
                    "Cannot Delete Connector",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"Are you sure you want to delete connector '{_selectedConnector.PartNumber}'?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            var (success, error) = ConnectorService.DeleteConnector(_selectedConnector.Id);
            if (!success)
            {
                MessageBox.Show(error ?? "Failed to delete connector.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Connector deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearForm();
            LoadConnectorData();
        }

        private void SelectRowByConnectorId(int id)
        {
            for (int i = 0; i < dgvConnectorList.Rows.Count; i++)
            {
                if (dgvConnectorList.Rows[i].Tag is int connectorId && connectorId == id)
                {
                    _isPopulatingForm = true;
                    dgvConnectorList.ClearSelection();
                    dgvConnectorList.Rows[i].Selected = true;
                    _isPopulatingForm = false;

                    var found = _allConnectors.FirstOrDefault(c => c.Id == id);
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
