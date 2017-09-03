using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Todo.Web.Security
{
    /// <summary>
    /// Authorizes controllers and actions with Jwt Bearer token schema.
    /// This is a shortcut for [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class JwtAuthorizeAttribute : AuthorizeAttribute
    {
        /// <inheritdoc />
        public JwtAuthorizeAttribute()
        {
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }

        /// <inheritdoc />
        public JwtAuthorizeAttribute(string policy) : base(policy)
        {
        }
    }
}
