using Schedule.Domain.Dto.Teachers.Responses;

namespace Schedule.Application.Teachers.Queries.Get
{
    public class GetTeacherQuery : BaseApiRequest<GetAllTeacherResponseDto>
    {
        public long Id { get; }

        public GetTeacherQuery(long id)
        {
            Id = id;
        }
    }
}
