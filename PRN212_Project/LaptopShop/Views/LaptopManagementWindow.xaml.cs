using LaptopShop.Services;
using LaptopShop.ViewModels;
using System.Windows;

namespace LaptopShop.Views
{
    public partial class LaptopManagementWindow : Window
    {
        public LaptopManagementWindow()
        {
            InitializeComponent();
            var factory = new ViewModelFactory();
            DataContext = factory.CreateLaptopManagementViewModel();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
