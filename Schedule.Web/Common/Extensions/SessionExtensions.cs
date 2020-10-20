using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Schedule.Web.Common.Extensions
{
    public static class SessionExtensions
    {
        public static T GetComplex<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            return data == null ? default : JsonConvert.DeserializeObject<T>(data);
        }

        public static void SetComplex(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
    }
}
