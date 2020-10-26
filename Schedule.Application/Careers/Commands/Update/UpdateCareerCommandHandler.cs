using AutoMapper;
using Microsoft.Extensions.Logging;
using Schedule.Application.Interfaces.Managers;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Careers.Responses;
using Schedule.Shared.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Application.Careers.Commands.Update
{
    public class UpdateCareerCommandHandler : BaseApiRequestHandler<UpdateCareerCommand, GetAllCareersResponseDto>
    {
        private readonly IMapper _mapper;
        public UpdateCareerCommandHandler(
            ILogger<UpdateCareerCommand> logger,
            IAppDataService appDataService,
            IAppUserManager appUserManager,
            IMapper mapper)
            : base(logger, appDataService, appUserManager)
        {
            _mapper = mapper;
        }

        public override async Task<ApiResponseDto<GetAllCareersResponseDto>> Handle(UpdateCareerCommand request, CancellationToken cancellationToken)
        {
            var career = await AppDataService.Careers.FirstOrDefaultAsync(c => c.Id == request.Id && c.SchoolId == AppUserManager.SchoolId);
            if (career == null)
            {
                var msg = $"CareerId = {request.Id} does not exist";
                Logger.LogWarning($"{nameof(Handle)}: {msg}");
                throw new NotFoundException(msg);
            }

            if (career.Name != request.Dto.Name)
            {
                bool isBeingUsed = await AppDataService.Careers.ExistsAsync(c => c.Name == request.Dto.Name && c.SchoolId == AppUserManager.SchoolId);
                if (isBeingUsed)
                {
                    var msg = $"Career = {request.Dto.Name} already exists";
                    throw new ResourceAlreadyExistsException(msg);
                }
            }

            career.Name = request.Dto.Name;
            await AppDataService.SaveChangesAsync();

            return new ApiResponseDto<GetAllCareersResponseDto>(_mapper.Map<GetAllCareersResponseDto>(career));
        }
    }
}
