using ProjectName.Application.Domain.Common;
using System.Collections.Generic;

namespace ProjectName.Application.Domain.ValueObjects
{
    public sealed class SerializedEntityState : ValueObject
    {
        private SerializedEntityState() { }

        public SerializedEntityState(string serialized)
        {
            Value = serialized;
        }

        public static implicit operator SerializedEntityState(string serialized) => new SerializedEntityState(serialized);
        public static implicit operator string(SerializedEntityState serialized) => serialized.Value;

        public string Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
