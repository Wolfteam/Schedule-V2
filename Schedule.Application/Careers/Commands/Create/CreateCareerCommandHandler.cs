using AutoMapper;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Careers.Responses;
using Schedule.Domain.Entities;
using Schedule.Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Careers.Commands.Create
{
    public class CreateCareerCommandHandler : BaseApiRequestHandler<CreateCareerCommand, GetAllCareersResponseDto>
    {
        private readonly IMapper _mapper;
        public CreateCareerCommandHandler(
            ILogger<CreateCareerCommandHandler> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager,
            IMapper mapper)
            : base(logger, appDataService, appUserManager)
        {
            _mapper = mapper;
        }

        public override async Task<ApiResponseDto<GetAllCareersResponseDto>> Handle(CreateCareerCommand request, CancellationToken cancellationToken)
        {
            bool careerExists = await AppDataService.Careers.ExistsAsync(
                c => c.Name == request.Dto.Name && c.SchoolId == AppUserManager.SchoolId);

            if (careerExists)
            {
                var msg = $"Career = {request.Dto.Name} already exists";
                Logger.LogWarning($"{nameof(Handle)}: {msg}");
                throw new ResourceAlreadyExistsException(msg);
            }

            var career = new Career
            {
                Name = request.Dto.Name,
                SchoolId = AppUserManager.SchoolId
            };

            AppDataService.Careers.Add(career);

            await AppDataService.SaveChangesAsync();

            return new ApiResponseDto<GetAllCareersResponseDto>(_mapper.Map<GetAllCareersResponseDto>(career));
        }
    }
}
