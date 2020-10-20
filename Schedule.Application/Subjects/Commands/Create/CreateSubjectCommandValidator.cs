using FluentValidation;
using Schedule.Domain.Enums;
using Schedule.Shared.Extensions;

namespace Schedule.Application.Subjects.Commands.Create
{
    public class CreateSubjectCommandValidator : AbstractValidator<CreateSubjectCommand>
    {
        public CreateSubjectCommandValidator()
        {
            var error = AppMessageType.SchApiInvalidRequest;
            RuleFor(cmd => cmd.Dto.Name)
                .NotEmpty()
                .MaximumLength(100)
                .WithGlobalErrorCode(error);

            RuleFor(cmd => cmd.Dto.CareerId)
                .GreaterThan(0)
                .WithGlobalErrorCode(error);

            RuleFor(cmd => cmd.Dto.ClassroomTypePerSubjectId)
                .GreaterThan(0)
                .WithGlobalErrorCode(error);

            RuleFor(cmd => cmd.Dto.TotalAcademicHours)
                .GreaterThan(0)
                .WithGlobalErrorCode(error);

            RuleFor(cmd => cmd.Dto.SemesterId)
                .GreaterThan(0)
                .WithGlobalErrorCode(error);

            RuleFor(cmd => cmd.Dto.AcademicHoursPerWeek)
                .GreaterThan(0)
                .WithGlobalErrorCode(error);
        }
    }
}
