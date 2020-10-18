using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Periods.Commands.Delete
{
    public class DeletePeriodCommandHandler : BaseEmptyRequestHandler<DeletePeriodCommand>
    {
        public DeletePeriodCommandHandler(
            ILogger<DeletePeriodCommandHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager)
            : base(logger, appDataService, appUserManager)
        {
        }

        public override async Task<EmptyResponseDto> Handle(DeletePeriodCommand request, CancellationToken cancellationToken)
        {
            var period = await AppDataService.Periods.FirstOrDefaultAsync(p => p.Id == request.Id && p.SchoolId == AppUserManager.SchoolId);
            if (period == null)
            {
                var msg = $"PeriodId = {request.Id} does not exist";
                Logger.LogWarning($"{nameof(Handle)}: {msg}");
                throw new NotFoundException(msg);
            }

            AppDataService.Periods.Remove(period);
            await AppDataService.SaveChangesAsync();

            return new EmptyResponseDto(true);
        }
    }
}
