using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thibeault.EindWerk.DataLayer.Interfaces;
using Thibeault.EindWerk.Objects.DataObjects;

namespace Thibeault.EindWerk.DataLayer
{
    public class StockActionRepository : BaseRepository, IStockActionRepository
    {
        public StockActionRepository(IDataContext dataContext) : base(dataContext)
        {
        }

        /// <summary>
        /// cfr <see cref="IStockActionRepository.AddNewStockActionAsync"/>
        /// </summary>
        public virtual async Task AddNewStockActionAsync(StockAction stockAction)
        {
            try
            {
                await ProductTable().AddAsync(stockAction);
                await SaveAsync();
            }
            catch (Exception ex)
            {
                string errorMessage = "-AddNewProductAsync-" + ex.Message;
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
        /// cfr <see cref="IStockActionRepository.UpdateStockAction(StockAction)(StockAction)"/>
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
