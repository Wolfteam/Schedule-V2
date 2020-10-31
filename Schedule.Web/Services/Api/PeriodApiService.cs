using Microsoft.Extensions.Logging;
using Refit;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Periods.Requests;
using Schedule.Domain.Dto.Periods.Responses;
using Schedule.Web.Interfaces.Apis;
using System;
using System.Threading.Tasks;

namespace Schedule.Web.Services.Api
{
    public class PeriodApiService : BaseApiService, IPeriodApiService
    {
        private readonly IPeriodApi _periodApi;

        public PeriodApiService(ILogger<PeriodApiService> logger, IPeriodApi periodApi) : base(logger)
        {
            _periodApi = periodApi;
        }

        public async Task<PaginatedResponseDto<GetAllPeriodsResponseDto>> GetAllPeriods(GetAllPeriodsRequestDto dto)
        {
            var response = new PaginatedResponseDto<GetAllPeriodsResponseDto>();
            try
            {
                response = await _periodApi.GetAllPeriods(dto);
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(GetAllPeriods)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(GetAllPeriods)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(GetAllPeriods)}: Completed.");
            return response;
        }

        public async Task<ApiResponseDto<GetAllPeriodsResponseDto>> GetPeriod(long id)
        {
            var response = new ApiResponseDto<GetAllPeriodsResponseDto>();
            try
            {
                response = await _periodApi.GetPeriod(id);
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(GetPeriod)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(GetPeriod)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(GetPeriod)}: Completed.");
            return response;
        }

        public async Task<ApiResponseDto<GetAllPeriodsResponseDto>> CreatePeriod(SavePeriodRequestDto dto)
        {
            var response = new ApiResponseDto<GetAllPeriodsResponseDto>();
            try
            {
                response = await _periodApi.CreatePeriod(dto);
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(CreatePeriod)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(CreatePeriod)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(CreatePeriod)}: Completed.");
            return response;
        }

        public async Task<ApiResponseDto<GetAllPeriodsResponseDto>> UpdatePeriod(long id, SavePeriodRequestDto dto)
        {
            var response = new ApiResponseDto<GetAllPeriodsResponseDto>();
            try
            {
                response = await _periodApi.UpdatePeriod(id, dto);
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(UpdatePeriod)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(UpdatePeriod)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(UpdatePeriod)}: Completed.");
            return response;
        }

        public async Task<EmptyResponseDto> DeletePeriod(long id)
        {
            var response = new EmptyResponseDto();
            try
            {
                response = await _periodApi.DeletePeriod(id);
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(DeletePeriod)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(DeletePeriod)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(DeletePeriod)}: Completed.");
            return response;
        }

        public async Task<ApiResponseDto<GetAllPeriodsResponseDto>> GetCurrentPeriod()
        {
            var response = new ApiResponseDto<GetAllPeriodsResponseDto>();
            try
            {
                response = await _periodApi.GetCurrentPeriod();
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(GetCurrentPeriod)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(GetCurrentPeriod)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(GetCurrentPeriod)}: Completed.");
            return response;
        }
    }
}
