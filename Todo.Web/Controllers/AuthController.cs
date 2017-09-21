using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Todo.Common.Logging;
using Todo.Web.Filters;
using Todo.Web.Models;
using Todo.Web.Security;

namespace Todo.Web.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : TodoControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtOptions _jwtOptions;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly JwtValidator _jwtValidator;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            IPasswordHasher<ApplicationUser> passwordHasher,
            ILoggerFactory loggerFactory,
            IOptions<JwtOptions> jwtOptions,
            JwtValidator jwtValidator)
            : base(loggerFactory)
        {
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
            _passwordHasher = passwordHasher;
            _jwtValidator = jwtValidator;
        }

        [AllowAnonymous]
        [ValidateModel]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            try
            {
                var user = new ApplicationUser()
                {
                    UserName = model.UserName
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return Ok(result);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("error", error.Description);
                }

                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                Logger.Error($"error while creating user: {ex}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "error while creating user");
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> CreateToken([FromBody]LoginViewModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    return Unauthorized();
                }

                if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) != PasswordVerificationResult.Success)
                {
                    return Unauthorized();
                }

                var userClaims = await _userManager.GetClaimsAsync(user);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    // Token ID. We need this to maintain a blacklist for Logout purpose.
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }.Union(userClaims);

                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SigningKey));
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, _jwtOptions.SigningAlgorithm);
                var rememberUser = model.RememberMe == true;
                var validTo = rememberUser ? DateTime.Now.AddDays(_jwtOptions.RememberMeExpireInDays) : DateTime.Now.AddMinutes(_jwtOptions.ExpireInMinutes);

                var jwtSecurityToken = new JwtSecurityToken(
                    issuer: _jwtOptions.Issuer,
                    audience: _jwtOptions.Audience,
                    claims: claims,
                    expires: validTo,
                    signingCredentials: signingCredentials
                );

                var jwt = _jwtValidator.WriteToken(jwtSecurityToken);

                if (_jwtOptions.UseCookie)
                {
                    SetTokenInCookie(jwt);
                }

                // Returns token as JSON.
                return Ok(new
                {
                    token = jwt,
                    expiration = validTo
                });
            }
            catch (Exception ex)
            {
                Logger.Error($"error while creating token: {ex}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "error while creating token");
            }
        }

        // Authentication by filter required since we don't validate token in the method.
        [HttpPost]
        [Route("renew")]
        public IActionResult RenewToken()
        {
            try
            {
                var protectedText = GetTokenFromRequest();

                if (string.IsNullOrEmpty(protectedText))
                {
                    return Unauthorized();
                }

                var newToken = _jwtValidator.RenewToken(protectedText, _jwtOptions);

                var jwt = _jwtValidator.WriteToken(newToken);

                if (_jwtOptions.UseCookie)
                {
                    SetTokenInCookie(jwt);
                }

                return Ok(new
                {
                    token = jwt,
                    expiration = newToken.ValidTo
                });
            }
            catch (Exception ex)
            {
                Logger.Error($"error while renew token: {ex}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "error while renew token");
            }
        }

        // Authentication by filter required since we don't validate token in the method.
        [HttpPost]
        [Route("revoke")]
        public IActionResult RevokeToken()
        {
            try
            {
                var protectedText = GetTokenFromRequest();

                if (string.IsNullOrEmpty(protectedText))
                {
                    return Unauthorized();
                }

                _jwtValidator.RevokeToken(protectedText);

                return Ok();
            }
            catch (Exception ex)
            {
                Logger.Error($"error while revoke token: {ex}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "error while revoke token");
            }
        }

        #region Private Methods

        private void SetTokenInCookie(string token)
        {
            Response.Cookies.Append(_jwtOptions.CookieName, token, new CookieOptions
            {
                HttpOnly = true,
                //Secure = true, // Todo: Enable this in production to ensure cookie only send over HTTPS.
                Path = "/",
                Expires = DateTimeOffset.Now.AddMinutes(_jwtOptions.ExpireInMinutes),
            });
        }

        private string GetTokenFromRequest()
        {
            string token;

            if (_jwtOptions.UseCookie)
            {
                Request.Cookies.TryGetValue(_jwtOptions.CookieName, out token);
            }
            else
            {
                Request.Headers.TryGetValue("Authorization", out var stringValues);
                var array = stringValues.FirstOrDefault()?.Split(" ");
                token = array?.Length == 1 ? array[1] : null;
            }

            return token;
        }

        #endregion
    }
}
