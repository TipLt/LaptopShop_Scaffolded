using LaptopShop.Helpers;
using LaptopShop.Model;
using LaptopShop.Repositories;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LaptopShop.ViewModels
{
    public class OrderManagementViewModel : BaseViewModel
    {
        private readonly OrderRepository _orderRepository;
        private ObservableCollection<Order> _orders;
        private Order? _selectedOrder;

        public OrderManagementViewModel(LaptopShopDbContext context)
        {
            _orderRepository = new OrderRepository(context);
            _orders = new ObservableCollection<Order>();

            LoadOrders();

            RefreshCommand = new RelayCommand(ExecuteRefresh);
        }

        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set => SetProperty(ref _orders, value);
        }

        public Order? SelectedOrder
        {
            get => _selectedOrder;
            set => SetProperty(ref _selectedOrder, value);
        }

        public ICommand RefreshCommand { get; }

        private void LoadOrders()
        {
            var orders = _orderRepository.GetAllWithDetails();
            Orders.Clear();
            foreach (var order in orders)
            {
                Orders.Add(order);
            }
        }

        private void ExecuteRefresh(object? parameter)
        {
            LoadOrders();
        }
    }
}
