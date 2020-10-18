using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Classrooms.Commands.DeleteType
{
    public class DeleteClassroomTypeCommandHandler : BaseEmptyRequestHandler<DeleteClassroomTypeCommand>
    {
        public DeleteClassroomTypeCommandHandler(
            ILogger<DeleteClassroomTypeCommandHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager)
            : base(logger, appDataService, appUserManager)
        {
        }

        public override async Task<EmptyResponseDto> Handle(DeleteClassroomTypeCommand request, CancellationToken cancellationToken)
        {
            var classroomType = await AppDataService.ClassroomSubject.FirstOrDefaultAsync(a => a.Id == request.Id && a.SchoolId == AppUserManager.SchoolId);
            if (classroomType == null)
            {
                var msg = $"ClassroomTypeId = {request.Id} does not exist";
                Logger.LogWarning($"{nameof(Handle)}: {msg}");
                throw new NotFoundException(msg);
            }

            AppDataService.ClassroomSubject.Remove(classroomType);
            await AppDataService.SaveChangesAsync();

            return new EmptyResponseDto(true);
        }
    }
}
