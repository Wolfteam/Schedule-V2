using AutoMapper;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Semesters.Responses;
using Schedule.Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Semesters.Commands.Update
{
    public class UpdateSemesterCommandHandler : BaseApiRequestHandler<UpdateSemesterCommand, GetAllSemestersResponseDto>
    {
        private readonly IMapper _mapper;
        public UpdateSemesterCommandHandler(
            ILogger<UpdateSemesterCommandHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager,
            IMapper mapper)
            : base(logger, appDataService, appUserManager)
        {
            _mapper = mapper;
        }

        public override async Task<ApiResponseDto<GetAllSemestersResponseDto>> Handle(UpdateSemesterCommand request, CancellationToken cancellationToken)
        {
            var semester = await AppDataService.Semesters.FirstOrDefaultAsync(s => s.Id == request.Id);
            if (semester == null)
            {
                var msg = $"SemesterId = {request.Id} does not exist";
                Logger.LogWarning($"{nameof(Handle)}: {msg}");
                throw new NotFoundException(msg);
            }

            if (request.Dto.Name != semester.Name)
            {
                bool isNewNameBeingUsed = await AppDataService.Semesters.ExistsAsync(s => s.Name == request.Dto.Name);
                if (isNewNameBeingUsed)
                {
                    var msg = $"Semester = {request.Dto.Name} already exists";
                    Logger.LogWarning($"{nameof(Handle)}: {msg}");
                    throw new ResourceAlreadyExistsException(msg);
                }
            }

            semester.Name = request.Dto.Name;

            await AppDataService.SaveChangesAsync();

            return new ApiResponseDto<GetAllSemestersResponseDto>(_mapper.Map<GetAllSemestersResponseDto>(semester));
        }
    }
}
