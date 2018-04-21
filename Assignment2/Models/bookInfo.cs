namespace Assignment2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("bookInfo")]
    public partial class bookInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public bookInfo()
        {
            bookLocations = new HashSet<bookLocation>();
        }

        [Key]
        public int bookID { get; set; }

        [StringLength(30)]
        [Display (Name="Title")]
        public string bookName { get; set; }

        [StringLength(30)]
        [Display(Name = "Author")]
        public string bookAuthor { get; set; }

        [StringLength(20)]
        [Display(Name = "Genre")]
        public string bookGenre { get; set; }

        [Display(Name = "Edition")]
        public int? bookEdition { get; set; }

        [Display(Name = "Release Year")]
        public int? bookReleaseYear { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bookLocation> bookLocations { get; set; }

        public static object Any(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }
}
