using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;


namespace Threenine.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }
        public void Add(T entity)
        {
         var entry = _dbSet.Add(entity);
            
        }
 
        public void Delete(T entity)
        {
            T existing = _dbSet.Find(entity);
            if (existing != null) _dbSet.Remove(existing);
        }
 
        public IEnumerable<T> Get()
        {
            return _dbSet.AsEnumerable<T>();
        }
 
        public IEnumerable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsEnumerable<T>();
        }
 
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Update(params T[] entities) => _dbSet.UpdateRange(entities);


        public void Update(IEnumerable<T> entities) => _dbSet.UpdateRange(entities);

    }
}