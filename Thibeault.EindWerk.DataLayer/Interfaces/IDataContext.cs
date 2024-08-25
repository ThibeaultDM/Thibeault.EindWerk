using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Thibeault.Example.Objects;
using Thibeault.Example.Objects.DataObjects;

namespace Thibeault.Example.DataLayer.Interfaces
{
    public interface IDataContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Address> Addresses { get; set; }
        DbSet<IdentityUser> Users { get; set; }
        DbSet<OrderHeader> OrderHeaders { get; set; }

        DbSet<StockAction> StockActions { get; set; }

        ValueTask DisposeAsync();

        Task<int> SaveChangesAsync();
    }
}