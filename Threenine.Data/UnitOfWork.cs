
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Threenine.Data
{
    public class UnitOfWork<TContext>  : IRepositoryFactory, IUnitOfWork<TContext>, IUnitOfWork where TContext:DbContext
    {
        private readonly TContext _context;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(TContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TContext Context => _context;

        public IRepository<T> GetRepository<T>() where T : class 
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }

            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type]= new Repository<T>(_context);
            }
            return (IRepository<T>) _repositories[type];
        }

        public int SaveChanges()
        {
           return _context.SaveChanges();
        }

        public void Dispose()
        {
           Context?.Dispose();
        }

        IRepository<TEntity> IUnitOfWork.Repository<TEntity>()
        {
            throw new NotImplementedException();
        }

    }

}