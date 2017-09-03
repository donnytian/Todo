using System;

namespace Todo.Core
{
    /// <summary>
    /// This interface should be implemented by all domain entities.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// The unique identifier for this entity.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// UTC date time of entity creation.
        /// </summary>
        DateTime CreatedUtc { get; }

        /// <summary>
        /// The identifier for the user who has created this entity.
        /// </summary>
        string CreatedBy { get; }

        /// <summary>
        /// UTC date time of entity update.
        /// </summary>
        DateTime? LastUpdatedUtc { get; }

        /// <summary>
        /// The identifier for the user who has updated this entity last time.
        /// </summary>
        string LastUpdatedBy { get; }

        /// <summary>
        /// Checks if this entity is transient (not persisted to database and it has not an valid ID).
        /// </summary>
        /// <returns>True, if this entity is transient; otherwise false.</returns>
        bool IsTransient();

        /// <summary>
        /// Generates identity for this entity.
        /// </summary>
        void GenerateIdentity();
    }
}
