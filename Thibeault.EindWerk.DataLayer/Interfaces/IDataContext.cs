using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Thibeault.EindWerk.Objects;
using Thibeault.EindWerk.Objects.DataObjects;

namespace Thibeault.EindWerk.DataLayer.Interfaces
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