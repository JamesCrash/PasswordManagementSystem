using CrashPasswordSystem.Data;
using CrashPasswordSystem.UI.Command;
using CrashPasswordSystem.UI.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CrashPasswordSystem.UI.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        #region Props
        private ObservableCollection<Product> _products;

        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set { _products = value; OnPropertyChanged(); }
        }
        private Product _SelectedItem;

        public Product SelectedItem
        {
            get { return _SelectedItem; }
            set { _SelectedItem = value; OnPropertyChanged(); }
        }


        public ICommand ClearFiltersCommand { get; set; }
        public ICommand OpenDetailsCommand { get; set; }
        public ICommand OpenAddProductCommand { get; set; }

        public List<string> Companies { get; set; }
        public List<string> Categories { get; set; }
        public List<string> Suppliers { get; set; }

        private string _SelectedCompany;

        public string SelectedCompany
        {
            get { return _SelectedCompany; }
            set { _SelectedCompany = value; OnPropertyChanged(); if (!string.IsNullOrWhiteSpace(value)) FilterData("SelectedCompany", value); }
        }

        private string _SelectedCategory;

        public string SelectedCategory
        {
            get { return _SelectedCategory; }
            set { _SelectedCategory = value; OnPropertyChanged(); if (!string.IsNullOrWhiteSpace(value)) FilterData("SelectedCategory", value); }
        }

        private string _SelectedSupplier;

        public string SelectedSupplier
        {
            get { return _SelectedSupplier; }
            set { _SelectedSupplier = value; OnPropertyChanged(); if(!string.IsNullOrWhiteSpace(value))FilterData("SelectedSupplier", value); }
        }

        private string _SearchBox;

        public string SearchBox
        {
            get { return _SearchBox; }
            set { _SearchBox = value;
                OnPropertyChanged();
                if(value != null)
                {
                    FilterData("SearchBox", value);
                }
            }
        }
        #endregion

        public HomeViewModel()
        {
            ClearFiltersCommand = new RelayCommand(ClearFilters);
            OpenDetailsCommand = new RelayCommand(OpenDetails);
            OpenAddProductCommand = new RelayCommand(OpenNewProduct);
            LoadFilters();
            LoadData();
        }

        #region Load Data
        public async void LoadData()
        {
            using (var dBContext = new ITDatabaseContext())
            {
                var p = dBContext.Products.ToList();
                Products = new ObservableCollection<Product>(p);
            }
        }
        #endregion

        #region Load Filters Options
        public async void LoadFilters()
        {
            using (var dBContext = new ITDatabaseContext())
            {
                Companies = dBContext.CrashCompanies.Select(s => s.CcName).ToList();
                Categories = dBContext.ProductCategories.Select(s => s.PcName).ToList();
                Suppliers = dBContext.Suppliers.Select(s => s.SupplierName).ToList();
            }
        }
        #endregion

        #region Filter Data
        public async void FilterData(string filter, string value)
        {
            using (var dBContext = new ITDatabaseContext())
            {
                if (filter == "SelectedCompany")
                {
                    var id = dBContext.CrashCompanies.Where(c => c.CcName == value).Select(c => c.Ccid).FirstOrDefault();
                    var p = dBContext.Products.Where(s => s.Ccid == id).ToList();
                    Products = new ObservableCollection<Product>(p);
                    SelectedSupplier = null;
                    SelectedCategory = null;
                    SearchBox = null;
                }
                else if (filter == "SelectedCategory")
                {
                    var id = dBContext.ProductCategories.Where(c => c.PcName == value).Select(c => c.Pcid).FirstOrDefault();
                    var p = dBContext.Products.Where(s => s.Pcid == id).ToList();
                    Products = new ObservableCollection<Product>(p);
                    SelectedCompany = null;
                    SelectedSupplier = null;
                    SearchBox = null;
                }
                else if (filter == "SelectedSupplier")
                {
                    var id = dBContext.Suppliers.Where(c => c.SupplierName == value).Select(c => c.SupplierId).FirstOrDefault();
                    var p = dBContext.Products.Where(s => s.SupplierId == id).ToList();
                    Products = new ObservableCollection<Product>(p);
                    SelectedCompany = null;
                    SelectedCategory = null;
                    SearchBox = null;
                }
                else if (filter == "SearchBox")
                {
                    var p = dBContext.Products.Where(s => s.ProductDescription.Contains(value)).ToList();
                    Products = new ObservableCollection<Product>(p);
                    SelectedCompany = null;
                    SelectedCategory = null;
                    SelectedSupplier = null;
                }
            }

        }
        #endregion

        #region Clear Filters
        public async void ClearFilters(object parameter)
        {
            SelectedCompany = null;
            SelectedCategory = null;
            SelectedSupplier = null;
            SearchBox = null;
            LoadData();
        }
        #endregion

        #region Open Details
        public async void OpenDetails(object parameter)
        {
            var vm = new ProductDetailsViewModel(SelectedItem);
            var productDetails = new ProductDetails
            {
                DataContext = vm
            };
            vm.OnRequestClose += (s, e) => productDetails.Close();
            productDetails.ShowDialog();
            LoadData();
        }
        #endregion

        #region Open Add New product
        public async void OpenNewProduct(object parameter)
        {
            var vm = new AddProductViewModel();
            var addProduct = new AddProduct
            {
                DataContext = vm
            };
            vm.OnRequestClose += (s, e) => addProduct.Close();
            addProduct.ShowDialog();
            LoadData();

        }
        #endregion
    }
}
