using System;

namespace ObjPropsDynamicSetter.Test.Unit.Models
{
    public sealed class InternalTestClass : IEquatable<InternalTestClass>
    {
        public string InternalStringValue { get; set; }

        public int IntValue { get; set; }

        public bool Equals(InternalTestClass other) => InternalStringValue == other?.InternalStringValue && IntValue == other?.IntValue;

        public override bool Equals(object obj) => obj is InternalTestClass @model && Equals(@model);

        public override int GetHashCode() => HashCode.Combine(InternalStringValue, IntValue);
    }
}
