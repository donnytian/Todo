using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Application.Dto;
using Todo.Common.Adapter;
using Todo.Core;

namespace Todo.Application
{
    public class TodoAppService : ITodoAppService
    {
        #region Non-Public Members

        protected readonly IRepository<TodoItem, IUnitOfWork> Repository;

        #endregion

        #region Constructors

        /// <inheritdoc />
        public TodoAppService(IRepository<TodoItem, IUnitOfWork> repository)
        {
            Repository = repository;
        }

        #endregion

        #region Non-Public Methods


        #endregion

        public Task<TodoItemDto> CreateItemAsync(TodoItemDto item)
        {
            var dto = Repository.Insert(item.AdaptAs<TodoItem>()).AdaptAs<TodoItemDto>();

            return Task.FromResult(dto);
        }

        public Task<TodoItemDto> UpdateItemAsync(TodoItemDto item)
        {
            var dto = Repository.Update(item.AdaptAs<TodoItem>()).AdaptAs<TodoItemDto>();

            return Task.FromResult(dto);
        }

        public Task<TodoItemDto> UpdateOrInsertItemAsync(TodoItemDto item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            var dto = Repository.InsertOrUpdate(item.AdaptAs<TodoItem>()).AdaptAs<TodoItemDto>();

            return Task.FromResult(dto);
        }

        public Task DeleteItemAsync(string id)
        {
            Repository.Delete(id);

            return Task.FromResult(0);
        }

        public async Task<List<TodoItemDto>> FindAllAsync()
        {
            var list = await Repository.GetAllAsync();
            list.Reverse();

            return list.AdaptAsList<TodoItem, TodoItemDto>();
        }
    }
}
