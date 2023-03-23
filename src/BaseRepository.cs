/* Copyright (c) threenine.co.uk . All rights reserved.
 
   GNU GENERAL PUBLIC LICENSE  Version 3, 29 June 2007
   This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Threenine.Data.Paging;

namespace Threenine.Data
{
    public abstract class BaseRepository<T> : IReadRepository<T> where T : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(DbContext context)
        {
            _dbContext = context ?? throw new ArgumentException(nameof(context));
            _dbSet = _dbContext.Set<T>();
        }
        
        #region SingleOrDefault
        public T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return SingleOrDefault(predicate, default);
        }

        public T SingleOrDefault(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            return SingleOrDefault(predicate, orderBy, default);
        }

        public T SingleOrDefault(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
        {
            return SingleOrDefault(predicate, orderBy, include, default);
        }

        public T SingleOrDefault(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include, bool enableTracking)
        {
           return SingleOrDefault(predicate, orderBy, include, enableTracking, default);
        }

        public T SingleOrDefault(Expression<Func<T, bool>> predicate ,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include , bool enableTracking ,
            bool ignoreQueryFilters)
        {
            IQueryable<T> query = _dbSet;

            if (!enableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (ignoreQueryFilters) query = query.IgnoreQueryFilters();

            return orderBy != null ? orderBy(query).FirstOrDefault() : query.FirstOrDefault();
        }

        public IPaginate<T> GetList(Expression<Func<T, bool>> predicate)
        {
            return GetList(predicate, default);
        }

        public IPaginate<T> GetList(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            return GetList(predicate, orderBy, default);
        }

        public IPaginate<T> GetList(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
        {
            return GetList(predicate, orderBy, include, default);
        }

        public IPaginate<T> GetList(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, Func<IQueryable<T>, IIncludableQueryable<T, object>> include, int index, int size)
        {
            return GetList(predicate, orderBy, include, index, size, default);
        }

        #endregion

        public IPaginate<T> GetList(Expression<Func<T, bool>> predicate = null,
            int size = 20, bool enableTracking = true)
        {
            return GetList(predicate, 0, size, enableTracking);
        }

        public IPaginate<T> GetList(Expression<Func<T, bool>> predicate = null, int index = 0,
            int size = 20, bool enableTracking = true)
        {
            return GetList(predicate, null, index, size, enableTracking);
        }

        public IPaginate<T> GetList(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, int index = 0,
            int size = 20, bool enableTracking = true)
        {
            return GetList(predicate, null, include, index, size, enableTracking);
        }

        public IPaginate<T> GetList(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, int index = 0,
            int size = 20, bool enableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (!enableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            return orderBy != null ? orderBy(query).ToPaginate(index, size) : query.ToPaginate(index, size);
        }
        
        
        public TResult SingleOrDefault<TResult>(Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false)
        {
            IQueryable<T> query = _dbSet;

            if (disableTracking) query = query.AsNoTracking();
            

            if (include != null) query = include(query);
            

            if (predicate != null)  query = query.Where(predicate);
            

            if (ignoreQueryFilters) query = query.IgnoreQueryFilters();
            
            return orderBy != null ? orderBy(query).Select(selector).SingleOrDefault() : query.Select(selector).SingleOrDefault();
        }
        

        public IPaginate<TResult> GetList<TResult>(Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int index = 0, int size = 20, bool enableTracking = true) where TResult : class
        {
            IQueryable<T> query = _dbSet;
            if (!enableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            return orderBy != null
                ? orderBy(query).Select(selector).ToPaginate(index, size)
                : query.Select(selector).ToPaginate(index, size);
        }
    }
}