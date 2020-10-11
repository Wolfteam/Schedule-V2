using Schedule.Domain.Dto.Priorities.Requests;
using Schedule.Domain.Dto.Priorities.Responses;

namespace Schedule.Application.Priorities.Commands.Create
{
    public class CreatePriorityCommand : BaseApiRequest<SavePriorityRequestDto, GetAllPrioritiesResponseDto>
    {
        public CreatePriorityCommand(SavePriorityRequestDto dto) : base(dto)
        {
        }
    }
}
