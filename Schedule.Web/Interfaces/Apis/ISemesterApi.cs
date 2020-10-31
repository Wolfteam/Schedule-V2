using Refit;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Semesters.Requests;
using Schedule.Domain.Dto.Semesters.Responses;
using System.Threading.Tasks;

namespace Schedule.Web.Interfaces.Apis
{
    [Headers("Authorization: Bearer")]
    public interface ISemesterApi
    {
        [Get("/api/Semester")]
        Task<PaginatedResponseDto<GetAllSemestersResponseDto>> GetAllSemesters();

        [Get("/api/Semester/{id}")]
        Task<ApiResponseDto<GetAllSemestersResponseDto>> GetSemester(long id);

        [Post("/api/Semester")]
        Task<ApiResponseDto<GetAllSemestersResponseDto>> CreateSemester(SaveSemesterRequestDto dto);

        [Put("/api/Semester/{id}")]
        Task<ApiResponseDto<GetAllSemestersResponseDto>> UpdateSemester(long id, SaveSemesterRequestDto dto);

        [Delete("/api/Semester/{id}")]
        Task<EmptyResponseDto> DeleteSemester(long id);
    }
}
