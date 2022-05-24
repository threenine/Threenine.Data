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
using System.Linq.Expressions;

namespace Threenine.Data
{
    public interface IRepository<T> : IReadRepository<T>, IDisposable where T : class
    {

        T Insert(T entity);
        void Insert(params T[] entities);
        void Insert(IEnumerable<T> entities);

        T InsertNotExists(Expression<Func<T, bool>> predicate, T entity);

        void Update(T entity);
        void Update(params T[] entities);
        void Update(IEnumerable<T> entities);

        void Delete(T entity);

        void Delete(params T[] entities);

        void Delete(IEnumerable<T> entities);
    }
}