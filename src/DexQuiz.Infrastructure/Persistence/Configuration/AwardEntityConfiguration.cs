using DexQuiz.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DexQuiz.Infrastructure.Persistence.Configuration
{
    public class AwardEntityConfiguration : AuditableEntityConfiguration<Award>
    {
        public override void Configure(EntityTypeBuilder<Award> builder)
        {
            builder.Property(x => x.Description)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(x => x.Type)
                .IsRequired();
            builder.Property(x => x.Position)
                .IsRequired();
        }
    }
}
