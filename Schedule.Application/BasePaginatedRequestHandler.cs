using MediatR;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application
{
    public abstract class BasePaginatedRequestHandler<TQuery, TResponse> : IRequestHandler<TQuery, PaginatedResponseDto<TResponse>>
        where TQuery : IRequest<PaginatedResponseDto<TResponse>>
    {
        protected readonly ILogger Logger;
        protected readonly IAppDataService AppDataService;
        protected readonly IAppUserManager AppUserManager;

        protected BasePaginatedRequestHandler(ILogger logger, IAppDataService appDataService, IAppUserManager appUserManager)
        {
            Logger = logger;
            AppDataService = appDataService;
            AppUserManager = appUserManager;
        }

        public abstract Task<PaginatedResponseDto<TResponse>> Handle(TQuery request, CancellationToken cancellationToken);
    }
}
