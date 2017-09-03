using System;
using System.ComponentModel.DataAnnotations;
using Todo.Common.Configurations;
using Todo.Core;

namespace Todo.Application.Dto
{
    public class TodoItemDto : DtoBase
    {
        public string UserId { get; set; }

        [StringLength(Config.TodoItemMaxLength)]
        public string Content { get; set; }

        public bool Completed { get; set; }

        public int Priority { get; set; }

        public TodoCategory Category { get; set; }

        public DateTime? DueDateUtc { get; set; }

        public DateTime? CompletionUtc { get; set; }
    }
}
