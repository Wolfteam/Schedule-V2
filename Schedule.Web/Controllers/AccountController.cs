using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Schedule.Domain.Dto;
using Schedule.Domain.Enums;
using Schedule.Domain.Interfaces.Managers;
using Schedule.Shared.Exceptions;
using Schedule.Web.Common.Dto.Account.Requests;
using Schedule.Web.Common.Extensions;
using Schedule.Web.Models;
using Schedule.Web.Models.Settings;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Schedule.Web.Controllers
{
    public class AccountController : BaseController<AccountController>
    {
        private readonly IdentityServerSettings _identityServerSettings;
        private readonly IDefaultAppUserManager _userManager;
        public AccountController(
            ILogger<AccountController> logger,
            IdentityServerSettings identityServerSettings,
            IDefaultAppUserManager userManager)
            : base(logger)
        {
            _identityServerSettings = identityServerSettings;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(_identityServerSettings.Authority);

            if (disco.IsError)
            {
                throw new Exception(disco.Error);
            }

            var request = new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = _identityServerSettings.ClientId,
                ClientSecret = _identityServerSettings.ClientSecret,
                Scope = _identityServerSettings.Scopes,
                UserName = dto.Username,
                Password = dto.Password
            };

            var response = await client.RequestPasswordTokenAsync(request);

            if (response.IsError)
            {
                throw new InvalidRequestException($"{response.ErrorDescription}. {response.Error}", AppMessageType.SchWebInvalidUsernameOrPassword);
            }

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(response.AccessToken) as JwtSecurityToken;

            var identity = new ClaimsIdentity(jsonToken?.Claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var props = new AuthenticationProperties
            {
                IsPersistent = dto.RememberMe
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                props);

            var currentUser = new CurrentUser
            {
                AccessToken = response.AccessToken,
                RefreshToken = response.RefreshToken
            };
            HttpContext.Session.SetComplex(nameof(CurrentUser), currentUser);

            return Ok(new EmptyResponseDto(true));
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await SignOut();
            }
            catch (Exception)
            {
                HttpContext.Session.Remove(nameof(CurrentUser));
            }

            return Ok(new EmptyResponseDto(true));
        }

        [AllowAnonymous]
        [HttpGet("CurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var currentUser = HttpContext.Session.GetComplex<CurrentUser>(nameof(CurrentUser));
            if (!User.Identity.IsAuthenticated || currentUser == null)
            {
                await SignOut();
                return Ok(new ApiResponseDto<CurrentLoggedUser>(null));
            }

            var user = new CurrentLoggedUser
            {
                Username = _userManager.Username,
                Language = _userManager.Language,
                FullName = _userManager.FullName
            };
            return Ok(new ApiResponseDto<CurrentLoggedUser>(user));
        }

        private async Task SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove(nameof(CurrentUser));
        }
    }
}
