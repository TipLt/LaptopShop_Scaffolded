using LaptopShop.Helpers;
using LaptopShop.Model;
using LaptopShop.Repositories;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LaptopShop.ViewModels
{
    public class SupplierManagementViewModel : BaseViewModel
    {
        private readonly Repository<Supplier> _supplierRepository;
        private ObservableCollection<Supplier> _suppliers;

        public SupplierManagementViewModel(LaptopShopDbContext context)
        {
            _supplierRepository = new Repository<Supplier>(context);
            _suppliers = new ObservableCollection<Supplier>();
            LoadSuppliers();
            RefreshCommand = new RelayCommand(ExecuteRefresh);
        }

        public ObservableCollection<Supplier> Suppliers
        {
            get => _suppliers;
            set => SetProperty(ref _suppliers, value);
        }

        public ICommand RefreshCommand { get; }

        private void LoadSuppliers()
        {
            var suppliers = _supplierRepository.GetAll();
            Suppliers.Clear();
            foreach (var supplier in suppliers)
            {
                Suppliers.Add(supplier);
            }
        }

        private void ExecuteRefresh(object? parameter)
        {
            LoadSuppliers();
        }
    }
}
