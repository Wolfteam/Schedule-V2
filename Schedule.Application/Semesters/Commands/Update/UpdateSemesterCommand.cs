using Schedule.Domain.Dto.Semesters.Requests;
using Schedule.Domain.Dto.Semesters.Responses;

namespace Schedule.Application.Semesters.Commands.Update
{
    public class UpdateSemesterCommand : BaseApiRequest<SaveSemesterRequestDto, GetAllSemestersResponseDto>
    {
        public long Id { get; }
        public UpdateSemesterCommand(long id, SaveSemesterRequestDto dto) : base(dto)
        {
            Id = id;
        }
    }
}
