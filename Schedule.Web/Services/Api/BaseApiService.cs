using Microsoft.Extensions.Logging;
using Refit;
using Schedule.Domain.Dto;
using Schedule.Domain.Enums;
using Schedule.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Web.Services.Api
{
    public abstract class BaseApiService
    {
        protected readonly ILogger Logger;

        protected BaseApiService(ILogger logger)
        {
            Logger = logger;
        }

        protected async Task<List<T>> HandleApiException<T>(
            ApiException ex,
            List<T> responses,
            AppMessageType defaultError = AppMessageType.SchWebUnknownErrorOccurred)
            where T : EmptyResponseDto, new()
        {
            if (responses is null || responses.Count == 0)
            {
                responses = new List<T>
                {
                    new T()
                };
            }
            foreach (var response in responses)
            {
                await HandleApiException(ex, response, defaultError);
            }
            return responses;
        }

        protected List<T> HandleUnknownException<T>(
            List<T> responses,
            AppMessageType defaultError = AppMessageType.SchWebUnknownErrorOccurred)
            where T : EmptyResponseDto, new()
        {
            if (responses is null || responses.Count == 0)
            {
                responses = new List<T>
                {
                    new T()
                };
            }
            foreach (var response in responses)
            {
                HandleUnknownException(response, defaultError);
            }
            return responses;
        }

        protected async Task HandleApiException<T>(
            ApiException ex,
            T response,
            AppMessageType defaultError = AppMessageType.SchWebUnknownErrorOccurred)
            where T : EmptyResponseDto
        {
            try
            {
                Logger.LogError(ex, $"{nameof(HandleApiException)}: Handling api exception...");
                var error = await TryGetApiResponse(ex);
                //If for some reason, we cant get an error response, lets set a default one
                if (error is null)
                {
                    Logger.LogError(ex,
                        $"{nameof(HandleApiException)}: Response doesn't have a body, " +
                        $"so this may be an error produced by this app");
                    HandleUnknownException(response, defaultError);
                }
                else
                {
                    Logger.LogError(ex,
                        $"{nameof(HandleApiException)}: Response does have a body, " +
                        $"Error = {error.ErrorMessage} - {error.ErrorMessageId}");
                    response.ErrorMessage = error.ErrorMessageId;
                    response.ErrorMessageId = error.ErrorMessageId;
                    response.ErrorMessageCode = error.ErrorMessageCode;
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e,
                    $"{nameof(HandleApiException)}: Couldn't get api empty response dto, " +
                    "the api may be returning a list or something different");
                HandleUnknownException(response, defaultError);
            }
        }

        protected void HandleUnknownException<T>(
            T response,
            AppMessageType defaultError = AppMessageType.SchWebUnknownErrorOccurred)
            where T : EmptyResponseDto
        {
            response.ErrorMessage = defaultError.GetErrorMsg();
            response.ErrorMessageId = defaultError.GetErrorCode();
        }

        private async Task<EmptyResponseDto> TryGetApiResponse(ApiException ex)
        {
            Logger.LogInformation($"{nameof(TryGetApiResponse)}: Checking if response is an empty response...");
            try
            {
                var error = await ex.GetContentAsAsync<EmptyResponseDto>();
                return error;
            }
            catch (Exception)
            {
                Logger.LogInformation($"{nameof(TryGetApiResponse)}: Response is not an empty response");
            }

            Logger.LogInformation($"{nameof(TryGetApiResponse)}: Checking if response is a list of empty response...");
            try
            {
                var error = await ex.GetContentAsAsync<List<EmptyResponseDto>>();
                return error.FirstOrDefault();
            }
            catch (Exception)
            {
                Logger.LogInformation($"{nameof(TryGetApiResponse)}: Response is not a list of empty response...");
            }

            return null;
        }
    }
}
