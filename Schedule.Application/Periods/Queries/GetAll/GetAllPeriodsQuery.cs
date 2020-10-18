using Schedule.Domain.Dto.Periods.Requests;
using Schedule.Domain.Dto.Periods.Responses;

namespace Schedule.Application.Periods.Queries.GetAll
{
    public class GetAllPeriodsQuery : BasePaginatedRequest<GetAllPeriodsRequestDto, GetAllPeriodsResponseDto>
    {
        public GetAllPeriodsQuery(GetAllPeriodsRequestDto dto) : base(dto)
        {
        }
    }
}
