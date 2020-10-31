using AutoMapper;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Priorities.Responses;
using Schedule.Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Priorities.Queries.Get
{
    public class GetPriorityQueryHandler : BaseApiRequestHandler<GetPriorityQuery, GetAllPrioritiesResponseDto>
    {
        private readonly IMapper _mapper;
        public GetPriorityQueryHandler(
            ILogger<GetPriorityQueryHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager,
            IMapper mapper)
            : base(logger, appDataService, appUserManager)
        {
            _mapper = mapper;
        }

        public override async Task<ApiResponseDto<GetAllPrioritiesResponseDto>> Handle(GetPriorityQuery request, CancellationToken cancellationToken)
        {
            var priority = await AppDataService.Priorities.FirstOrDefaultAsync(p => p.Id == request.Id && p.SchoolId == AppUserManager.SchoolId);
            if (priority != null)
                return new ApiResponseDto<GetAllPrioritiesResponseDto>(_mapper.Map<GetAllPrioritiesResponseDto>(priority));
            var msg = $"PriorityId = {request.Id} was not found";
            Logger.LogWarning($"{nameof(Handle)}:{msg}");
            throw new NotFoundException(msg);
        }
    }
}
