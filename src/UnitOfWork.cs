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
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Threenine.Data
{
    public class UnitOfWork<TContext> : IRepositoryFactory, IUnitOfWork<TContext>
        where TContext : DbContext, IDisposable
    {
        private Dictionary<(Type type, string name), object> _repositories;

        public UnitOfWork(TContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {

            return (IRepository<TEntity>) GetOrAddRepository(typeof(TEntity), new Repository<TEntity>(Context));
        }


        public IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : class
        {
           return (IRepositoryAsync<TEntity>) GetOrAddRepository( typeof(TEntity),  new RepositoryAsync<TEntity>(Context));
        }

        public IRepositoryReadOnly<TEntity> GetReadOnlyRepository<TEntity>() where TEntity : class
        {
           return (IRepositoryReadOnly<TEntity>)GetOrAddRepository( typeof(TEntity),  new RepositoryReadOnly<TEntity>(Context));
        }

        public IRepositoryReadOnlyAsync<TEntity> GetReadOnlyRepositoryAsync<TEntity>() where TEntity : class
        {
            return (IRepositoryReadOnlyAsync<TEntity>) GetOrAddRepository( typeof(TEntity), new RepositoryReadOnlyAsync<TEntity>(Context));
        }

        private object GetOrAddRepository(Type type, object repo)
        {
            if (_repositories == null)
            {
               _repositories = new Dictionary<(Type type, string Name), object>();
            }
            
            if (!_repositories.TryGetValue((type, repo.GetType().FullName), out var repository))
            {
                _repositories.Add((type, repo.GetType().FullName), repo);
               
            }

            return repo;
        }
        public TContext Context { get; }

        public int Commit(bool autoHistory = false)
        {
            if (autoHistory) Context.EnsureAutoHistory();
            return Context.SaveChanges();
        }

        public async Task<int> CommitAsync(bool autoHistory = false)
        {
            if (autoHistory) Context.EnsureAutoHistory();

            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}