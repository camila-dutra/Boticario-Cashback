using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cashback.Domain.DTOs;
using Cashback.Domain.Entities;

namespace Cashback.Service.Interfaces
{
    public interface IPurchaseService
    {
        bool PostPurchase(PurchaseDTO purchase);
        void SetStatusPurchase(Purchase purchase);
        List<CashbackPurchaseDTO> GetPurchases(long cpf);
    }
}
