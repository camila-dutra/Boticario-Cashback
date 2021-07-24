using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cashback.Data.Context;
using Cashback.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cashback.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly CashbackContext context;
        protected DbSet<TEntity> DbSet
        {
            get
            {
                return context.Set<TEntity>();
            }
        }
        public Repository(CashbackContext context)
        {
            this.context = context;
        }

        public TEntity Create(TEntity model)
        {
            try
            {
                DbSet.Add(model);
                Save();
                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return DbSet.Where(where).AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TEntity Find(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return DbSet.AsNoTracking().FirstOrDefault(where);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Save()
        {
            try
            {
                return context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Dispose()
        {
            try
            {
                if (context != null)
                    context.Dispose();

                GC.SuppressFinalize(this);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
