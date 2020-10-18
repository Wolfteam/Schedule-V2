using System.Collections.Generic;
using Schedule.IdentityServer.Services;
using Schedule.Shared.Extensions;
using Schedule.Shared.Models.Logging;

namespace Schedule.IdentityServer.Common
{
    public static class LoggingConfig
    {
        public static void SetupLogging()
        {
            var logs = new List<FileToLog>
            {
                //Others
                new FileToLog(typeof(Startup), "app_startup"),
                new FileToLog("identity_server"),
                new FileToLog(typeof(CustomProfileService), "service_profile"),
                new FileToLog(typeof(CustomResourceOwnerPasswordValidator), "validator_resource_owner")
            };

            logs.SetupLogging();
        }
    }
}
