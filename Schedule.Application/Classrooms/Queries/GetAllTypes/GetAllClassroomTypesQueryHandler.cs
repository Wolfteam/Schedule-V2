using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Classrooms.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Classrooms.Queries.GetAllTypes
{
    public class GetAllClassroomTypesQueryHandler : BasePaginatedRequestHandler<GetAllClassroomTypesQuery, GetAllClassroomTypesResponseDto>
    {
        public GetAllClassroomTypesQueryHandler(
            ILogger<GetAllClassroomTypesQueryHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager)
            : base(logger, appDataService, appUserManager)
        {
        }

        public override async Task<PaginatedResponseDto<GetAllClassroomTypesResponseDto>> Handle(
            GetAllClassroomTypesQuery request,
            CancellationToken cancellationToken)
        {
            var response = new PaginatedResponseDto<GetAllClassroomTypesResponseDto>();
            response.Result = await AppDataService.ClassroomSubject.GetAll<GetAllClassroomTypesResponseDto>(AppUserManager.SchoolId, request.Dto, response);
            response.Succeed = true;
            return response;
        }
    }
}
