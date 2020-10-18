using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Subjects.Commands.Delete
{
    public class DeleteSubjectCommandHandler : BaseEmptyRequestHandler<DeleteSubjectCommand>
    {
        public DeleteSubjectCommandHandler(
            ILogger<DeleteSubjectCommandHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager)
            : base(logger, appDataService, appUserManager)
        {
        }

        public override async Task<EmptyResponseDto> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = await AppDataService.Subjects.FirstOrDefaultAsync(s => s.Id == request.Id && s.SchoolId == AppUserManager.SchoolId);
            if (subject == null)
            {
                var msg = $"SubjectId = {request.Id} was not found";
                Logger.LogWarning($"{nameof(Handle)}: {msg}");
                throw new NotFoundException(msg);
            }

            AppDataService.Subjects.Remove(subject);

            await AppDataService.SaveChangesAsync();

            return new EmptyResponseDto(true);
        }
    }
}
