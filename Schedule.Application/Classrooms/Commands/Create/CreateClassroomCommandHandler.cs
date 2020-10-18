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

namespace Schedule.Application.Classrooms.Commands.Create
{
    public class CreateClassroomCommandHandler : BaseApiRequestHandler<CreateClassroomCommand, GetAllClassroomsResponseDto>
    {
        private readonly IMapper _mapper;
        public CreateClassroomCommandHandler(
            ILogger<CreateClassroomCommand> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager,
            IMapper mapper)
            : base(logger, appDataService, appUserManager)
        {
            _mapper = mapper;
        }

        public override async Task<ApiResponseDto<GetAllClassroomsResponseDto>> Handle(CreateClassroomCommand request, CancellationToken cancellationToken)
        {
            bool classroomExists = await AppDataService.Classrooms.ExistsAsync(c => c.Name == request.Dto.Name && c.SchoolId == AppUserManager.SchoolId);
            if (classroomExists)
            {
                var msg = $"Classroom = {request.Dto.Name} already exists";
                Logger.LogWarning($"{nameof(Handle)}: {msg}");
                throw new InvalidRequestException(msg);
            }

            bool typeExists = await AppDataService.ClassroomSubject.ExistsAsync(c => c.Id == request.Dto.ClassroomTypePerSubjectId && c.SchoolId == AppUserManager.SchoolId);
            if (!typeExists)
            {
                var msg = $"No type was found for class room type id = {request.Dto.ClassroomTypePerSubjectId}";
                Logger.LogWarning($"{nameof(Handle)}: {msg}");
                throw new InvalidRequestException(msg);
            }

            var classroom = _mapper.Map<Classroom>(request.Dto);
            classroom.SchoolId = AppUserManager.SchoolId;
            AppDataService.Classrooms.Add(classroom);
            await AppDataService.SaveChangesAsync();
            return new ApiResponseDto<GetAllClassroomsResponseDto>(_mapper.Map<GetAllClassroomsResponseDto>(classroom));
        }
    }
}
