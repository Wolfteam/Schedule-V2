using Schedule.Domain.Dto.Classrooms.Responses;

namespace Schedule.Application.Classrooms.Queries.Get
{
    public class GetClassroomQuery : BaseApiRequest<GetAllClassroomsResponseDto>
    {
        public long Id { get; }

        public GetClassroomQuery(long id)
        {
            Id = id;
        }
    }
}
