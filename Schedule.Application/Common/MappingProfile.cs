using AutoMapper;
using Schedule.Application.Subjects.Queries.GetAllTeachers;
using Schedule.Domain.Entities;

namespace Schedule.Application.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Teacher, GetAllTeacherResponseDto>();
        }
    }
}
