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
using CrashPasswordSystem.UI.Event;

namespace CrashPasswordSystem.UI.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly Func<DataContext> _contextCreator;
        private IProductDataService _ProductDataService;
        private ISupplierDataService _SupplierDataService;
        private ICategoryDataService _CategoryDataService;
        private ICompanyDataService _CompanyDataService;

        #region Props

        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set => base.SetProperty(ref _products, value);
        }

        private Product _SelectedItem;
        public Product SelectedItem
        {
            get { return _SelectedItem; }
            set => base.SetProperty(ref _SelectedItem, value);
        }

        public ICommand ClearFiltersCommand { get; set; }
        public ICommand OpenDetailsCommand { get; set; }
        public ICommand OpenAddProductCommand { get; set; }

        private List<string> _Companies;
        public List<string> Companies
        {
            get { return _Companies; }
            set => base.SetProperty(ref _Companies, value);
        }

        public void NotifySave(Product instance)
        {
            if (!Products.Contains(instance) && !Products.Any(p => p.ProductID == instance.ProductID))
            {
                Products.Add(instance);
            }
        }

        private List<string> _Categories;
        public List<string> Categories
        {
            get { return _Categories; }
            set => base.SetProperty(ref _Categories, value);
        }

        private List<string> _Suppliers;
        public List<string> Suppliers
        {
            get { return _Suppliers; }
            set => base.SetProperty(ref _Suppliers, value);
        }

        private string _SelectedCompany;
        public string SelectedCompany
        {
            get { return _SelectedCompany; }
            set
            {
                base.SetProperty(ref _SelectedCompany, value);

                if (!string.IsNullOrWhiteSpace(value))
                    FilterData("SelectedCompany", value);
            }
        }

        private string _SelectedCategory;

        public string SelectedCategory
        {
            get { return _SelectedCategory; }
            set {
                base.SetProperty(ref _SelectedCategory, value);

                if (!string.IsNullOrWhiteSpace(value))
                    FilterData("SelectedCategory", value); }
        }

        private string _SelectedSupplier;

        public string SelectedSupplier
        {
            get { return _SelectedSupplier; }
            set {
                base.SetProperty(ref _SelectedSupplier, value);

                if (!string.IsNullOrWhiteSpace(value))
                    FilterData("SelectedSupplier", value); }
        }

        private string _SearchBox;
        public string SearchBox
        {
            get { return _SearchBox; }
            set
            {
                base.SetProperty(ref _SearchBox, value);

                if (value != null)
                {
                    FilterData("SearchBox", value);
                }
            }
        }

        public override bool IsVisible => true;

        public IDependencyContainer DependencyContainer { get; private set; }

        #endregion

        public HomeViewModel(IDependencyContainer container)
        {
            DependencyContainer = container;

            EventAggregator = container.Resolve<IEventAggregator>();
            _contextCreator = () => container.Resolve<DataContext>();

            _ProductDataService = container.Resolve<IProductDataService>();
            _SupplierDataService = container.Resolve<ISupplierDataService>();
            _CategoryDataService = container.Resolve<ICategoryDataService>();
            _CompanyDataService = container.Resolve<ICompanyDataService>();

            ClearFiltersCommand = new RelayCommand(ClearFilters);
            OpenDetailsCommand = new RelayCommand(OpenDetails);
            OpenAddProductCommand = new RelayCommand(OpenNewProduct);

            LoadFilters();
            LoadDataAsync();
        }

        private void OnEdit()
        {
            OpenNewProduct(null);
        }

        #region Load Data
        public async void LoadDataAsync()
        {
            var p = await _ProductDataService.GetAllAsync();
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
            using (var dBContext = _contextCreator())
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
            LoadDataAsync();
        }
        #endregion

        #region Open Details
        public async void OpenDetails(object parameter)
        {
            var vm = new ProductDetailsViewModel(SelectedItem, DependencyContainer);
            var productDetails = new ProductDetails
            {
                DataContext = vm
            };
            vm.OnRequestClose += (s, e) => productDetails.Close();
            productDetails.ShowDialog();
            LoadDataAsync();
        }
        #endregion

        #region Open Add New product

        private AddProductViewModel _productEdit;

        public AddProductViewModel ProductEdit
        {
            get => _productEdit;
            set => base.SetProperty(ref _productEdit, value);
        }

        public void OpenNewProduct(object parameter)
        {
            ProductEdit = DependencyContainer.Resolve<AddProductViewModel>();

            EventAggregator.GetEvent<EditEvent>().Publish(parameter);
        }
        #endregion
    }
}
