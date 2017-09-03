using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using Todo.Core;

namespace Todo.Mongo
{
    /// <inheritdoc />
    /// <summary>
    /// Represents an UoW for MongoDB.
    /// </summary>
    public class MongoDbUow : IUnitOfWork
    {
        #region Constructors

        /// <summary>Creates a new instance of the UoW.</summary>
        /// <param name="connectionUri">The URI string used to connect the MongoDB.</param>
        /// <param name="userIdProvider">The provider for user ID used to filter user specific objects.</param>
        public MongoDbUow(string connectionUri, IUserIdProvider userIdProvider)
        {
            if (string.IsNullOrWhiteSpace(connectionUri)) throw new ArgumentNullException(nameof(connectionUri));

            var mongoUrl = new MongoUrl(connectionUri);

            if (string.IsNullOrWhiteSpace(mongoUrl.DatabaseName)) throw new ArgumentNullException("Missing database name in connection string");

            _database = new MongoClient(mongoUrl).GetDatabase(mongoUrl.DatabaseName);
            UserId = userIdProvider?.GetUserId();
        }

        #endregion

        #region Private Members

        private readonly IMongoDatabase _database;

        #endregion

        #region Public Properties

        public virtual string UserId { get; }


        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the MongoDB collection for the entity.
        /// </summary>
        /// <typeparam name="TEntity">The type for entity.</typeparam>
        /// <param name="collectionName">The collection name.</param>
        /// <returns>MongoDB collection for the entity.</returns>
        public IMongoCollection<TEntity> GetCollection<TEntity>(string collectionName = null)
        {
            return _database.GetCollection<TEntity>(collectionName ?? typeof(TEntity).Name);
        }

        public Task<int> CommitAsync()
        {
            throw new NotImplementedException();
        }

        public int Commit()
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
