using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Schedule.Api.IntegrationTests.Config
{
    public class TestAuthHandler : AuthenticationHandler<TestAuthSchemeOptions>
    {
        public TestAuthHandler(
            IOptionsMonitor<TestAuthSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var principal = Users.GetAuthUser(Options.SchoolId ,Options.Permissions);
            var ticket = new AuthenticationTicket(principal, AppConstants.TestAuthScheme);

            var result = AuthenticateResult.Success(ticket);

            return Task.FromResult(result);
        }
    }
}
