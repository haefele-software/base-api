using ProjectName.Application.Domain.Common;
using ProjectName.Application.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace ProjectName.Application.Domain.ValueObjects
{
    public sealed class CreatedDate : ValueObject
    {
        private CreatedDate() { }

        public CreatedDate(DateTimeOffset? createdDate = null)
        {
            if (createdDate != null && createdDate > DateTimeOffset.UtcNow)
            {
                throw new DomainValidationException("Invalid value. Date cannot be set in the future", nameof(createdDate));
            }

            if (createdDate != null && createdDate <= DateTimeOffset.MinValue)
            {
                throw new DomainValidationException("Invalid value.", nameof(createdDate));
            }

            if (createdDate != null && createdDate >= DateTimeOffset.MaxValue)
            {
                throw new DomainValidationException("Invalid value.", nameof(createdDate));
            }

            this.Value = createdDate ?? DateTimeOffset.UtcNow;
        }

        public static implicit operator CreatedDate(DateTimeOffset? value) => new CreatedDate(value);
        public static implicit operator DateTimeOffset?(CreatedDate created) => created.Value;

        public DateTimeOffset Value { get; }

        public static bool operator <(CreatedDate left, CreatedDate right)
        {
            return left.Value < right.Value;
        }

        public static bool operator >(CreatedDate left, CreatedDate right)
        {
            return left.Value > right.Value;
        }

        public static bool operator <=(CreatedDate left, CreatedDate right)
        {
            return (left < right || left == right);
        }

        public static bool operator >=(CreatedDate left, CreatedDate right)
        {
            return (left > right || left == right);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
