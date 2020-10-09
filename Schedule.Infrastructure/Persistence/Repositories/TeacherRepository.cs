using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Entities;
using Schedule.Shared.Interfaces.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Schedule.Shared.Extensions;

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
            return query.Paginate<Teacher, TMapTo>(request, response, _mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
