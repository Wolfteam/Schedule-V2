using AutoMapper;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Careers.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Careers.Queries.GetAll
{
    public class GetAllCareersQueryHandler : BaseApiListRequestHandler<GetAllCareersQuery, GetAllCareersResponseDto>
    {
        private readonly IMapper _mapper;
        public GetAllCareersQueryHandler(
            ILogger<GetAllCareersQueryHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager,
            IMapper mapper)
            : base(logger, appDataService, appUserManager)
        {
            _mapper = mapper;
        }

        public override async Task<ApiListResponseDto<GetAllCareersResponseDto>> Handle(GetAllCareersQuery request, CancellationToken cancellationToken)
        {
            var careers = await AppDataService.Careers.GetAllAsync(c => c.SchoolId == AppUserManager.SchoolId);
            return new ApiListResponseDto<GetAllCareersResponseDto>
            {
                Result = _mapper.Map<List<GetAllCareersResponseDto>>(careers.OrderBy(c => c.Name)),
                Succeed = true
            };
        }
    }
}
