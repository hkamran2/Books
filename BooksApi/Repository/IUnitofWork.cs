using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksApi.Repository
{
    public interface IUnitofWork : IDisposable
    {
        Task<bool> SaveChangesAsync();
        IRepository<T> GetRepository<T>() where T : class;
    }

}
