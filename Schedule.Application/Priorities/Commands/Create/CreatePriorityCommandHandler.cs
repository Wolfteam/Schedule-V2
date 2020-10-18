using AutoMapper;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Priorities.Responses;
using Schedule.Domain.Entities;
using Schedule.Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Priorities.Commands.Create
{
    public class CreatePriorityCommandHandler : BaseApiRequestHandler<CreatePriorityCommand, GetAllPrioritiesResponseDto>
    {
        private readonly IMapper _mapper;
        public CreatePriorityCommandHandler(
            ILogger<CreatePriorityCommandHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager,
            IMapper mapper)
            : base(logger, appDataService, appUserManager)
        {
            _mapper = mapper;
        }

        public override async Task<ApiResponseDto<GetAllPrioritiesResponseDto>> Handle(CreatePriorityCommand request, CancellationToken cancellationToken)
        {
            bool nameIsBeingUsed = await AppDataService.Priorities.ExistsAsync(p => p.Name == request.Dto.Name && p.SchoolId == AppUserManager.SchoolId);
            if (nameIsBeingUsed)
            {
                var msg = $"Name = {request.Dto.Name} is being used";
                Logger.LogWarning($"{nameof(Handle)}: {msg}");
                throw new InvalidRequestException(msg);
            }

            var priority = _mapper.Map<Priority>(request.Dto);
            priority.SchoolId = AppUserManager.SchoolId;
            AppDataService.Priorities.Add(priority);

            await AppDataService.SaveChangesAsync();

            return new ApiResponseDto<GetAllPrioritiesResponseDto>(_mapper.Map<GetAllPrioritiesResponseDto>(priority));
        }
    }
}
