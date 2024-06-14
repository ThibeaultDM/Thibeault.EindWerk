using Thibeault.EindWerk.Objects.DataObjects;

namespace Thibeault.EindWerk.DataLayer.Interfaces
{
    public interface IProductRepository
    {
        /// <summary>
        /// Creates a product in the database with serialNumber "0" and returns an empty product who you can update.
        /// Use it to ensure no duplicate serialNumbers in the database.
        /// </summary>
        /// <returns>An empty product whom you can update to assign values</returns>
        Task<Product> AddNewProductAsync();

        /// <summary>
        /// Deletes a Product
        /// </summary>
        ///  <param name="serialNumber"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task DeleteProductAsync(int serialNumber);

        /// <summary>
        /// Gets a product
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

        /// <summary>
        /// To allow queries to be sent for products
        /// </summary>
        /// <returns></returns>
        IQueryable<Product> QueryProducts();
    }
}