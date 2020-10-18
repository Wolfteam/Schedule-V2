using Schedule.Domain.Dto.Periods.Requests;
using Schedule.Domain.Dto.Periods.Responses;

namespace Schedule.Application.Periods.Commands.Create
{
    public class CreatePeriodCommand : BaseApiRequest<SavePeriodRequestDto, GetAllPeriodsResponseDto>
    {
        public CreatePeriodCommand(SavePeriodRequestDto dto) : base(dto)
        {
        }
    }
}
