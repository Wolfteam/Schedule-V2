using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Entities;
using Schedule.Domain.Interfaces.Dto;
using Schedule.Shared.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Infrastructure.Persistence.Repositories
{
    public class PeriodRepository : Repository<Period>, IPeriodRepository
    {
        private readonly IMapper _mapper;
        public PeriodRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public Task<List<TMapTo>> GetAll<TMapTo>(long schoolId, IPaginatedRequestDto request, IPaginatedResponseDto response)
            where TMapTo : class, new()
        {
            var query = Context.Periods.Where(p => p.SchoolId == schoolId);
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                var searchTerm = request.SearchTerm.Trim();
                query = query.Where(t => t.Name.Contains(searchTerm));
            }

            return query.Paginate<Period, TMapTo>(request, response, _mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task InactiveAllPeriods(long schoolId)
        {
            var periods = await Context.Periods
                .Where(p => p.SchoolId == schoolId)
                .ToListAsync();

            foreach (var period in periods)
                period.IsActive = false;
        }
    }
}
