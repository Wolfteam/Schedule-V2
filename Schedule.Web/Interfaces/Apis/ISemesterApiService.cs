using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Semesters.Requests;
using Schedule.Domain.Dto.Semesters.Responses;
using System.Threading.Tasks;

namespace Schedule.Web.Interfaces.Apis
{
    public interface ISemesterApiService
    {
        Task<ApiListResponseDto<GetAllSemestersResponseDto>> GetAllSemesters();

        Task<ApiResponseDto<GetAllSemestersResponseDto>> GetSemester(long id);

        Task<ApiResponseDto<GetAllSemestersResponseDto>> CreateSemester(SaveSemesterRequestDto dto);

        Task<ApiResponseDto<GetAllSemestersResponseDto>> UpdateSemester(long id, SaveSemesterRequestDto dto);

        Task<EmptyResponseDto> DeleteSemester(long id);
    }
}
