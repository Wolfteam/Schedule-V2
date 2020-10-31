using AutoMapper;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Careers.Responses;
using Schedule.Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Careers.Queries.Get
{
    public class GetCareerQueryHandler : BaseApiRequestHandler<GetCareerQuery, GetAllCareersResponseDto>
    {
        private readonly IMapper _mapper;

        public GetCareerQueryHandler(
            ILogger<GetCareerQueryHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager,
            IMapper mapper)
            : base(logger, appDataService, appUserManager)
        {
            _mapper = mapper;
        }

        public override async Task<ApiResponseDto<GetAllCareersResponseDto>> Handle(GetCareerQuery request, CancellationToken cancellationToken)
        {
            var career = await AppDataService.Careers.FirstOrDefaultAsync(p => p.Id == request.Id && p.SchoolId == AppUserManager.SchoolId);
            if (career != null)
                return new ApiResponseDto<GetAllCareersResponseDto>(_mapper.Map<GetAllCareersResponseDto>(career));
            var msg = $"CareerId = {request.Id} was not found";
            Logger.LogWarning($"{nameof(Handle)}: {msg}");
            throw new NotFoundException(msg);
        }
    }
}
