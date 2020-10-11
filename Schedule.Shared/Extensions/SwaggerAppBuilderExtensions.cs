using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Schedule.Shared.Models.Settings;

namespace Schedule.Shared.Extensions
{
    public static class SwaggerAppBuilderExtensions
    {
        public static IApplicationBuilder UseSwagger(
            this IApplicationBuilder app,
            IConfiguration config,
            string apiName,
            string version = "V1")
        {
            var settings = config.GetSection(nameof(SwaggerSettings)).Get<SwaggerSettings>();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                // To serve the Swagger UI at the app's root (http://localhost:<random_port>/)
                c.SwaggerEndpoint($"../swagger/{version}/swagger.json", $"{apiName} {version}");
                c.DocumentTitle = $"{apiName} API";

                c.OAuthClientId(settings.SwaggerClientId);
                c.OAuthClientSecret(settings.SwaggerClientSecret);
                c.OAuthRealm("realm");
                c.OAuthAppName(apiName);
                c.OAuthScopeSeparator(" ");
                c.OAuthUseBasicAuthenticationWithAccessCodeGrant();
            });

            return app;
        }
    }
}
