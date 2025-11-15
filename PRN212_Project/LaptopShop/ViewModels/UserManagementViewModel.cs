using LaptopShop.Helpers;
using LaptopShop.Model;
using LaptopShop.Repositories;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LaptopShop.ViewModels
{
    public class UserManagementViewModel : BaseViewModel
    {
        private readonly UserRepository _userRepository;
        private ObservableCollection<User> _users;

        public UserManagementViewModel(LaptopShopDbContext context)
        {
            _userRepository = new UserRepository(context);
            _users = new ObservableCollection<User>();
            LoadUsers();
            RefreshCommand = new RelayCommand(ExecuteRefresh);
        }

        public ObservableCollection<User> Users
        {
            get => _users;
            set => SetProperty(ref _users, value);
        }

        public ICommand RefreshCommand { get; }

        private void LoadUsers()
        {
            var users = _userRepository.GetAll();
            Users.Clear();
            foreach (var user in users)
            {
                Users.Add(user);
            }
        }

        private void ExecuteRefresh(object? parameter)
        {
            LoadUsers();
        }
    }
}
