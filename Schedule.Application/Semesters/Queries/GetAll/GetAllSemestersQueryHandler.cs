using AutoMapper;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Semesters.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Semesters.Queries.GetAll
{
    public class GetAllSemestersQueryHandler : BaseApiListRequestHandler<GetAllSemestersQuery, GetAllSemestersResponseDto>
    {
        private readonly IMapper _mapper;
        public GetAllSemestersQueryHandler(
            ILogger<GetAllSemestersQueryHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager,
            IMapper mapper)
            : base(logger, appDataService, appUserManager)
        {
            _mapper = mapper;
        }

        public override async Task<ApiListResponseDto<GetAllSemestersResponseDto>> Handle(GetAllSemestersQuery request, CancellationToken cancellationToken)
        {
            var semesters = await AppDataService.Semesters.GetAllAsync(s => s.SchoolId == AppUserManager.SchoolId);
            return new ApiListResponseDto<GetAllSemestersResponseDto>(_mapper.Map<List<GetAllSemestersResponseDto>>(semesters));
        }
    }
}
