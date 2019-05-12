using CrashPasswordSystem.Core;
using CrashPasswordSystem.Data;
using CrashPasswordSystem.Models;
using CrashPasswordSystem.Services;
using CrashPasswordSystem.UI.Command;
using CrashPasswordSystem.UI.Event;
using CrashPasswordSystem.UI.Views;
using CrashPasswordSystem.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

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
        public DelegateCommand<object> OpenDetailsCommand { get; set; }
        public ICommand OpenAddProductCommand { get; set; }

        private List<string> _Companies;
        public List<string> Companies
        {
            get { return _Companies; }
            set => base.SetProperty(ref _Companies, value);
        }

        private NotifyDataErrorInfoBase _selectedDetailViewModel;
        public NotifyDataErrorInfoBase SelectedDetail
        {
            get => _selectedDetailViewModel;
            set => base.SetProperty(ref _selectedDetailViewModel, value);
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
            OpenDetailsCommand = new DelegateCommand<object>(OpenDetails);
            OpenAddProductCommand = new RelayCommand(OpenNewProduct);

            LoadFilters();
            LoadDataAsync();

            EventAggregator.GetEvent<SaveEvent<Product>>()
                           .Subscribe(OnSave, keepSubscriberReferenceAlive: true);
        }

        #region Methods

        private void OnEdit()
        {
            OpenNewProduct(null);
        }

        public void NotifySave(Product instance)
        {
            if (!Products.Contains(instance) && !Products.Any(p => p.ProductID == instance.ProductID))
            {
                Products.Add(instance);
            }
        }

        public async void LoadDataAsync()
        {
            var p = await _ProductDataService.GetAllAsync();
            Products = new ObservableCollection<Product>(p);
        }

        private void OnSave(Product instance)
        {
            CollectionViewSource.GetDefaultView(Products)?.Refresh();
        }

        #endregion

        #region Filter Data
        public async void LoadFilters()
        {
            Companies = await _CompanyDataService.GetAllDesctiption();
            Categories = await _CategoryDataService.GetAllDesctiption();
            Suppliers = await _SupplierDataService.GetAllDesctiption();
        }
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
        public void OpenDetails(object parameter)
        {
            var product = parameter as Product;
            if (product == null)
            {
                return;
            }
            SelectedDetail = DependencyContainer.Resolve<ProductDetailsViewModel>();

            EventAggregator.GetEvent<EditEvent>().Publish(parameter);
        }
        #endregion

        #region Open Add New product

        public void OpenNewProduct(object parameter)
        {
            SelectedDetail = DependencyContainer.Resolve<AddProductViewModel>();

            EventAggregator.GetEvent<EditEvent>().Publish(typeof(Product));
        }
        #endregion
    }
}
