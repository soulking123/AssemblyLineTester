using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AssemblyLineTester.Helpers;
using AssemblyLineTester.Models;
using AssemblyLineTester.Services;
using AssemblyLineTester.Views.Modals;

namespace AssemblyLineTester.Views.Pages.ProductDatabaseSubPages
{
    public partial class ProductModelSubPage : UserControl
    {
        private List<ProductModel> _allProducts = new();
        private List<ProductModel> _filteredProducts = new();
        private ProductModel? _selectedProduct = null;

        // Harness assemblies attached to the active product model in the form
        private List<HarnessAssembly> _attachedHarnesses = new();

        // Image tracking for the active product model in the form (preserves exact order)
        private List<string> _orderedImagePaths = new();
        private List<bool> _orderedReportInclusions = new();
        private int _currentImageIndex = -1;
        private bool _isUpdatingCheckbox = false;

        private bool _isPopulatingForm = false;

        public ProductModelSubPage()
        {
            InitializeComponent();
            ApplyCustomStyles();
            SetupEvents();

            DoubleBuffered = true;
            UIStyleHelper.EnableDoubleBuffering(dgvProductModelList);
            UIStyleHelper.EnableDoubleBuffering(dgvAttachedHarness);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                LoadProductData();
            }
        }

        private void ApplyCustomStyles()
        {
            UIStyleHelper.StyleDataGridView(dgvProductModelList);
            UIStyleHelper.StyleDataGridView(dgvAttachedHarness);

            float scale1 = UIStyleHelper.GetHostScaleFactor(dgvProductModelList);
            colNo.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colNo.Width = (int)Math.Round(55 * scale1);
            colTotalHarnessAssembly.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colTotalHarnessAssembly.Width = (int)Math.Round(140 * scale1);

            float scale2 = UIStyleHelper.GetHostScaleFactor(dgvAttachedHarness);
            colAttNo.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colAttNo.Width = (int)Math.Round(55 * scale2);
            colAttTotalConnector.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colAttTotalConnector.Width = (int)Math.Round(130 * scale2);

            UIStyleHelper.SetupButtonRoundedEdges(btnNewProduct, 8,
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
            btnNewProduct.Click += (s, e) => ClearForm();
            txtSearch.TextChanged += (s, e) => ApplyFilter();
            dgvProductModelList.SelectionChanged += DgvProductModelList_SelectionChanged;

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

        public void LoadProductData()
        {
            _allProducts = ProductModelService.GetAllProductModels();
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            string term = txtSearch.Text.Trim().ToLowerInvariant();

            _filteredProducts = string.IsNullOrEmpty(term)
                ? _allProducts.ToList()
                : _allProducts.Where(p =>
                    (p.Name != null && p.Name.ToLowerInvariant().Contains(term)) ||
                    (p.PartNumber != null && p.PartNumber.ToLowerInvariant().Contains(term))
                ).ToList();

            _isPopulatingForm = true;
            dgvProductModelList.Rows.Clear();

            for (int i = 0; i < _filteredProducts.Count; i++)
            {
                var p = _filteredProducts[i];
                int rowIndex = dgvProductModelList.Rows.Add(
                    i + 1,
                    p.Name,
                    p.PartNumber,
                    p.HarnessAssemblies?.Count ?? 0
                );
                dgvProductModelList.Rows[rowIndex].Tag = p.Id;
            }
            _isPopulatingForm = false;

            if (_filteredProducts.Count > 0)
            {
                SelectRowByProductId(_selectedProduct?.Id ?? _filteredProducts[0].Id);
            }
            else
            {
                ClearForm();
            }
        }

        private void DgvProductModelList_SelectionChanged(object? sender, EventArgs e)
        {
            if (_isPopulatingForm) return;

            if (dgvProductModelList.SelectedRows.Count > 0)
            {
                var row = dgvProductModelList.SelectedRows[0];
                if (row.Tag is int productId)
                {
                    var found = _allProducts.FirstOrDefault(p => p.Id == productId);
                    if (found != null)
                    {
                        PopulateForm(found);
                    }
                }
            }
        }

        private void PopulateForm(ProductModel product)
        {
            _selectedProduct = product;

            txtName.Text = product.Name;
            txtPartNumber.Text = product.PartNumber;
            txtDescription.Text = product.Description;

            // Load attached harness assemblies
            _attachedHarnesses = product.HarnessAssemblies != null
                ? new List<HarnessAssembly>(product.HarnessAssemblies)
                : new List<HarnessAssembly>();
            RefreshAttachedHarnessGrid();

            // Load attached images (preserving stored order)
            _orderedImagePaths = ProductModelService.GetProductModelImagePaths(product);
            _orderedReportInclusions = product.IncludedForReport != null
                ? new List<bool>(product.IncludedForReport)
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
            _selectedProduct = null;

            txtName.Clear();
            txtPartNumber.Clear();
            txtDescription.Clear();

            _attachedHarnesses.Clear();
            RefreshAttachedHarnessGrid();

            _orderedImagePaths.Clear();
            _orderedReportInclusions.Clear();
            _currentImageIndex = -1;

            UpdateImageDisplay();
            btnDelete.Enabled = false;

            if (dgvProductModelList.SelectedRows.Count > 0)
            {
                _isPopulatingForm = true;
                dgvProductModelList.ClearSelection();
                _isPopulatingForm = false;
            }

            txtName.Focus();
        }

        private void RefreshAttachedHarnessGrid()
        {
            dgvAttachedHarness.Rows.Clear();
            for (int i = 0; i < _attachedHarnesses.Count; i++)
            {
                var h = _attachedHarnesses[i];
                int rowIndex = dgvAttachedHarness.Rows.Add(
                    i + 1,
                    h.Name,
                    h.PartNumber,
                    h.Connectors?.Count ?? 0
                );
                dgvAttachedHarness.Rows[rowIndex].Tag = h;
            }
        }

        private void BtnAttach_Click(object? sender, EventArgs e)
        {
            var alreadyAttachedIds = _attachedHarnesses.Select(h => h.Id).ToList();
            using var modal = new SelectHarnessModalForm(alreadyAttachedIds);
            if (modal.ShowDialog(this) == DialogResult.OK && modal.SelectedHarnesses.Count > 0)
            {
                foreach (var h in modal.SelectedHarnesses)
                {
                    if (!_attachedHarnesses.Any(existing => existing.Id == h.Id))
                    {
                        _attachedHarnesses.Add(h);
                    }
                }
                RefreshAttachedHarnessGrid();
            }
        }

        private void BtnRemove_Click(object? sender, EventArgs e)
        {
            if (dgvAttachedHarness.SelectedRows.Count == 0) return;

            var row = dgvAttachedHarness.SelectedRows[0];
            if (row.Tag is HarnessAssembly h)
            {
                _attachedHarnesses.RemoveAll(item => item.Id == h.Id);
                RefreshAttachedHarnessGrid();
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
                ? $"PRODUCT: {txtPartNumber.Text.Trim()}"
                : "PRODUCT PREVIEW";

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
                Title = "Select Product Model Images"
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
                MessageBox.Show("Please enter a Product Model Name or Part Number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            var harnessIds = _attachedHarnesses.Select(h => h.Id).ToList();

            if (_selectedProduct == null)
            {
                // Create
                var newProduct = new ProductModel
                {
                    Name = name,
                    PartNumber = partNumber,
                    Description = txtDescription.Text.Trim()
                };

                var (created, error) = ProductModelService.AddProductModel(newProduct, harnessIds, _orderedImagePaths, _orderedReportInclusions);
                if (created == null)
                {
                    MessageBox.Show(error ?? "Failed to create product model.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show($"Product Model '{created.PartNumber}' created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadProductData();
                SelectRowByProductId(created.Id);
            }
            else
            {
                // Update
                _selectedProduct.Name = name;
                _selectedProduct.PartNumber = partNumber;
                _selectedProduct.Description = txtDescription.Text.Trim();

                var (success, error) = ProductModelService.UpdateProductModelOrdered(_selectedProduct, harnessIds, _orderedImagePaths, _orderedReportInclusions);
                if (!success)
                {
                    MessageBox.Show(error ?? "Failed to update product model.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show($"Product Model '{_selectedProduct.PartNumber}' updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                int savedId = _selectedProduct.Id;
                LoadProductData();
                SelectRowByProductId(savedId);
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (_selectedProduct == null) return;

            var confirm = MessageBox.Show(
                $"Are you sure you want to delete product model '{_selectedProduct.Name}'?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            var (success, error) = ProductModelService.DeleteProductModel(_selectedProduct.Id);
            if (!success)
            {
                MessageBox.Show(error ?? "Failed to delete product model.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Product Model deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearForm();
            LoadProductData();
        }

        private void SelectRowByProductId(int id)
        {
            for (int i = 0; i < dgvProductModelList.Rows.Count; i++)
            {
                if (dgvProductModelList.Rows[i].Tag is int productId && productId == id)
                {
                    _isPopulatingForm = true;
                    dgvProductModelList.ClearSelection();
                    dgvProductModelList.Rows[i].Selected = true;
                    _isPopulatingForm = false;

                    var found = _allProducts.FirstOrDefault(p => p.Id == id);
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
