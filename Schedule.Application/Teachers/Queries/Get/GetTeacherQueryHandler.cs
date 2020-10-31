using AutoMapper;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Teachers.Responses;
using Schedule.Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Teachers.Queries.Get
{
    public class GetTeacherQueryHandler : BaseApiRequestHandler<GetTeacherQuery, GetAllTeacherResponseDto>
    {
        private readonly IMapper _mapper;
        public GetTeacherQueryHandler(
            ILogger<GetTeacherQueryHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager,
            IMapper mapper)
            : base(logger, appDataService, appUserManager)
        {
            _mapper = mapper;
        }

        public override async Task<ApiResponseDto<GetAllTeacherResponseDto>> Handle(GetTeacherQuery request, CancellationToken cancellationToken)
        {
            var teacher = await AppDataService.Teachers.FirstOrDefaultAsync(t => t.Id == request.Id && t.SchoolId == AppUserManager.SchoolId);
            if (teacher != null)
                return new ApiResponseDto<GetAllTeacherResponseDto>(_mapper.Map<GetAllTeacherResponseDto>(teacher));
            var msg = $"TeacherId = {request.Id} was not found";
            Logger.LogWarning($"{nameof(Handle)}: {msg}");
            throw new NotFoundException(msg);
        }
    }
}
