using LaptopShop.Services;
using System.Windows;

namespace LaptopShop.Views
{
    public partial class UserManagementWindow : Window
    {
        public UserManagementWindow()
        {
            InitializeComponent();
            var factory = new ViewModelFactory();
            DataContext = factory.CreateUserManagementViewModel();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
