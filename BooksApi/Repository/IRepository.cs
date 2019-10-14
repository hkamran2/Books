using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BooksApi.Repository
{
    public interface IRepository<T>
    {
        IQueryable<T> Get();
        IQueryable<T> Get(Expression<Func<T, bool>> filter);
        Task<T> FindAsync(Expression<Func<T, bool>> filter);
        void Add(T obj);
        void Delete(T obj);
    }
}
