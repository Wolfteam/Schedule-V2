using Schedule.Domain.Dto.Careers.Requests;
using Schedule.Domain.Dto.Careers.Responses;

namespace Schedule.Application.Careers.Commands.Update
{
    public class UpdateCareerCommand : BaseApiRequest<SaveCareerRequestDto, GetAllCareersResponseDto>
    {
        public long Id { get; }
        public UpdateCareerCommand(long id, SaveCareerRequestDto dto) : base(dto)
        {
            Id = id;
        }
    }
}
