using AutoMapper;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Priorities.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Priorities.Queries.GetAll
{
    public class GetAllPrioritiesQueryHandler : BaseApiListRequestHandler<GetAllPrioritiesQuery, GetAllPrioritiesResponseDto>
    {
        private readonly IMapper _mapper;
        public GetAllPrioritiesQueryHandler(
            ILogger<GetAllPrioritiesQueryHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager,
            IMapper mapper)
            : base(logger, appDataService, appUserManager)
        {
            _mapper = mapper;
        }

        public override async Task<ApiListResponseDto<GetAllPrioritiesResponseDto>> Handle(GetAllPrioritiesQuery request, CancellationToken cancellationToken)
        {
            var response = new PaginatedResponseDto<GetAllPrioritiesResponseDto>();
            var priorities = await AppDataService.Priorities.GetAllAsync(p => p.SchoolId == AppUserManager.SchoolId);
            response.Result = _mapper.Map<List<GetAllPrioritiesResponseDto>>(priorities);
            response.Succeed = true;
            return response;
        }
    }
}
