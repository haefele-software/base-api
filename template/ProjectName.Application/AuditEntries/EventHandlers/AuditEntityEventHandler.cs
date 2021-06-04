using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProjectName.Application.Contracts.Interfaces;
using ProjectName.Application.Domain.Entities;
using ProjectName.Application.Domain.Events.AuditEntry;
using ProjectName.Application.Models.EventNotifications;

namespace ProjectName.Application.AuditEntries.EventHandlers
{
    public class AuditEntityEventHandler : INotificationHandler<DomainEventNotification<AuditEntityEvent>>
    {
        private readonly ILogger<AuditEntityEventHandler> _logger;
        private readonly IApplicationDbContext _applicationContext;
        private readonly ICurrentUserService _currentUserService;

        public AuditEntityEventHandler(ILogger<AuditEntityEventHandler> logger, IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _applicationContext = context;
            _currentUserService = currentUserService;
        }

        public async Task Handle(DomainEventNotification<AuditEntityEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            _applicationContext.Audits.Add(new AuditEntry()
            {
                AuditedEntityId = domainEvent.EntityNew.Id,
                AuditedEntityType = domainEvent.EntityNew.GetType(),
                InitiatedBy = _currentUserService.UserId,
                AuditDate = DateTimeOffset.UtcNow,
                AuditEvent = domainEvent.Event,
                ObjectPreviousState = JsonConvert.SerializeObject(domainEvent.EntityOld, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }),
                ObjectNewState = JsonConvert.SerializeObject(domainEvent.EntityNew, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                })
            });

            await _applicationContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
