using System;
using System.Collections.Generic;
using System.Text;
using Cashback.Data.Extensions;
using Cashback.Data.Mappings;
using Cashback.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cashback.Data.Context
{
    public class CashbackContext: DbContext
    {
        public CashbackContext(DbContextOptions<CashbackContext> option) : base(option) { }

        #region DbSets
        public DbSet<User> User { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap()); 
            modelBuilder.ApplyConfiguration(new PurchaseMap());
            modelBuilder.ApplyConfiguration(new UserStatusMap());


            //modelBuilder.ApplyGlobalConfigurations();
            modelBuilder.SeedData();

            base.OnModelCreating(modelBuilder);
        }

    }
}
