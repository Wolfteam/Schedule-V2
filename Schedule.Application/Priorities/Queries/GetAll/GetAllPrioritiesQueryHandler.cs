using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Priorities.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Priorities.Queries.GetAll
{
    public class GetAllPrioritiesQueryHandler : BasePaginatedRequestHandler<GetAllPrioritiesQuery, GetAllPrioritiesResponseDto>
    {
        public GetAllPrioritiesQueryHandler(
            ILogger<GetAllPrioritiesQueryHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager)
            : base(logger, appDataService, appUserManager)
        {
        }

        public override async Task<PaginatedResponseDto<GetAllPrioritiesResponseDto>> Handle(GetAllPrioritiesQuery request, CancellationToken cancellationToken)
        {
            var response = new PaginatedResponseDto<GetAllPrioritiesResponseDto>();
            response.Result = await AppDataService.Priorities.GetAll<GetAllPrioritiesResponseDto>(AppUserManager.SchoolId, request.Dto, response);
            response.Succeed = true;
            return response;
        }
    }
}
