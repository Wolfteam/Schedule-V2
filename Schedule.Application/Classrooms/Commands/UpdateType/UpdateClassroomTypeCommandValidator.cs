using FluentValidation;
using Schedule.Domain.Enums;
using Schedule.Shared.Extensions;

namespace Schedule.Application.Classrooms.Commands.UpdateType
{
    public class UpdateClassroomTypeCommandValidator : AbstractValidator<UpdateClassroomTypeCommand>
    {
        public UpdateClassroomTypeCommandValidator()
        {
            RuleFor(cmd => cmd.Id)
                .GreaterThan(0)
                .WithGlobalErrorCode(AppMessageType.SchApiInvalidRequest);

            RuleFor(cmd => cmd.Dto.Name)
                .NotEmpty()
                .WithGlobalErrorCode(AppMessageType.SchApiInvalidRequest);
        }
    }
}
