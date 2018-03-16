namespace Assignment2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("bookLocations")] //bookLocation
    public partial class bookLocation
    {
        [Key]
        public int bookStoreNumber { get; set; }

        public int bookID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name ="Store Location")]
        public string bookStoreLocation { get; set; }

        [StringLength(3)]
        [Display(Name = "Shelve")]
        public string bookShelve { get; set; }

        [StringLength(3)]
        [Display(Name = "ebook")]
        public string bookPhysical { get; set; }

        public virtual bookInfo bookInfo { get; set; }
    }
}
