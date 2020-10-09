using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Domain.Entities;

namespace Schedule.Infrastructure.Persistence.EntitiesConfiguration
{
    public class ClassroomTypePerSubjectTypeConfiguration : BaseTypeConfiguration<ClassroomTypePerSubject>
    {
        public override void Configure(EntityTypeBuilder<ClassroomTypePerSubject> builder)
        {
            base.Configure(builder);
            builder.Property(b => b.Name).IsRequired().HasMaxLength(100);
        }
    }
}
