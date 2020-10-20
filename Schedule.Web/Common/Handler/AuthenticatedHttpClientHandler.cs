using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Schedule.Web.Common.Extensions;
using Schedule.Web.Models;
using Schedule.Web.Models.Settings;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.Web.Common.Handler
{
    public class AuthenticatedHttpClientHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IdentityServerSettings _identityServerSettings;

        public AuthenticatedHttpClientHandler(
            IHttpContextAccessor httpContextAccessor,
            IdentityServerSettings identityServerServerSettings)
        {
            _httpContextAccessor = httpContextAccessor;
            _identityServerSettings = identityServerServerSettings;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var currentUser = _httpContextAccessor.HttpContext.Session.GetComplex<CurrentUser>(nameof(CurrentUser));

            //TODO: THIS MAY CHANGE IN THE FUTURE
            if (request.Headers.Authorization.Scheme != JwtBearerDefaults.AuthenticationScheme || string.IsNullOrEmpty(currentUser?.AccessToken))
                return await base.SendAsync(request, cancellationToken);

            var expDate = GetExpirationDate(currentUser.AccessToken);
            bool isExpired = !expDate.HasValue || expDate <= DateTime.Now;
            if (isExpired)
            {
                var tokenResponse = await RefreshToken(currentUser.RefreshToken);
                currentUser = new CurrentUser
                {
                    RefreshToken = tokenResponse.RefreshToken,
                    AccessToken = tokenResponse.AccessToken
                };
                _httpContextAccessor.HttpContext.Session.SetComplex(nameof(CurrentUser), currentUser);
            }

            request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, currentUser.AccessToken);

            return await base.SendAsync(request, cancellationToken);
        }

        private static DateTime? GetExpirationDate(string jwtToken)
        {
            //Parse Jwt token
            var handler = new JwtSecurityTokenHandler();
            var tokenInfo = handler.ReadToken(jwtToken) as JwtSecurityToken;

            //Get "exp" claim
            var exp = tokenInfo?.Claims.FirstOrDefault(x => x.Type == "exp")?.Value;
            if (int.TryParse(exp, out int unixTimeSeconds))
                return DateTimeOffset.FromUnixTimeSeconds(unixTimeSeconds).ToLocalTime().DateTime;

            return null;
        }

        private async Task<TokenResponse> RefreshToken(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
                throw new Exception("Refresh token is required");

            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(_identityServerSettings.Authority);

            var tokenResponse = await client.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                RefreshToken = refreshToken,
                Address = disco.TokenEndpoint,
                ClientId = _identityServerSettings.ClientId,
                ClientSecret = _identityServerSettings.ClientSecret,
                Scope = _identityServerSettings.Scopes
            });

            return tokenResponse;
        }
    }
}
