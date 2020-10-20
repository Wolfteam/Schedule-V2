using Microsoft.Extensions.Logging;
using Refit;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Classrooms.Requests;
using Schedule.Domain.Dto.Classrooms.Responses;
using Schedule.Web.Interfaces.Apis;
using System;
using System.Threading.Tasks;

namespace Schedule.Web.Services.Api
{
    public class ClassroomApiService : BaseApiService, IClassroomApiService
    {
        private readonly IClassroomApi _classroomApi;
        public ClassroomApiService(ILogger<ClassroomApiService> logger, IClassroomApi classroomApi) : base(logger)
        {
            _classroomApi = classroomApi;
        }

        public async Task<PaginatedResponseDto<GetAllClassroomsResponseDto>> GetAllClassrooms(GetAllClassroomsRequestDto dto)
        {
            var response = new PaginatedResponseDto<GetAllClassroomsResponseDto>();
            try
            {
                response = await _classroomApi.GetAllClassrooms(dto);
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(GetAllClassrooms)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(GetAllClassrooms)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(GetAllClassrooms)}: Completed.");
            return response;
        }

        public async Task<ApiResponseDto<GetAllClassroomsResponseDto>> CreateClassroom(SaveClassroomRequestDto dto)
        {
            var response = new ApiResponseDto<GetAllClassroomsResponseDto>();
            try
            {
                response = await _classroomApi.CreateClassroom(dto);
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(CreateClassroom)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(CreateClassroom)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(CreateClassroom)}: Completed.");
            return response;
        }

        public async Task<ApiResponseDto<GetAllClassroomsResponseDto>> UpdateClassroom(long id, SaveClassroomRequestDto dto)
        {
            var response = new ApiResponseDto<GetAllClassroomsResponseDto>();
            try
            {
                response = await _classroomApi.UpdateClassroom(id, dto);
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(UpdateClassroom)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(UpdateClassroom)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(UpdateClassroom)}: Completed.");
            return response;
        }

        public async Task<EmptyResponseDto> DeleteClassroom(long id)
        {
            var response = new EmptyResponseDto();
            try
            {
                response = await _classroomApi.DeleteClassroom(id);
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(DeleteClassroom)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(DeleteClassroom)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(DeleteClassroom)}: Completed.");
            return response;
        }

        public async Task<PaginatedResponseDto<GetAllClassroomTypesResponseDto>> GetAllClassroomTypes(GetAllClassroomTypesRequestDto dto)
        {
            var response = new PaginatedResponseDto<GetAllClassroomTypesResponseDto>();
            try
            {
                response = await _classroomApi.GetAllClassroomTypes(dto);
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(GetAllClassroomTypes)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(GetAllClassroomTypes)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(GetAllClassroomTypes)}: Completed.");
            return response;
        }
    }
}
