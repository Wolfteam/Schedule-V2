using IdentityModel;
using Microsoft.AspNetCore.Http;
using Schedule.Application.Interfaces.Managers;
using System.Linq;

namespace Schedule.Api.Managers
{
    public class AppUserManager : IAppUserManager
    {
        private const string Na = "N/A";

        public long Id { get; }
        public string Username { get; }
        public string Email { get; }
        public string FullName { get; }

        public AppUserManager(IHttpContextAccessor context)
        {
            var httpContext = context.HttpContext;
            Username = string.IsNullOrEmpty(context.HttpContext?.User.Identity.Name)
                ? (context.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.ClientId)?.Value ?? Na)
                : context.HttpContext?.User.Identity.Name;

            Email = context.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Email)?.Value ?? Na;

            FullName = httpContext?.User.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.FamilyName)?.Value ?? Na;

            //Language = GetLanguage(httpContext?.User.Claims.FirstOrDefault(c => c.Type == AppConstants.LanguageClaim)?.Value);
        }
    }
}
