using Microsoft.AspNetCore.Http;
using Schedule.Domain.Enums;
using Schedule.Shared.Managers;

namespace Schedule.Web.Managers
{
    public class AppUserManager : DefaultAppUserManager
    {
        public override ApplicationType Application => ApplicationType.ScheduleWeb;

        public AppUserManager(IHttpContextAccessor context) : base(context)
        {
        }
    }
}
