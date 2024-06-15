using Microsoft.EntityFrameworkCore;
using Thibeault.EindWerk.DataLayer.Interfaces;
using Thibeault.EindWerk.Objects.DataObjects;

namespace Thibeault.EindWerk.DataLayer
{
    public class StockActionRepository : BaseRepository, IStockActionRepository
    {
        private readonly IProductRepository productRepository;
        private readonly IOrderHeaderRepository orderRepository;

        public StockActionRepository(IDataContext dataContext, IProductRepository productRepository, IOrderHeaderRepository orderRepository) : base(dataContext)
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
        }

        /// <summary>
        /// cfr <see cref="IStockActionRepository.ProductAddNewStockActionsAsync"/>
        /// </summary>
        public virtual async Task ProductAddNewStockActionAsync(Product product, StockAction stockAction)
        {
            try
            {
                await Create(stockAction);
                product.StockActions.Add(stockAction);

                await productRepository.UpdateProductAsync(product);
            }
            catch (Exception ex)
            {
                string errorMessage = "-ProductAddNewStockActionsAsync-" + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        /// <summary>
        /// cfr <see cref="IStockActionRepository.OrderHeaderAddNewStockActionAsync"/>
        /// </summary>
        public virtual async Task OrderHeaderAddNewStockActionAsync(OrderHeader orderHeader, StockAction stockAction)
        {
            try
            {
                await Create(stockAction);
                orderHeader.StockActions.Add(stockAction);

                await orderRepository.UpdateOrderHeaderAsync(orderHeader);
            }
            catch (Exception ex)
            {
                string errorMessage = "-OrderHeaderAddNewStockActionAsync-" + ex.Message;
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