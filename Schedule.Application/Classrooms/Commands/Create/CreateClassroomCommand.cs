using MediatR;
using Schedule.Domain.Dto;
using Schedule.Domain.Dto.Classrooms.Requests;
using Schedule.Domain.Dto.Classrooms.Responses;

namespace Schedule.Application.Classrooms.Commands.Create
{
    public class CreateClassroomCommand : BaseRequest<SaveClassroomRequestDto>, IRequest<ApiResponseDto<GetAllClassroomsResponseDto>>
    {
        public CreateClassroomCommand(SaveClassroomRequestDto dto) : base(dto)
        {
        }
    }
}
