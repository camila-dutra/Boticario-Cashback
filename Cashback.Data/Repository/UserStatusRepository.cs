using System;
using System.Collections.Generic;
using System.Text;
using Cashback.Data.Context;
using Cashback.Data.Interfaces;
using Cashback.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cashback.Data.Repository
{
    public class UserStatusRepository : Repository<UserStatus>, IUserStatusRepository
    {
        public UserStatusRepository(CashbackContext context) : base(context) { }

    }
}
