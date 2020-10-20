using Microsoft.Extensions.Logging;
using Refit;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Priorities.Requests;
using Schedule.Domain.Dto.Priorities.Responses;
using Schedule.Domain.Dto.Teachers.Requests;
using Schedule.Domain.Dto.Teachers.Responses;
using Schedule.Web.Interfaces.Apis;
using System;
using System.Threading.Tasks;

namespace Schedule.Web.Services.Api
{
    public class TeacherApiService : BaseApiService, ITeacherApiService
    {
        private readonly ITeacherApi _teacherApi;

        public TeacherApiService(ILogger<TeacherApiService> logger, ITeacherApi teacherApi) : base(logger)
        {
            _teacherApi = teacherApi;
        }

        public async Task<ApiListResponseDto<GetAllTeacherResponseDto>> GetAllTeachers()
        {
            var response = new ApiListResponseDto<GetAllTeacherResponseDto>();
            try
            {
                response = await _teacherApi.GetAllTeachers();
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(GetAllTeachers)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(GetAllTeachers)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(GetAllTeachers)}: Completed.");
            return response;
        }

        public async Task<ApiResponseDto<GetAllTeacherResponseDto>> CreateTeacher(SaveTeacherRequestDto dto)
        {
            var response = new ApiResponseDto<GetAllTeacherResponseDto>();
            try
            {
                response = await _teacherApi.CreateTeacher(dto);
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(CreateTeacher)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(CreateTeacher)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(CreateTeacher)}: Completed.");
            return response;
        }

        public async Task<ApiResponseDto<GetAllTeacherResponseDto>> UpdateTeacher(long id, SaveTeacherRequestDto dto)
        {
            var response = new ApiResponseDto<GetAllTeacherResponseDto>();
            try
            {
                response = await _teacherApi.UpdateTeacher(id, dto);
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(UpdateTeacher)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(UpdateTeacher)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(UpdateTeacher)}: Completed.");
            return response;
        }

        public async Task<ApiResponseDto<GetAllTeacherResponseDto>> DeleteTeacher(long id)
        {
            var response = new ApiResponseDto<GetAllTeacherResponseDto>();
            try
            {
                response = await _teacherApi.DeleteTeacher(id);
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(DeleteTeacher)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(DeleteTeacher)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(DeleteTeacher)}: Completed.");
            return response;
        }

        public async Task<ApiResponseDto<TeacherAvailabilityDetailsResponseDto>> GetAvailability(long id)
        {
            var response = new ApiResponseDto<TeacherAvailabilityDetailsResponseDto>();
            try
            {
                response = await _teacherApi.GetAvailability(id);
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(GetAvailability)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(GetAvailability)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(GetAvailability)}: Completed.");
            return response;
        }

        public async Task<ApiListResponseDto<TeacherAvailabilityResponseDto>> SaveAvailability(long id, SaveTeacherAvailabilityRequestDto dto)
        {
            var response = new ApiListResponseDto<TeacherAvailabilityResponseDto>();
            try
            {
                response = await _teacherApi.SaveAvailability(id, dto);
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(SaveAvailability)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(SaveAvailability)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(SaveAvailability)}: Completed.");
            return response;
        }

        public async Task<ApiListResponseDto<GetAllPrioritiesResponseDto>> GetAllPriorities()
        {
            var response = new ApiListResponseDto<GetAllPrioritiesResponseDto>();
            try
            {
                response = await _teacherApi.GetAllPriorities();
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(GetAllPriorities)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(GetAllPriorities)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(GetAllPriorities)}: Completed.");
            return response;
        }

        public async Task<ApiResponseDto<GetAllPrioritiesResponseDto>> CreatePriority(SavePriorityRequestDto dto)
        {
            var response = new ApiResponseDto<GetAllPrioritiesResponseDto>();
            try
            {
                response = await _teacherApi.CreatePriority(dto);
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(CreatePriority)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(CreatePriority)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(CreatePriority)}: Completed.");
            return response;
        }

        public async Task<ApiResponseDto<GetAllPrioritiesResponseDto>> UpdatePriority(long id, SavePriorityRequestDto dto)
        {
            var response = new ApiResponseDto<GetAllPrioritiesResponseDto>();
            try
            {
                response = await _teacherApi.UpdatePriority(id, dto);
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(UpdatePriority)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(UpdatePriority)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(UpdatePriority)}: Completed.");
            return response;
        }

        public async Task<EmptyResponseDto> DeletePriority(long id)
        {
            var response = new EmptyResponseDto();
            try
            {
                response = await _teacherApi.DeletePriority(id);
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(DeletePriority)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(DeletePriority)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(DeletePriority)}: Completed.");
            return response;
        }
    }
}
