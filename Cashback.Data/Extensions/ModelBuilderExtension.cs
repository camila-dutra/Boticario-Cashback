using System;
using System.Collections.Generic;
using System.Text;
using Cashback.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cashback.Data.Extensions
{
    public static class ModelBuilderExtension
    {
        public static ModelBuilder SeedData(this ModelBuilder builder)
        {
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
