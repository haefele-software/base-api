using ProjectName.Application.Domain.Common;
using ProjectName.Application.Domain.Exceptions;
using System.Collections.Generic;

namespace ProjectName.Application.Domain.ValueObjects
{
    public sealed class EmailAddress : ValueObject
    {
        private EmailAddress() { }

        public EmailAddress(string emailAddress)
        {
            if (emailAddress == null)
            {
                throw new DomainValidationException("Invalid value. Cannot be null.", nameof(emailAddress));
            }

            if (!emailAddress.Contains('@'))
            {
                throw new DomainValidationException("Invalid value.", nameof(emailAddress));
            }

            if (!emailAddress.Contains('.'))
            {
                throw new DomainValidationException("Invalid value.", nameof(emailAddress));
            }

            this.Value = emailAddress;
        }

        public static implicit operator EmailAddress(string value) => new EmailAddress(value);
        public static implicit operator string(EmailAddress emailAddress) => emailAddress.Value;

        public string Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.ToLowerInvariant();
        }
    }
}
