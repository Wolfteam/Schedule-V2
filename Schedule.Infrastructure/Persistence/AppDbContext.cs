using Microsoft.EntityFrameworkCore;
using Schedule.Domain.Entities;
using Schedule.Domain.Interfaces.Managers;
using Schedule.Infrastructure.Persistence.Queries;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public const string ScheduleDbScheme = "sch";
        private const string DateFormat = "'%d/%m/%Y'";

        private const string SubjectsViewName = "subjects_view";
        private static readonly string SubjectsViewFullName = $"{ScheduleDbScheme}_{SubjectsViewName}";
        public static readonly string SubjectsViewQuery = $@"
            DROP VIEW IF EXISTS {SubjectsViewFullName};
            CREATE VIEW {SubjectsViewFullName}
            AS
            SELECT ss.*,
                   sc.Name                            as Career,
                   st.Name                            AS ClassroomType,
                   se.Name                            AS Semester,
                   date_format(ss.CreatedAt, {DateFormat}) as CreatedAtString
            FROM sch_subjects ss
                     INNER JOIN sch_careers sc on ss.CareerId = sc.Id
                     INNER JOIN sch_classroomtypepersubject st on ss.ClassroomTypePerSubjectId = st.Id
                     INNER JOIN sch_semesters se on ss.SemesterId = se.Id;";

        private const string ClassroomViewName = "classrooms_view";
        private static readonly string ClassroomViewFullName = $"{ScheduleDbScheme}_{ClassroomViewName}";
        public static readonly string ClassroomViewQuery = $@"
            DROP VIEW IF EXISTS {ClassroomViewFullName};
            CREATE VIEW {ClassroomViewFullName}
            AS
            SELECT 
                c.*, 
                date_format(c.CreatedAt, {DateFormat}) as CreatedAtString,
                sc.Name as ClassroomSubject
            FROM sch_classrooms c
            INNER JOIN sch_classroomtypepersubject sc on c.ClassroomSubjectId = sc.Id;";

        private readonly IDefaultAppUserManager _appUserManager;

        #region Properties
        public DbSet<Career> Careers { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<ClassroomSubject> ClassroomTypePerSubject { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<PeriodSection> PeriodSection { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherAvailability> TeacherAvailabilities { get; set; }
        public DbSet<TeacherSubject> TeacherPerSubjects { get; set; }
        public DbSet<TeacherSchedule> TeacherSchedules { get; set; }

        public DbSet<SubjectView> SubjectView { get; set; }
        public DbSet<ClassroomView> ClassroomView { get; set; }
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

            modelBuilder.Entity<SubjectView>().HasNoKey().ToView(SubjectsViewName);
            modelBuilder.Entity<ClassroomView>().HasNoKey().ToView(ClassroomViewName);
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
