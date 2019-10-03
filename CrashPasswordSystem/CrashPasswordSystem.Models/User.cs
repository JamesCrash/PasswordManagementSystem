using System;
using System.Collections.Generic;

namespace CrashPasswordSystem.Models
{
    public partial class User
    {
        public User()
        {
            Products = new HashSet<Product>();
        }

        public int UserID { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserInitials { get; set; }
        public string UserEmail { get; set; }
        public string UserHash { get; set; }
        public string UserSalt { get; set; }
        public DateTime? UserDateCreated { get; set; }
        public bool? UserActive { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
