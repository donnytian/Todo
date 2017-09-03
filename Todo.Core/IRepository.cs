using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Todo.Core
{
    /// <summary>
    /// This interface is implemented by all repositories to ensure implementation of fixed methods.
    /// </summary>
    /// <typeparam name="TEntity">Main Entity type this repository works on.</typeparam>
    /// <typeparam name="TUnitOfWork">UnitOfWork type this repository works on.</typeparam>
    public interface IRepository<TEntity, out TUnitOfWork>  where TEntity : class, IEntity where TUnitOfWork : class, IUnitOfWork
    {
        /// <summary>
        /// Gets the IUnitOfWork object to perform transactional operations.
        /// </summary>
        TUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Used to get a IQueryable that is used to retrieve entities from entire table.
        /// </summary>
        /// <returns>IQueryable to be used to select entities from database</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>Used to get all entities.</summary>
        /// <returns>List of all entities</returns>
        Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// Used to get all entities based on given <paramref name="predicate" />.
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        /// <returns>List of all entities</returns>
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>Gets an entity with given primary key.</summary>
        /// <param name="id">Primary key of the entity to get</param>
        /// <returns>Entity</returns>
        TEntity Get(string id);

        /// <summary>Gets an entity with given primary key.</summary>
        /// <param name="id">Primary key of the entity to get</param>
        /// <returns>Entity</returns>
        Task<TEntity> GetAsync(string id);

        /// <summary>
        /// Gets exactly one entity with given predicate.
        /// Throws exception if no entity or more than one entity.
        /// </summary>
        /// <param name="predicate">Entity</param>
        /// <returns>Entity if found; otherwise throws exception</returns>
        TEntity Single(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets exactly one entity with given predicate.
        /// Throws exception if no entity or more than one entity.
        /// </summary>
        /// <param name="predicate">Entity</param>
        /// <returns>Entity if found; otherwise throws exception</returns>
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets an entity with given predicate or null if not found.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities</param>
        /// <returns>The first matched entity if found; otherwise null</returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets an entity with given predicate or null if not found.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities</param>
        /// <returns>The first matched entity if found; otherwise null</returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>Inserts a new entity.</summary>
        /// <param name="entity">Inserted entity</param>
        /// <returns>The entity with identity.</returns>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// Inserts or updates given entity depending on Id's value.
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>The entity with identity.</returns>
        TEntity InsertOrUpdate(TEntity entity);

        /// <summary>Updates an existing entity.</summary>
        /// <param name="entity">Entity</param>
        /// <returns>The entity with identity.</returns>
        TEntity Update(TEntity entity);

        /// <summary>Deletes an entity.</summary>
        /// <param name="entity">Entity to be deleted</param>
        void Delete(TEntity entity);

        /// <summary>Deletes an entity by primary key.</summary>
        /// <param name="id">Primary key of the entity</param>
        void Delete(string id);

        /// <summary>
        /// Deletes many entities by function.
        /// Notice that: All entities fits to given predicate are retrieved and deleted.
        /// This may cause major performance problems if there are too many entities with
        /// given predicate.
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        void Delete(Expression<Func<TEntity, bool>> predicate);
    }
}
