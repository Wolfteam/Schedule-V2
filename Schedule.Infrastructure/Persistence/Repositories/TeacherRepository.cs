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
    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        private readonly IMapper _mapper;
        public TeacherRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public Task<List<TMapTo>> GetAll<TMapTo>(IPaginatedRequestDto request, IPaginatedResponseDto response)
            where TMapTo : class, new()
        {
            var query = Context.Teachers.AsQueryable();
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                var searchTerm = request.SearchTerm;
                query = query.Where(t =>
                    t.FirstName.Contains(searchTerm) ||
                    t.FirstLastName.Contains(searchTerm) ||
                    t.IdentifierNumber.ToString().Contains(searchTerm));
            }

            return query.Paginate<Teacher, TMapTo>(request, response, _mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
