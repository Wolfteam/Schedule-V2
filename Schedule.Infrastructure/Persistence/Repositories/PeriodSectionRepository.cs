using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Entities;

namespace Schedule.Infrastructure.Persistence.Repositories
{
    public class PeriodSectionRepository : Repository<PeriodSection>, IPeriodSectionRepository
    {
        public PeriodSectionRepository(AppDbContext context) : base(context)
        {
        }
    }
}
