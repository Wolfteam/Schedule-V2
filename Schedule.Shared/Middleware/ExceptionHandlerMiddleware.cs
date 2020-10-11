using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Schedule.Domain.Dto;
using Schedule.Domain.Enums;
using Schedule.Domain.Interfaces.Managers;
using Schedule.Shared.Exceptions;
using Schedule.Shared.Extensions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Schedule.Shared.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<ExceptionHandlerMiddleware> logger, IDefaultAppUserManager userManager)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e, logger, userManager);
            }
        }

        private Task HandleExceptionAsync(
            HttpContext context,
            Exception exception,
            ILogger logger,
            IDefaultAppUserManager appUserManager)
        {
            logger.LogInformation($"{nameof(HandleExceptionAsync)}: Handling exception of type = {exception.GetType()}....");

            var code = HttpStatusCode.InternalServerError;

            var unknownError = appUserManager.Application switch
            {
                ApplicationType.Schedule => AppMessageType.SchUnknownErrorOccurred,
                ApplicationType.IdentityServer => AppMessageType.IdsUnknownErrorOccurred,
                _ => throw new ArgumentOutOfRangeException()
            };

            var invalidRequest = appUserManager.Application switch
            {
                ApplicationType.Schedule => AppMessageType.SchInvalidRequest,
                ApplicationType.IdentityServer => AppMessageType.IdsInvalidRequest,
                _ => throw new ArgumentOutOfRangeException()
            };

            var response = new EmptyResponseDto
            {
                ErrorMessage = unknownError.GetErrorMsg(),
                ErrorMessageId = unknownError.GetErrorCode()
            };
            context.Response.ContentType = "application/json";
            switch (exception)
            {
                case ValidationException validationEx:
                    code = HttpStatusCode.BadRequest;
                    response.ErrorMessageId = invalidRequest.GetErrorCode();
                    response.ErrorMessage = validationEx.Error;
                    break;
                case NotFoundException notFoundEx:
                    code = HttpStatusCode.NotFound;
                    response.ErrorMessageId = notFoundEx.ErrorMessageId.GetErrorCode();
                    response.ErrorMessage = notFoundEx.Message;
                    break;
                case InvalidRequestException invEx:
                    code = HttpStatusCode.BadRequest;
                    response.ErrorMessageId = invEx.ErrorMessageId.GetErrorCode();
                    response.ErrorMessage = invEx.Message;
                    break;
                default:
                    logger.LogError(exception, $"{nameof(HandleExceptionAsync)}: Unknown exception was captured");
                    break;
            }
            context.Response.StatusCode = (int)code;

            logger.LogInformation(
                $"{nameof(HandleExceptionAsync)}: The final response is going to " +
                $"be = {response.ErrorMessageId} - {response.ErrorMessage}");

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
