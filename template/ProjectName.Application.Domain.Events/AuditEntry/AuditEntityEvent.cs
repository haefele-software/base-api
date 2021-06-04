using ProjectName.Application.Domain.Common;
using ProjectName.Application.Domain.Enums;

namespace ProjectName.Application.Domain.Events.AuditEntry
{
    public class AuditEntityEvent : DomainEvent
    {
        public AuditEntityEvent(Entity entityNew, Entity entityOld, AuditEvent auditEvent)
        {
            EntityOld = entityOld;
            EntityNew = entityNew;
            Event = auditEvent;
        }

        public Entity EntityOld { get; }
        public Entity EntityNew { get; }
        public AuditEvent Event { get; }
    }
}
