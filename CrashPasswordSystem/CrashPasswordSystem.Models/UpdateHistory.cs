using System;
using System.Collections.Generic;

namespace CrashPasswordSystem.Models
{
    public partial class UpdateHistory
    {
        public int UHID { get; set; }
        public int ProductID { get; set; }
        public int StaffID { get; set; }
        public DateTime DateUpdated { get; set; }

        public virtual Product Product { get; set; }
    }
}
