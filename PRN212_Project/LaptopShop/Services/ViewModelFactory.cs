using LaptopShop.Model;
using LaptopShop.ViewModels;
using System;

namespace LaptopShop.Services
{
    // Factory Pattern: Creates ViewModels
    public class ViewModelFactory
    {
        private readonly LaptopShopDbContext _context;

        public ViewModelFactory()
        {
            _context = new LaptopShopDbContext();
        }

        public LoginViewModel CreateLoginViewModel()
        {
            return new LoginViewModel(_context);
        }

        public MainViewModel CreateMainViewModel()
        {
            return new MainViewModel(_context);
        }

        public LaptopManagementViewModel CreateLaptopManagementViewModel()
        {
            return new LaptopManagementViewModel(_context);
        }

        public OrderManagementViewModel CreateOrderManagementViewModel()
        {
            return new OrderManagementViewModel(_context);
        }

        public CustomerManagementViewModel CreateCustomerManagementViewModel()
        {
            return new CustomerManagementViewModel(_context);
        }

        public CategoryManagementViewModel CreateCategoryManagementViewModel()
        {
            return new CategoryManagementViewModel(_context);
        }

        public SupplierManagementViewModel CreateSupplierManagementViewModel()
        {
            return new SupplierManagementViewModel(_context);
        }

        public UserManagementViewModel CreateUserManagementViewModel()
        {
            return new UserManagementViewModel(_context);
        }
    }
}
