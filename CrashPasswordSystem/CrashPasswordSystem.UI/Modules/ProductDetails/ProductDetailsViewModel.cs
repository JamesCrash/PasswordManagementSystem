using CrashPasswordSystem.BusinessLogic.Validation;
using CrashPasswordSystem.Data;
using CrashPasswordSystem.Models;
using CrashPasswordSystem.UI.Command;
using CrashPasswordSystem.UI.Event;
using CrashPasswordSystem.UI.Wrapper;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CrashPasswordSystem.UI.ViewModels
{
    public class ProductDetailsViewModel : NotifyDataErrorInfoBase
    {
        private readonly Func<DataContext> _contextCreator;

        #region Props

        public Product Product { get; private set; }

        private Product _ProductOriginal;
        public Product ProductOriginal
        {
            get { return _ProductOriginal; }
            set { SetProperty(ref _ProductOriginal, value); }
        }

        public ICommand QuitCommand { get; set; }
        public ICommand QuitDeleteCommand { get; set; }
        public ICommand QuitSaveCommand { get; set; }

        public List<CrashCompany> Companies { get; set; }
        public List<ProductCategory> Categories { get; set; }
        public List<Supplier> Suppliers { get; set; }

        private CrashCompany _SelectedCompany;

        public CrashCompany SelectedCompany
        {
            get { return _SelectedCompany; }
            set { base.SetProperty(ref _SelectedCompany, value); }
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

        public ProductDetailsViewModel(IDependencyContainer container)
        {
            _contextCreator = () => container.Resolve<DataContext>();

            QuitCommand = new RelayCommand(param => OnRequestClose());
            QuitDeleteCommand = new RelayCommand(QuitDelete);
            QuitSaveCommand = new RelayCommand(QuitSave);

            EventAggregator = container.Resolve<IEventAggregator>();

            Product = new Product();
        }

        #region Load Filters Options

        protected override bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            var result = base.SetProperty(ref storage, value, propertyName);

            if (result && propertyName == nameof(ProductOriginal))
            {
                LoadComboData();

                TransferValues(ProductOriginal, Product);
            }

            return result;
        }


        public void LoadComboData()
        {
            using (var dBContext = _contextCreator())
            {
                Companies = dBContext.CrashCompanies.ToList();
                SelectedCompany = dBContext.CrashCompanies.Where(s => s.CCID == ProductOriginal.CCID).FirstOrDefault();
                Categories = dBContext.ProductCategories.ToList();
                SelectedCategory = dBContext.ProductCategories.Where(s => s.PCID == ProductOriginal.PCID).FirstOrDefault();
                Suppliers = dBContext.Suppliers.ToList();
                SelectedSupplier = dBContext.Suppliers.Where(s => s.SupplierID == ProductOriginal.SupplierID).FirstOrDefault();
            }
        }
        #endregion

        #region Quit and Delete Button
        public async void QuitDelete(object parameter)
        {
            using (var dBContext = _contextCreator())
            {
                dBContext.Products.Attach(ProductOriginal);
                dBContext.Products.Remove(ProductOriginal);

                await dBContext.SaveChangesAsync();

                OnRequestClose();
            }

        }
        #endregion

        #region Quit and Save Button
        public async void QuitSave(object parameter)
        {
            Product.ProductCategory = SelectedCategory;
            Product.Supplier = SelectedSupplier;
            Product.Company = SelectedCompany;

            if (!Validate())
            {
                return;
            }
            using (var dBContext = _contextCreator())
            {
                var p = dBContext.Products.Where(s => s.ProductID == Product.ProductID).SingleOrDefault();

                p.PCID = Product.PCID;
                p.CCID = Product.CCID;
                p.SupplierID = Product.SupplierID;
                p.ProductDescription = Product.ProductDescription;
                p.ProductURL = Product.ProductURL;
                p.ProductUsername = Product.ProductUsername;
                p.ProductPassword = Product.ProductPassword;
                p.ProductExpiry = Product.ProductExpiry;
                p.CCID = SelectedCompany.CCID;
                p.PCID = SelectedCategory.PCID;
                p.SupplierID = SelectedSupplier.SupplierID;

                await dBContext.SaveChangesAsync();

                TransferValues(Product, ProductOriginal);

                OnRequestClose();
            }
        }

        public bool Validate()
        {
            if (Product == null)
            {
                return false;
            }
            SetErrors(ProductDetailsValidation.Validate(Product));
            if (Errors.Count != 0) return false;
            else return true;
        }
        #endregion

        #region Methods

        public bool Validate(object parameter)
        {
            SetErrors(ProductDetailsValidation.Validate(Product));
            if (Errors.Count != 0 ) return false;
            else return true;
        }

        private static void TransferValues(Product fromModel, Product toModel)
        {
            toModel.ProductID = fromModel.ProductID;
            toModel.CCID = fromModel.CCID;
            toModel.SupplierID = fromModel.SupplierID;
            toModel.ProductDescription = fromModel.ProductDescription;
            toModel.ProductURL = fromModel.ProductURL;
            toModel.ProductUsername = fromModel.ProductUsername;
            toModel.ProductPassword = fromModel.ProductPassword;
            toModel.ProductExpiry = fromModel.ProductExpiry;
            toModel.StaffID = fromModel.StaffID;
            toModel.PCID = fromModel.PCID;
        }

        private void OnRequestClose()
        {
            EventAggregator
                .GetEvent<CloseEvent>()
                .Publish(this);
        }

        #endregion
    }
}
