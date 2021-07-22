using System;
using System.Collections.Generic;
using System.Text;
using Cashback.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cashback.Data.Extensions
{
    public static class ModelBuilderExtension
    {
        public static ModelBuilder SeedData(this ModelBuilder builder)
        {
            builder.Entity<User>().HasData(new User
                                                 {
                                                     Id = 1,
                                                     Name = "User Test",
                                                     Cpf = 12312312323,
                                                     Email = "usertest@gmail.com",
                                                     Password = "123123"
                                                 });

            builder.Entity<UserStatus>().HasData(new UserStatus
            {
                Id = 1,
                Cpf = 15350946056,
                Status = 1
            });


            return builder;
        }
    }
}
