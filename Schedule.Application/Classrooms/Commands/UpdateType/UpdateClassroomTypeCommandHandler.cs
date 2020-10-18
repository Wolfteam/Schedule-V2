using AutoMapper;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Classrooms.Responses;
using Schedule.Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Classrooms.Commands.UpdateType
{
    public class UpdateClassroomTypeCommandHandler : BaseApiRequestHandler<UpdateClassroomTypeCommand, GetAllClassroomTypesResponseDto>
    {
        private readonly IMapper _mapper;
        public UpdateClassroomTypeCommandHandler(
            ILogger<UpdateClassroomTypeCommand> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager,
            IMapper mapper)
            : base(logger, appDataService, appUserManager)
        {
            _mapper = mapper;
        }

        public override async Task<ApiResponseDto<GetAllClassroomTypesResponseDto>> Handle(UpdateClassroomTypeCommand request, CancellationToken cancellationToken)
        {
            var classroomType = await AppDataService.ClassroomSubject.FirstOrDefaultAsync(c => c.Id == request.Id && c.SchoolId == AppUserManager.SchoolId);
            if (classroomType == null)
            {
                var msg = $"ClassroomTypeId = {request.Id} does not exist";
                Logger.LogWarning($"{nameof(Handle)}: {msg}");
                throw new NotFoundException(msg);
            }

            if (classroomType.Name != request.Dto.Name)
            {
                bool nameIsBeingUsed = await AppDataService.ClassroomSubject.ExistsAsync(c => c.Name == request.Dto.Name && c.SchoolId == AppUserManager.SchoolId);
                if (nameIsBeingUsed)
                {
                    var msg = $"ClassroomType = {request.Dto.Name} already exists";
                    Logger.LogWarning($"{nameof(Handle)}: {msg}");
                    throw new InvalidRequestException(msg);
                }
            }

            classroomType.Name = request.Dto.Name;

            await AppDataService.SaveChangesAsync();
            return new ApiResponseDto<GetAllClassroomTypesResponseDto>(_mapper.Map<GetAllClassroomTypesResponseDto>(classroomType));
        }
    }
}
