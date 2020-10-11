using AutoMapper;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Subjects.Responses;
using Schedule.Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Subjects.Commands.Update
{
    public class UpdateSubjectCommandHandler : BaseApiRequestHandler<UpdateSubjectCommand, GetAllSubjectsResponseDto>
    {
        private readonly IMapper _mapper;
        public UpdateSubjectCommandHandler(
            ILogger<UpdateSubjectCommand> logger,
            IAppDataService appDataService,
            IMapper mapper)
            : base(logger, appDataService)
        {
            _mapper = mapper;
        }

        public override async Task<ApiResponseDto<GetAllSubjectsResponseDto>> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = await AppDataService.Subjects.FirstOrDefaultAsync(s => s.Id == request.Id);
            if (subject == null)
            {
                var msg = $"SubjectId = {request.Id} does not exist";
                Logger.LogWarning($"{nameof(Handle)}: {msg}");
                throw new NotFoundException(msg);
            }

            await AppDataService.Subjects.CheckBeforeSaving(request.Dto.SemesterId, request.Dto.CareerId, request.Dto.ClassroomTypePerSubjectId);

            if (subject.Code != request.Dto.Code)
            {
                bool codeIsBeingUsed = await AppDataService.Subjects.ExistsAsync(s => s.Code == request.Dto.Code);
                if (codeIsBeingUsed)
                {
                    var msg = $"Code = {request.Dto.Code} is being used";
                    Logger.LogWarning($"{nameof(Handle)}: {msg}");
                    throw new InvalidRequestException(msg);
                }
            }

            _mapper.Map(request.Dto, subject);

            await AppDataService.SaveChangesAsync();

            return new ApiResponseDto<GetAllSubjectsResponseDto>(_mapper.Map<GetAllSubjectsResponseDto>(subject));
        }
    }
}
