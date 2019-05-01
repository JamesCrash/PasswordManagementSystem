using CrashPasswordSystem.Data;
using CrashPasswordSystem.Models;
using CrashPasswordSystem.UI.Command;
using CrashPasswordSystem.UI.Event;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace CrashPasswordSystem.UI.ViewModels
{
    public class AddProductViewModel : ViewModelBase
    {
        private readonly Func<DataContext> _contextCreator;

        public IDependencyContainer Container { get; set; }

        public AddProductViewModel(IDependencyContainer container)
        {
            _contextCreator = () => container.Resolve<DataContext>();

            Container = container;
            Product = new Product();
            LoadComboData();
            QuitCommand = new RelayCommand(Quit);
            QuitSaveCommand = new RelayCommand(QuitAdd);

            EventAggregator = container.Resolve<IEventAggregator>();
        }

        #region Props

        private Product _Product;

        public Product Product
        {
            get => _Product;
            set => base.SetProperty(ref _Product, value);
        }

        public ICommand QuitCommand { get; set; }
        public ICommand QuitSaveCommand { get; set; }

        public List<CrashCompany> Companies { get; set; }
        public List<ProductCategory> Categories { get; set; }
        public List<Supplier> Suppliers { get; set; }

        private CrashCompany _SelectedCompany;

        public CrashCompany SelectedCompany
        {
            get { return _SelectedCompany; }
            set => base.SetProperty(ref _SelectedCompany, value);
        }

        private ProductCategory _SelectedCategory;
        public ProductCategory SelectedCategory
        {
            get { return _SelectedCategory; }
            set => base.SetProperty(ref _SelectedCategory, value);
        }

        private Supplier _SelectedSupplier;
        public Supplier SelectedSupplier
        {
            get { return _SelectedSupplier; }
            set => base.SetProperty(ref _SelectedSupplier, value);
        }

        #endregion

        #region Load Filters Options
        public async void LoadComboData()
        {
            using (var dBContext = _contextCreator())
            {
                Companies = dBContext.CrashCompanies.ToList();
                Categories = dBContext.ProductCategories.ToList();
                Suppliers = dBContext.Suppliers.ToList();
            }
        }
        #endregion

        #region Quit Button

        public void Quit(object parameter)
        {
            EventAggregator.GetEvent<CloseEvent>().Publish(this);
        }
        #endregion

        #region Quit and Add Button
        public async void QuitAdd(object parameter)
        {
            if (SelectedCompany == null)
            {
                return;
            }
            if (SelectedCategory == null)
            {
                return;
            }
            if (SelectedSupplier == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(Product.ProductDescription))
            {
                return;
            }
            if (string.IsNullOrEmpty(Product.ProductURL))
            {
                return;
            }
            using (var dBContext = _contextCreator())
            {
                var p = new Product();
                
                p.ProductDescription = Product.ProductDescription;
                p.ProductURL = Product.ProductURL;
                p.ProductUsername = Product.ProductUsername;
                p.ProductPassword = Product.ProductPassword;
                p.ProductExpiry = Product.ProductExpiry;
                p.CCID = SelectedCompany.CCID;
                p.PCID = SelectedCategory.PCID;
                p.SupplierID = SelectedSupplier.SupplierID;
                p.StaffID = 1;
                p.ProductDateAdded = DateTime.Now;

                dBContext.Products.Add(p);
                await dBContext.SaveChangesAsync();
            }

            EventAggregator.GetEvent<CloseEvent>().Publish(this);
        }

        internal void Show()
        {
        }
        #endregion
    }
}
