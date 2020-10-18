using Schedule.Domain.Dto.Classrooms.Requests;
using Schedule.Domain.Dto.Classrooms.Responses;

namespace Schedule.Application.Classrooms.Queries.GetAllTypes
{
    public class GetAllClassroomTypesQuery : BasePaginatedRequest<GetAllClassroomTypesRequestDto, GetAllClassroomTypesResponseDto>
    {
        public GetAllClassroomTypesQuery(GetAllClassroomTypesRequestDto dto) : base(dto)
        {
        }
    }
}
