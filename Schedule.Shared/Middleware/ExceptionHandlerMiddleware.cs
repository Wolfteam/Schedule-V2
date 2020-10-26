using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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

        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

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
                ApplicationType.ScheduleApi => AppMessageType.SchApiUnknownErrorOccurred,
                ApplicationType.IdentityServer => AppMessageType.IdsUnknownErrorOccurred,
                ApplicationType.ScheduleWeb => AppMessageType.SchWebUnknownErrorOccurred,
                _ => throw new ArgumentOutOfRangeException()
            };

            var invalidRequest = appUserManager.Application switch
            {
                ApplicationType.ScheduleApi => AppMessageType.SchApiInvalidRequest,
                ApplicationType.IdentityServer => AppMessageType.IdsInvalidRequest,
                ApplicationType.ScheduleWeb => AppMessageType.SchWebInvalidRequest,
                _ => throw new ArgumentOutOfRangeException()
            };

            var response = new EmptyResponseDto
            {
                ErrorMessage = unknownError.GetErrorMsg(),
                ErrorMessageId = unknownError.GetErrorCode(),
                ErrorMessageCode = (int)unknownError
            };
            context.Response.ContentType = "application/json";
            switch (exception)
            {
                case ValidationException validationEx:
                    code = HttpStatusCode.BadRequest;
                    response.ErrorMessageId = invalidRequest.GetErrorCode();
                    response.ErrorMessage = validationEx.Error;
                    response.ErrorMessageCode = (int)invalidRequest;
                    break;
                case NotFoundException notFoundEx:
                    code = HttpStatusCode.NotFound;
                    response.ErrorMessageId = notFoundEx.ErrorMessageId.GetErrorCode();
                    response.ErrorMessage = notFoundEx.Message;
                    response.ErrorMessageCode = (int)notFoundEx.ErrorMessageId;
                    break;
                case InvalidRequestException invEx:
                    code = HttpStatusCode.BadRequest;
                    response.ErrorMessageId = invEx.ErrorMessageId.GetErrorCode();
                    response.ErrorMessage = invEx.Message;
                    response.ErrorMessageCode = (int)invEx.ErrorMessageId;
                    break;
                default:
                    logger.LogError(exception, $"{nameof(HandleExceptionAsync)}: Unknown exception was captured");
                    break;
            }
#if DEBUG
            response.ErrorMessage += $". Ex: {exception}";
#endif

            context.Response.StatusCode = (int)code;

            logger.LogInformation(
                $"{nameof(HandleExceptionAsync)}: The final response is going to " +
                $"be = {response.ErrorMessageId} - {response.ErrorMessage}");

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response, SerializerSettings));
        }
    }
}
