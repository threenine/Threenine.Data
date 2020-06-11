using System;
using Microsoft.EntityFrameworkCore;

namespace Threenine.Data
{
    public class DeleteRepository<T> : IDeleteRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public DeleteRepository(DbContext context)
        {
            _dbContext = context ?? throw new ArgumentException(nameof(context));
            _dbSet = _dbContext.Set<T>();
        }
    }
}