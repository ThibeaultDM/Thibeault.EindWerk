using Microsoft.EntityFrameworkCore;
using Thibeault.EindWerk.DataLayer.Interfaces;
using Thibeault.EindWerk.Objects.DataObjects;

namespace Thibeault.EindWerk.DataLayer
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        public CustomerRepository(IDataContext dataContext) : base(dataContext)
        {
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

                customerToAdd = await Create(customerToAdd);

                if (customerToAdd.Id == 0)
                {
                    await CustomerTable().AddAsync(customerToAdd);
                    await SaveAsync();

                    // To prevent tracking bug
                    CustomerTable().Entry(customerToAdd).State = EntityState.Detached;

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
        /// cfr <see cref="ICustomerRepository.DeleteCustomerAsync(string)"/>
        /// </summary>

        public virtual async Task DeleteCustomerAsync(string trackingNumber)
        {
            Customer customerToDelete = await GetCustomerByTrackingNumberAsync(trackingNumber);

            try
            {
                CustomerTable().Remove(customerToDelete);
                await SaveAsync();
            }
            catch (Exception ex)
            {
                string errorMessage = $"{trackingNumber}-DeleteCustomerAsync-" + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        /// <summary>
        /// cfr <see cref="ICustomerRepository.GetCustomerByTrackingNumberAsync(string)"/>
        /// </summary>
        public virtual async Task<Customer> GetCustomerByTrackingNumberAsync(string trackingNumber)
        {
            try
            {
                // I don't use singleOrDefault for possible edge case of multiple invalid ("K0") customers existing
                Customer customer = await CustomerTable()
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
        public virtual async Task<List<Customer>> GetCustomersAsync()
        {
            try
            {
                return await CustomerTable().Include(c => c.Address).ToListAsync();
            }
            catch (Exception ex)
            {
                string errorMessage = "-GetCustomersAsync-" + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        /// <summary>
        /// cfr <see cref="ICustomerRepository.UpdateCustomerAsync(Customer)"/>
        /// </summary>
        public virtual async Task UpdateCustomerAsync(Customer customerToUpdate)
        {
            try
            {
                customerToUpdate = await Update(customerToUpdate);

                var entry = CustomerTable().Attach(customerToUpdate);
                entry.State = EntityState.Modified;
                await SaveAsync();
            }
            catch (Exception ex)
            {
                string errorMessage = $"{customerToUpdate.TrackingNumber}-UpdateCustomerAsync-" + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        #region Helper Methodes

        public DbSet<Customer> CustomerTable()
        {
            return dataContext.Customers;
        }

        public IQueryable<Customer> QueryCustomers()
        {
            IQueryable<Customer> queryBase = dataContext.Customers;

            return queryBase;
        }

        #endregion Helper Methodes
    }
}