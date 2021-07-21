using System;
using System.Collections.Generic;
using System.Text;

namespace Cashback.Domain.Entities
{
    public class Purchase
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public long Cpf { get; set; }
        public int Status { get; set; }
    }
}
