using Schedule.Domain.Dto.Subjects.Responses;
using Schedule.Domain.Dto.Teachers.Requests;
using System.Collections.Generic;

namespace Schedule.Application.Subjects.Queries.GetAll
{
    public class GetAllSubjectsQueryValidator : BasePaginatedRequestValidator<GetAllTeachersRequestDto>
    {
        public override IReadOnlyList<string> OrderByOnLy { get; } = new List<string>
        {
            nameof(GetAllSubjectsResponseDto.Code),
            nameof(GetAllSubjectsResponseDto.Name),
            nameof(GetAllSubjectsResponseDto.TotalAcademicHours),
            nameof(GetAllSubjectsResponseDto.AcademicHoursPerWeek)
        };
    }
}
