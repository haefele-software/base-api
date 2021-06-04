using ProjectName.Application.Domain.Common;
using ProjectName.Application.Domain.Entities;

namespace ProjectName.Application.Domain.Events.TodoItems
{
    public class TodoItemCompletedEvent : DomainEvent
    {
        public TodoItemCompletedEvent(TodoItem item)
        {
            Item = item;
        }

        public TodoItem Item { get; }
    }
}
