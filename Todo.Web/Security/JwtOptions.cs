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
        public string Key { get; set; }

        /// <summary>
        /// Token issuer.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Token audience.
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// How many minutes the toen will be expired after it's issued.
        /// </summary>
        public int ExpireInMinutes { get; set; }
    }
}
