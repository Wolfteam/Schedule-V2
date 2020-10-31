using Schedule.Domain.Dto.Careers.Responses;

namespace Schedule.Application.Careers.Queries.Get
{
    public class GetCareerQuery : BaseApiRequest<GetAllCareersResponseDto>
    {
        public long Id { get; }

        public GetCareerQuery(long id)
        {
            Id = id;
        }
    }
}
