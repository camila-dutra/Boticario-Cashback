using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cashback.Domain.DTOs
{
    public class PurchaseDTO
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public decimal Value { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public long Cpf { get; set; }
    }
}
