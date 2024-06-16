using Microsoft.EntityFrameworkCore;
using Thibeault.EindWerk.DataLayer.Interfaces;
using Thibeault.EindWerk.Objects.DataObjects;

namespace Thibeault.EindWerk.DataLayer
{
    public class OrderHeaderRepository : BaseRepository, IOrderHeaderRepository
    {
        private readonly ICustomerRepository customerRepository;

        public OrderHeaderRepository(IDataContext dataContext, ICustomerRepository customerRepository) : base(dataContext)
        {
            this.customerRepository = customerRepository;
        }

        /// <summary>
        /// cfr <see cref="IOrderHeaderRepository.CreateOrderHeaderAsync"/>
        /// </summary>
        public virtual async Task<OrderHeader> CreateOrderHeaderAsync(Customer customer)
        {
            try
            {
                // update customer with a valid OrderHeader look at stockAction,
                // may first need to add the actions to the products and then construct a OrderHeader with it

                // should an invalid OrderHeader have been created it will have a serial number of 0
                // No need to have it keep taking up space
                OrderHeader orderHeaderToCreate = await GetOrderHeaderByTrackingNumberAsync(0);

                // to create an OrderHeader I need a unique Id from which I generate a unique serial number.
                // I let the database deal with making sure it's unique
                orderHeaderToCreate.TrackingNumber = 0;

                orderHeaderToCreate = await Create(orderHeaderToCreate);
                orderHeaderToCreate.Customer = customer;


                if (orderHeaderToCreate.Id == 0)
                {
                    customerRepository.CustomerTable().Attach(customer);
                    await OrderHeaderTable().AddAsync(orderHeaderToCreate);


                    await SaveAsync();

                    // To prevent tracking bug
                    OrderHeaderTable().Entry(orderHeaderToCreate).State = EntityState.Detached;

                    orderHeaderToCreate = await GetOrderHeaderByTrackingNumberAsync(0);
                }

                return orderHeaderToCreate;
            }
            catch (Exception ex)
            {
                string errorMessage = "-CreateOrderHeaderAsync-" + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        /// <summary>
        /// cfr <see cref="IOrderHeaderRepository.DeleteOrderHeaderAsync(int)"/>
        /// </summary>
        public virtual async Task DeleteOrderHeaderAsync(int trackingNumber)
        {
            OrderHeader orderHeaderToDelete = await GetOrderHeaderByTrackingNumberAsync(trackingNumber);

            try
            {
                OrderHeaderTable().Remove(orderHeaderToDelete);
                await SaveAsync();
            }
            catch (Exception ex)
            {
                string errorMessage = $"{trackingNumber}-DeleteOrderHeaderAsync-" + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        /// <summary>
        /// cfr <see cref="IOrderHeaderRepository.GetOrderHeadersAsync"/>
        /// </summary>
        public virtual async Task<List<OrderHeader>> GetOrderHeadersAsync()
        {
            try
            {
                return await OrderHeaderTable().Include(c => c.StockActions).ToListAsync();
            }
            catch (Exception ex)
            {
                string errorMessage = "-GetOrderHeadersAsync-" + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        /// <summary>
        /// cfr <see cref="IOrderHeaderRepository.GetOrderHeaderByTrackingNumberAsync(int)"/>
        /// </summary>
        public virtual async Task<OrderHeader> GetOrderHeaderByTrackingNumberAsync(int trackingNumber)
        {
            try
            {
                // I don't use singleOrDefault for possible edge case of multiple invalid (0) OrderHeaders existing
                OrderHeader orderHeader = await OrderHeaderTable()
                                                     .AsNoTracking().Include(c => c.StockActions)
                                                     .FirstOrDefaultAsync(c => c.TrackingNumber == trackingNumber)
                                                     ?? (trackingNumber == 0 ? new() : throw new Exception("OrderHeader not found"));
                return orderHeader;
            }
            catch (Exception ex)
            {
                string errorMessage = $"{trackingNumber}-GetOrderHeaderByTrackingNumberAsync-" + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        /// <summary>
        /// cfr <see cref="IOrderHeaderRepository.UpdateOrderHeaderAsync(OrderHeader)"/>
        /// </summary>
        public virtual async Task UpdateOrderHeaderAsync(OrderHeader orderHeaderToUpdate)
        {
            try
            {
                orderHeaderToUpdate = await Update(orderHeaderToUpdate);

                var entry = OrderHeaderTable().Attach(orderHeaderToUpdate);
                entry.State = EntityState.Modified;
                await SaveAsync();
            }
            catch (Exception ex)
            {
                string errorMessage = $"{orderHeaderToUpdate.TrackingNumber}-UpdateOrderHeaderAsync-" + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        #region Helper Methodes

        private DbSet<OrderHeader> OrderHeaderTable()
        {
            return dataContext.OrderHeaders;
        }

        #endregion Helper Methodes
    }
}