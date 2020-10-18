using Microsoft.AspNetCore.Authorization;
using Schedule.Domain.Enums;
using System;

namespace Schedule.Shared.Authorization
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
    public class ScheduleHasPermissionAttribute : AuthorizeAttribute
    {
        public ScheduleHasPermissionAttribute(SchedulePermissionType permission) : base(((int)permission).ToString())
        {
        }
    }
}
