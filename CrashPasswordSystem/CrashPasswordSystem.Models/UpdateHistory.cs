namespace CrashPasswordSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UpdateHistory")]
    public partial class UpdateHistory
    {
        [Key]
        public int UHID { get; set; }

        public int ProductID { get; set; }

        public int StaffID { get; set; }

        public DateTime DateUpdated { get; set; }

        public virtual Product Product { get; set; }
    }
}
