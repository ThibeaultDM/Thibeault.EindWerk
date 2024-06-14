using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thibeault.EindWerk.DataLayer;
using Thibeault.EindWerk.Objects.DataObjects;
using Thibeault.EindWerk.Objects.Enums;

namespace Thibeault.EindWerk.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository repository;

        public CustomerService(ICustomerRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<Customer>> MostLoyalCustomers(int howMany)
        {
            List<Customer> customers = await repository.QueryCustomers()
                                                       .Select(c => new
                                                       {
                                                         Customer = c,
                                                         OrderedAmount = c.Orders
                                                         .Where(o => o.Status != Status.Cancelled)
                                                         .SelectMany(o => o.StockActions)
                                                         .Sum(sa => sa.Amount)
                                                       })
           .OrderByDescending(an => an.OrderedAmount).Take(howMany).Select(an => an.Customer).ToListAsync();

            return customers;
        }
    }
}
