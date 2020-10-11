using Schedule.Domain.Dto.Teachers.Requests;
using Schedule.Domain.Dto.Teachers.Responses;

namespace Schedule.Application.Teachers.Queries.GetAll
{
    public class GetAllTeachersQuery : BasePaginatedRequest<GetAllTeachersRequestDto, GetAllTeacherResponseDto>
    {
        public GetAllTeachersQuery(GetAllTeachersRequestDto dto) : base(dto)
        {
        }
    }
}
