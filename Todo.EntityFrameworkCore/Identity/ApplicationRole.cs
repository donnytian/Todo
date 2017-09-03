using Microsoft.AspNetCore.Identity;
using Todo.Common.Security;

namespace Todo.EntityFrameworkCore.Identity
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
