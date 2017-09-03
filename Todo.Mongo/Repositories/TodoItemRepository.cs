using System;
using System.Linq.Expressions;
using Todo.Common.Extensions;
using Todo.Core;

namespace Todo.Mongo.Repositories
{
    /// <inheritdoc />
    /// <summary>
    /// To-do item repository.
    /// </summary>
    public class TodoItemRepository : MongoRepository<TodoItem, MongoDbUow>
    {
        #region Non-Public Members

        protected readonly string UserId;

        #endregion

        #region Constructors

        /// <inheritdoc />
        public TodoItemRepository(MongoDbUow uow)
            :base(uow)
        {
            UserId = uow.UserId;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public override TodoItem Insert(TodoItem entity)
        {
            entity.UserId = UserId;

            return base.Insert(entity);
        }

        /// <inheritdoc />
        public override TodoItem InsertOrUpdate(TodoItem entity)
        {
            entity.UserId = UserId;

            return base.InsertOrUpdate(entity);
        }

        /// <inheritdoc />
        public override TodoItem Update(TodoItem entity)
        {
            entity.UserId = UserId;

            return base.Update(entity);
        }

        #endregion

        #region Non-Public Methods

        /// <inheritdoc />
        /// <summary>
        /// Adds an user ID filter for any predicates.
        /// </summary>
        protected override Expression<Func<TodoItem, bool>> Predicate(Expression<Func<TodoItem, bool>> predicate)
        {
            if (UserId == null) return predicate;

            Expression<Func<TodoItem, bool>> userFilter = item => item.UserId == UserId;

            return userFilter.AndAlso(predicate);
        }

        #endregion
    }
}
