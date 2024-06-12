using Microsoft.EntityFrameworkCore;
using Thibeault.EindWerk.DataLayer.Interfaces;
using Thibeault.EindWerk.Objects;

namespace Thibeault.EindWerk.DataLayer
{
    public class BaseRepository
    {
        protected readonly IDataContext dataContext;

        protected BaseRepository(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        //public virtual async Task UpdateCustomer<T>(T toUpdate) where T : BaseObject
        //{
        //        toUpdate.UpdatedOn = DateTime.Now;
        //        toUpdate.UpdatedBy = Environment.UserName;

        //}

        // todo look up if I can make DisposeAsync private for DI-container
        public async Task DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

        protected virtual async Task DisposeAsync(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    await dataContext.DisposeAsync();
                }
            }
            this.disposed = true;
        }

        protected async Task SaveAsync()
        {
            await dataContext.SaveChangesAsync();
        }

    }
}