using Microsoft.AspNetCore.Http;
using Schedule.Application.Interfaces.Managers;
using Schedule.Domain.Enums;
using Schedule.Shared;
using Schedule.Shared.Managers;
using System.Linq;

namespace Schedule.Api.Managers
{
    public class AppUserManager : DefaultAppUserManager, IAppUserManager
    {
        public override ApplicationType Application => ApplicationType.ScheduleApi;
        public long SchoolId { get; }

        public AppUserManager(IHttpContextAccessor context) : base(context)
        {
            var httpContext = context.HttpContext;
            var schoolClaim = httpContext?.User.Claims.FirstOrDefault(c => c.Type == AppConstants.SchoolClaim);
            SchoolId = long.Parse(schoolClaim?.Value ?? "0");
        }
    }
}
