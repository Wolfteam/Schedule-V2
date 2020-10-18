using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Classrooms.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Classrooms.Queries.GetAll
{
    public class GetAllClassroomsQueryHandler : BasePaginatedRequestHandler<GetAllClassroomsQuery, GetAllClassroomsResponseDto>
    {
        public GetAllClassroomsQueryHandler(
            ILogger<GetAllClassroomsQueryHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager)
            : base(logger, appDataService, appUserManager)
        {
        }

        public override async Task<PaginatedResponseDto<GetAllClassroomsResponseDto>> Handle(GetAllClassroomsQuery request, CancellationToken cancellationToken)
        {
            var response = new PaginatedResponseDto<GetAllClassroomsResponseDto>();
            response.Result = await AppDataService.Classrooms.GetAll<GetAllClassroomsResponseDto>(AppUserManager.SchoolId, request.Dto, response);
            response.Succeed = true;
            return response;
        }
    }
}
