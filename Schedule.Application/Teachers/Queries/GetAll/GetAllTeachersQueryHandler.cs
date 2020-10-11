using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Teachers.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Teachers.Queries.GetAll
{
    public class GetAllTeachersQueryHandler : BasePaginatedRequestHandler<GetAllTeachersQuery, GetAllTeacherResponseDto>
    {
        public GetAllTeachersQueryHandler(ILogger<GetAllTeachersQueryHandler> logger, IAppDataService appDataService)
            : base(logger, appDataService)
        {
        }

        public override async Task<PaginatedResponseDto<GetAllTeacherResponseDto>> Handle(GetAllTeachersQuery request, CancellationToken cancellationToken)
        {
            var response = new PaginatedResponseDto<GetAllTeacherResponseDto>();
            response.Result = await AppDataService.Teachers.GetAll<GetAllTeacherResponseDto>(request.Dto, response);
            response.Succeed = true;
            return response;
        }
    }
}
