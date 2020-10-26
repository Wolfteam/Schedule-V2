using Schedule.Domain.Entities;
using System.Threading.Tasks;

namespace Schedule.Application.Interfaces.Repositories
{
    public interface ISchoolRepository : IRepository<School>
    {
        Task CheckIfSchoolExists(long id);
    }
}
