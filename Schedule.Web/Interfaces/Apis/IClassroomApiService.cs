using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Classrooms.Requests;
using Schedule.Domain.Dto.Classrooms.Responses;
using System.Threading.Tasks;

namespace Schedule.Web.Interfaces.Apis
{
    public interface IClassroomApiService
    {
        Task<PaginatedResponseDto<GetAllClassroomsResponseDto>> GetAllClassrooms(GetAllClassroomsRequestDto dto);

        Task<ApiResponseDto<GetAllClassroomsResponseDto>> GetClassroom(long id);

        Task<ApiResponseDto<GetAllClassroomsResponseDto>> CreateClassroom(SaveClassroomRequestDto dto);

        Task<ApiResponseDto<GetAllClassroomsResponseDto>> UpdateClassroom(long id, SaveClassroomRequestDto dto);

        Task<EmptyResponseDto> DeleteClassroom(long id);

        Task<PaginatedResponseDto<GetAllClassroomTypesResponseDto>> GetAllClassroomTypes(GetAllClassroomTypesRequestDto dto);

        Task<ApiResponseDto<GetAllClassroomTypesResponseDto>> GetClassroomType(long id);

        Task<ApiResponseDto<GetAllClassroomTypesResponseDto>> CreateClassroomType(SaveClassroomTypeRequestDto dto);

        Task<ApiResponseDto<GetAllClassroomTypesResponseDto>> UpdateClassroomType(long id, SaveClassroomTypeRequestDto dto);

        Task<EmptyResponseDto> DeleteClassroomType(long id);
    }
}
