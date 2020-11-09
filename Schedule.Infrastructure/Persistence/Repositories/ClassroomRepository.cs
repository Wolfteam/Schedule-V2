using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Entities;
using Schedule.Domain.Interfaces.Dto;
using Schedule.Infrastructure.Persistence.Queries;
using Schedule.Shared.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Infrastructure.Persistence.Repositories
{
    public class ClassroomRepository : Repository<Classroom>, IClassroomRepository
    {
        private readonly IMapper _mapper;
        public ClassroomRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public Task<List<TMapTo>> GetAll<TMapTo>(long schoolId, IPaginatedRequestDto request, IPaginatedResponseDto response)
            where TMapTo : class, new()
        {
            var query = Context.ClassroomView.Where(c => c.SchoolId == schoolId);
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                var searchTerm = request.SearchTerm.Trim();
                query = query.Where(t => t.Name.Contains(searchTerm) ||
                                         t.Capacity.ToString().Contains(searchTerm) ||
                                         t.CreatedAtString.Contains(searchTerm));
            }

            return query.Paginate<ClassroomView, TMapTo>(request, response, _mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
