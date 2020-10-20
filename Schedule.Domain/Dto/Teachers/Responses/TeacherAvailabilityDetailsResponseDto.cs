using System.Collections.Generic;

namespace Schedule.Domain.Dto.Teachers.Responses
{
    public class TeacherAvailabilityDetailsResponseDto
    {
        public long TeacherId { get; }
        public int HoursToComplete { get; }
        public int AssignedHours { get; }
        public List<TeacherAvailabilityResponseDto> Availability { get; }

        public TeacherAvailabilityDetailsResponseDto(long teacherId, int hoursToComplete, int assignedHours, List<TeacherAvailabilityResponseDto> availability)
        {
            TeacherId = teacherId;
            HoursToComplete = hoursToComplete;
            AssignedHours = assignedHours;
            Availability = availability;
        }
    }
}
