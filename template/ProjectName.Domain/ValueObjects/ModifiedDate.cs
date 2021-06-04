using ProjectName.Application.Domain.Common;
using ProjectName.Application.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace ProjectName.Application.Domain.ValueObjects
{
    public sealed class ModifiedDate : ValueObject
    {
        private ModifiedDate() { }

        public ModifiedDate(DateTimeOffset? modifiedDate = null)
        {
            if (modifiedDate != null && modifiedDate >= DateTimeOffset.UtcNow)
            {
                throw new DomainValidationException("Invalid value. Modified date cannot be in the future.", nameof(modifiedDate));
            }

            if (modifiedDate != null && modifiedDate <= DateTimeOffset.MinValue)
            {
                throw new DomainValidationException("Invalid value.", nameof(modifiedDate));
            }

            if (modifiedDate != null && modifiedDate > DateTimeOffset.MaxValue)
            {
                throw new DomainValidationException("Invalid value.", nameof(modifiedDate));
            }

            this.Value = modifiedDate;
        }

        public static implicit operator ModifiedDate(DateTimeOffset? value) => new ModifiedDate(value);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public DateTimeOffset? Value { get; }

        public static bool operator <(ModifiedDate left, ModifiedDate right)
        {
            return left.Value < right.Value;
        }

        public static bool operator >(ModifiedDate left, ModifiedDate right)
        {
            return left.Value > right.Value;
        }

        public static bool operator <=(ModifiedDate left, ModifiedDate right)
        {
            return (left < right || left == right);
        }

        public static bool operator >=(ModifiedDate left, ModifiedDate right)
        {
            return (left > right || left == right);
        }


    }
}
