using AspNet.Identity.MongoDb;
using Todo.Common.Security;

namespace Todo.Web.Security
{
    /// <summary>
    /// Identity implementation of IUser.
    /// </summary>
    public class ApplicationUser : IdentityUser, IUser
    {
    }
}
