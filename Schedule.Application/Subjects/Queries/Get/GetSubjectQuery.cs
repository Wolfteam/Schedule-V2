using Schedule.Domain.Dto.Subjects.Responses;

namespace Schedule.Application.Subjects.Queries.Get
{
    public class GetSubjectQuery : BaseApiRequest<GetAllSubjectsResponseDto>
    {
        public long Id { get; }

        public GetSubjectQuery(long id)
        {
            Id = id;
        }
    }
}
