using LaptopShop.Helpers;
using LaptopShop.Model;
using LaptopShop.Repositories;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace LaptopShop.ViewModels
{
    public class CustomerManagementViewModel : BaseViewModel
    {
        private readonly Repository<Customer> _customerRepository;
        private ObservableCollection<Customer> _customers;
        private Customer? _selectedCustomer;

        public CustomerManagementViewModel(LaptopShopDbContext context)
        {
            _customerRepository = new Repository<Customer>(context);
            _customers = new ObservableCollection<Customer>();

            LoadCustomers();

            AddCommand = new RelayCommand(ExecuteAdd);
            DeleteCommand = new RelayCommand(ExecuteDelete, CanExecuteDelete);
            RefreshCommand = new RelayCommand(ExecuteRefresh);
        }

        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set => SetProperty(ref _customers, value);
        }

        public Customer? SelectedCustomer
        {
            get => _selectedCustomer;
            set => SetProperty(ref _selectedCustomer, value);
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }

        private void LoadCustomers()
        {
            var customers = _customerRepository.GetAll();
            Customers.Clear();
            foreach (var customer in customers)
            {
                Customers.Add(customer);
            }
        }

        private void ExecuteAdd(object? parameter)
        {
            var newCustomer = new Customer
            {
                CustomerName = "New Customer",
                Email = "email@example.com",
                Phone = "0000000000",
                Address = "Address"
            };

            var editWindow = new Views.CustomerEditWindow(newCustomer, _customerRepository);
            if (editWindow.ShowDialog() == true)
            {
                LoadCustomers();
            }
        }

        private bool CanExecuteDelete(object? parameter)
        {
            return SelectedCustomer != null;
        }

        private void ExecuteDelete(object? parameter)
        {
            if (SelectedCustomer != null)
            {
                var result = MessageBox.Show(
                    $"Are you sure you want to delete {SelectedCustomer.CustomerName}?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _customerRepository.Delete(SelectedCustomer);
                    _customerRepository.SaveChanges();
                    LoadCustomers();
                }
            }
        }

        private void ExecuteRefresh(object? parameter)
        {
            LoadCustomers();
        }
    }
}
