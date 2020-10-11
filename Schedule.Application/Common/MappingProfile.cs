using AutoMapper;
using Schedule.Domain.Dto.Careers.Responses;
using Schedule.Domain.Dto.Classrooms.Responses;
using Schedule.Domain.Dto.Periods.Responses;
using Schedule.Domain.Dto.Priorities.Responses;
using Schedule.Domain.Dto.Subjects.Responses;
using Schedule.Domain.Dto.Teachers.Responses;
using Schedule.Domain.Entities;

namespace Schedule.Application.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Teacher, GetAllTeacherResponseDto>()
                .ForMember(d => d.Priority, s => s.MapFrom(x => x.Priority.Name));
            CreateMap<Subject, GetAllSubjectsResponseDto>()
                .ForMember(d => d.Career, s => s.MapFrom(x => x.Career.Name))
                .ForMember(d => d.Semester, s => s.MapFrom(x => x.Semester.Name))
                .ForMember(d => d.ClassroomType, s => s.MapFrom(x => x.ClassroomTypePerSubject.Name));
            CreateMap<Career, GetAllCareersResponseDto>();
            CreateMap<Period, GetAllPeriodsResponseDto>();
            CreateMap<Classroom, GetAllClassroomsResponseDto>()
                .ForMember(d => d.ClassroomType, s => s.MapFrom(x => x.ClassroomTypePerSubject.Name));
            CreateMap<Priority, GetAllPrioritiesResponseDto>();
        }
    }
}
