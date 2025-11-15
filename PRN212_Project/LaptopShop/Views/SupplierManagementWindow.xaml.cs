using LaptopShop.Services;
using System.Windows;

namespace LaptopShop.Views
{
    public partial class SupplierManagementWindow : Window
    {
        public SupplierManagementWindow()
        {
            InitializeComponent();
            var factory = new ViewModelFactory();
            DataContext = factory.CreateSupplierManagementViewModel();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
