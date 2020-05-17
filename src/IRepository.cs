using System;
using System.Collections.Generic;

namespace Threenine.Data
{
    public interface IRepository<T> : IReadRepository<T>, IDisposable where T : class
    {
        void Insert(T entity);
        void Insert(params T[] entities);
        void Insert(IEnumerable<T> entities);


        void Delete(T entity);
        void Delete(object id);
        void Delete(params T[] entities);
        void Delete(IEnumerable<T> entities);
        
        
        void Update(T entity);
        void Update(params T[] entities);
        void Update(IEnumerable<T> entities);
    }
}