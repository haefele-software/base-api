using ProjectName.Application.Domain.Common;
using ProjectName.Application.Domain.Entities;

namespace ProjectName.Application.Domain.Events.TodoItems
{
    public class TodoItemUpdatedEvent : DomainEvent
    {
        public TodoItemUpdatedEvent(TodoItem item)
        {
            Item = item;
        }

        public TodoItem Item { get; }
    }
}
