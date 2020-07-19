namespace phonebook
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Abonent")]
    public partial class Abonent
    {
        [Key]
        public int Id_abonent { get; set; }

        [Required]
        [StringLength(50)]
        public string surname { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [StringLength(50)]
        public string phone { get; set; }

        [StringLength(50)]
        public string otchestvo { get; set; }

        public int Id_address { get; set; }

        public virtual Address Address { get; set; }
    }
}
