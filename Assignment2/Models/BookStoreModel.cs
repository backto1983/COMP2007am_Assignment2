namespace Assignment2.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BookStoreModel : DbContext
    {
        public BookStoreModel()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<bookInfo> bookInfo { get; set; }
        public virtual DbSet<bookLocation> bookLocations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<bookInfo>()
                .Property(e => e.bookName)
                .IsUnicode(false);

            modelBuilder.Entity<bookInfo>()
                .Property(e => e.bookAuthor)
                .IsUnicode(false);

            modelBuilder.Entity<bookInfo>()
                .Property(e => e.bookGenre)
                .IsUnicode(false);

            modelBuilder.Entity<bookInfo>()
                .HasMany(e => e.bookLocations)
                .WithRequired(e => e.bookInfo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<bookLocation>()
                .Property(e => e.bookStoreLocation)
                .IsUnicode(false);

            modelBuilder.Entity<bookLocation>()
                .Property(e => e.bookShelve)
                .IsUnicode(false);

            modelBuilder.Entity<bookLocation>()
                .Property(e => e.bookPhysical)
                .IsUnicode(false);
        }
    }
}
