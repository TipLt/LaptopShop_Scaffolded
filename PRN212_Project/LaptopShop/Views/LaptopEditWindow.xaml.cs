using LaptopShop.Model;
using LaptopShop.Repositories;
using System;
using System.Windows;

namespace LaptopShop.Views
{
    public partial class LaptopEditWindow : Window
    {
        private readonly Laptop _laptop;
        private readonly LaptopRepository _repository;
        private readonly bool _isNewLaptop;

        public LaptopEditWindow(Laptop laptop, LaptopRepository repository)
        {
            InitializeComponent();
            _laptop = laptop;
            _repository = repository;
            _isNewLaptop = laptop.LaptopId == 0;

            HeaderText.Text = _isNewLaptop ? "Add New Laptop" : "Edit Laptop";

            LoadData();
        }

        private void LoadData()
        {
            BrandTextBox.Text = _laptop.Brand;
            ModelTextBox.Text = _laptop.Model;
            ProcessorTextBox.Text = _laptop.Processor;
            RamTextBox.Text = _laptop.Ram;
            StorageTextBox.Text = _laptop.Storage;
            GpuTextBox.Text = _laptop.Gpu;
            PriceTextBox.Text = _laptop.Price.ToString();
            StockTextBox.Text = _laptop.Stock?.ToString() ?? "0";
            DescriptionTextBox.Text = _laptop.Description;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate
                if (string.IsNullOrWhiteSpace(BrandTextBox.Text) || 
                    string.IsNullOrWhiteSpace(ModelTextBox.Text))
                {
                    MessageBox.Show("Brand and Model are required!", "Validation Error", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!decimal.TryParse(PriceTextBox.Text, out decimal price) || price < 0)
                {
                    MessageBox.Show("Please enter a valid price!", "Validation Error", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!int.TryParse(StockTextBox.Text, out int stock) || stock < 0)
                {
                    MessageBox.Show("Please enter a valid stock quantity!", "Validation Error", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Save data
                _laptop.Brand = BrandTextBox.Text.Trim();
                _laptop.Model = ModelTextBox.Text.Trim();
                _laptop.Processor = ProcessorTextBox.Text?.Trim();
                _laptop.Ram = RamTextBox.Text?.Trim();
                _laptop.Storage = StorageTextBox.Text?.Trim();
                _laptop.Gpu = GpuTextBox.Text?.Trim();
                _laptop.Price = price;
                _laptop.Stock = stock;
                _laptop.Description = DescriptionTextBox.Text?.Trim();

                if (_isNewLaptop)
                {
                    _repository.Add(_laptop);
                }
                else
                {
                    _repository.Update(_laptop);
                }

                _repository.SaveChanges();

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving laptop: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
