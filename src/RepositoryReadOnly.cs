using Microsoft.EntityFrameworkCore;

namespace Threenine.Data
{
    public class RepositoryReadOnly<T> : BaseRepository<T>, IRepositoryReadOnly<T> where T : class
    {
        public RepositoryReadOnly(DbContext context):base(context)
        {
            
        }
        
        
    }
}