using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Semesters.Commands.Delete
{
    public class DeleteSemesterCommandHandler : BaseEmptyRequestHandler<DeleteSemesterCommand>
    {
        public DeleteSemesterCommandHandler(
            ILogger<DeleteSemesterCommandHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager)
            : base(logger, appDataService, appUserManager)
        {
        }

        public override async Task<EmptyResponseDto> Handle(DeleteSemesterCommand request, CancellationToken cancellationToken)
        {
            var semester = await AppDataService.Semesters.FirstOrDefaultAsync(s => s.Id == request.Id);
            if (semester == null)
            {
                var msg = $"SemesterId = {request.Id} does not exist";
                Logger.LogWarning($"{nameof(Handle)}: {msg}");
                throw new NotFoundException(msg);
            }

            AppDataService.Semesters.Remove(semester);

            await AppDataService.SaveChangesAsync();

            return new EmptyResponseDto(true);
        }
    }
}
