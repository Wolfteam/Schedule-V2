using Schedule.Api.Controllers;
using Schedule.Shared.Extensions;
using Schedule.Shared.Models.Logging;
using System.Collections.Generic;

namespace Schedule.Api.Common
{
    public static class LoggingConfig
    {
        public static void SetupLogging()
        {
            var logs = new List<FileToLog>
            {
                new FileToLog(typeof(TeacherController), "controllers_teacher")
            };

            logs.SetupLogging();
        }
    }
}
