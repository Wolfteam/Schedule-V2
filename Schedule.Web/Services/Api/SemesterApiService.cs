using Microsoft.Extensions.Logging;
using Refit;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Semesters.Requests;
using Schedule.Domain.Dto.Semesters.Responses;
using Schedule.Web.Interfaces.Apis;
using System;
using System.Threading.Tasks;

namespace Schedule.Web.Services.Api
{
    public class SemesterApiService : BaseApiService, ISemesterApiService
    {
        private readonly ISemesterApi _semesterApi;
        public SemesterApiService(ILogger<SemesterApiService> logger, ISemesterApi semesterApi) : base(logger)
        {
            _semesterApi = semesterApi;
        }

        public async Task<ApiListResponseDto<GetAllSemestersResponseDto>> GetAllSemesters()
        {
            var response = new ApiListResponseDto<GetAllSemestersResponseDto>();
            try
            {
                response = await _semesterApi.GetAllSemesters();
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(GetAllSemesters)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(GetAllSemesters)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(GetAllSemesters)}: Completed.");
            return response;
        }

        public async Task<ApiResponseDto<GetAllSemestersResponseDto>> GetSemester(long id)
        {
            var response = new ApiResponseDto<GetAllSemestersResponseDto>();
            try
            {
                response = await _semesterApi.GetSemester(id);
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(GetSemester)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(GetSemester)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(GetSemester)}: Completed.");
            return response;
        }

        public async Task<ApiResponseDto<GetAllSemestersResponseDto>> CreateSemester(SaveSemesterRequestDto dto)
        {
            var response = new ApiResponseDto<GetAllSemestersResponseDto>();
            try
            {
                response = await _semesterApi.CreateSemester(dto);
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(CreateSemester)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(CreateSemester)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(CreateSemester)}: Completed.");
            return response;
        }

        public async Task<ApiResponseDto<GetAllSemestersResponseDto>> UpdateSemester(long id, SaveSemesterRequestDto dto)
        {
            var response = new ApiResponseDto<GetAllSemestersResponseDto>();
            try
            {
                response = await _semesterApi.UpdateSemester(id, dto);
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(UpdateSemester)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(UpdateSemester)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(UpdateSemester)}: Completed.");
            return response;
        }

        public async Task<EmptyResponseDto> DeleteSemester(long id)
        {
            var response = new EmptyResponseDto();
            try
            {
                response = await _semesterApi.DeleteSemester(id);
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(UpdateSemester)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(UpdateSemester)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(UpdateSemester)}: Completed.");
            return response;
        }
    }
}
