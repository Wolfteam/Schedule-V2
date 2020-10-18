using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Periods.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Periods.Queries.GetAll
{
    public class GetAllPeriodsQueryHandler : BasePaginatedRequestHandler<GetAllPeriodsQuery, GetAllPeriodsResponseDto>
    {
        public GetAllPeriodsQueryHandler(
            ILogger<GetAllPeriodsQueryHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager)
            : base(logger, appDataService, appUserManager)
        {
        }

        public override async Task<PaginatedResponseDto<GetAllPeriodsResponseDto>> Handle(GetAllPeriodsQuery request, CancellationToken cancellationToken)
        {
            var response = new PaginatedResponseDto<GetAllPeriodsResponseDto>();
            response.Result = await AppDataService.Periods.GetAll<GetAllPeriodsResponseDto>(AppUserManager.SchoolId, request.Dto, response);
            response.Succeed = true;
            return response;
        }
    }
}
