using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Entities;

namespace Schedule.Infrastructure.Persistence.Repositories
{
    public class TeacherPerSubjectRepository : Repository<TeacherPerSubject>, ITeacherPerSubjectRepository
    {
        public TeacherPerSubjectRepository(AppDbContext context) : base(context)
        {
        }
    }
}
