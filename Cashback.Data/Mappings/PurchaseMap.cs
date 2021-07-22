using System;
using System.Collections.Generic;
using System.Text;
using Cashback.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cashback.Data.Mappings
{
    public class PurchaseMap : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.Code).IsRequired();

            builder.Property(x => x.Value).IsRequired().HasPrecision(9,2);

            builder.Property(x => x.Date).IsRequired();

            builder.Property(x => x.Cpf).HasMaxLength(11).IsRequired();

            builder.Property(x => x.Status);
        }
    }
}
