using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Todo.Web.Security
{
    /// <summary>
    /// We write this class since Asp.NET Core does not provide built-in support for token validation in cookie
    /// while it does for Bearer token in HTTP header though.
    /// For now we only decode token and validate. We encode and sign token in the login/ create token action.
    /// </summary>
    public class JwtSecureDataFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly TokenValidationParameters _validationParameters;
        private readonly JwtValidator _validator;

        public JwtSecureDataFormat(TokenValidationParameters validationParameters, JwtValidator validator)
        {
            _validationParameters = validationParameters;
            _validator = validator;
        }

        public AuthenticationTicket Unprotect(string protectedText) => Unprotect(protectedText, null);

        public AuthenticationTicket Unprotect(string protectedText, string purpose)
        {
            ClaimsPrincipal principal;

            try
            {
                principal = _validator.ValidateToken(protectedText, _validationParameters, out var validToken);
            }
            catch (SecurityTokenException)
            {
                return null;
            }
            catch (ArgumentException)
            {
                return null;
            }

            // Validation passed. Return a valid AuthenticationTicket.
            return new AuthenticationTicket(principal, new AuthenticationProperties(), JwtBearerDefaults.AuthenticationScheme);
        }

        // This ISecureDataFormat implementation is decode-only
        public string Protect(AuthenticationTicket data)
        {
            throw new NotImplementedException();
        }

        public string Protect(AuthenticationTicket data, string purpose)
        {
            throw new NotImplementedException();
        }
    }
}
