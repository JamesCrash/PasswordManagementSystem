using CrashPasswordSystem.Data;
using CrashPasswordSystem.Models;
using CrashPasswordSystem.UI.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CrashPasswordSystem.UI.ViewModels
{
    public class AddProductViewModel : ViewModelBase
    {
        #region Props
        public event EventHandler OnRequestClose;

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

        public AddProductViewModel()
        {
            Product = new Product();
            LoadComboData();
            QuitCommand = new RelayCommand(Quit);
            QuitSaveCommand = new RelayCommand(QuitAdd);
        }

        #region Load Filters Options
        public async void LoadComboData()
        {
            using (var dBContext = new DataContext())
            {
                Companies = dBContext.CrashCompanies.ToList();
                Categories = dBContext.ProductCategories.ToList();
                Suppliers = dBContext.Suppliers.ToList();
            }
        }
        #endregion

        #region Quit Button
        public async void Quit(object parameter)
        {
            OnRequestClose(this, new EventArgs());
        }
        #endregion

        #region Quit and Add Button
        public async void QuitAdd(object parameter)
        {
            using (var dBContext = new DataContext())
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

                dBContext.Products.Add(p);
                dBContext.SaveChanges();

                OnRequestClose(this, new EventArgs());
            }
        }
        #endregion
    }
}
