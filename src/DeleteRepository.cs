using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Threenine.Data
{
    public class DeleteRepository<T> : IDeleteRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public DeleteRepository(DbContext context)
        {
            var dbContext = context ?? throw new ArgumentException(nameof(context));
            _dbSet = dbContext.Set<T>();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Delete(params T[] entities)
        {
            _dbSet.RemoveRange(entities);
        }


        public void Delete(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}