using AutoMapper;
using Schedule.Domain.Dto.Careers.Responses;
using Schedule.Domain.Dto.Classrooms.Requests;
using Schedule.Domain.Dto.Classrooms.Responses;
using Schedule.Domain.Dto.Periods.Requests;
using Schedule.Domain.Dto.Periods.Responses;
using Schedule.Domain.Dto.Priorities.Requests;
using Schedule.Domain.Dto.Priorities.Responses;
using Schedule.Domain.Dto.Semesters.Responses;
using Schedule.Domain.Dto.Subjects.Requests;
using Schedule.Domain.Dto.Subjects.Responses;
using Schedule.Domain.Dto.Teachers.Requests;
using Schedule.Domain.Dto.Teachers.Responses;
using Schedule.Domain.Entities;

namespace Schedule.Application.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Career, GetAllCareersResponseDto>();

            CreateMap<Classroom, GetAllClassroomsResponseDto>()
                .ForMember(d => d.ClassroomType, s => s.MapFrom(x => x.ClassroomSubject.Name));
            CreateMap<SaveClassroomRequestDto, Classroom>();
            CreateMap<SaveClassroomTypeRequestDto, ClassroomSubject>();
            CreateMap<ClassroomSubject, GetAllClassroomTypesResponseDto>();

            CreateMap<Period, GetAllPeriodsResponseDto>();
            CreateMap<SavePeriodRequestDto, Period>();

            CreateMap<Priority, GetAllPrioritiesResponseDto>();
            CreateMap<SavePriorityRequestDto, Priority>();

            CreateMap<Semester, GetAllSemestersResponseDto>();

            CreateMap<SaveSubjectRequestDto, Subject>();
            CreateMap<Subject, GetAllSubjectsResponseDto>()
                .ForMember(d => d.Career, s => s.MapFrom(x => x.Career.Name))
                .ForMember(d => d.Semester, s => s.MapFrom(x => x.Semester.Name))
                .ForMember(d => d.ClassroomType, s => s.MapFrom(x => x.ClassroomTypePerSubject.Name));

            CreateMap<Teacher, GetAllTeacherResponseDto>()
                .ForMember(d => d.Priority, s => s.MapFrom(x => x.Priority.Name));
            CreateMap<SaveTeacherRequestDto, Teacher>();
            CreateMap<TeacherAvailabilityRequestDto, TeacherAvailability>();
            CreateMap<TeacherAvailability, TeacherAvailabilityResponseDto>();
        }
    }
}
