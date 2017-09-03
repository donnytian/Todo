using System.Threading.Tasks;

namespace Todo.Core
{
    /// <summary>
    /// Base contract for 'UnitOfWork pattern'. For more related info see
    /// http://martinfowler.com/eaaCatalog/unitOfWork.html or
    /// http://msdn.microsoft.com/en-us/magazine/dd882510.aspx
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Commit all changes made in a container asynchronously.
        /// </summary>
        ///<remarks>
        /// If the entity have fixed properties and any optimistic concurrency problem exists,
        /// then an exception is thrown.
        ///</remarks>
        ///<returns>Number of records affected.</returns>
        Task<int> CommitAsync();

        /// <summary>
        /// Commit all changes made in a container.
        /// </summary>
        ///<remarks>
        /// If the entity have fixed properties and any optimistic concurrency problem exists,
        /// then an exception is thrown.
        ///</remarks>
        ///<returns>Number of records affected.</returns>
        int Commit();

        /// <summary>
        /// Rollback tracked changes. See references of UnitOfWork pattern.
        /// </summary>
        void Rollback();
    }
}
