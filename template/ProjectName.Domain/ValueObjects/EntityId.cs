using ProjectName.Application.Domain.Common;
using ProjectName.Application.Domain.Exceptions;
using System.Collections.Generic;

namespace ProjectName.Application.Domain.ValueObjects
{
    public sealed class EntityId : ValueObject
    {
        private EntityId() { }

        public EntityId(long? entityId = null)
        {
            if (entityId != null && entityId <= 0)
            {
                throw new DomainValidationException("Invalid value. Entity Id cannot be less than 1.", nameof(entityId));
            }

            this.Value = entityId;
        }

        public static implicit operator EntityId(long? value) => new EntityId(value);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public long? Value { get; }
    }
}
