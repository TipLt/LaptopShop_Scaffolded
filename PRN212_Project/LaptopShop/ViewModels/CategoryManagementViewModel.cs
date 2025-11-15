using LaptopShop.Helpers;
using LaptopShop.Model;
using LaptopShop.Repositories;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LaptopShop.ViewModels
{
    public class CategoryManagementViewModel : BaseViewModel
    {
        private readonly Repository<Category> _categoryRepository;
        private ObservableCollection<Category> _categories;

        public CategoryManagementViewModel(LaptopShopDbContext context)
        {
            _categoryRepository = new Repository<Category>(context);
            _categories = new ObservableCollection<Category>();
            LoadCategories();
            RefreshCommand = new RelayCommand(ExecuteRefresh);
        }

        public ObservableCollection<Category> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }

        public ICommand RefreshCommand { get; }

        private void LoadCategories()
        {
            var categories = _categoryRepository.GetAll();
            Categories.Clear();
            foreach (var category in categories)
            {
                Categories.Add(category);
            }
        }

        private void ExecuteRefresh(object? parameter)
        {
            LoadCategories();
        }
    }
}
