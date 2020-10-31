using AutoMapper;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Semesters.Responses;
using Schedule.Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Semesters.Queries.Get
{
    public class GetSemesterQueryHandler : BaseApiRequestHandler<GetSemesterQuery, GetAllSemestersResponseDto>
    {
        private readonly IMapper _mapper;
        public GetSemesterQueryHandler(
            ILogger<GetSemesterQueryHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager,
            IMapper mapper)
            : base(logger, appDataService, appUserManager)
        {
            _mapper = mapper;
        }

        public override async Task<ApiResponseDto<GetAllSemestersResponseDto>> Handle(GetSemesterQuery request, CancellationToken cancellationToken)
        {
            var semester = await AppDataService.Semesters.FirstOrDefaultAsync(s => s.Id == request.Id && s.SchoolId == AppUserManager.SchoolId);
            if (semester != null)
                return new ApiResponseDto<GetAllSemestersResponseDto>(_mapper.Map<GetAllSemestersResponseDto>(semester));
            var msg = $"SemestersId = {request.Id} was not found";
            Logger.LogWarning($"{nameof(Handle)}: {msg}");
            throw new NotFoundException(msg);
        }
    }
}
