using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Todo.Web.Security
{
    /// <inheritdoc />
    /// <summary>
    /// Holds custom validation logics for JWT.
    /// </summary>
    public class JwtValidator : JwtSecurityTokenHandler
    {
        private readonly string _algorithm;
        private readonly InvalidTokenDictionary _invalidTokenDictionary;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of <see cref="JwtValidator"/>.
        /// </summary>
        /// <param name="algorithm">Signature signing algorithm name.</param>
        /// <param name="invalidTokenDictionary">Invalid token dictionary.</param>
        public JwtValidator(string algorithm, InvalidTokenDictionary invalidTokenDictionary)
        {
            _algorithm = algorithm;
            _invalidTokenDictionary = invalidTokenDictionary;
        }

        /// <inheritdoc />
        public override ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            // Base method will do the most dirty work such as decode and signature check.
            var principal = base.ValidateToken(securityToken, validationParameters, out validatedToken);

            // Verify signing algorithm name.
            if (!(validatedToken is JwtSecurityToken jwt && jwt.SignatureAlgorithm == _algorithm))
            {
                throw new SecurityTokenValidationException("invalid algorithm");
            }

            // Checks if the token has been revoked.
            if (_invalidTokenDictionary.ContainsKey(validatedToken.Id))
            {
                throw new SecurityTokenValidationException("invalid token");
            }

            return principal;
        }

        public JwtSecurityToken RenewToken(string token, JwtOptions jwtOptions)
        {
            var jwt = ReadJwtToken(token);

            // Create a new token based on old token and extend the expiration.
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, jwtOptions.SigningAlgorithm);
            var newJwt = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: jwt.Claims,
                expires: DateTime.Now.AddMinutes(jwtOptions.ExpireInMinutes),
                signingCredentials: signingCredentials
            );

            return newJwt;
        }

        /// <summary>
        /// Revokes a token.
        /// </summary>
        /// <param name="token">The token string.</param>
        public void RevokeToken(string token)
        {
            var jwt = ReadJwtToken(token);

            _invalidTokenDictionary[jwt.Id] = jwt.ValidTo.ToLocalTime();
        }
    }
}
