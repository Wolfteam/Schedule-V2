using AutoMapper;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Subjects.Responses;
using Schedule.Domain.Entities;
using Schedule.Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Subjects.Commands.Create
{
    public class CreateSubjectCommandHandler : BaseApiRequestHandler<CreateSubjectCommand, GetAllSubjectsResponseDto>
    {
        private readonly IMapper _mapper;
        public CreateSubjectCommandHandler(
            ILogger<CreateSubjectCommandHandler> logger,
            IAppDataService appDataService,
            IMapper mapper)
            : base(logger, appDataService)
        {
            _mapper = mapper;
        }

        public override async Task<ApiResponseDto<GetAllSubjectsResponseDto>> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
        {
            bool codeExists = await AppDataService.Subjects.ExistsAsync(s => s.Code == request.Dto.Code);
            if (codeExists)
            {
                var msg = $"Code = {request.Dto.Code} already exists";
                Logger.LogWarning($"{nameof(Handle)}: {msg}");
                throw new InvalidRequestException(msg);
            }

            await AppDataService.Subjects.CheckBeforeSaving(request.Dto.SemesterId, request.Dto.CareerId, request.Dto.ClassroomTypePerSubjectId);

            var subject = _mapper.Map<Subject>(request.Dto);
            AppDataService.Subjects.Add(subject);

            await AppDataService.SaveChangesAsync();

            return new ApiResponseDto<GetAllSubjectsResponseDto>(_mapper.Map<GetAllSubjectsResponseDto>(subject));
        }
    }
}
