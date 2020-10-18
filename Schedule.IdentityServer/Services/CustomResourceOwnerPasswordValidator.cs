using IdentityModel;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Schedule.Domain.Enums;
using Schedule.IdentityServer.Models;
using Schedule.IdentityServer.Models.Entities;
using System.Threading.Tasks;

namespace Schedule.IdentityServer.Services
{
    public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ResourceOwnerPasswordValidator<ApplicationUser>> _logger;
        private readonly AspIdentityDbContext _dbContext;

        public CustomResourceOwnerPasswordValidator(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<ResourceOwnerPasswordValidator<ApplicationUser>> logger,
            AspIdentityDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await _userManager.FindByNameAsync(context.UserName);
            if (user == null)
            {
                _logger.LogInformation($"No user found matching username: {context.UserName}");
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "User was not found");
                return;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, context.Password, true);

            if (result.IsLockedOut || user.Status == UserStatusType.Locked)
            {
                if (user.Status != UserStatusType.Locked)
                {
                    user.Status = UserStatusType.Locked;
                    _dbContext.Update(user);
                    await _dbContext.SaveChangesAsync();
                }

                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User is locked");
                _logger.LogInformation($"Authentication failed for username: {context.UserName}, reason: locked out");
                return;
            }

            if (result.IsNotAllowed)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User is not allowed");
                _logger.LogInformation($"Authentication failed for username: {context.UserName}, reason: not allowed");
                return;
            }

            if (result.Succeeded)
            {
                var sub = await _userManager.GetUserIdAsync(user);

                _logger.LogInformation($"Credentials validated for username: {context.UserName}");

                context.Result = new GrantValidationResult(sub, OidcConstants.AuthenticationMethods.Password);
                return;
            }

            _logger.LogInformation($"Authentication failed for username: {context.UserName}, reason: invalid credentials");
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "User is not allowed. Please verify user and password");
        }
    }
}
