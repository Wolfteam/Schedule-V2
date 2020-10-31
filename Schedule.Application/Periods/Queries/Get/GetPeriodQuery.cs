using Schedule.Domain.Dto.Periods.Responses;

namespace Schedule.Application.Periods.Queries.Get
{
    public class GetPeriodQuery : BaseApiRequest<GetAllPeriodsResponseDto>
    {
        public long Id { get; }

        public GetPeriodQuery(long id)
        {
            Id = id;
        }
    }
}
