using Schedule.Domain.Dto.Classrooms.Responses;

namespace Schedule.Application.Classrooms.Queries.GetType
{
    public class GetClassroomTypeQuery : BaseApiRequest<GetAllClassroomTypesResponseDto>
    {
        public long Id { get; }

        public GetClassroomTypeQuery(long id)
        {
            Id = id;
        }
    }
}
