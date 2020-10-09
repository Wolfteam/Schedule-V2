using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Domain.Entities;

namespace Schedule.Infrastructure.Persistence.EntitiesConfiguration
{
    public class PeriodTypeConfiguration : BaseTypeConfiguration<Period>
    {
        public override void Configure(EntityTypeBuilder<Period> builder)
        {
            base.Configure(builder);
            builder.Property(b => b.Name).IsRequired().HasMaxLength(100);
            builder.HasIndex(b => b.Name).IsUnique();
        }
    }
}
