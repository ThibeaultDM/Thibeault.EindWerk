using Thibeault.EindWerk.DataLayer.Interfaces;
using Thibeault.EindWerk.Objects;

namespace Thibeault.EindWerk.DataLayer
{
    public abstract class BaseRepository
    {
        protected readonly IDataContext dataContext;

        protected BaseRepository(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        /// <summary>
        /// Updates the UpdatedOn and UpdatedBy property's
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toUpdate"></param>
        /// <returns></returns>
        public async Task<T> Update<T>(T toUpdate) where T : BaseObject
        {
            toUpdate.UpdatedOn = DateTime.Now;
            toUpdate.UpdatedBy = Environment.UserName;

            return toUpdate;
        }

        /// <summary>
        /// Updates the CreatedOn and CreatedBy property's
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toUpdate"></param>
        /// <returns></returns>
        public async Task<T> Create<T>(T toCreate) where T : BaseObject
        {
            toCreate.CreatedOn = DateTime.Now;
            toCreate.CreatedBy = Environment.UserName;

            return toCreate;
        }

        // TODO look up if I can make DisposeAsync private for DI-container
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