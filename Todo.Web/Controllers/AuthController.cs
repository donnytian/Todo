using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<ApplicationUser> userManager, IPasswordHasher<ApplicationUser> passwordHasher
            , IConfiguration configuration, ILoggerFactory loggerFactory, IOptions<JwtOptions> jwtOptions)
            : base(loggerFactory)
        {
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

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

        [ValidateModel]
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

                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                var jwtSecurityToken = new JwtSecurityToken(
                    issuer: _jwtOptions.Issuer,
                    audience: _jwtOptions.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(_jwtOptions.ExpireInMinutes),
                    signingCredentials: signingCredentials
                );

                var jwt = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                // For demo purpose, we respond token in both cookie and JSON result(then it can be sent from client in HTTP Authorization Header).
                // We will show different use cases on different Controllers. You can just use either one of them.

                // Stores token in cookie.
                Response.Cookies.Append(_configuration["Cookies:AccessTokenName"], jwt, new CookieOptions
                {
                    HttpOnly = true,
                    //Secure = true, // Enable this in production to ensure cookie only send over HTTPS.
                    Path = "/",
                    Expires = DateTimeOffset.Now.AddMinutes(_jwtOptions.ExpireInMinutes),
                });

                // Returns token as JSON.
                return Ok(new
                {
                    token = jwt,
                    expiration = jwtSecurityToken.ValidTo
                });
            }
            catch (Exception ex)
            {
                Logger.Error($"error while creating token: {ex}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "error while creating token");
            }
        }

        [JwtAuthorize] // Authentication required since we don't validate token during renew.
        [HttpPost]
        [Route("renew")]
        public IActionResult RenewToken()
        {
            try
            {
                // Gets token from HTTP Authorization header.
                var protectedText = Request.Headers.First(h => h.Key == "Authorization").Value.First().Split(" ")[1];
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(protectedText);

                // Create a new token based on old token and extend the expiration.
                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
                var newToken = new JwtSecurityToken(
                    issuer: _jwtOptions.Issuer,
                    audience: _jwtOptions.Audience,
                    claims: token.Claims,
                    expires: DateTime.Now.AddMinutes(_jwtOptions.ExpireInMinutes),
                    signingCredentials: signingCredentials
                );

                var jwt = new JwtSecurityTokenHandler().WriteToken(newToken);

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
    }
}
