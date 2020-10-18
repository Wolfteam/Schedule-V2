using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Schedule.Domain.Enums;
using Schedule.IdentityServer.Models.Entities;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Schedule.IdentityServer.Services
{
    public class CustomProfileService : IProfileService
    {
        private readonly ILogger<CustomProfileService> _logger;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomProfileService(
            ILogger<CustomProfileService> logger,
            UserManager<ApplicationUser> userManager,
            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)
        {
            _logger = logger;
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            _logger.LogInformation($"{nameof(GetProfileDataAsync)}: Retrieving all user claims associated to userId = {sub}");
            var user = await _userManager.FindByIdAsync(sub);
            var principal = await _claimsFactory.CreateAsync(user);
            var claims = principal.Claims.ToList();
            //claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();

            //var roleNames = await _userManager.GetRolesAsync(user);
            //var roles = _roleManager.Roles.Where(r => roleNames.Contains(r.Name)).ToList();
            //foreach (var role in roles)
            //{
            //    var roleClaims = await _roleManager.GetClaimsAsync(role);
            //    claims.AddRange(roleClaims.Select(rc => new Claim("permissions", rc.Type)));
            //}

            //var clientClaims = context.Client.Claims.Select(cc => new Claim(cc.Type, cc.Value, cc.ValueType)).ToList();
            //claims.AddRange(clientClaims);
            //claims.Add(new Claim(JwtClaimTypes.Email, user.Email));
            //claims.Add(new Claim(JwtClaimTypes.EmailVerified, $"{user.EmailConfirmed}"));
            //claims.Add(new Claim(Shared.AppConstants.LanguageClaim, $"{user.Language}"));
            claims.Add(new Claim(JwtClaimTypes.FamilyName, user.FullName));
            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            _logger.LogInformation($"{nameof(IsActiveAsync)}: Checking if userId = {sub} associated to clientId = {context.Client} is active...");
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null && user.Status != UserStatusType.Locked;
            _logger.LogInformation($"{nameof(IsActiveAsync)}: UserStatus = {user?.Status} - IsActive = {context.IsActive}");
        }
    }
}
