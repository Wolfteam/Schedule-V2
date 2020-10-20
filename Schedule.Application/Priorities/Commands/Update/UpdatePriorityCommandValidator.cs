using FluentValidation;
using Schedule.Domain.Enums;
using Schedule.Shared.Extensions;

namespace Schedule.Application.Priorities.Commands.Update
{
    public class UpdatePriorityCommandValidator : AbstractValidator<UpdatePriorityCommand>
    {
        public UpdatePriorityCommandValidator()
        {
            var error = AppMessageType.SchApiInvalidRequest;
            RuleFor(dto => dto.Id)
                .GreaterThan(0)
                .WithGlobalErrorCode(error);

            RuleFor(cmd => cmd.Dto.Name)
                .NotEmpty()
                .WithGlobalErrorCode(error);

            RuleFor(cmd => cmd.Dto.HoursToComplete)
                .GreaterThan(0)
                .WithGlobalErrorCode(error);
        }
    }
}
