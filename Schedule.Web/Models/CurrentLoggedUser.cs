using Schedule.Domain.Enums;

namespace Schedule.Web.Models
{
    public class CurrentLoggedUser
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public AppLanguageType Language { get; set; }
        public SchedulePermissionType Permissions { get; set; }
    }
}
