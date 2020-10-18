using AutoMapper;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Periods.Responses;
using Schedule.Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Periods.Commands.Update
{
    public class UpdatePeriodCommandHandler : BaseApiRequestHandler<UpdatePeriodCommand, GetAllPeriodsResponseDto>
    {
        private readonly IMapper _mapper;
        public UpdatePeriodCommandHandler(
            ILogger<UpdatePeriodCommandHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager,
            IMapper mapper)
            : base(logger, appDataService, appUserManager)
        {
            _mapper = mapper;
        }

        public override async Task<ApiResponseDto<GetAllPeriodsResponseDto>> Handle(UpdatePeriodCommand request, CancellationToken cancellationToken)
        {
            var period = await AppDataService.Periods.FirstOrDefaultAsync(p => p.Id == request.Id && p.SchoolId == AppUserManager.SchoolId);
            if (period == null)
            {
                var msg = $"PeriodId = {request.Id} does not exist";
                Logger.LogWarning($"{nameof(Handle)}: {msg}");
                throw new NotFoundException(msg);
            }

            if (period.Name != request.Dto.Name)
            {
                bool periodExists = await AppDataService.Periods.ExistsAsync(p => p.Name == request.Dto.Name && p.SchoolId == AppUserManager.SchoolId);
                if (periodExists)
                {
                    var msg = $"Period = {request.Dto.Name} already exists";
                    Logger.LogWarning($"{nameof(Handle)}: {msg}");
                    throw new InvalidRequestException(msg);
                }
            }

            if (request.Dto.IsActive)
                await AppDataService.Periods.InactiveAllPeriods(AppUserManager.SchoolId);

            _mapper.Map(request.Dto, period);

            await AppDataService.SaveChangesAsync();

            return new ApiResponseDto<GetAllPeriodsResponseDto>(_mapper.Map<GetAllPeriodsResponseDto>(period));
        }
    }
}
