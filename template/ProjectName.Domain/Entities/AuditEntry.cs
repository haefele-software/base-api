using Destructurama.Attributed;
using ProjectName.Application.Domain.Common;
using ProjectName.Application.Domain.Enums;
using ProjectName.Application.Domain.ValueObjects;
using ProjectName.Common.Security;

namespace ProjectName.Application.Domain.Entities
{
    public class AuditEntry : Entity
    {
        public AuditEvent AuditEvent { get; set; }

        public EntityType AuditedEntityType { get; set; }

        public EntityId AuditedEntityId { get; set; }

        public CreatedDate AuditDate { get; set; }

        [LogMasked(Text = "***")]
        public Name InitiatedBy { get; set; }

        [LogMasked(Text = "***")]
        [Sensitive]
        public SerializedEntityState ObjectPreviousState { get; set; }

        [LogMasked(Text = "***")]
        [Sensitive]
        public SerializedEntityState ObjectNewState { get; set; }
    }
}
