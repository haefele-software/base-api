using Destructurama.Attributed;
using ProjectName.Application.Domain.Common;
using ProjectName.Application.Domain.ValueObjects;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ProjectName.Application.Domain.Entities
{
    public class TodoList : AuditableEntity
    {
        [LogMasked(Text = "***")]
        public Title Title { get; set; }

        [JsonIgnore]
        public virtual ICollection<TodoItem> Items { get; private set; } = new HashSet<TodoItem>();
    }
}
