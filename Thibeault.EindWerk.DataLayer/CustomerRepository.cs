using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thibeault.EindWerk.Objects;

namespace Thibeault.EindWerk.DataLayer
{
    public class CustomerRepository
    {
        private readonly IDataContext dataContext;

        public CustomerRepository(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public virtual async Task<Customer> CreateCustomer(Customer customerToCreate)
        {
            // TODO give a place holder TrackingNumber
            customerToCreate.CreatedOn = DateTime.Now;
            customerToCreate.CreatedBy = Environment.UserName;
            customerToCreate.UpdatedOn = DateTime.Now;
            customerToCreate.UpdatedBy = Environment.UserName;

            await dataContext.Customers.AddAsync(customerToCreate);
            await dataContext.SaveChangesAsync();

            // TODO with TrackingNumber add k-id+999
            return customerToCreate;
        }

        public virtual async Task<bool> AddCustomer(Customer customerToSave)
        {
            try
            {
                await dataContext.Customers.AddAsync(customerToSave);
                await dataContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual async Task<List<Customer>> GetCustomers()
        {
            return await dataContext.Customers.ToListAsync();
        }

        public virtual async Task<Customer> GetCustomerByTrackingNumber(string trackingNumber)
        {
            Customer customer = await dataContext.Customers.AsNoTracking().Include(c => c.Address)
                                                           .SingleOrDefaultAsync(c => c.TrackingNumber == trackingNumber);
            return customer;
        }

        public virtual async Task UpdateCustomer(Customer customerToUpdate)
        {
            customerToUpdate.UpdatedOn = DateTime.Now;
            customerToUpdate.UpdatedBy = Environment.UserName;

            var entry = dataContext.Customers.Attach(customerToUpdate);
            entry.State = EntityState.Modified;
            await dataContext.SaveChangesAsync();
        }

        public virtual async Task<bool> DeleteCustomer(string trackingNumber)
        {
            bool isDeleted;
            Customer customerToDelete = await GetCustomerByTrackingNumber(trackingNumber);

            if (customerToDelete != null)
            {
                try
                {
                    dataContext.Customers.Remove(customerToDelete);
                    SaveAsync();
                    isDeleted = true;
                }
                catch (Exception)
                {

                    isDeleted = false;
                }
            }
            else
            {
                isDeleted = false;
            }

            return isDeleted;
        }
        #region Helper methodes
        public async Task DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

        protected virtual async Task DisposeAsync(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    await dataContext.DisposeAsync();
                }
            }
            this.disposed = true;
        }

        public async Task SaveAsync()
        {
            await dataContext.SaveChangesAsync();
        }

        #endregion
    }
}
