using System;
using System.Collections.Generic;
using System.Text;
using Cashback.Data.Context;
using Cashback.Data.Interfaces;
using Cashback.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cashback.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private ILogger _logger;
        public UserRepository(CashbackContext context, ILogger logger) : base(context)
        {
            this._logger = logger;
        }

        public void Create(User user)
        {
            _logger.LogInformation(1000,$"Creating a new user {user}", user);
            base.Create(user);
        }
        public IEnumerable<User> GetAll()
        {
            _logger.LogInformation(1000, $"Getting all users");
            return Query(x => true).AsNoTracking();
        }
    }
}
