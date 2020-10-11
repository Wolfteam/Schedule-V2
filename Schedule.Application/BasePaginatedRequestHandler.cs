using MediatR;
using Microsoft.Extensions.Logging;
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

        protected BasePaginatedRequestHandler(ILogger logger, IAppDataService appDataService)
        {
            Logger = logger;
            AppDataService = appDataService;
        }

        public abstract Task<PaginatedResponseDto<TResponse>> Handle(TQuery request, CancellationToken cancellationToken);
    }
}
