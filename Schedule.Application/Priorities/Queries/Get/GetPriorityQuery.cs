using Schedule.Domain.Dto.Priorities.Responses;

namespace Schedule.Application.Priorities.Queries.Get
{
    public class GetPriorityQuery : BaseApiRequest<GetAllPrioritiesResponseDto>
    {
        public long Id { get; }

        public GetPriorityQuery(long id)
        {
            Id = id;
        }
    }
}
