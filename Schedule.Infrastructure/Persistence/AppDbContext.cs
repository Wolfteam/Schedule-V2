using Microsoft.EntityFrameworkCore;
using Schedule.Domain.Entities;
using Schedule.Domain.Interfaces.Managers;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public const string ScheduleDbScheme = "sch";

        private readonly IDefaultAppUserManager _appUserManager;

        #region Properties
        public DbSet<Career> Careers { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<ClassroomTypePerSubject> ClassroomTypePerSubject { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<PeriodSection> PeriodSection { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherAvailability> TeacherAvailabilities { get; set; }
        public DbSet<TeacherPerSubject> TeacherPerSubjects { get; set; }
        public DbSet<TeacherSchedule> TeacherSchedules { get; set; }
        #endregion

        public AppDbContext(
            DbContextOptions options,
            IDefaultAppUserManager appUserManager)
            : base(options)
        {
            _appUserManager = appUserManager;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            modelBuilder.HasDefaultSchema(ScheduleDbScheme);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            var now = DateTimeOffset.UtcNow;

            foreach (var entry in modifiedEntries)
            {
                if (!(entry.Entity is BaseEntity))
                    continue;

                var entity = entry.Entity as BaseEntity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = now;
                    entity.CreatedBy = _appUserManager.Username;
                }
                else
                {
                    entity.UpdatedAt = now;
                    entity.UpdatedBy = _appUserManager.Username;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
