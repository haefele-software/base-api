using Destructurama.Attributed;
using ProjectName.Application.Domain.Common;
using ProjectName.Application.Domain.Enums;
using ProjectName.Application.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ProjectName.Application.Domain.Entities
{

    public class TodoItem : AuditableEntity, IHasDomainEvent
    {
        public TodoItem()
        {
            CreatedDate = DateTimeOffset.Now;
            ReminderDate = new ReminderDate(null);
            Note = new Note(null);
        }

        [JsonIgnore]
        public virtual TodoList TodoList { get; set; }

        public long TodoListId { get; set; }

        [LogMasked(Text = "***")]
        public Title Title { get; set; }

        [LogMasked(Text = "***")]
        public Note Note { get; set; }


        public PriorityLevel Priority { get; set; }

        public ReminderDate ReminderDate { get; set; }

        public CreatedDate CreatedDate { get; set; }

        [JsonIgnore]
        public bool Done { get; set; }

        [JsonIgnore]
        public IList<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
