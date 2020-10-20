using FluentValidation;
using Schedule.Domain.Dto.Teachers.Requests;
using Schedule.Domain.Enums;
using Schedule.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Schedule.Application.Teachers.Commands.SaveAvailability
{
    public class SaveAvailabilityCommandValidator : AbstractValidator<SaveAvailabilityCommand>
    {
        public SaveAvailabilityCommandValidator()
        {
            var error = AppMessageType.SchApiInvalidRequest;
            RuleFor(cmd => cmd.TeacherId)
                .GreaterThan(0)
                .WithGlobalErrorCode(error);

            RuleFor(cmd => cmd.Dto.Availability)
                .NotEmpty()
                //TODO: MOVE THIS TO A SETTING
                .Must(val => AreAvailabilitiesValid(2, val))
                .WithGlobalMessage("One of the provided availabilities is not valid. Check for duplicates or overlaps")
                .WithGlobalErrorCode(error);

            RuleForEach(cmd => cmd.Dto.Availability)
                .SetValidator(new AvailabilityValidator())
                .WithGlobalErrorCode(error);
        }

        private static bool AreAvailabilitiesValid(int minHoursPerDay, IReadOnlyCollection<TeacherAvailabilityRequestDto> availabilities)
        {
            if (availabilities.Count == 0)
                return false;

            var lunchHour = LaboralHoursType.TwelvePmToOnePm;
            if (availabilities.Any(a => a.StartHour == lunchHour || a.EndHour == lunchHour))
                return false;

            bool duplicatesExists = availabilities
                .GroupBy(a => $"{a.Day}_{a.StartHour}_{a.EndHour}").Select(g => g.Key).Count() != availabilities.Count;

            if (duplicatesExists)
                return false;

            foreach (LaboralDaysType day in Enum.GetValues(typeof(LaboralDaysType)))
            {
                var availabilityForToday = availabilities.Where(a => a.Day == day).ToList();
                if (availabilityForToday.Count == 0)
                    continue;

                var occurrences = 0;
                foreach (var item in availabilityForToday)
                {
                    bool overlapExists = OverlapExists(item, availabilityForToday.Where(a => a.StartHour != item.StartHour && a.EndHour != item.EndHour).ToList());
                    if (overlapExists)
                        return false;

                    var diff = item.EndHour - item.StartHour + 1;
                    if (diff >= minHoursPerDay)
                        occurrences++;
                }

                if (occurrences != availabilityForToday.Count)
                    return false;
            }
            return true;
        }

        private static bool OverlapExists(TeacherAvailabilityRequestDto current, IReadOnlyCollection<TeacherAvailabilityRequestDto> availabilities)
        {
            if (availabilities.Count == 0)
                return false;

            foreach (var item in availabilities)
            {
                var currentStart = (int)current.StartHour;
                var currentEnd = (int)current.EndHour;
                var startHour = (int)item.StartHour;
                var endHour = (int)item.EndHour;

                for (int i = currentStart; i <= currentEnd; i++)
                {
                    if (i == startHour || i == endHour)
                        return true;
                }
            }

            return false;
        }
    }

    class AvailabilityValidator : AbstractValidator<TeacherAvailabilityRequestDto>
    {
        public AvailabilityValidator()
        {
            var error = AppMessageType.SchApiInvalidRequest;
            RuleFor(dto => dto.StartHour)
                .IsInEnum()
                .Must((dto, val) => (int)val < (int)dto.EndHour)
                .WithGlobalErrorCode(error);

            RuleFor(dto => dto.EndHour)
                .IsInEnum()
                .Must((dto, val) => (int)val > (int)dto.StartHour)
                .WithGlobalErrorCode(error);

            RuleFor(dto => dto.Day)
                .IsInEnum()
                .WithGlobalErrorCode(error);
        }
    }
}
