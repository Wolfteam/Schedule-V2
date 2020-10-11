using Schedule.Domain.Dto.Classrooms.Requests;
using Schedule.Domain.Dto.Classrooms.Responses;

namespace Schedule.Application.Classrooms.Queries.GetAll
{
    public class GetAllClassroomsQuery : BasePaginatedRequest<GetAllClassroomsRequestDto, GetAllClassroomsResponseDto>
    {
        public GetAllClassroomsQuery(GetAllClassroomsRequestDto dto) : base(dto)
        {
        }
    }
}
