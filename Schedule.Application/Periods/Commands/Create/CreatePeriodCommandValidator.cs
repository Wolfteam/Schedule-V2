using FluentValidation;
using Schedule.Domain.Enums;
using Schedule.Shared.Extensions;

namespace Schedule.Application.Periods.Commands.Create
{
    public class CreatePeriodCommandValidator : AbstractValidator<CreatePeriodCommand>
    {
        public CreatePeriodCommandValidator()
        {
            var error = AppMessageType.SchInvalidRequest;
            RuleFor(cmd => cmd.Dto.Name)
                .NotEmpty()
                .WithGlobalErrorCode(error);
        }
    }
}
