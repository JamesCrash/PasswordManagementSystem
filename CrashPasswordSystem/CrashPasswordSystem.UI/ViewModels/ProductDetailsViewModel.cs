using CrashPasswordSystem.BusinessLogic.Validation;
using CrashPasswordSystem.Data;
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
            set { _SelectedCategory = value; OnPropertyChanged(); }
        }

        private Supplier _SelectedSupplier;

        public Supplier SelectedSupplier
        {
            get { return _SelectedSupplier; }
            set { _SelectedSupplier = value; OnPropertyChanged(); }
        }

        private List<string> _Errors;

        public List<string> Errors
        {
            get { return _Errors; }
            set { _Errors = value; OnPropertyChanged(); }
        }


        #endregion

        public ProductDetailsViewModel(Product product)
        {
            Product = product;
            ProductOriginal = product;
            QuitCommand = new RelayCommand(Quit);
            QuitDeleteCommand = new RelayCommand(QuitDelete);
            QuitSaveCommand = new RelayCommand(QuitSave);

            LoadComboData();
        }

        #region Load Filters Options
        public async void LoadComboData()
        {
            using (var dBContext = new ITDatabaseContext())
            {
                Companies = dBContext.CrashCompanies.ToList();
                SelectedCompany = dBContext.CrashCompanies.Where(s => s.Ccid == Product.Ccid).FirstOrDefault();
                Categories = dBContext.ProductCategories.ToList();
                SelectedCategory = dBContext.ProductCategories.Where(s => s.Pcid == Product.Pcid).FirstOrDefault();
                Suppliers = dBContext.Suppliers.ToList();
                SelectedSupplier = dBContext.Suppliers.Where(s => s.SupplierId == Product.SupplierId).FirstOrDefault();
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
            using (var dBContext = new ITDatabaseContext())
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
            using (var dBContext = new ITDatabaseContext())
            {
                var p = dBContext.Products.Where(s => s.ProductId == Product.ProductId).SingleOrDefault();

                p.Pcid = Product.Pcid;
                p.Ccid = Product.Ccid;
                p.SupplierId = Product.SupplierId;
                p.ProductDescription = Product.ProductDescription;
                p.ProductUrl = Product.ProductUrl;
                p.ProductUsername = Product.ProductUsername;
                p.ProductPassword = Product.ProductPassword;
                p.ProductExpiry = Product.ProductExpiry;
                p.Ccid = SelectedCompany.Ccid;
                p.Pcid = SelectedCategory.Pcid;
                p.SupplierId = SelectedSupplier.SupplierId;

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
