using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Domain.Entities;

namespace Schedule.Infrastructure.Persistence.EntitiesConfiguration
{
    public class ClassroomSubjectTypeConfiguration : BaseTypeConfiguration<ClassroomSubject>
    {
        public override void Configure(EntityTypeBuilder<ClassroomSubject> builder)
        {
            base.Configure(builder);
            builder.Property(b => b.Name).IsRequired().HasMaxLength(100);
        }
    }
}
