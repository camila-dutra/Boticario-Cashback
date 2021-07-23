using System;
using System.Collections.Generic;
using System.Text;

namespace Cashback.Domain.DTOs
{
    public class UserAuthenticateResponseDTO
    {
        public ResellerRequestDTO User { get; set; }
        public string Token { get; set; }
        public UserAuthenticateResponseDTO(ResellerRequestDTO user, string token)
        {
            this.User = user;
            this.Token = token;
        }
    }
}
