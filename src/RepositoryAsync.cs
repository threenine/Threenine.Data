using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Threenine.Data
{
  public class RepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public RepositoryAsync(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();


        }

        public async Task<T> SingleAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            
            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).FirstOrDefaultAsync();
            }
            else
            {
                return await query.FirstOrDefaultAsync();
            }
        }

        public IEnumerable<T> GetAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public void DeleteAsync(params T[] entities)
        {
            throw new NotImplementedException();
        }

        public void DeleteAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(params T[] entities)
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }
    }
}
