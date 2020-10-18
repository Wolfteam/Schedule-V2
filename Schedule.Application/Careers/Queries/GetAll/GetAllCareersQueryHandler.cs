using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Careers.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Careers.Queries.GetAll
{
    public class GetAllCareersQueryHandler : BasePaginatedRequestHandler<GetAllCareersQuery, GetAllCareersResponseDto>
    {
        public GetAllCareersQueryHandler(
            ILogger<GetAllCareersQueryHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager)
            : base(logger, appDataService, appUserManager)
        {
        }

        public override async Task<PaginatedResponseDto<GetAllCareersResponseDto>> Handle(GetAllCareersQuery request, CancellationToken cancellationToken)
        {
            var response = new PaginatedResponseDto<GetAllCareersResponseDto>();
            response.Result = await AppDataService.Careers.GetAll<GetAllCareersResponseDto>(AppUserManager.SchoolId, request.Dto, response);
            response.Succeed = true;
            return response;
        }
    }
}
