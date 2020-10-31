using AutoMapper;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Classrooms.Responses;
using Schedule.Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Classrooms.Queries.Get
{
    public class GetClassroomQueryHandler : BaseApiRequestHandler<GetClassroomQuery, GetAllClassroomsResponseDto>
    {
        private readonly IMapper _mapper;
        public GetClassroomQueryHandler(
            ILogger<GetClassroomQueryHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager,
            IMapper mapper)
            : base(logger, appDataService, appUserManager)
        {
            _mapper = mapper;
        }

        public override async Task<ApiResponseDto<GetAllClassroomsResponseDto>> Handle(GetClassroomQuery request, CancellationToken cancellationToken)
        {
            var classroom = await AppDataService.Classrooms.FirstOrDefaultAsync(c => c.Id == request.Id && c.SchoolId == AppUserManager.SchoolId);
            if (classroom != null)
                return new ApiResponseDto<GetAllClassroomsResponseDto>(_mapper.Map<GetAllClassroomsResponseDto>(classroom));
            var msg = $"ClassroomId = {request.Id} was not found";
            Logger.LogWarning($"{nameof(Handle)}: {msg}");
            throw new NotFoundException(msg);
        }
    }
}
