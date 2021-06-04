using ProjectName.Application.Domain.Common;
using System.Collections.Generic;
using static System.String;

namespace ProjectName.Application.Domain.ValueObjects
{
    public sealed class Note : ValueObject
    {
        private Note() { }


        public Note(string text)
        {
            if (!IsNullOrWhiteSpace(text) && text.Length < 2)
            {
                throw new System.ArgumentOutOfRangeException(nameof(text));
            }

            Value = IsNullOrWhiteSpace(text) ? null : text;
        }

        public static implicit operator Note(string value) => new Note(value);
        public static implicit operator string(Note note) => note.Value;

        public string Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
