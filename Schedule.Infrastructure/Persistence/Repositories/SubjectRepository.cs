using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Entities;
using Schedule.Domain.Interfaces.Dto;
using Schedule.Shared.Exceptions;
using Schedule.Shared.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Infrastructure.Persistence.Repositories
{
    public class SubjectRepository : Repository<Subject>, ISubjectRepository
    {
        private readonly IMapper _mapper;
        public SubjectRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public Task<List<TMapTo>> GetAll<TMapTo>(long schoolId, IPaginatedRequestDto request, IPaginatedResponseDto response)
            where TMapTo : class, new()
        {
            var query = Context.Subjects.Where(s => s.SchoolId == schoolId);
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                var searchTerm = request.SearchTerm;
                query = query.Where(t =>
                    t.Code.ToString().Contains(searchTerm) ||
                    t.Name.Contains(searchTerm) ||
                    t.AcademicHoursPerWeek.ToString().Contains(searchTerm) ||
                    t.TotalAcademicHours.ToString().Contains(searchTerm));
            }

            return query.Paginate<Subject, TMapTo>(request, response, _mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task CheckBeforeSaving(long schoolId, long semesterId, long careerId, long classroomTypeId)
        {
            bool semesterExists = await Context.Semesters.AnyAsync(s => s.Id == semesterId && s.SchoolId == schoolId);
            if (!semesterExists)
            {
                var msg = $"SemesterId = {semesterId} was not found for schoolId = {schoolId}";
                throw new NotFoundException(msg);
            }

            bool careerExists = await Context.Careers.AnyAsync(s => s.Id == careerId && s.SchoolId == schoolId);
            if (!careerExists)
            {
                var msg = $"CareerId = {careerId} was not found for schoolId = {schoolId}";
                throw new NotFoundException(msg);
            }

            bool classroomTypeExists = await Context.ClassroomTypePerSubject.AnyAsync(c => c.Id == classroomTypeId && c.SchoolId == schoolId);
            if (!classroomTypeExists)
            {
                var msg = $"ClassroomTypeId = {classroomTypeId} was not found for schoolId = {schoolId}";
                throw new NotFoundException(msg);
            }
        }
    }
}
