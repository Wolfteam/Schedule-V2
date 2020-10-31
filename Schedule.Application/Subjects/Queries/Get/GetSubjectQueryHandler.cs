using AutoMapper;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Subjects.Responses;
using Schedule.Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Subjects.Queries.Get
{
    public class GetSubjectQueryHandler : BaseApiRequestHandler<GetSubjectQuery, GetAllSubjectsResponseDto>
    {
        private readonly IMapper _mapper;
        public GetSubjectQueryHandler(
            ILogger<GetSubjectQueryHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager,
            IMapper mapper)
            : base(logger, appDataService, appUserManager)
        {
            _mapper = mapper;
        }

        public override async Task<ApiResponseDto<GetAllSubjectsResponseDto>> Handle(GetSubjectQuery request, CancellationToken cancellationToken)
        {
            var subject = await AppDataService.Subjects.FirstOrDefaultAsync(f => f.Id == request.Id && f.SchoolId == AppUserManager.SchoolId);
            if (subject != null)
                return new ApiResponseDto<GetAllSubjectsResponseDto>(_mapper.Map<GetAllSubjectsResponseDto>(subject));
            var msg = $"SubjectId = {request.Id} was not found";
            Logger.LogWarning($"{nameof(Handle)}: {msg}");
            throw new NotFoundException(msg);
        }
    }
}
