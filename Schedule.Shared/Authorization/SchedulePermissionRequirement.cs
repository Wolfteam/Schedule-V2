using Microsoft.AspNetCore.Authorization;
using System;

namespace Schedule.Shared.Authorization
{
    public class SchedulePermissionRequirement : IAuthorizationRequirement
    {
        public string PermissionName { get; }
        public SchedulePermissionRequirement(string permissionName)
        {
            PermissionName = permissionName ?? throw new ArgumentNullException(nameof(permissionName));
        }
    }
}
