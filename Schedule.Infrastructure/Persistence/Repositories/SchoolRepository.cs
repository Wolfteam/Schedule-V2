using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Entities;
using Schedule.Shared.Exceptions;
using System.Threading.Tasks;

namespace Schedule.Infrastructure.Persistence.Repositories
{
    public class SchoolRepository : Repository<School>, ISchoolRepository
    {
        public SchoolRepository(AppDbContext context) : base(context)
        {
        }

        public async Task CheckIfSchoolExists(long id)
        {
            bool exists = await ExistsAsync(s => s.Id == id);
            if (!exists)
                throw new NotFoundException($"SchoolId = {id} does not exist");
        }
    }
}
