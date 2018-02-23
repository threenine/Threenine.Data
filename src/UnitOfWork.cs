
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

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class 
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type]= new Repository<TEntity>(_context);
            }
            return (IRepository<TEntity>) _repositories[type];
        }

        public int SaveChanges()
        {
           return _context.SaveChanges();
        }

        public void Dispose()
        {
           Context?.Dispose();
        }

        
    }

}