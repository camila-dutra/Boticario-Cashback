using System;
using System.Collections.Generic;
using System.Text;
using Cashback.Domain.DTOs;

namespace Cashback.Service.Interfaces
{
    public interface IResellerService
    {
        bool PostReseller(ResellerRequestDTO reseller);
    }
}
