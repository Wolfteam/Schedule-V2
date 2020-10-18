using MediatR;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application
{
    public abstract class BaseApiListRequestHandler<TQuery, TResponse> : IRequestHandler<TQuery, ApiListResponseDto<TResponse>>
        where TQuery : IRequest<ApiListResponseDto<TResponse>>
    {
        protected readonly ILogger Logger;
        protected readonly IAppDataService AppDataService;
        protected readonly IAppUserManager AppUserManager;

        protected BaseApiListRequestHandler(ILogger logger, IAppDataService appDataService, IAppUserManager appUserManager)
        {
            Logger = logger;
            AppDataService = appDataService;
            AppUserManager = appUserManager;
        }

        public abstract Task<ApiListResponseDto<TResponse>> Handle(TQuery request, CancellationToken cancellationToken);
    }
}
