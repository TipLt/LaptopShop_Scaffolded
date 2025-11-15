using LaptopShop.Services;
using System.Windows;

namespace LaptopShop.Views
{
    public partial class OrderManagementWindow : Window
    {
        public OrderManagementWindow()
        {
            InitializeComponent();
            var factory = new ViewModelFactory();
            DataContext = factory.CreateOrderManagementViewModel();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
