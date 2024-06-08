using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Thibeault.EindWerk.DataLayer.Interfaces;
using Thibeault.EindWerk.Objects;

namespace Thibeault.EindWerk.DataLayer
{
    public class DataContext : IdentityDbContext<User>, IDataContext
    {
        #region Constructors

        public DataContext()
        {
        }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        #endregion Constructors

        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Address> Addresss { get; set; }
        public DbSet<User> Users { get; set; }

        // Wraping DbContext.SaveChangesAsync method
        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>().ToTable("Products");
            builder.Entity<Customer>().ToTable("Customers");
            builder.Entity<Address>().ToTable("Customers");
            builder.Entity<User>().ToTable("Users");

            builder.Entity<Product>().HasIndex(x => x.Id).IsUnique();
            builder.Entity<Address>().HasIndex(x => x.Id).IsUnique();
            builder.Entity<Customer>().HasIndex(x => x.Id).IsUnique();
            builder.Entity<User>().HasIndex(x => x.Id).IsUnique();

            builder.Entity<Product>().HasAlternateKey(x => x.SerialNumber);
            builder.Entity<Customer>().HasAlternateKey(x => x.TrackingNumber);

            builder.Entity<Customer>().HasOne<Address>(c => c.Address);

            base.OnModelCreating(builder);
        }
    }
}