using System.Runtime.InteropServices;

namespace Threenine.Data
{
    public interface IRepositoryFactory
    {
        IRepository<T> GetRepository<T>() where T : class;

    }
}