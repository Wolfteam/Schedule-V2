using Microsoft.Extensions.Logging;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Careers.Requests;
using Schedule.Domain.Dto.Careers.Responses;
using Schedule.Web.Interfaces.Apis;
using System;
using System.Threading.Tasks;
using Refit;

namespace Schedule.Web.Services.Api
{
    public class CareerApiService : BaseApiService, ICareerApiService
    {
        private readonly ICareerApi _careerApi;
        public CareerApiService(ILogger<CareerApiService> logger, ICareerApi careerApi) : base(logger)
        {
            _careerApi = careerApi;
        }

        public async Task<ApiListResponseDto<GetAllCareersResponseDto>> GetAllCareers()
        {
            var response = new ApiListResponseDto<GetAllCareersResponseDto>();
            try
            {
                response = await _careerApi.GetAllCareers();
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(GetAllCareers)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(GetAllCareers)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(GetAllCareers)}: Completed.");
            return response;
        }

        public async Task<ApiResponseDto<GetAllCareersResponseDto>> GetCareer(long id)
        {
            var response = new ApiResponseDto<GetAllCareersResponseDto>();
            try
            {
                response = await _careerApi.GetCareer(id);
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(GetCareer)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(GetCareer)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(GetCareer)}: Completed.");
            return response;
        }

        public async Task<ApiResponseDto<GetAllCareersResponseDto>> CreateCareer(SaveCareerRequestDto dto)
        {
            var response = new ApiResponseDto<GetAllCareersResponseDto>();
            try
            {
                response = await _careerApi.CreateCareer(dto);
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(CreateCareer)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(CreateCareer)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(CreateCareer)}: Completed.");
            return response;
        }

        public async Task<ApiResponseDto<GetAllCareersResponseDto>> UpdateCareer(long id, SaveCareerRequestDto dto)
        {
            var response = new ApiResponseDto<GetAllCareersResponseDto>();
            try
            {
                response = await _careerApi.UpdateCareer(id, dto);
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(UpdateCareer)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(UpdateCareer)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(UpdateCareer)}: Completed.");
            return response;
        }

        public async Task<EmptyResponseDto> DeleteCareer(long id)
        {
            var response = new EmptyResponseDto();
            try
            {
                response = await _careerApi.DeleteCareer(id);
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(DeleteCareer)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(DeleteCareer)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(DeleteCareer)}: Completed.");
            return response;
        }
    }
}
