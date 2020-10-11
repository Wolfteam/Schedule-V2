using Schedule.Domain.Dto.Priorities.Requests;
using Schedule.Domain.Dto.Priorities.Responses;

namespace Schedule.Application.Priorities.Queries.GetAll
{
    public class GetAllPrioritiesQuery : BasePaginatedRequest<GetAllPrioritiesRequestDto, GetAllPrioritiesResponseDto>
    {
        public GetAllPrioritiesQuery(GetAllPrioritiesRequestDto dto) : base(dto)
        {
        }
    }
}
