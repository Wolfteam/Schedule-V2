﻿using FluentValidation;
using Schedule.Domain.Enums;
using Schedule.Shared.Extensions;

namespace Schedule.Application.Classrooms.Commands.Update
{
    public class UpdateClassroomCommandValidator : AbstractValidator<UpdateClassroomCommand>
    {
        public UpdateClassroomCommandValidator()
        {
            var error = AppMessageType.SchApiInvalidRequest;
            RuleFor(cmd => cmd.Id)
                .GreaterThan(0)
                .WithGlobalErrorCode(error);

            RuleFor(cmd => cmd.Dto.Name)
                .NotEmpty()
                .MaximumLength(100)
                .WithGlobalErrorCode(error);

            RuleFor(cmd => cmd.Dto.ClassroomSubjectId)
                .NotEmpty()
                .WithGlobalErrorCode(error);

            RuleFor(cmd => cmd.Dto.Capacity)
                .GreaterThan(0)
                .WithGlobalErrorCode(error);
        }
    }
}
