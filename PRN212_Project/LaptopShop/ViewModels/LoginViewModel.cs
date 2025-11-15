using LaptopShop.Helpers;
using LaptopShop.Model;
using LaptopShop.Repositories;
using LaptopShop.Services;
using System.Windows;
using System.Windows.Input;

namespace LaptopShop.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly UserRepository _userRepository;
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _errorMessage = string.Empty;

        public LoginViewModel(LaptopShopDbContext context)
        {
            _userRepository = new UserRepository(context);
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
        }

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public ICommand LoginCommand { get; }

        private bool CanExecuteLogin(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }

        private void ExecuteLogin(object? parameter)
        {
            ErrorMessage = string.Empty;

            var user = _userRepository.Authenticate(Username, Password);
            
            if (user != null)
            {
                SessionManager.Instance.Login(user);
                
                // Close login window and open main window
                var mainWindow = new Views.MainWindow();
                mainWindow.Show();
                
                // Close login window
                Application.Current.Windows[0]?.Close();
            }
            else
            {
                ErrorMessage = "Invalid username or password!";
            }
        }
    }
}
