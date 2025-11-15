using LaptopShop.Helpers;
using LaptopShop.Model;
using LaptopShop.Repositories;
using LaptopShop.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace LaptopShop.ViewModels
{
    public class LaptopManagementViewModel : BaseViewModel
    {
        private readonly LaptopRepository _laptopRepository;
        private readonly Repository<Category> _categoryRepository;
        private ObservableCollection<Laptop> _laptops;
        private Laptop? _selectedLaptop;
        private bool _isReadOnly;

        public LaptopManagementViewModel(LaptopShopDbContext context)
        {
            _laptopRepository = new LaptopRepository(context);
            _categoryRepository = new Repository<Category>(context);
            _laptops = new ObservableCollection<Laptop>();

            // Determine read-only based on role
            var role = SessionManager.Instance.CurrentUser?.Role;
            _isReadOnly = role == "Sales";

            LoadLaptops();

            // Commands
            AddCommand = new RelayCommand(ExecuteAdd, CanExecuteModify);
            EditCommand = new RelayCommand(ExecuteEdit, CanExecuteEdit);
            DeleteCommand = new RelayCommand(ExecuteDelete, CanExecuteModify);
            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
            CancelCommand = new RelayCommand(ExecuteCancel);
            RefreshCommand = new RelayCommand(ExecuteRefresh);
        }

        public ObservableCollection<Laptop> Laptops
        {
            get => _laptops;
            set => SetProperty(ref _laptops, value);
        }

        public Laptop? SelectedLaptop
        {
            get => _selectedLaptop;
            set => SetProperty(ref _selectedLaptop, value);
        }

        public bool IsReadOnly
        {
            get => _isReadOnly;
            set => SetProperty(ref _isReadOnly, value);
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand RefreshCommand { get; }

        private void LoadLaptops()
        {
            var laptops = _laptopRepository.GetAllWithDetails();
            Laptops.Clear();
            foreach (var laptop in laptops)
            {
                Laptops.Add(laptop);
            }
        }

        private bool CanExecuteModify(object? parameter)
        {
            return !IsReadOnly;
        }

        private bool CanExecuteEdit(object? parameter)
        {
            return !IsReadOnly && SelectedLaptop != null;
        }

        private bool CanExecuteSave(object? parameter)
        {
            return !IsReadOnly;
        }

        private void ExecuteAdd(object? parameter)
        {
            var newLaptop = new Laptop
            {
                Brand = "New Brand",
                Model = "New Model",
                Price = 0,
                Stock = 0
            };

            var editWindow = new Views.LaptopEditWindow(newLaptop, _laptopRepository);
            if (editWindow.ShowDialog() == true)
            {
                LoadLaptops();
            }
        }

        private void ExecuteEdit(object? parameter)
        {
            if (SelectedLaptop != null)
            {
                var editWindow = new Views.LaptopEditWindow(SelectedLaptop, _laptopRepository);
                if (editWindow.ShowDialog() == true)
                {
                    LoadLaptops();
                }
            }
        }

        private void ExecuteDelete(object? parameter)
        {
            if (SelectedLaptop != null)
            {
                var result = MessageBox.Show(
                    $"Are you sure you want to delete {SelectedLaptop.Brand} {SelectedLaptop.Model}?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _laptopRepository.Delete(SelectedLaptop);
                    _laptopRepository.SaveChanges();
                    LoadLaptops();
                }
            }
        }

        private void ExecuteSave(object? parameter)
        {
            _laptopRepository.SaveChanges();
            MessageBox.Show("Changes saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ExecuteCancel(object? parameter)
        {
            LoadLaptops();
        }

        private void ExecuteRefresh(object? parameter)
        {
            LoadLaptops();
        }
    }
}
