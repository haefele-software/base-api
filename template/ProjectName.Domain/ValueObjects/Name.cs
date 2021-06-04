using ProjectName.Application.Domain.Common;
using System.Collections.Generic;
using static System.String;

namespace ProjectName.Application.Domain.ValueObjects
{
    public sealed class Name : ValueObject
    {
        private Name() { }

        public Name(string text)
        {
            if (!IsNullOrWhiteSpace(text) && text.Length > 250)
            {
                throw new System.ArgumentOutOfRangeException(nameof(text));
            }

            if (!IsNullOrWhiteSpace(text) && text.Length < 2)
            {
                throw new System.ArgumentOutOfRangeException(nameof(text));
            }

            Value = IsNullOrWhiteSpace(text) ? null : text;
        }

        public static implicit operator Name(string name) => new Name(name);
        public static implicit operator string(Name name) => name.Value;

        public string Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.ToLowerInvariant();
        }
    }
}
