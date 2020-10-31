using Schedule.Domain.Dto.Semesters.Responses;

namespace Schedule.Application.Semesters.Queries.Get
{
    public class GetSemesterQuery : BaseApiRequest<GetAllSemestersResponseDto>
    {
        public long Id { get; }

        public GetSemesterQuery(long id)
        {
            Id = id;
        }
    }
}
