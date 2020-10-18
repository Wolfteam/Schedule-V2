using AutoMapper;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Periods.Responses;
using Schedule.Domain.Entities;
using Schedule.Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Periods.Commands.Create
{
    public class CreatePeriodCommandHandler : BaseApiRequestHandler<CreatePeriodCommand, GetAllPeriodsResponseDto>
    {
        private readonly IMapper _mapper;
        public CreatePeriodCommandHandler(
            ILogger<CreatePeriodCommandHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager,
            IMapper mapper)
            : base(logger, appDataService, appUserManager)
        {
            _mapper = mapper;
        }

        public override async Task<ApiResponseDto<GetAllPeriodsResponseDto>> Handle(CreatePeriodCommand request, CancellationToken cancellationToken)
        {
            bool periodExists = await AppDataService.Periods.ExistsAsync(p => p.Name == request.Dto.Name && p.SchoolId == AppUserManager.SchoolId);
            if (periodExists)
            {
                var msg = $"Period = {request.Dto.Name} already exists";
                Logger.LogWarning($"{nameof(Handle)}: {msg}");
                throw new InvalidRequestException(msg);
            }

            var period = _mapper.Map<Period>(request.Dto);
            period.SchoolId = AppUserManager.SchoolId;

            if (period.IsActive)
                await AppDataService.Periods.InactiveAllPeriods(AppUserManager.SchoolId);

            AppDataService.Periods.Add(period);
            await AppDataService.SaveChangesAsync();

            return new ApiResponseDto<GetAllPeriodsResponseDto>(_mapper.Map<GetAllPeriodsResponseDto>(period));
        }
    }
}
