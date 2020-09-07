using DexQuiz.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DexQuiz.Infrastructure.Persistence.Configuration
{
    public class QuestionEntityConfiguration : AuditableEntityConfiguration<Question>
    {
        public override void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.Property(x => x.Text)
                .HasMaxLength(500)
                .IsRequired();
            builder.Property(x => x.QuestionLevel)
                .IsRequired();
            builder.Property(x => x.TrackId)
                .IsRequired();
            builder.HasMany(x => x.Answers)
                .WithOne(y => y.Question)
                .IsRequired();
        }
    }
}
