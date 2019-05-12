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
        public static Dictionary<string, List<string>> Validate(Product product)
        {
            var list = new Dictionary<string, List<string>>();

            if (product == null)
                list.Add(nameof(Product), new List<string> { "Product cannot be null" });

            if (product.ProductCategory == null)
                list.Add(nameof(product.ProductCategory), new List<string> { "Category cannot be null" });

            if (product.Company == null)
                list.Add(nameof(product.Company), new List<string> { "Company cannot be null" });

            if (product.Supplier == null)
                list.Add(nameof(product.Supplier), new List<string> { "Supplier cannot be null" });

            if (string.IsNullOrWhiteSpace(product.ProductDescription))
                list.Add(nameof(product.ProductDescription), new List<string> { "Description cannot be null" });

            if (string.IsNullOrWhiteSpace(product.ProductURL))
                list.Add(nameof(product.ProductURL), new List<string> { "URL cannot be null" });

            return list;
        }

        
        #endregion
        

    }
}
