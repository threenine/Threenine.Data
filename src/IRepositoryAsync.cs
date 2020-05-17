using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using Threenine.Data.Paging;

namespace Threenine.Data
{
    public interface IRepositoryAsync<T> where T : class
    {
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false);

        Task<IPaginate<T>> GetListAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int index = 0,
            int size = 20,
            bool disableTracking = true,
            CancellationToken cancellationToken = default);


        ValueTask<EntityEntry<T>> InsertAsync(T entity,
            CancellationToken cancellationToken = default);

        Task InsertAsync(params T[] entities);

        Task InsertAsync(IEnumerable<T> entities,
            CancellationToken cancellationToken = default);

    }
}