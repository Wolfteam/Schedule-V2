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
                new FileToLog(typeof(TeacherController), "controllers_classroom"),
                new FileToLog(typeof(TeacherController), "controllers_periods"),
                new FileToLog(typeof(TeacherController), "controllers_subjects"),
                new FileToLog(typeof(TeacherController), "controllers_teacher"),
                //Others
                new FileToLog(typeof(Startup), "app_startup"),
            };

            logs.SetupLogging();
        }
    }
}
