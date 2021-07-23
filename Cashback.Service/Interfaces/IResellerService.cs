using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cashback.Domain.DTOs;

namespace Cashback.Service.Interfaces
{
    public interface IResellerService
    {
        bool PostReseller(ResellerRequestDTO reseller);
        Task<object> GetResellerCashback(long cpf);
    }
}
