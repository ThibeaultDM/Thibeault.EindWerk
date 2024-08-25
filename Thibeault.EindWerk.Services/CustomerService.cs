using Microsoft.EntityFrameworkCore;
using Thibeault.Example.Objects.DataObjects;
using Thibeault.Example.Objects.Enums;

namespace Thibeault.Example.Services
{
    public class CustomerService
    {
        // TODO replace this with a generated view, save the string here and execute RawSQLAsync on the database
        public async Task<List<Customer>> MostLoyalCustomersAsync(IQueryable<Customer> query, int howMany)
        {
            List<Customer> customers = await query.Select(c => new
            {
                Customer = c,
                OrderedAmount = c.Orders
                .Where(o => o.Status != Status.Cancelled).SelectMany(o => o.StockActions)
                .Sum(sa => sa.Amount)
            })
           .OrderByDescending(an => an.OrderedAmount).Take(howMany).Select(an => an.Customer).ToListAsync();

            return customers;
        }
    }
}