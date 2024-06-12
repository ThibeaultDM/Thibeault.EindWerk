using Microsoft.EntityFrameworkCore;
using Thibeault.EindWerk.DataLayer.Interfaces;
using Thibeault.EindWerk.Objects;

namespace Thibeault.EindWerk.DataLayer
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDataContext dataContext;

        public CustomerRepository(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        /// <summary>
        /// cfr <see cref="ICustomerRepository.CreateCustomerAsync"/>
        /// </summary>
        public virtual async Task<Customer> CreateCustomerAsync()
        {
            try
            {
                // should an invalid customer have been created it will have a tracking Id of "K0"
                // No need to have it keep taking up space
                Customer customerToAdd = await GetCustomerByTrackingNumberAsync("K0");

                // to create a customer I need a unique Id from which I generate a unique tracking number.
                // I let the database deal with making sure it's unique
                customerToAdd.TrackingNumber = "K0";

                customerToAdd.CreatedOn = DateTime.Now;
                customerToAdd.CreatedBy = Environment.UserName;

                if (customerToAdd.Id == 0)
                {
                    // don't make async creates a tracking bug
                    dataContext.Customers.Add(customerToAdd);
                    dataContext.SaveChanges();

                    customerToAdd = await GetCustomerByTrackingNumberAsync("K0");
                }

                return customerToAdd;
            }
            catch (Exception ex)
            {
                string errorMessage = "-CreateCustomerAsync-" + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        /// <summary>
        /// cfr <see cref="ICustomerRepository.GetCustomersAsync"/>
        /// </summary>
        public virtual async Task<List<Customer>> GetCustomersAsync()
        {
            try
            {
                return await dataContext.Customers.AsNoTracking().Include(c => c.Address).ToListAsync();
            }
            catch (Exception ex)
            {
                string errorMessage = "-GetCustomersAsync-" + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public virtual async Task<Customer> GetCustomerByTrackingNumberAsync(string trackingNumber)
        {
            try
            {
                // I don't use singleOrDefault for possible edge case of multiple invalid ("K0") customers existing
                Customer customer = await dataContext.Customers
                                                     .AsNoTracking().Include(c => c.Address)
                                                     .FirstOrDefaultAsync(c => c.TrackingNumber == trackingNumber)
                                                     ?? (trackingNumber == "K0" ? new() : throw new Exception("Customer not found"));
                return customer;
            }
            catch (Exception ex)
            {
                string errorMessage = $"{trackingNumber}-GetCustomerByTrackingNumberAsync-" + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        /// <summary>
        /// cfr <see cref="ICustomerRepository.GetCustomersAsync"/>
        /// </summary>
        public virtual async Task UpdateCustomer(Customer customerToUpdate)
        {
            try
            {
                customerToUpdate.UpdatedOn = DateTime.Now;
                customerToUpdate.UpdatedBy = Environment.UserName;

                var entry = dataContext.Customers.Attach(customerToUpdate);
                entry.State = EntityState.Modified;
                await SaveAsync();
            }
            catch (Exception ex)
            {
                string errorMessage = $"{customerToUpdate.TrackingNumber}-UpdateCustomer-" + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public virtual async Task<bool> DeleteCustomerAsync(string trackingNumber)
        {
            bool isDeleted;

            Customer customerToDelete = await GetCustomerByTrackingNumberAsync(trackingNumber);

            try
            {
                dataContext.Customers.Remove(customerToDelete);
                await SaveAsync();

                isDeleted = true;
            }
            catch (Exception ex)
            {
                string errorMessage = $"{trackingNumber}-DeleteCustomerAsync-" + ex.Message;
                isDeleted = false;
                throw new Exception(errorMessage);
            }

            return isDeleted;
        }

        #region Helper methodes

        // todo look up if I can make DisposeAsync private for DI-container
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

        private async Task SaveAsync()
        {
            await dataContext.SaveChangesAsync();
        }

        #endregion Helper methodes
    }
}