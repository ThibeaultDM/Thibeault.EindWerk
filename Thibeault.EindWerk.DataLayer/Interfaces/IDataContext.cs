using Microsoft.EntityFrameworkCore;
using Thibeault.EindWerk.Objects;

namespace Thibeault.EindWerk.DataLayer.Interfaces
{
    public interface IDataContext
    {
        DbSet<Product> Products { get; set; }

        DbSet<Customer> Customers { get; set; }

        DbSet<Address> Addresss { get; set; }
        DbSet<User> Users { get; set; }

        ValueTask DisposeAsync();

        Task<int> SaveChangesAsync();
    }
}