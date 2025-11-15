using LaptopShop.Model;
using LaptopShop.Repositories;
using System;
using System.Windows;

namespace LaptopShop.Views
{
    public partial class CustomerEditWindow : Window
    {
        private readonly Customer _customer;
        private readonly Repository<Customer> _repository;
        private readonly bool _isNew;

        public CustomerEditWindow(Customer customer, Repository<Customer> repository)
        {
            InitializeComponent();
            _customer = customer;
            _repository = repository;
            _isNew = customer.CustomerId == 0;

            HeaderText.Text = _isNew ? "Add New Customer" : "Edit Customer";
            LoadData();
        }

        private void LoadData()
        {
            NameTextBox.Text = _customer.CustomerName;
            EmailTextBox.Text = _customer.Email;
            PhoneTextBox.Text = _customer.Phone;
            AddressTextBox.Text = _customer.Address;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(NameTextBox.Text))
                {
                    MessageBox.Show("Customer name is required!", "Validation Error", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                _customer.CustomerName = NameTextBox.Text.Trim();
                _customer.Email = EmailTextBox.Text?.Trim();
                _customer.Phone = PhoneTextBox.Text?.Trim();
                _customer.Address = AddressTextBox.Text?.Trim();

                if (_isNew)
                {
                    _repository.Add(_customer);
                }
                else
                {
                    _repository.Update(_customer);
                }

                _repository.SaveChanges();
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving customer: {ex.Message}", "Error", 
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
