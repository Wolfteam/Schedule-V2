using Schedule.Domain.Entities;
using Schedule.Domain.Interfaces.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schedule.Application.Interfaces.Repositories
{
    public interface ISubjectRepository : IRepository<Subject>
    {
        Task<List<TMapTo>> GetAll<TMapTo>(long schoolId, IPaginatedRequestDto request, IPaginatedResponseDto response)
            where TMapTo : class, new();

        Task CheckBeforeSaving(long schoolId, long semesterId, long careerId, long classroomTypeId);
    }
}
