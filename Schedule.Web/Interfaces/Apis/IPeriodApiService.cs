using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Periods.Requests;
using Schedule.Domain.Dto.Periods.Responses;
using System.Threading.Tasks;

namespace Schedule.Web.Interfaces.Apis
{
    public interface IPeriodApiService
    {
        Task<PaginatedResponseDto<GetAllPeriodsResponseDto>> GetAllPeriods(GetAllPeriodsRequestDto dto);

        Task<ApiResponseDto<GetAllPeriodsResponseDto>> CreatePeriod(SavePeriodRequestDto dto);

        Task<ApiResponseDto<GetAllPeriodsResponseDto>> UpdatePeriod(long id, SavePeriodRequestDto dto);

        Task<EmptyResponseDto> DeletePeriod(long id);

        Task<ApiResponseDto<GetAllPeriodsResponseDto>> GetCurrentPeriod();
    }
}
