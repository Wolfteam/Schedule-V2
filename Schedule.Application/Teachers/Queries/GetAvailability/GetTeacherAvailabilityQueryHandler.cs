using AutoMapper;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Teachers.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Teachers.Queries.GetAvailability
{
    public class GetTeacherAvailabilityQueryHandler : BaseApiRequestHandler<GetTeacherAvailabilityQuery, TeacherAvailabilityDetailsResponseDto>
    {
        private readonly IMapper _mapper;
        public GetTeacherAvailabilityQueryHandler(
            ILogger<GetTeacherAvailabilityQueryHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager,
            IMapper mapper)
            : base(logger, appDataService, appUserManager)
        {
            _mapper = mapper;
        }

        public override async Task<ApiResponseDto<TeacherAvailabilityDetailsResponseDto>> Handle(GetTeacherAvailabilityQuery request, CancellationToken cancellationToken)
        {
            var availabilities = await AppDataService.TeacherAvailabilities.GetAllAsync(t =>
                t.TeacherId == request.TeacherId &&
                t.Teacher.SchoolId == AppUserManager.SchoolId);
            var hoursToComplete = await AppDataService.Teachers.GetHoursToComplete(AppUserManager.SchoolId, request.TeacherId);
            var assignedHours = availabilities.Sum(a => (int)a.EndHour - (int)a.StartHour + 1);
            var mapped = _mapper.Map<List<TeacherAvailabilityResponseDto>>(availabilities);

            var response = new TeacherAvailabilityDetailsResponseDto(request.TeacherId, hoursToComplete, assignedHours, mapped);
            return new ApiResponseDto<TeacherAvailabilityDetailsResponseDto>(response);
        }
    }
}
