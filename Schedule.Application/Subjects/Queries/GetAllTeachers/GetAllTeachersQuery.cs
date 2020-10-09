using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Services;
using Schedule.Shared.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Subjects.Queries.GetAllTeachers
{
    public class GetAllTeachersQuery : BasePaginatedQuery<GetAllTeacherResponseDto>
    {
    }

    public class GetAllTeachersHandler : BasePaginatedHandler<GetAllTeachersQuery, GetAllTeacherResponseDto>
    {
        private readonly ILogger<GetAllTeachersHandler> _logger;
        private readonly IAppDataService _dataService;

        public GetAllTeachersHandler(ILogger<GetAllTeachersHandler> logger, IAppDataService dataService)
        {
            _logger = logger;
            _dataService = dataService;
        }

        public override async Task<PaginatedResponseDto<GetAllTeacherResponseDto>> Handle(GetAllTeachersQuery request, CancellationToken cancellationToken)
        {
            var response = new PaginatedResponseDto<GetAllTeacherResponseDto>();
            response.Result = await _dataService.Teachers.GetAll<GetAllTeacherResponseDto>(request, response);
            return response;
        }
    }
}
