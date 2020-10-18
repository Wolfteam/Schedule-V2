using Schedule.Domain.Enums;
using Schedule.Shared.Extensions;
using System.Security.Claims;

namespace Schedule.Api.IntegrationTests.Config
{
    public static class Users
    {
        public static ClaimsPrincipal GetAuthUser(long schoolId, SchedulePermissionType permission)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "Test user"),
                new Claim(Shared.AppConstants.SchoolClaim, $"{schoolId}"),
                new Claim(Shared.AppConstants.SchedulePermissionsClaim, permission.GetPermissionStringValue()),
            };
            var identity = new ClaimsIdentity(claims, AppConstants.TestAuthScheme);
            return new ClaimsPrincipal(identity);
        }
    }

}
