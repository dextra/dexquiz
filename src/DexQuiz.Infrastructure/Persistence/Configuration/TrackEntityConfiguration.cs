using DexQuiz.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DexQuiz.Infrastructure.Persistence.Configuration
{
    public class TrackEntityConfiguration : AuditableEntityConfiguration<Track>
    {
        public override void Configure(EntityTypeBuilder<Track> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(x => x.Available)
                .HasDefaultValue(false)
                .IsRequired();
        }
    }
}
