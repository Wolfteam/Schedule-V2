using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Domain.Entities;

namespace Schedule.Infrastructure.Persistence.EntitiesConfiguration
{
    public class CareerTypeConfiguration : BaseTypeConfiguration<Career>
    {
        public override void Configure(EntityTypeBuilder<Career> builder)
        {
            base.Configure(builder);
            builder.Property(b => b.Name).IsRequired().HasMaxLength(255);

            builder.HasIndex(b => new {b.SchoolId, b.Name}).IsUnique();
        }
    }
}
