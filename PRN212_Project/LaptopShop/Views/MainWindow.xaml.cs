using LaptopShop.Services;
using LaptopShop.ViewModels;
using System.Windows;

namespace LaptopShop.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var factory = new ViewModelFactory();
            DataContext = factory.CreateMainViewModel();
        }
    }
}