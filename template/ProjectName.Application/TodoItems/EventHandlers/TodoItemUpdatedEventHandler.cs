using MediatR;
using Microsoft.Extensions.Logging;
using ProjectName.Application.Domain.Events.TodoItems;
using ProjectName.Application.Models.EventNotifications;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectName.Application.TodoItems.EventHandlers
{
    public class TodoItemUpdatedEventHandler : INotificationHandler<DomainEventNotification<TodoItemUpdatedEvent>>
    {
        private readonly ILogger<TodoItemUpdatedEventHandler> _logger;

        public TodoItemUpdatedEventHandler(ILogger<TodoItemUpdatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<TodoItemUpdatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
