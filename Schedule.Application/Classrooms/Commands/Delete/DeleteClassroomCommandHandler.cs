using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Classrooms.Commands.Delete
{
    public class DeleteClassroomCommandHandler : BaseEmptyRequestHandler<DeleteClassroomCommand>
    {
        public DeleteClassroomCommandHandler(
            ILogger<DeleteClassroomCommandHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager)
            : base(logger, appDataService, appUserManager)
        {
        }

        public override async Task<EmptyResponseDto> Handle(DeleteClassroomCommand request, CancellationToken cancellationToken)
        {
            var classroom = await AppDataService.Classrooms.FirstOrDefaultAsync(c => c.Id == request.Id && c.SchoolId == AppUserManager.SchoolId);
            if (classroom == null)
            {
                var msg = $"ClassroomId = {request.Id} does not exist";
                Logger.LogWarning($"{nameof(Handle)}: {msg}");
                throw new NotFoundException(msg);
            }

            AppDataService.Classrooms.Remove(classroom);

            await AppDataService.SaveChangesAsync();

            return new EmptyResponseDto(true);
        }
    }
}
