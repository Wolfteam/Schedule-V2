using Schedule.Domain.Dto.Priorities.Requests;
using Schedule.Domain.Dto.Priorities.Responses;
using System.Collections.Generic;

namespace Schedule.Application.Priorities.Queries.GetAll
{
    public class GetAllPrioritiesQueryValidator : BasePaginatedRequestValidator<GetAllPrioritiesRequestDto>
    {
        public override IReadOnlyList<string> OrderByOnLy => new List<string>
        {
            nameof(GetAllPrioritiesResponseDto.Name),
            nameof(GetAllPrioritiesResponseDto.HoursToComplete)
        };
    }
}
