﻿using System;
using System.Collections.Generic;
using System.Text;
using Cashback.Data.Context;
using Cashback.Data.Interfaces;
using Cashback.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cashback.Data.Repository
{
    public class ResellerRepository : Repository<User>, IResellerRepository
    {
        public ResellerRepository(CashbackContext context) : base(context) { }

        public IEnumerable<User> GetAll()
        {
            return Query(x => true).AsNoTracking();
        }
    }
}
