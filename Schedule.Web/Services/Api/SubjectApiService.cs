using Microsoft.Extensions.Logging;
using Refit;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Subjects.Requests;
using Schedule.Domain.Dto.Subjects.Responses;
using Schedule.Web.Interfaces.Apis;
using System;
using System.Threading.Tasks;

namespace Schedule.Web.Services.Api
{
    public class SubjectApiService : BaseApiService, ISubjectApiService
    {
        private readonly ISubjectApi _subjectApi;

        public SubjectApiService(ILogger<SubjectApiService> logger, ISubjectApi subjectApi) : base(logger)
        {
            _subjectApi = subjectApi;
        }

        public async Task<PaginatedResponseDto<GetAllSubjectsResponseDto>> GetAllSubjects(GetAllSubjectsRequestDto dto)
        {
            var response = new PaginatedResponseDto<GetAllSubjectsResponseDto>();
            try
            {
                response = await _subjectApi.GetAllSubjects(dto);
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(GetAllSubjects)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(GetAllSubjects)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(GetAllSubjects)}: Completed.");
            return response;
        }

        public async Task<ApiResponseDto<GetAllSubjectsResponseDto>> GetSubject(long id)
        {
            var response = new ApiResponseDto<GetAllSubjectsResponseDto>();
            try
            {
                response = await _subjectApi.GetSubject(id);
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(GetSubject)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(GetSubject)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(GetSubject)}: Completed.");
            return response;
        }

        public async Task<ApiResponseDto<GetAllSubjectsResponseDto>> CreateSubject(SaveSubjectRequestDto dto)
        {
            var response = new ApiResponseDto<GetAllSubjectsResponseDto>();
            try
            {
                response = await _subjectApi.CreateSubject(dto);
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(CreateSubject)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(CreateSubject)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(CreateSubject)}: Completed.");
            return response;
        }

        public async Task<ApiResponseDto<GetAllSubjectsResponseDto>> UpdateSubject(long id, SaveSubjectRequestDto dto)
        {
            var response = new ApiResponseDto<GetAllSubjectsResponseDto>();
            try
            {
                response = await _subjectApi.UpdateSubject(id, dto);
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(UpdateSubject)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(UpdateSubject)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(UpdateSubject)}: Completed.");
            return response;
        }

        public async Task<EmptyResponseDto> DeleteSubject(long id)
        {
            var response = new EmptyResponseDto();
            try
            {
                response = await _subjectApi.DeleteSubject(id);
            }
            catch (ApiException apiEx)
            {
                Logger.LogError(apiEx, $"{nameof(DeleteSubject)}: Api exception occurred");
                await HandleApiException(apiEx, response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{nameof(DeleteSubject)}: Unknown error occurred");
                HandleUnknownException(response);
            }

            Logger.LogInformation($"{nameof(DeleteSubject)}: Completed.");
            return response;
        }
    }
}
