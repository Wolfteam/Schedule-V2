using AutoMapper;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Periods.Responses;
using Schedule.Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Periods.Queries.Get
{
    public class GetPeriodQueryHandler : BaseApiRequestHandler<GetPeriodQuery, GetAllPeriodsResponseDto>
    {
        private readonly IMapper _mapper;
        public GetPeriodQueryHandler(
            ILogger<GetPeriodQueryHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager,
            IMapper mapper)
            : base(logger, appDataService, appUserManager)
        {
            _mapper = mapper;
        }

        public override async Task<ApiResponseDto<GetAllPeriodsResponseDto>> Handle(GetPeriodQuery request, CancellationToken cancellationToken)
        {
            var period = await AppDataService.Periods.FirstOrDefaultAsync(p => p.Id == request.Id && p.SchoolId == AppUserManager.SchoolId);
            if (period != null)
                return new ApiResponseDto<GetAllPeriodsResponseDto>(_mapper.Map<GetAllPeriodsResponseDto>(period));
            var msg = $"PeriodId = {request.Id} was not found";
            Logger.LogWarning($"{nameof(Handle)}: {msg}");
            throw new NotFoundException(msg);
        }
    }
}
