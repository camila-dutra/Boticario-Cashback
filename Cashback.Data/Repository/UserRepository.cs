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
            //_logger.LogInformation(1002, "Creating a new user Name:" + user.Name + "- CPF:" + user.Cpf + "- Email:" + user.Email);
           // _logger.LogInformation("Creating a new user Name");
            _logger.LogInformation(1000,$"Processing request from {user}", user);
            base.Create(user);
        }
        public IEnumerable<User> GetAll()
        {
            return Query(x => true).AsNoTracking();
        }
    }
}
