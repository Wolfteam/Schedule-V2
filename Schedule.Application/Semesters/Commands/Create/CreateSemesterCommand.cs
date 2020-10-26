using Schedule.Domain.Dto.Semesters.Requests;
using Schedule.Domain.Dto.Semesters.Responses;

namespace Schedule.Application.Semesters.Commands.Create
{
    public class CreateSemesterCommand : BaseApiRequest<SaveSemesterRequestDto, GetAllSemestersResponseDto>
    {
        public CreateSemesterCommand(SaveSemesterRequestDto dto) : base(dto)
        {
        }
    }
}
