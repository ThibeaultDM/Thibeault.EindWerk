using Thibeault.EindWerk.Objects.DataObjects;

namespace Thibeault.EindWerk.DataLayer.Interfaces
{
    public interface IStockActionRepository
    {
        /// <summary>
        /// Creates a stockActionToUpdate in the database with serialNumber "0" and returns an empty stockAction ToUpdate who you can update.
        /// Use it to ensure no duplicate serialNumbers in the database.
        /// </summary>
        ///  <param name="stockAction"></param>
        /// <returns>An empty stockActionToUpdate whom you can update to assign values</returns>
        Task AddNewStockActionAsync(StockAction stockAction);

        /// <summary>
        /// Deletes stockAction
        /// </summary>
        ///  <param name="stockAction"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task DeleteStockActionAsync(StockAction stockAction);

        /// <summary>
        /// Updates the UpdatedOn and UpdatedBy props as well
        /// </summary>
        /// <param name="stockActionToUpdate"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task UpdateStockAction(StockAction stockActionToUpdate);
    }
}