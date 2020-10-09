using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Entities;

namespace Schedule.Infrastructure.Persistence.Repositories
{
    public class TeacherAvailabilityRepository : Repository<TeacherAvailability>, ITeacherAvailabilityRepository
    {
        public TeacherAvailabilityRepository(AppDbContext context) : base(context)
        {
        }
    }
}
