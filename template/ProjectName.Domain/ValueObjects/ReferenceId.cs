using ProjectName.Application.Domain.Common;
using ProjectName.Application.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace ProjectName.Application.Domain.ValueObjects
{
    public sealed class ReferenceId : ValueObject
    {
        private ReferenceId() { }

        public ReferenceId(Guid? referenceId = null)
        {
            if (referenceId != null && referenceId == Guid.Empty)
            {
                throw new DomainValidationException("Invalid value. ReferenceId cannot be an empty Guid.", nameof(referenceId));
            }

            this.Value = referenceId ?? Guid.NewGuid();
        }

        public static implicit operator ReferenceId(Guid? value) => new ReferenceId(value);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public Guid Value { get; }
    }
}
