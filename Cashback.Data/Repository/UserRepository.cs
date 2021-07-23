using System;
using System.Collections.Generic;
using System.Text;
using Cashback.Data.Context;
using Cashback.Data.Interfaces;
using Cashback.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cashback.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(CashbackContext context) : base(context) { }

        public IEnumerable<User> GetAll()
        {
            return Query(x => true).AsNoTracking();
        }
    }
}
