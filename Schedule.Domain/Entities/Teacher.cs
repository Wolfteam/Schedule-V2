using System.Collections.Generic;

namespace Schedule.Domain.Entities
{
    public class Teacher : BaseEntityWithSchool
    {
        public int IdentifierNumber { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }

        public long PriorityId { get; set; }
        public Priority Priority { get; set; }
        public ICollection<TeacherAvailability> Availabilities { get; } = new List<TeacherAvailability>();
        public ICollection<TeacherSchedule> Schedules { get; } = new List<TeacherSchedule>();
        public ICollection<TeacherSubject> Subjects { get; } = new List<TeacherSubject>();

        public static Teacher NewTeacher(
            int identifierNumber,
            string firstName,
            string lastName,
            long priorityId,
            School school)
        {
            return new Teacher
            {
                IdentifierNumber = identifierNumber,
                FirstName = firstName,
                FirstLastName = lastName,
                PriorityId = priorityId,
                School = school
            };
        }
    }
}
