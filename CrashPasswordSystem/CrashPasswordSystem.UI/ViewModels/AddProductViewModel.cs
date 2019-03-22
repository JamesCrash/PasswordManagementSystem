﻿using CrashPasswordSystem.Data;
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
            get { return _Product; }
            set { _Product = value; OnPropertyChanged(); }
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
            using (var dBContext = new ITDatabaseContext())
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
            using (var dBContext = new ITDatabaseContext())
            {
                var p = new Product();
                
                p.ProductDescription = Product.ProductDescription;
                p.ProductUrl = Product.ProductUrl;
                p.ProductUsername = Product.ProductUsername;
                p.ProductPassword = Product.ProductPassword;
                p.ProductExpiry = Product.ProductExpiry;
                p.Ccid = SelectedCompany.Ccid;
                p.Pcid = SelectedCategory.Pcid;
                p.SupplierId = SelectedSupplier.SupplierId;
                p.StaffId = 1;

                dBContext.Products.Add(p);
                dBContext.SaveChanges();

                OnRequestClose(this, new EventArgs());
            }
        }
        #endregion
    }
}
