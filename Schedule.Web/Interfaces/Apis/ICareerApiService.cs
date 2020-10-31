using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Careers.Requests;
using Schedule.Domain.Dto.Careers.Responses;
using System.Threading.Tasks;

namespace Schedule.Web.Interfaces.Apis
{
    public interface ICareerApiService
    {
        Task<ApiListResponseDto<GetAllCareersResponseDto>> GetAllCareers();

        Task<ApiResponseDto<GetAllCareersResponseDto>> GetCareer(long id);

        Task<ApiResponseDto<GetAllCareersResponseDto>> CreateCareer(SaveCareerRequestDto dto);

        Task<ApiResponseDto<GetAllCareersResponseDto>> UpdateCareer(long id, SaveCareerRequestDto dto);

        Task<EmptyResponseDto> DeleteCareer(long id);
    }
}
