using System.Collections.Generic;
using Schedule.Domain.Dto.Teachers.Requests;
using Schedule.Domain.Dto.Teachers.Responses;

namespace Schedule.Application.Teachers.Queries.GetAll
{
    public class GetAllTeachersQueryValidator : BasePaginatedRequestValidator<GetAllTeachersRequestDto>
    {
        public override IReadOnlyList<string> OrderByOnLy { get; } = new List<string>
        {
            nameof(GetAllTeacherResponseDto.FirstName),
            nameof(GetAllTeacherResponseDto.SecondName),
            nameof(GetAllTeacherResponseDto.IdentifierNumber)
        };
    }
}
