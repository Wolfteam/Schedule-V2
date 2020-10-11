using AutoMapper;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Classrooms.Responses;
using Schedule.Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Classrooms.Commands.Update
{
    public class UpdateClassroomCommandHandler : BaseApiRequestHandler<UpdateClassroomCommand, GetAllClassroomsResponseDto>
    {
        private readonly IMapper _mapper;
        public UpdateClassroomCommandHandler(
            ILogger<UpdateClassroomCommandHandler> logger,
            IAppDataService appDataService,
            IMapper mapper)
            : base(logger, appDataService)
        {
            _mapper = mapper;
        }

        public override async Task<ApiResponseDto<GetAllClassroomsResponseDto>> Handle(UpdateClassroomCommand request, CancellationToken cancellationToken)
        {
            var classroom = await AppDataService.Classrooms.FirstOrDefaultAsync(c => c.Id == request.Id);
            if (classroom == null)
            {
                var msg = $"ClassroomId = {request.Id} does not exist";
                Logger.LogWarning($"{nameof(Handle)}: {msg}");
                throw new NotFoundException(msg);
            }

            _mapper.Map(request.Dto, classroom);

            await AppDataService.SaveChangesAsync();

            return new ApiResponseDto<GetAllClassroomsResponseDto>(_mapper.Map<GetAllClassroomsResponseDto>(classroom));
        }
    }
}
