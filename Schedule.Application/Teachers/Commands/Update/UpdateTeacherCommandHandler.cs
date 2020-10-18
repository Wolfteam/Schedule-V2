using AutoMapper;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Teachers.Responses;
using Schedule.Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Teachers.Commands.Update
{
    public class UpdateTeacherCommandHandler : BaseApiRequestHandler<UpdateTeacherCommand, GetAllTeacherResponseDto>
    {
        private readonly IMapper _mapper;
        public UpdateTeacherCommandHandler(
            ILogger<UpdateTeacherCommandHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager,
            IMapper mapper)
            : base(logger, appDataService, appUserManager)
        {
            _mapper = mapper;
        }

        public override async Task<ApiResponseDto<GetAllTeacherResponseDto>> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = await AppDataService.Teachers.FirstOrDefaultAsync(t => t.Id == request.Id && t.SchoolId == AppUserManager.SchoolId);
            if (teacher == null)
            {
                var msg = $"TeacherId = {request.Id} was not found";
                Logger.LogWarning($"{nameof(Handle)}: {msg}");
                throw new NotFoundException(msg);
            }

            if (teacher.IdentifierNumber != request.Dto.IdentifierNumber)
            {
                bool idNumberIsBeingUsed = await AppDataService.Teachers.ExistsAsync(t => t.IdentifierNumber == request.Dto.IdentifierNumber && t.SchoolId == AppUserManager.SchoolId);
                if (idNumberIsBeingUsed)
                {
                    var msg = $"IdentifierNumber = {request.Dto.IdentifierNumber} is being used";
                    Logger.LogWarning($"{nameof(Handle)}: {msg}");
                    throw new InvalidRequestException(msg);
                }
            }

            bool priorityExists = await AppDataService.Priorities.ExistsAsync(p => p.Id == request.Dto.PriorityId && p.SchoolId == AppUserManager.SchoolId);
            if (!priorityExists)
            {
                var msg = $"PriorityId = {request.Dto.PriorityId} does not exist";
                Logger.LogWarning($"{nameof(Handle)}: {msg}");
                throw new NotFoundException(msg);
            }

            _mapper.Map(request.Dto, teacher);
            await AppDataService.SaveChangesAsync();

            return new ApiResponseDto<GetAllTeacherResponseDto>(_mapper.Map<GetAllTeacherResponseDto>(teacher));
        }
    }
}
