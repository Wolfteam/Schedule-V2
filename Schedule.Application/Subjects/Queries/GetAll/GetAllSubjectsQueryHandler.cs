using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Subjects.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Subjects.Queries.GetAll
{
    public class GetAllSubjectsQueryHandler : BasePaginatedRequestHandler<GetAllSubjectsQuery, GetAllSubjectsResponseDto>
    {
        public GetAllSubjectsQueryHandler(
            ILogger<GetAllSubjectsQueryHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager)
            : base(logger, appDataService, appUserManager)
        {
        }

        public override async Task<PaginatedResponseDto<GetAllSubjectsResponseDto>> Handle(GetAllSubjectsQuery request, CancellationToken cancellationToken)
        {
            var response = new PaginatedResponseDto<GetAllSubjectsResponseDto>();
            response.Result = await AppDataService.Subjects.GetAll<GetAllSubjectsResponseDto>(AppUserManager.SchoolId, request.Dto, response);
            response.Succeed = true;
            return response;
        }
    }
}
