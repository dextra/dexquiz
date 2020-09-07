using DexQuiz.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DexQuiz.Infrastructure.Persistence.Configuration
{
    public class AnsweredQuestionEntityConfiguration : BaseEntityConfiguration<AnsweredQuestion>
    {
        public override void Configure(EntityTypeBuilder<AnsweredQuestion> builder)
        {
            builder.Property(x => x.AnswerId)
                .IsRequired();
            builder.Property(x => x.QuestionId)
                .IsRequired();
            builder.Property(x => x.UserId)
                .IsRequired();
        }
    }
}
