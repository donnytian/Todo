namespace Todo.Common.Security
{
    /// <summary>
    /// Represents a user role in application.
    /// </summary>
    public interface IRole
    {
        /// <summary>Gets or sets the primary key for this role.</summary>
        string Id { get; set; }

        /// <summary>Gets or sets the name for this role.</summary>
        string Name { get; set; }

        /// <summary>Gets or sets the name for this role.</summary>
        string Description { get; set; }
    }
}
