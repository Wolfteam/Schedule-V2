using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Domain.Entities;

namespace Schedule.Infrastructure.Persistence.EntitiesConfiguration
{
    public class ClassroomTypeConfiguration : BaseTypeConfiguration<Classroom>
    {
        public override void Configure(EntityTypeBuilder<Classroom> builder)
        {
            base.Configure(builder);
            builder.Property(b => b.Name).IsRequired().HasMaxLength(100);

            builder.HasIndex(b => new { b.SchoolId, b.Name }).IsUnique();
        }
    }
}
