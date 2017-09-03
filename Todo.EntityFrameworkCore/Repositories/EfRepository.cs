using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Todo.Core;

namespace Todo.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Implements IRepository for Entity Framework. Inherit this class to do further customization for certain entity.
    /// </summary>
    /// <typeparam name="TDbContext">DbContext which contains <typeparamref name="TEntity" />.</typeparam>
    /// <typeparam name="TEntity">Type of the Entity for this repository</typeparam>
    public class EfRepository<TEntity, TDbContext> : IRepository<TEntity, TDbContext> where TDbContext : DbContext, IUnitOfWork where TEntity : class, IEntity
    {
        #region Properties

        /// <summary>Gets EF DbContext object.</summary>
        public virtual TDbContext UnitOfWork { get; }

        /// <summary>Gets DbSet for given entity.</summary>
        public virtual DbSet<TEntity> Table => UnitOfWork.Set<TEntity>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance for the type of <see cref="EfRepository{TDbContext,TEntity}"/>.
        /// </summary>
        /// <param name="dbContext">The dbContext object.</param>
        public EfRepository(TDbContext dbContext)
        {
            UnitOfWork = dbContext;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public virtual IQueryable<TEntity> GetAll()
        {
            return Table;
        }

        /// <inheritdoc />
        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await GetAll().ToListAsync();
        }

        /// <inheritdoc />
        public virtual async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).ToListAsync();
        }

        /// <inheritdoc />
        public virtual TEntity Get(string id)
        {
            return FirstOrDefault(entity => entity.Id == id);
        }

        /// <inheritdoc />
        public virtual Task<TEntity> GetAsync(string id)
        {
            return FirstOrDefaultAsync(entity => entity.Id == id);
        }

        /// <inheritdoc />
        public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Single(predicate);
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().SingleAsync(predicate);
        }

        /// <inheritdoc />
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().FirstOrDefaultAsync(predicate);
        }

        /// <inheritdoc />
        public virtual TEntity Insert(TEntity entity)
        {
            if (entity.IsTransient())
            {
                entity.GenerateIdentity();
            }

            return Table.Add(entity).Entity;
        }

        /// <inheritdoc />
        public virtual TEntity InsertOrUpdate(TEntity entity)
        {
            return entity.IsTransient() ? Insert(entity) : Update(entity);
        }

        public virtual TEntity Update(TEntity entity)
        {
            UnitOfWork.Entry(entity).State = EntityState.Modified;

            return entity;
        }

        public virtual void Delete(TEntity entity)
        {
            Table.Remove(entity);
        }

        public virtual void Delete(string id)
        {
            var entity = Table.Local.FirstOrDefault(ent => ent.Id == id);

            if (entity == null)
            {
                throw new InvalidOperationException($"ID [{id}] not found for the deletion!");
            }

            Delete(entity);
        }

        /// <inheritdoc />
        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            foreach (var entity in GetAll().Where(predicate).ToList())
            {
                Delete(entity);
            }
        }

        #endregion
    }
}
