using LaptopShop.Services;
using System.Windows;

namespace LaptopShop.Views
{
    public partial class CategoryManagementWindow : Window
    {
        public CategoryManagementWindow()
        {
            InitializeComponent();
            var factory = new ViewModelFactory();
            DataContext = factory.CreateCategoryManagementViewModel();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
