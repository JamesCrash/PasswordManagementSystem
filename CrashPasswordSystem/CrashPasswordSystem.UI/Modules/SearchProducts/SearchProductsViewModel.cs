using CrashPasswordSystem.Data;
using CrashPasswordSystem.Models;
using CrashPasswordSystem.Services;
using CrashPasswordSystem.UI.Command;
using CrashPasswordSystem.UI.Event;
using CrashPasswordSystem.UI.ViewModels;
using CrashPasswordSystem.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace CrashPasswordSystem.UI.Search.SearchProducts
{
    /// <summary>
    /// Represents the Product search module VM. 
    /// 
    /// It contains all the logic under the hood for the search actions and reflects its state back to the UI 
    /// through Bindings, Commands and Event(Aggregator)-s
    /// </summary>
    public class SearchProductsViewModel : ListViewModel<Product>
    {
        private IProductDataService _ProductDataService;
        private ISupplierDataService _SupplierDataService;
        private ICategoryDataService _CategoryDataService;
        private ICompanyDataService _CompanyDataService;
        private DataContext _DataContext;

        public const string FILTER_BY_PRODUCT_NAME = "SearchBox";
        public const string FILTER_BY_COMPANY = "SelectedCompany";
        public const string FILTER_BY_SUPPLIER = "SelectedSupplier";
        public const string FILTER_BY_CATEGORY = "SelectedCategory";

        #region Props

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
                    FilterData(FILTER_BY_COMPANY);
            }
        }

        private string _SelectedCategory;

        public string SelectedCategory
        {
            get { return _SelectedCategory; }
            set
            {
                base.SetProperty(ref _SelectedCategory, value);

                if (!string.IsNullOrWhiteSpace(value))
                    FilterData(FILTER_BY_CATEGORY);
            }
        }

        private string _SelectedSupplier;

        public string SelectedSupplier
        {
            get { return _SelectedSupplier; }
            set
            {
                base.SetProperty(ref _SelectedSupplier, value);

                if (!string.IsNullOrWhiteSpace(value))
                    FilterData(FILTER_BY_SUPPLIER);
            }
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
                    FilterData(FILTER_BY_PRODUCT_NAME);
                }
            }
        }

        public override bool IsVisible => true;

        public IDependencyContainer DependencyContainer { get; private set; }
      
        #endregion

        public SearchProductsViewModel(IDependencyContainer container)
        {
            DependencyContainer = container;

            EventAggregator = container.Resolve<IEventAggregator>();

            _ProductDataService = container.Resolve<IProductDataService>();
            _SupplierDataService = container.Resolve<ISupplierDataService>();
            _CategoryDataService = container.Resolve<ICategoryDataService>();
            _CompanyDataService = container.Resolve<ICompanyDataService>();
            _DataContext = container.Resolve<DataContext>();

            ClearFiltersCommand = new RelayCommand(ClearFilters);
            OpenDetailsCommand = new DelegateCommand<object>(OpenDetails);
            OpenAddProductCommand = new RelayCommand(OpenNewProduct);

            EventAggregator.GetEvent<SaveEvent<Product>>()
                           .Subscribe(OnSave, keepSubscriberReferenceAlive: true);

            EventAggregator.GetEvent<DeleteEvent<Product>>()
                           .Subscribe(OnDelete, keepSubscriberReferenceAlive: true);

            Items = new ObservableCollection<Product>();

            LoadFilters();
            LoadDataAsync();
        }

        private void OnDelete(Product instance)
        {
            if (!Exists(instance))
            {
                return;
            }
            Items.Remove(instance);
            RefreshView();
        }

        private bool Exists(Product instance)
        {
            return Items.Contains(instance) && Items.Any(p => p.ProductID == instance.ProductID);
        }

        #region Methods

        public void NotifySave(Product instance)
        {
            if (!Exists(instance))
            {
                Items.Add(instance);
            }
        }

        public async void LoadDataAsync()
        {
            Items.Clear();

            var data = await _ProductDataService.GetAllAsync();
            Items.AddRange(data);

            SetupPagination(pageCount: SelectedPageSize, Items.ToArray());
        }

        private void OnSave(Product instance)
        {
            if (!Exists(instance))
            {
                Items.Add(instance);
            }
            CollectionViewSource.GetDefaultView(Items)?.Refresh();
        }

        #endregion

        #region Filter Data
        public async void LoadFilters()
        {
            Companies = await _CompanyDataService.GetAllDesctiption();
            Categories = await _CategoryDataService.GetAllDesctiption();
            Suppliers = await _SupplierDataService.GetAllDesctiption();
        }

        protected override void FilterData(string filter)
        {
            IQueryable<Product> query = _DataContext.Products.AsQueryable();

            if (!GlobalFilter.Contains(filter))
            {
                GlobalFilter.Add(filter);
            }
            if (string.IsNullOrEmpty(SearchBox))
            {
                GlobalFilter.Remove(FILTER_BY_PRODUCT_NAME);
            }

            if (GlobalFilter.Contains(FILTER_BY_COMPANY))
            {
                var id = _DataContext.CrashCompanies.Where(company => (company.CCName ?? string.Empty).Equals(SelectedCompany ?? string.Empty, StringComparison.OrdinalIgnoreCase))
                                                    .Select(company => company.CCID).FirstOrDefault();

                query = query.AsQueryable()
                             .Where(s => s.CCID == id).AsQueryable();
            }
            if (GlobalFilter.Contains(FILTER_BY_CATEGORY))
            {
                var id = _DataContext.ProductCategories.FirstOrDefault(category => (category.PCName ?? string.Empty).Equals(SelectedCategory ?? string.Empty, StringComparison.OrdinalIgnoreCase))?
                                                       .PCID;

                query = query.AsQueryable()
                             .Where(product => product.PCID == id)
                             .AsQueryable();
            }
            if (GlobalFilter.Contains(FILTER_BY_SUPPLIER))
            {
                var id = _DataContext.Suppliers.FirstOrDefault(supplier => supplier.SupplierName == SelectedSupplier)?
                                               .SupplierID;

                query = query.AsQueryable()
                             .Where(item => item.SupplierID == id)
                             .AsQueryable();
            }
            if (GlobalFilter.Contains(FILTER_BY_PRODUCT_NAME))
            {
                query = query.AsQueryable()
                             .Where(s => s.ProductDescription.ToLowerInvariant().Contains(SearchBox.ToLowerInvariant()))
                             .AsQueryable();
            }

            Items.Clear();
            Items.AddRange(query.ToArray());

            SetupPagination(SelectedPageSize, Items.ToArray());
        }

        #endregion

        #region Clear Filters
        public async void ClearFilters(object parameter)
        {
            SelectedCompany = null;
            SelectedCategory = null;
            SelectedSupplier = null;
            SearchBox = null;

            GlobalFilter.Clear();

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
