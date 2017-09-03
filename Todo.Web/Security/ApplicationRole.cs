using AspNet.Identity.MongoDb;
using Todo.Common.Security;

namespace Todo.Web.Security
{
    /// <summary>
    /// Identity implementation of IRole.
    /// </summary>
    public class ApplicationRole : IdentityRole, IRole
    {
        /// <inheritdoc />
        public string Description { get; set; }
    }
}
