using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Priorities.Commands.Delete
{
    public class DeletePriorityCommandHandler : BaseEmptyRequestHandler<DeletePriorityCommand>
    {
        public DeletePriorityCommandHandler(
            ILogger<DeletePriorityCommand> logger,
            IAppDataService appDataService)
            : base(logger, appDataService)
        {
        }

        public override async Task<EmptyResponseDto> Handle(DeletePriorityCommand request, CancellationToken cancellationToken)
        {
            var priority = await AppDataService.Priorities.FirstOrDefaultAsync(p => p.Id == request.Id);
            if (priority == null)
            {
                var msg = $"PriorityId = {request.Id} was not found";
                Logger.LogWarning($"{nameof(Handle)}: {msg}");
                throw new NotFoundException(msg);
            }

            bool isBeingUsed = await AppDataService.Teachers.ExistsAsync(t => t.PriorityId == request.Id);

            if (isBeingUsed)
            {
                var msg = $"PriorityId = {request.Id} can't be deleted, because is being used";
                Logger.LogWarning($"{nameof(Handle)}: {msg}");
                throw new InvalidRequestException(msg);
            }

            AppDataService.Priorities.Remove(priority);

            await AppDataService.SaveChangesAsync();

            return new EmptyResponseDto(true);
        }
    }
}
