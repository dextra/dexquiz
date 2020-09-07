using DexQuiz.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DexQuiz.Infrastructure.Persistence.Configuration
{
    public class UserEntityConfiguration : AuditableEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(100);
            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(255);
            builder.HasIndex(x => x.Email)
                .IsUnique();
            builder.Property(x => x.CellPhone)
                .HasMaxLength(14);
            builder.Property(x => x.Password)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(x => x.UserType)
                .IsRequired();
            builder.Property(x => x.AllowContact)
                .HasDefaultValue(false);
            builder.Property(x => x.Linkedin)
                .HasMaxLength(100);
        }
    }
}
