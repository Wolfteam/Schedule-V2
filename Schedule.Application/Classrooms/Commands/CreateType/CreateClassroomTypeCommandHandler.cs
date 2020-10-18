using AutoMapper;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Classrooms.Responses;
using Schedule.Domain.Entities;
using Schedule.Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Classrooms.Commands.CreateType
{
    public class CreateClassroomTypeCommandHandler : BaseApiRequestHandler<CreateClassroomTypeCommand, GetAllClassroomTypesResponseDto>
    {
        private readonly IMapper _mapper;
        public CreateClassroomTypeCommandHandler(
            ILogger<CreateClassroomTypeCommandHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager,
            IMapper mapper)
            : base(logger, appDataService, appUserManager)
        {
            _mapper = mapper;
        }

        public override async Task<ApiResponseDto<GetAllClassroomTypesResponseDto>> Handle(CreateClassroomTypeCommand request, CancellationToken cancellationToken)
        {
            bool alreadyExists = await AppDataService.ClassroomSubject.ExistsAsync(a => a.Name == request.Dto.Name && a.SchoolId == AppUserManager.SchoolId);
            if (alreadyExists)
            {
                var msg = $"ClassroomType = {request.Dto.Name} already exists";
                Logger.LogWarning($"{nameof(Handle)}: {msg}");
                throw new InvalidRequestException(msg);
            }

            var classroomType = _mapper.Map<ClassroomSubject>(request.Dto);
            classroomType.SchoolId = AppUserManager.SchoolId;
            AppDataService.ClassroomSubject.Add(classroomType);

            await AppDataService.SaveChangesAsync();

            return new ApiResponseDto<GetAllClassroomTypesResponseDto>(_mapper.Map<GetAllClassroomTypesResponseDto>(classroomType));
        }
    }
}
