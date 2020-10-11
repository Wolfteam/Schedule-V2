using Schedule.Domain.Dto.Classrooms.Requests;
using Schedule.Domain.Dto.Classrooms.Responses;
using System.Collections.Generic;

namespace Schedule.Application.Classrooms.Queries.GetAll
{
    public class GetAllClassroomsQueryValidator : BasePaginatedRequestValidator<GetAllClassroomsRequestDto>
    {
        public override IReadOnlyList<string> OrderByOnLy => new List<string>
        {
            nameof(GetAllClassroomsResponseDto.Name),
            nameof(GetAllClassroomsResponseDto.ClassroomType),
            nameof(GetAllClassroomsResponseDto.Capacity)
        };
    }
}
