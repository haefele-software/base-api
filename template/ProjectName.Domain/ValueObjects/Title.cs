using ProjectName.Application.Domain.Common;
using System.Collections.Generic;
using static System.String;

namespace ProjectName.Application.Domain.ValueObjects
{
    public sealed class Title : ValueObject
    {
        private Title() { }

        public Title(string text)
        {
            if (IsNullOrWhiteSpace(text))
            {
                throw new System.ArgumentOutOfRangeException(nameof(text));
            }

            Value = text;
        }

        public static implicit operator Title(string value) => new Title(value);
        public static implicit operator string(Title title) => title.Value;


        public string Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.ToLower();
        }
    }
}
