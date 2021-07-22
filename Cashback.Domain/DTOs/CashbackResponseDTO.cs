using System;
using System.Collections.Generic;
using System.Text;

namespace Cashback.Domain.DTOs
{
    public class CashbackResponseDTO
    {
        public int statusCode { get; set; }
        public CashbackResponseBodyDTO body { get; set; }
    }
}
