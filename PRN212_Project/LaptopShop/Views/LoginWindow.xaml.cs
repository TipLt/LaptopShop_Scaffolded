using LaptopShop.Services;
using LaptopShop.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace LaptopShop.Views
{
    public partial class LoginWindow : Window
    {
        private readonly LoginViewModel _viewModel;

        public LoginWindow()
        {
            InitializeComponent();
            var factory = new ViewModelFactory();
            _viewModel = factory.CreateLoginViewModel();
            DataContext = _viewModel;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                _viewModel.Password = passwordBox.Password;
            }
        }
    }
}
