using AutoMapper;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Teachers.Responses;
using Schedule.Domain.Entities;
using Schedule.Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Teachers.Commands.Create
{
    public class CreateTeacherCommandHandler : BaseApiRequestHandler<CreateTeacherCommand, GetAllTeacherResponseDto>
    {
        private readonly IMapper _mapper;
        public CreateTeacherCommandHandler(
            ILogger<CreateTeacherCommand> logger,
            IAppDataService appDataService,
            IMapper mapper)
            : base(logger, appDataService)
        {
            _mapper = mapper;
        }

        public override async Task<ApiResponseDto<GetAllTeacherResponseDto>> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
        {
            bool idNumberExists = await AppDataService.Teachers.ExistsAsync(t => t.IdentifierNumber == request.Dto.IdentifierNumber);
            if (idNumberExists)
            {
                var msg = $"IdentificationNumber = {request.Dto.IdentifierNumber} already exists";
                Logger.LogWarning($"{nameof(Handle)}: {msg}");
                throw new InvalidRequestException(msg);
            }

            bool priorityExists = await AppDataService.Priorities.ExistsAsync(p => p.Id == request.Dto.PriorityId);
            if (!priorityExists)
            {
                var msg = $"PriorityId = {request.Dto.PriorityId} does not exist";
                Logger.LogWarning($"{nameof(Handle)}: {msg}");
                throw new NotFoundException(msg);
            }

            var teacher = _mapper.Map<Teacher>(request.Dto);
            AppDataService.Teachers.Add(teacher);

            await AppDataService.SaveChangesAsync();

            return new ApiResponseDto<GetAllTeacherResponseDto>(_mapper.Map<GetAllTeacherResponseDto>(teacher));
        }
    }
}
