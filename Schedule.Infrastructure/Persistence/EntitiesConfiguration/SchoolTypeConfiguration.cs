using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Domain.Entities;

namespace Schedule.Infrastructure.Persistence.EntitiesConfiguration
{
    public class SchoolTypeConfiguration : BaseTypeConfiguration<School>
    {
        public override void Configure(EntityTypeBuilder<School> builder)
        {
            base.Configure(builder);

            builder.Property(b => b.Name).IsRequired().HasMaxLength(255);
            builder.Property(b => b.Address).IsRequired().HasMaxLength(500);
        }
    }
}
