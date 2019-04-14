namespace CrashPasswordSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Products = new HashSet<Product>();
        }

        public int UserID { get; set; }

        [StringLength(50)]
        public string UserFirstName { get; set; }

        [StringLength(50)]
        public string UserLastName { get; set; }

        [StringLength(10)]
        public string UserInitials { get; set; }

        [StringLength(100)]
        public string UserEmail { get; set; }

        [StringLength(100)]
        public string UserHash { get; set; }

        [StringLength(20)]
        public string UserSalt { get; set; }

        public DateTime? UserDateCreated { get; set; }

        public bool? UserActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
