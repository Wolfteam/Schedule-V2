using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Entities;

namespace Schedule.Infrastructure.Persistence.Repositories
{
    public class TeacherScheduleRepository : Repository<TeacherSchedule>, ITeacherScheduleRepository
    {
        public TeacherScheduleRepository(AppDbContext context) : base(context)
        {
        }
    }
}
