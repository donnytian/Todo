using System.ComponentModel.DataAnnotations;
using Todo.Common.Configurations;

namespace Todo.Application.Dto
{
    /// <summary>
    /// Represents the base class for all entity data transfer objects.
    /// </summary>
    public abstract class DtoBase
    {
        [StringLength(Config.EntityIdMaxLength)]
        public virtual string Id { get; set; }
    }
}
