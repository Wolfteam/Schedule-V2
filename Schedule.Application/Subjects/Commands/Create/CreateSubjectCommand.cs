using Schedule.Domain.Dto.Subjects.Requests;
using Schedule.Domain.Dto.Subjects.Responses;

namespace Schedule.Application.Subjects.Commands.Create
{
    public class CreateSubjectCommand : BaseApiRequest<SaveSubjectRequestDto, GetAllSubjectsResponseDto>
    {
        public CreateSubjectCommand(SaveSubjectRequestDto dto) : base(dto)
        {
        }
    }
}
