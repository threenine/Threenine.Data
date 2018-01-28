
using System;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Threenine.Data
{
    public class UnitOfWork :  IUnitOfWork
    {
        public DbContext Context { get; }
 
        public UnitOfWork(DbContext context)
        {
            Context = context;
        }
        public void Commit()
        {
            Context.SaveChanges();
        }
 
        public void Dispose()
       {
           Dispose(true);
           GC.SuppressFinalize(this);
       }

         private void Dispose(bool disposing)
       {
           if (disposing)
           {
               if (Context != null)
               {
                   Context.Dispose();
                   
               }
           }
       }
    }

}