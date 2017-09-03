using System.Configuration;

namespace Todo.Common.Configurations
{
    /// <summary>
    /// Use this class to hold application config.
    /// </summary>
    public static class Config
    {
        #region Constants

        public const int EntityIdMaxLength = 256;

        public const int TodoItemMaxLength = 256;

        public const int TodoCategoryNameMaxLength = 32;

        public const int TodoCategoryDescriptionMaxLength = 256;

        public const int TodoCategoryColorMaxLength = 128;

        public const int TodoCategoryIconMaxLength = 128;

        public const int TokenMaxLength = 256;

        public const int UserNameMaxLength = 256;

        public const int PasswordMaxLength = 128;

        public const string SessionUserNameCookieName = "SessionUserName";

        public const string SessionTokenCookieName = "SessionToken";

        #endregion

        #region Settings

        public const int DefaultTokenTimeOut = 60; // Defaults to one hour.

        /// <summary>
        /// Token time out in minutes.
        /// </summary>
        public static int TokenTimeOutInMinutes
        {
            get
            {
                var value = ConfigurationManager.AppSettings[nameof(TokenTimeOutInMinutes)];
                int result;

                if (!int.TryParse(value, out result))
                {
                    result = DefaultTokenTimeOut;
                }

                return result;
            }
        }

        public const int DefaultMaxInvalidAttempts = 3;
        /// <summary>
        /// Gets the maximum number of invalid attempts before lockout.
        /// </summary>
        public static int MaxInvalidAttempts
        {
            get
            {
                var value = ConfigurationManager.AppSettings[nameof(MaxInvalidAttempts)];
                int result;

                if (!int.TryParse(value, out result))
                {
                    result = DefaultMaxInvalidAttempts;
                }

                return result;
            }
        }

        public const int DefaultLockOutMinutes = 15;
        /// <summary>
        /// Gets the duration in minutes for lock out.
        /// </summary>
        public static int LockOutDurationInMinutes
        {
            get
            {
                var value = ConfigurationManager.AppSettings[nameof(LockOutDurationInMinutes)];
                int result;

                if (!int.TryParse(value, out result))
                {
                    result = DefaultLockOutMinutes;
                }

                return result;
            }
        }

        #endregion
    }
}
