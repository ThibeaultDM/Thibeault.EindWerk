using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thibeault.EindWerk.Objects.DataObjects;

namespace Thibeault.EindWerk.DataLayer.Interfaces
{
    public interface IOrderHeaderRepository
    {
        /// <summary>
        /// Creates an OrderHeader in the database with trackingNumber "0" and returns an empty OrderHeader who you can update.
        /// Use it to ensure no duplicate trackingNumber in the database.
        /// </summary>
        /// <returns>An empty OrderHeader whom you can update to assign values</returns>
        Task<OrderHeader> CreateOrderHeaderAsync();

        /// <summary>
        /// Deletes an OrderHeader
        /// </summary>
        ///  <param name="trackingNumber"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task DeleteOrderHeaderAsync(int trackingNumber);

        /// <summary>
        /// Gets a OrderHeader
        /// </summary>
        ///  <param name="trackingNumber"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task<OrderHeader> GetOrderHeaderByTrackingNumberAsync(int trackingNumber);

        Task<List<OrderHeader>> GetOrderHeadersAsync();

        /// <summary>
        /// Updates the UpdatedOn and UpdatedBy props as well
        /// </summary>
        /// <param name="orderHeaderToUpdate"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task UpdateOrderHeaderAsync(OrderHeader orderHeaderToUpdate);


    }
}
