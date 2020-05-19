using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Threenine.Data
{
    public interface IRepository<T> : IReadRepository<T>, IDisposable where T : class
    {
        T GetSingleOrDefault(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool enableTracking = true,
            bool ignoreQueryFilters = false);

        T Insert(T entity);
        void Insert(params T[] entities);
        void Insert(IEnumerable<T> entities);

        void Update(T entity);
        void Update(params T[] entities);
        void Update(IEnumerable<T> entities);
    }
}