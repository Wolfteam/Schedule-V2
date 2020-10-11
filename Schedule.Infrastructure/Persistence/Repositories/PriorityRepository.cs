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
    public class PriorityRepository : Repository<Priority>, IPriorityRepository
    {
        private readonly IMapper _mapper;
        public PriorityRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public Task<List<TMapTo>> GetAll<TMapTo>(IPaginatedRequestDto request, IPaginatedResponseDto response)
            where TMapTo : class, new()
        {
            var query = Context.Priorities.AsQueryable();
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                var searchTerm = request.SearchTerm;
                query = query.Where(t =>
                    t.HoursToComplete.ToString().Contains(searchTerm) ||
                    t.Name.Contains(searchTerm));
            }

            return query.Paginate<Priority, TMapTo>(request, response, _mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
