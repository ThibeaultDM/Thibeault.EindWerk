using Thibeault.EindWerk.Objects;

namespace Thibeault.EindWerk.DataLayer.Interfaces
{
    public interface IStockActionRepository
    {
        /// <summary>
        /// Creates a stockActionToUpdate in the database with serialNumber "0" and returns an empty stockAction ToUpdate who you can update.
        /// Use it to ensure no duplicate serialNumbers in the database.
        /// </summary>
        /// <returns>An empty stockActionToUpdate whom you can update to assign values</returns>
        Task AddNewStockActionAsync(StockAction stockAction);

        /// <summary>
        /// Deletes stockActionToUpdate
        /// </summary>
        ///  <param name="serialNumber"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task DeleteStockActionAsync(int serialNumber);

        /// <summary>
        /// Deletes stockActionToUpdate
        /// </summary>
        ///  <param name="serialNumber"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task<StockAction> GetStockActionBySerialNumberAsync(int serialNumber);
        Task<List<StockAction>> GetStockActionsAsync();

        /// <summary>
        /// Updates the UpdatedOn and UpdatedBy props as well
        /// </summary>
        /// <param name="stockActionToUpdate"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task UpdateStockAction(StockAction stockActionToUpdate);
    }
}