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
        public DbSet<OrderHeader> OrderHeaders { get; set; }

        //public DbSet<OrderLineProbalyDepricated> OrderLines { get; set; }
        public DbSet<StockAction> StockActions { get; set; }

        // Wraping DbContext.SaveChangesAsync method
        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();
        public override int SaveChanges() => base.SaveChanges();
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>().ToTable("Products");
            builder.Entity<Customer>().ToTable("Customers");
            builder.Entity<Address>().ToTable("Adresses");
            builder.Entity<User>().ToTable("Users");
            builder.Entity<OrderHeader>().ToTable("OrderHeaders");
            //builder.Entity<OrderLineProbalyDepricated>().ToTable("OrderLines");
            builder.Entity<StockAction>().ToTable("StockActions");

            builder.Entity<Product>().HasIndex(x => x.Id).IsUnique();
            builder.Entity<Address>().HasIndex(x => x.Id).IsUnique();
            builder.Entity<Customer>().HasIndex(x => x.Id).IsUnique();
            builder.Entity<OrderHeader>().HasIndex(x => x.TrackingNumber).IsUnique();

            builder.Entity<Customer>().HasMany(c => c.Orders).WithOne(oh => oh.Customer);
            builder.Entity<OrderHeader>().HasMany(oh => oh.StockActions).WithOne(ol => ol.OrderHeader);
            //builder.Entity<OrderLineProbalyDepricated>().HasOne(ol => ol.StockAction).WithOne(sa => sa.OrderLineProbalyDepricated);
            builder.Entity<Product>().HasMany(p => p.StockActions).WithOne(s => s.Product);

            base.OnModelCreating(builder);
        }
    }
}