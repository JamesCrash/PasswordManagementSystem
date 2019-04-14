namespace CrashPasswordSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            UpdateHistories = new HashSet<UpdateHistory>();
        }

        public int ProductID { get; set; }

        public int PCID { get; set; }

        public int CCID { get; set; }

        public int SupplierID { get; set; }

        public int StaffID { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductDescription { get; set; }

        [Required]
        [StringLength(200)]
        public string ProductURL { get; set; }

        [Required]
        [StringLength(40)]
        public string ProductUsername { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductPassword { get; set; }

        public DateTime ProductDateAdded { get; set; }

        public DateTime? ProductExpiry { get; set; }

        public virtual CrashCompany CrashCompany { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }

        public virtual User User { get; set; }

        public virtual Supplier Supplier { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UpdateHistory> UpdateHistories { get; set; }
    }
}
