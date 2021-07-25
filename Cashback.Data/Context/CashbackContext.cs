using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Cashback.Data.Extensions;
using Cashback.Data.Mappings;
using Cashback.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Cashback.Data.Context
{
    public class CashbackContext: DbContext
    {
        public CashbackContext(DbContextOptions<CashbackContext> option) : base(option)
        {
        }

        #region DbSets
        public DbSet<User> User { get; set; }
        public DbSet<Purchase> Purchase { get; set; }
        public DbSet<LoggerApp> LoggerApp { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap()); 
            modelBuilder.ApplyConfiguration(new PurchaseMap());
            modelBuilder.ApplyConfiguration(new UserStatusMap());
            modelBuilder.ApplyConfiguration(new LoggerAppMap());

            modelBuilder.SeedData();

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbConnectionString = "server=localhost;database=project;uid=root;password=Teste1234**";
            optionsBuilder.UseMySql(dbConnectionString, ServerVersion.AutoDetect(dbConnectionString));
        }

    }
}
