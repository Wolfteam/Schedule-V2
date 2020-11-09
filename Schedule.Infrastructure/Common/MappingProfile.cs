using AutoMapper;
using Schedule.Domain.Dto.Classrooms.Responses;
using Schedule.Domain.Dto.Subjects.Responses;
using Schedule.Infrastructure.Persistence.Queries;

namespace Schedule.Infrastructure.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SubjectView, GetAllSubjectsResponseDto>();
            CreateMap<ClassroomView, GetAllClassroomsResponseDto>();
        }
    }
}
