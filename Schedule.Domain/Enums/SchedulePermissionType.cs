using System;

namespace Schedule.Domain.Enums
{
    [Flags]
    public enum SchedulePermissionType
    {
        None = 0,
        Create = 1,

        All = Create
    }
}
