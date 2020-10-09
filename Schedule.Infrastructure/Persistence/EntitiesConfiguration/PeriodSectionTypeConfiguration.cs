using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Domain.Entities;

namespace Schedule.Infrastructure.Persistence.EntitiesConfiguration
{
    public class PeriodSectionTypeConfiguration : BaseTypeConfiguration<PeriodSection>
    {
        public override void Configure(EntityTypeBuilder<PeriodSection> builder)
        {
            base.Configure(builder);
            builder.HasIndex(b => new {b.PeriodId, b.SubjectId});
        }
    }
}
