using Microsoft.AspNetCore.Http;
using Schedule.Domain.Enums;
using Schedule.Shared.Managers;

namespace Schedule.IdentityServer.Managers
{
    public class AppUserManager : DefaultAppUserManager
    {
        public override ApplicationType Application => ApplicationType.IdentityServer;

        public AppUserManager(IHttpContextAccessor context) : base(context)
        {
        }
    }
}
