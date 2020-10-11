using Microsoft.AspNetCore.Authorization;
using Schedule.Shared.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Shared.Authorization
{
    public class SchedulePermissionHandler : AuthorizationHandler<SchedulePermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SchedulePermissionRequirement requirement)
        {
            var permission = context.User?.Claims.SingleOrDefault(c => c.Type == AppConstants.SchedulePermissionsClaim);
            // If user does not have the claim, get out of here
            if (permission == null)
                return Task.CompletedTask;

            if (permission.Value.IsThisPermissionAllowed(requirement.PermissionName))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
