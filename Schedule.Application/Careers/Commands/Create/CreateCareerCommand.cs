using Schedule.Domain.Dto.Careers.Requests;
using Schedule.Domain.Dto.Careers.Responses;

namespace Schedule.Application.Careers.Commands.Create
{
    public class CreateCareerCommand : BaseApiRequest<SaveCareerRequestDto, GetAllCareersResponseDto>
    {
        public CreateCareerCommand(SaveCareerRequestDto dto) : base(dto)
        {
        }
    }
}
