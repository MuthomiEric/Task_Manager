using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Utils;

namespace Infrastructure.Data.TypeConfiguration
{
    internal class CardTypeConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.Property(c => c.Name).HasMaxLength(200);
            builder.Property(c => c.Color).HasMaxLength(7);
            builder.Property(c => c.Description).HasMaxLength(500);
            builder.Property(c => c.OwnerId).HasMaxLength(450);

            builder.Property(c => c.Status).HasConversion(
            v => v.ToString(),
            v => (Status)Enum.Parse(typeof(Status), v)).HasMaxLength(20);

            builder.HasIndex(c => c.Name);
            builder.HasIndex(c => c.Color);
            builder.HasIndex(c => c.Status);
            builder.HasIndex(c => c.CreatedDate);
        }
    }
}