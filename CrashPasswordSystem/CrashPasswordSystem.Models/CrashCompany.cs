using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrashPasswordSystem.Models
{
    [Table(nameof(CrashCompany))]
    public partial class CrashCompany
    {
        public CrashCompany()
        {
            Products = new HashSet<Product>();
        }

        public int CCID { get; set; }
        public string CCName { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
