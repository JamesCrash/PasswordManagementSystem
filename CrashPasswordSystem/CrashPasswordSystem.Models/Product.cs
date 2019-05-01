using System;
using System.Collections.Generic;

namespace CrashPasswordSystem.Models
{
    public partial class Product
    {
        public Product()
        {
            UpdateHistories = new HashSet<UpdateHistory>();
        }

        public int ProductID { get; set; }
        public int PCID { get; set; }
        public int CCID { get; set; }
        public int SupplierID { get; set; }
        public int StaffID { get; set; }
        public string ProductDescription { get; set; }
        public string ProductURL { get; set; }
        public string ProductUsername { get; set; }
        public string ProductPassword { get; set; }
        public DateTime ProductDateAdded { get; set; }
        public DateTime? ProductExpiry { get; set; }

        public virtual CrashCompany Company { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual User Staff { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<UpdateHistory> UpdateHistories { get; set; }
    }
}
