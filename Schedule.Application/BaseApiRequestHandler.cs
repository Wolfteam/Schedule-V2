using MediatR;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application
{
    public abstract class BaseApiRequestHandler<TQuery, TResponse> : IRequestHandler<TQuery, ApiResponseDto<TResponse>>
        where TQuery : IRequest<ApiResponseDto<TResponse>>
    {
        protected readonly ILogger Logger;
        protected readonly IAppDataService AppDataService;
        protected readonly IAppUserManager AppUserManager;

        protected BaseApiRequestHandler(ILogger logger, IAppDataService appDataService, IAppUserManager appUserManager)
        {
            Logger = logger;
            AppDataService = appDataService;
            AppUserManager = appUserManager;
        }

        public abstract Task<ApiResponseDto<TResponse>> Handle(TQuery request, CancellationToken cancellationToken);
    }
}
