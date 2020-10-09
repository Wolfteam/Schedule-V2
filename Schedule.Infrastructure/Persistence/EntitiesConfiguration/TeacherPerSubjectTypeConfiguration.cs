using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Domain.Entities;

namespace Schedule.Infrastructure.Persistence.EntitiesConfiguration
{
    public class TeacherPerSubjectTypeConfiguration : BaseTypeConfiguration<TeacherPerSubject>
    {
        public override void Configure(EntityTypeBuilder<TeacherPerSubject> builder)
        {
            base.Configure(builder);
            builder.HasIndex(b => new {b.TeacherId, b.SubjectId}).IsUnique();
        }
    }
}
