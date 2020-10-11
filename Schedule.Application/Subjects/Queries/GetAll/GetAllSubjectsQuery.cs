using Schedule.Domain.Dto.Subjects.Requests;
using Schedule.Domain.Dto.Subjects.Responses;

namespace Schedule.Application.Subjects.Queries.GetAll
{
    public class GetAllSubjectsQuery : BasePaginatedRequest<GetAllSubjectsRequestDto, GetAllSubjectsResponseDto>
    {
        public GetAllSubjectsQuery(GetAllSubjectsRequestDto dto) : base(dto)
        {
        }
    }
}
