using Schedule.Domain.Dto.Careers.Requests;
using Schedule.Domain.Dto.Careers.Responses;
using System.Collections.Generic;

namespace Schedule.Application.Careers.Queries.GetAll
{
    public class GetAllCareersQueryValidator : BasePaginatedRequestValidator<GetAllCareersRequestDto>
    {
        public override IReadOnlyList<string> OrderByOnLy => new List<string>
        {
            nameof(GetAllCareersResponseDto.Name)
        };
    }
}
