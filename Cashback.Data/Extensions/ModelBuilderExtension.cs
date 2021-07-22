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
        public static ModelBuilder ApplyPurchaseConfigurations(this ModelBuilder builder)
        {
            foreach (IMutableEntityType entityType in builder.Model.GetEntityTypes())
            {
                foreach (IMutableProperty property in entityType.GetProperties())
                {
                    switch (property.Name)
                    {
                        case nameof(Purchase.Id):
                            property.IsKey();
                            break;
                        case nameof(Purchase.Value):
                            property.SetPrecision(2);
                            break;
                        case nameof(Purchase.Date):
                            property.IsNullable = false;
                            property.SetDefaultValue(DateTime.Now);
                            break;
                        default:
                            break;
                    }

                }
            }

            return builder;
        }
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
