using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Teachers.Commands.Delete
{
    public class DeleteTeacherCommandHandler : BaseEmptyRequestHandler<DeleteTeacherCommand>
    {
        public DeleteTeacherCommandHandler(
            ILogger<DeleteTeacherCommandHandler> logger, IAppDataService appDataService)
            : base(logger, appDataService)
        {
        }

        public override async Task<EmptyResponseDto> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = await AppDataService.Teachers.FirstOrDefaultAsync(t => t.Id == request.Id);
            if (teacher == null)
            {
                var msg = $"TeacherId {request.Id} was not found";
                Logger.LogWarning($"{nameof(Handle)}: {msg}");
                throw new NotFoundException(msg);
            }

            AppDataService.Teachers.Remove(teacher);

            await AppDataService.SaveChangesAsync();

            return new EmptyResponseDto(true);
        }
    }
}
