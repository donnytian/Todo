using System;

namespace Todo.Common.Security
{
    /// <summary>
    /// Represents an application user.
    /// The original shape is from Identity framework. But of course we can switch to other frameworks or customizations.
    /// </summary>
    public interface IUser
    {
        /// <summary>Gets or sets the primary key for this user.</summary>
        string Id { get; set; }

        /// <summary>Gets or sets the user name for this user.</summary>
        string UserName { get; set; }

        /// <summary>Gets or sets the email address for this user.</summary>
        string Email { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating if a user has confirmed their email address.
        /// </summary>
        /// <value>True if the email address has been confirmed, otherwise false.</value>
        bool EmailConfirmed { get; set; }

        /// <summary>Gets or sets a telephone number for the user.</summary>
        string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating if a user has confirmed their telephone address.
        /// </summary>
        /// <value>True if the telephone number has been confirmed, otherwise false.</value>
        bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// Gets or sets the date and time, in UTC, when any user lockout ends.
        /// </summary>
        /// <remarks>A value in the past means the user is not locked out.</remarks>
        DateTimeOffset? LockoutEnd { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating if the user could be locked out.
        /// </summary>
        /// <value>True if the user could be locked out, otherwise false.</value>
        bool LockoutEnabled { get; set; }

        /// <summary>
        /// Gets or sets the number of failed login attempts for the current user.
        /// </summary>
        int AccessFailedCount { get; set; }
    }
}
