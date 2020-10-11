using Schedule.Domain.Dto.Teachers.Requests;
using Schedule.Domain.Dto.Teachers.Responses;

namespace Schedule.Application.Teachers.Commands.Create
{
    public class CreateTeacherCommand : BaseApiRequest<SaveTeacherRequestDto, GetAllTeacherResponseDto>
    {
        public CreateTeacherCommand(SaveTeacherRequestDto dto) : base(dto)
        {
        }
    }
}
