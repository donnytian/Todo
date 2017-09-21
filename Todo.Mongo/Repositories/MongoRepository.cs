using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using Todo.Core;

namespace Todo.Mongo.Repositories
{
    /// <inheritdoc />
    /// <summary>
    /// Implements IRepository for MongoDB. Inherit this class to do further customization for certain entity.
    /// </summary>
    /// <typeparam name="TEntity">Type of the Entity for this repository</typeparam>
    /// <typeparam name="TMongoUow">UoW which contains <typeparamref name="TEntity" />.</typeparam>

    public class MongoRepository<TEntity, TMongoUow> : IRepository<TEntity, TMongoUow>
        where TEntity : class, IEntity
        where TMongoUow : MongoDbUow
    {
        #region Non-Public Members

        protected readonly IMongoCollection<TEntity> Collection;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance for the type of <see cref="MongoRepository{TEntity,TMongoUow}"/>.
        /// </summary>
        /// <param name="uow">The UoW object.</param>
        public MongoRepository(TMongoUow uow)
        {
            UnitOfWork = uow ?? throw new ArgumentNullException(nameof(uow));
            Collection = uow.GetCollection<TEntity>();
        }

        #endregion

        #region IRepository

        /// <inheritdoc />
        public virtual TMongoUow UnitOfWork { get; }

        /// <inheritdoc />
        public virtual IQueryable<TEntity> GetAll()
        {
            return Collection.AsQueryable();
        }

        /// <inheritdoc />
        public virtual Task<List<TEntity>> GetAllAsync()
        {
            return Collection.Find(Predicate(null)).ToListAsync();
        }

        /// <inheritdoc />
        public virtual Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Collection.Find(Predicate(predicate)).ToListAsync();
        }

        /// <inheritdoc />
        public virtual TEntity Get(string id)
        {
            return Collection.Find(Predicate(t => t.Id.Equals(id))).SingleOrDefault();
        }

        /// <inheritdoc />
        public virtual Task<TEntity> GetAsync(string id)
        {
            return Collection.Find(Predicate(t => t.Id.Equals(id))).SingleOrDefaultAsync();
        }

        /// <inheritdoc />
        public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return Collection.Find(Predicate(predicate)).SingleOrDefault();
        }

        /// <inheritdoc />
        public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Collection.Find(Predicate(predicate)).SingleOrDefaultAsync();
        }

        /// <inheritdoc />
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Collection.Find(Predicate(predicate)).FirstOrDefault();
        }

        /// <inheritdoc />
        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Collection.Find(Predicate(predicate)).FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public virtual TEntity Insert(TEntity entity)
        {
            if (entity.IsTransient())
            {
                entity.GenerateIdentity();
            }

            Collection.InsertOne(entity);
            return entity;
        }

        /// <inheritdoc />
        public virtual TEntity InsertOrUpdate(TEntity entity)
        {
            return entity.IsTransient() ? Insert(entity) : Update(entity);
        }

        /// <inheritdoc />
        public virtual TEntity Update(TEntity entity)
        {
            Collection.FindOneAndReplace(Predicate(e => e.Id.Equals(entity.Id)), entity);

            // Note: Collection.FindOneAndReplace will return the old entity, we need return new entity here.
            return entity;
        }

        /// <inheritdoc />
        public virtual void Delete(TEntity entity)
        {
            Collection.DeleteOne(Predicate(e => e.Id.Equals(entity.Id)));
        }

        /// <inheritdoc />
        public virtual void Delete(string id)
        {
            var result = Collection.DeleteOne(Predicate(e => e.Id.Equals(id)));

            if (result.DeletedCount == 0)
            {
                throw new ArgumentException("ID not found, no item was deleted.");
            }
        }

        /// <inheritdoc />
        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            Collection.DeleteMany(Predicate(predicate));
        }

        #endregion

        #region Non-Public Methods

        /// <summary>
        /// Creates a new predicate based on input function. Used to apply global filters.
        /// </summary>
        /// <param name="predicate">The predicate function to be wrapped.</param>
        /// <returns>The new predicate function.</returns>
        protected virtual Expression<Func<TEntity, bool>> Predicate(Expression<Func<TEntity, bool>> predicate)
        {
            return predicate;
        }

        #endregion
    }
}
