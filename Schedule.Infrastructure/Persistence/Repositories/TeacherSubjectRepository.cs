using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Entities;

namespace Schedule.Infrastructure.Persistence.Repositories
{
    public class TeacherSubjectRepository : Repository<TeacherSubject>, ITeacherSubjectRepository
    {
        public TeacherSubjectRepository(AppDbContext context) : base(context)
        {
        }
    }
}
