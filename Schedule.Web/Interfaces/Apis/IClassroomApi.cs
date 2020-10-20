using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Classrooms.Requests;
using Schedule.Domain.Dto.Classrooms.Responses;
using System.Threading.Tasks;
using Refit;

namespace Schedule.Web.Interfaces.Apis
{
    [Headers("Authorization: Bearer")]
    public interface IClassroomApi
    {
        [Get("/api/Classroom")]
        Task<PaginatedResponseDto<GetAllClassroomsResponseDto>> GetAllClassrooms([Query] GetAllClassroomsRequestDto dto);

        [Post("/api/Classroom")]
        Task<ApiResponseDto<GetAllClassroomsResponseDto>> CreateClassroom(SaveClassroomRequestDto dto);

        [Put("/api/Classroom/{id}")]
        Task<ApiResponseDto<GetAllClassroomsResponseDto>> UpdateClassroom(long id, SaveClassroomRequestDto dto);

        [Delete("/api/Classroom/{id}")]
        Task<EmptyResponseDto> DeleteClassroom(long id);

        [Get("/api/Classroom/Types")]
        Task<PaginatedResponseDto<GetAllClassroomTypesResponseDto>> GetAllClassroomTypes([Query] GetAllClassroomTypesRequestDto dto);
    }
}
