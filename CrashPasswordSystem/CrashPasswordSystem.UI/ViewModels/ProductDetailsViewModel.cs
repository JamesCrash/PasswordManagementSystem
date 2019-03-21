using CrashPasswordSystem.Data;

namespace CrashPasswordSystem.UI.ViewModels
{
    public class ProductDetailsViewModel : ViewModelBase
    {
        #region Props
        private Product _Product;

        public Product Product
        {
            get { return _Product; }
            set { _Product = value; }
        }

        #endregion
        public ProductDetailsViewModel(Product product)
        {
            Product = product;
        }
    }
}
