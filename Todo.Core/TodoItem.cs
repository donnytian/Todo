using System;
using System.ComponentModel.DataAnnotations;

using Todo.Common.Configurations;

namespace Todo.Core
{
    /// <summary>
    /// Represents a to-do item for the purpose of tracking and reminding.
    /// </summary>
    public class TodoItem : Entity
    {
        /// <summary>
        /// The ID of user who own the to-do item.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The to-do item content.
        /// </summary>
        [StringLength(Config.TodoItemMaxLength)]
        public string Content { get; set; }

        /// <summary>
        /// Indicates whether or not the item is completed.
        /// </summary>
        public bool Completed { get; set; }

        /// <summary>
        /// The priority of the item. Priority 1 is highest.
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// The category that this item belongs to.
        /// </summary>
        public TodoCategory Category { get; set; }

        /// <summary>
        /// Due date in UTC.
        /// </summary>
        public DateTime? DueDateUtc { get; set; }

        /// <summary>
        /// Completion date time in UTC.
        /// </summary>
        public DateTime? CompletionUtc { get; set; }
    }
}
