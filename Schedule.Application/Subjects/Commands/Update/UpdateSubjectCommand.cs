using Schedule.Domain.Dto.Subjects.Requests;
using Schedule.Domain.Dto.Subjects.Responses;

namespace Schedule.Application.Subjects.Commands.Update
{
    public class UpdateSubjectCommand : BaseApiRequest<SaveSubjectRequestDto, GetAllSubjectsResponseDto>
    {
        public long Id { get; }
        public UpdateSubjectCommand(long id, SaveSubjectRequestDto dto) : base(dto)
        {
            Id = id;
        }
    }
}
