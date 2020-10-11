using Schedule.Domain.Dto.Priorities.Requests;
using Schedule.Domain.Dto.Priorities.Responses;

namespace Schedule.Application.Priorities.Commands.Update
{
    public class UpdatePriorityCommand : BaseApiRequest<SavePriorityRequestDto, GetAllPrioritiesResponseDto>
    {
        public long Id { get; set; }
        public UpdatePriorityCommand(long id, SavePriorityRequestDto dto) : base(dto)
        {
            Id = id;
        }
    }
}
