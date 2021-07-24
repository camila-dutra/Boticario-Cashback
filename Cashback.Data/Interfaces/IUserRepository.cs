using System;
using System.Collections.Generic;
using System.Text;
using Cashback.Domain.Entities;

namespace Cashback.Data.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public void Create(User user);
        public IEnumerable<User> GetAll();
    }
}
