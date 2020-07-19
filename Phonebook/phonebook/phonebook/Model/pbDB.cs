namespace phonebook
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class pbDB : DbContext
    {
        public pbDB()
            : base("name=pbDB")
        {
        }

        public virtual DbSet<Abonent> Abonent { get; set; }
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Company> Company { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .HasMany(e => e.Abonent)
                .WithRequired(e => e.Address)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Address>()
                .HasMany(e => e.Company)
                .WithRequired(e => e.Address)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Address)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);
        }
    }
}
