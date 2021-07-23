using System;
using System.Collections.Generic;
using System.Text;
using Cashback.Data.Context;
using Cashback.Data.Interfaces;
using Cashback.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cashback.Data.Repository
{
    public class PurchaseRepository: Repository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(CashbackContext context) : base(context) { }

        public IEnumerable<Purchase> GetAll(long cpf)
        {
            return Query(x => x.Cpf == cpf).AsNoTracking();
        }
    }
}
