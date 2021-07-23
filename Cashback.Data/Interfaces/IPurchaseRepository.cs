using System;
using System.Collections.Generic;
using System.Text;
using Cashback.Domain.Entities;

namespace Cashback.Data.Interfaces
{
    public interface IPurchaseRepository : IRepository<Purchase>
    {
         IEnumerable<Purchase> GetAll(long cpf);
    }
}
