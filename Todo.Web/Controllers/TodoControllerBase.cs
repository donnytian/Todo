using Microsoft.AspNetCore.Mvc;

using Todo.Common.Logging;

namespace Todo.Web.Controllers
{
    /// <summary>
    /// Derives all Controllers from this class.
    /// </summary>
    public abstract class TodoControllerBase : Controller
    {
        /// <summary>Gets the logger to write logs.</summary>
        protected ILogger Logger { get; set; }

        /// <summary>Constructor.</summary>
        /// <param name="loggerFactory">The logger factory object.</param>
        protected TodoControllerBase(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.Create();
        }
    }
}
