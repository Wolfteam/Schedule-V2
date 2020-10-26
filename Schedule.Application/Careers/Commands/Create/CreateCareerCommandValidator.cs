using FluentValidation;
using Schedule.Domain.Enums;
using Schedule.Shared.Extensions;

namespace Schedule.Application.Careers.Commands.Create
{
    public class CreateCareerCommandValidator : AbstractValidator<CreateCareerCommand>
    {
        public CreateCareerCommandValidator()
        {
            RuleFor(dto => dto.Dto.Name)
                .NotEmpty()
                .MaximumLength(255)
                .WithGlobalErrorCode(AppMessageType.SchApiInvalidRequest);
        }
    }
}
