using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cashback.Domain.DTOs
{
    public class ResellerRequestDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public long Cpf { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
