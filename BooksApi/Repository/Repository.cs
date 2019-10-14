using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BooksApi.Context;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private BooksContext _context;
        private readonly DbSet<T> _set;

        public Repository(BooksContext context)
        {
            _context = context;
            this._set = _context.Set<T>();
        }
        public void Add(T obj)
        {
            _set.Add(obj);
        }

        public void Delete(T obj)
        {
            _set.Remove(obj);
        }
        /**
         * Allow the classes to override the functionality
         */
        public IQueryable<T> Get()
        {
            return _set.AsQueryable();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> filter)
        {
            return _set.Where(filter);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> filter)
        {
            return await _set.FirstOrDefaultAsync(filter);
        }

        public void Remove(T obj)
        {
            _set.Remove(obj);
        }
    }
}
