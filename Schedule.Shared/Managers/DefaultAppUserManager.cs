using IdentityModel;
using Microsoft.AspNetCore.Http;
using Schedule.Domain.Enums;
using Schedule.Domain.Interfaces.Managers;
using System;
using System.Linq;

namespace Schedule.Shared.Managers
{
    public abstract class DefaultAppUserManager : IDefaultAppUserManager
    {
        private const string Na = "N/A";

        public string Username { get; }
        public string Email { get; }
        public string FullName { get; }
        public AppLanguageType Language { get; }
        public abstract ApplicationType Application { get; }

        protected DefaultAppUserManager(IHttpContextAccessor context)
        {
            var httpContext = context.HttpContext;
            Username = string.IsNullOrEmpty(context.HttpContext?.User.Identity.Name)
                ? (context.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.ClientId)?.Value ?? Na)
                : context.HttpContext?.User.Identity.Name;

            Email = context.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Email)?.Value ?? Na;

            FullName = httpContext?.User.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.FamilyName)?.Value ?? Na;

            Language = GetLanguage(httpContext?.User.Claims.FirstOrDefault(c => c.Type == AppConstants.LanguageClaim)?.Value);
        }

        private static AppLanguageType GetLanguage(string val)
        {
            return string.IsNullOrEmpty(val)
                ? AppLanguageType.English
                : (AppLanguageType)Enum.Parse(typeof(AppLanguageType), val, true);
        }
    }
}
