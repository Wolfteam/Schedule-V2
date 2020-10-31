using AutoMapper;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Classrooms.Responses;
using Schedule.Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Classrooms.Queries.GetType
{
    public class GetClassroomTypeQueryHandler : BaseApiRequestHandler<GetClassroomTypeQuery, GetAllClassroomTypesResponseDto>
    {
        private readonly IMapper _mapper;
        public GetClassroomTypeQueryHandler(
            ILogger<GetClassroomTypeQueryHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager,
            IMapper mapper)
            : base(logger, appDataService, appUserManager)
        {
            _mapper = mapper;
        }

        public override async Task<ApiResponseDto<GetAllClassroomTypesResponseDto>> Handle(GetClassroomTypeQuery request, CancellationToken cancellationToken)
        {
            var classroomType = await AppDataService.ClassroomSubject.FirstOrDefaultAsync(c => c.Id == request.Id && c.SchoolId == AppUserManager.SchoolId);
            if (classroomType != null)
                return new ApiResponseDto<GetAllClassroomTypesResponseDto>(_mapper.Map<GetAllClassroomTypesResponseDto>(classroomType));

            var msg = $"ClassroomTypeId = {request.Id} was not found";
            Logger.LogWarning($"{nameof(Handle)}: {msg}");
            throw new NotFoundException(msg);
        }
    }
}
