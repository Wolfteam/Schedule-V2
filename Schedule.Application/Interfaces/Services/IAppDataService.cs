using Schedule.Application.Interfaces.Repositories;
using System;
using System.Threading.Tasks;

namespace Schedule.Application.Interfaces.Services
{
    public interface IAppDataService : IDisposable
    {
        ICareerRepository Careers { get; }
        IClassroomRepository Classrooms { get; }
        IClassroomTypePerSubjectRepository ClassroomTypes { get; }
        IPeriodRepository Periods { get; }
        IPeriodSectionRepository PeriodSection { get; }
        IPriorityRepository Priorities { get; }
        ISemesterRepository Semesters { get; }
        ISubjectRepository Subjects { get; }
        ITeacherAvailabilityRepository TeacherAvailabilities { get; }
        ITeacherPerSubjectRepository TeacherPerSubjects { get; }
        ITeacherRepository Teachers { get; }
        ITeacherScheduleRepository TeacherSchedules { get; }

        Task SaveChangesAsync();
    }
}
