using LaptopShop.Helpers;
using LaptopShop.Model;
using LaptopShop.Services;
using System.Windows;
using System.Windows.Input;

namespace LaptopShop.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly LaptopShopDbContext _context;
        private string _welcomeMessage = string.Empty;

        public MainViewModel(LaptopShopDbContext context)
        {
            _context = context;
            
            var currentUser = SessionManager.Instance.CurrentUser;
            WelcomeMessage = $"Welcome, {currentUser?.FullName} ({currentUser?.Role})";

            // Initialize commands
            ManageLaptopsCommand = new RelayCommand(ExecuteManageLaptops, CanManageLaptops);
            ManageOrdersCommand = new RelayCommand(ExecuteManageOrders, CanManageOrders);
            ManageCustomersCommand = new RelayCommand(ExecuteManageCustomers, CanManageCustomers);
            ManageCategoriesCommand = new RelayCommand(ExecuteManageCategories, CanManageCategories);
            ManageSuppliersCommand = new RelayCommand(ExecuteManageSuppliers, CanManageSuppliers);
            ManageUsersCommand = new RelayCommand(ExecuteManageUsers, CanManageUsers);
            LogoutCommand = new RelayCommand(ExecuteLogout);
        }

        public string WelcomeMessage
        {
            get => _welcomeMessage;
            set => SetProperty(ref _welcomeMessage, value);
        }

        // Commands
        public ICommand ManageLaptopsCommand { get; }
        public ICommand ManageOrdersCommand { get; }
        public ICommand ManageCustomersCommand { get; }
        public ICommand ManageCategoriesCommand { get; }
        public ICommand ManageSuppliersCommand { get; }
        public ICommand ManageUsersCommand { get; }
        public ICommand LogoutCommand { get; }

        // Role-based visibility properties
        public bool CanAccessLaptops => SessionManager.Instance.HasAnyRole("Admin", "Manager", "Warehouse", "Sales");
        public bool CanAccessOrders => SessionManager.Instance.HasAnyRole("Admin", "Manager", "Sales");
        public bool CanAccessCustomers => SessionManager.Instance.HasAnyRole("Admin", "Sales");
        public bool CanAccessCategories => SessionManager.Instance.HasAnyRole("Admin", "Manager");
        public bool CanAccessSuppliers => SessionManager.Instance.HasAnyRole("Admin", "Manager", "Warehouse");
        public bool CanAccessUsers => SessionManager.Instance.HasRole("Admin");

        private bool CanManageLaptops(object? parameter) => CanAccessLaptops;
        private bool CanManageOrders(object? parameter) => CanAccessOrders;
        private bool CanManageCustomers(object? parameter) => CanAccessCustomers;
        private bool CanManageCategories(object? parameter) => CanAccessCategories;
        private bool CanManageSuppliers(object? parameter) => CanAccessSuppliers;
        private bool CanManageUsers(object? parameter) => CanAccessUsers;

        private void ExecuteManageLaptops(object? parameter)
        {
            var window = new Views.LaptopManagementWindow();
            window.ShowDialog();
        }

        private void ExecuteManageOrders(object? parameter)
        {
            var window = new Views.OrderManagementWindow();
            window.ShowDialog();
        }

        private void ExecuteManageCustomers(object? parameter)
        {
            var window = new Views.CustomerManagementWindow();
            window.ShowDialog();
        }

        private void ExecuteManageCategories(object? parameter)
        {
            var window = new Views.CategoryManagementWindow();
            window.ShowDialog();
        }

        private void ExecuteManageSuppliers(object? parameter)
        {
            var window = new Views.SupplierManagementWindow();
            window.ShowDialog();
        }

        private void ExecuteManageUsers(object? parameter)
        {
            var window = new Views.UserManagementWindow();
            window.ShowDialog();
        }

        private void ExecuteLogout(object? parameter)
        {
            SessionManager.Instance.Logout();
            
            var loginWindow = new Views.LoginWindow();
            loginWindow.Show();
            
            // Close all windows except the new login window
            foreach (Window window in Application.Current.Windows)
            {
                if (window != loginWindow)
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}
