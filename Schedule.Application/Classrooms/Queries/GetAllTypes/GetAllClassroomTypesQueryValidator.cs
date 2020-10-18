using Schedule.Domain.Dto.Classrooms.Requests;
using Schedule.Domain.Dto.Classrooms.Responses;
using System.Collections.Generic;

namespace Schedule.Application.Classrooms.Queries.GetAllTypes
{
    public class GetAllClassroomTypesQueryValidator : BasePaginatedRequestValidator<GetAllClassroomTypesRequestDto>
    {
        public override IReadOnlyList<string> OrderByOnLy => new List<string>
        {
            nameof(GetAllClassroomTypesResponseDto.Name)
        };
    }
}
