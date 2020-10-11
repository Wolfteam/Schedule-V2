using Schedule.Domain.Dto.Classrooms.Requests;
using Schedule.Domain.Dto.Classrooms.Responses;

namespace Schedule.Application.Classrooms.Commands.Update
{
    public class UpdateClassroomCommand : BaseApiRequest<SaveClassroomRequestDto, GetAllClassroomsResponseDto>
    {
        public long Id { get; }
        public UpdateClassroomCommand(long id, SaveClassroomRequestDto dto) : base(dto)
        {
            Id = id;
        }
    }
}
