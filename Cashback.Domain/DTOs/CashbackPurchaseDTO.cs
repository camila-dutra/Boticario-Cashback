using System;
using System.Collections.Generic;
using System.Text;

namespace Cashback.Domain.DTOs
{
    public class CashbackPurchaseDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public long Cpf { get; set; }
        public int Status { get; set; }
        public string CashbackPerc { get; set; }
        public decimal CashbackValue { get; set; }
        public string DscStatus { get; set; }
    }
}
