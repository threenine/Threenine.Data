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
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Threenine.Data.Paging;

namespace Threenine.Data
{
    public class RepositoryReadOnlyAsync<T> : RepositoryAsync<T>, IRepositoryReadOnlyAsync<T> where T : class
    {
        public RepositoryReadOnlyAsync(DbContext context) : base(context)
        {
        }

        /*public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>,
            IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>,
            IIncludableQueryable<T, object>> include = null)
        {
            return await base.SingleOrDefaultAsync(predicate, orderBy, include, false);
        }*/

        public async Task<IPaginate<T>> GetListAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int index = 0,
            int size = 20)
        {
            return await base.GetListAsync(predicate, orderBy, include, index, size, false);
        }

        public async Task<IPaginate<TResult>> GetListAsync<TResult>(Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int index = 0,
            int size = 20,
            CancellationToken cancellationToken = default,
            bool ignoreQueryFilters = false) where TResult : class
        {
            return await base.GetListAsync(selector, predicate, orderBy, include, index, size, false, cancellationToken,
                ignoreQueryFilters);
        }
    }
}