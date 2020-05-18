using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Threenine.Data
{
    public class Repository<T> : BaseRepository<T>, IRepository<T> where T : class
    {
        public Repository(DbContext context) : base(context)
        {
        }

        #region Get Functions
        public T GetSingleOrDefault(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool enableTracking = true,
            bool ignoreQueryFilters = false)
        {
            IQueryable<T> query = _dbSet;

            if (!enableTracking)
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

            if (ignoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }

            return orderBy != null ? orderBy(query).FirstOrDefault() : query.FirstOrDefault();
        }
        
        
        
      
        #endregion
        
        #region Insert Functions

       

        public virtual  T  Insert(T entity)
        {
           return _dbSet.Add(entity).Entity;
        }

        public void Insert(params T[] entities) => _dbSet.AddRange(entities);
        
        public void Insert(IEnumerable<T> entities) => _dbSet.AddRange(entities);
        

        #endregion


        #region Delete Functions
        
        public void Delete(T entity)
        {
            var existing = _dbSet.Find(entity);
            if (existing != null) _dbSet.Remove(existing);
        }


        public void Delete(object id)
        {
            var typeInfo = typeof(T).GetTypeInfo();
            var key = _dbContext.Model.FindEntityType(typeInfo).FindPrimaryKey().Properties.FirstOrDefault();
            var property = typeInfo.GetProperty(key?.Name);
            if (property != null)
            {
                var entity = Activator.CreateInstance<T>();
                property.SetValue(entity, id);
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }
            else
            {
                var entity = _dbSet.Find(id);
                if (entity != null) Delete(entity);
            }
        }

        public void Delete(params T[] entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Delete(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        

        #endregion
       

        #region  Update Functions
       

       public void Update(T entity)
       {
           _dbSet.Update(entity);
           /*_dbSet.Attach(entity);
           _dbContext.Entry(entity).State = EntityState.Modified;*/

       }

        public void Update(params T[] entities) => _dbSet.UpdateRange(entities);

        public void Update(IEnumerable<T> entities) => _dbSet.UpdateRange(entities);
        
        
        #endregion

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}