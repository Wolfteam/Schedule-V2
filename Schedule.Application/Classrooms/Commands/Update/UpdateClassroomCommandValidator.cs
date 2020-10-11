using FluentValidation;
using Schedule.Domain.Enums;
using Schedule.Shared.Extensions;

namespace Schedule.Application.Classrooms.Commands.Update
{
    public class UpdateClassroomCommandValidator : AbstractValidator<UpdateClassroomCommand>
    {
        public UpdateClassroomCommandValidator()
        {
            var error = AppMessageType.SchInvalidRequest;
            RuleFor(cmd => cmd.Id)
                .GreaterThan(0)
                .WithGlobalErrorCode(error);

            RuleFor(cmd => cmd.Dto.Name)
                .NotEmpty()
                .MaximumLength(100)
                .WithGlobalErrorCode(error);

            RuleFor(cmd => cmd.Dto.ClassroomTypePerSubjectId)
                .NotEmpty()
                .WithGlobalErrorCode(error);

            RuleFor(cmd => cmd.Dto.Capacity)
                .GreaterThan(0)
                .WithGlobalErrorCode(error);
        }
    }
}
