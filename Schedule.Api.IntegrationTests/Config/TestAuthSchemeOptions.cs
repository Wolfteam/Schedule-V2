using Microsoft.AspNetCore.Authentication;
using Schedule.Domain.Enums;

namespace Schedule.Api.IntegrationTests.Config
{
    public class TestAuthSchemeOptions : AuthenticationSchemeOptions
    {
        public long SchoolId { get; set; }
        public SchedulePermissionType Permissions { get; set; }
    }
}
