using Microsoft.IdentityModel.Tokens;

namespace Todo.Web.Security
{
    /// <summary>
    /// Represents options when issue or validate a JWT.
    /// </summary>
    public class JwtOptions
    {
        /// <summary>
        /// Signing key.
        /// </summary>
        public string SigningKey { get; set; }

        /// <summary>
        /// Signing algorithm.
        /// </summary>
        public string SigningAlgorithm { get; set; } = SecurityAlgorithms.HmacSha256;

        /// <summary>
        /// Token issuer.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Token audience.
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// How many minutes the token will be expired after it has been issued.
        /// </summary>
        public int ExpireInMinutes { get; set; }
        
        /// <summary>
        /// True to add token in cookie for generation and retrieve token from there for authentication and authorization,
        /// otherwise use HTTP Authorization Header.
        /// </summary>
        public bool UseCookie { get; set; }

        /// <summary>
        /// Cookie name for the token. Enabled only if <see cref="UseCookie"/> is <c>true</c>.
        /// </summary>
        public string CookieName { get; set; }
    }
}
