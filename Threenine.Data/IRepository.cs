using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections.Generic;

namespace Threenine.Data
{
    
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get();
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
      }
 }
