using DexQuiz.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DexQuiz.Infrastructure.Persistence.Configuration
{
    public class AvailableQuestionEntityConfiguration :BaseEntityConfiguration<AvailableQuestion>
    {
        public override void Configure(EntityTypeBuilder<AvailableQuestion> builder)
        {
            builder.Property(x => x.TrackId)
                .IsRequired();
            builder.Property(x => x.QuestionId)
                .IsRequired();
            builder.Property(x => x.UserId)
                .IsRequired();
        }
    }
}
