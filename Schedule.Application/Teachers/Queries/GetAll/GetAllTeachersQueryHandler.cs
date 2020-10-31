using AutoMapper;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Teachers.Responses;
using Schedule.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Teachers.Queries.GetAll
{
    public class GetAllTeachersQueryHandler : BaseApiListRequestHandler<GetAllTeachersQuery, GetAllTeacherResponseDto>
    {
        private readonly IMapper _mapper;
        public GetAllTeachersQueryHandler(
            ILogger<GetAllTeachersQueryHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager,
            IMapper mapper)
            : base(logger, appDataService, appUserManager)
        {
            _mapper = mapper;
        }

        public override async Task<ApiListResponseDto<GetAllTeacherResponseDto>> Handle(GetAllTeachersQuery request, CancellationToken cancellationToken)
        {
            var response = new ApiListResponseDto<GetAllTeacherResponseDto>();
            var teachers = await AppDataService.Teachers.GetAllAsync(t => t.SchoolId == AppUserManager.SchoolId, includeProperties: nameof(Teacher.Priority));
            response.Result = _mapper.Map<List<GetAllTeacherResponseDto>>(teachers.OrderBy(t => t.FirstName));
            response.Succeed = true;
            return response;
        }
    }
}
