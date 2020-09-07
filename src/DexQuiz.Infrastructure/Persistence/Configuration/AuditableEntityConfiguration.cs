using DexQuiz.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DexQuiz.Infrastructure.Persistence.Configuration
{
    public abstract class AuditableEntityConfiguration<TEntity>: BaseEntityConfiguration<TEntity>
        where TEntity: AuditableEntity
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.CreatedAtUtc)
                .IsRequired();
            builder.Property(x => x.CreatedBy)
                .IsRequired();
        }
    }
}
