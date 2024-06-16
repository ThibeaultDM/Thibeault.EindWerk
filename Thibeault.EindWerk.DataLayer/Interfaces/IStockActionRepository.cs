using Thibeault.EindWerk.Objects.DataObjects;

namespace Thibeault.EindWerk.DataLayer.Interfaces
{
    public interface IStockActionRepository
    {
        /// <summary>
        /// Adds the stock actions to the Product and updates the Product
        /// </summary>
        ///  <param name="stockAction"></param>
        /// <returns>An empty stockActionToUpdate whom you can update to assign values</returns>
        Task ProductAddNewStockActionAsync(Product product, StockAction stockAction);

        /// <summary>
        /// Adds the stock actions to the OrderHeader and updates the OrderHeader
        /// </summary>
        ///  <param name="stockAction"></param>
        /// <returns>An empty stockActionToUpdate whom you can update to assign values</returns>
        Task OrderHeaderAddNewStockActionAsync(OrderHeader orderHeader, StockAction stockAction);

        /// <summary>
        /// Deletes stockAction
        /// </summary>
        ///  <param name="stockAction"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task DeleteStockActionAsync(StockAction stockAction);

        /// <summary>
        /// Gets a StockAction by it's Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<StockAction> GetStockActionByIdAsync(int Id);

        /// <summary>
        /// Updates the UpdatedOn and UpdatedBy props as well
        /// </summary>
        /// <param name="stockActionToUpdate"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task UpdateStockAction(StockAction stockActionToUpdate);
    }
}