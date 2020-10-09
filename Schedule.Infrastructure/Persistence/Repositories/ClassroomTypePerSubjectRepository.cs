using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Entities;

namespace Schedule.Infrastructure.Persistence.Repositories
{
    public class ClassroomTypePerSubjectRepository : Repository<ClassroomTypePerSubject>, IClassroomTypePerSubjectRepository
    {
        public ClassroomTypePerSubjectRepository(AppDbContext context) : base(context)
        {
        }
    }
}
