using System;
using System.Collections.Generic;
using System.Text;
using Cashback.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cashback.Data.Mappings
{
    public class UserStatusMap: IEntityTypeConfiguration<UserStatus>
    {
        public void Configure(EntityTypeBuilder<UserStatus> builder)
        {
            builder.Property(x => x.Id).IsRequired();

            builder.Property(x => x.Cpf);

            builder.Property(x => x.Status); // 1- Approved    2- Under evaluation
        }
    }
}
