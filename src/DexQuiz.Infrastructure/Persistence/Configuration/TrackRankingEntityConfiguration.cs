
using DexQuiz.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DexQuiz.Infrastructure.Persistence.Configuration
{
    public class TrackRankingEntityConfiguration : BaseEntityConfiguration<TrackRanking>
    {
        public override void Configure(EntityTypeBuilder<TrackRanking> builder)
        {
            builder.Property(x => x.TrackId)
                .IsRequired();
            builder.Property(x => x.UserId)
                .IsRequired();
            builder.Property(x => x.Points)
                .IsRequired();
            builder.Property(x => x.StartedAtUtc)
                .IsRequired();
            builder.Ignore(x => x.Position);
            builder.Ignore(x => x.Username);
        }
    }
}
