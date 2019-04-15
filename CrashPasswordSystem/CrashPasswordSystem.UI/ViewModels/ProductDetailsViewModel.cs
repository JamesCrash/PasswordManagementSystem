using CrashPasswordSystem.BusinessLogic.Validation;
using CrashPasswordSystem.Data;
using CrashPasswordSystem.Models;
using CrashPasswordSystem.UI.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace CrashPasswordSystem.UI.ViewModels
{
    public class ProductDetailsViewModel : ViewModelBase
    {
        #region Props
        public event EventHandler OnRequestClose;

        private Product _Product;

        public Product Product
        {
            get { return _Product; }
            set { _Product = value; }
        }

        private Product _ProductOriginal;
        public Product ProductOriginal
        {
            get { return _ProductOriginal; }
            set { _ProductOriginal = value; }
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
            set { _SelectedCompany = value; OnPropertyChanged(); }
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

        private List<string> _Errors;
        public List<string> Errors
        {
            get { return _Errors; }
            set => base.SetProperty(ref _Errors, value);
        }

        #endregion

        public ProductDetailsViewModel(Product product)
        {
            Product = product;
            ProductOriginal = product;
            QuitCommand = new RelayCommand(Quit);
            QuitDeleteCommand = new RelayCommand(QuitDelete);
            QuitSaveCommand = new RelayCommand(QuitSave, Validate);

            LoadComboData();
        }

        #region Load Filters Options
        public async void LoadComboData()
        {
            using (var dBContext = new DataContext())
            {
                Companies = dBContext.CrashCompanies.ToList();
                SelectedCompany = dBContext.CrashCompanies.Where(s => s.CCID == Product.CCID).FirstOrDefault();
                Categories = dBContext.ProductCategories.ToList();
                SelectedCategory = dBContext.ProductCategories.Where(s => s.PCID == Product.PCID).FirstOrDefault();
                Suppliers = dBContext.Suppliers.ToList();
                SelectedSupplier = dBContext.Suppliers.Where(s => s.SupplierID == Product.SupplierID).FirstOrDefault();
            }
        }
        #endregion

        #region Quit Button
        public async void Quit(object parameter)
        {
            OnRequestClose(this, new EventArgs());
        }
        #endregion

        #region Quit and Delete Button
        public async void QuitDelete(object parameter)
        {
            using (var dBContext = new DataContext())
            {
                dBContext.Products.Attach(ProductOriginal);
                dBContext.Products.Remove(ProductOriginal);
                dBContext.SaveChanges();
                OnRequestClose(this, new EventArgs());
            }

        }
        #endregion

        #region Quit and Save Button
        public async void QuitSave(object parameter)
        {
            using (var dBContext = new DataContext())
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

                dBContext.SaveChanges();
                OnRequestClose(this, new EventArgs());
            }
        }
        #endregion

        #region Validation
        public bool Validate(object parameter)
        {
            Errors = ProductDetailsValidation.CheckNulls(Product);
            if (Errors.Count != 0 ) return false;
            else return true;
        }
        #endregion
    }
}
