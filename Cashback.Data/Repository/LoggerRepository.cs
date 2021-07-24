using System;
using System.Collections.Generic;
using System.Text;
using Cashback.Data.Context;
using Cashback.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cashback.Data.Repository
{
    public class LoggerRepository
    {
        public LoggerRepository() { }

        public bool Insert(LoggerApp log)
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<CashbackContext>();
                using (var context = new CashbackContext(optionsBuilder.Options))
                {
                    context.LoggerApp.Add(log);
                    context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
