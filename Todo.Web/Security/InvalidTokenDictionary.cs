using System;
using System.Collections.Generic;

namespace Todo.Web.Security
{
    /// <summary>
    /// Stores token IDs that have been revoked.
    /// </summary>
    public class InvalidTokenDictionary : Dictionary<string, DateTime>
    {
    }
}
