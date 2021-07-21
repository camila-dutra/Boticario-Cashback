using System;
using System.Collections.Generic;
using System.Text;

namespace Cashback.Domain.Entities
{
    public class UserStatus
    {
        public long Id { get; set; }
        public long Cpf { get; set; }
        public int Status { get; set; }
    }
}
