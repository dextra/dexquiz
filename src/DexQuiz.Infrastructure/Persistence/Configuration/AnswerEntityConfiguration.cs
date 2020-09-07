using DexQuiz.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DexQuiz.Infrastructure.Persistence.Configuration
{
    public class AnswerEntityConfiguration: AuditableEntityConfiguration<Answer>
    {
        public override void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.Property(x => x.Text)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(x => x.IsAnswerCorrect)
                .IsRequired();
            builder.Property(x => x.QuestionId)
                .IsRequired();
        }
    }
}
