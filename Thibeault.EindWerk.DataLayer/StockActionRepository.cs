using Microsoft.EntityFrameworkCore;
using Thibeault.EindWerk.DataLayer.Interfaces;
using Thibeault.EindWerk.Objects.DataObjects;

namespace Thibeault.EindWerk.DataLayer
{
    public class StockActionRepository : BaseRepository, IStockActionRepository
    {
        private readonly IProductRepository productRepository;

        public StockActionRepository(IDataContext dataContext, IProductRepository productRepository) : base(dataContext)
        {
            this.productRepository = productRepository;
        }

        /// <summary>
        /// cfr <see cref="IStockActionRepository.AddNewStockActionAsync"/>
        /// </summary>
        public virtual async Task AddNewStockActionAsync(Product product, StockAction stockAction)
        {
            try
            {
                await Create(stockAction);

                product.StockActions.Add(stockAction);

                await productRepository.UpdateProductAsync(product);
            }
            catch (Exception ex)
            {
                string errorMessage = "-AddNewStockActionAsync-" + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        /// <summary>
        /// cfr <see cref="IStockActionRepository.DeleteStockActionAsync(StockAction)"/>
        /// </summary>

        public virtual async Task DeleteStockActionAsync(StockAction stockAction)
        {
            try
            {
                ProductTable().Remove(stockAction);
                await SaveAsync();
            }
            catch (Exception ex)
            {
                string errorMessage = $"{stockAction}-DeleteStockActionAsync-" + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        /// <summary>
        /// cfr <see cref="IStockActionRepository.UpdateStockAction(StockAction)"/>
        /// </summary>
        public virtual async Task UpdateStockAction(StockAction stockActionToUpdate)
        {
            try
            {
                stockActionToUpdate = await Update(stockActionToUpdate);

                var entry = ProductTable().Attach(stockActionToUpdate);
                entry.State = EntityState.Modified;
                await SaveAsync();
            }
            catch (Exception ex)
            {
                string errorMessage = $"{stockActionToUpdate}-UpdateStockAction-" + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        #region Helper Methodes

        private DbSet<StockAction> ProductTable()
        {
            return dataContext.StockActions;
        }

        #endregion Helper Methodes
    }
}