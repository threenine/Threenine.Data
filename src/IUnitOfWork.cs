using System;
using Microsoft.EntityFrameworkCore;

namespace Threenine.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : class;
        IRepositoryReadOnly<TEntity> GetReadOnlyRepository<TEntity>() where TEntity : class;

        int Commit();
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext Context { get; }
    }
}