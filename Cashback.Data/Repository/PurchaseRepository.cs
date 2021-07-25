using System.Collections.Generic;
using Cashback.Data.Context;
using Cashback.Data.Interfaces;
using Cashback.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cashback.Data.Repository
{
    public class PurchaseRepository: Repository<Purchase>, IPurchaseRepository
    {
        private ILogger _logger;

        public PurchaseRepository(CashbackContext context, ILogger logger) : base(context)
        {
            this._logger = logger;
        }

        public void Create(Purchase purchase)
        {
            _logger.LogInformation(1000, $"Creating a new purchase {purchase}", purchase);
            base.Create(purchase);
        }

        public IEnumerable<Purchase> GetAll(long cpf)
        {
            _logger.LogInformation(1000, $"Getting all purchases");
            return Query(x => x.Cpf == cpf).AsNoTracking();
        }
    }
}
