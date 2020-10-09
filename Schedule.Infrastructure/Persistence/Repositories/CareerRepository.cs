using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Entities;

namespace Schedule.Infrastructure.Persistence.Repositories
{
    public class CareerRepository : Repository<Career>, ICareerRepository
    {
        public CareerRepository(AppDbContext context) : base(context)
        {
        }
    }
}
