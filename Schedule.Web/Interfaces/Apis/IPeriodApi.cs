using Refit;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Periods.Requests;
using Schedule.Domain.Dto.Periods.Responses;
using System.Threading.Tasks;

namespace Schedule.Web.Interfaces.Apis
{
    [Headers("Authorization: Bearer")]
    public interface IPeriodApi
    {
        [Get("/api/Period")]
        Task<PaginatedResponseDto<GetAllPeriodsResponseDto>> GetAllPeriods([Query] GetAllPeriodsRequestDto dto);

        [Get("/api/Period/{id}")]
        Task<ApiResponseDto<GetAllPeriodsResponseDto>> GetPeriod(long id);

        [Post("/api/Period")]
        Task<ApiResponseDto<GetAllPeriodsResponseDto>> CreatePeriod(SavePeriodRequestDto dto);

        [Post("/api/Period/{id}")]
        Task<ApiResponseDto<GetAllPeriodsResponseDto>> UpdatePeriod(long id, SavePeriodRequestDto dto);

        [Delete("/api/Period/{id}")]
        Task<EmptyResponseDto> DeletePeriod(long id);

        [Get("/api/Period/Current")]
        Task<ApiResponseDto<GetAllPeriodsResponseDto>> GetCurrentPeriod();
    }
}
