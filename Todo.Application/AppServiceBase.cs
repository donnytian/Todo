using Todo.Common.Logging;
using Todo.Core;

namespace Todo.Application
{
    /// <summary>
    /// This class can be used as a base class for applications services.
    /// It has some useful objects property-injected and has some basic methods
    /// that most of services may need to.
    /// </summary>
    public abstract class AppServiceBase
    {
        /// <summary>Gets current unit of work.</summary>
        protected IUnitOfWork UnitOfWork { get; set; }

        /// <summary>Gets the logger to write logs.</summary>
        protected ILogger Logger { get; set; }

        /// <summary>Constructor.</summary>
        /// <param name="loggerFactory">The logger factory object.</param>
        /// <param name="unitOfWork">The current Unit of Work object.</param>
        protected AppServiceBase(ILoggerFactory loggerFactory, IUnitOfWork unitOfWork)
        {
            Logger = loggerFactory.Create();
            UnitOfWork = unitOfWork;
        }
    }
}
