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
     Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate,   Func<IQueryable<T>, IOrderedQueryable<T>> orderBy);
        
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate,   Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,  Func<IQueryable<T>, IIncludableQueryable<T, object>> include );
        
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate,   Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,  Func<IQueryable<T>, IIncludableQueryable<T, object>> include, bool enableTracking);
        
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool enableTracking = true,
            bool ignoreQueryFilters = false);

        Task<IPaginate<T>> GetListAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int index = 0,
            int size = 20,
            bool enableTracking = true,
            CancellationToken cancellationToken = default);

        #region Insert Functions

        ValueTask<EntityEntry<T>> InsertAsync(T entity,
            CancellationToken cancellationToken = default);

        Task InsertAsync(params T[] entities);

        Task InsertAsync(IEnumerable<T> entities,
            CancellationToken cancellationToken = default);

        #endregion
    }
}