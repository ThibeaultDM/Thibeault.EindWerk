using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thibeault.EindWerk.Objects.DataObjects;

namespace Thibeault.EindWerk.DataLayer.Interfaces
{
    internal interface IOrderHeaderRepository
    {
        /// <summary>
        /// Creates an OrderHeader in the database with serialNumber "0" and returns an empty product who you can update.
        /// Use it to ensure no duplicate serialNumbers in the database.
        /// </summary>
        /// <returns>An empty product whom you can update to assign values</returns>
        Task<Product> CreateOrderHeaderAsync();

        /// <summary>
        /// Deletes a Product
        /// </summary>
        ///  <param name="serialNumber"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task DeleteOrderHeaderAsync(int serialNumber);

        /// <summary>
        /// Gets a OrderHeader
        /// </summary>
        ///  <param name="serialNumber"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task<Product> GetProductBySerialNumberAsync(int serialNumber);

        Task<List<Product>> GetProductsAsync();

        /// <summary>
        /// Updates the UpdatedOn and UpdatedBy props as well
        /// </summary>
        /// <param name="productToUpdate"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task UpdateProductAsync(Product productToUpdate);


    }
}
