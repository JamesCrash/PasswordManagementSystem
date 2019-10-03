using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrashPasswordSystem.Models
{
    [Table(nameof(ProductCategory))]

    public partial class ProductCategory
    {
        public ProductCategory()
        {
            Products = new HashSet<Product>();
        }

        public int PCID { get; set; }
        public string PCName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
