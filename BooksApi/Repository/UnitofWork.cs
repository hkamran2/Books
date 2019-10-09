using System;
using System.Threading.Tasks;
using BooksApi.Context;

namespace BooksApi.Repository
{
    public class UnitofWork : IUnitofWork
    {
        private readonly BooksContext _context;
        //Flag for dispose method
        private bool disposed = false;

        public UnitofWork(BooksContext context)
        {
            this._context = context;
        }

        public async Task<bool> SaveChangesAsync()
        {
            //Only return success if at least one row was changed
            var resp = await _context.SaveChangesAsync() > 0;
            return resp;
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(_context);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}