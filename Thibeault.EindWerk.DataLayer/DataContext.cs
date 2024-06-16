using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Thibeault.EindWerk.DataLayer.Interfaces;
using Thibeault.EindWerk.Objects;
using Thibeault.EindWerk.Objects.DataObjects;

namespace Thibeault.EindWerk.DataLayer
{
    public class DataContext : IdentityDbContext<IdentityUser>, IDataContext
    {
        // TODO transaction scope

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
        public DbSet<Address> Addresses { get; set; }
        public DbSet<IdentityUser> Users { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }

        //public DbSet<OrderLineProbablyDeprecated> OrderLines { get; set; }
        public DbSet<StockAction> StockActions { get; set; }

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
            builder.Entity<Address>().ToTable("Adresses");
            builder.Entity<IdentityUser>().ToTable("Users");
            builder.Entity<OrderHeader>().ToTable("OrderHeaders");
            //builder.Entity<OrderLineProbablyDeprecated>().ToTable("OrderLines");
            builder.Entity<StockAction>().ToTable("StockActions");

            builder.Entity<Product>().HasIndex(x => x.Id).IsUnique();
            builder.Entity<Address>().HasIndex(x => x.Id).IsUnique();
            builder.Entity<Customer>().HasIndex(x => x.Id).IsUnique();
            builder.Entity<OrderHeader>().HasIndex(x => x.TrackingNumber).IsUnique();

            builder.Entity<Customer>().HasOne(c => c.Address);
            builder.Entity<Customer>().HasMany(c => c.Orders)
                                      .WithOne(oh => oh.Customer);

            builder.Entity<OrderHeader>().HasMany(oh => oh.StockActions)
                                         .WithOne(ol => ol.OrderHeader);

            builder.Entity<Product>().HasMany(p => p.StockActions)
                                     .WithOne(s => s.Product);

            //builder.Entity<OrderLineProbablyDeprecated>().HasOne(ol => ol.StockAction).WithOne(sa => sa.OrderLineProbablyDeprecated);
            base.OnModelCreating(builder);
        }
    }
}