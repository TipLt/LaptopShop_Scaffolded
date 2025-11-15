using LaptopShop.Services;
using LaptopShop.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        private void InputField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (_viewModel.LoginCommand.CanExecute(null))
                {
                    _viewModel.LoginCommand.Execute(null);
                }
            }
        }
    }
}
