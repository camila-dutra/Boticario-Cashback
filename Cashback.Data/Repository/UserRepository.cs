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
        private readonly ILogger _logger;

        public UserRepository(CashbackContext context,
            ILogger<UserRepository> logger) : base(context)
        {
            _logger = logger;
        }

        void Create(User user)
        {
            _logger.LogInformation(1002, "Creating a new user Name:" + user.Name + "- CPF:" + user.Cpf + "- Email:" + user.Email);
            Create(user);
            _logger.LogInformation(1002, "User Created:" + user.Name + "- CPF:" + user.Cpf + "- Email:" + user.Email);
        }

        public IEnumerable<User> GetAll()
        {
            return Query(x => true).AsNoTracking();
        }
    }
}
