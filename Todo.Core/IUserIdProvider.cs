using System;

namespace Todo.Core
{
    /// <summary>
    /// Provides user ID. Used in a session or user based context such as queries for entities that only belongs to the current user.
    /// </summary>
    public interface IUserIdProvider
    {
        /// <summary>
        /// Get the user ID.
        /// </summary>
        /// <returns>User ID.</returns>
        string GetUserId();
    }
}
