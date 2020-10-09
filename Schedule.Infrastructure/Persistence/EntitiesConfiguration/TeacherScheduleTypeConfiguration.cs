using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Domain.Entities;

namespace Schedule.Infrastructure.Persistence.EntitiesConfiguration
{
    public class TeacherScheduleTypeConfiguration : BaseTypeConfiguration<TeacherSchedule>
    {
        public override void Configure(EntityTypeBuilder<TeacherSchedule> builder)
        {
            base.Configure(builder);

            builder.HasIndex(b => new {b.PeriodId, b.Day, b.TeacherId, b.StartHour, b.EndHour}).IsUnique();
        }
    }
}
