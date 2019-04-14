using System.Collections.Generic;
using CrashPasswordSystem.Data;
using CrashPasswordSystem.Models;

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
            if (product.ProductID == 0) { list.Add("Product ID cannot be null"); }
            if (product.PCID == 0) { list.Add("Category cannot be null"); }
            if (product.CCID == 0) { list.Add("Company cannot be null"); }
            if (product.SupplierID == 0) { list.Add("Supplier cannot be null"); }
            if (string.IsNullOrWhiteSpace(product.ProductDescription)) { list.Add("Description cannot be null"); }
            if (string.IsNullOrWhiteSpace(product.ProductURL)) { list.Add("URL cannot be null"); }

            return list;

        }

        
        #endregion
        

    }
}
