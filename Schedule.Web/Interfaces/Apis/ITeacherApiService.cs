using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Priorities.Requests;
using Schedule.Domain.Dto.Priorities.Responses;
using Schedule.Domain.Dto.Teachers.Requests;
using Schedule.Domain.Dto.Teachers.Responses;
using System.Threading.Tasks;

namespace Schedule.Web.Interfaces.Apis
{
    public interface ITeacherApiService
    {
        Task<ApiListResponseDto<GetAllTeacherResponseDto>> GetAllTeachers();

        Task<ApiResponseDto<GetAllTeacherResponseDto>> GetTeacher(long id);

        Task<ApiResponseDto<GetAllTeacherResponseDto>> CreateTeacher(SaveTeacherRequestDto dto);

        Task<ApiResponseDto<GetAllTeacherResponseDto>> UpdateTeacher(long id, SaveTeacherRequestDto dto);

        Task<ApiResponseDto<GetAllTeacherResponseDto>> DeleteTeacher(long id);

        Task<ApiResponseDto<TeacherAvailabilityDetailsResponseDto>> GetAvailability(long id);

        Task<ApiListResponseDto<TeacherAvailabilityResponseDto>> SaveAvailability(long id, SaveTeacherAvailabilityRequestDto dto);

        Task<ApiListResponseDto<GetAllPrioritiesResponseDto>> GetAllPriorities();

        Task<ApiResponseDto<GetAllPrioritiesResponseDto>> GetPriority(long id);

        Task<ApiResponseDto<GetAllPrioritiesResponseDto>> CreatePriority(SavePriorityRequestDto dto);

        Task<ApiResponseDto<GetAllPrioritiesResponseDto>> UpdatePriority(long id, SavePriorityRequestDto dto);

        Task<EmptyResponseDto> DeletePriority(long id);
    }
}
