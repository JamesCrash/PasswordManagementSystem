using CrashPasswordSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrashPasswordSystem.BusinessLogic.Validation
{
    public class ProductDetailsValidation
    {
        #region Props

        #endregion


        #region Validate Product
        //public static List<string> ValidateProduct(Product product)
        //{

        //}
        #endregion

        #region Check Product Nulls
        public static List<string> CheckNulls(Product product)
        {
            var list = new List<string>();

            if (product == null) { list.Add("Product cannot be null"); }
            if (product.ProductId == 0) { list.Add("Product ID cannot be null"); }
            if (product.Pcid == 0) { list.Add("Category cannot be null"); }
            if (product.Ccid == 0) { list.Add("Company cannot be null"); }
            if (product.SupplierId == 0) { list.Add("Supplier cannot be null"); }
            if (string.IsNullOrWhiteSpace(product.ProductDescription)) { list.Add("Description cannot be null"); }
            if (string.IsNullOrWhiteSpace(product.ProductUrl)) { list.Add("URL cannot be null"); }

            return list;

        }

        
        #endregion
        

    }
}
