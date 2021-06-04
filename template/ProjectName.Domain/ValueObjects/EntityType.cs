using System;
using System.Collections.Generic;
using ProjectName.Application.Domain.Common;
using ProjectName.Application.Domain.Exceptions;

namespace ProjectName.Application.Domain.ValueObjects
{
    public class EntityType : ValueObject
    {
        private EntityType() { }

        public EntityType(Type entityType)
        {
            if (entityType is null)
            {
                throw new DomainValidationException("Invalid value. Entity Type cannot be null.", nameof(entityType));
            }

            this.Value = entityType.Name;
        }

        public static implicit operator EntityType(Type value) => new EntityType(value);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public string Value { get; }
    }
}
