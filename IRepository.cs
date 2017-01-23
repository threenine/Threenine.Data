
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace Threenine.Repository
{    public interface IRepository<T>
    {
        T Get<TKey>(TKey id);
        IQueryable<T> GetAll();

        Task<IEnumerable<T>> GetAllAsync();
        void Add(T entity);
        void Update(T entity);
    }
}