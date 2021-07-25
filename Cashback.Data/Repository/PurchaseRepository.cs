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
            _logger.LogInformation( $"Creating a new purchase CODE:" + purchase.Code + " VALUE:" + purchase.Value +" CPF:" + purchase.Cpf);
            base.Create(purchase);
            _logger.LogInformation( $"Purchase created!");
        }

        public IEnumerable<Purchase> GetAll(long cpf)
        {
            _logger.LogInformation($"Getting all purchases");
            return Query(x => x.Cpf == cpf).AsNoTracking();
        }
    }
}
