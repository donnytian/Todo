using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Application.Dto;

namespace Todo.Application
{
    /// <summary>
    /// Privodes services to display and manage to-do items.
    /// </summary>
    public interface ITodoAppService
    {
        /// <summary>
        /// Creates a to-do item.
        /// </summary>
        /// <param name="item">The item to be created.</param>
        /// <returns>The created item.</returns>
        Task<TodoItemDto> CreateItemAsync(TodoItemDto item);

        /// <summary>
        /// Updates a to-do item.
        /// </summary>
        /// <param name="item">The item to be updated.</param>
        /// <returns>The updated item.</returns>
        Task<TodoItemDto> UpdateItemAsync(TodoItemDto item);

        /// <summary>
        /// Updates or inserts a to-do item.
        /// </summary>
        /// <param name="item">The item to be updated or created.</param>
        /// <returns>The updated or created item.</returns>
        Task<TodoItemDto> UpdateOrInsertItemAsync(TodoItemDto item);

        /// <summary>
        /// Deletes a to-do item.
        /// </summary>
        /// <param name="id">The ID for the item to be deleted.</param>
        /// <returns>The deleted item.</returns>
        Task DeleteItemAsync(string id);

        /// <summary>
        /// Deletes a to-do item.
        /// </summary>
        /// <returns>The result items.</returns>
        Task<List<TodoItemDto>> FindAllAsync();
    }
}
