using System;
using System.Collections.Generic;
using System.Text;

namespace Cashback.Domain.DTOs
{
    public class UserAuthenticateRequestDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
