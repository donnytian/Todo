using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Todo.Core;

namespace Todo.Web.Security
{
    /// <inheritdoc />
    /// <summary>
    /// Get user ID from incoming request.
    /// </summary>
    public class AspNetUserIdProvider : IUserIdProvider
    {
        public string GetUserId()
        {
            var accessor = new HttpContextAccessor();
            var user = accessor.HttpContext?.User;
            var idClaim = user?.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);

            return idClaim?.Value;
        }
    }
}
