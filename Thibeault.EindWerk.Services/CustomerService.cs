using Microsoft.EntityFrameworkCore;
using Thibeault.EindWerk.Objects.DataObjects;
using Thibeault.EindWerk.Objects.Enums;

namespace Thibeault.EindWerk.Services
{
    public class CustomerService
    {
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