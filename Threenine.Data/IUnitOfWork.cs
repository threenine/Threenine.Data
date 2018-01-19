using System;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Threenine.Data
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get;  }
        void Commit();
    }
}