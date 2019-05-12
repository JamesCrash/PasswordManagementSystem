using CrashPasswordSystem.BusinessLogic.Validation;
using CrashPasswordSystem.Core;
using CrashPasswordSystem.Data;
using CrashPasswordSystem.Models;
using CrashPasswordSystem.UI.Command;
using CrashPasswordSystem.UI.Event;
using CrashPasswordSystem.UI.Wrapper;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace CrashPasswordSystem.UI.ViewModels
{
    public class AddProductViewModel : NotifyDataErrorInfoBase
    {
        private readonly Func<DataContext> _contextCreator;

        public AddProductViewModel(IDependencyContainer container)
        {
            _contextCreator = () => container.Resolve<DataContext>();

            Container = container;
            Product = new Product();
            LoadComboData();
            QuitCommand = new RelayCommand(Quit);
            QuitSaveCommand = new RelayCommand(QuitSave);

            EventAggregator = container.Resolve<IEventAggregator>();
        }

        public bool Validate()
        {
            if (Product == null)
            {
                return false;
            }
            if (CurrentUser == null)
            {
                return false;
            }
            SetErrors(ProductDetailsValidation.Validate(Product));
            if (Errors.Count != 0) return false;
            else return true;
        }

        #region Props

        public IDependencyContainer Container { get; set; }

        public User CurrentUser { get => Container.Resolve<IAuthenticationService>()?.User; } 

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
        public void LoadComboData()
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
        public async void QuitSave(object parameter)
        {
            Product.ProductCategory = SelectedCategory;
            Product.Supplier = SelectedSupplier;
            Product.Company = SelectedCompany;
            Product.Staff = CurrentUser;

            if (!Validate())
            {
                return;
            }

            using (var dBContext = _contextCreator())
            {
                var model = new Product();
                
                model.ProductDescription = Product.ProductDescription;
                model.ProductURL = Product.ProductURL;
                model.ProductUsername = Product.ProductUsername;
                model.ProductPassword = Product.ProductPassword;
                model.ProductExpiry = Product.ProductExpiry;
                model.CCID = SelectedCompany.CCID;
                model.PCID = SelectedCategory.PCID;
                model.SupplierID = SelectedSupplier.SupplierID;
                model.ProductDateAdded = DateTime.Now;
                model.StaffID = CurrentUser.UserID;

                dBContext.Products.Add(model);

                await dBContext.SaveChangesAsync();

                Product = model;
            }

            EventAggregator.GetEvent<SaveEvent<Product>>()
                               .Publish(Product);

            OnRequestClose();
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
