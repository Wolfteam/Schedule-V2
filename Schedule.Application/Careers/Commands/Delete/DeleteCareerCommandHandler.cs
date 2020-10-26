using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Enums;
using Schedule.Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Careers.Commands.Delete
{
    public class DeleteCareerCommandHandler : BaseEmptyRequestHandler<DeleteCareerCommand>
    {
        public DeleteCareerCommandHandler(
            ILogger<DeleteCareerCommandHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager)
            : base(logger, appDataService, appUserManager)
        {
        }

        public override async Task<EmptyResponseDto> Handle(DeleteCareerCommand request, CancellationToken cancellationToken)
        {
            var career = await AppDataService.Careers.FirstOrDefaultAsync(c => c.Id == request.Id);
            if (career == null)
            {
                var msg = $"CareerId = {request.Id} does not exist";
                Logger.LogWarning($"{nameof(Handle)}: {msg}");
                throw new NotFoundException(msg);
            }

            bool isBeingUsed = await AppDataService.Subjects.ExistsAsync(s => s.CareerId == request.Id);
            if (isBeingUsed)
            {
                var msg = $"CareerId = {request.Id} is being used";
                Logger.LogWarning($"{nameof(Handle)}: {msg}");
                throw new InvalidRequestException(msg, AppMessageType.SchApiResourceCantBeDeletedIsBeingUsed);
            }

            AppDataService.Careers.Remove(career);
            await AppDataService.SaveChangesAsync();

            return new EmptyResponseDto(true);
        }
    }
}
