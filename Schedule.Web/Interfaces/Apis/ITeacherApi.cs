using Refit;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Priorities.Requests;
using Schedule.Domain.Dto.Priorities.Responses;
using Schedule.Domain.Dto.Teachers.Requests;
using Schedule.Domain.Dto.Teachers.Responses;
using System.Threading.Tasks;

namespace Schedule.Web.Interfaces.Apis
{
    [Headers("Authorization: Bearer")]
    public interface ITeacherApi
    {
        [Get("/api/Teacher")]
        Task<ApiListResponseDto<GetAllTeacherResponseDto>> GetAllTeachers();

        [Get("/api/Teacher/{id}")]
        Task<ApiResponseDto<GetAllTeacherResponseDto>> GetTeacher(long id);

        [Post("/api/Teacher")]
        Task<ApiResponseDto<GetAllTeacherResponseDto>> CreateTeacher(SaveTeacherRequestDto dto);

        [Put("/api/Teacher/{id}")]
        Task<ApiResponseDto<GetAllTeacherResponseDto>> UpdateTeacher(long id, SaveTeacherRequestDto dto);

        [Delete("/api/Teacher/{id}")]
        Task<ApiResponseDto<GetAllTeacherResponseDto>> DeleteTeacher(long id);

        [Get("/api/Teacher/{id}/Availability")]
        Task<ApiResponseDto<TeacherAvailabilityDetailsResponseDto>> GetAvailability(long id);

        [Post("/api/Teacher/{id}/Availability")]
        Task<ApiListResponseDto<TeacherAvailabilityResponseDto>> SaveAvailability(long id, SaveTeacherAvailabilityRequestDto dto);

        [Get("/api/Teacher/Priorities")]
        Task<ApiListResponseDto<GetAllPrioritiesResponseDto>> GetAllPriorities();

        [Get("/api/Teacher/Priorities/{id}")]
        Task<ApiResponseDto<GetAllPrioritiesResponseDto>> GetPriority(long id);

        [Post("/api/Teacher/Priorities")]
        Task<ApiResponseDto<GetAllPrioritiesResponseDto>> CreatePriority(SavePriorityRequestDto dto);

        [Put("/api/Teacher/Priorities/{id}")]
        Task<ApiResponseDto<GetAllPrioritiesResponseDto>> UpdatePriority(long id, SavePriorityRequestDto dto);

        [Delete("/api/Teacher/Priorities/{id}")]
        Task<EmptyResponseDto> DeletePriority(long id);
    }
}
