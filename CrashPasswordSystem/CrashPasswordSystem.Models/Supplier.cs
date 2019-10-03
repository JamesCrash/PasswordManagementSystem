using System;
using System.Collections.Generic;

namespace CrashPasswordSystem.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            Products = new HashSet<Product>();
        }

        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierContactNumber { get; set; }
        public string SupplierEmail { get; set; }
        public string SupplierWebsite { get; set; }
        public DateTime SupplierDateAdded { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
