using System;
using System.Collections.Generic;
using System.Text;

namespace Cashback.Domain.DTOs
{
    public class CashbackResponseDTO
    {
        public int StatusCode { get; set; }
        public CashbackResponseBodyDTO Body { get; set; }
    }
}
