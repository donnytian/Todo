using Microsoft.AspNetCore.Identity;
using Todo.Common.Security;

namespace Todo.EntityFrameworkCore.Identity
{
    /// <summary>
    /// Identity implementation of IUser.
    /// </summary>
    public class ApplicationUser : IdentityUser, IUser
    {
    }
}
