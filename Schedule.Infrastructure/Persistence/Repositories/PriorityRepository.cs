using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Entities;

namespace Schedule.Infrastructure.Persistence.Repositories
{
    public class PriorityRepository : Repository<Priority>, IPriorityRepository
    {
        public PriorityRepository(AppDbContext context) : base(context)
        {
        }
    }
}
