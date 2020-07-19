namespace phonebook
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Company")]
    public partial class Company
    {
        [Key]
        public int Id_company { get; set; }

        [Required]
        [StringLength(50)]
        public string name_company { get; set; }

        [Required]
        [StringLength(50)]
        public string department { get; set; }

        public int Id_address { get; set; }

        [Required]
        [StringLength(50)]
        public string phone { get; set; }

        public virtual Address Address { get; set; }
    }
}
