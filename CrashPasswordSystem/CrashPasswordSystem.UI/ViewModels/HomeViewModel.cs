using CrashPasswordSystem.UI.Command;
using CrashPasswordSystem.UI.Data;
using CrashPasswordSystem.UI.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Prism.Events;
using CrashPasswordSystem.Models;
using CrashPasswordSystem.Data;
using System;
using CrashPasswordSystem.Services;

namespace CrashPasswordSystem.UI.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private IProductDataService _ProductDataService;
        private ISupplierDataService _SupplierDataService;
        private ICategoryDataService _CategoryDataService;
        private ICompanyDataService _CompanyDataService;

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

        private List<string> _Companies;

        public List<string> Companies
        {
            get { return _Companies; }
            set { _Companies = value; OnPropertyChanged(); }
        }
        private List<string> _Categories;

        public List<string> Categories
        {
            get { return _Categories; }
            set { _Categories = value; OnPropertyChanged(); }
        }
        private List<string> _Suppliers;

        public List<string> Suppliers
        {
            get { return _Suppliers; }
            set { _Suppliers = value; OnPropertyChanged(); }
        }

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
            set { _SelectedSupplier = value; OnPropertyChanged(); if (!string.IsNullOrWhiteSpace(value)) FilterData("SelectedSupplier", value); }
        }

        private string _SearchBox;

        public string SearchBox
        {
            get { return _SearchBox; }
            set
            {
                _SearchBox = value;
                OnPropertyChanged();
                if (value != null)
                {
                    FilterData("SearchBox", value);
                }
            }
        }

        private bool _isVisible;
        public bool IsVisible
        {
            get => _isVisible;
            set => SetValue(ref _isVisible, value);
        }

        #endregion

        public HomeViewModel(IEventAggregator iEventAggregator, IProductDataService productDataService
            , ISupplierDataService supplierDataService
            , ICategoryDataService categoryDataService
        , ICompanyDataService companyDataService)
        {
            _ProductDataService = productDataService;
            _SupplierDataService = supplierDataService;
            _CategoryDataService = categoryDataService;
            _CompanyDataService = companyDataService;

            ClearFiltersCommand = new RelayCommand(ClearFilters);
            OpenDetailsCommand = new RelayCommand(OpenDetails);
            OpenAddProductCommand = new RelayCommand(OpenNewProduct);

            LoadFilters();
            LoadData();
        }
        
        #region Load Data
        public async void LoadData()
        {
            var p = await _ProductDataService.GetAll();
            Products = new ObservableCollection<Product>(p);
        }
        #endregion

        #region Load Filters Options
        public async void LoadFilters()
        {
            Companies = await _CompanyDataService.GetAllDesctiption();
            Categories = await _CategoryDataService.GetAllDesctiption();
            Suppliers = await _SupplierDataService.GetAllDesctiption();
        }

        #endregion

        #region Filter Data
        public void FilterData(string filter, string value)
        {
            using (var dBContext = new DataContext())
            {
                if (filter == "SelectedCompany")
                {
                    var id = dBContext.CrashCompanies.Where(c => c.CCName == value).Select(c => c.CCID).FirstOrDefault();
                    var p = dBContext.Products.Where(s => s.CCID == id).ToList();
                    Products = new ObservableCollection<Product>(p);
                    SelectedSupplier = null;
                    SelectedCategory = null;
                    SearchBox = null;
                }
                else if (filter == "SelectedCategory")
                {
                    var id = dBContext.ProductCategories.Where(c => c.PCName == value).Select(c => c.PCID).FirstOrDefault();
                    var p = dBContext.Products.Where(s => s.PCID == id).ToList();
                    Products = new ObservableCollection<Product>(p);
                    SelectedCompany = null;
                    SelectedSupplier = null;
                    SearchBox = null;
                }
                else if (filter == "SelectedSupplier")
                {
                    var id = dBContext.Suppliers.Where(c => c.SupplierName == value).Select(c => c.SupplierID).FirstOrDefault();
                    var p = dBContext.Products.Where(s => s.SupplierID == id).ToList();
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
