using Refit;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Classrooms.Requests;
using Schedule.Domain.Dto.Classrooms.Responses;
using System.Threading.Tasks;

namespace Schedule.Web.Interfaces.Apis
{
    [Headers("Authorization: Bearer")]
    public interface IClassroomApi
    {
        [Get("/api/Classroom")]
        Task<PaginatedResponseDto<GetAllClassroomsResponseDto>> GetAllClassrooms([Query] GetAllClassroomsRequestDto dto);

        [Get("/api/Classroom/{id}")]
        Task<ApiResponseDto<GetAllClassroomsResponseDto>> GetClassroom(long id);

        [Post("/api/Classroom")]
        Task<ApiResponseDto<GetAllClassroomsResponseDto>> CreateClassroom(SaveClassroomRequestDto dto);

        [Put("/api/Classroom/{id}")]
        Task<ApiResponseDto<GetAllClassroomsResponseDto>> UpdateClassroom(long id, SaveClassroomRequestDto dto);

        [Delete("/api/Classroom/{id}")]
        Task<EmptyResponseDto> DeleteClassroom(long id);

        [Get("/api/Classroom/Types")]
        Task<PaginatedResponseDto<GetAllClassroomTypesResponseDto>> GetAllClassroomTypes([Query] GetAllClassroomTypesRequestDto dto);

        [Get("/api/Classroom/Types/{id}")]
        Task<ApiResponseDto<GetAllClassroomTypesResponseDto>> GetClassroomType(long id);

        [Post("/api/Classroom/Types")]
        Task<ApiResponseDto<GetAllClassroomTypesResponseDto>> CreateClassroomType(SaveClassroomTypeRequestDto dto);

        [Put("/api/Classroom/Types/{id}")]
        Task<ApiResponseDto<GetAllClassroomTypesResponseDto>> UpdateClassroomType(long id, SaveClassroomTypeRequestDto dto);

        [Delete("/api/Classroom/Types")]
        Task<EmptyResponseDto> DeleteClassroomType(long id);
    }
}
