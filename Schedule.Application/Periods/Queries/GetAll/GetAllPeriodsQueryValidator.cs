using Schedule.Domain.Dto.Periods.Requests;
using Schedule.Domain.Dto.Periods.Responses;
using System.Collections.Generic;

namespace Schedule.Application.Periods.Queries.GetAll
{
    public class GetAllPeriodsQueryValidator : BasePaginatedRequestValidator<GetAllPeriodsRequestDto>
    {
        public override IReadOnlyList<string> OrderByOnLy => new List<string>
        {
            nameof(GetAllPeriodsResponseDto.Name)
        };
    }
}
