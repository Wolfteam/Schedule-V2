using Schedule.Domain.Entities;
using Schedule.Domain.Interfaces.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schedule.Application.Interfaces.Repositories
{
    public interface IPeriodRepository : IRepository<Period>
    {
        Task<List<TMapTo>> GetAll<TMapTo>(long schoolId, IPaginatedRequestDto request, IPaginatedResponseDto response)
            where TMapTo : class, new();

        Task InactiveAllPeriods(long schoolId);
    }
}