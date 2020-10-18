using Schedule.Domain.Dto.Periods.Requests;
using Schedule.Domain.Dto.Periods.Responses;

namespace Schedule.Application.Periods.Commands.Update
{
    public class UpdatePeriodCommand : BaseApiRequest<SavePeriodRequestDto, GetAllPeriodsResponseDto>
    {
        public long Id { get; }
        public UpdatePeriodCommand(long id, SavePeriodRequestDto dto) : base(dto)
        {
            Id = id;
        }
    }
}
