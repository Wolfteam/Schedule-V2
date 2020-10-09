using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Repositories;
using Schedule.Application.Interfaces.Services;
using Schedule.Infrastructure.Persistence;
using System;
using System.Threading.Tasks;

namespace Schedule.Infrastructure.Services
{
    public class AppDataService : IAppDataService
    {
        #region Members
        private readonly AppDbContext _dbContext;
        private readonly ILogger<AppDataService> _logger;
        private bool _disposedValue; // To detect redundant calls
        #endregion

        #region Properties
        public ICareerRepository Careers { get; }
        public IClassroomRepository Classrooms { get; }
        public IClassroomTypePerSubjectRepository ClassroomTypes { get; }
        public IPeriodRepository Periods { get; }
        public IPeriodSectionRepository PeriodSection { get; }
        public IPriorityRepository Priorities { get; }
        public ISemesterRepository Semesters { get; }
        public ISubjectRepository Subjects { get; }
        public ITeacherAvailabilityRepository TeacherAvailabilities { get; }
        public ITeacherPerSubjectRepository TeacherPerSubjects { get; }
        public ITeacherRepository Teachers { get; }
        public ITeacherScheduleRepository TeacherSchedules { get; }
        #endregion

        public AppDataService(
            AppDbContext dbContext,
            ILogger<AppDataService> logger,
            ICareerRepository careers,
            IClassroomRepository classrooms,
            IClassroomTypePerSubjectRepository classroomTypes,
            IPeriodRepository periods,
            IPeriodSectionRepository periodSection,
            IPriorityRepository priorities,
            ISemesterRepository semesters,
            ISubjectRepository subjects,
            ITeacherAvailabilityRepository teacherAvailabilities,
            ITeacherPerSubjectRepository teacherPerSubjects,
            ITeacherRepository teachers,
            ITeacherScheduleRepository teacherSchedules)
        {
            _dbContext = dbContext;
            _logger = logger;
            Careers = careers;
            Classrooms = classrooms;
            ClassroomTypes = classroomTypes;
            Periods = periods;
            PeriodSection = periodSection;
            Priorities = priorities;
            Semesters = semesters;
            Subjects = subjects;
            TeacherAvailabilities = teacherAvailabilities;
            TeacherPerSubjects = teacherPerSubjects;
            Teachers = teachers;
            TeacherSchedules = teacherSchedules;
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                var changesMade = await _dbContext.SaveChangesAsync();

                if (changesMade == 0)
                    _logger.LogWarning("There weren't changes made in this context");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while trying to save the changes");
                throw;
            }
        }

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue)
                return;
            if (disposing)
            {
                _dbContext.Dispose();
            }

            _disposedValue = true;
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
