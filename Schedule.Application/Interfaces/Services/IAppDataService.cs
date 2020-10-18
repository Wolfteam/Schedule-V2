using Schedule.Application.Interfaces.Repositories;
using System;
using System.Threading.Tasks;

namespace Schedule.Application.Interfaces.Services
{
    public interface IAppDataService : IDisposable
    {
        ICareerRepository Careers { get; }
        IClassroomRepository Classrooms { get; }
        IClassroomSubjectRepository ClassroomSubject { get; }
        IPeriodRepository Periods { get; }
        IPeriodSectionRepository PeriodSection { get; }
        IPriorityRepository Priorities { get; }
        ISchoolRepository Schools { get; }
        ISemesterRepository Semesters { get; }
        ISubjectRepository Subjects { get; }
        ITeacherAvailabilityRepository TeacherAvailabilities { get; }
        ITeacherSubjectRepository TeacherSubjects { get; }
        ITeacherRepository Teachers { get; }
        ITeacherScheduleRepository TeacherSchedules { get; }

        Task SaveChangesAsync();
    }
}
