using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thibeault.EindWerk.DataLayer.Interfaces;
using Thibeault.EindWerk.Objects;

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
        public virtual async Task<StockAction> AddNewStockActionAsync(StockAction stockAction)
        {
            try
            {
                // should an invalid product have been created it will have a tracking Id of "K0"
                // No need to have it keep taking up space
                StockAction productToAdd = await GetProductBySerialNumberAsync(0);

                // to create a product I need a unique Id from which I generate a unique serial number.
                // I let the database deal with making sure it's unique
                productToAdd.SerialNumber = 0;

                productToAdd = await Create(productToAdd);

                if (productToAdd.Id == 0)
                {
                    await ProductTable().AddAsync(productToAdd);
                    await SaveAsync();

                    // To prevent tracking bug
                    ProductTable().Entry(productToAdd).State = EntityState.Detached;

                    productToAdd = await GetProductBySerialNumberAsync(0);
                }

                return productToAdd;
            }
            catch (Exception ex)
            {
                string errorMessage = "-AddNewProductAsync-" + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        /// <summary>
        /// cfr <see cref="IProductRepository.DeleteProductAsync(int)"/>
        /// </summary>

        public virtual async Task DeleteProductAsync(int serialNumber)
        {
            StockAction productToDelete = await GetProductBySerialNumberAsync(serialNumber);

            try
            {
                ProductTable().Remove(productToDelete);
                await SaveAsync();
            }
            catch (Exception ex)
            {
                string errorMessage = $"{serialNumber}-DeleteProductAsync-" + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        /// <summary>
        /// cfr <see cref="IProductRepository.GetProductBySerialNumberAsync(int)"/>
        /// </summary>
        public virtual async Task<StockAction> GetProductBySerialNumberAsync(int serialNumber)
        {
            try
            {
                // I don't use singleOrDefault for possible edge case of multiple invalid (0) products existing
                StockAction customer = await ProductTable()
                                                     .AsNoTracking().Include(c => c.StockActions)
                                                     .FirstOrDefaultAsync(c => c.SerialNumber == serialNumber)
                                                     ?? (serialNumber == 0 ? new() : throw new Exception("Product not found"));
                return customer;
            }
            catch (Exception ex)
            {
                string errorMessage = $"{serialNumber}-GetProductBySerialNumberAsync-" + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        /// <summary>
        /// cfr <see cref="IProductRepository.UpdateProduct(StockAction)"/>
        /// </summary>
        public virtual async Task UpdateProduct(StockAction productToUpdate)
        {
            try
            {
                productToUpdate = await Update(productToUpdate);

                var entry = ProductTable().Attach(productToUpdate);
                entry.State = EntityState.Modified;
                await SaveAsync();
            }
            catch (Exception ex)
            {
                string errorMessage = $"{productToUpdate.SerialNumber}-UpdateProduct-" + ex.Message;
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
