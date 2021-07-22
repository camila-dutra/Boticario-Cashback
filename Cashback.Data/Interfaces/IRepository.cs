using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Cashback.Data.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        TEntity Create(TEntity model);
        TEntity Find(Expression<Func<TEntity, bool>> where);
    }
}
