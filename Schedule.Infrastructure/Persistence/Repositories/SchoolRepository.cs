using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Entities;

namespace Schedule.Infrastructure.Persistence.Repositories
{
    public class SchoolRepository : Repository<School>, ISchoolRepository
    {
        public SchoolRepository(AppDbContext context) : base(context)
        {
        }
    }
}
