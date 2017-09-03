using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Todo.Core;

namespace Todo.EntityFrameworkCore
{
    /// <summary>
    /// The DB context object for the application.
    /// </summary>
    public class TodoDbContext : DbContext, IUnitOfWork
    {
        public virtual string UserId { get; }

        public virtual DbSet<TodoItem> TodoItems { get; set; }

        public virtual DbSet<TodoCategory> TodoCategories { get; set; }

        #region Constructors

        public TodoDbContext(DbContextOptions<TodoDbContext> options, IUserIdProvider userIdProvider)
            : base(options)
        {
            UserId = userIdProvider.GetUserId();
        }

        #endregion

        public int Commit()
        {
            return SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await SaveChangesAsync();
        }

        public void Rollback()
        {
            // Sets all entities in change tracker as 'unchanged state'.
            ChangeTracker.Entries()
                .ToList()
                .ForEach(entry => entry.State = EntityState.Unchanged);
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Global filter to restricting item reference for only the current user.
            modelBuilder.Entity<TodoItem>().HasQueryFilter(item => item.UserId == UserId);
        }
    }
}
