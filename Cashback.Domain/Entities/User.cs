using System;
using System.Collections.Generic;
using System.Text;

namespace Cashback.Domain.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long Cpf { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
