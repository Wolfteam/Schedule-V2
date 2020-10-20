using AutoMapper;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Periods.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Periods.Queries.Current
{
    public class GetCurrentPeriodQueryHandler : BaseApiRequestHandler<GetCurrentPeriodQuery, GetAllPeriodsResponseDto>
    {
        private readonly IMapper _mapper;
        public GetCurrentPeriodQueryHandler(
            ILogger<GetCurrentPeriodQueryHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager,
            IMapper mapper)
            : base(logger, appDataService, appUserManager)
        {
            _mapper = mapper;
        }

        public override async Task<ApiResponseDto<GetAllPeriodsResponseDto>> Handle(GetCurrentPeriodQuery request, CancellationToken cancellationToken)
        {
            var current = await AppDataService.Periods.FirstOrDefaultAsync(p => p.IsActive && p.SchoolId == AppUserManager.SchoolId);
            return current == null ?
                new ApiResponseDto<GetAllPeriodsResponseDto>(null) :
                new ApiResponseDto<GetAllPeriodsResponseDto>(_mapper.Map<GetAllPeriodsResponseDto>(current));
        }
    }
}
