using System;
using System.Collections.Generic;
using System.Text;

namespace Cashback.Data.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        TEntity Create(TEntity model);
    }
}
