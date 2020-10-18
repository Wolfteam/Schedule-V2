using Schedule.Domain.Dto.Careers.Requests;
using Schedule.Domain.Dto.Careers.Responses;

namespace Schedule.Application.Careers.Queries.GetAll
{
    public class GetAllCareersQuery : BasePaginatedRequest<GetAllCareersRequestDto, GetAllCareersResponseDto>
    {
        public GetAllCareersQuery(GetAllCareersRequestDto dto) : base(dto)
        {
        }
    }
}
