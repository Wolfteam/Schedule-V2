using Refit;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Careers.Requests;
using Schedule.Domain.Dto.Careers.Responses;
using System.Threading.Tasks;

namespace Schedule.Web.Interfaces.Apis
{
    [Headers("Authorization: Bearer")]
    public interface ICareerApi
    {
        [Get("/api/Career")]
        Task<ApiListResponseDto<GetAllCareersResponseDto>> GetAllCareers();

        [Get("/api/Career/{id}")]
        Task<ApiResponseDto<GetAllCareersResponseDto>> GetCareer(long id);

        [Post("/api/Career")]
        Task<ApiResponseDto<GetAllCareersResponseDto>> CreateCareer(SaveCareerRequestDto dto);

        [Put("/api/Career/{id}")]
        Task<ApiResponseDto<GetAllCareersResponseDto>> UpdateCareer(long id, SaveCareerRequestDto dto);

        [Delete("/api/Career/{id}")]
        Task<EmptyResponseDto> DeleteCareer(long id);
    }
}
