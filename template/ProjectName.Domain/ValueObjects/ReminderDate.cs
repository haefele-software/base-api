using ProjectName.Application.Domain.Common;
using ProjectName.Application.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace ProjectName.Application.Domain.ValueObjects
{
    public sealed class ReminderDate : ValueObject
    {
        private ReminderDate() { }

        public ReminderDate(DateTimeOffset? reminderDate = null)
        {
            if (reminderDate != null && reminderDate < DateTimeOffset.UtcNow)
            {
                throw new DomainValidationException("Invalid value. Reminder date cannot be in the past.", nameof(reminderDate));
            }

            if (reminderDate != null && reminderDate <= DateTimeOffset.MinValue)
            {
                throw new DomainValidationException("Invalid value.", nameof(reminderDate));
            }

            if (reminderDate != null && reminderDate > DateTimeOffset.MaxValue)
            {
                throw new DomainValidationException("Invalid value.", nameof(reminderDate));
            }

            this.Value = reminderDate;
        }

        public static implicit operator ReminderDate(DateTimeOffset? value) => new ReminderDate(value);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public DateTimeOffset? Value { get; }

        public static bool operator <(ReminderDate left, ReminderDate right)
        {
            return left.Value < right.Value;
        }

        public static bool operator >(ReminderDate left, ReminderDate right)
        {
            return left.Value > right.Value;
        }

        public static bool operator <=(ReminderDate left, ReminderDate right)
        {
            return (left < right || left == right);
        }

        public static bool operator >=(ReminderDate left, ReminderDate right)
        {
            return (left > right || left == right);
        }


    }
}
