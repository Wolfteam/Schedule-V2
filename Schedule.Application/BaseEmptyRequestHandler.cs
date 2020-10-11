using MediatR;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application
{
    public abstract class BaseEmptyRequestHandler<TQuery> : IRequestHandler<TQuery, EmptyResponseDto>
        where TQuery : IRequest<EmptyResponseDto>
    {
        protected readonly ILogger Logger;
        protected readonly IAppDataService AppDataService;

        protected BaseEmptyRequestHandler(ILogger logger, IAppDataService appDataService)
        {
            Logger = logger;
            AppDataService = appDataService;
        }

        public abstract Task<EmptyResponseDto> Handle(TQuery request, CancellationToken cancellationToken);
    }
}
