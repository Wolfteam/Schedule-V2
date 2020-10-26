using AutoMapper;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Semesters.Responses;
using Schedule.Domain.Entities;
using Schedule.Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Semesters.Commands.Create
{
    public class CreateSemesterCommandHandler : BaseApiRequestHandler<CreateSemesterCommand, GetAllSemestersResponseDto>
    {
        private readonly IMapper _mapper;
        public CreateSemesterCommandHandler(
            ILogger<CreateSemesterCommandHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager,
            IMapper mapper)
            : base(logger, appDataService, appUserManager)
        {
            _mapper = mapper;
        }

        public override async Task<ApiResponseDto<GetAllSemestersResponseDto>> Handle(CreateSemesterCommand request, CancellationToken cancellationToken)
        {
            bool nameIsBeingUsed = await AppDataService.Semesters.ExistsAsync(s => s.Name == request.Dto.Name && s.SchoolId == AppUserManager.SchoolId);
            if (nameIsBeingUsed)
            {
                var msg = $"Semester = {request.Dto.Name} already exists";
                Logger.LogInformation($"{nameof(Handle)}: {msg}");
                throw new ResourceAlreadyExistsException(msg);
            }

            var semester = new Semester
            {
                SchoolId = AppUserManager.SchoolId,
                Name = request.Dto.Name
            };
            AppDataService.Semesters.Add(semester);

            await AppDataService.SaveChangesAsync();

            return new ApiResponseDto<GetAllSemestersResponseDto>(_mapper.Map<GetAllSemestersResponseDto>(semester));
        }
    }
}
