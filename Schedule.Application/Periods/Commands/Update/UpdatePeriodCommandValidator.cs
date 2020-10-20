using FluentValidation;
using Schedule.Domain.Enums;
using Schedule.Shared.Extensions;

namespace Schedule.Application.Periods.Commands.Update
{
    public class UpdatePeriodCommandValidator : AbstractValidator<UpdatePeriodCommand>
    {
        public UpdatePeriodCommandValidator()
        {
            var error = AppMessageType.SchApiInvalidRequest;
            RuleFor(cmd => cmd.Id)
                .GreaterThan(0)
                .WithGlobalErrorCode(error);

            RuleFor(cmd => cmd.Dto.Name)
                .NotEmpty()
                .WithGlobalErrorCode(error);
        }
    }
}
