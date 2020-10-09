using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schedule.Domain.Entities;

namespace Schedule.Infrastructure.Persistence.EntitiesConfiguration
{
    public class TeacherTypeConfiguration : BaseTypeConfiguration<Teacher>
    {
        public override void Configure(EntityTypeBuilder<Teacher> builder)
        {
            base.Configure(builder);
            builder.HasIndex(b => b.IdentifierNumber).IsUnique();

            builder.Property(b => b.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(b => b.SecondName).HasMaxLength(50);

            builder.Property(b => b.FirstLastName).IsRequired().HasMaxLength(50);
            builder.Property(b => b.SecondLastName).HasMaxLength(50);
        }
    }
}
