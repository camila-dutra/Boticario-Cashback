using System;
using System.Collections.Generic;
using System.Text;
using Cashback.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cashback.Data.Mappings
{
    class LoggerAppMap : IEntityTypeConfiguration<LoggerApp>
    {
        public void Configure(EntityTypeBuilder<LoggerApp> builder)
        {
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.LogLevel).IsRequired();

            builder.Property(x => x.Message).IsRequired();

            builder.Property(x => x.CreatedTime).IsRequired(); ;
        }
    }
}
