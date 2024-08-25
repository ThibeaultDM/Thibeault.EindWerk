using Microsoft.EntityFrameworkCore;
using Thibeault.Example.DataLayer.Interfaces;
using Thibeault.Example.Objects.DataObjects;

namespace Thibeault.Example.DataLayer
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
        /// cfr <see cref="IStockActionRepository.ProductAddNewStockActionAsync(Product, StockAction)"/>
        /// </summary>
        public virtual async Task ProductAddNewStockActionAsync(Product product, StockAction stockAction)
        {
            try
            {
                stockAction.Id = 0;
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
                stockAction.Id = 0;
                await Create(stockAction);
                orderHeader.StockActions.Add(stockAction);
                await StockActionTable().AddAsync(stockAction);
                await SaveAsync();

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
                StockActionTable().Remove(stockAction);
                await SaveAsync();
            }
            catch (Exception ex)
            {
                string errorMessage = $"{stockAction}-DeleteStockActionAsync-" + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        /// <summary>
        /// cfr <see cref="IStockActionRepository.GetStockActionByIdAsync(int)"/>
        /// </summary>
        public virtual async Task<StockAction> GetStockActionByIdAsync(int Id)
        {
            try
            {
                // I don't use singleOrDefault for possible edge case of multiple invalid (0) products existing
                StockAction stockAction = await StockActionTable().AsNoTracking()
                                                     .SingleOrDefaultAsync(s => s.Id == Id)
                                                     ?? throw new Exception("StockAction not found");

                StockActionTable().Entry(stockAction).State = EntityState.Detached;

                return stockAction;
            }
            catch (Exception ex)
            {
                string errorMessage = $"{Id}-GetStockActionByIdAsync-" + ex.Message;
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

                var entry = StockActionTable().Attach(stockActionToUpdate);
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

        private DbSet<StockAction> StockActionTable()
        {
            return dataContext.StockActions;
        }

        #endregion Helper Methodes
    }
}