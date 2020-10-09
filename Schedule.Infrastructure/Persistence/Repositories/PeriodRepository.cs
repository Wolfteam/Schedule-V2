using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Entities;

namespace Schedule.Infrastructure.Persistence.Repositories
{
    public class PeriodRepository : Repository<Period>, IPeriodRepository
    {
        public PeriodRepository(AppDbContext context) : base(context)
        {
        }
    }
}
