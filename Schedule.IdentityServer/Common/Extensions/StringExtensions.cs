using System.Reflection;

namespace Schedule.IdentityServer.Common.Extensions
{
    public static class StringExtensions
    {
        public static string GetCookieName(this string suffix)
        {
            var prefix = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            return $"{prefix}.{suffix}";
        }
    }
}
